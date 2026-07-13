using FruitAccounting.Data.Context;
using FruitAccounting.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace FruitAccounting.Core.services;

public class BankReconciliationService
{
    private readonly IDbContextFactory<FruitAccountingContext> _contextFactory;

    public BankReconciliationService(IDbContextFactory<FruitAccountingContext> contextFactory)
    {
        _contextFactory = contextFactory;
    }

    public async Task<List<Daybook>> GetBankDaybooksAsync(long companyId)
    {
        using var context = _contextFactory.CreateDbContext();
        return await context.Daybooks
            .AsNoTracking()
            .Include(d => d.LinkedAccount)
            .Where(d => d.CompanyId == companyId && d.BookType == 'B')
            .OrderBy(d => d.Name)
            .ToListAsync();
    }

    public enum LineType { Receipt, Payment }

    public class ReconciliationLine
    {
        public LineType Type { get; set; }
        public long VoucherId { get; set; }
        public int VNo { get; set; }
        public string? ChequeNo { get; set; }
        public DateOnly Date { get; set; }
        public string? AccountName { get; set; }
        public decimal ReceiveAmount { get; set; }
        public decimal PaymentAmount { get; set; }
        public DateOnly? ClearanceDate { get; set; }
    }

    public async Task<List<ReconciliationLine>> GetReconciliationLinesAsync(long daybookId, long financialYearId, bool pendingOnly)
    {
        using var context = _contextFactory.CreateDbContext();

        var receiptsQuery = context.Receipts
            .AsNoTracking()
            .Include(r => r.Account)
            .Where(r => r.DaybookId == daybookId && r.FinancialYearId == financialYearId);
        if (pendingOnly)
            receiptsQuery = receiptsQuery.Where(r => r.ClearanceDate == null);

        var paymentsQuery = context.Payments
            .AsNoTracking()
            .Include(p => p.Account)
            .Where(p => p.DaybookId == daybookId && p.FinancialYearId == financialYearId && !p.IsFreightPayment);
        if (pendingOnly)
            paymentsQuery = paymentsQuery.Where(p => p.ClearanceDate == null);

        var receipts = await receiptsQuery.ToListAsync();
        var payments = await paymentsQuery.ToListAsync();

        var lines = new List<ReconciliationLine>();
        lines.AddRange(receipts.Select(r => new ReconciliationLine
        {
            Type = LineType.Receipt,
            VoucherId = r.ReceiptId,
            VNo = r.ReceiptNo,
            ChequeNo = r.ChequeNo,
            Date = r.ReceiptDate,
            AccountName = r.Account?.Name,
            ReceiveAmount = r.TotalSettled,
            PaymentAmount = 0,
            ClearanceDate = r.ClearanceDate
        }));
        lines.AddRange(payments.Select(p => new ReconciliationLine
        {
            Type = LineType.Payment,
            VoucherId = p.PaymentId,
            VNo = p.PaymentNo,
            ChequeNo = p.ChequeNo,
            Date = p.PaymentDate,
            AccountName = p.Account?.Name,
            ReceiveAmount = 0,
            PaymentAmount = p.TotalSettled,
            ClearanceDate = p.ClearanceDate
        }));

        return lines.OrderBy(l => l.Date).ThenBy(l => l.VNo).ToList();
    }

    public async Task<(bool success, string message)> SetClearanceDateAsync(LineType type, long voucherId, DateOnly? clearanceDate)
    {
        using var context = _contextFactory.CreateDbContext();
        try
        {
            if (type == LineType.Receipt)
            {
                var receipt = await context.Receipts.FirstOrDefaultAsync(r => r.ReceiptId == voucherId);
                if (receipt == null) return (false, "Receipt not found");
                receipt.ClearanceDate = clearanceDate;
            }
            else
            {
                var payment = await context.Payments.FirstOrDefaultAsync(p => p.PaymentId == voucherId);
                if (payment == null) return (false, "Payment not found");
                payment.ClearanceDate = clearanceDate;
            }

            await context.SaveChangesAsync();
            return (true, "");
        }
        catch (Exception ex)
        {
            return (false, $"Error updating clearance date: {ex.Message}");
        }
    }
}
