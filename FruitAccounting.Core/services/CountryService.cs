using FruitAccounting.Data.Context;
using FruitAccounting.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace FruitAccounting.Core.services;

public class CountryService
{
    private readonly IDbContextFactory<FruitAccountingContext> _contextFactory;

    public CountryService(IDbContextFactory<FruitAccountingContext> contextFactory)
    {
        _contextFactory = contextFactory;
    }

    public async Task<List<Country>> GetAllCountriesAsync(long companyId)
    {
        using var context = _contextFactory.CreateDbContext();
        return await context.Countries
            .AsNoTracking()
            .Where(c => c.CompanyId == companyId)
            .OrderBy(c => c.Name)
            .ToListAsync();
    }

    public async Task<(bool success, string message)> CreateCountryAsync(long companyId, string name)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(name))
                return (false, "Country name is required");

            using var context = _contextFactory.CreateDbContext();

            var existing = await context.Countries
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.CompanyId == companyId && c.Name == name);

            if (existing != null)
                return (false, "A Country with this name already exists");

            var country = new Country
            {
                CompanyId = companyId,
                Name = name
            };

            context.Countries.Add(country);
            await context.SaveChangesAsync();
            return (true, "Country created successfully");
        }
        catch (Exception ex)
        {
            return (false, $"Error creating Country: {ex.Message}");
        }
    }

    public async Task<(bool success, string message)> UpdateCountryAsync(long countryId, string name)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(name))
                return (false, "Country name is required");

            using var context = _contextFactory.CreateDbContext();

            var country = await context.Countries.FirstOrDefaultAsync(c => c.CountryId == countryId);
            if (country == null)
                return (false, "Country not found");

            var existing = await context.Countries
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.CompanyId == country.CompanyId &&
                                         c.Name == name &&
                                         c.CountryId != countryId);

            if (existing != null)
                return (false, "Another Country with this name already exists");

            country.Name = name;

            await context.SaveChangesAsync();
            return (true, "Country updated successfully");
        }
        catch (Exception ex)
        {
            return (false, $"Error updating Country: {ex.Message}");
        }
    }

    public async Task<(bool success, string message)> DeleteCountryAsync(long countryId)
    {
        try
        {
            using var context = _contextFactory.CreateDbContext();

            var country = await context.Countries.FirstOrDefaultAsync(c => c.CountryId == countryId);
            if (country == null)
                return (false, "Country not found");

            context.Countries.Remove(country);
            await context.SaveChangesAsync();
            return (true, "Country deleted successfully");
        }
        catch (Exception ex)
        {
            return (false, $"Error deleting Country: {ex.Message}");
        }
    }
}
