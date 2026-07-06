using System;
using System.Collections.Generic;

namespace FruitAccounting.Data.Entities;

public partial class LoginLog
{
    public long LoginLogId { get; set; }

    public long? UserId { get; set; }

    public string? UsernameTried { get; set; }

    public bool Success { get; set; }

    public string Source { get; set; } = null!;

    public DateTime LoggedInAt { get; set; }

    public DateTime? LoggedOutAt { get; set; }

    public virtual User? User { get; set; }
}
