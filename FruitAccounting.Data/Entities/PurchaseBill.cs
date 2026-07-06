using System;
using System.Collections.Generic;
using FruitAccounting.Data.Enums;

namespace FruitAccounting.Data.Entities;

public partial class PurchaseBill
{
    public long PurchaseBillId { get; set; }

    public long FinancialYearId { get; set; }

    public int BillNo { get; set; }

    public DateOnly BillDate { get; set; }

    public long SupplierId { get; set; }

    public PurchaseMode Mode { get; set; }

    public string? ChallanNo { get; set; }

    public string? TruckNo { get; set; }

    public string? Mark { get; set; }

    public string? DeliveryPerson { get; set; }

    public decimal GrossAmount { get; set; }

    public decimal CommissionPct { get; set; }

    public decimal CommissionAmount { get; set; }

    public decimal? MarketFeePct { get; set; }

    public decimal? MarketFee { get; set; }

    public decimal? Freight { get; set; }

    public decimal? Labour { get; set; }

    public decimal? Postage { get; set; }

    public decimal? PackingMaterial { get; set; }

    public decimal? ColdStore { get; set; }

    public decimal? Vatav { get; set; }

    public decimal? Amanat { get; set; }

    public decimal? DdCharge { get; set; }

    public decimal? Inam { get; set; }

    public decimal? OtherDeduction { get; set; }

    public decimal NetAmount { get; set; }

    public decimal? OurFreight { get; set; }

    public decimal? OurLabour { get; set; }

    public decimal? OurMarketFee { get; set; }

    public decimal? OurOther { get; set; }

    public decimal? TdsPct { get; set; }

    public decimal? TdsAmount { get; set; }

    public string? Remarks { get; set; }

    public string? AuthorisedBy { get; set; }

    public long? CreatedBy { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual User? CreatedByNavigation { get; set; }

    public virtual FinancialYear FinancialYear { get; set; } = null!;

    public virtual ICollection<PaymentAllocation> PaymentAllocations { get; set; } = new List<PaymentAllocation>();

    public virtual ICollection<PurchaseBillItem> PurchaseBillItems { get; set; } = new List<PurchaseBillItem>();

    public virtual Account Supplier { get; set; } = null!;
}
