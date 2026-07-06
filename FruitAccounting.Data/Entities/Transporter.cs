using System;
using System.Collections.Generic;

namespace FruitAccounting.Data.Entities;

public partial class Transporter
{
    public long TransporterId { get; set; }

    public long CompanyId { get; set; }

    public string Name { get; set; } = null!;

    public string? City { get; set; }

    public string? Mobile { get; set; }

    public virtual Company Company { get; set; } = null!;
}
