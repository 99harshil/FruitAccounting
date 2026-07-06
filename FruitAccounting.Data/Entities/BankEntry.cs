using System;
using System.Collections.Generic;
using FruitAccounting.Data.Enums;

namespace FruitAccounting.Data.Entities;

public partial class BankEntry
{
    public long BankEntryId { get; set; }

    public long FinancialYearId { get; set; }

    public int EntryNo { get; set; }

    public DateOnly EntryDate { get; set; }

    public long DaybookId { get; set; }

    public long AccountId { get; set; }

    public decimal Amount { get; set; }

    public DrCr Side { get; set; }

    public string? ChequeNo { get; set; }

    public string? Remarks { get; set; }

    public long? CreatedBy { get; set; }

    public virtual Account Account { get; set; } = null!;

    public virtual User? CreatedByNavigation { get; set; }

    public virtual Daybook Daybook { get; set; } = null!;

    public virtual FinancialYear FinancialYear { get; set; } = null!;
}
