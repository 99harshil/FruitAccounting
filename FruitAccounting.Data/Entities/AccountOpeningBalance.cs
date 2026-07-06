using System;
using System.Collections.Generic;
using FruitAccounting.Data.Enums;

namespace FruitAccounting.Data.Entities;

public partial class AccountOpeningBalance
{
    public long AccountId { get; set; }

    public long FinancialYearId { get; set; }

    public decimal Amount { get; set; }

    public DrCr Side { get; set; }

    public virtual Account Account { get; set; } = null!;

    public virtual FinancialYear FinancialYear { get; set; } = null!;
}
