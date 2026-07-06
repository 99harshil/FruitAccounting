using System;
using System.Collections.Generic;

namespace FruitAccounting.Data.Entities;

public partial class BankStatementLine
{
    public long StatementLineId { get; set; }

    public long DaybookId { get; set; }

    public DateOnly StatementDate { get; set; }

    public string? Description { get; set; }

    public string? Reference { get; set; }

    public decimal Debit { get; set; }

    public decimal Credit { get; set; }

    public DateTime ImportedAt { get; set; }

    public long? MatchedLedgerEntryId { get; set; }

    public bool Reconciled { get; set; }

    public long? ReconciledBy { get; set; }

    public DateTime? ReconciledAt { get; set; }

    public virtual Daybook Daybook { get; set; } = null!;

    public virtual LedgerEntry? MatchedLedgerEntry { get; set; }

    public virtual User? ReconciledByNavigation { get; set; }
}
