using System;
using System.Collections.Generic;
using FruitAccounting.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace FruitAccounting.Data.Context;

public partial class FruitAccountingContext : DbContext
{
    public FruitAccountingContext(DbContextOptions<FruitAccountingContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Account> Accounts { get; set; }

    public virtual DbSet<AccountGroup> AccountGroups { get; set; }

    public virtual DbSet<AccountOpeningBalance> AccountOpeningBalances { get; set; }

    public virtual DbSet<Attachment> Attachments { get; set; }

    public virtual DbSet<AuditLog> AuditLogs { get; set; }

    public virtual DbSet<BankEntry> BankEntries { get; set; }

    public virtual DbSet<BankStatementLine> BankStatementLines { get; set; }

    public virtual DbSet<ColdStorageTransaction> ColdStorageTransactions { get; set; }

    public virtual DbSet<ColdStorageTransactionItem> ColdStorageTransactionItems { get; set; }

    public virtual DbSet<Company> Companies { get; set; }

    public virtual DbSet<CrateTransaction> CrateTransactions { get; set; }

    public virtual DbSet<CrateTransactionItem> CrateTransactionItems { get; set; }

    public virtual DbSet<DataLock> DataLocks { get; set; }

    public virtual DbSet<Daybook> Daybooks { get; set; }

    public virtual DbSet<DesavarPurchase> DesavarPurchases { get; set; }

    public virtual DbSet<DesavarSale> DesavarSales { get; set; }

    public virtual DbSet<FeatureToggle> FeatureToggles { get; set; }

    public virtual DbSet<FinancialYear> FinancialYears { get; set; }

    public virtual DbSet<ImportPurchase> ImportPurchases { get; set; }

    public virtual DbSet<ImportPurchaseItem> ImportPurchaseItems { get; set; }

    public virtual DbSet<ImportSale> ImportSales { get; set; }

    public virtual DbSet<Item> Items { get; set; }

    public virtual DbSet<ItemCategory> ItemCategories { get; set; }

    public virtual DbSet<ItemCount> ItemCounts { get; set; }

    public virtual DbSet<ItemGroup> ItemGroups { get; set; }

    public virtual DbSet<JournalVoucher> JournalVouchers { get; set; }

    public virtual DbSet<JournalVoucherLine> JournalVoucherLines { get; set; }

    public virtual DbSet<LedgerEntry> LedgerEntries { get; set; }

    public virtual DbSet<LoginLog> LoginLogs { get; set; }

    public virtual DbSet<Lot> Lots { get; set; }

    public virtual DbSet<LotOperation> LotOperations { get; set; }

    public virtual DbSet<Payment> Payments { get; set; }

    public virtual DbSet<PaymentAllocation> PaymentAllocations { get; set; }

    public virtual DbSet<PurchaseBill> PurchaseBills { get; set; }

    public virtual DbSet<PurchaseBillItem> PurchaseBillItems { get; set; }

    public virtual DbSet<Receipt> Receipts { get; set; }

    public virtual DbSet<ReceiptAllocation> ReceiptAllocations { get; set; }

    public virtual DbSet<Region> Regions { get; set; }

    public virtual DbSet<Sale> Sales { get; set; }

    public virtual DbSet<SalesBill> SalesBills { get; set; }

    public virtual DbSet<Salesman> Salesmen { get; set; }

    public virtual DbSet<SystemParameter> SystemParameters { get; set; }

    public virtual DbSet<TdsPurchaseDeduction> TdsPurchaseDeductions { get; set; }

    public virtual DbSet<Transporter> Transporters { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserPreference> UserPreferences { get; set; }

    public virtual DbSet<UserPermission> UserPermissions { get; set; }

    public virtual DbSet<WhatsappDispatch> WhatsappDispatches { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .HasPostgresEnum("cold_txn_type", new[] { "inward", "outward" })
            .HasPostgresEnum("crate_txn_type", new[] { "issue", "return", "amount_conversion" })
            .HasPostgresEnum("dispatch_status", new[] { "queued", "sent", "delivered", "read", "failed" })
            .HasPostgresEnum("dr_cr", new[] { "debit", "credit" })
            .HasPostgresEnum("lot_op_type", new[] { "split", "merge", "transfer" })
            .HasPostgresEnum("payment_mode", new[] { "cash", "bank", "cheque" })
            .HasPostgresEnum("purchase_mode", new[] { "with_commission", "trading", "without_commission" })
            .HasPostgresEnum("user_role", new[] { "admin", "operator", "readonly" })
            .HasPostgresEnum("voucher_type", new[] { "purchase_bill", "sales_bill", "receipt", "payment", "journal", "bank_entry", "crate", "cold_storage", "desavar_purchase", "desavar_sale", "import_purchase", "import_sale", "opening_balance" });

        modelBuilder.Entity<Account>(entity =>
        {
            entity.HasKey(e => e.AccountId).HasName("accounts_pkey");

            entity.ToTable("accounts");

            entity.HasIndex(e => new { e.CompanyId, e.Code }, "accounts_company_id_code_key").IsUnique();

            entity.HasIndex(e => e.AccountGroupId, "idx_accounts_group");

            entity.HasIndex(e => new { e.CompanyId, e.Name }, "idx_accounts_name");

            entity.HasIndex(e => e.RegionId, "idx_accounts_region");

            entity.Property(e => e.AccountId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("account_id");
            entity.Property(e => e.AccountGroupId).HasColumnName("account_group_id");
            entity.Property(e => e.Address1)
                .HasMaxLength(100)
                .HasColumnName("address1");
            entity.Property(e => e.Address2)
                .HasMaxLength(100)
                .HasColumnName("address2");
            entity.Property(e => e.BankAccountNo)
                .HasMaxLength(30)
                .HasColumnName("bank_account_no");
            entity.Property(e => e.BankBranch)
                .HasMaxLength(60)
                .HasColumnName("bank_branch");
            entity.Property(e => e.BankIfsc)
                .HasMaxLength(15)
                .HasColumnName("bank_ifsc");
            entity.Property(e => e.BankName)
                .HasMaxLength(60)
                .HasColumnName("bank_name");
            entity.Property(e => e.City)
                .HasMaxLength(50)
                .HasColumnName("city");
            entity.Property(e => e.Code)
                .HasMaxLength(10)
                .HasColumnName("code");
            entity.Property(e => e.CommissionPct)
                .HasPrecision(6, 3)
                .HasDefaultValueSql("0")
                .HasColumnName("commission_pct");
            entity.Property(e => e.CompanyId).HasColumnName("company_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("created_at");
            entity.Property(e => e.CreditDays).HasColumnName("credit_days");
            entity.Property(e => e.CreditLimit)
                .HasPrecision(14, 2)
                .HasColumnName("credit_limit");
            entity.Property(e => e.DefaultPurchaseMode).HasColumnName("default_purchase_mode");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("email");
            entity.Property(e => e.InterestPct)
                .HasPrecision(6, 3)
                .HasDefaultValueSql("0")
                .HasColumnName("interest_pct");
            entity.Property(e => e.IsApmc)
                .HasDefaultValue(false)
                .HasColumnName("is_apmc");
            entity.Property(e => e.IsBlocked)
                .HasDefaultValue(false)
                .HasColumnName("is_blocked");
            entity.Property(e => e.MergedInto).HasColumnName("merged_into");
            entity.Property(e => e.Mobile)
                .HasMaxLength(15)
                .HasColumnName("mobile");
            entity.Property(e => e.Name)
                .HasMaxLength(80)
                .HasColumnName("name");
            entity.Property(e => e.PanNo)
                .HasMaxLength(15)
                .HasColumnName("pan_no");
            entity.Property(e => e.Phone)
                .HasMaxLength(20)
                .HasColumnName("phone");
            entity.Property(e => e.PinCode)
                .HasMaxLength(10)
                .HasColumnName("pin_code");
            entity.Property(e => e.RegionId).HasColumnName("region_id");
            entity.Property(e => e.Remarks)
                .HasMaxLength(200)
                .HasColumnName("remarks");
            entity.Property(e => e.SalesmanId).HasColumnName("salesman_id");
            entity.Property(e => e.TdsPct)
                .HasPrecision(6, 3)
                .HasColumnName("tds_pct");
            entity.Property(e => e.TdsThreshold)
                .HasPrecision(14, 2)
                .HasColumnName("tds_threshold");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("updated_at");
            entity.Property(e => e.VatavPct)
                .HasPrecision(6, 3)
                .HasDefaultValueSql("0")
                .HasColumnName("vatav_pct");

            entity.HasOne(d => d.AccountGroup).WithMany(p => p.Accounts)
                .HasForeignKey(d => d.AccountGroupId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("accounts_account_group_id_fkey");

            entity.HasOne(d => d.Company).WithMany(p => p.Accounts)
                .HasForeignKey(d => d.CompanyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("accounts_company_id_fkey");

            entity.HasOne(d => d.MergedIntoNavigation).WithMany(p => p.InverseMergedIntoNavigation)
                .HasForeignKey(d => d.MergedInto)
                .HasConstraintName("accounts_merged_into_fkey");

            entity.HasOne(d => d.Region).WithMany(p => p.Accounts)
                .HasForeignKey(d => d.RegionId)
                .HasConstraintName("accounts_region_id_fkey");

            entity.HasOne(d => d.Salesman).WithMany(p => p.Accounts)
                .HasForeignKey(d => d.SalesmanId)
                .HasConstraintName("accounts_salesman_id_fkey");
        });

        modelBuilder.Entity<AccountGroup>(entity =>
        {
            entity.HasKey(e => e.AccountGroupId).HasName("account_groups_pkey");

            entity.ToTable("account_groups");

            entity.HasIndex(e => new { e.CompanyId, e.Name }, "account_groups_company_id_name_key").IsUnique();

            entity.Property(e => e.AccountGroupId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("account_group_id");
            entity.Property(e => e.Code)
                .HasMaxLength(10)
                .HasColumnName("code");
            entity.Property(e => e.CompanyId).HasColumnName("company_id");
            entity.Property(e => e.IsSystem)
                .HasDefaultValue(false)
                .HasColumnName("is_system");
            entity.Property(e => e.Name)
                .HasMaxLength(60)
                .HasColumnName("name");
            entity.Property(e => e.Nature)
                .HasMaxLength(20)
                .HasColumnName("nature");
            entity.Property(e => e.ParentId).HasColumnName("parent_id");

            entity.HasOne(d => d.Company).WithMany(p => p.AccountGroups)
                .HasForeignKey(d => d.CompanyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("account_groups_company_id_fkey");

            entity.HasOne(d => d.Parent).WithMany(p => p.InverseParent)
                .HasForeignKey(d => d.ParentId)
                .HasConstraintName("account_groups_parent_id_fkey");
        });

        modelBuilder.Entity<AccountOpeningBalance>(entity =>
        {
            entity.HasKey(e => new { e.AccountId, e.FinancialYearId }).HasName("account_opening_balances_pkey");

            entity.ToTable("account_opening_balances");

            entity.Property(e => e.AccountId).HasColumnName("account_id");
            entity.Property(e => e.FinancialYearId).HasColumnName("financial_year_id");
            entity.Property(e => e.Amount)
                .HasPrecision(14, 2)
                .HasColumnName("amount");
            entity.Property(e => e.Side).HasColumnName("side");

            entity.HasOne(d => d.Account).WithMany(p => p.AccountOpeningBalances)
                .HasForeignKey(d => d.AccountId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("account_opening_balances_account_id_fkey");

            entity.HasOne(d => d.FinancialYear).WithMany(p => p.AccountOpeningBalances)
                .HasForeignKey(d => d.FinancialYearId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("account_opening_balances_financial_year_id_fkey");
        });

        modelBuilder.Entity<Attachment>(entity =>
        {
            entity.HasKey(e => e.AttachmentId).HasName("attachments_pkey");

            entity.ToTable("attachments");

            entity.Property(e => e.AttachmentId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("attachment_id");
            entity.Property(e => e.FileName)
                .HasMaxLength(120)
                .HasColumnName("file_name");
            entity.Property(e => e.FilePath)
                .HasMaxLength(255)
                .HasColumnName("file_path");
            entity.Property(e => e.UploadedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("uploaded_at");
            entity.Property(e => e.UploadedBy).HasColumnName("uploaded_by");
            entity.Property(e => e.VoucherId).HasColumnName("voucher_id");
            entity.Property(e => e.VoucherType).HasColumnName("voucher_type");

            entity.HasOne(d => d.UploadedByNavigation).WithMany(p => p.Attachments)
                .HasForeignKey(d => d.UploadedBy)
                .HasConstraintName("attachments_uploaded_by_fkey");
        });

        modelBuilder.Entity<AuditLog>(entity =>
        {
            entity.HasKey(e => e.AuditLogId).HasName("audit_log_pkey");

            entity.ToTable("audit_log");

            entity.HasIndex(e => e.OccurredAt, "idx_audit_log_occurred");

            entity.HasIndex(e => new { e.TableName, e.RecordPk }, "idx_audit_log_table_record");

            entity.Property(e => e.AuditLogId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("audit_log_id");
            entity.Property(e => e.Action)
                .HasMaxLength(10)
                .HasColumnName("action");
            entity.Property(e => e.CompanyId).HasColumnName("company_id");
            entity.Property(e => e.Detail)
                .HasColumnType("jsonb")
                .HasColumnName("detail");
            entity.Property(e => e.OccurredAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("occurred_at");
            entity.Property(e => e.RecordPk).HasColumnName("record_pk");
            entity.Property(e => e.TableName)
                .HasMaxLength(60)
                .HasColumnName("table_name");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Company).WithMany(p => p.AuditLogs)
                .HasForeignKey(d => d.CompanyId)
                .HasConstraintName("audit_log_company_id_fkey");

            entity.HasOne(d => d.User).WithMany(p => p.AuditLogs)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("audit_log_user_id_fkey");
        });

        modelBuilder.Entity<BankEntry>(entity =>
        {
            entity.HasKey(e => e.BankEntryId).HasName("bank_entries_pkey");

            entity.ToTable("bank_entries");

            entity.HasIndex(e => new { e.FinancialYearId, e.EntryNo }, "bank_entries_financial_year_id_entry_no_key").IsUnique();

            entity.Property(e => e.BankEntryId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("bank_entry_id");
            entity.Property(e => e.AccountId).HasColumnName("account_id");
            entity.Property(e => e.Amount)
                .HasPrecision(14, 2)
                .HasColumnName("amount");
            entity.Property(e => e.ChequeNo)
                .HasMaxLength(20)
                .HasColumnName("cheque_no");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.DaybookId).HasColumnName("daybook_id");
            entity.Property(e => e.EntryDate).HasColumnName("entry_date");
            entity.Property(e => e.EntryNo).HasColumnName("entry_no");
            entity.Property(e => e.FinancialYearId).HasColumnName("financial_year_id");
            entity.Property(e => e.Remarks)
                .HasMaxLength(200)
                .HasColumnName("remarks");
            entity.Property(e => e.Side).HasColumnName("side");

            entity.HasOne(d => d.Account).WithMany(p => p.BankEntries)
                .HasForeignKey(d => d.AccountId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("bank_entries_account_id_fkey");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.BankEntries)
                .HasForeignKey(d => d.CreatedBy)
                .HasConstraintName("bank_entries_created_by_fkey");

            entity.HasOne(d => d.Daybook).WithMany(p => p.BankEntries)
                .HasForeignKey(d => d.DaybookId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("bank_entries_daybook_id_fkey");

            entity.HasOne(d => d.FinancialYear).WithMany(p => p.BankEntries)
                .HasForeignKey(d => d.FinancialYearId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("bank_entries_financial_year_id_fkey");
        });

        modelBuilder.Entity<BankStatementLine>(entity =>
        {
            entity.HasKey(e => e.StatementLineId).HasName("bank_statement_lines_pkey");

            entity.ToTable("bank_statement_lines");

            entity.HasIndex(e => new { e.DaybookId, e.StatementDate }, "idx_stmt_daybook_date");

            entity.Property(e => e.StatementLineId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("statement_line_id");
            entity.Property(e => e.Credit)
                .HasPrecision(14, 2)
                .HasColumnName("credit");
            entity.Property(e => e.DaybookId).HasColumnName("daybook_id");
            entity.Property(e => e.Debit)
                .HasPrecision(14, 2)
                .HasColumnName("debit");
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .HasColumnName("description");
            entity.Property(e => e.ImportedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("imported_at");
            entity.Property(e => e.MatchedLedgerEntryId).HasColumnName("matched_ledger_entry_id");
            entity.Property(e => e.Reconciled)
                .HasDefaultValue(false)
                .HasColumnName("reconciled");
            entity.Property(e => e.ReconciledAt).HasColumnName("reconciled_at");
            entity.Property(e => e.ReconciledBy).HasColumnName("reconciled_by");
            entity.Property(e => e.Reference)
                .HasMaxLength(50)
                .HasColumnName("reference");
            entity.Property(e => e.StatementDate).HasColumnName("statement_date");

            entity.HasOne(d => d.Daybook).WithMany(p => p.BankStatementLines)
                .HasForeignKey(d => d.DaybookId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("bank_statement_lines_daybook_id_fkey");

            entity.HasOne(d => d.MatchedLedgerEntry).WithMany(p => p.BankStatementLines)
                .HasForeignKey(d => d.MatchedLedgerEntryId)
                .HasConstraintName("bank_statement_lines_matched_ledger_entry_id_fkey");

            entity.HasOne(d => d.ReconciledByNavigation).WithMany(p => p.BankStatementLines)
                .HasForeignKey(d => d.ReconciledBy)
                .HasConstraintName("bank_statement_lines_reconciled_by_fkey");
        });

        modelBuilder.Entity<ColdStorageTransaction>(entity =>
        {
            entity.HasKey(e => e.ColdTxnId).HasName("cold_storage_transactions_pkey");

            entity.ToTable("cold_storage_transactions");

            entity.HasIndex(e => new { e.FinancialYearId, e.TxnNo }, "cold_storage_transactions_financial_year_id_txn_no_key").IsUnique();

            entity.Property(e => e.ColdTxnId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("cold_txn_id");
            entity.Property(e => e.ColdStoreId).HasColumnName("cold_store_id");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.FinancialYearId).HasColumnName("financial_year_id");
            entity.Property(e => e.PartyId).HasColumnName("party_id");
            entity.Property(e => e.Remarks)
                .HasMaxLength(200)
                .HasColumnName("remarks");
            entity.Property(e => e.TxnDate).HasColumnName("txn_date");
            entity.Property(e => e.TxnNo).HasColumnName("txn_no");
            entity.Property(e => e.TxnType).HasColumnName("txn_type");

            entity.HasOne(d => d.ColdStore).WithMany(p => p.ColdStorageTransactionColdStores)
                .HasForeignKey(d => d.ColdStoreId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("cold_storage_transactions_cold_store_id_fkey");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.ColdStorageTransactions)
                .HasForeignKey(d => d.CreatedBy)
                .HasConstraintName("cold_storage_transactions_created_by_fkey");

            entity.HasOne(d => d.FinancialYear).WithMany(p => p.ColdStorageTransactions)
                .HasForeignKey(d => d.FinancialYearId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("cold_storage_transactions_financial_year_id_fkey");

            entity.HasOne(d => d.Party).WithMany(p => p.ColdStorageTransactionParties)
                .HasForeignKey(d => d.PartyId)
                .HasConstraintName("cold_storage_transactions_party_id_fkey");
        });

        modelBuilder.Entity<ColdStorageTransactionItem>(entity =>
        {
            entity.HasKey(e => e.ColdTxnItemId).HasName("cold_storage_transaction_items_pkey");

            entity.ToTable("cold_storage_transaction_items");

            entity.Property(e => e.ColdTxnItemId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("cold_txn_item_id");
            entity.Property(e => e.ColdLotNo)
                .HasMaxLength(20)
                .HasColumnName("cold_lot_no");
            entity.Property(e => e.ColdTxnId).HasColumnName("cold_txn_id");
            entity.Property(e => e.ItemId).HasColumnName("item_id");
            entity.Property(e => e.LotId).HasColumnName("lot_id");
            entity.Property(e => e.Quantity)
                .HasPrecision(12, 3)
                .HasColumnName("quantity");

            entity.HasOne(d => d.ColdTxn).WithMany(p => p.ColdStorageTransactionItems)
                .HasForeignKey(d => d.ColdTxnId)
                .HasConstraintName("cold_storage_transaction_items_cold_txn_id_fkey");

            entity.HasOne(d => d.Item).WithMany(p => p.ColdStorageTransactionItems)
                .HasForeignKey(d => d.ItemId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("cold_storage_transaction_items_item_id_fkey");

            entity.HasOne(d => d.Lot).WithMany(p => p.ColdStorageTransactionItems)
                .HasForeignKey(d => d.LotId)
                .HasConstraintName("cold_storage_transaction_items_lot_id_fkey");
        });

        modelBuilder.Entity<Company>(entity =>
        {
            entity.HasKey(e => e.CompanyId).HasName("companies_pkey");

            entity.ToTable("companies");

            entity.HasIndex(e => e.Code, "companies_code_key").IsUnique();

            entity.Property(e => e.CompanyId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("company_id");
            entity.Property(e => e.Address1)
                .HasMaxLength(100)
                .HasColumnName("address1");
            entity.Property(e => e.Address2)
                .HasMaxLength(100)
                .HasColumnName("address2");
            entity.Property(e => e.ApmcLicenceNo)
                .HasMaxLength(30)
                .HasColumnName("apmc_licence_no");
            entity.Property(e => e.BankAccountNo)
                .HasMaxLength(30)
                .HasColumnName("bank_account_no");
            entity.Property(e => e.BankIfsc)
                .HasMaxLength(15)
                .HasColumnName("bank_ifsc");
            entity.Property(e => e.BankName)
                .HasMaxLength(60)
                .HasColumnName("bank_name");
            entity.Property(e => e.City)
                .HasMaxLength(50)
                .HasColumnName("city");
            entity.Property(e => e.Code)
                .HasMaxLength(10)
                .HasColumnName("code");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("created_at");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("email");
            entity.Property(e => e.Gstin)
                .HasMaxLength(20)
                .HasColumnName("gstin");
            entity.Property(e => e.IsActive)
                .HasDefaultValue(true)
                .HasColumnName("is_active");
            entity.Property(e => e.Logo).HasColumnName("logo");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
            entity.Property(e => e.PanNo)
                .HasMaxLength(15)
                .HasColumnName("pan_no");
            entity.Property(e => e.Phone)
                .HasMaxLength(20)
                .HasColumnName("phone");
        });

        modelBuilder.Entity<CrateTransaction>(entity =>
        {
            entity.HasKey(e => e.CrateTxnId).HasName("crate_transactions_pkey");

            entity.ToTable("crate_transactions");

            entity.HasIndex(e => new { e.FinancialYearId, e.TxnNo }, "crate_transactions_financial_year_id_txn_no_key").IsUnique();

            entity.HasIndex(e => new { e.AccountId, e.TxnDate }, "idx_crate_txn_account");

            entity.Property(e => e.CrateTxnId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("crate_txn_id");
            entity.Property(e => e.AccountId).HasColumnName("account_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.FinancialYearId).HasColumnName("financial_year_id");
            entity.Property(e => e.Remarks)
                .HasMaxLength(200)
                .HasColumnName("remarks");
            entity.Property(e => e.TruckNo)
                .HasMaxLength(20)
                .HasColumnName("truck_no");
            entity.Property(e => e.TxnDate).HasColumnName("txn_date");
            entity.Property(e => e.TxnNo).HasColumnName("txn_no");
            entity.Property(e => e.TxnType).HasColumnName("txn_type");

            entity.HasOne(d => d.Account).WithMany(p => p.CrateTransactions)
                .HasForeignKey(d => d.AccountId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("crate_transactions_account_id_fkey");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.CrateTransactions)
                .HasForeignKey(d => d.CreatedBy)
                .HasConstraintName("crate_transactions_created_by_fkey");

            entity.HasOne(d => d.FinancialYear).WithMany(p => p.CrateTransactions)
                .HasForeignKey(d => d.FinancialYearId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("crate_transactions_financial_year_id_fkey");
        });

        modelBuilder.Entity<CrateTransactionItem>(entity =>
        {
            entity.HasKey(e => e.CrateTxnItemId).HasName("crate_transaction_items_pkey");

            entity.ToTable("crate_transaction_items");

            entity.Property(e => e.CrateTxnItemId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("crate_txn_item_id");
            entity.Property(e => e.Amount)
                .HasPrecision(14, 2)
                .HasDefaultValueSql("0")
                .HasColumnName("amount");
            entity.Property(e => e.CrateTxnId).HasColumnName("crate_txn_id");
            entity.Property(e => e.CrateType)
                .HasMaxLength(30)
                .HasColumnName("crate_type");
            entity.Property(e => e.Quantity)
                .HasPrecision(10)
                .HasColumnName("quantity");
            entity.Property(e => e.Rate)
                .HasPrecision(10, 2)
                .HasDefaultValueSql("0")
                .HasColumnName("rate");

            entity.HasOne(d => d.CrateTxn).WithMany(p => p.CrateTransactionItems)
                .HasForeignKey(d => d.CrateTxnId)
                .HasConstraintName("crate_transaction_items_crate_txn_id_fkey");
        });

        modelBuilder.Entity<DataLock>(entity =>
        {
            entity.HasKey(e => e.DataLockId).HasName("data_locks_pkey");

            entity.ToTable("data_locks");

            entity.Property(e => e.DataLockId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("data_lock_id");
            entity.Property(e => e.FinancialYearId).HasColumnName("financial_year_id");
            entity.Property(e => e.LockedUpto).HasColumnName("locked_upto");
            entity.Property(e => e.SetAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("set_at");
            entity.Property(e => e.SetBy).HasColumnName("set_by");
            entity.Property(e => e.VoucherType).HasColumnName("voucher_type");

            entity.HasOne(d => d.FinancialYear).WithMany(p => p.DataLocks)
                .HasForeignKey(d => d.FinancialYearId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("data_locks_financial_year_id_fkey");

            entity.HasOne(d => d.SetByNavigation).WithMany(p => p.DataLocks)
                .HasForeignKey(d => d.SetBy)
                .HasConstraintName("fk_data_locks_user");
        });

        modelBuilder.Entity<Daybook>(entity =>
        {
            entity.HasKey(e => e.DaybookId).HasName("daybooks_pkey");

            entity.ToTable("daybooks");

            entity.HasIndex(e => new { e.CompanyId, e.Name }, "daybooks_company_id_name_key").IsUnique();

            entity.Property(e => e.DaybookId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("daybook_id");
            entity.Property(e => e.BookType)
                .HasMaxLength(1)
                .HasColumnName("book_type");
            entity.Property(e => e.CompanyId).HasColumnName("company_id");
            entity.Property(e => e.LinkedAccountId).HasColumnName("linked_account_id");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");

            entity.HasOne(d => d.Company).WithMany(p => p.Daybooks)
                .HasForeignKey(d => d.CompanyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("daybooks_company_id_fkey");

            entity.HasOne(d => d.LinkedAccount).WithMany(p => p.Daybooks)
                .HasForeignKey(d => d.LinkedAccountId)
                .HasConstraintName("daybooks_linked_account_id_fkey");
        });

        modelBuilder.Entity<DesavarPurchase>(entity =>
        {
            entity.HasKey(e => e.DesavarPurchaseId).HasName("desavar_purchases_pkey");

            entity.ToTable("desavar_purchases");

            entity.HasIndex(e => new { e.FinancialYearId, e.BillNo }, "desavar_purchases_financial_year_id_bill_no_key").IsUnique();

            entity.Property(e => e.DesavarPurchaseId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("desavar_purchase_id");
            entity.Property(e => e.BillDate).HasColumnName("bill_date");
            entity.Property(e => e.BillNo).HasColumnName("bill_no");
            entity.Property(e => e.FinancialYearId).HasColumnName("financial_year_id");
            entity.Property(e => e.Freight)
                .HasPrecision(14, 2)
                .HasDefaultValueSql("0")
                .HasColumnName("freight");
            entity.Property(e => e.ItemId).HasColumnName("item_id");
            entity.Property(e => e.LotId).HasColumnName("lot_id");
            entity.Property(e => e.NetAmount)
                .HasPrecision(14, 2)
                .HasColumnName("net_amount");
            entity.Property(e => e.Origin)
                .HasMaxLength(60)
                .HasColumnName("origin");
            entity.Property(e => e.OtherCharges)
                .HasPrecision(14, 2)
                .HasDefaultValueSql("0")
                .HasColumnName("other_charges");
            entity.Property(e => e.Quantity)
                .HasPrecision(12, 3)
                .HasColumnName("quantity");
            entity.Property(e => e.Rate)
                .HasPrecision(12, 2)
                .HasColumnName("rate");
            entity.Property(e => e.SupplierId).HasColumnName("supplier_id");

            entity.HasOne(d => d.FinancialYear).WithMany(p => p.DesavarPurchases)
                .HasForeignKey(d => d.FinancialYearId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("desavar_purchases_financial_year_id_fkey");

            entity.HasOne(d => d.Item).WithMany(p => p.DesavarPurchases)
                .HasForeignKey(d => d.ItemId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("desavar_purchases_item_id_fkey");

            entity.HasOne(d => d.Lot).WithMany(p => p.DesavarPurchases)
                .HasForeignKey(d => d.LotId)
                .HasConstraintName("desavar_purchases_lot_id_fkey");

            entity.HasOne(d => d.Supplier).WithMany(p => p.DesavarPurchases)
                .HasForeignKey(d => d.SupplierId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("desavar_purchases_supplier_id_fkey");
        });

        modelBuilder.Entity<DesavarSale>(entity =>
        {
            entity.HasKey(e => e.DesavarSaleId).HasName("desavar_sales_pkey");

            entity.ToTable("desavar_sales");

            entity.Property(e => e.DesavarSaleId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("desavar_sale_id");
            entity.Property(e => e.Amount)
                .HasPrecision(14, 2)
                .HasColumnName("amount");
            entity.Property(e => e.BuyerId).HasColumnName("buyer_id");
            entity.Property(e => e.DesavarPurchaseId).HasColumnName("desavar_purchase_id");
            entity.Property(e => e.FinancialYearId).HasColumnName("financial_year_id");
            entity.Property(e => e.ItemId).HasColumnName("item_id");
            entity.Property(e => e.LotId).HasColumnName("lot_id");
            entity.Property(e => e.PurchaseRate)
                .HasPrecision(12, 2)
                .HasColumnName("purchase_rate");
            entity.Property(e => e.Quantity)
                .HasPrecision(12, 3)
                .HasColumnName("quantity");
            entity.Property(e => e.Rate)
                .HasPrecision(12, 2)
                .HasColumnName("rate");
            entity.Property(e => e.SaleDate).HasColumnName("sale_date");
            entity.Property(e => e.Weight)
                .HasPrecision(12, 3)
                .HasColumnName("weight");

            entity.HasOne(d => d.Buyer).WithMany(p => p.DesavarSales)
                .HasForeignKey(d => d.BuyerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("desavar_sales_buyer_id_fkey");

            entity.HasOne(d => d.DesavarPurchase).WithMany(p => p.DesavarSales)
                .HasForeignKey(d => d.DesavarPurchaseId)
                .HasConstraintName("desavar_sales_desavar_purchase_id_fkey");

            entity.HasOne(d => d.FinancialYear).WithMany(p => p.DesavarSales)
                .HasForeignKey(d => d.FinancialYearId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("desavar_sales_financial_year_id_fkey");

            entity.HasOne(d => d.Item).WithMany(p => p.DesavarSales)
                .HasForeignKey(d => d.ItemId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("desavar_sales_item_id_fkey");

            entity.HasOne(d => d.Lot).WithMany(p => p.DesavarSales)
                .HasForeignKey(d => d.LotId)
                .HasConstraintName("desavar_sales_lot_id_fkey");
        });

        modelBuilder.Entity<FeatureToggle>(entity =>
        {
            entity.HasKey(e => e.FeatureKey).HasName("feature_toggles_pkey");

            entity.ToTable("feature_toggles");

            entity.Property(e => e.FeatureKey)
                .HasMaxLength(40)
                .HasColumnName("feature_key");
            entity.Property(e => e.Description)
                .HasMaxLength(200)
                .HasColumnName("description");
            entity.Property(e => e.IsEnabled)
                .HasDefaultValue(false)
                .HasColumnName("is_enabled");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("updated_at");
        });

        modelBuilder.Entity<FinancialYear>(entity =>
        {
            entity.HasKey(e => e.FinancialYearId).HasName("financial_years_pkey");

            entity.ToTable("financial_years");

            entity.HasIndex(e => new { e.CompanyId, e.Code }, "financial_years_company_id_code_key").IsUnique();

            entity.Property(e => e.FinancialYearId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("financial_year_id");
            entity.Property(e => e.Code)
                .HasMaxLength(10)
                .HasColumnName("code");
            entity.Property(e => e.CompanyId).HasColumnName("company_id");
            entity.Property(e => e.EndDate).HasColumnName("end_date");
            entity.Property(e => e.IsActive)
                .HasDefaultValue(true)
                .HasColumnName("is_active");
            entity.Property(e => e.IsClosed)
                .HasDefaultValue(false)
                .HasColumnName("is_closed");
            entity.Property(e => e.StartDate).HasColumnName("start_date");

            entity.HasOne(d => d.Company).WithMany(p => p.FinancialYears)
                .HasForeignKey(d => d.CompanyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("financial_years_company_id_fkey");
        });

        modelBuilder.Entity<ImportPurchase>(entity =>
        {
            entity.HasKey(e => e.ImportPurchaseId).HasName("import_purchases_pkey");

            entity.ToTable("import_purchases");

            entity.HasIndex(e => new { e.FinancialYearId, e.InvoiceNo }, "import_purchases_financial_year_id_invoice_no_key").IsUnique();

            entity.Property(e => e.ImportPurchaseId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("import_purchase_id");
            entity.Property(e => e.BlNo)
                .HasMaxLength(30)
                .HasColumnName("bl_no");
            entity.Property(e => e.CifValue)
                .HasPrecision(14, 2)
                .HasColumnName("cif_value");
            entity.Property(e => e.Clearing)
                .HasPrecision(14, 2)
                .HasDefaultValueSql("0")
                .HasColumnName("clearing");
            entity.Property(e => e.Commission)
                .HasPrecision(14, 2)
                .HasDefaultValueSql("0")
                .HasColumnName("commission");
            entity.Property(e => e.Country)
                .HasMaxLength(50)
                .HasColumnName("country");
            entity.Property(e => e.Duty)
                .HasPrecision(14, 2)
                .HasDefaultValueSql("0")
                .HasColumnName("duty");
            entity.Property(e => e.ExchangeRate)
                .HasPrecision(10, 4)
                .HasColumnName("exchange_rate");
            entity.Property(e => e.FinancialYearId).HasColumnName("financial_year_id");
            entity.Property(e => e.Freight)
                .HasPrecision(14, 2)
                .HasDefaultValueSql("0")
                .HasColumnName("freight");
            entity.Property(e => e.Handling)
                .HasPrecision(14, 2)
                .HasDefaultValueSql("0")
                .HasColumnName("handling");
            entity.Property(e => e.InvoiceDate).HasColumnName("invoice_date");
            entity.Property(e => e.InvoiceNo)
                .HasMaxLength(30)
                .HasColumnName("invoice_no");
            entity.Property(e => e.NetAmount)
                .HasPrecision(14, 2)
                .HasColumnName("net_amount");
            entity.Property(e => e.OtherCharges)
                .HasPrecision(14, 2)
                .HasDefaultValueSql("0")
                .HasColumnName("other_charges");
            entity.Property(e => e.SupplierId).HasColumnName("supplier_id");
            entity.Property(e => e.TotalUsd)
                .HasPrecision(14, 2)
                .HasColumnName("total_usd");

            entity.HasOne(d => d.FinancialYear).WithMany(p => p.ImportPurchases)
                .HasForeignKey(d => d.FinancialYearId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("import_purchases_financial_year_id_fkey");

            entity.HasOne(d => d.Supplier).WithMany(p => p.ImportPurchases)
                .HasForeignKey(d => d.SupplierId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("import_purchases_supplier_id_fkey");
        });

        modelBuilder.Entity<ImportPurchaseItem>(entity =>
        {
            entity.HasKey(e => e.ImportPurchaseItemId).HasName("import_purchase_items_pkey");

            entity.ToTable("import_purchase_items");

            entity.Property(e => e.ImportPurchaseItemId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("import_purchase_item_id");
            entity.Property(e => e.Amount)
                .HasPrecision(14, 2)
                .HasColumnName("amount");
            entity.Property(e => e.CountId).HasColumnName("count_id");
            entity.Property(e => e.ImportPurchaseId).HasColumnName("import_purchase_id");
            entity.Property(e => e.ItemId).HasColumnName("item_id");
            entity.Property(e => e.LandedRate)
                .HasPrecision(12, 2)
                .HasColumnName("landed_rate");
            entity.Property(e => e.LotId).HasColumnName("lot_id");
            entity.Property(e => e.Quantity)
                .HasPrecision(12, 3)
                .HasColumnName("quantity");
            entity.Property(e => e.RateUsd)
                .HasPrecision(12, 2)
                .HasColumnName("rate_usd");

            entity.HasOne(d => d.Count).WithMany(p => p.ImportPurchaseItems)
                .HasForeignKey(d => d.CountId)
                .HasConstraintName("import_purchase_items_count_id_fkey");

            entity.HasOne(d => d.ImportPurchase).WithMany(p => p.ImportPurchaseItems)
                .HasForeignKey(d => d.ImportPurchaseId)
                .HasConstraintName("import_purchase_items_import_purchase_id_fkey");

            entity.HasOne(d => d.Item).WithMany(p => p.ImportPurchaseItems)
                .HasForeignKey(d => d.ItemId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("import_purchase_items_item_id_fkey");

            entity.HasOne(d => d.Lot).WithMany(p => p.ImportPurchaseItems)
                .HasForeignKey(d => d.LotId)
                .HasConstraintName("import_purchase_items_lot_id_fkey");
        });

        modelBuilder.Entity<ImportSale>(entity =>
        {
            entity.HasKey(e => e.ImportSaleId).HasName("import_sales_pkey");

            entity.ToTable("import_sales");

            entity.Property(e => e.ImportSaleId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("import_sale_id");
            entity.Property(e => e.Amount)
                .HasPrecision(14, 2)
                .HasColumnName("amount");
            entity.Property(e => e.BuyerId).HasColumnName("buyer_id");
            entity.Property(e => e.FinancialYearId).HasColumnName("financial_year_id");
            entity.Property(e => e.ItemId).HasColumnName("item_id");
            entity.Property(e => e.LotId).HasColumnName("lot_id");
            entity.Property(e => e.Quantity)
                .HasPrecision(12, 3)
                .HasColumnName("quantity");
            entity.Property(e => e.Rate)
                .HasPrecision(12, 2)
                .HasColumnName("rate");
            entity.Property(e => e.SaleDate).HasColumnName("sale_date");

            entity.HasOne(d => d.Buyer).WithMany(p => p.ImportSales)
                .HasForeignKey(d => d.BuyerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("import_sales_buyer_id_fkey");

            entity.HasOne(d => d.FinancialYear).WithMany(p => p.ImportSales)
                .HasForeignKey(d => d.FinancialYearId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("import_sales_financial_year_id_fkey");

            entity.HasOne(d => d.Item).WithMany(p => p.ImportSales)
                .HasForeignKey(d => d.ItemId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("import_sales_item_id_fkey");

            entity.HasOne(d => d.Lot).WithMany(p => p.ImportSales)
                .HasForeignKey(d => d.LotId)
                .HasConstraintName("import_sales_lot_id_fkey");
        });

        modelBuilder.Entity<Item>(entity =>
        {
            entity.HasKey(e => e.ItemId).HasName("items_pkey");

            entity.ToTable("items");

            entity.HasIndex(e => new { e.CompanyId, e.Name }, "items_company_id_name_key").IsUnique();

            entity.Property(e => e.ItemId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("item_id");
            entity.Property(e => e.Code)
                .HasMaxLength(10)
                .HasColumnName("code");
            entity.Property(e => e.CompanyId).HasColumnName("company_id");
            entity.Property(e => e.IsActive)
                .HasDefaultValue(true)
                .HasColumnName("is_active");
            entity.Property(e => e.ItemCategoryId).HasColumnName("item_category_id");
            entity.Property(e => e.ItemGroupId).HasColumnName("item_group_id");
            entity.Property(e => e.LabourRate)
                .HasPrecision(10, 2)
                .HasDefaultValueSql("0")
                .HasColumnName("labour_rate");
            entity.Property(e => e.Name)
                .HasMaxLength(60)
                .HasColumnName("name");
            entity.Property(e => e.PackingRate)
                .HasPrecision(10, 2)
                .HasDefaultValueSql("0")
                .HasColumnName("packing_rate");
            entity.Property(e => e.Unit)
                .HasMaxLength(10)
                .HasDefaultValueSql("'Box'::character varying")
                .HasColumnName("unit");
            entity.Property(e => e.UsesCrate)
                .HasDefaultValue(false)
                .HasColumnName("uses_crate");

            entity.HasOne(d => d.Company).WithMany(p => p.Items)
                .HasForeignKey(d => d.CompanyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("items_company_id_fkey");

            entity.HasOne(d => d.ItemCategory).WithMany(p => p.Items)
                .HasForeignKey(d => d.ItemCategoryId)
                .HasConstraintName("items_item_category_id_fkey");

            entity.HasOne(d => d.ItemGroup).WithMany(p => p.Items)
                .HasForeignKey(d => d.ItemGroupId)
                .HasConstraintName("items_item_group_id_fkey");
        });

        modelBuilder.Entity<ItemCategory>(entity =>
        {
            entity.HasKey(e => e.ItemCategoryId).HasName("item_categories_pkey");

            entity.ToTable("item_categories");

            entity.HasIndex(e => new { e.CompanyId, e.Name }, "item_categories_company_id_name_key").IsUnique();

            entity.Property(e => e.ItemCategoryId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("item_category_id");
            entity.Property(e => e.CompanyId).HasColumnName("company_id");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");

            entity.HasOne(d => d.Company).WithMany(p => p.ItemCategories)
                .HasForeignKey(d => d.CompanyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("item_categories_company_id_fkey");
        });

        modelBuilder.Entity<ItemCount>(entity =>
        {
            entity.HasKey(e => e.ItemCountId).HasName("item_counts_pkey");

            entity.ToTable("item_counts");

            entity.HasIndex(e => new { e.CompanyId, e.ItemGroupId, e.Name }, "item_counts_company_id_item_group_id_name_key").IsUnique();

            entity.Property(e => e.ItemCountId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("item_count_id");
            entity.Property(e => e.CompanyId).HasColumnName("company_id");
            entity.Property(e => e.ItemGroupId).HasColumnName("item_group_id");
            entity.Property(e => e.Name)
                .HasMaxLength(30)
                .HasColumnName("name");

            entity.HasOne(d => d.Company).WithMany(p => p.ItemCounts)
                .HasForeignKey(d => d.CompanyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("item_counts_company_id_fkey");

            entity.HasOne(d => d.ItemGroup).WithMany(p => p.ItemCounts)
                .HasForeignKey(d => d.ItemGroupId)
                .HasConstraintName("item_counts_item_group_id_fkey");
        });

        modelBuilder.Entity<ItemGroup>(entity =>
        {
            entity.HasKey(e => e.ItemGroupId).HasName("item_groups_pkey");

            entity.ToTable("item_groups");

            entity.HasIndex(e => new { e.CompanyId, e.Name }, "item_groups_company_id_name_key").IsUnique();

            entity.Property(e => e.ItemGroupId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("item_group_id");
            entity.Property(e => e.CompanyId).HasColumnName("company_id");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");

            entity.HasOne(d => d.Company).WithMany(p => p.ItemGroups)
                .HasForeignKey(d => d.CompanyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("item_groups_company_id_fkey");
        });

        modelBuilder.Entity<JournalVoucher>(entity =>
        {
            entity.HasKey(e => e.JournalVoucherId).HasName("journal_vouchers_pkey");

            entity.ToTable("journal_vouchers");

            entity.HasIndex(e => new { e.FinancialYearId, e.VoucherNo }, "journal_vouchers_financial_year_id_voucher_no_key").IsUnique();

            entity.Property(e => e.JournalVoucherId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("journal_voucher_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.FinancialYearId).HasColumnName("financial_year_id");
            entity.Property(e => e.Narration)
                .HasMaxLength(255)
                .HasColumnName("narration");
            entity.Property(e => e.ReferenceNo)
                .HasMaxLength(30)
                .HasColumnName("reference_no");
            entity.Property(e => e.VoucherDate).HasColumnName("voucher_date");
            entity.Property(e => e.VoucherNo).HasColumnName("voucher_no");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.JournalVouchers)
                .HasForeignKey(d => d.CreatedBy)
                .HasConstraintName("journal_vouchers_created_by_fkey");

            entity.HasOne(d => d.FinancialYear).WithMany(p => p.JournalVouchers)
                .HasForeignKey(d => d.FinancialYearId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("journal_vouchers_financial_year_id_fkey");
        });

        modelBuilder.Entity<JournalVoucherLine>(entity =>
        {
            entity.HasKey(e => e.JournalVoucherLineId).HasName("journal_voucher_lines_pkey");

            entity.ToTable("journal_voucher_lines");

            entity.Property(e => e.JournalVoucherLineId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("journal_voucher_line_id");
            entity.Property(e => e.AccountId).HasColumnName("account_id");
            entity.Property(e => e.Credit)
                .HasPrecision(14, 2)
                .HasColumnName("credit");
            entity.Property(e => e.Debit)
                .HasPrecision(14, 2)
                .HasColumnName("debit");
            entity.Property(e => e.JournalVoucherId).HasColumnName("journal_voucher_id");
            entity.Property(e => e.Narration)
                .HasMaxLength(255)
                .HasColumnName("narration");

            entity.HasOne(d => d.Account).WithMany(p => p.JournalVoucherLines)
                .HasForeignKey(d => d.AccountId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("journal_voucher_lines_account_id_fkey");

            entity.HasOne(d => d.JournalVoucher).WithMany(p => p.JournalVoucherLines)
                .HasForeignKey(d => d.JournalVoucherId)
                .HasConstraintName("journal_voucher_lines_journal_voucher_id_fkey");
        });

        modelBuilder.Entity<LedgerEntry>(entity =>
        {
            entity.HasKey(e => e.LedgerEntryId).HasName("ledger_entries_pkey");

            entity.ToTable("ledger_entries");

            entity.HasIndex(e => new { e.AccountId, e.EntryDate }, "idx_ledger_account_date");

            entity.HasIndex(e => new { e.FinancialYearId, e.EntryDate }, "idx_ledger_fy_date");

            entity.Property(e => e.LedgerEntryId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("ledger_entry_id");
            entity.Property(e => e.AccountId).HasColumnName("account_id");
            entity.Property(e => e.BillNo)
                .HasMaxLength(20)
                .HasColumnName("bill_no");
            entity.Property(e => e.ChequeNo)
                .HasMaxLength(20)
                .HasColumnName("cheque_no");
            entity.Property(e => e.ClearanceDate).HasColumnName("clearance_date");
            entity.Property(e => e.ContraAccountId).HasColumnName("contra_account_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("created_at");
            entity.Property(e => e.Credit)
                .HasPrecision(14, 2)
                .HasColumnName("credit");
            entity.Property(e => e.Debit)
                .HasPrecision(14, 2)
                .HasColumnName("debit");
            entity.Property(e => e.EntryDate).HasColumnName("entry_date");
            entity.Property(e => e.FinancialYearId).HasColumnName("financial_year_id");
            entity.Property(e => e.Narration)
                .HasMaxLength(255)
                .HasColumnName("narration");
            entity.Property(e => e.Quantity)
                .HasPrecision(12, 3)
                .HasColumnName("quantity");
            entity.Property(e => e.VoucherId).HasColumnName("voucher_id");
            entity.Property(e => e.VoucherType).HasColumnName("voucher_type");

            entity.HasOne(d => d.Account).WithMany(p => p.LedgerEntryAccounts)
                .HasForeignKey(d => d.AccountId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("ledger_entries_account_id_fkey");

            entity.HasOne(d => d.ContraAccount).WithMany(p => p.LedgerEntryContraAccounts)
                .HasForeignKey(d => d.ContraAccountId)
                .HasConstraintName("ledger_entries_contra_account_id_fkey");

            entity.HasOne(d => d.FinancialYear).WithMany(p => p.LedgerEntries)
                .HasForeignKey(d => d.FinancialYearId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("ledger_entries_financial_year_id_fkey");
        });

        modelBuilder.Entity<LoginLog>(entity =>
        {
            entity.HasKey(e => e.LoginLogId).HasName("login_log_pkey");

            entity.ToTable("login_log");

            entity.Property(e => e.LoginLogId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("login_log_id");
            entity.Property(e => e.LoggedInAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("logged_in_at");
            entity.Property(e => e.LoggedOutAt).HasColumnName("logged_out_at");
            entity.Property(e => e.Source)
                .HasMaxLength(20)
                .HasDefaultValueSql("'desktop'::character varying")
                .HasColumnName("source");
            entity.Property(e => e.Success).HasColumnName("success");
            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.UsernameTried)
                .HasMaxLength(30)
                .HasColumnName("username_tried");

            entity.HasOne(d => d.User).WithMany(p => p.LoginLogs)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("login_log_user_id_fkey");
        });

        modelBuilder.Entity<Lot>(entity =>
        {
            entity.HasKey(e => e.LotId).HasName("lots_pkey");

            entity.ToTable("lots");

            entity.HasIndex(e => e.ItemId, "idx_lots_item");

            entity.HasIndex(e => e.SupplierId, "idx_lots_supplier");

            entity.HasIndex(e => new { e.FinancialYearId, e.LotNo }, "lots_financial_year_id_lot_no_key").IsUnique();

            entity.Property(e => e.LotId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("lot_id");
            entity.Property(e => e.FinancialYearId).HasColumnName("financial_year_id");
            entity.Property(e => e.IsClosed)
                .HasDefaultValue(false)
                .HasColumnName("is_closed");
            entity.Property(e => e.ItemId).HasColumnName("item_id");
            entity.Property(e => e.LotNo).HasColumnName("lot_no");
            entity.Property(e => e.ParentLotId).HasColumnName("parent_lot_id");
            entity.Property(e => e.ReceivedDate).HasColumnName("received_date");
            entity.Property(e => e.SupplierId).HasColumnName("supplier_id");

            entity.HasOne(d => d.FinancialYear).WithMany(p => p.Lots)
                .HasForeignKey(d => d.FinancialYearId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("lots_financial_year_id_fkey");

            entity.HasOne(d => d.Item).WithMany(p => p.Lots)
                .HasForeignKey(d => d.ItemId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("lots_item_id_fkey");

            entity.HasOne(d => d.ParentLot).WithMany(p => p.InverseParentLot)
                .HasForeignKey(d => d.ParentLotId)
                .HasConstraintName("lots_parent_lot_id_fkey");

            entity.HasOne(d => d.Supplier).WithMany(p => p.Lots)
                .HasForeignKey(d => d.SupplierId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("lots_supplier_id_fkey");
        });

        modelBuilder.Entity<LotOperation>(entity =>
        {
            entity.HasKey(e => e.LotOperationId).HasName("lot_operations_pkey");

            entity.ToTable("lot_operations");

            entity.Property(e => e.LotOperationId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("lot_operation_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.FinancialYearId).HasColumnName("financial_year_id");
            entity.Property(e => e.OpDate).HasColumnName("op_date");
            entity.Property(e => e.OpType).HasColumnName("op_type");
            entity.Property(e => e.Quantity)
                .HasPrecision(12, 3)
                .HasColumnName("quantity");
            entity.Property(e => e.Remarks)
                .HasMaxLength(200)
                .HasColumnName("remarks");
            entity.Property(e => e.SourceLotId).HasColumnName("source_lot_id");
            entity.Property(e => e.TargetAccountId).HasColumnName("target_account_id");
            entity.Property(e => e.TargetLotId).HasColumnName("target_lot_id");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.LotOperations)
                .HasForeignKey(d => d.CreatedBy)
                .HasConstraintName("lot_operations_created_by_fkey");

            entity.HasOne(d => d.FinancialYear).WithMany(p => p.LotOperations)
                .HasForeignKey(d => d.FinancialYearId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("lot_operations_financial_year_id_fkey");

            entity.HasOne(d => d.SourceLot).WithMany(p => p.LotOperationSourceLots)
                .HasForeignKey(d => d.SourceLotId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("lot_operations_source_lot_id_fkey");

            entity.HasOne(d => d.TargetAccount).WithMany(p => p.LotOperations)
                .HasForeignKey(d => d.TargetAccountId)
                .HasConstraintName("lot_operations_target_account_id_fkey");

            entity.HasOne(d => d.TargetLot).WithMany(p => p.LotOperationTargetLots)
                .HasForeignKey(d => d.TargetLotId)
                .HasConstraintName("lot_operations_target_lot_id_fkey");
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.HasKey(e => e.PaymentId).HasName("payments_pkey");

            entity.ToTable("payments");

            entity.HasIndex(e => new { e.AccountId, e.PaymentDate }, "idx_payments_account");

            entity.HasIndex(e => new { e.FinancialYearId, e.PaymentNo }, "payments_financial_year_id_payment_no_key").IsUnique();

            entity.Property(e => e.PaymentId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("payment_id");
            entity.Property(e => e.AccountId).HasColumnName("account_id");
            entity.Property(e => e.Advance)
                .HasPrecision(14, 2)
                .HasColumnName("advance");
            entity.Property(e => e.Amount)
                .HasPrecision(14, 2)
                .HasColumnName("amount");
            entity.Property(e => e.BankBranch)
                .HasMaxLength(60)
                .HasColumnName("bank_branch");
            entity.Property(e => e.BankName)
                .HasMaxLength(60)
                .HasColumnName("bank_name");
            entity.Property(e => e.ChequeDate).HasColumnName("cheque_date");
            entity.Property(e => e.ChequeNo)
                .HasMaxLength(20)
                .HasColumnName("cheque_no");
            entity.Property(e => e.ClearanceDate).HasColumnName("clearance_date");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.DaybookId).HasColumnName("daybook_id");
            entity.Property(e => e.Discount)
                .HasPrecision(14, 2)
                .HasColumnName("discount");
            entity.Property(e => e.FinancialYearId).HasColumnName("financial_year_id");
            entity.Property(e => e.Hamali)
                .HasPrecision(14, 2)
                .HasColumnName("hamali");
            entity.Property(e => e.IsFreightPayment)
                .HasDefaultValue(false)
                .HasColumnName("is_freight_payment");
            entity.Property(e => e.IsReturned)
                .HasDefaultValue(false)
                .HasColumnName("is_returned");
            entity.Property(e => e.Mode).HasColumnName("mode");
            entity.Property(e => e.PaymentDate).HasColumnName("payment_date");
            entity.Property(e => e.PaymentNo).HasColumnName("payment_no");
            entity.Property(e => e.Remarks)
                .HasMaxLength(200)
                .HasColumnName("remarks");
            entity.Property(e => e.ReturnedDate).HasColumnName("returned_date");
            entity.Property(e => e.TdsAmount)
                .HasPrecision(14, 2)
                .HasColumnName("tds_amount");
            entity.Property(e => e.TotalSettled)
                .HasPrecision(14, 2)
                .HasColumnName("total_settled");
            entity.Property(e => e.Vatav)
                .HasPrecision(14, 2)
                .HasColumnName("vatav");

            entity.HasOne(d => d.Account).WithMany(p => p.Payments)
                .HasForeignKey(d => d.AccountId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("payments_account_id_fkey");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.Payments)
                .HasForeignKey(d => d.CreatedBy)
                .HasConstraintName("payments_created_by_fkey");

            entity.HasOne(d => d.Daybook).WithMany(p => p.Payments)
                .HasForeignKey(d => d.DaybookId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("payments_daybook_id_fkey");

            entity.HasOne(d => d.FinancialYear).WithMany(p => p.Payments)
                .HasForeignKey(d => d.FinancialYearId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("payments_financial_year_id_fkey");
        });

        modelBuilder.Entity<PaymentAllocation>(entity =>
        {
            entity.HasKey(e => e.PaymentAllocationId).HasName("payment_allocations_pkey");

            entity.ToTable("payment_allocations");

            entity.HasIndex(e => new { e.PaymentId, e.PurchaseBillId }, "payment_allocations_payment_id_purchase_bill_id_key").IsUnique();

            entity.Property(e => e.PaymentAllocationId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("payment_allocation_id");
            entity.Property(e => e.AllocatedAmount)
                .HasPrecision(14, 2)
                .HasColumnName("allocated_amount");
            entity.Property(e => e.PaymentId).HasColumnName("payment_id");
            entity.Property(e => e.PurchaseBillId).HasColumnName("purchase_bill_id");

            entity.HasOne(d => d.Payment).WithMany(p => p.PaymentAllocations)
                .HasForeignKey(d => d.PaymentId)
                .HasConstraintName("payment_allocations_payment_id_fkey");

            entity.HasOne(d => d.PurchaseBill).WithMany(p => p.PaymentAllocations)
                .HasForeignKey(d => d.PurchaseBillId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("payment_allocations_purchase_bill_id_fkey");
        });

        modelBuilder.Entity<PurchaseBill>(entity =>
        {
            entity.HasKey(e => e.PurchaseBillId).HasName("purchase_bills_pkey");

            entity.ToTable("purchase_bills");

            entity.HasIndex(e => new { e.FinancialYearId, e.BillDate }, "idx_purchase_bills_date");

            entity.HasIndex(e => new { e.SupplierId, e.BillDate }, "idx_purchase_bills_supplier");

            entity.HasIndex(e => new { e.FinancialYearId, e.BillNo }, "purchase_bills_financial_year_id_bill_no_key").IsUnique();

            entity.Property(e => e.PurchaseBillId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("purchase_bill_id");
            entity.Property(e => e.Amanat)
                .HasPrecision(14, 2)
                .HasDefaultValueSql("0")
                .HasColumnName("amanat");
            entity.Property(e => e.AuthorisedBy)
                .HasMaxLength(60)
                .HasColumnName("authorised_by");
            entity.Property(e => e.BillDate).HasColumnName("bill_date");
            entity.Property(e => e.BillNo).HasColumnName("bill_no");
            entity.Property(e => e.ChallanNo)
                .HasMaxLength(30)
                .HasColumnName("challan_no");
            entity.Property(e => e.ColdStore)
                .HasPrecision(14, 2)
                .HasDefaultValueSql("0")
                .HasColumnName("cold_store");
            entity.Property(e => e.CommissionAmount)
                .HasPrecision(14, 2)
                .HasColumnName("commission_amount");
            entity.Property(e => e.CommissionPct)
                .HasPrecision(6, 3)
                .HasColumnName("commission_pct");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.DdCharge)
                .HasPrecision(14, 2)
                .HasDefaultValueSql("0")
                .HasColumnName("dd_charge");
            entity.Property(e => e.DeliveryPerson)
                .HasMaxLength(60)
                .HasColumnName("delivery_person");
            entity.Property(e => e.FinancialYearId).HasColumnName("financial_year_id");
            entity.Property(e => e.Freight)
                .HasPrecision(14, 2)
                .HasDefaultValueSql("0")
                .HasColumnName("freight");
            entity.Property(e => e.GrossAmount)
                .HasPrecision(14, 2)
                .HasColumnName("gross_amount");
            entity.Property(e => e.Inam)
                .HasPrecision(14, 2)
                .HasDefaultValueSql("0")
                .HasColumnName("inam");
            entity.Property(e => e.Labour)
                .HasPrecision(14, 2)
                .HasDefaultValueSql("0")
                .HasColumnName("labour");
            entity.Property(e => e.Mark)
                .HasMaxLength(30)
                .HasColumnName("mark");
            entity.Property(e => e.MarketFee)
                .HasPrecision(14, 2)
                .HasDefaultValueSql("0")
                .HasColumnName("market_fee");
            entity.Property(e => e.MarketFeePct)
                .HasPrecision(6, 3)
                .HasDefaultValueSql("0")
                .HasColumnName("market_fee_pct");
            entity.Property(e => e.Mode).HasColumnName("mode");
            entity.Property(e => e.NetAmount)
                .HasPrecision(14, 2)
                .HasColumnName("net_amount");
            entity.Property(e => e.OtherDeduction)
                .HasPrecision(14, 2)
                .HasDefaultValueSql("0")
                .HasColumnName("other_deduction");
            entity.Property(e => e.OurFreight)
                .HasPrecision(14, 2)
                .HasDefaultValueSql("0")
                .HasColumnName("our_freight");
            entity.Property(e => e.OurLabour)
                .HasPrecision(14, 2)
                .HasDefaultValueSql("0")
                .HasColumnName("our_labour");
            entity.Property(e => e.OurMarketFee)
                .HasPrecision(14, 2)
                .HasDefaultValueSql("0")
                .HasColumnName("our_market_fee");
            entity.Property(e => e.OurOther)
                .HasPrecision(14, 2)
                .HasDefaultValueSql("0")
                .HasColumnName("our_other");
            entity.Property(e => e.PackingMaterial)
                .HasPrecision(14, 2)
                .HasDefaultValueSql("0")
                .HasColumnName("packing_material");
            entity.Property(e => e.Postage)
                .HasPrecision(14, 2)
                .HasDefaultValueSql("0")
                .HasColumnName("postage");
            entity.Property(e => e.Remarks)
                .HasMaxLength(200)
                .HasColumnName("remarks");
            entity.Property(e => e.SupplierId).HasColumnName("supplier_id");
            entity.Property(e => e.TdsAmount)
                .HasPrecision(14, 2)
                .HasDefaultValueSql("0")
                .HasColumnName("tds_amount");
            entity.Property(e => e.TdsPct)
                .HasPrecision(6, 3)
                .HasDefaultValueSql("0")
                .HasColumnName("tds_pct");
            entity.Property(e => e.TruckNo)
                .HasMaxLength(20)
                .HasColumnName("truck_no");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("updated_at");
            entity.Property(e => e.Vatav)
                .HasPrecision(14, 2)
                .HasDefaultValueSql("0")
                .HasColumnName("vatav");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.PurchaseBills)
                .HasForeignKey(d => d.CreatedBy)
                .HasConstraintName("purchase_bills_created_by_fkey");

            entity.HasOne(d => d.FinancialYear).WithMany(p => p.PurchaseBills)
                .HasForeignKey(d => d.FinancialYearId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("purchase_bills_financial_year_id_fkey");

            entity.HasOne(d => d.Supplier).WithMany(p => p.PurchaseBills)
                .HasForeignKey(d => d.SupplierId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("purchase_bills_supplier_id_fkey");
        });

        modelBuilder.Entity<PurchaseBillItem>(entity =>
        {
            entity.HasKey(e => e.PurchaseBillItemId).HasName("purchase_bill_items_pkey");

            entity.ToTable("purchase_bill_items");

            entity.HasIndex(e => e.LotId, "idx_pbi_lot");

            entity.Property(e => e.PurchaseBillItemId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("purchase_bill_item_id");
            entity.Property(e => e.Amount)
                .HasPrecision(14, 2)
                .HasColumnName("amount");
            entity.Property(e => e.CrateInfo)
                .HasMaxLength(30)
                .HasColumnName("crate_info");
            entity.Property(e => e.Description)
                .HasMaxLength(100)
                .HasColumnName("description");
            entity.Property(e => e.GrossRate)
                .HasPrecision(12, 2)
                .HasColumnName("gross_rate");
            entity.Property(e => e.ItemId).HasColumnName("item_id");
            entity.Property(e => e.LotId).HasColumnName("lot_id");
            entity.Property(e => e.NetRate)
                .HasPrecision(12, 2)
                .HasColumnName("net_rate");
            entity.Property(e => e.PurchaseBillId).HasColumnName("purchase_bill_id");
            entity.Property(e => e.Quantity)
                .HasPrecision(12, 3)
                .HasColumnName("quantity");
            entity.Property(e => e.Weight)
                .HasPrecision(12, 3)
                .HasColumnName("weight");

            entity.HasOne(d => d.Item).WithMany(p => p.PurchaseBillItems)
                .HasForeignKey(d => d.ItemId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("purchase_bill_items_item_id_fkey");

            entity.HasOne(d => d.Lot).WithMany(p => p.PurchaseBillItems)
                .HasForeignKey(d => d.LotId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("purchase_bill_items_lot_id_fkey");

            entity.HasOne(d => d.PurchaseBill).WithMany(p => p.PurchaseBillItems)
                .HasForeignKey(d => d.PurchaseBillId)
                .HasConstraintName("purchase_bill_items_purchase_bill_id_fkey");
        });

        modelBuilder.Entity<Receipt>(entity =>
        {
            entity.HasKey(e => e.ReceiptId).HasName("receipts_pkey");

            entity.ToTable("receipts");

            entity.HasIndex(e => new { e.AccountId, e.ReceiptDate }, "idx_receipts_account");

            entity.HasIndex(e => new { e.FinancialYearId, e.ReceiptNo }, "receipts_financial_year_id_receipt_no_key").IsUnique();

            entity.Property(e => e.ReceiptId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("receipt_id");
            entity.Property(e => e.AccountId).HasColumnName("account_id");
            entity.Property(e => e.Amount)
                .HasPrecision(14, 2)
                .HasColumnName("amount");
            entity.Property(e => e.BankBranch)
                .HasMaxLength(60)
                .HasColumnName("bank_branch");
            entity.Property(e => e.BankName)
                .HasMaxLength(60)
                .HasColumnName("bank_name");
            entity.Property(e => e.ChequeDate).HasColumnName("cheque_date");
            entity.Property(e => e.ChequeNo)
                .HasMaxLength(20)
                .HasColumnName("cheque_no");
            entity.Property(e => e.ClearanceDate).HasColumnName("clearance_date");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.DaybookId).HasColumnName("daybook_id");
            entity.Property(e => e.FinancialYearId).HasColumnName("financial_year_id");
            entity.Property(e => e.IsReturned)
                .HasDefaultValue(false)
                .HasColumnName("is_returned");
            entity.Property(e => e.Mode).HasColumnName("mode");
            entity.Property(e => e.ReceiptDate).HasColumnName("receipt_date");
            entity.Property(e => e.ReceiptNo).HasColumnName("receipt_no");
            entity.Property(e => e.Remarks)
                .HasMaxLength(200)
                .HasColumnName("remarks");
            entity.Property(e => e.ReturnedDate).HasColumnName("returned_date");
            entity.Property(e => e.RoundingDiff)
                .HasPrecision(8, 2)
                .HasColumnName("rounding_diff");
            entity.Property(e => e.TotalSettled)
                .HasPrecision(14, 2)
                .HasColumnName("total_settled");
            entity.Property(e => e.Vatav)
                .HasPrecision(14, 2)
                .HasColumnName("vatav");

            entity.HasOne(d => d.Account).WithMany(p => p.Receipts)
                .HasForeignKey(d => d.AccountId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("receipts_account_id_fkey");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.Receipts)
                .HasForeignKey(d => d.CreatedBy)
                .HasConstraintName("receipts_created_by_fkey");

            entity.HasOne(d => d.Daybook).WithMany(p => p.Receipts)
                .HasForeignKey(d => d.DaybookId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("receipts_daybook_id_fkey");

            entity.HasOne(d => d.FinancialYear).WithMany(p => p.Receipts)
                .HasForeignKey(d => d.FinancialYearId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("receipts_financial_year_id_fkey");
        });

        modelBuilder.Entity<ReceiptAllocation>(entity =>
        {
            entity.HasKey(e => e.ReceiptAllocationId).HasName("receipt_allocations_pkey");

            entity.ToTable("receipt_allocations");

            entity.HasIndex(e => new { e.ReceiptId, e.SalesBillId }, "receipt_allocations_receipt_id_sales_bill_id_key").IsUnique();

            entity.Property(e => e.ReceiptAllocationId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("receipt_allocation_id");
            entity.Property(e => e.AllocatedAmount)
                .HasPrecision(14, 2)
                .HasColumnName("allocated_amount");
            entity.Property(e => e.FlaggedByAi)
                .HasDefaultValue(false)
                .HasColumnName("flagged_by_ai");
            entity.Property(e => e.ReceiptId).HasColumnName("receipt_id");
            entity.Property(e => e.SalesBillId).HasColumnName("sales_bill_id");
            entity.Property(e => e.VatavAmount)
                .HasPrecision(14, 2)
                .HasColumnName("vatav_amount");

            entity.HasOne(d => d.Receipt).WithMany(p => p.ReceiptAllocations)
                .HasForeignKey(d => d.ReceiptId)
                .HasConstraintName("receipt_allocations_receipt_id_fkey");

            entity.HasOne(d => d.SalesBill).WithMany(p => p.ReceiptAllocations)
                .HasForeignKey(d => d.SalesBillId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("receipt_allocations_sales_bill_id_fkey");
        });

        modelBuilder.Entity<Region>(entity =>
        {
            entity.HasKey(e => e.RegionId).HasName("regions_pkey");

            entity.ToTable("regions");

            entity.HasIndex(e => new { e.CompanyId, e.Code }, "regions_company_id_code_key").IsUnique();

            entity.HasIndex(e => new { e.CompanyId, e.Name }, "regions_company_id_name_key").IsUnique();

            entity.Property(e => e.RegionId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("region_id");
            entity.Property(e => e.Code)
                .HasMaxLength(5)
                .HasColumnName("code");
            entity.Property(e => e.CompanyId).HasColumnName("company_id");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");

            entity.HasOne(d => d.Company).WithMany(p => p.Regions)
                .HasForeignKey(d => d.CompanyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("regions_company_id_fkey");
        });

        modelBuilder.Entity<Sale>(entity =>
        {
            entity.HasKey(e => e.SaleId).HasName("sales_pkey");

            entity.ToTable("sales");

            entity.HasIndex(e => new { e.BuyerId, e.SaleDate }, "idx_sales_buyer");

            entity.HasIndex(e => new { e.FinancialYearId, e.SaleDate }, "idx_sales_date");

            entity.HasIndex(e => e.LotId, "idx_sales_lot");

            entity.Property(e => e.SaleId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("sale_id");
            entity.Property(e => e.Amount)
                .HasPrecision(14, 2)
                .HasColumnName("amount");
            entity.Property(e => e.BuyerId).HasColumnName("buyer_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.DeliveredQty)
                .HasPrecision(12, 3)
                .HasColumnName("delivered_qty");
            entity.Property(e => e.DeliveredTo)
                .HasMaxLength(60)
                .HasColumnName("delivered_to");
            entity.Property(e => e.FinancialYearId).HasColumnName("financial_year_id");
            entity.Property(e => e.Freight)
                .HasPrecision(14, 2)
                .HasDefaultValueSql("0")
                .HasColumnName("freight");
            entity.Property(e => e.ItemId).HasColumnName("item_id");
            entity.Property(e => e.Labour)
                .HasPrecision(14, 2)
                .HasDefaultValueSql("0")
                .HasColumnName("labour");
            entity.Property(e => e.LabourRate)
                .HasPrecision(10, 2)
                .HasDefaultValueSql("0")
                .HasColumnName("labour_rate");
            entity.Property(e => e.LotId).HasColumnName("lot_id");
            entity.Property(e => e.MarketFee)
                .HasPrecision(14, 2)
                .HasDefaultValueSql("0")
                .HasColumnName("market_fee");
            entity.Property(e => e.MarketFeePct)
                .HasPrecision(6, 3)
                .HasDefaultValueSql("0")
                .HasColumnName("market_fee_pct");
            entity.Property(e => e.Packing)
                .HasPrecision(14, 2)
                .HasDefaultValueSql("0")
                .HasColumnName("packing");
            entity.Property(e => e.PackingRate)
                .HasPrecision(10, 2)
                .HasDefaultValueSql("0")
                .HasColumnName("packing_rate");
            entity.Property(e => e.Quantity)
                .HasPrecision(12, 3)
                .HasColumnName("quantity");
            entity.Property(e => e.Rate)
                .HasPrecision(12, 2)
                .HasColumnName("rate");
            entity.Property(e => e.Remarks)
                .HasMaxLength(200)
                .HasColumnName("remarks");
            entity.Property(e => e.SaleDate).HasColumnName("sale_date");
            entity.Property(e => e.SalesBillId).HasColumnName("sales_bill_id");
            entity.Property(e => e.TcsAmount)
                .HasPrecision(14, 2)
                .HasDefaultValueSql("0")
                .HasColumnName("tcs_amount");
            entity.Property(e => e.TcsPct)
                .HasPrecision(6, 3)
                .HasDefaultValueSql("0")
                .HasColumnName("tcs_pct");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("updated_at");
            entity.Property(e => e.Weight)
                .HasPrecision(12, 3)
                .HasColumnName("weight");

            entity.HasOne(d => d.Buyer).WithMany(p => p.Sales)
                .HasForeignKey(d => d.BuyerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("sales_buyer_id_fkey");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.Sales)
                .HasForeignKey(d => d.CreatedBy)
                .HasConstraintName("sales_created_by_fkey");

            entity.HasOne(d => d.FinancialYear).WithMany(p => p.Sales)
                .HasForeignKey(d => d.FinancialYearId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("sales_financial_year_id_fkey");

            entity.HasOne(d => d.Item).WithMany(p => p.Sales)
                .HasForeignKey(d => d.ItemId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("sales_item_id_fkey");

            entity.HasOne(d => d.Lot).WithMany(p => p.Sales)
                .HasForeignKey(d => d.LotId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("sales_lot_id_fkey");

            entity.HasOne(d => d.SalesBill).WithMany(p => p.Sales)
                .HasForeignKey(d => d.SalesBillId)
                .HasConstraintName("sales_sales_bill_id_fkey");
        });

        modelBuilder.Entity<SalesBill>(entity =>
        {
            entity.HasKey(e => e.SalesBillId).HasName("sales_bills_pkey");

            entity.ToTable("sales_bills");

            entity.HasIndex(e => new { e.BuyerId, e.BillDate }, "idx_sales_bills_buyer");

            entity.HasIndex(e => new { e.FinancialYearId, e.BillNo }, "sales_bills_financial_year_id_bill_no_key").IsUnique();

            entity.Property(e => e.SalesBillId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("sales_bill_id");
            entity.Property(e => e.BillDate).HasColumnName("bill_date");
            entity.Property(e => e.BillNo).HasColumnName("bill_no");
            entity.Property(e => e.BuyerId).HasColumnName("buyer_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.FinancialYearId).HasColumnName("financial_year_id");
            entity.Property(e => e.IsWeekly)
                .HasDefaultValue(false)
                .HasColumnName("is_weekly");
            entity.Property(e => e.NetAmount)
                .HasPrecision(14, 2)
                .HasColumnName("net_amount");
            entity.Property(e => e.PeriodFrom).HasColumnName("period_from");
            entity.Property(e => e.PeriodTo).HasColumnName("period_to");
            entity.Property(e => e.TotalAmount)
                .HasPrecision(14, 2)
                .HasColumnName("total_amount");

            entity.HasOne(d => d.Buyer).WithMany(p => p.SalesBills)
                .HasForeignKey(d => d.BuyerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("sales_bills_buyer_id_fkey");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.SalesBills)
                .HasForeignKey(d => d.CreatedBy)
                .HasConstraintName("sales_bills_created_by_fkey");

            entity.HasOne(d => d.FinancialYear).WithMany(p => p.SalesBills)
                .HasForeignKey(d => d.FinancialYearId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("sales_bills_financial_year_id_fkey");
        });

        modelBuilder.Entity<Salesman>(entity =>
        {
            entity.HasKey(e => e.SalesmanId).HasName("salesmen_pkey");

            entity.ToTable("salesmen");

            entity.HasIndex(e => new { e.CompanyId, e.Name }, "salesmen_company_id_name_key").IsUnique();

            entity.Property(e => e.SalesmanId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("salesman_id");
            entity.Property(e => e.CommissionPct)
                .HasPrecision(6, 3)
                .HasDefaultValueSql("0")
                .HasColumnName("commission_pct");
            entity.Property(e => e.CompanyId).HasColumnName("company_id");
            entity.Property(e => e.IsActive)
                .HasDefaultValue(true)
                .HasColumnName("is_active");
            entity.Property(e => e.Mobile)
                .HasMaxLength(15)
                .HasColumnName("mobile");
            entity.Property(e => e.Name)
                .HasMaxLength(60)
                .HasColumnName("name");

            entity.HasOne(d => d.Company).WithMany(p => p.Salesmen)
                .HasForeignKey(d => d.CompanyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("salesmen_company_id_fkey");
        });

        modelBuilder.Entity<SystemParameter>(entity =>
        {
            entity.HasKey(e => e.ParameterKey).HasName("system_parameters_pkey");

            entity.ToTable("system_parameters");

            entity.Property(e => e.ParameterKey)
                .HasMaxLength(50)
                .HasColumnName("parameter_key");
            entity.Property(e => e.Description)
                .HasMaxLength(200)
                .HasColumnName("description");
            entity.Property(e => e.EffectiveFrom).HasColumnName("effective_from");
            entity.Property(e => e.ParameterValue)
                .HasMaxLength(100)
                .HasColumnName("parameter_value");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("updated_at");
        });

        modelBuilder.Entity<TdsPurchaseDeduction>(entity =>
        {
            entity.HasKey(e => e.TdsDeductionId).HasName("tds_purchase_deductions_pkey");

            entity.ToTable("tds_purchase_deductions");

            entity.HasIndex(e => new { e.FinancialYearId, e.SupplierId }, "idx_tds_pur_supplier");

            entity.Property(e => e.TdsDeductionId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("tds_deduction_id");
            entity.Property(e => e.CumulativeBefore)
                .HasPrecision(14, 2)
                .HasColumnName("cumulative_before");
            entity.Property(e => e.DeductedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("deducted_at");
            entity.Property(e => e.FinancialYearId).HasColumnName("financial_year_id");
            entity.Property(e => e.PaymentId).HasColumnName("payment_id");
            entity.Property(e => e.SupplierId).HasColumnName("supplier_id");
            entity.Property(e => e.TaxableExcess)
                .HasPrecision(14, 2)
                .HasColumnName("taxable_excess");
            entity.Property(e => e.TdsAmount)
                .HasPrecision(14, 2)
                .HasColumnName("tds_amount");
            entity.Property(e => e.TdsRate)
                .HasPrecision(6, 3)
                .HasColumnName("tds_rate");

            entity.HasOne(d => d.FinancialYear).WithMany(p => p.TdsPurchaseDeductions)
                .HasForeignKey(d => d.FinancialYearId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("tds_purchase_deductions_financial_year_id_fkey");

            entity.HasOne(d => d.Payment).WithMany(p => p.TdsPurchaseDeductions)
                .HasForeignKey(d => d.PaymentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("tds_purchase_deductions_payment_id_fkey");

            entity.HasOne(d => d.Supplier).WithMany(p => p.TdsPurchaseDeductions)
                .HasForeignKey(d => d.SupplierId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("tds_purchase_deductions_supplier_id_fkey");
        });

        modelBuilder.Entity<Transporter>(entity =>
        {
            entity.HasKey(e => e.TransporterId).HasName("transporters_pkey");

            entity.ToTable("transporters");

            entity.HasIndex(e => new { e.CompanyId, e.Name }, "transporters_company_id_name_key").IsUnique();

            entity.Property(e => e.TransporterId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("transporter_id");
            entity.Property(e => e.City)
                .HasMaxLength(50)
                .HasColumnName("city");
            entity.Property(e => e.CompanyId).HasColumnName("company_id");
            entity.Property(e => e.Mobile)
                .HasMaxLength(15)
                .HasColumnName("mobile");
            entity.Property(e => e.Name)
                .HasMaxLength(60)
                .HasColumnName("name");

            entity.HasOne(d => d.Company).WithMany(p => p.Transporters)
                .HasForeignKey(d => d.CompanyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("transporters_company_id_fkey");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("users_pkey");

            entity.ToTable("users");

            entity.HasIndex(e => e.Username, "users_username_key").IsUnique();

            entity.Property(e => e.UserId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("user_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("created_at");
            entity.Property(e => e.DisplayName)
                .HasMaxLength(60)
                .HasColumnName("display_name");
            entity.Property(e => e.IsActive)
                .HasDefaultValue(true)
                .HasColumnName("is_active");
            entity.Property(e => e.PasswordHash)
                .HasMaxLength(255)
                .HasColumnName("password_hash");
            entity.Property(e => e.Role).HasColumnName("role");
            entity.Property(e => e.Username)
                .HasMaxLength(30)
                .HasColumnName("username");

            entity.HasMany(d => d.Companies).WithMany(p => p.Users)
                .UsingEntity<Dictionary<string, object>>(
                    "UserCompanyAccess",
                    r => r.HasOne<Company>().WithMany()
                        .HasForeignKey("CompanyId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("user_company_access_company_id_fkey"),
                    l => l.HasOne<User>().WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("user_company_access_user_id_fkey"),
                    j =>
                    {
                        j.HasKey("UserId", "CompanyId").HasName("user_company_access_pkey");
                        j.ToTable("user_company_access");
                        j.IndexerProperty<long>("UserId").HasColumnName("user_id");
                        j.IndexerProperty<long>("CompanyId").HasColumnName("company_id");
                    });
        });

        modelBuilder.Entity<UserPreference>(entity =>
        {
            entity.HasKey(e => e.UserPreferenceId).HasName("user_preferences_pkey");

            entity.ToTable("user_preferences");

            entity.HasIndex(e => new { e.UserId, e.PreferenceKey }, "idx_user_preferences_key").IsUnique();

            entity.Property(e => e.UserPreferenceId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("user_preference_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.PreferenceKey)
                .HasMaxLength(60)
                .HasColumnName("preference_key");
            entity.Property(e => e.PreferenceValue).HasColumnName("preference_value");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("created_at");

            entity.HasOne(d => d.User).WithMany(p => p.UserPreferences)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("user_preferences_user_id_fkey");
        });

        modelBuilder.Entity<UserPermission>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.MenuKey }).HasName("user_permissions_pkey");

            entity.ToTable("user_permissions");

            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.MenuKey)
                .HasMaxLength(60)
                .HasColumnName("menu_key");
            entity.Property(e => e.CanAdd)
                .HasDefaultValue(false)
                .HasColumnName("can_add");
            entity.Property(e => e.CanDelete)
                .HasDefaultValue(false)
                .HasColumnName("can_delete");
            entity.Property(e => e.CanModify)
                .HasDefaultValue(false)
                .HasColumnName("can_modify");
            entity.Property(e => e.CanView)
                .HasDefaultValue(false)
                .HasColumnName("can_view");

            entity.HasOne(d => d.User).WithMany(p => p.UserPermissions)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("user_permissions_user_id_fkey");
        });

        modelBuilder.Entity<WhatsappDispatch>(entity =>
        {
            entity.HasKey(e => e.DispatchId).HasName("whatsapp_dispatches_pkey");

            entity.ToTable("whatsapp_dispatches");

            entity.HasIndex(e => new { e.AccountId, e.SentAt }, "idx_wa_dispatch_account");

            entity.Property(e => e.DispatchId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("dispatch_id");
            entity.Property(e => e.AccountId).HasColumnName("account_id");
            entity.Property(e => e.DocumentType)
                .HasMaxLength(30)
                .HasColumnName("document_type");
            entity.Property(e => e.ErrorDetail)
                .HasMaxLength(255)
                .HasColumnName("error_detail");
            entity.Property(e => e.Mobile)
                .HasMaxLength(15)
                .HasColumnName("mobile");
            entity.Property(e => e.PdfPath)
                .HasMaxLength(255)
                .HasColumnName("pdf_path");
            entity.Property(e => e.PeriodFrom).HasColumnName("period_from");
            entity.Property(e => e.PeriodTo).HasColumnName("period_to");
            entity.Property(e => e.SentAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("sent_at");
            entity.Property(e => e.SentBy).HasColumnName("sent_by");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.StatusUpdatedAt).HasColumnName("status_updated_at");
            entity.Property(e => e.WaMessageId)
                .HasMaxLength(80)
                .HasColumnName("wa_message_id");

            entity.HasOne(d => d.Account).WithMany(p => p.WhatsappDispatches)
                .HasForeignKey(d => d.AccountId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("whatsapp_dispatches_account_id_fkey");

            entity.HasOne(d => d.SentByNavigation).WithMany(p => p.WhatsappDispatches)
                .HasForeignKey(d => d.SentBy)
                .HasConstraintName("whatsapp_dispatches_sent_by_fkey");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
