using System;
using System.Collections.Generic;

namespace FruitAccounting.Data.Entities;

public partial class SystemParameter
{
    public string ParameterKey { get; set; } = null!;

    public string ParameterValue { get; set; } = null!;

    public string? Description { get; set; }

    public DateOnly? EffectiveFrom { get; set; }

    public DateTime UpdatedAt { get; set; }
}
