using FruitAccounting.Data.Context;
using FruitAccounting.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace FruitAccounting.Core.services;

public class AccountGroupService
{
    private readonly IDbContextFactory<FruitAccountingContext> _contextFactory;

    public AccountGroupService(IDbContextFactory<FruitAccountingContext> contextFactory)
    {
        _contextFactory = contextFactory;
    }

    public async Task<List<AccountGroup>> GetAllMainGroupsAsync(long companyId)
    {
        using var context = _contextFactory.CreateDbContext();
        return await context.AccountGroups
            .AsNoTracking()
            .Where(g => g.CompanyId == companyId && g.ParentId == null && g.IsActive)
            .OrderBy(g => g.Name)
            .ToListAsync();
    }

    public async Task<AccountGroup?> GetMainGroupAsync(long mainGroupId)
    {
        using var context = _contextFactory.CreateDbContext();
        return await context.AccountGroups
            .AsNoTracking()
            .FirstOrDefaultAsync(g => g.AccountGroupId == mainGroupId);
    }

    public async Task<(bool success, string message)> CreateMainGroupAsync(long companyId, string code, string name, string nature)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(name))
                return (false, "Main Group name is required");

            using var context = _contextFactory.CreateDbContext();

            // Check if name already exists
            var existing = await context.AccountGroups
                .AsNoTracking()
                .FirstOrDefaultAsync(g => g.CompanyId == companyId && g.Name == name && g.ParentId == null);

            if (existing != null)
                return (false, "Main Group with this name already exists");

            var mainGroup = new AccountGroup
            {
                CompanyId = companyId,
                Code = code,
                Name = name,
                Nature = nature,
                ParentId = null,
                IsSystem = false
            };

            context.AccountGroups.Add(mainGroup);
            await context.SaveChangesAsync();
            return (true, "Main Group created successfully");
        }
        catch (Exception ex)
        {
            return (false, $"Error creating Main Group: {ex.Message}");
        }
    }

    public async Task<(bool success, string message)> UpdateMainGroupAsync(long mainGroupId, string code, string name, string nature)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(name))
                return (false, "Main Group name is required");

            using var context = _contextFactory.CreateDbContext();

            var mainGroup = await context.AccountGroups.FirstOrDefaultAsync(g => g.AccountGroupId == mainGroupId);
            if (mainGroup == null)
                return (false, "Main Group not found");

            // Check if name already exists (excluding current record)
            var existing = await context.AccountGroups
                .AsNoTracking()
                .FirstOrDefaultAsync(g => g.CompanyId == mainGroup.CompanyId &&
                                         g.Name == name &&
                                         g.ParentId == null &&
                                         g.AccountGroupId != mainGroupId);

            if (existing != null)
                return (false, "Another Main Group with this name already exists");

            mainGroup.Code = code;
            mainGroup.Name = name;
            mainGroup.Nature = nature;

            await context.SaveChangesAsync();
            return (true, "Main Group updated successfully");
        }
        catch (Exception ex)
        {
            return (false, $"Error updating Main Group: {ex.Message}");
        }
    }

    public async Task<(bool success, string message)> DeleteMainGroupAsync(long mainGroupId)
    {
        try
        {
            using var context = _contextFactory.CreateDbContext();

            var mainGroup = await context.AccountGroups
                .Include(g => g.Accounts)
                .Include(g => g.InverseParent)
                .FirstOrDefaultAsync(g => g.AccountGroupId == mainGroupId);

            if (mainGroup == null)
                return (false, "Main Group not found");

            // Check if it has active sub-groups
            if (mainGroup.InverseParent.Any(g => g.IsActive))
                return (false, "Cannot delete Main Group that has active Sub Groups");

            // Check if it has any accounts (blocked or not)
            if (mainGroup.Accounts.Any())
                return (false, "Cannot delete Main Group that has Accounts");

            // Soft delete: mark as inactive
            mainGroup.IsActive = false;
            await context.SaveChangesAsync();
            return (true, "Main Group deleted successfully");
        }
        catch (Exception ex)
        {
            return (false, $"Error deleting Main Group: {ex.Message}");
        }
    }
}
