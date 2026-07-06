using System;
using System.Collections.Generic;

namespace FruitAccounting.Data.Entities;

public partial class UserPermission
{
    public long UserId { get; set; }

    public string MenuKey { get; set; } = null!;

    public bool CanView { get; set; }

    public bool CanAdd { get; set; }

    public bool CanModify { get; set; }

    public bool CanDelete { get; set; }

    public virtual User User { get; set; } = null!;
}
