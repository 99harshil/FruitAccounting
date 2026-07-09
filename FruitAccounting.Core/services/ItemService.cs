using FruitAccounting.Data.Context;
using FruitAccounting.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace FruitAccounting.Core.services;

public class ItemService
{
    private readonly IDbContextFactory<FruitAccountingContext> _contextFactory;

    public ItemService(IDbContextFactory<FruitAccountingContext> contextFactory)
    {
        _contextFactory = contextFactory;
    }

    public async Task<List<Item>> GetAllItemsAsync(long companyId)
    {
        using var context = _contextFactory.CreateDbContext();
        return await context.Items
            .AsNoTracking()
            .Include(i => i.ItemGroup)
            .Include(i => i.ItemCategory)
            .Where(i => i.CompanyId == companyId && i.IsActive)
            .OrderBy(i => i.Name)
            .ToListAsync();
    }

    public class ItemInput
    {
        public string? Code { get; set; }
        public string Name { get; set; } = "";
        public long? ItemGroupId { get; set; }
        public long? ItemCategoryId { get; set; }
        public string Unit { get; set; } = "Box";
        public decimal? LabourRate { get; set; }
        public decimal? PackingRate { get; set; }
        public bool UsesCrate { get; set; }
    }

    public async Task<(bool success, string message)> CreateItemAsync(long companyId, ItemInput input)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(input.Name))
                return (false, "Item name is required");

            using var context = _contextFactory.CreateDbContext();

            var existing = await context.Items
                .AsNoTracking()
                .FirstOrDefaultAsync(i => i.CompanyId == companyId && i.Name == input.Name);

            if (existing != null)
                return (false, existing.IsActive
                    ? "An Item with this name already exists"
                    : "This name was used by a previously deactivated Item. Please choose a different name");

            var item = new Item
            {
                CompanyId = companyId,
                Code = input.Code,
                Name = input.Name,
                ItemGroupId = input.ItemGroupId,
                ItemCategoryId = input.ItemCategoryId,
                Unit = input.Unit,
                LabourRate = input.LabourRate,
                PackingRate = input.PackingRate,
                UsesCrate = input.UsesCrate
            };

            context.Items.Add(item);
            await context.SaveChangesAsync();
            return (true, "Item created successfully");
        }
        catch (Exception ex)
        {
            return (false, $"Error creating Item: {ex.Message}");
        }
    }

    public async Task<(bool success, string message)> UpdateItemAsync(long itemId, ItemInput input)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(input.Name))
                return (false, "Item name is required");

            using var context = _contextFactory.CreateDbContext();

            var item = await context.Items.FirstOrDefaultAsync(i => i.ItemId == itemId);
            if (item == null)
                return (false, "Item not found");

            var existing = await context.Items
                .AsNoTracking()
                .FirstOrDefaultAsync(i => i.CompanyId == item.CompanyId &&
                                         i.Name == input.Name &&
                                         i.ItemId != itemId);

            if (existing != null)
                return (false, existing.IsActive
                    ? "Another Item with this name already exists"
                    : "This name was used by a previously deactivated Item. Please choose a different name");

            item.Code = input.Code;
            item.Name = input.Name;
            item.ItemGroupId = input.ItemGroupId;
            item.ItemCategoryId = input.ItemCategoryId;
            item.Unit = input.Unit;
            item.LabourRate = input.LabourRate;
            item.PackingRate = input.PackingRate;
            item.UsesCrate = input.UsesCrate;

            await context.SaveChangesAsync();
            return (true, "Item updated successfully");
        }
        catch (Exception ex)
        {
            return (false, $"Error updating Item: {ex.Message}");
        }
    }

    public async Task<(bool success, string message)> DeleteItemAsync(long itemId)
    {
        try
        {
            using var context = _contextFactory.CreateDbContext();

            var item = await context.Items.FirstOrDefaultAsync(i => i.ItemId == itemId);
            if (item == null)
                return (false, "Item not found");

            // Soft delete: an item can have purchase/sale history, so deactivating it
            // (rather than blocking or hard-deleting) is the expected behavior
            item.IsActive = false;
            await context.SaveChangesAsync();
            return (true, "Item deleted successfully");
        }
        catch (Exception ex)
        {
            return (false, $"Error deleting Item: {ex.Message}");
        }
    }
}
