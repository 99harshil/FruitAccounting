using System;
using System.Collections.Generic;

namespace FruitAccounting.Data.Entities;

public partial class Daybook
{
    public long DaybookId { get; set; }

    public long CompanyId { get; set; }

    public string Name { get; set; } = null!;

    public char BookType { get; set; }

    public long? LinkedAccountId { get; set; }

    public virtual ICollection<BankEntry> BankEntries { get; set; } = new List<BankEntry>();

    public virtual ICollection<BankStatementLine> BankStatementLines { get; set; } = new List<BankStatementLine>();

    public virtual Company Company { get; set; } = null!;

    public virtual Account? LinkedAccount { get; set; }

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

    public virtual ICollection<Receipt> Receipts { get; set; } = new List<Receipt>();
}
