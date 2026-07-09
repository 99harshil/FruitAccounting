using System;
using System.Collections.Generic;
using FruitAccounting.Data.Enums;

namespace FruitAccounting.Data.Entities;

public partial class User
{
    public long UserId { get; set; }

    public string Username { get; set; } = null!;

    public string? DisplayName { get; set; }

    public string PasswordHash { get; set; } = null!;

    public UserRole Role { get; set; }

    public bool IsActive { get; set; } = true;

    public DateTime CreatedAt { get; set; }

    public virtual ICollection<Attachment> Attachments { get; set; } = new List<Attachment>();

    public virtual ICollection<AuditLog> AuditLogs { get; set; } = new List<AuditLog>();

    public virtual ICollection<BankEntry> BankEntries { get; set; } = new List<BankEntry>();

    public virtual ICollection<BankStatementLine> BankStatementLines { get; set; } = new List<BankStatementLine>();

    public virtual ICollection<ColdStorageTransaction> ColdStorageTransactions { get; set; } = new List<ColdStorageTransaction>();

    public virtual ICollection<CrateTransaction> CrateTransactions { get; set; } = new List<CrateTransaction>();

    public virtual ICollection<DataLock> DataLocks { get; set; } = new List<DataLock>();

    public virtual ICollection<JournalVoucher> JournalVouchers { get; set; } = new List<JournalVoucher>();

    public virtual ICollection<LoginLog> LoginLogs { get; set; } = new List<LoginLog>();

    public virtual ICollection<LotOperation> LotOperations { get; set; } = new List<LotOperation>();

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

    public virtual ICollection<PurchaseBill> PurchaseBills { get; set; } = new List<PurchaseBill>();

    public virtual ICollection<Receipt> Receipts { get; set; } = new List<Receipt>();

    public virtual ICollection<Sale> Sales { get; set; } = new List<Sale>();

    public virtual ICollection<SalesBill> SalesBills { get; set; } = new List<SalesBill>();

    public virtual ICollection<UserPermission> UserPermissions { get; set; } = new List<UserPermission>();

    public virtual ICollection<UserPreference> UserPreferences { get; set; } = new List<UserPreference>();

    public virtual ICollection<WhatsappDispatch> WhatsappDispatches { get; set; } = new List<WhatsappDispatch>();

    public virtual ICollection<Company> Companies { get; set; } = new List<Company>();
}
