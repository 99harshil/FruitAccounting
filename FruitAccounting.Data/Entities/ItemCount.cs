using System;
using System.Collections.Generic;

namespace FruitAccounting.Data.Entities;

public partial class ItemCount
{
    public long ItemCountId { get; set; }

    public long CompanyId { get; set; }

    public long? ItemGroupId { get; set; }

    public string Name { get; set; } = null!;

    public virtual Company Company { get; set; } = null!;

    public virtual ICollection<ImportPurchaseItem> ImportPurchaseItems { get; set; } = new List<ImportPurchaseItem>();

    public virtual ItemGroup? ItemGroup { get; set; }
}
