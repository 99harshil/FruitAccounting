using System;
using System.Collections.Generic;
using FruitAccounting.Data.Enums;

namespace FruitAccounting.Data.Entities;

public partial class LotOperation
{
    public long LotOperationId { get; set; }

    public long FinancialYearId { get; set; }

    public DateOnly OpDate { get; set; }

    public LotOpType OpType { get; set; }

    public long SourceLotId { get; set; }

    public long? TargetLotId { get; set; }

    public long? TargetAccountId { get; set; }

    public decimal? Quantity { get; set; }

    public string? Remarks { get; set; }

    public long? CreatedBy { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual User? CreatedByNavigation { get; set; }

    public virtual FinancialYear FinancialYear { get; set; } = null!;

    public virtual Lot SourceLot { get; set; } = null!;

    public virtual Account? TargetAccount { get; set; }

    public virtual Lot? TargetLot { get; set; }
}
