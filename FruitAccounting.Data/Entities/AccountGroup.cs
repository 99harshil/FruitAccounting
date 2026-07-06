using System;
using System.Collections.Generic;

namespace FruitAccounting.Data.Entities;

public partial class AccountGroup
{
    public long AccountGroupId { get; set; }

    public long CompanyId { get; set; }

    public long? ParentId { get; set; }

    public string? Code { get; set; }

    public string Name { get; set; } = null!;

    public string Nature { get; set; } = null!;

    public bool IsSystem { get; set; }

    public virtual ICollection<Account> Accounts { get; set; } = new List<Account>();

    public virtual Company Company { get; set; } = null!;

    public virtual ICollection<AccountGroup> InverseParent { get; set; } = new List<AccountGroup>();

    public virtual AccountGroup? Parent { get; set; }
}
