using FruitAccounting.Data.Context;
using FruitAccounting.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace FruitAccounting.Core.services;

public class RegionService
{
    private readonly IDbContextFactory<FruitAccountingContext> _contextFactory;

    public RegionService(IDbContextFactory<FruitAccountingContext> contextFactory)
    {
        _contextFactory = contextFactory;
    }

    public async Task<List<Region>> GetAllRegionsAsync(long companyId)
    {
        using var context = _contextFactory.CreateDbContext();
        return await context.Regions
            .AsNoTracking()
            .Where(r => r.CompanyId == companyId)
            .OrderBy(r => r.Name)
            .ToListAsync();
    }

    public async Task<(bool success, string message)> CreateRegionAsync(long companyId, string code, string name)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(code))
                return (false, "Region code is required");

            if (string.IsNullOrWhiteSpace(name))
                return (false, "Region name is required");

            using var context = _contextFactory.CreateDbContext();

            // DB enforces uniqueness on (company_id, code) and (company_id, name) separately
            var existing = await context.Regions
                .AsNoTracking()
                .FirstOrDefaultAsync(r => r.CompanyId == companyId && (r.Code == code || r.Name == name));

            if (existing != null)
            {
                return existing.Code == code
                    ? (false, "A Region with this code already exists")
                    : (false, "A Region with this name already exists");
            }

            var region = new Region
            {
                CompanyId = companyId,
                Code = code,
                Name = name
            };

            context.Regions.Add(region);
            await context.SaveChangesAsync();
            return (true, "Region created successfully");
        }
        catch (Exception ex)
        {
            return (false, $"Error creating Region: {ex.Message}");
        }
    }

    public async Task<(bool success, string message)> UpdateRegionAsync(long regionId, string code, string name)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(code))
                return (false, "Region code is required");

            if (string.IsNullOrWhiteSpace(name))
                return (false, "Region name is required");

            using var context = _contextFactory.CreateDbContext();

            var region = await context.Regions.FirstOrDefaultAsync(r => r.RegionId == regionId);
            if (region == null)
                return (false, "Region not found");

            var existing = await context.Regions
                .AsNoTracking()
                .FirstOrDefaultAsync(r => r.CompanyId == region.CompanyId &&
                                         (r.Code == code || r.Name == name) &&
                                         r.RegionId != regionId);

            if (existing != null)
            {
                return existing.Code == code
                    ? (false, "Another Region with this code already exists")
                    : (false, "Another Region with this name already exists");
            }

            region.Code = code;
            region.Name = name;

            await context.SaveChangesAsync();
            return (true, "Region updated successfully");
        }
        catch (Exception ex)
        {
            return (false, $"Error updating Region: {ex.Message}");
        }
    }

    public async Task<(bool success, string message)> DeleteRegionAsync(long regionId)
    {
        try
        {
            using var context = _contextFactory.CreateDbContext();

            var region = await context.Regions
                .Include(r => r.Accounts)
                .FirstOrDefaultAsync(r => r.RegionId == regionId);

            if (region == null)
                return (false, "Region not found");

            // No soft-delete column on this table - block deletion if it's referenced by any Account
            if (region.Accounts.Any())
                return (false, "Cannot delete Region that is used by one or more Accounts");

            context.Regions.Remove(region);
            await context.SaveChangesAsync();
            return (true, "Region deleted successfully");
        }
        catch (Exception ex)
        {
            return (false, $"Error deleting Region: {ex.Message}");
        }
    }
}
