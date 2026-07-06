using System;
using System.Collections.Generic;

namespace FruitAccounting.Data.Entities;

public partial class ImportPurchaseItem
{
    public long ImportPurchaseItemId { get; set; }

    public long ImportPurchaseId { get; set; }

    public long? LotId { get; set; }

    public long ItemId { get; set; }

    public long? CountId { get; set; }

    public decimal Quantity { get; set; }

    public decimal? RateUsd { get; set; }

    public decimal? LandedRate { get; set; }

    public decimal Amount { get; set; }

    public virtual ItemCount? Count { get; set; }

    public virtual ImportPurchase ImportPurchase { get; set; } = null!;

    public virtual Item Item { get; set; } = null!;

    public virtual Lot? Lot { get; set; }
}
