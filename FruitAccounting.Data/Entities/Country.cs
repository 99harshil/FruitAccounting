using System;
using System.Collections.Generic;

namespace FruitAccounting.Data.Entities;

public partial class Country
{
    public long CountryId { get; set; }

    public long CompanyId { get; set; }

    public string Name { get; set; } = null!;

    public virtual Company Company { get; set; } = null!;
}
