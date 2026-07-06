using System;
using System.Collections.Generic;

namespace FruitAccounting.Data.Entities;

public partial class Region
{
    public long RegionId { get; set; }

    public long CompanyId { get; set; }

    public string Code { get; set; } = null!;

    public string Name { get; set; } = null!;

    public virtual ICollection<Account> Accounts { get; set; } = new List<Account>();

    public virtual Company Company { get; set; } = null!;
}
