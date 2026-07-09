using FruitAccounting.Data.Context;
using FruitAccounting.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace FruitAccounting.Core.services;

public class ItemCategoryService
{
    private readonly IDbContextFactory<FruitAccountingContext> _contextFactory;

    public ItemCategoryService(IDbContextFactory<FruitAccountingContext> contextFactory)
    {
        _contextFactory = contextFactory;
    }

    public async Task<List<ItemCategory>> GetAllItemCategoriesAsync(long companyId)
    {
        using var context = _contextFactory.CreateDbContext();
        return await context.ItemCategories
            .AsNoTracking()
            .Where(c => c.CompanyId == companyId)
            .OrderBy(c => c.Name)
            .ToListAsync();
    }

    public async Task<(bool success, string message)> CreateItemCategoryAsync(long companyId, string name)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(name))
                return (false, "Item Category name is required");

            using var context = _contextFactory.CreateDbContext();

            var existing = await context.ItemCategories
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.CompanyId == companyId && c.Name == name);

            if (existing != null)
                return (false, "An Item Category with this name already exists");

            var category = new ItemCategory
            {
                CompanyId = companyId,
                Name = name
            };

            context.ItemCategories.Add(category);
            await context.SaveChangesAsync();
            return (true, "Item Category created successfully");
        }
        catch (Exception ex)
        {
            return (false, $"Error creating Item Category: {ex.Message}");
        }
    }

    public async Task<(bool success, string message)> UpdateItemCategoryAsync(long itemCategoryId, string name)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(name))
                return (false, "Item Category name is required");

            using var context = _contextFactory.CreateDbContext();

            var category = await context.ItemCategories.FirstOrDefaultAsync(c => c.ItemCategoryId == itemCategoryId);
            if (category == null)
                return (false, "Item Category not found");

            var existing = await context.ItemCategories
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.CompanyId == category.CompanyId &&
                                         c.Name == name &&
                                         c.ItemCategoryId != itemCategoryId);

            if (existing != null)
                return (false, "Another Item Category with this name already exists");

            category.Name = name;

            await context.SaveChangesAsync();
            return (true, "Item Category updated successfully");
        }
        catch (Exception ex)
        {
            return (false, $"Error updating Item Category: {ex.Message}");
        }
    }

    public async Task<(bool success, string message)> DeleteItemCategoryAsync(long itemCategoryId)
    {
        try
        {
            using var context = _contextFactory.CreateDbContext();

            var category = await context.ItemCategories
                .Include(c => c.Items)
                .FirstOrDefaultAsync(c => c.ItemCategoryId == itemCategoryId);

            if (category == null)
                return (false, "Item Category not found");

            if (category.Items.Any())
                return (false, "Cannot delete Item Category that has Items");

            context.ItemCategories.Remove(category);
            await context.SaveChangesAsync();
            return (true, "Item Category deleted successfully");
        }
        catch (Exception ex)
        {
            return (false, $"Error deleting Item Category: {ex.Message}");
        }
    }
}
