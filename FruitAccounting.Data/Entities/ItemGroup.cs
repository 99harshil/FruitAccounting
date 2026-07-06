using System;
using System.Collections.Generic;

namespace FruitAccounting.Data.Entities;

public partial class ItemGroup
{
    public long ItemGroupId { get; set; }

    public long CompanyId { get; set; }

    public string Name { get; set; } = null!;

    public virtual Company Company { get; set; } = null!;

    public virtual ICollection<ItemCount> ItemCounts { get; set; } = new List<ItemCount>();

    public virtual ICollection<Item> Items { get; set; } = new List<Item>();
}
