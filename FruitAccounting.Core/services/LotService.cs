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
}
