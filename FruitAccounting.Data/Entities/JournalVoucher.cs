using System;
using System.Collections.Generic;

namespace FruitAccounting.Data.Entities;

public partial class JournalVoucher
{
    public long JournalVoucherId { get; set; }

    public long FinancialYearId { get; set; }

    public int VoucherNo { get; set; }

    public DateOnly VoucherDate { get; set; }

    public string? ReferenceNo { get; set; }

    public string? Narration { get; set; }

    public long? CreatedBy { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual User? CreatedByNavigation { get; set; }

    public virtual FinancialYear FinancialYear { get; set; } = null!;

    public virtual ICollection<JournalVoucherLine> JournalVoucherLines { get; set; } = new List<JournalVoucherLine>();
}
