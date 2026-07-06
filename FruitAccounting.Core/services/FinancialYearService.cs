using FruitAccounting.Data.Context;
using FruitAccounting.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace FruitAccounting.Core.services
{
    public class FinancialYearService
    {
        private readonly IDbContextFactory<FruitAccountingContext> _contextFactory;

        public FinancialYearService(IDbContextFactory<FruitAccountingContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task<(bool, string)> AddFinancialYearAsync(long companyId, string code, DateOnly startDate, DateOnly endDate)
        {
            using var context = _contextFactory.CreateDbContext();

            if (string.IsNullOrWhiteSpace(code))
                return (false, "Code is required.");

            if (endDate <= startDate)
                return (false, "End date must be after start date.");

            // Check for duplicate code in this company
            var exists = await context.FinancialYears
                .AsNoTracking()
                .AnyAsync(fy => fy.CompanyId == companyId && fy.Code == code.Trim());

            if (exists)
                return (false, $"Financial year with code '{code}' already exists for this company.");

            var fy = new FinancialYear
            {
                CompanyId = companyId,
                Code = code.Trim(),
                StartDate = startDate,
                EndDate = endDate,
                IsActive = true,
                IsClosed = false
            };

            context.FinancialYears.Add(fy);
            await context.SaveChangesAsync();

            return (true, "Financial year added successfully.");
        }

        public async Task<(bool, string)> EditFinancialYearAsync(long financialYearId, string code, DateOnly startDate, DateOnly endDate, bool isActive)
        {
            using var context = _contextFactory.CreateDbContext();

            if (string.IsNullOrWhiteSpace(code))
                return (false, "Code is required.");

            if (endDate <= startDate)
                return (false, "End date must be after start date.");

            var fy = await context.FinancialYears.FirstOrDefaultAsync(f => f.FinancialYearId == financialYearId);
            if (fy == null)
                return (false, "Financial year not found.");

            // Check for duplicate code in this company (excluding current FY)
            var exists = await context.FinancialYears
                .AsNoTracking()
                .AnyAsync(f => f.CompanyId == fy.CompanyId && f.Code == code.Trim() && f.FinancialYearId != financialYearId);

            if (exists)
                return (false, $"Financial year with code '{code}' already exists for this company.");

            fy.Code = code.Trim();
            fy.StartDate = startDate;
            fy.EndDate = endDate;
            fy.IsActive = isActive;

            context.FinancialYears.Update(fy);
            await context.SaveChangesAsync();

            return (true, "Financial year updated successfully.");
        }

        public async Task<(bool, string)> DeleteFinancialYearAsync(long financialYearId)
        {
            using var context = _contextFactory.CreateDbContext();

            var fy = await context.FinancialYears.FirstOrDefaultAsync(f => f.FinancialYearId == financialYearId);
            if (fy == null)
                return (false, "Financial year not found.");

            // Check if this FY has any ledger entries
            var hasTransactions = await context.LedgerEntries
                .AsNoTracking()
                .AnyAsync(le => le.FinancialYearId == financialYearId);

            if (hasTransactions)
                return (false, "Cannot delete financial year with existing transactions.");

            context.FinancialYears.Remove(fy);
            await context.SaveChangesAsync();

            return (true, "Financial year deleted successfully.");
        }

        public async Task<FinancialYear?> GetFinancialYearAsync(long financialYearId)
        {
            using var context = _contextFactory.CreateDbContext();
            return await context.FinancialYears
                .AsNoTracking()
                .FirstOrDefaultAsync(f => f.FinancialYearId == financialYearId);
        }

        public async Task<List<FinancialYear>> GetFinancialYearsByCompanyAsync(long companyId)
        {
            using var context = _contextFactory.CreateDbContext();
            return await context.FinancialYears
                .AsNoTracking()
                .Where(f => f.CompanyId == companyId)
                .OrderByDescending(f => f.StartDate)
                .ToListAsync();
        }
    }
}
