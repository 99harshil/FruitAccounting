using System;
using System.Collections.Generic;
using FruitAccounting.Data.Enums;

namespace FruitAccounting.Data.Entities;

public partial class WhatsappDispatch
{
    public long DispatchId { get; set; }

    public long AccountId { get; set; }

    public string Mobile { get; set; } = null!;

    public string DocumentType { get; set; } = null!;

    public DateOnly? PeriodFrom { get; set; }

    public DateOnly? PeriodTo { get; set; }

    public string? PdfPath { get; set; }

    public string? WaMessageId { get; set; }

    public DispatchStatus Status { get; set; }

    public DateTime? StatusUpdatedAt { get; set; }

    public string? ErrorDetail { get; set; }

    public long? SentBy { get; set; }

    public DateTime SentAt { get; set; }

    public virtual Account Account { get; set; } = null!;

    public virtual User? SentByNavigation { get; set; }
}
