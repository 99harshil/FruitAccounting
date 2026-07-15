using FruitAccounting.Data.Context;
using FruitAccounting.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace FruitAccounting.Core.services;

public class LotService
{
    private readonly IDbContextFactory<FruitAccountingContext> _contextFactory;

    public LotService(IDbContextFactory<FruitAccountingContext> contextFactory)
    {
        _contextFactory = contextFactory;
    }

    public async Task<int> GetNextLotNoAsync(long financialYearId)
    {
        using var context = _contextFactory.CreateDbContext();
        var max = await context.Lots
            .Where(l => l.FinancialYearId == financialYearId)
            .MaxAsync(l => (int?)l.LotNo);
        return (max ?? 0) + 1;
    }

    public async Task<Lot?> FindLotByNoAsync(long financialYearId, int lotNo)
    {
        using var context = _contextFactory.CreateDbContext();
        return await context.Lots
            .AsNoTracking()
            .Include(l => l.Item)
            .Include(l => l.Supplier)
            .FirstOrDefaultAsync(l => l.FinancialYearId == financialYearId && l.LotNo == lotNo);
    }

    public async Task<List<Lot>> GetAllLotsAsync(long financialYearId)
    {
        using var context = _contextFactory.CreateDbContext();
        return await context.Lots
            .AsNoTracking()
            .Include(l => l.Item)
            .Include(l => l.Supplier)
            .Where(l => l.FinancialYearId == financialYearId)
            .OrderByDescending(l => l.LotNo)
            .ToListAsync();
    }

    public class StockInfo
    {
        public long LotId { get; set; }
        public int LotNo { get; set; }
        public long ItemId { get; set; }
        public string ItemName { get; set; } = "";
        public long SupplierId { get; set; }
        public string SupplierName { get; set; } = "";
        public DateOnly PurchaseDate { get; set; }
        public decimal PurchasedQty { get; set; }
        public decimal SoldQty { get; set; }
        public decimal BalanceQty { get; set; }
        public long PurchaseBillId { get; set; }
        public int PurchaseBillNo { get; set; }
    }

    // One row per distinct Lot - Purchased Qty is summed across every PurchaseBillItem that
    // originated it (a Lot can end up split across more than one Purchase Bill if the same Lot
    // No. was entered twice; see the Sales balance bug this was built to make visible/consistent
    // with). Sold Qty sums every Sale row referencing the Lot, regardless of financial year.
    public async Task<List<StockInfo>> GetStockAsync(long financialYearId)
    {
        using var context = _contextFactory.CreateDbContext();

        var items = await context.PurchaseBillItems.AsNoTracking()
            .Include(i => i.Lot).ThenInclude(l => l.Item)
            .Include(i => i.PurchaseBill).ThenInclude(b => b.Supplier)
            .Where(i => i.Lot.FinancialYearId == financialYearId)
            .ToListAsync();

        var lotIds = items.Select(i => i.LotId).Distinct().ToList();

        var soldByLot = await context.Sales.AsNoTracking()
            .Where(s => lotIds.Contains(s.LotId))
            .GroupBy(s => s.LotId)
            .Select(g => new { LotId = g.Key, Sold = g.Sum(s => s.Quantity) })
            .ToDictionaryAsync(x => x.LotId, x => x.Sold);

        return items
            .GroupBy(i => i.LotId)
            .Select(g =>
            {
                var first = g.First();
                var purchased = g.Sum(i => i.Quantity);
                var sold = soldByLot.TryGetValue(g.Key, out var s) ? s : 0;
                return new StockInfo
                {
                    LotId = g.Key,
                    LotNo = first.Lot.LotNo,
                    ItemId = first.ItemId,
                    ItemName = first.Lot.Item?.Name ?? "",
                    SupplierId = first.PurchaseBill.SupplierId,
                    SupplierName = first.PurchaseBill.Supplier?.Name ?? "",
                    PurchaseDate = first.PurchaseBill.BillDate,
                    PurchasedQty = purchased,
                    SoldQty = sold,
                    BalanceQty = purchased - sold,
                    PurchaseBillId = first.PurchaseBillId,
                    PurchaseBillNo = first.PurchaseBill.BillNo
                };
            })
            .OrderByDescending(x => x.LotNo)
            .ToList();
    }
}
