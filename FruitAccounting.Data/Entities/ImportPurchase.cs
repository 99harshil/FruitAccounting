using System;
using System.Collections.Generic;

namespace FruitAccounting.Data.Entities;

public partial class ImportPurchase
{
    public long ImportPurchaseId { get; set; }

    public long FinancialYearId { get; set; }

    public string InvoiceNo { get; set; } = null!;

    public DateOnly InvoiceDate { get; set; }

    public long SupplierId { get; set; }

    public string? Country { get; set; }

    public string? BlNo { get; set; }

    public decimal? TotalUsd { get; set; }

    public decimal? ExchangeRate { get; set; }

    public decimal? CifValue { get; set; }

    public decimal? Duty { get; set; }

    public decimal? Clearing { get; set; }

    public decimal? Freight { get; set; }

    public decimal? Handling { get; set; }

    public decimal? OtherCharges { get; set; }

    public decimal? Commission { get; set; }

    public decimal NetAmount { get; set; }

    public virtual FinancialYear FinancialYear { get; set; } = null!;

    public virtual ICollection<ImportPurchaseItem> ImportPurchaseItems { get; set; } = new List<ImportPurchaseItem>();

    public virtual Account Supplier { get; set; } = null!;
}
