using System;
using System.Collections.Generic;
using FruitAccounting.Data.Enums;

namespace FruitAccounting.Data.Entities;

public partial class DataLock
{
    public long DataLockId { get; set; }

    public long FinancialYearId { get; set; }

    public VoucherType VoucherType { get; set; }

    public DateOnly LockedUpto { get; set; }

    public long? SetBy { get; set; }

    public DateTime SetAt { get; set; }

    public virtual FinancialYear FinancialYear { get; set; } = null!;

    public virtual User? SetByNavigation { get; set; }
}
