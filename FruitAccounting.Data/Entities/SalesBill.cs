using System;
using System.Collections.Generic;

namespace FruitAccounting.Data.Entities;

public partial class SalesBill
{
    public long SalesBillId { get; set; }

    public long FinancialYearId { get; set; }

    public int BillNo { get; set; }

    public DateOnly BillDate { get; set; }

    public long BuyerId { get; set; }

    public bool IsWeekly { get; set; }

    public DateOnly? PeriodFrom { get; set; }

    public DateOnly? PeriodTo { get; set; }

    public decimal TotalAmount { get; set; }

    public decimal NetAmount { get; set; }

    public long? CreatedBy { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual Account Buyer { get; set; } = null!;

    public virtual User? CreatedByNavigation { get; set; }

    public virtual FinancialYear FinancialYear { get; set; } = null!;

    public virtual ICollection<ReceiptAllocation> ReceiptAllocations { get; set; } = new List<ReceiptAllocation>();

    public virtual ICollection<Sale> Sales { get; set; } = new List<Sale>();
}
