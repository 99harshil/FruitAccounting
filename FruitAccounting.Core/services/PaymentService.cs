using FruitAccounting.Data.Context;
using FruitAccounting.Data.Entities;
using FruitAccounting.Data.Enums;
using Microsoft.EntityFrameworkCore;

namespace FruitAccounting.Core.services;

public class PaymentService
{
    private const string VatavExpensesKey = "VATAV_EXPENSES_ACCOUNT_ID";
    private const string HamaliPayableKey = "HAMALI_PAYABLE_ACCOUNT_ID";
    private const string TdsPayableKey = "TDS_PAYABLE_ACCOUNT_ID";

    private readonly IDbContextFactory<FruitAccountingContext> _contextFactory;
    private readonly Tds194QService _tds194QService;

    public PaymentService(IDbContextFactory<FruitAccountingContext> contextFactory, Tds194QService tds194QService)
    {
        _contextFactory = contextFactory;
        _tds194QService = tds194QService;
    }

    public async Task<List<Payment>> GetAllPaymentsAsync(long financialYearId, char bookType)
    {
        using var context = _contextFactory.CreateDbContext();
        return await context.Payments
            .AsNoTracking()
            .Include(p => p.Account)
            .Include(p => p.Daybook)
            .Where(p => p.FinancialYearId == financialYearId
                && p.Daybook.BookType == bookType
                && !p.IsFreightPayment)
            .OrderByDescending(p => p.PaymentNo)
            .ToListAsync();
    }

    public async Task<int> GetNextPaymentNoAsync(long financialYearId)
    {
        using var context = _contextFactory.CreateDbContext();
        var max = await context.Payments
            .Where(p => p.FinancialYearId == financialYearId)
            .MaxAsync(p => (int?)p.PaymentNo);
        return (max ?? 0) + 1;
    }

    public class PaymentInput
    {
        public long AccountId { get; set; }
        public long DaybookId { get; set; }
        public DateOnly PaymentDate { get; set; }
        public decimal Amount { get; set; } // gross bill amount being settled - debited to the party
        public decimal Vatav { get; set; }
        public decimal Hamali { get; set; }
        public PaymentMode Mode { get; set; }
        public string? ChequeNo { get; set; }
        public DateOnly? ChequeDate { get; set; }
        public string? BankName { get; set; }
        public string? BankBranch { get; set; }
        public string? Remarks { get; set; }
        public bool IsReturned { get; set; }
        public DateOnly? ReturnedDate { get; set; }
        public long FinancialYearId { get; set; }
        public long? CreatedBy { get; set; }
    }

    public async Task<(bool success, string message)> CreatePaymentAsync(PaymentInput input)
    {
        using var context = _contextFactory.CreateDbContext();
        await using var transaction = await context.Database.BeginTransactionAsync();
        try
        {
            var (valid, validationMessage, totalSettled, daybook) = await ValidateAsync(context, input);
            if (!valid)
                return (false, validationMessage);

            var nextPaymentNo = await context.Payments
                .Where(p => p.FinancialYearId == input.FinancialYearId)
                .MaxAsync(p => (int?)p.PaymentNo) ?? 0;
            nextPaymentNo++;

            // 194Q also triggers on payment (not just on a booked Purchase Bill) - an advance can
            // itself cross the per-supplier ₹50L threshold. The cash actually handed over doesn't
            // change; TDS is recorded as an extra ledger charge against the party instead (see
            // BuildAndAddLedgerEntriesAsync).
            var (tdsRate, cumulativeBefore, taxableExcess, tdsAmount) = await _tds194QService.ComputeAsync(
                context, input.AccountId, input.FinancialYearId, input.Amount, input.PaymentDate,
                isPayment: true, excludingPurchaseBillId: null, excludingPaymentId: null);

            var payment = new Payment
            {
                FinancialYearId = input.FinancialYearId,
                PaymentNo = nextPaymentNo,
                PaymentDate = input.PaymentDate,
                AccountId = input.AccountId,
                DaybookId = input.DaybookId,
                Mode = input.Mode,
                ChequeNo = input.ChequeNo,
                ChequeDate = input.ChequeDate,
                BankName = input.BankName,
                BankBranch = input.BankBranch,
                Amount = input.Amount,
                Vatav = input.Vatav,
                Hamali = input.Hamali,
                TdsAmount = tdsAmount,
                TotalSettled = totalSettled,
                IsReturned = input.IsReturned,
                ReturnedDate = input.ReturnedDate,
                Remarks = input.Remarks,
                CreatedBy = input.CreatedBy,
                CreatedAt = DateTime.UtcNow
            };

            context.Payments.Add(payment);
            await context.SaveChangesAsync();

            if (tdsAmount != 0)
            {
                context.TdsPurchaseDeductions.Add(new TdsPurchaseDeduction
                {
                    FinancialYearId = input.FinancialYearId,
                    SupplierId = input.AccountId,
                    PaymentId = payment.PaymentId,
                    CumulativeBefore = cumulativeBefore,
                    TaxableExcess = taxableExcess,
                    TdsRate = tdsRate,
                    TdsAmount = tdsAmount,
                    DeductedAt = DateTime.UtcNow
                });
            }

            var (postOk, postMessage) = await BuildAndAddLedgerEntriesAsync(context, payment, daybook!.LinkedAccountId!.Value);
            if (!postOk)
            {
                await transaction.RollbackAsync();
                return (false, postMessage);
            }

            await context.SaveChangesAsync();
            await transaction.CommitAsync();
            return (true, $"Payment #{nextPaymentNo} created successfully");
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            return (false, $"Error creating Payment: {ex.Message}");
        }
    }

    public async Task<(bool success, string message)> UpdatePaymentAsync(long paymentId, PaymentInput input)
    {
        using var context = _contextFactory.CreateDbContext();
        await using var transaction = await context.Database.BeginTransactionAsync();
        try
        {
            var payment = await context.Payments.FirstOrDefaultAsync(p => p.PaymentId == paymentId);
            if (payment == null)
                return (false, "Payment not found");

            var (valid, validationMessage, totalSettled, daybook) = await ValidateAsync(context, input);
            if (!valid)
                return (false, validationMessage);

            var (tdsRate, cumulativeBefore, taxableExcess, tdsAmount) = await _tds194QService.ComputeAsync(
                context, input.AccountId, input.FinancialYearId, input.Amount, input.PaymentDate,
                isPayment: true, excludingPurchaseBillId: null, excludingPaymentId: paymentId);

            payment.PaymentDate = input.PaymentDate;
            payment.AccountId = input.AccountId;
            payment.DaybookId = input.DaybookId;
            payment.Mode = input.Mode;
            payment.ChequeNo = input.ChequeNo;
            payment.ChequeDate = input.ChequeDate;
            payment.BankName = input.BankName;
            payment.BankBranch = input.BankBranch;
            payment.Amount = input.Amount;
            payment.Vatav = input.Vatav;
            payment.Hamali = input.Hamali;
            payment.TdsAmount = tdsAmount;
            payment.TotalSettled = totalSettled;
            payment.IsReturned = input.IsReturned;
            payment.ReturnedDate = input.ReturnedDate;
            payment.Remarks = input.Remarks;

            // Remove the old ledger lines for this voucher and repost fresh ones,
            // rather than trying to patch individual lines - simplest way to stay correct.
            var oldEntries = await context.LedgerEntries
                .Where(e => e.VoucherId == paymentId && e.VoucherType == VoucherType.Payment)
                .ToListAsync();
            context.LedgerEntries.RemoveRange(oldEntries);

            var oldDeductions = await context.TdsPurchaseDeductions
                .Where(d => d.PaymentId == paymentId)
                .ToListAsync();
            context.TdsPurchaseDeductions.RemoveRange(oldDeductions);

            if (tdsAmount != 0)
            {
                context.TdsPurchaseDeductions.Add(new TdsPurchaseDeduction
                {
                    FinancialYearId = input.FinancialYearId,
                    SupplierId = input.AccountId,
                    PaymentId = payment.PaymentId,
                    CumulativeBefore = cumulativeBefore,
                    TaxableExcess = taxableExcess,
                    TdsRate = tdsRate,
                    TdsAmount = tdsAmount,
                    DeductedAt = DateTime.UtcNow
                });
            }

            await context.SaveChangesAsync();

            var (postOk, postMessage) = await BuildAndAddLedgerEntriesAsync(context, payment, daybook!.LinkedAccountId!.Value);
            if (!postOk)
            {
                await transaction.RollbackAsync();
                return (false, postMessage);
            }

            await context.SaveChangesAsync();
            await transaction.CommitAsync();
            return (true, "Payment updated successfully");
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            return (false, $"Error updating Payment: {ex.Message}");
        }
    }

    public async Task<(bool success, string message)> DeletePaymentAsync(long paymentId)
    {
        using var context = _contextFactory.CreateDbContext();
        await using var transaction = await context.Database.BeginTransactionAsync();
        try
        {
            var payment = await context.Payments
                .Include(p => p.PaymentAllocations)
                .FirstOrDefaultAsync(p => p.PaymentId == paymentId);

            if (payment == null)
                return (false, "Payment not found");

            if (payment.PaymentAllocations.Any())
                return (false, "Cannot delete Payment that has been allocated against Purchase Bills");

            var entries = await context.LedgerEntries
                .Where(e => e.VoucherId == paymentId && e.VoucherType == VoucherType.Payment)
                .ToListAsync();
            context.LedgerEntries.RemoveRange(entries);

            var deductions = await context.TdsPurchaseDeductions
                .Where(d => d.PaymentId == paymentId)
                .ToListAsync();
            context.TdsPurchaseDeductions.RemoveRange(deductions);

            context.Payments.Remove(payment);
            await context.SaveChangesAsync();
            await transaction.CommitAsync();
            return (true, "Payment deleted successfully");
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            return (false, $"Error deleting Payment: {ex.Message}");
        }
    }

    private async Task<(bool valid, string message, decimal totalSettled, Daybook? daybook)> ValidateAsync(
        FruitAccountingContext context, PaymentInput input)
    {
        if (input.Amount <= 0)
            return (false, "Paid Amount must be greater than zero", 0, null);

        var daybook = await context.Daybooks.AsNoTracking().FirstOrDefaultAsync(d => d.DaybookId == input.DaybookId);
        if (daybook == null)
            return (false, "Daybook not found", 0, null);
        if (daybook.LinkedAccountId == null)
            return (false, $"Daybook '{daybook.Name}' has no Linked Account set - cannot post to the ledger. Set one in the Daybook master first.", 0, null);

        var totalSettled = input.Amount - input.Vatav - input.Hamali;
        if (totalSettled < 0)
            return (false, "Total Amount works out negative - check Vatav / Hamali against Paid Amount", 0, null);

        return (true, "", totalSettled, daybook);
    }

    // Lets the UI show a live TDS estimate as the payment is being entered, using the same
    // combined Payment+Purchase-Bill 194Q cumulative logic that actually runs at save time.
    public async Task<(decimal rate, decimal tdsAmount)> PreviewTdsAsync(long accountId, long financialYearId, decimal amount, DateOnly paymentDate, long? excludingPaymentId)
    {
        using var context = _contextFactory.CreateDbContext();
        var (rate, _, _, tdsAmount) = await _tds194QService.ComputeAsync(context, accountId, financialYearId, amount, paymentDate, isPayment: true, excludingPurchaseBillId: null, excludingPaymentId);
        return (rate, tdsAmount);
    }

    private async Task<(bool success, string message)> BuildAndAddLedgerEntriesAsync(
        FruitAccountingContext context, Payment payment, long cashBankAccountId)
    {
        var entries = new List<LedgerEntry>();

        // The full gross amount clears against the party's outstanding balance
        entries.Add(new LedgerEntry
        {
            FinancialYearId = payment.FinancialYearId,
            EntryDate = payment.PaymentDate,
            AccountId = payment.AccountId,
            Debit = payment.Amount,
            Credit = 0,
            VoucherId = payment.PaymentId,
            VoucherType = VoucherType.Payment,
            Narration = $"Payment #{payment.PaymentNo}",
            ContraAccountId = cashBankAccountId,
            CreatedAt = DateTime.UtcNow
        });

        // The actual cash/bank amount paid out
        entries.Add(new LedgerEntry
        {
            FinancialYearId = payment.FinancialYearId,
            EntryDate = payment.PaymentDate,
            AccountId = cashBankAccountId,
            Debit = 0,
            Credit = payment.TotalSettled,
            VoucherId = payment.PaymentId,
            VoucherType = VoucherType.Payment,
            Narration = $"Payment #{payment.PaymentNo}",
            ContraAccountId = payment.AccountId,
            CreatedAt = DateTime.UtcNow
        });

        if (payment.Vatav != 0)
        {
            var vatavAccountId = await GetConfiguredAccountIdAsync(context, VatavExpensesKey);
            if (vatavAccountId == null)
                return (false, "Vatav amount entered, but no Vatav Expenses account is configured (system_parameters: VATAV_EXPENSES_ACCOUNT_ID)");

            // Discount received from the supplier - credited here, netting against the debit
            // side Receipt posts to the same account when a discount is given to a customer.
            entries.Add(new LedgerEntry
            {
                FinancialYearId = payment.FinancialYearId,
                EntryDate = payment.PaymentDate,
                AccountId = vatavAccountId.Value,
                Debit = 0,
                Credit = payment.Vatav,
                VoucherId = payment.PaymentId,
                VoucherType = VoucherType.Payment,
                Narration = $"Vatav on Payment #{payment.PaymentNo}",
                ContraAccountId = payment.AccountId,
                CreatedAt = DateTime.UtcNow
            });
        }

        if (payment.Hamali != 0)
        {
            var hamaliAccountId = await GetConfiguredAccountIdAsync(context, HamaliPayableKey);
            if (hamaliAccountId == null)
                return (false, "Hamali amount entered, but no Hamali Payable account is configured (system_parameters: HAMALI_PAYABLE_ACCOUNT_ID)");

            entries.Add(new LedgerEntry
            {
                FinancialYearId = payment.FinancialYearId,
                EntryDate = payment.PaymentDate,
                AccountId = hamaliAccountId.Value,
                Debit = 0,
                Credit = payment.Hamali,
                VoucherId = payment.PaymentId,
                VoucherType = VoucherType.Payment,
                Narration = $"Hamali withheld on Payment #{payment.PaymentNo}",
                ContraAccountId = payment.AccountId,
                CreatedAt = DateTime.UtcNow
            });
        }

        if (payment.TdsAmount != 0)
        {
            var tdsPayableAccountId = await GetConfiguredAccountIdAsync(context, TdsPayableKey);
            if (tdsPayableAccountId == null)
                return (false, "TDS amount computed, but no TDS Payable account is configured (system_parameters: TDS_PAYABLE_ACCOUNT_ID)");

            // TDS doesn't reduce the cash actually handed over (payment.Amount/TotalSettled are
            // unaffected) - it's recorded as an extra charge against the party instead: a further
            // Debit on top of the Amount debit above, paired with a Credit to TDS Payable for what
            // gets remitted to the government on their behalf.
            entries.Add(new LedgerEntry
            {
                FinancialYearId = payment.FinancialYearId,
                EntryDate = payment.PaymentDate,
                AccountId = payment.AccountId,
                Debit = payment.TdsAmount,
                Credit = 0,
                VoucherId = payment.PaymentId,
                VoucherType = VoucherType.Payment,
                Narration = $"194Q TDS on Payment #{payment.PaymentNo}",
                ContraAccountId = tdsPayableAccountId.Value,
                CreatedAt = DateTime.UtcNow
            });

            entries.Add(new LedgerEntry
            {
                FinancialYearId = payment.FinancialYearId,
                EntryDate = payment.PaymentDate,
                AccountId = tdsPayableAccountId.Value,
                Debit = 0,
                Credit = payment.TdsAmount,
                VoucherId = payment.PaymentId,
                VoucherType = VoucherType.Payment,
                Narration = $"194Q TDS on Payment #{payment.PaymentNo}",
                ContraAccountId = payment.AccountId,
                CreatedAt = DateTime.UtcNow
            });
        }

        context.LedgerEntries.AddRange(entries);
        return (true, "");
    }

    private async Task<long?> GetConfiguredAccountIdAsync(FruitAccountingContext context, string key)
    {
        var param = await context.SystemParameters.AsNoTracking().FirstOrDefaultAsync(p => p.ParameterKey == key);
        if (param == null) return null;
        return long.TryParse(param.ParameterValue, out var id) ? id : null;
    }
}
