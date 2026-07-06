using System;
using System.Collections.Generic;

namespace FruitAccounting.Data.Entities;

public partial class PurchaseBillItem
{
    public long PurchaseBillItemId { get; set; }

    public long PurchaseBillId { get; set; }

    public long LotId { get; set; }

    public long ItemId { get; set; }

    public string? Description { get; set; }

    public decimal Quantity { get; set; }

    public decimal? Weight { get; set; }

    public decimal GrossRate { get; set; }

    public decimal NetRate { get; set; }

    public decimal Amount { get; set; }

    public string? CrateInfo { get; set; }

    public virtual Item Item { get; set; } = null!;

    public virtual Lot Lot { get; set; } = null!;

    public virtual PurchaseBill PurchaseBill { get; set; } = null!;
}
