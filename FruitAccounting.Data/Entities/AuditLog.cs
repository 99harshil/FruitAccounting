using System;
using System.Collections.Generic;

namespace FruitAccounting.Data.Entities;

public partial class AuditLog
{
    public long AuditLogId { get; set; }

    public long? UserId { get; set; }

    public long? CompanyId { get; set; }

    public string TableName { get; set; } = null!;

    public long? RecordPk { get; set; }

    public string Action { get; set; } = null!;

    public string? Detail { get; set; }

    public DateTime OccurredAt { get; set; }

    public virtual Company? Company { get; set; }

    public virtual User? User { get; set; }
}
