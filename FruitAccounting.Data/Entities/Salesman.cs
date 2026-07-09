using System;
using System.Collections.Generic;

namespace FruitAccounting.Data.Entities;

public partial class Salesman
{
    public long SalesmanId { get; set; }

    public long CompanyId { get; set; }

    public string Name { get; set; } = null!;

    public string? Mobile { get; set; }

    public decimal? CommissionPct { get; set; }

    public bool IsActive { get; set; } = true;

    public virtual ICollection<Account> Accounts { get; set; } = new List<Account>();

    public virtual Company Company { get; set; } = null!;
}
