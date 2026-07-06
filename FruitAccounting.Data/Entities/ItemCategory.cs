using System;
using System.Collections.Generic;

namespace FruitAccounting.Data.Entities;

public partial class ItemCategory
{
    public long ItemCategoryId { get; set; }

    public long CompanyId { get; set; }

    public string Name { get; set; } = null!;

    public virtual Company Company { get; set; } = null!;

    public virtual ICollection<Item> Items { get; set; } = new List<Item>();
}
