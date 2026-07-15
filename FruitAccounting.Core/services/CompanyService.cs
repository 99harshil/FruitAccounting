using FruitAccounting.Data.Context;
using FruitAccounting.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace FruitAccounting.Core.services;

public class CompanyService
{
    private readonly IDbContextFactory<FruitAccountingContext> _contextFactory;

    public CompanyService(IDbContextFactory<FruitAccountingContext> contextFactory)
    {
        _contextFactory = contextFactory;
    }

    public async Task<Company?> GetCompanyAsync(long companyId)
    {
        using var context = _contextFactory.CreateDbContext();
        return await context.Companies.AsNoTracking().FirstOrDefaultAsync(c => c.CompanyId == companyId);
    }

    public class CompanyInfoInput
    {
        public string? Address1 { get; set; }
        public string? Address2 { get; set; }
        public string? City { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? PanNo { get; set; }
        public string? Gstin { get; set; }
        public string? ApmcLicenceNo { get; set; }
        public decimal? ApmcPct { get; set; }
        public string? BankName { get; set; }
        public string? BankAccountNo { get; set; }
        public string? BankIfsc { get; set; }
    }

    public async Task<(bool success, string message)> UpdateCompanyInfoAsync(long companyId, CompanyInfoInput input)
    {
        try
        {
            using var context = _contextFactory.CreateDbContext();

            var company = await context.Companies.FirstOrDefaultAsync(c => c.CompanyId == companyId);
            if (company == null)
                return (false, "Company not found");

            company.Address1 = input.Address1;
            company.Address2 = input.Address2;
            company.City = input.City;
            company.Phone = input.Phone;
            company.Email = input.Email;
            company.PanNo = input.PanNo;
            company.Gstin = input.Gstin;
            company.ApmcLicenceNo = input.ApmcLicenceNo;
            company.ApmcPct = input.ApmcPct;
            company.BankName = input.BankName;
            company.BankAccountNo = input.BankAccountNo;
            company.BankIfsc = input.BankIfsc;

            await context.SaveChangesAsync();
            return (true, "Company Information updated successfully");
        }
        catch (Exception ex)
        {
            return (false, $"Error updating Company Information: {ex.Message}");
        }
    }
}
