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

            // The database enforces name uniqueness per company across ALL account groups
            // (main groups and sub groups share one name space, including soft-deleted rows)
            var existing = await context.AccountGroups
                .AsNoTracking()
                .FirstOrDefaultAsync(g => g.CompanyId == companyId && g.Name == name);

            if (existing != null)
                return (false, existing.IsActive
                    ? "An Account Group with this name already exists"
                    : "This name was used by a previously deleted Account Group. Please choose a different name");

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

            // Check if name already exists (excluding current record) - across ALL account groups, matching the DB constraint
            var existing = await context.AccountGroups
                .AsNoTracking()
                .FirstOrDefaultAsync(g => g.CompanyId == mainGroup.CompanyId &&
                                         g.Name == name &&
                                         g.AccountGroupId != mainGroupId);

            if (existing != null)
                return (false, existing.IsActive
                    ? "Another Account Group with this name already exists"
                    : "This name was used by a previously deleted Account Group. Please choose a different name");

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

    public async Task<List<AccountGroup>> GetAllSubGroupsAsync(long companyId)
    {
        using var context = _contextFactory.CreateDbContext();
        return await context.AccountGroups
            .AsNoTracking()
            .Include(g => g.Parent)
            .Where(g => g.CompanyId == companyId && g.ParentId != null && g.IsActive)
            .OrderBy(g => g.Name)
            .ToListAsync();
    }

    public async Task<(bool success, string message)> CreateSubGroupAsync(long companyId, long parentId, string name)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(name))
                return (false, "Sub Group name is required");

            using var context = _contextFactory.CreateDbContext();

            var parent = await context.AccountGroups
                .AsNoTracking()
                .FirstOrDefaultAsync(g => g.AccountGroupId == parentId && g.CompanyId == companyId && g.IsActive);

            if (parent == null)
                return (false, "Selected Main Group not found");

            // Name uniqueness is per company across ALL account groups (main + sub, including soft-deleted)
            var existing = await context.AccountGroups
                .AsNoTracking()
                .FirstOrDefaultAsync(g => g.CompanyId == companyId && g.Name == name);

            if (existing != null)
                return (false, existing.IsActive
                    ? "An Account Group with this name already exists"
                    : "This name was used by a previously deleted Account Group. Please choose a different name");

            var subGroup = new AccountGroup
            {
                CompanyId = companyId,
                Code = "",
                Name = name,
                Nature = parent.Nature,   // sub group inherits nature from its main group
                ParentId = parentId,
                IsSystem = false
            };

            context.AccountGroups.Add(subGroup);
            await context.SaveChangesAsync();
            return (true, "Sub Group created successfully");
        }
        catch (Exception ex)
        {
            return (false, $"Error creating Sub Group: {ex.Message}");
        }
    }

    public async Task<(bool success, string message)> UpdateSubGroupAsync(long subGroupId, long parentId, string name)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(name))
                return (false, "Sub Group name is required");

            using var context = _contextFactory.CreateDbContext();

            var subGroup = await context.AccountGroups.FirstOrDefaultAsync(g => g.AccountGroupId == subGroupId);
            if (subGroup == null)
                return (false, "Sub Group not found");

            var parent = await context.AccountGroups
                .AsNoTracking()
                .FirstOrDefaultAsync(g => g.AccountGroupId == parentId && g.CompanyId == subGroup.CompanyId && g.IsActive);

            if (parent == null)
                return (false, "Selected Main Group not found");

            var existing = await context.AccountGroups
                .AsNoTracking()
                .FirstOrDefaultAsync(g => g.CompanyId == subGroup.CompanyId &&
                                         g.Name == name &&
                                         g.AccountGroupId != subGroupId);

            if (existing != null)
                return (false, existing.IsActive
                    ? "Another Account Group with this name already exists"
                    : "This name was used by a previously deleted Account Group. Please choose a different name");

            subGroup.Name = name;
            subGroup.ParentId = parentId;
            subGroup.Nature = parent.Nature;

            await context.SaveChangesAsync();
            return (true, "Sub Group updated successfully");
        }
        catch (Exception ex)
        {
            return (false, $"Error updating Sub Group: {ex.Message}");
        }
    }

    public async Task<(bool success, string message)> DeleteSubGroupAsync(long subGroupId)
    {
        try
        {
            using var context = _contextFactory.CreateDbContext();

            var subGroup = await context.AccountGroups
                .Include(g => g.Accounts)
                .Include(g => g.InverseParent)
                .FirstOrDefaultAsync(g => g.AccountGroupId == subGroupId);

            if (subGroup == null)
                return (false, "Sub Group not found");

            if (subGroup.InverseParent.Any(g => g.IsActive))
                return (false, "Cannot delete Sub Group that has active child groups");

            if (subGroup.Accounts.Any())
                return (false, "Cannot delete Sub Group that has Accounts");

            // Soft delete: mark as inactive
            subGroup.IsActive = false;
            await context.SaveChangesAsync();
            return (true, "Sub Group deleted successfully");
        }
        catch (Exception ex)
        {
            return (false, $"Error deleting Sub Group: {ex.Message}");
        }
    }
}
