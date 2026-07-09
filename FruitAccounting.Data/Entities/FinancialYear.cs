using System;
using System.Collections.Generic;

namespace FruitAccounting.Data.Entities;

public partial class FinancialYear
{
    public long FinancialYearId { get; set; }

    public long CompanyId { get; set; }

    public string Code { get; set; } = null!;

    public DateOnly StartDate { get; set; }

    public DateOnly EndDate { get; set; }

    public bool IsActive { get; set; } = true;

    public bool IsClosed { get; set; }

    public virtual ICollection<AccountOpeningBalance> AccountOpeningBalances { get; set; } = new List<AccountOpeningBalance>();

    public virtual ICollection<BankEntry> BankEntries { get; set; } = new List<BankEntry>();

    public virtual ICollection<ColdStorageTransaction> ColdStorageTransactions { get; set; } = new List<ColdStorageTransaction>();

    public virtual Company Company { get; set; } = null!;

    public virtual ICollection<CrateTransaction> CrateTransactions { get; set; } = new List<CrateTransaction>();

    public virtual ICollection<DataLock> DataLocks { get; set; } = new List<DataLock>();

    public virtual ICollection<DesavarPurchase> DesavarPurchases { get; set; } = new List<DesavarPurchase>();

    public virtual ICollection<DesavarSale> DesavarSales { get; set; } = new List<DesavarSale>();

    public virtual ICollection<ImportPurchase> ImportPurchases { get; set; } = new List<ImportPurchase>();

    public virtual ICollection<ImportSale> ImportSales { get; set; } = new List<ImportSale>();

    public virtual ICollection<JournalVoucher> JournalVouchers { get; set; } = new List<JournalVoucher>();

    public virtual ICollection<LedgerEntry> LedgerEntries { get; set; } = new List<LedgerEntry>();

    public virtual ICollection<LotOperation> LotOperations { get; set; } = new List<LotOperation>();

    public virtual ICollection<Lot> Lots { get; set; } = new List<Lot>();

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

    public virtual ICollection<PurchaseBill> PurchaseBills { get; set; } = new List<PurchaseBill>();

    public virtual ICollection<Receipt> Receipts { get; set; } = new List<Receipt>();

    public virtual ICollection<Sale> Sales { get; set; } = new List<Sale>();

    public virtual ICollection<SalesBill> SalesBills { get; set; } = new List<SalesBill>();

    public virtual ICollection<TdsPurchaseDeduction> TdsPurchaseDeductions { get; set; } = new List<TdsPurchaseDeduction>();
}
