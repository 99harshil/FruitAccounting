using System;
using System.Collections.Generic;

namespace FruitAccounting.Data.Entities;

public partial class Sale
{
    public long SaleId { get; set; }

    public long FinancialYearId { get; set; }

    public DateOnly SaleDate { get; set; }

    public long LotId { get; set; }

    public long BuyerId { get; set; }

    public long ItemId { get; set; }

    public long? SalesBillId { get; set; }

    public decimal Quantity { get; set; }

    public decimal? Weight { get; set; }

    public decimal Rate { get; set; }

    public decimal Amount { get; set; }

    public decimal? MarketFeePct { get; set; }

    public decimal? MarketFee { get; set; }

    public decimal? LabourRate { get; set; }

    public decimal? Labour { get; set; }

    public decimal? PackingRate { get; set; }

    public decimal? Packing { get; set; }

    public decimal? Freight { get; set; }

    public decimal? TcsPct { get; set; }

    public decimal? TcsAmount { get; set; }

    public decimal? DeliveredQty { get; set; }

    public string? DeliveredTo { get; set; }

    public string? Remarks { get; set; }

    public long? CreatedBy { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual Account Buyer { get; set; } = null!;

    public virtual User? CreatedByNavigation { get; set; }

    public virtual FinancialYear FinancialYear { get; set; } = null!;

    public virtual Item Item { get; set; } = null!;

    public virtual Lot Lot { get; set; } = null!;

    public virtual SalesBill? SalesBill { get; set; }
}
