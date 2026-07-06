using System;
using System.Collections.Generic;

namespace FruitAccounting.Data.Entities;

public partial class CrateTransactionItem
{
    public long CrateTxnItemId { get; set; }

    public long CrateTxnId { get; set; }

    public string CrateType { get; set; } = null!;

    public decimal Quantity { get; set; }

    public decimal? Rate { get; set; }

    public decimal? Amount { get; set; }

    public virtual CrateTransaction CrateTxn { get; set; } = null!;
}
