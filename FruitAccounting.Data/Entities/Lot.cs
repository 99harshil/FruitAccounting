using System;
using System.Collections.Generic;

namespace FruitAccounting.Data.Entities;

public partial class Lot
{
    public long LotId { get; set; }

    public long FinancialYearId { get; set; }

    public int LotNo { get; set; }

    public long SupplierId { get; set; }

    public long ItemId { get; set; }

    public DateOnly ReceivedDate { get; set; }

    public long? ParentLotId { get; set; }

    public bool IsClosed { get; set; }

    public virtual ICollection<ColdStorageTransactionItem> ColdStorageTransactionItems { get; set; } = new List<ColdStorageTransactionItem>();

    public virtual ICollection<DesavarPurchase> DesavarPurchases { get; set; } = new List<DesavarPurchase>();

    public virtual ICollection<DesavarSale> DesavarSales { get; set; } = new List<DesavarSale>();

    public virtual FinancialYear FinancialYear { get; set; } = null!;

    public virtual ICollection<ImportPurchaseItem> ImportPurchaseItems { get; set; } = new List<ImportPurchaseItem>();

    public virtual ICollection<ImportSale> ImportSales { get; set; } = new List<ImportSale>();

    public virtual ICollection<Lot> InverseParentLot { get; set; } = new List<Lot>();

    public virtual Item Item { get; set; } = null!;

    public virtual ICollection<LotOperation> LotOperationSourceLots { get; set; } = new List<LotOperation>();

    public virtual ICollection<LotOperation> LotOperationTargetLots { get; set; } = new List<LotOperation>();

    public virtual Lot? ParentLot { get; set; }

    public virtual ICollection<PurchaseBillItem> PurchaseBillItems { get; set; } = new List<PurchaseBillItem>();

    public virtual ICollection<Sale> Sales { get; set; } = new List<Sale>();

    public virtual Account Supplier { get; set; } = null!;
}
