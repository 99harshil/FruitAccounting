using System;
using System.Collections.Generic;
using FruitAccounting.Data.Enums;

namespace FruitAccounting.Data.Entities;

public partial class Payment
{
    public long PaymentId { get; set; }

    public long FinancialYearId { get; set; }

    public int PaymentNo { get; set; }

    public DateOnly PaymentDate { get; set; }

    public long AccountId { get; set; }

    public long DaybookId { get; set; }

    public PaymentMode Mode { get; set; }

    public string? ChequeNo { get; set; }

    public DateOnly? ChequeDate { get; set; }

    public string? BankName { get; set; }

    public string? BankBranch { get; set; }

    public decimal Amount { get; set; }

    public decimal TdsAmount { get; set; }

    public decimal Vatav { get; set; }

    public decimal Hamali { get; set; }

    public decimal Discount { get; set; }

    public decimal Advance { get; set; }

    public decimal TotalSettled { get; set; }

    public bool IsFreightPayment { get; set; }

    public DateOnly? ClearanceDate { get; set; }

    public bool IsReturned { get; set; }

    public DateOnly? ReturnedDate { get; set; }

    public string? Remarks { get; set; }

    public long? CreatedBy { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual Account Account { get; set; } = null!;

    public virtual User? CreatedByNavigation { get; set; }

    public virtual Daybook Daybook { get; set; } = null!;

    public virtual FinancialYear FinancialYear { get; set; } = null!;

    public virtual ICollection<PaymentAllocation> PaymentAllocations { get; set; } = new List<PaymentAllocation>();

    public virtual ICollection<TdsPurchaseDeduction> TdsPurchaseDeductions { get; set; } = new List<TdsPurchaseDeduction>();
}
