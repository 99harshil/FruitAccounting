using FruitAccounting.Data.Context;
using FruitAccounting.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace FruitAccounting.Core.services
{
    public class FinancialYearService
    {
        private readonly IDbContextFactory<FruitAccountingContext> _contextFactory;

        public FinancialYearService(IDbContextFactory<FruitAccountingContext> contextFactory) => _contextFactory = contextFactory;

        public async Task<List<FinancialYear>> GetFinancialYearsByCompanyAsync(long companyId)
        {
            await using var db = await _contextFactory.CreateDbContextAsync();
            return await db.FinancialYears
                .Where(fy => fy.CompanyId == companyId)
                .OrderByDescending(fy => fy.StartDate)
                .ToListAsync();
        }

        public async Task<(bool success, string message)> AddFinancialYearAsync(long companyId, string code, DateOnly startDate, DateOnly endDate)
        {
            if (string.IsNullOrWhiteSpace(code) || code.Length > 10)
                return (false, "Financial year code is required and must be 10 characters or less.");

            if (endDate <= startDate)
                return (false, "End date must be after start date.");

            await using var db = await _contextFactory.CreateDbContextAsync();

            // Check if code already exists for this company
            var existingCode = await db.FinancialYears
                .AnyAsync(fy => fy.CompanyId == companyId && fy.Code == code);
            if (existingCode)
                return (false, "Financial year code already exists for this company.");

            var financialYear = new FinancialYear
            {
                CompanyId = companyId,
                Code = code,
                StartDate = startDate,
                EndDate = endDate,
                IsActive = true,
                IsClosed = false
            };

            db.FinancialYears.Add(financialYear);
            await db.SaveChangesAsync();

            return (true, "Financial year added successfully.");
        }

        public async Task<(bool success, string message)> EditFinancialYearAsync(long financialYearId, string code, DateOnly startDate, DateOnly endDate, bool isActive)
        {
            if (string.IsNullOrWhiteSpace(code) || code.Length > 10)
                return (false, "Financial year code is required and must be 10 characters or less.");

            if (endDate <= startDate)
                return (false, "End date must be after start date.");

            await using var db = await _contextFactory.CreateDbContextAsync();

            var financialYear = await db.FinancialYears.FindAsync(financialYearId);
            if (financialYear == null)
                return (false, "Financial year not found.");

            // Check if code is already used by another financial year in the same company
            var existingCode = await db.FinancialYears
                .AnyAsync(fy => fy.FinancialYearId != financialYearId &&
                                 fy.CompanyId == financialYear.CompanyId &&
                                 fy.Code == code);
            if (existingCode)
                return (false, "Financial year code already exists for this company.");

            financialYear.Code = code;
            financialYear.StartDate = startDate;
            financialYear.EndDate = endDate;
            financialYear.IsActive = isActive;

            await db.SaveChangesAsync();

            return (true, "Financial year updated successfully.");
        }

        public async Task<(bool success, string message)> DeleteFinancialYearAsync(long financialYearId)
        {
            await using var db = await _contextFactory.CreateDbContextAsync();

            var financialYear = await db.FinancialYears.FindAsync(financialYearId);
            if (financialYear == null)
                return (false, "Financial year not found.");

            // Check if there are any transactions/data linked to this financial year
            var hasTransactions = await db.LedgerEntries.AnyAsync(le => le.FinancialYearId == financialYearId) ||
                                 await db.Daybooks.AnyAsync(db => db.LinkedAccountId != null);

            if (hasTransactions)
                return (false, "Cannot delete financial year that has transactions.");

            db.FinancialYears.Remove(financialYear);
            await db.SaveChangesAsync();

            return (true, "Financial year deleted successfully.");
        }

        public async Task<FinancialYear?> GetFinancialYearAsync(long financialYearId)
        {
            await using var db = await _contextFactory.CreateDbContextAsync();
            return await db.FinancialYears.FindAsync(financialYearId);
        }
    }
}
