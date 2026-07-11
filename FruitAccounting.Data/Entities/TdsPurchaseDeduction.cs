using System;
using System.Collections.Generic;

namespace FruitAccounting.Data.Entities;

public partial class TdsPurchaseDeduction
{
    public long TdsDeductionId { get; set; }

    public long FinancialYearId { get; set; }

    public long SupplierId { get; set; }

    public long PaymentId { get; set; }

    public decimal CumulativeBefore { get; set; }

    public decimal TaxableExcess { get; set; }

    public decimal TdsRate { get; set; }

    public decimal TdsAmount { get; set; }

    public DateTime DeductedAt { get; set; }

    public long? TdsPaymentId { get; set; }

    public virtual FinancialYear FinancialYear { get; set; } = null!;

    public virtual Payment Payment { get; set; } = null!;

    public virtual Account Supplier { get; set; } = null!;

    public virtual TdsPayment? TdsPayment { get; set; }
}
