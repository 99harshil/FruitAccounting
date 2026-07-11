using System;
using System.Collections.Generic;

namespace FruitAccounting.Data.Entities;

public partial class TdsPayment
{
    public long TdsPaymentId { get; set; }

    public long FinancialYearId { get; set; }

    public int TdsPaymentNo { get; set; }

    public DateOnly PaymentDate { get; set; }

    public long TdsAccountId { get; set; }

    public long DaybookId { get; set; }

    public string? BsrCode { get; set; }

    public string? ChallanSerialNo { get; set; }

    public decimal InterestRatePct { get; set; }

    public decimal TaxAmount { get; set; }

    public decimal InterestAmount { get; set; }

    public decimal FeesAmount { get; set; }

    public decimal PenaltyAmount { get; set; }

    public long? InterestAccountId { get; set; }

    public long? FeesAccountId { get; set; }

    public long? PenaltyAccountId { get; set; }

    public decimal TotalAmount { get; set; }

    public string? Remarks { get; set; }

    public long? CreatedBy { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual Account TdsAccount { get; set; } = null!;

    public virtual Daybook Daybook { get; set; } = null!;

    public virtual Account? InterestAccount { get; set; }

    public virtual Account? FeesAccount { get; set; }

    public virtual Account? PenaltyAccount { get; set; }

    public virtual FinancialYear FinancialYear { get; set; } = null!;

    public virtual User? CreatedByNavigation { get; set; }

    public virtual ICollection<TdsPurchaseDeduction> TdsPurchaseDeductions { get; set; } = new List<TdsPurchaseDeduction>();
}
