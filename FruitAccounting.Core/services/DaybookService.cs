using FruitAccounting.Data.Context;
using FruitAccounting.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace FruitAccounting.Core.services;

public class DaybookService
{
    private readonly IDbContextFactory<FruitAccountingContext> _contextFactory;

    public DaybookService(IDbContextFactory<FruitAccountingContext> contextFactory)
    {
        _contextFactory = contextFactory;
    }

    public async Task<List<Daybook>> GetAllDaybooksAsync(long companyId)
    {
        using var context = _contextFactory.CreateDbContext();
        return await context.Daybooks
            .AsNoTracking()
            .Include(d => d.LinkedAccount)
            .Where(d => d.CompanyId == companyId)
            .OrderBy(d => d.Name)
            .ToListAsync();
    }

    public async Task<List<Account>> GetAccountsForLinkingAsync(long companyId)
    {
        using var context = _contextFactory.CreateDbContext();
        return await context.Accounts
            .AsNoTracking()
            .Where(a => a.CompanyId == companyId && !a.IsBlocked)
            .OrderBy(a => a.Name)
            .ToListAsync();
    }

    public async Task<(bool success, string message)> CreateDaybookAsync(long companyId, string name, char bookType, long? linkedAccountId)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(name))
                return (false, "Daybook name is required");

            using var context = _contextFactory.CreateDbContext();

            var existing = await context.Daybooks
                .AsNoTracking()
                .FirstOrDefaultAsync(d => d.CompanyId == companyId && d.Name == name);

            if (existing != null)
                return (false, "A Daybook with this name already exists");

            var daybook = new Daybook
            {
                CompanyId = companyId,
                Name = name,
                BookType = bookType,
                LinkedAccountId = linkedAccountId
            };

            context.Daybooks.Add(daybook);
            await context.SaveChangesAsync();
            return (true, "Daybook created successfully");
        }
        catch (Exception ex)
        {
            return (false, $"Error creating Daybook: {ex.Message}");
        }
    }

    public async Task<(bool success, string message)> UpdateDaybookAsync(long daybookId, string name, char bookType, long? linkedAccountId)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(name))
                return (false, "Daybook name is required");

            using var context = _contextFactory.CreateDbContext();

            var daybook = await context.Daybooks.FirstOrDefaultAsync(d => d.DaybookId == daybookId);
            if (daybook == null)
                return (false, "Daybook not found");

            var existing = await context.Daybooks
                .AsNoTracking()
                .FirstOrDefaultAsync(d => d.CompanyId == daybook.CompanyId &&
                                         d.Name == name &&
                                         d.DaybookId != daybookId);

            if (existing != null)
                return (false, "Another Daybook with this name already exists");

            daybook.Name = name;
            daybook.BookType = bookType;
            daybook.LinkedAccountId = linkedAccountId;

            await context.SaveChangesAsync();
            return (true, "Daybook updated successfully");
        }
        catch (Exception ex)
        {
            return (false, $"Error updating Daybook: {ex.Message}");
        }
    }

    public async Task<(bool success, string message)> DeleteDaybookAsync(long daybookId)
    {
        try
        {
            using var context = _contextFactory.CreateDbContext();

            var daybook = await context.Daybooks
                .Include(d => d.Payments)
                .Include(d => d.Receipts)
                .Include(d => d.BankEntries)
                .Include(d => d.BankStatementLines)
                .FirstOrDefaultAsync(d => d.DaybookId == daybookId);

            if (daybook == null)
                return (false, "Daybook not found");

            // No soft-delete column on this table - block deletion if it's referenced anywhere
            if (daybook.Payments.Any() || daybook.Receipts.Any() ||
                daybook.BankEntries.Any() || daybook.BankStatementLines.Any())
                return (false, "Cannot delete Daybook that has transactions recorded against it");

            context.Daybooks.Remove(daybook);
            await context.SaveChangesAsync();
            return (true, "Daybook deleted successfully");
        }
        catch (Exception ex)
        {
            return (false, $"Error deleting Daybook: {ex.Message}");
        }
    }
}
