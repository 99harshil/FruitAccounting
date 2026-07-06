using System;
using System.Collections.Generic;

namespace FruitAccounting.Data.Entities;

public partial class FeatureToggle
{
    public string FeatureKey { get; set; } = null!;

    public bool IsEnabled { get; set; }

    public string? Description { get; set; }

    public DateTime UpdatedAt { get; set; }
}
