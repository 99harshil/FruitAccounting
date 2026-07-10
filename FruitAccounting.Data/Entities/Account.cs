using System;
using System.Collections.Generic;
using FruitAccounting.Data.Enums;

namespace FruitAccounting.Data.Entities;

public partial class Account
{
    public long AccountId { get; set; }

    public long CompanyId { get; set; }

    public string Code { get; set; } = null!;

    public string Name { get; set; } = null!;

    public long AccountGroupId { get; set; }

    public long? RegionId { get; set; }

    public long? SalesmanId { get; set; }

    public string? Address1 { get; set; }

    public string? Address2 { get; set; }

    public string? City { get; set; }

    public string? PinCode { get; set; }

    public string? Phone { get; set; }

    public string? Mobile { get; set; }

    public string? Email { get; set; }

    public string? PanNo { get; set; }

    public decimal? CommissionPct { get; set; }

    public decimal? VatavPct { get; set; }

    public int? CreditDays { get; set; }

    public decimal? CreditLimit { get; set; }

    public decimal? InterestPct { get; set; }

    public decimal? TdsPct { get; set; }

    public decimal? TdsThreshold { get; set; }

    public string? BankName { get; set; }

    public string? BankBranch { get; set; }

    public string? BankAccountNo { get; set; }

    public string? BankIfsc { get; set; }

    public bool IsApmc { get; set; }

    public bool IsBlocked { get; set; }

    public PurchaseMode? DefaultPurchaseMode { get; set; }

    public string? Remarks { get; set; }

    public long? MergedInto { get; set; }

    public string? Country { get; set; }

    public string? ContactPerson { get; set; }

    public string? Fax { get; set; }

    public decimal? AamanatPct { get; set; }

    public decimal? CrateDeposit { get; set; }

    public decimal? Labour { get; set; }

    public bool IsTdsApplicable { get; set; } = true;

    public string? NameInBank { get; set; }

    public string? TinNo { get; set; }

    public string? CstNo { get; set; }

    public string? TdsHead { get; set; }

    public string? EditPin { get; set; }

    public long? AmanatPartyId { get; set; }

    // Legacy-migration-prep columns (added directly to the DB ahead of the Phase 9
    // BGT .mdb import work). Not yet used by any form - present here only so the
    // EF model and migrations stay an accurate reflection of the live schema.
    public long? CountryId { get; set; }

    public decimal? LabourPct { get; set; }

    public long? PartyGroupId { get; set; }

    public string? PersonName { get; set; }

    public string? PhoneResidence { get; set; }

    public byte[]? Photo { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual Account? AmanatParty { get; set; }

    public virtual ICollection<Account> InverseAmanatParty { get; set; } = new List<Account>();

    public virtual Country? CountryNavigation { get; set; }

    public virtual Account? PartyGroup { get; set; }

    public virtual ICollection<Account> InversePartyGroup { get; set; } = new List<Account>();

    public virtual AccountGroup AccountGroup { get; set; } = null!;

    public virtual ICollection<AccountOpeningBalance> AccountOpeningBalances { get; set; } = new List<AccountOpeningBalance>();

    public virtual ICollection<BankEntry> BankEntries { get; set; } = new List<BankEntry>();

    public virtual ICollection<ColdStorageTransaction> ColdStorageTransactionColdStores { get; set; } = new List<ColdStorageTransaction>();

    public virtual ICollection<ColdStorageTransaction> ColdStorageTransactionParties { get; set; } = new List<ColdStorageTransaction>();

    public virtual Company Company { get; set; } = null!;

    public virtual ICollection<CrateTransaction> CrateTransactions { get; set; } = new List<CrateTransaction>();

    public virtual ICollection<Daybook> Daybooks { get; set; } = new List<Daybook>();

    public virtual ICollection<DesavarPurchase> DesavarPurchases { get; set; } = new List<DesavarPurchase>();

    public virtual ICollection<DesavarSale> DesavarSales { get; set; } = new List<DesavarSale>();

    public virtual ICollection<ImportPurchase> ImportPurchases { get; set; } = new List<ImportPurchase>();

    public virtual ICollection<ImportSale> ImportSales { get; set; } = new List<ImportSale>();

    public virtual ICollection<Account> InverseMergedIntoNavigation { get; set; } = new List<Account>();

    public virtual ICollection<JournalVoucherLine> JournalVoucherLines { get; set; } = new List<JournalVoucherLine>();

    public virtual ICollection<LedgerEntry> LedgerEntryAccounts { get; set; } = new List<LedgerEntry>();

    public virtual ICollection<LedgerEntry> LedgerEntryContraAccounts { get; set; } = new List<LedgerEntry>();

    public virtual ICollection<LotOperation> LotOperations { get; set; } = new List<LotOperation>();

    public virtual ICollection<Lot> Lots { get; set; } = new List<Lot>();

    public virtual Account? MergedIntoNavigation { get; set; }

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

    public virtual ICollection<PurchaseBill> PurchaseBills { get; set; } = new List<PurchaseBill>();

    public virtual ICollection<Receipt> Receipts { get; set; } = new List<Receipt>();

    public virtual Region? Region { get; set; }

    public virtual ICollection<Sale> Sales { get; set; } = new List<Sale>();

    public virtual ICollection<SalesBill> SalesBills { get; set; } = new List<SalesBill>();

    public virtual Salesman? Salesman { get; set; }

    public virtual ICollection<TdsPurchaseDeduction> TdsPurchaseDeductions { get; set; } = new List<TdsPurchaseDeduction>();

    public virtual ICollection<WhatsappDispatch> WhatsappDispatches { get; set; } = new List<WhatsappDispatch>();
}
