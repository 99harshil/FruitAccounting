using FruitAccounting.Data.Context;
using FruitAccounting.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace FruitAccounting.Core.services;

public class ItemGroupService
{
    private readonly IDbContextFactory<FruitAccountingContext> _contextFactory;

    public ItemGroupService(IDbContextFactory<FruitAccountingContext> contextFactory)
    {
        _contextFactory = contextFactory;
    }

    public async Task<List<ItemGroup>> GetAllItemGroupsAsync(long companyId)
    {
        using var context = _contextFactory.CreateDbContext();
        return await context.ItemGroups
            .AsNoTracking()
            .Where(g => g.CompanyId == companyId)
            .OrderBy(g => g.Name)
            .ToListAsync();
    }

    public async Task<(bool success, string message)> CreateItemGroupAsync(long companyId, string name)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(name))
                return (false, "Item Group name is required");

            using var context = _contextFactory.CreateDbContext();

            var existing = await context.ItemGroups
                .AsNoTracking()
                .FirstOrDefaultAsync(g => g.CompanyId == companyId && g.Name == name);

            if (existing != null)
                return (false, "An Item Group with this name already exists");

            var group = new ItemGroup
            {
                CompanyId = companyId,
                Name = name
            };

            context.ItemGroups.Add(group);
            await context.SaveChangesAsync();
            return (true, "Item Group created successfully");
        }
        catch (Exception ex)
        {
            return (false, $"Error creating Item Group: {ex.Message}");
        }
    }

    public async Task<(bool success, string message)> UpdateItemGroupAsync(long itemGroupId, string name)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(name))
                return (false, "Item Group name is required");

            using var context = _contextFactory.CreateDbContext();

            var group = await context.ItemGroups.FirstOrDefaultAsync(g => g.ItemGroupId == itemGroupId);
            if (group == null)
                return (false, "Item Group not found");

            var existing = await context.ItemGroups
                .AsNoTracking()
                .FirstOrDefaultAsync(g => g.CompanyId == group.CompanyId &&
                                         g.Name == name &&
                                         g.ItemGroupId != itemGroupId);

            if (existing != null)
                return (false, "Another Item Group with this name already exists");

            group.Name = name;

            await context.SaveChangesAsync();
            return (true, "Item Group updated successfully");
        }
        catch (Exception ex)
        {
            return (false, $"Error updating Item Group: {ex.Message}");
        }
    }

    public async Task<(bool success, string message)> DeleteItemGroupAsync(long itemGroupId)
    {
        try
        {
            using var context = _contextFactory.CreateDbContext();

            var group = await context.ItemGroups
                .Include(g => g.Items)
                .Include(g => g.ItemCounts)
                .FirstOrDefaultAsync(g => g.ItemGroupId == itemGroupId);

            if (group == null)
                return (false, "Item Group not found");

            if (group.Items.Any())
                return (false, "Cannot delete Item Group that has Items");

            if (group.ItemCounts.Any())
                return (false, "Cannot delete Item Group that has Item Counts");

            context.ItemGroups.Remove(group);
            await context.SaveChangesAsync();
            return (true, "Item Group deleted successfully");
        }
        catch (Exception ex)
        {
            return (false, $"Error deleting Item Group: {ex.Message}");
        }
    }
}
