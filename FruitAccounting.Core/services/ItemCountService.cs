using FruitAccounting.Data.Context;
using FruitAccounting.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace FruitAccounting.Core.services;

public class ItemCountService
{
    private readonly IDbContextFactory<FruitAccountingContext> _contextFactory;

    public ItemCountService(IDbContextFactory<FruitAccountingContext> contextFactory)
    {
        _contextFactory = contextFactory;
    }

    public async Task<List<ItemCount>> GetAllItemCountsAsync(long companyId)
    {
        using var context = _contextFactory.CreateDbContext();
        return await context.ItemCounts
            .AsNoTracking()
            .Include(c => c.ItemGroup)
            .Where(c => c.CompanyId == companyId)
            .OrderBy(c => c.Name)
            .ToListAsync();
    }

    public async Task<(bool success, string message)> CreateItemCountAsync(long companyId, long? itemGroupId, string name)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(name))
                return (false, "Count name is required");

            using var context = _contextFactory.CreateDbContext();

            // Uniqueness is scoped per (company, group) - the same count name can exist under different groups
            var existing = await context.ItemCounts
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.CompanyId == companyId && c.ItemGroupId == itemGroupId && c.Name == name);

            if (existing != null)
                return (false, "A Count with this name already exists for the selected Group");

            var count = new ItemCount
            {
                CompanyId = companyId,
                ItemGroupId = itemGroupId,
                Name = name
            };

            context.ItemCounts.Add(count);
            await context.SaveChangesAsync();
            return (true, "Count created successfully");
        }
        catch (Exception ex)
        {
            return (false, $"Error creating Count: {ex.Message}");
        }
    }

    public async Task<(bool success, string message)> UpdateItemCountAsync(long itemCountId, long? itemGroupId, string name)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(name))
                return (false, "Count name is required");

            using var context = _contextFactory.CreateDbContext();

            var count = await context.ItemCounts.FirstOrDefaultAsync(c => c.ItemCountId == itemCountId);
            if (count == null)
                return (false, "Count not found");

            var existing = await context.ItemCounts
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.CompanyId == count.CompanyId &&
                                         c.ItemGroupId == itemGroupId &&
                                         c.Name == name &&
                                         c.ItemCountId != itemCountId);

            if (existing != null)
                return (false, "Another Count with this name already exists for the selected Group");

            count.ItemGroupId = itemGroupId;
            count.Name = name;

            await context.SaveChangesAsync();
            return (true, "Count updated successfully");
        }
        catch (Exception ex)
        {
            return (false, $"Error updating Count: {ex.Message}");
        }
    }

    public async Task<(bool success, string message)> DeleteItemCountAsync(long itemCountId)
    {
        try
        {
            using var context = _contextFactory.CreateDbContext();

            var count = await context.ItemCounts
                .Include(c => c.ImportPurchaseItems)
                .FirstOrDefaultAsync(c => c.ItemCountId == itemCountId);

            if (count == null)
                return (false, "Count not found");

            if (count.ImportPurchaseItems.Any())
                return (false, "Cannot delete Count that has been used in Purchase records");

            context.ItemCounts.Remove(count);
            await context.SaveChangesAsync();
            return (true, "Count deleted successfully");
        }
        catch (Exception ex)
        {
            return (false, $"Error deleting Count: {ex.Message}");
        }
    }
}
