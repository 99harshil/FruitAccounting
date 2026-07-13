using FruitAccounting.Data.Context;
using FruitAccounting.Data.Entities;
using FruitAccounting.Data.Enums;
using Microsoft.EntityFrameworkCore;

namespace FruitAccounting.Core.services;

public class JournalService
{
    private readonly IDbContextFactory<FruitAccountingContext> _contextFactory;

    public JournalService(IDbContextFactory<FruitAccountingContext> contextFactory)
    {
        _contextFactory = contextFactory;
    }

    public async Task<List<JournalVoucher>> GetAllJournalVouchersAsync(long financialYearId)
    {
        using var context = _contextFactory.CreateDbContext();
        return await context.JournalVouchers
            .AsNoTracking()
            .Include(j => j.JournalVoucherLines).ThenInclude(l => l.Account)
            .Where(j => j.FinancialYearId == financialYearId)
            .OrderByDescending(j => j.VoucherNo)
            .ToListAsync();
    }

    public async Task<int> GetNextVoucherNoAsync(long financialYearId)
    {
        using var context = _contextFactory.CreateDbContext();
        var max = await context.JournalVouchers
            .Where(j => j.FinancialYearId == financialYearId)
            .MaxAsync(j => (int?)j.VoucherNo);
        return (max ?? 0) + 1;
    }

    public class JournalLineInput
    {
        public long AccountId { get; set; }
        public decimal Debit { get; set; }
        public decimal Credit { get; set; }
        public string? Narration { get; set; }
    }

    public class JournalVoucherInput
    {
        public DateOnly VoucherDate { get; set; }
        public string? Narration { get; set; }
        public long FinancialYearId { get; set; }
        public long? CreatedBy { get; set; }
        public List<JournalLineInput> Lines { get; set; } = new();
    }

    public async Task<(bool success, string message)> CreateJournalVoucherAsync(JournalVoucherInput input)
    {
        using var context = _contextFactory.CreateDbContext();
        await using var transaction = await context.Database.BeginTransactionAsync();
        try
        {
            var (valid, validationMessage) = Validate(input);
            if (!valid)
                return (false, validationMessage);

            var nextNo = await context.JournalVouchers
                .Where(j => j.FinancialYearId == input.FinancialYearId)
                .MaxAsync(j => (int?)j.VoucherNo) ?? 0;
            nextNo++;

            var voucher = new JournalVoucher
            {
                FinancialYearId = input.FinancialYearId,
                VoucherNo = nextNo,
                VoucherDate = input.VoucherDate,
                Narration = input.Narration,
                CreatedBy = input.CreatedBy,
                CreatedAt = DateTime.UtcNow
            };

            context.JournalVouchers.Add(voucher);
            await context.SaveChangesAsync();

            foreach (var line in input.Lines)
            {
                context.JournalVoucherLines.Add(new JournalVoucherLine
                {
                    JournalVoucherId = voucher.JournalVoucherId,
                    AccountId = line.AccountId,
                    Debit = line.Debit,
                    Credit = line.Credit,
                    Narration = line.Narration
                });
            }

            await context.SaveChangesAsync();
            await BuildAndAddLedgerEntriesAsync(context, voucher.JournalVoucherId, input);

            await context.SaveChangesAsync();
            await transaction.CommitAsync();
            return (true, $"Journal Voucher #{nextNo} created successfully");
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            return (false, $"Error creating Journal Voucher: {ex.Message}");
        }
    }

    public async Task<(bool success, string message)> UpdateJournalVoucherAsync(long journalVoucherId, JournalVoucherInput input)
    {
        using var context = _contextFactory.CreateDbContext();
        await using var transaction = await context.Database.BeginTransactionAsync();
        try
        {
            var voucher = await context.JournalVouchers
                .Include(j => j.JournalVoucherLines)
                .FirstOrDefaultAsync(j => j.JournalVoucherId == journalVoucherId);
            if (voucher == null)
                return (false, "Journal Voucher not found");

            var (valid, validationMessage) = Validate(input);
            if (!valid)
                return (false, validationMessage);

            voucher.VoucherDate = input.VoucherDate;
            voucher.Narration = input.Narration;

            context.JournalVoucherLines.RemoveRange(voucher.JournalVoucherLines);
            await context.SaveChangesAsync();

            foreach (var line in input.Lines)
            {
                context.JournalVoucherLines.Add(new JournalVoucherLine
                {
                    JournalVoucherId = voucher.JournalVoucherId,
                    AccountId = line.AccountId,
                    Debit = line.Debit,
                    Credit = line.Credit,
                    Narration = line.Narration
                });
            }

            var oldEntries = await context.LedgerEntries
                .Where(e => e.VoucherId == journalVoucherId && e.VoucherType == VoucherType.Journal)
                .ToListAsync();
            context.LedgerEntries.RemoveRange(oldEntries);

            await context.SaveChangesAsync();
            await BuildAndAddLedgerEntriesAsync(context, journalVoucherId, input);

            await context.SaveChangesAsync();
            await transaction.CommitAsync();
            return (true, "Journal Voucher updated successfully");
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            return (false, $"Error updating Journal Voucher: {ex.Message}");
        }
    }

    public async Task<(bool success, string message)> DeleteJournalVoucherAsync(long journalVoucherId)
    {
        using var context = _contextFactory.CreateDbContext();
        await using var transaction = await context.Database.BeginTransactionAsync();
        try
        {
            var voucher = await context.JournalVouchers
                .Include(j => j.JournalVoucherLines)
                .FirstOrDefaultAsync(j => j.JournalVoucherId == journalVoucherId);
            if (voucher == null)
                return (false, "Journal Voucher not found");

            var entries = await context.LedgerEntries
                .Where(e => e.VoucherId == journalVoucherId && e.VoucherType == VoucherType.Journal)
                .ToListAsync();
            context.LedgerEntries.RemoveRange(entries);

            context.JournalVoucherLines.RemoveRange(voucher.JournalVoucherLines);
            context.JournalVouchers.Remove(voucher);
            await context.SaveChangesAsync();
            await transaction.CommitAsync();
            return (true, "Journal Voucher deleted successfully");
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            return (false, $"Error deleting Journal Voucher: {ex.Message}");
        }
    }

    private static (bool valid, string message) Validate(JournalVoucherInput input)
    {
        var lines = input.Lines.Where(l => l.AccountId != 0).ToList();
        if (lines.Count < 2)
            return (false, "A Journal Voucher needs at least two lines");

        foreach (var line in lines)
        {
            if (line.Debit != 0 && line.Credit != 0)
                return (false, "A line cannot have both Debit and Credit - use separate lines");
            if (line.Debit == 0 && line.Credit == 0)
                return (false, "Every line needs either a Debit or a Credit amount");
        }

        var totalDebit = lines.Sum(l => l.Debit);
        var totalCredit = lines.Sum(l => l.Credit);
        if (totalDebit != totalCredit)
            return (false, $"Total Debit ({totalDebit:N2}) must equal Total Credit ({totalCredit:N2})");
        if (totalDebit <= 0)
            return (false, "Total amount must be greater than zero");

        return (true, "");
    }

    private static async Task BuildAndAddLedgerEntriesAsync(FruitAccountingContext context, long journalVoucherId, JournalVoucherInput input)
    {
        var voucher = await context.JournalVouchers.FirstAsync(j => j.JournalVoucherId == journalVoucherId);

        foreach (var line in input.Lines.Where(l => l.AccountId != 0))
        {
            context.LedgerEntries.Add(new LedgerEntry
            {
                FinancialYearId = input.FinancialYearId,
                EntryDate = input.VoucherDate,
                AccountId = line.AccountId,
                Debit = line.Debit,
                Credit = line.Credit,
                VoucherId = journalVoucherId,
                VoucherType = VoucherType.Journal,
                Narration = string.IsNullOrWhiteSpace(line.Narration) ? $"Journal Voucher #{voucher.VoucherNo}" : line.Narration,
                CreatedAt = DateTime.UtcNow
            });
        }
    }
}
