using System;
using System.Collections.Generic;

namespace FruitAccounting.Data.Entities;

public partial class PaymentAllocation
{
    public long PaymentAllocationId { get; set; }

    public long PaymentId { get; set; }

    public long PurchaseBillId { get; set; }

    public decimal AllocatedAmount { get; set; }

    public virtual Payment Payment { get; set; } = null!;

    public virtual PurchaseBill PurchaseBill { get; set; } = null!;
}
