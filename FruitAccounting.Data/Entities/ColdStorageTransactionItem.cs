using System;
using System.Collections.Generic;

namespace FruitAccounting.Data.Entities;

public partial class ColdStorageTransactionItem
{
    public long ColdTxnItemId { get; set; }

    public long ColdTxnId { get; set; }

    public long? LotId { get; set; }

    public string? ColdLotNo { get; set; }

    public long ItemId { get; set; }

    public decimal Quantity { get; set; }

    public virtual ColdStorageTransaction ColdTxn { get; set; } = null!;

    public virtual Item Item { get; set; } = null!;

    public virtual Lot? Lot { get; set; }
}
