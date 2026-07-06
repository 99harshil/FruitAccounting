using System;
using System.Collections.Generic;

namespace FruitAccounting.Data.Entities;

public partial class DesavarPurchase
{
    public long DesavarPurchaseId { get; set; }

    public long FinancialYearId { get; set; }

    public int BillNo { get; set; }

    public DateOnly BillDate { get; set; }

    public string? Origin { get; set; }

    public long SupplierId { get; set; }

    public long ItemId { get; set; }

    public long? LotId { get; set; }

    public decimal Quantity { get; set; }

    public decimal Rate { get; set; }

    public decimal? Freight { get; set; }

    public decimal? OtherCharges { get; set; }

    public decimal NetAmount { get; set; }

    public virtual ICollection<DesavarSale> DesavarSales { get; set; } = new List<DesavarSale>();

    public virtual FinancialYear FinancialYear { get; set; } = null!;

    public virtual Item Item { get; set; } = null!;

    public virtual Lot? Lot { get; set; }

    public virtual Account Supplier { get; set; } = null!;
}
