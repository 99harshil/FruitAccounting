using System;
using System.Collections.Generic;
using FruitAccounting.Data.Enums;

namespace FruitAccounting.Data.Entities;

public partial class LedgerEntry
{
    public long LedgerEntryId { get; set; }

    public long FinancialYearId { get; set; }

    public DateOnly EntryDate { get; set; }

    public long AccountId { get; set; }

    public decimal Debit { get; set; }

    public decimal Credit { get; set; }

    public long VoucherId { get; set; }

    public VoucherType VoucherType { get; set; }

    public string? BillNo { get; set; }

    public string? Narration { get; set; }

    public string? ChequeNo { get; set; }

    public long? ContraAccountId { get; set; }

    public decimal? Quantity { get; set; }

    public DateOnly? ClearanceDate { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual Account Account { get; set; } = null!;

    public virtual ICollection<BankStatementLine> BankStatementLines { get; set; } = new List<BankStatementLine>();

    public virtual Account? ContraAccount { get; set; }

    public virtual FinancialYear FinancialYear { get; set; } = null!;
}
