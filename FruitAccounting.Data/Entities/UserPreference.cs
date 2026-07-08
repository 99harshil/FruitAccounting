using System;

namespace FruitAccounting.Data.Entities;

public partial class UserPreference
{
    public long UserPreferenceId { get; set; }

    public long UserId { get; set; }

    public string PreferenceKey { get; set; } = null!;

    public string PreferenceValue { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public virtual User User { get; set; } = null!;
}
