using System;
using System.Collections.Generic;
using FruitAccounting.Data.Enums;

namespace FruitAccounting.Data.Entities;

public partial class CrateTransaction
{
    public long CrateTxnId { get; set; }

    public long FinancialYearId { get; set; }

    public int TxnNo { get; set; }

    public DateOnly TxnDate { get; set; }

    public long AccountId { get; set; }

    public CrateTxnType TxnType { get; set; }

    public string? TruckNo { get; set; }

    public string? Remarks { get; set; }

    public long? CreatedBy { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual Account Account { get; set; } = null!;

    public virtual ICollection<CrateTransactionItem> CrateTransactionItems { get; set; } = new List<CrateTransactionItem>();

    public virtual User? CreatedByNavigation { get; set; }

    public virtual FinancialYear FinancialYear { get; set; } = null!;
}
