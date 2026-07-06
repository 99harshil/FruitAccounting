using System;
using System.Collections.Generic;

namespace FruitAccounting.Data.Entities;

public partial class JournalVoucherLine
{
    public long JournalVoucherLineId { get; set; }

    public long JournalVoucherId { get; set; }

    public long AccountId { get; set; }

    public decimal Debit { get; set; }

    public decimal Credit { get; set; }

    public string? Narration { get; set; }

    public virtual Account Account { get; set; } = null!;

    public virtual JournalVoucher JournalVoucher { get; set; } = null!;
}
