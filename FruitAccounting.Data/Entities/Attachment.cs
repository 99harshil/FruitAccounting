using System;
using System.Collections.Generic;
using FruitAccounting.Data.Enums;

namespace FruitAccounting.Data.Entities;

public partial class Attachment
{
    public long AttachmentId { get; set; }

    public long VoucherId { get; set; }

    public VoucherType VoucherType { get; set; }

    public string FileName { get; set; } = null!;

    public string FilePath { get; set; } = null!;

    public long? UploadedBy { get; set; }

    public DateTime UploadedAt { get; set; }

    public virtual User? UploadedByNavigation { get; set; }
}
