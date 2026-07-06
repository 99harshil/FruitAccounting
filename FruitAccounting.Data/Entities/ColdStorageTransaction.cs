using System;
using System.Collections.Generic;
using FruitAccounting.Data.Enums;

namespace FruitAccounting.Data.Entities;

public partial class ColdStorageTransaction
{
    public long ColdTxnId { get; set; }

    public long FinancialYearId { get; set; }

    public int TxnNo { get; set; }

    public DateOnly TxnDate { get; set; }

    public long ColdStoreId { get; set; }

    public long? PartyId { get; set; }

    public ColdTxnType TxnType { get; set; }

    public string? Remarks { get; set; }

    public long? CreatedBy { get; set; }

    public virtual ICollection<ColdStorageTransactionItem> ColdStorageTransactionItems { get; set; } = new List<ColdStorageTransactionItem>();

    public virtual Account ColdStore { get; set; } = null!;

    public virtual User? CreatedByNavigation { get; set; }

    public virtual FinancialYear FinancialYear { get; set; } = null!;

    public virtual Account? Party { get; set; }
}
