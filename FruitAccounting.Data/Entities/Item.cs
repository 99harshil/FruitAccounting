using System;
using System.Collections.Generic;

namespace FruitAccounting.Data.Entities;

public partial class Item
{
    public long ItemId { get; set; }

    public long CompanyId { get; set; }

    public string? Code { get; set; }

    public string Name { get; set; } = null!;

    public long? ItemGroupId { get; set; }

    public long? ItemCategoryId { get; set; }

    public string Unit { get; set; } = null!;

    public decimal? LabourRate { get; set; }

    public decimal? PackingRate { get; set; }

    public bool UsesCrate { get; set; }

    public bool IsActive { get; set; }

    public virtual ICollection<ColdStorageTransactionItem> ColdStorageTransactionItems { get; set; } = new List<ColdStorageTransactionItem>();

    public virtual Company Company { get; set; } = null!;

    public virtual ICollection<DesavarPurchase> DesavarPurchases { get; set; } = new List<DesavarPurchase>();

    public virtual ICollection<DesavarSale> DesavarSales { get; set; } = new List<DesavarSale>();

    public virtual ICollection<ImportPurchaseItem> ImportPurchaseItems { get; set; } = new List<ImportPurchaseItem>();

    public virtual ICollection<ImportSale> ImportSales { get; set; } = new List<ImportSale>();

    public virtual ItemCategory? ItemCategory { get; set; }

    public virtual ItemGroup? ItemGroup { get; set; }

    public virtual ICollection<Lot> Lots { get; set; } = new List<Lot>();

    public virtual ICollection<PurchaseBillItem> PurchaseBillItems { get; set; } = new List<PurchaseBillItem>();

    public virtual ICollection<Sale> Sales { get; set; } = new List<Sale>();
}
