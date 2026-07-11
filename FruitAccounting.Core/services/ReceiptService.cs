using FruitAccounting.Data.Context;
using FruitAccounting.Data.Entities;
using FruitAccounting.Data.Enums;
using Microsoft.EntityFrameworkCore;

namespace FruitAccounting.Core.services;

public class ReceiptService
{
    private const string VatavExpensesKey = "VATAV_EXPENSES_ACCOUNT_ID";
    private const string RateDifferenceKey = "RATE_DIFFERENCE_ACCOUNT_ID";
    private const string TdsReceivableKey = "TDS_RECEIVABLE_ACCOUNT_ID";

    private readonly IDbContextFactory<FruitAccountingContext> _contextFactory;

    public ReceiptService(IDbContextFactory<FruitAccountingContext> contextFactory)
    {
        _contextFactory = contextFactory;
    }

    public async Task<List<Receipt>> GetAllReceiptsAsync(long financialYearId)
    {
        using var context = _contextFactory.CreateDbContext();
        return await context.Receipts
            .AsNoTracking()
            .Include(r => r.Account)
            .Include(r => r.Daybook)
            .Where(r => r.FinancialYearId == financialYearId)
            .OrderByDescending(r => r.ReceiptNo)
            .ToListAsync();
    }

    public async Task<int> GetNextReceiptNoAsync(long financialYearId)
    {
        using var context = _contextFactory.CreateDbContext();
        var max = await context.Receipts
            .Where(r => r.FinancialYearId == financialYearId)
            .MaxAsync(r => (int?)r.ReceiptNo);
        return (max ?? 0) + 1;
    }

    public class ReceiptInput
    {
        public long AccountId { get; set; }
        public long DaybookId { get; set; }
        public DateOnly ReceiptDate { get; set; }
        public decimal Amount { get; set; } // gross bill amount - credited to the party
        public decimal Vatav { get; set; }
        public decimal TdsAmount { get; set; }
        public decimal RoundingDiff { get; set; }
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

    public async Task<(bool success, string message)> CreateReceiptAsync(ReceiptInput input)
    {
        using var context = _contextFactory.CreateDbContext();
        await using var transaction = await context.Database.BeginTransactionAsync();
        try
        {
            var (valid, validationMessage, totalSettled, daybook) = await ValidateAsync(context, input);
            if (!valid)
                return (false, validationMessage);

            var nextReceiptNo = await context.Receipts
                .Where(r => r.FinancialYearId == input.FinancialYearId)
                .MaxAsync(r => (int?)r.ReceiptNo) ?? 0;
            nextReceiptNo++;

            var receipt = new Receipt
            {
                FinancialYearId = input.FinancialYearId,
                ReceiptNo = nextReceiptNo,
                ReceiptDate = input.ReceiptDate,
                AccountId = input.AccountId,
                DaybookId = input.DaybookId,
                Mode = input.Mode,
                ChequeNo = input.ChequeNo,
                ChequeDate = input.ChequeDate,
                BankName = input.BankName,
                BankBranch = input.BankBranch,
                Amount = input.Amount,
                Vatav = input.Vatav,
                TdsAmount = input.TdsAmount,
                RoundingDiff = input.RoundingDiff,
                TotalSettled = totalSettled,
                IsReturned = input.IsReturned,
                ReturnedDate = input.ReturnedDate,
                Remarks = input.Remarks,
                CreatedBy = input.CreatedBy,
                CreatedAt = DateTime.UtcNow
            };

            context.Receipts.Add(receipt);
            await context.SaveChangesAsync();

            var (postOk, postMessage) = await BuildAndAddLedgerEntriesAsync(context, receipt, daybook!.LinkedAccountId!.Value);
            if (!postOk)
            {
                await transaction.RollbackAsync();
                return (false, postMessage);
            }

            await context.SaveChangesAsync();
            await transaction.CommitAsync();
            return (true, $"Receipt #{nextReceiptNo} created successfully");
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            return (false, $"Error creating Receipt: {ex.Message}");
        }
    }

    public async Task<(bool success, string message)> UpdateReceiptAsync(long receiptId, ReceiptInput input)
    {
        using var context = _contextFactory.CreateDbContext();
        await using var transaction = await context.Database.BeginTransactionAsync();
        try
        {
            var receipt = await context.Receipts.FirstOrDefaultAsync(r => r.ReceiptId == receiptId);
            if (receipt == null)
                return (false, "Receipt not found");

            var (valid, validationMessage, totalSettled, daybook) = await ValidateAsync(context, input);
            if (!valid)
                return (false, validationMessage);

            receipt.ReceiptDate = input.ReceiptDate;
            receipt.AccountId = input.AccountId;
            receipt.DaybookId = input.DaybookId;
            receipt.Mode = input.Mode;
            receipt.ChequeNo = input.ChequeNo;
            receipt.ChequeDate = input.ChequeDate;
            receipt.BankName = input.BankName;
            receipt.BankBranch = input.BankBranch;
            receipt.Amount = input.Amount;
            receipt.Vatav = input.Vatav;
            receipt.TdsAmount = input.TdsAmount;
            receipt.RoundingDiff = input.RoundingDiff;
            receipt.TotalSettled = totalSettled;
            receipt.IsReturned = input.IsReturned;
            receipt.ReturnedDate = input.ReturnedDate;
            receipt.Remarks = input.Remarks;

            // Remove the old ledger lines for this voucher and repost fresh ones,
            // rather than trying to patch individual lines - simplest way to stay correct.
            var oldEntries = await context.LedgerEntries
                .Where(e => e.VoucherId == receiptId && e.VoucherType == VoucherType.Receipt)
                .ToListAsync();
            context.LedgerEntries.RemoveRange(oldEntries);

            await context.SaveChangesAsync();

            var (postOk, postMessage) = await BuildAndAddLedgerEntriesAsync(context, receipt, daybook!.LinkedAccountId!.Value);
            if (!postOk)
            {
                await transaction.RollbackAsync();
                return (false, postMessage);
            }

            await context.SaveChangesAsync();
            await transaction.CommitAsync();
            return (true, "Receipt updated successfully");
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            return (false, $"Error updating Receipt: {ex.Message}");
        }
    }

    public async Task<(bool success, string message)> DeleteReceiptAsync(long receiptId)
    {
        using var context = _contextFactory.CreateDbContext();
        await using var transaction = await context.Database.BeginTransactionAsync();
        try
        {
            var receipt = await context.Receipts
                .Include(r => r.ReceiptAllocations)
                .FirstOrDefaultAsync(r => r.ReceiptId == receiptId);

            if (receipt == null)
                return (false, "Receipt not found");

            if (receipt.ReceiptAllocations.Any())
                return (false, "Cannot delete Receipt that has been allocated against Sales Bills");

            var entries = await context.LedgerEntries
                .Where(e => e.VoucherId == receiptId && e.VoucherType == VoucherType.Receipt)
                .ToListAsync();
            context.LedgerEntries.RemoveRange(entries);

            context.Receipts.Remove(receipt);
            await context.SaveChangesAsync();
            await transaction.CommitAsync();
            return (true, "Receipt deleted successfully");
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            return (false, $"Error deleting Receipt: {ex.Message}");
        }
    }

    private async Task<(bool valid, string message, decimal totalSettled, Daybook? daybook)> ValidateAsync(
        FruitAccountingContext context, ReceiptInput input)
    {
        if (input.Amount <= 0)
            return (false, "Rec. Amount must be greater than zero", 0, null);

        var daybook = await context.Daybooks.AsNoTracking().FirstOrDefaultAsync(d => d.DaybookId == input.DaybookId);
        if (daybook == null)
            return (false, "Daybook not found", 0, null);
        if (daybook.LinkedAccountId == null)
            return (false, $"Daybook '{daybook.Name}' has no Linked Account set - cannot post to the ledger. Set one in the Daybook master first.", 0, null);

        var totalSettled = input.Amount - input.Vatav - input.TdsAmount - input.RoundingDiff;
        if (totalSettled < 0)
            return (false, "Total Amount works out negative - check Vatav / TDS / Difference against Rec. Amount", 0, null);

        return (true, "", totalSettled, daybook);
    }

    private async Task<(bool success, string message)> BuildAndAddLedgerEntriesAsync(
        FruitAccountingContext context, Receipt receipt, long cashBankAccountId)
    {
        var entries = new List<LedgerEntry>();

        // The full gross amount clears from the party's outstanding balance
        entries.Add(new LedgerEntry
        {
            FinancialYearId = receipt.FinancialYearId,
            EntryDate = receipt.ReceiptDate,
            AccountId = receipt.AccountId,
            Debit = 0,
            Credit = receipt.Amount,
            VoucherId = receipt.ReceiptId,
            VoucherType = VoucherType.Receipt,
            Narration = $"Receipt #{receipt.ReceiptNo}",
            ContraAccountId = cashBankAccountId,
            CreatedAt = DateTime.UtcNow
        });

        // The actual cash/bank amount received
        entries.Add(new LedgerEntry
        {
            FinancialYearId = receipt.FinancialYearId,
            EntryDate = receipt.ReceiptDate,
            AccountId = cashBankAccountId,
            Debit = receipt.TotalSettled,
            Credit = 0,
            VoucherId = receipt.ReceiptId,
            VoucherType = VoucherType.Receipt,
            Narration = $"Receipt #{receipt.ReceiptNo}",
            ContraAccountId = receipt.AccountId,
            CreatedAt = DateTime.UtcNow
        });

        if (receipt.Vatav != 0)
        {
            var vatavAccountId = await GetConfiguredAccountIdAsync(context, VatavExpensesKey);
            if (vatavAccountId == null)
                return (false, "Vatav amount entered, but no Vatav Expenses account is configured (system_parameters: VATAV_EXPENSES_ACCOUNT_ID)");

            entries.Add(new LedgerEntry
            {
                FinancialYearId = receipt.FinancialYearId,
                EntryDate = receipt.ReceiptDate,
                AccountId = vatavAccountId.Value,
                Debit = receipt.Vatav,
                Credit = 0,
                VoucherId = receipt.ReceiptId,
                VoucherType = VoucherType.Receipt,
                Narration = $"Vatav on Receipt #{receipt.ReceiptNo}",
                ContraAccountId = receipt.AccountId,
                CreatedAt = DateTime.UtcNow
            });
        }

        if (receipt.TdsAmount != 0)
        {
            var tdsAccountId = await GetConfiguredAccountIdAsync(context, TdsReceivableKey);
            if (tdsAccountId == null)
                return (false, "TDS amount entered, but no TDS Receivable account is configured (system_parameters: TDS_RECEIVABLE_ACCOUNT_ID)");

            entries.Add(new LedgerEntry
            {
                FinancialYearId = receipt.FinancialYearId,
                EntryDate = receipt.ReceiptDate,
                AccountId = tdsAccountId.Value,
                Debit = receipt.TdsAmount,
                Credit = 0,
                VoucherId = receipt.ReceiptId,
                VoucherType = VoucherType.Receipt,
                Narration = $"TDS deducted on Receipt #{receipt.ReceiptNo}",
                ContraAccountId = receipt.AccountId,
                CreatedAt = DateTime.UtcNow
            });
        }

        if (receipt.RoundingDiff != 0)
        {
            var rateDiffAccountId = await GetConfiguredAccountIdAsync(context, RateDifferenceKey);
            if (rateDiffAccountId == null)
                return (false, "Difference amount entered, but no Rate Difference account is configured (system_parameters: RATE_DIFFERENCE_ACCOUNT_ID)");

            bool isPositive = receipt.RoundingDiff > 0;
            entries.Add(new LedgerEntry
            {
                FinancialYearId = receipt.FinancialYearId,
                EntryDate = receipt.ReceiptDate,
                AccountId = rateDiffAccountId.Value,
                Debit = isPositive ? receipt.RoundingDiff : 0,
                Credit = isPositive ? 0 : -receipt.RoundingDiff,
                VoucherId = receipt.ReceiptId,
                VoucherType = VoucherType.Receipt,
                Narration = $"Rounding on Receipt #{receipt.ReceiptNo}",
                ContraAccountId = receipt.AccountId,
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
