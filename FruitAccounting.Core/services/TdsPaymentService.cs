using FruitAccounting.Data.Context;
using FruitAccounting.Data.Entities;
using FruitAccounting.Data.Enums;
using Microsoft.EntityFrameworkCore;

namespace FruitAccounting.Core.services;

public class TdsPaymentService
{
    private readonly IDbContextFactory<FruitAccountingContext> _contextFactory;

    public TdsPaymentService(IDbContextFactory<FruitAccountingContext> contextFactory)
    {
        _contextFactory = contextFactory;
    }

    public async Task<List<TdsPayment>> GetAllTdsPaymentsAsync(long financialYearId)
    {
        using var context = _contextFactory.CreateDbContext();
        return await context.TdsPayments
            .AsNoTracking()
            .Include(t => t.TdsAccount)
            .Include(t => t.Daybook)
            .Where(t => t.FinancialYearId == financialYearId)
            .OrderByDescending(t => t.TdsPaymentNo)
            .ToListAsync();
    }

    public async Task<int> GetNextTdsPaymentNoAsync(long financialYearId)
    {
        using var context = _contextFactory.CreateDbContext();
        var max = await context.TdsPayments
            .Where(t => t.FinancialYearId == financialYearId)
            .MaxAsync(t => (int?)t.TdsPaymentNo);
        return (max ?? 0) + 1;
    }

    // Deductions still awaiting remittance - the pool a new TDS Payment challan can be built from.
    // On an existing (already-saved) challan, its own already-linked lines are included too so they
    // still show up (and can be unchecked) when re-opening that record for edit.
    public async Task<List<TdsPurchaseDeduction>> GetAvailableDeductionsAsync(long financialYearId, long? forTdsPaymentId = null)
    {
        using var context = _contextFactory.CreateDbContext();
        return await context.TdsPurchaseDeductions
            .AsNoTracking()
            .Include(d => d.Supplier)
            .Include(d => d.Payment)
            .Where(d => d.FinancialYearId == financialYearId
                && (d.TdsPaymentId == null || d.TdsPaymentId == forTdsPaymentId))
            .OrderBy(d => d.DeductedAt)
            .ToListAsync();
    }

    public class TdsPaymentInput
    {
        public DateOnly PaymentDate { get; set; }
        public long TdsAccountId { get; set; }
        public long DaybookId { get; set; }
        public string? BsrCode { get; set; }
        public string? ChallanSerialNo { get; set; }
        public decimal InterestRatePct { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal InterestAmount { get; set; }
        public decimal FeesAmount { get; set; }
        public decimal PenaltyAmount { get; set; }
        public long? InterestAccountId { get; set; }
        public long? FeesAccountId { get; set; }
        public long? PenaltyAccountId { get; set; }
        public string? Remarks { get; set; }
        public long FinancialYearId { get; set; }
        public long? CreatedBy { get; set; }
        public List<long> DeductionIds { get; set; } = new();
    }

    public async Task<(bool success, string message)> CreateTdsPaymentAsync(TdsPaymentInput input)
    {
        using var context = _contextFactory.CreateDbContext();
        await using var transaction = await context.Database.BeginTransactionAsync();
        try
        {
            var (valid, validationMessage, totalAmount, daybook) = await ValidateAsync(context, input);
            if (!valid)
                return (false, validationMessage);

            var nextNo = await context.TdsPayments
                .Where(t => t.FinancialYearId == input.FinancialYearId)
                .MaxAsync(t => (int?)t.TdsPaymentNo) ?? 0;
            nextNo++;

            var tdsPayment = new TdsPayment
            {
                FinancialYearId = input.FinancialYearId,
                TdsPaymentNo = nextNo,
                PaymentDate = input.PaymentDate,
                TdsAccountId = input.TdsAccountId,
                DaybookId = input.DaybookId,
                BsrCode = input.BsrCode,
                ChallanSerialNo = input.ChallanSerialNo,
                InterestRatePct = input.InterestRatePct,
                TaxAmount = input.TaxAmount,
                InterestAmount = input.InterestAmount,
                FeesAmount = input.FeesAmount,
                PenaltyAmount = input.PenaltyAmount,
                InterestAccountId = input.InterestAccountId,
                FeesAccountId = input.FeesAccountId,
                PenaltyAccountId = input.PenaltyAccountId,
                TotalAmount = totalAmount,
                Remarks = input.Remarks,
                CreatedBy = input.CreatedBy,
                CreatedAt = DateTime.UtcNow
            };

            context.TdsPayments.Add(tdsPayment);
            await context.SaveChangesAsync();

            if (input.DeductionIds.Count > 0)
            {
                var deductions = await context.TdsPurchaseDeductions
                    .Where(d => input.DeductionIds.Contains(d.TdsDeductionId) && d.FinancialYearId == input.FinancialYearId)
                    .ToListAsync();
                foreach (var d in deductions)
                    d.TdsPaymentId = tdsPayment.TdsPaymentId;
            }

            var (postOk, postMessage) = await BuildAndAddLedgerEntriesAsync(context, tdsPayment, daybook!.LinkedAccountId!.Value);
            if (!postOk)
            {
                await transaction.RollbackAsync();
                return (false, postMessage);
            }

            await context.SaveChangesAsync();
            await transaction.CommitAsync();
            return (true, $"TDS Payment #{nextNo} created successfully");
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            return (false, $"Error creating TDS Payment: {ex.Message}");
        }
    }

    public async Task<(bool success, string message)> UpdateTdsPaymentAsync(long tdsPaymentId, TdsPaymentInput input)
    {
        using var context = _contextFactory.CreateDbContext();
        await using var transaction = await context.Database.BeginTransactionAsync();
        try
        {
            var tdsPayment = await context.TdsPayments.FirstOrDefaultAsync(t => t.TdsPaymentId == tdsPaymentId);
            if (tdsPayment == null)
                return (false, "TDS Payment not found");

            var (valid, validationMessage, totalAmount, daybook) = await ValidateAsync(context, input);
            if (!valid)
                return (false, validationMessage);

            tdsPayment.PaymentDate = input.PaymentDate;
            tdsPayment.TdsAccountId = input.TdsAccountId;
            tdsPayment.DaybookId = input.DaybookId;
            tdsPayment.BsrCode = input.BsrCode;
            tdsPayment.ChallanSerialNo = input.ChallanSerialNo;
            tdsPayment.InterestRatePct = input.InterestRatePct;
            tdsPayment.TaxAmount = input.TaxAmount;
            tdsPayment.InterestAmount = input.InterestAmount;
            tdsPayment.FeesAmount = input.FeesAmount;
            tdsPayment.PenaltyAmount = input.PenaltyAmount;
            tdsPayment.InterestAccountId = input.InterestAccountId;
            tdsPayment.FeesAccountId = input.FeesAccountId;
            tdsPayment.PenaltyAccountId = input.PenaltyAccountId;
            tdsPayment.TotalAmount = totalAmount;
            tdsPayment.Remarks = input.Remarks;

            // Unlink whatever this challan previously covered, then relink to the current selection
            var previouslyLinked = await context.TdsPurchaseDeductions
                .Where(d => d.TdsPaymentId == tdsPaymentId)
                .ToListAsync();
            foreach (var d in previouslyLinked)
                d.TdsPaymentId = null;

            if (input.DeductionIds.Count > 0)
            {
                var deductions = await context.TdsPurchaseDeductions
                    .Where(d => input.DeductionIds.Contains(d.TdsDeductionId) && d.FinancialYearId == input.FinancialYearId)
                    .ToListAsync();
                foreach (var d in deductions)
                    d.TdsPaymentId = tdsPaymentId;
            }

            var oldEntries = await context.LedgerEntries
                .Where(e => e.VoucherId == tdsPaymentId && e.VoucherType == VoucherType.TdsPayment)
                .ToListAsync();
            context.LedgerEntries.RemoveRange(oldEntries);

            await context.SaveChangesAsync();

            var (postOk, postMessage) = await BuildAndAddLedgerEntriesAsync(context, tdsPayment, daybook!.LinkedAccountId!.Value);
            if (!postOk)
            {
                await transaction.RollbackAsync();
                return (false, postMessage);
            }

            await context.SaveChangesAsync();
            await transaction.CommitAsync();
            return (true, "TDS Payment updated successfully");
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            return (false, $"Error updating TDS Payment: {ex.Message}");
        }
    }

    public async Task<(bool success, string message)> DeleteTdsPaymentAsync(long tdsPaymentId)
    {
        using var context = _contextFactory.CreateDbContext();
        await using var transaction = await context.Database.BeginTransactionAsync();
        try
        {
            var tdsPayment = await context.TdsPayments.FirstOrDefaultAsync(t => t.TdsPaymentId == tdsPaymentId);
            if (tdsPayment == null)
                return (false, "TDS Payment not found");

            var linked = await context.TdsPurchaseDeductions
                .Where(d => d.TdsPaymentId == tdsPaymentId)
                .ToListAsync();
            foreach (var d in linked)
                d.TdsPaymentId = null;

            var entries = await context.LedgerEntries
                .Where(e => e.VoucherId == tdsPaymentId && e.VoucherType == VoucherType.TdsPayment)
                .ToListAsync();
            context.LedgerEntries.RemoveRange(entries);

            context.TdsPayments.Remove(tdsPayment);
            await context.SaveChangesAsync();
            await transaction.CommitAsync();
            return (true, "TDS Payment deleted successfully");
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            return (false, $"Error deleting TDS Payment: {ex.Message}");
        }
    }

    private async Task<(bool valid, string message, decimal totalAmount, Daybook? daybook)> ValidateAsync(
        FruitAccountingContext context, TdsPaymentInput input)
    {
        if (input.TaxAmount <= 0)
            return (false, "Tax amount must be greater than zero", 0, null);

        var daybook = await context.Daybooks.AsNoTracking().FirstOrDefaultAsync(d => d.DaybookId == input.DaybookId);
        if (daybook == null)
            return (false, "Daybook not found", 0, null);
        if (daybook.LinkedAccountId == null)
            return (false, $"Daybook '{daybook.Name}' has no Linked Account set - cannot post to the ledger. Set one in the Daybook master first.", 0, null);

        if (input.InterestAmount != 0 && input.InterestAccountId == null)
            return (false, "Interest amount entered, but no Interest account selected", 0, null);
        if (input.FeesAmount != 0 && input.FeesAccountId == null)
            return (false, "Fees amount entered, but no Fees account selected", 0, null);
        if (input.PenaltyAmount != 0 && input.PenaltyAccountId == null)
            return (false, "Other Penalty amount entered, but no Penalty account selected", 0, null);

        var totalAmount = input.TaxAmount + input.InterestAmount + input.FeesAmount + input.PenaltyAmount;
        return (true, "", totalAmount, daybook);
    }

    private async Task<(bool success, string message)> BuildAndAddLedgerEntriesAsync(
        FruitAccountingContext context, TdsPayment tdsPayment, long cashBankAccountId)
    {
        var entries = new List<LedgerEntry>();

        // Clears the TDS Payable liability that built up from individual purchase deductions
        entries.Add(new LedgerEntry
        {
            FinancialYearId = tdsPayment.FinancialYearId,
            EntryDate = tdsPayment.PaymentDate,
            AccountId = tdsPayment.TdsAccountId,
            Debit = tdsPayment.TaxAmount,
            Credit = 0,
            VoucherId = tdsPayment.TdsPaymentId,
            VoucherType = VoucherType.TdsPayment,
            Narration = $"TDS Payment #{tdsPayment.TdsPaymentNo} - Challan {tdsPayment.ChallanSerialNo}",
            ContraAccountId = cashBankAccountId,
            CreatedAt = DateTime.UtcNow
        });

        if (tdsPayment.InterestAmount != 0 && tdsPayment.InterestAccountId != null)
        {
            entries.Add(new LedgerEntry
            {
                FinancialYearId = tdsPayment.FinancialYearId,
                EntryDate = tdsPayment.PaymentDate,
                AccountId = tdsPayment.InterestAccountId.Value,
                Debit = tdsPayment.InterestAmount,
                Credit = 0,
                VoucherId = tdsPayment.TdsPaymentId,
                VoucherType = VoucherType.TdsPayment,
                Narration = $"Interest (201(1A)) on TDS Payment #{tdsPayment.TdsPaymentNo}",
                ContraAccountId = cashBankAccountId,
                CreatedAt = DateTime.UtcNow
            });
        }

        if (tdsPayment.FeesAmount != 0 && tdsPayment.FeesAccountId != null)
        {
            entries.Add(new LedgerEntry
            {
                FinancialYearId = tdsPayment.FinancialYearId,
                EntryDate = tdsPayment.PaymentDate,
                AccountId = tdsPayment.FeesAccountId.Value,
                Debit = tdsPayment.FeesAmount,
                Credit = 0,
                VoucherId = tdsPayment.TdsPaymentId,
                VoucherType = VoucherType.TdsPayment,
                Narration = $"Late filing fee (234E) on TDS Payment #{tdsPayment.TdsPaymentNo}",
                ContraAccountId = cashBankAccountId,
                CreatedAt = DateTime.UtcNow
            });
        }

        if (tdsPayment.PenaltyAmount != 0 && tdsPayment.PenaltyAccountId != null)
        {
            entries.Add(new LedgerEntry
            {
                FinancialYearId = tdsPayment.FinancialYearId,
                EntryDate = tdsPayment.PaymentDate,
                AccountId = tdsPayment.PenaltyAccountId.Value,
                Debit = tdsPayment.PenaltyAmount,
                Credit = 0,
                VoucherId = tdsPayment.TdsPaymentId,
                VoucherType = VoucherType.TdsPayment,
                Narration = $"Penalty (271H) on TDS Payment #{tdsPayment.TdsPaymentNo}",
                ContraAccountId = cashBankAccountId,
                CreatedAt = DateTime.UtcNow
            });
        }

        // The actual cash/bank amount paid to the government
        entries.Add(new LedgerEntry
        {
            FinancialYearId = tdsPayment.FinancialYearId,
            EntryDate = tdsPayment.PaymentDate,
            AccountId = cashBankAccountId,
            Debit = 0,
            Credit = tdsPayment.TotalAmount,
            VoucherId = tdsPayment.TdsPaymentId,
            VoucherType = VoucherType.TdsPayment,
            Narration = $"TDS Payment #{tdsPayment.TdsPaymentNo} - Challan {tdsPayment.ChallanSerialNo}",
            ContraAccountId = tdsPayment.TdsAccountId,
            CreatedAt = DateTime.UtcNow
        });

        context.LedgerEntries.AddRange(entries);
        return (true, "");
    }
}
