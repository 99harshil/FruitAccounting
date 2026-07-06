using System;
using System.Collections.Generic;

namespace FruitAccounting.Data.Entities;

public partial class DesavarSale
{
    public long DesavarSaleId { get; set; }

    public long FinancialYearId { get; set; }

    public DateOnly SaleDate { get; set; }

    public long? DesavarPurchaseId { get; set; }

    public long? LotId { get; set; }

    public long BuyerId { get; set; }

    public long ItemId { get; set; }

    public decimal Quantity { get; set; }

    public decimal? Weight { get; set; }

    public decimal Rate { get; set; }

    public decimal? PurchaseRate { get; set; }

    public decimal Amount { get; set; }

    public virtual Account Buyer { get; set; } = null!;

    public virtual DesavarPurchase? DesavarPurchase { get; set; }

    public virtual FinancialYear FinancialYear { get; set; } = null!;

    public virtual Item Item { get; set; } = null!;

    public virtual Lot? Lot { get; set; }
}
