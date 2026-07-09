using FruitAccounting.Data.Context;
using FruitAccounting.Data.Entities;
using FruitAccounting.Data.Enums;
using Microsoft.EntityFrameworkCore;

namespace FruitAccounting.Core.services;

public class AccountService
{
    private readonly IDbContextFactory<FruitAccountingContext> _contextFactory;

    public AccountService(IDbContextFactory<FruitAccountingContext> contextFactory)
    {
        _contextFactory = contextFactory;
    }

    public async Task<List<Account>> GetAllAccountsAsync(long companyId)
    {
        using var context = _contextFactory.CreateDbContext();
        return await context.Accounts
            .AsNoTracking()
            .Include(a => a.AccountGroup)
            .Include(a => a.Region)
            .Include(a => a.AmanatParty)
            .Where(a => a.CompanyId == companyId)
            .OrderBy(a => a.Name)
            .ToListAsync();
    }

    public async Task<AccountOpeningBalance?> GetOpeningBalanceAsync(long accountId, long financialYearId)
    {
        using var context = _contextFactory.CreateDbContext();
        return await context.AccountOpeningBalances
            .AsNoTracking()
            .FirstOrDefaultAsync(b => b.AccountId == accountId && b.FinancialYearId == financialYearId);
    }

    public async Task<Dictionary<long, AccountOpeningBalance>> GetOpeningBalancesForYearAsync(long companyId, long financialYearId)
    {
        using var context = _contextFactory.CreateDbContext();
        var balances = await context.AccountOpeningBalances
            .AsNoTracking()
            .Where(b => b.FinancialYearId == financialYearId && b.Account.CompanyId == companyId)
            .ToListAsync();
        return balances.ToDictionary(b => b.AccountId);
    }

    /// <summary>
    /// Sets (or clears, when amount is 0) an account's opening balance for a financial year.
    /// Intended for the financial-year carry-forward utility, not for manual entry on the Account form.
    /// </summary>
    public async Task<(bool success, string message)> SetOpeningBalanceAsync(long accountId, long financialYearId, decimal amount, DrCr side)
    {
        try
        {
            using var context = _contextFactory.CreateDbContext();

            var existing = await context.AccountOpeningBalances
                .FirstOrDefaultAsync(b => b.AccountId == accountId && b.FinancialYearId == financialYearId);

            if (amount == 0)
            {
                if (existing != null)
                    context.AccountOpeningBalances.Remove(existing);
            }
            else if (existing != null)
            {
                existing.Amount = amount;
                existing.Side = side;
            }
            else
            {
                context.AccountOpeningBalances.Add(new AccountOpeningBalance
                {
                    AccountId = accountId,
                    FinancialYearId = financialYearId,
                    Amount = amount,
                    Side = side
                });
            }

            await context.SaveChangesAsync();
            return (true, "Opening balance updated successfully");
        }
        catch (Exception ex)
        {
            return (false, $"Error setting opening balance: {ex.Message}");
        }
    }

    public class AccountInput
    {
        public string Code { get; set; } = "";
        public string Name { get; set; } = "";
        public long AccountGroupId { get; set; }
        public long? RegionId { get; set; }
        public string? Address1 { get; set; }
        public string? Address2 { get; set; }
        public string? City { get; set; }
        public string? PinCode { get; set; }
        public string? Country { get; set; }
        public string? ContactPerson { get; set; }
        public string? Phone { get; set; }
        public string? Mobile { get; set; }
        public string? Fax { get; set; }
        public string? Email { get; set; }
        public decimal? CommissionPct { get; set; }
        public decimal? VatavPct { get; set; }
        public decimal? AamanatPct { get; set; }
        public decimal? CrateDeposit { get; set; }
        public decimal? Labour { get; set; }
        public bool IsApmc { get; set; }
        public bool IsTdsApplicable { get; set; } = true;
        public string? TdsHead { get; set; }
        public long? AmanatPartyId { get; set; }
        public string? NameInBank { get; set; }
        public decimal? CreditLimit { get; set; }
        public string? BankName { get; set; }
        public string? BankBranch { get; set; }
        public string? BankAccountNo { get; set; }
        public string? BankIfsc { get; set; }
        public string? PanNo { get; set; }
        public string? TinNo { get; set; }
        public string? CstNo { get; set; }
        public string? EditPin { get; set; }
        public bool IsBlocked { get; set; }
    }

    public async Task<(bool success, string message)> CreateAccountAsync(long companyId, AccountInput input)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(input.Code))
                return (false, "Code is required");

            if (string.IsNullOrWhiteSpace(input.Name))
                return (false, "Name is required");

            using var context = _contextFactory.CreateDbContext();

            var existing = await context.Accounts
                .AsNoTracking()
                .FirstOrDefaultAsync(a => a.CompanyId == companyId && a.Code == input.Code);

            if (existing != null)
                return (false, "An Account with this code already exists");

            var account = new Account
            {
                CompanyId = companyId,
                Code = input.Code,
                Name = input.Name,
                AccountGroupId = input.AccountGroupId,
                RegionId = input.RegionId,
                Address1 = input.Address1,
                Address2 = input.Address2,
                City = input.City,
                PinCode = input.PinCode,
                Country = input.Country,
                ContactPerson = input.ContactPerson,
                Phone = input.Phone,
                Mobile = input.Mobile,
                Fax = input.Fax,
                Email = input.Email,
                CommissionPct = input.CommissionPct,
                VatavPct = input.VatavPct,
                AamanatPct = input.AamanatPct,
                CrateDeposit = input.CrateDeposit,
                Labour = input.Labour,
                IsApmc = input.IsApmc,
                IsTdsApplicable = input.IsTdsApplicable,
                TdsHead = input.TdsHead,
                AmanatPartyId = input.AmanatPartyId,
                NameInBank = input.NameInBank,
                CreditLimit = input.CreditLimit,
                BankName = input.BankName,
                BankBranch = input.BankBranch,
                BankAccountNo = input.BankAccountNo,
                BankIfsc = input.BankIfsc,
                PanNo = input.PanNo,
                TinNo = input.TinNo,
                CstNo = input.CstNo,
                EditPin = input.EditPin,
                IsBlocked = input.IsBlocked
            };

            context.Accounts.Add(account);
            await context.SaveChangesAsync();

            // Amanat Party defaults to the account itself unless a different party was chosen
            if (input.AmanatPartyId == null)
            {
                account.AmanatPartyId = account.AccountId;
                await context.SaveChangesAsync();
            }

            // New accounts start with no opening balance (0). A carried-forward balance from a
            // prior financial year is set separately via SetOpeningBalanceAsync, not entered here.
            return (true, "Account created successfully");
        }
        catch (Exception ex)
        {
            return (false, $"Error creating Account: {ex.Message}");
        }
    }

    public async Task<(bool success, string message)> UpdateAccountAsync(long accountId, AccountInput input)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(input.Code))
                return (false, "Code is required");

            if (string.IsNullOrWhiteSpace(input.Name))
                return (false, "Name is required");

            using var context = _contextFactory.CreateDbContext();

            var account = await context.Accounts.FirstOrDefaultAsync(a => a.AccountId == accountId);
            if (account == null)
                return (false, "Account not found");

            var existing = await context.Accounts
                .AsNoTracking()
                .FirstOrDefaultAsync(a => a.CompanyId == account.CompanyId &&
                                         a.Code == input.Code &&
                                         a.AccountId != accountId);

            if (existing != null)
                return (false, "Another Account with this code already exists");

            account.Code = input.Code;
            account.Name = input.Name;
            account.AccountGroupId = input.AccountGroupId;
            account.RegionId = input.RegionId;
            account.Address1 = input.Address1;
            account.Address2 = input.Address2;
            account.City = input.City;
            account.PinCode = input.PinCode;
            account.Country = input.Country;
            account.ContactPerson = input.ContactPerson;
            account.Phone = input.Phone;
            account.Mobile = input.Mobile;
            account.Fax = input.Fax;
            account.Email = input.Email;
            account.CommissionPct = input.CommissionPct;
            account.VatavPct = input.VatavPct;
            account.AamanatPct = input.AamanatPct;
            account.CrateDeposit = input.CrateDeposit;
            account.Labour = input.Labour;
            account.IsApmc = input.IsApmc;
            account.IsTdsApplicable = input.IsTdsApplicable;
            account.TdsHead = input.TdsHead;
            account.AmanatPartyId = input.AmanatPartyId ?? accountId; // default back to self if cleared
            account.NameInBank = input.NameInBank;
            account.CreditLimit = input.CreditLimit;
            account.BankName = input.BankName;
            account.BankBranch = input.BankBranch;
            account.BankAccountNo = input.BankAccountNo;
            account.BankIfsc = input.BankIfsc;
            account.PanNo = input.PanNo;
            account.TinNo = input.TinNo;
            account.CstNo = input.CstNo;
            account.EditPin = input.EditPin;
            account.IsBlocked = input.IsBlocked;
            account.UpdatedAt = DateTime.UtcNow;

            // Opening balance is untouched by this method - it's only ever set by
            // SetOpeningBalanceAsync (the future financial-year carry-forward utility)
            await context.SaveChangesAsync();
            return (true, "Account updated successfully");
        }
        catch (Exception ex)
        {
            return (false, $"Error updating Account: {ex.Message}");
        }
    }

    public async Task<(bool success, string message)> DeleteAccountAsync(long accountId)
    {
        try
        {
            using var context = _contextFactory.CreateDbContext();

            var account = await context.Accounts
                .Include(a => a.AccountOpeningBalances)
                .Include(a => a.LedgerEntryAccounts)
                .Include(a => a.LedgerEntryContraAccounts)
                .Include(a => a.InverseAmanatParty)
                .FirstOrDefaultAsync(a => a.AccountId == accountId);

            if (account == null)
                return (false, "Account not found");

            if (account.AccountOpeningBalances.Any())
                return (false, "Cannot delete Account that has an Opening Balance recorded in one or more financial years");

            if (account.LedgerEntryAccounts.Any() || account.LedgerEntryContraAccounts.Any())
                return (false, "Cannot delete Account that has transactions posted against it. Use 'Block Party' instead to retire it");

            if (account.InverseAmanatParty.Any(a => a.AccountId != accountId))
                return (false, "Cannot delete Account that is set as the Amanat Party for other accounts");

            context.Accounts.Remove(account);
            await context.SaveChangesAsync();
            return (true, "Account deleted successfully");
        }
        catch (Exception ex)
        {
            return (false, $"Error deleting Account: {ex.Message}");
        }
    }
}
