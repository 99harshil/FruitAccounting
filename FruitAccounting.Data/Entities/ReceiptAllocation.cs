using System;
using System.Collections.Generic;

namespace FruitAccounting.Data.Entities;

public partial class ReceiptAllocation
{
    public long ReceiptAllocationId { get; set; }

    public long ReceiptId { get; set; }

    public long SalesBillId { get; set; }

    public decimal AllocatedAmount { get; set; }

    public decimal VatavAmount { get; set; }

    public bool FlaggedByAi { get; set; }

    public virtual Receipt Receipt { get; set; } = null!;

    public virtual SalesBill SalesBill { get; set; } = null!;
}
