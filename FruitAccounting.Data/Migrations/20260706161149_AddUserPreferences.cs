using System;
using FruitAccounting.Data.Enums;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace FruitAccounting.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddUserPreferences : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:Enum:cold_txn_type", "inward,outward")
                .Annotation("Npgsql:Enum:crate_txn_type", "issue,return,amount_conversion")
                .Annotation("Npgsql:Enum:dispatch_status", "queued,sent,delivered,read,failed")
                .Annotation("Npgsql:Enum:dr_cr", "debit,credit")
                .Annotation("Npgsql:Enum:lot_op_type", "split,merge,transfer")
                .Annotation("Npgsql:Enum:payment_mode", "cash,bank,cheque")
                .Annotation("Npgsql:Enum:purchase_mode", "with_commission,trading,without_commission")
                .Annotation("Npgsql:Enum:user_role", "admin,operator,readonly")
                .Annotation("Npgsql:Enum:voucher_type", "purchase_bill,sales_bill,receipt,payment,journal,bank_entry,crate,cold_storage,desavar_purchase,desavar_sale,import_purchase,import_sale,opening_balance");

            migrationBuilder.CreateTable(
                name: "companies",
                columns: table => new
                {
                    company_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    code = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    address1 = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    address2 = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    city = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    phone = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    email = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    pan_no = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: true),
                    gstin = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    apmc_licence_no = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: true),
                    bank_name = table.Column<string>(type: "character varying(60)", maxLength: 60, nullable: true),
                    bank_account_no = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: true),
                    bank_ifsc = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: true),
                    logo = table.Column<byte[]>(type: "bytea", nullable: true),
                    is_active = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("companies_pkey", x => x.company_id);
                });

            migrationBuilder.CreateTable(
                name: "feature_toggles",
                columns: table => new
                {
                    feature_key = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false),
                    is_enabled = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    description = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("feature_toggles_pkey", x => x.feature_key);
                });

            migrationBuilder.CreateTable(
                name: "system_parameters",
                columns: table => new
                {
                    parameter_key = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    parameter_value = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    description = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    effective_from = table.Column<DateOnly>(type: "date", nullable: true),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("system_parameters_pkey", x => x.parameter_key);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    user_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    username = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    display_name = table.Column<string>(type: "character varying(60)", maxLength: 60, nullable: true),
                    password_hash = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    role = table.Column<UserRole>(type: "user_role", nullable: false),
                    is_active = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("users_pkey", x => x.user_id);
                });

            migrationBuilder.CreateTable(
                name: "account_groups",
                columns: table => new
                {
                    account_group_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    company_id = table.Column<long>(type: "bigint", nullable: false),
                    parent_id = table.Column<long>(type: "bigint", nullable: true),
                    code = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true),
                    name = table.Column<string>(type: "character varying(60)", maxLength: 60, nullable: false),
                    nature = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    is_system = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("account_groups_pkey", x => x.account_group_id);
                    table.ForeignKey(
                        name: "account_groups_company_id_fkey",
                        column: x => x.company_id,
                        principalTable: "companies",
                        principalColumn: "company_id");
                    table.ForeignKey(
                        name: "account_groups_parent_id_fkey",
                        column: x => x.parent_id,
                        principalTable: "account_groups",
                        principalColumn: "account_group_id");
                });

            migrationBuilder.CreateTable(
                name: "financial_years",
                columns: table => new
                {
                    financial_year_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    company_id = table.Column<long>(type: "bigint", nullable: false),
                    code = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    start_date = table.Column<DateOnly>(type: "date", nullable: false),
                    end_date = table.Column<DateOnly>(type: "date", nullable: false),
                    is_active = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    is_closed = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("financial_years_pkey", x => x.financial_year_id);
                    table.ForeignKey(
                        name: "financial_years_company_id_fkey",
                        column: x => x.company_id,
                        principalTable: "companies",
                        principalColumn: "company_id");
                });

            migrationBuilder.CreateTable(
                name: "item_categories",
                columns: table => new
                {
                    item_category_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    company_id = table.Column<long>(type: "bigint", nullable: false),
                    name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("item_categories_pkey", x => x.item_category_id);
                    table.ForeignKey(
                        name: "item_categories_company_id_fkey",
                        column: x => x.company_id,
                        principalTable: "companies",
                        principalColumn: "company_id");
                });

            migrationBuilder.CreateTable(
                name: "item_groups",
                columns: table => new
                {
                    item_group_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    company_id = table.Column<long>(type: "bigint", nullable: false),
                    name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("item_groups_pkey", x => x.item_group_id);
                    table.ForeignKey(
                        name: "item_groups_company_id_fkey",
                        column: x => x.company_id,
                        principalTable: "companies",
                        principalColumn: "company_id");
                });

            migrationBuilder.CreateTable(
                name: "regions",
                columns: table => new
                {
                    region_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    company_id = table.Column<long>(type: "bigint", nullable: false),
                    code = table.Column<string>(type: "character varying(5)", maxLength: 5, nullable: false),
                    name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("regions_pkey", x => x.region_id);
                    table.ForeignKey(
                        name: "regions_company_id_fkey",
                        column: x => x.company_id,
                        principalTable: "companies",
                        principalColumn: "company_id");
                });

            migrationBuilder.CreateTable(
                name: "salesmen",
                columns: table => new
                {
                    salesman_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    company_id = table.Column<long>(type: "bigint", nullable: false),
                    name = table.Column<string>(type: "character varying(60)", maxLength: 60, nullable: false),
                    mobile = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: true),
                    commission_pct = table.Column<decimal>(type: "numeric(6,3)", precision: 6, scale: 3, nullable: true, defaultValueSql: "0"),
                    is_active = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("salesmen_pkey", x => x.salesman_id);
                    table.ForeignKey(
                        name: "salesmen_company_id_fkey",
                        column: x => x.company_id,
                        principalTable: "companies",
                        principalColumn: "company_id");
                });

            migrationBuilder.CreateTable(
                name: "transporters",
                columns: table => new
                {
                    transporter_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    company_id = table.Column<long>(type: "bigint", nullable: false),
                    name = table.Column<string>(type: "character varying(60)", maxLength: 60, nullable: false),
                    city = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    mobile = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("transporters_pkey", x => x.transporter_id);
                    table.ForeignKey(
                        name: "transporters_company_id_fkey",
                        column: x => x.company_id,
                        principalTable: "companies",
                        principalColumn: "company_id");
                });

            migrationBuilder.CreateTable(
                name: "attachments",
                columns: table => new
                {
                    attachment_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    voucher_id = table.Column<long>(type: "bigint", nullable: false),
                    voucher_type = table.Column<VoucherType>(type: "voucher_type", nullable: false),
                    file_name = table.Column<string>(type: "character varying(120)", maxLength: 120, nullable: false),
                    file_path = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    uploaded_by = table.Column<long>(type: "bigint", nullable: true),
                    uploaded_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("attachments_pkey", x => x.attachment_id);
                    table.ForeignKey(
                        name: "attachments_uploaded_by_fkey",
                        column: x => x.uploaded_by,
                        principalTable: "users",
                        principalColumn: "user_id");
                });

            migrationBuilder.CreateTable(
                name: "audit_log",
                columns: table => new
                {
                    audit_log_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    user_id = table.Column<long>(type: "bigint", nullable: true),
                    company_id = table.Column<long>(type: "bigint", nullable: true),
                    table_name = table.Column<string>(type: "character varying(60)", maxLength: 60, nullable: false),
                    record_pk = table.Column<long>(type: "bigint", nullable: true),
                    action = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    detail = table.Column<string>(type: "jsonb", nullable: true),
                    occurred_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("audit_log_pkey", x => x.audit_log_id);
                    table.ForeignKey(
                        name: "audit_log_company_id_fkey",
                        column: x => x.company_id,
                        principalTable: "companies",
                        principalColumn: "company_id");
                    table.ForeignKey(
                        name: "audit_log_user_id_fkey",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "user_id");
                });

            migrationBuilder.CreateTable(
                name: "login_log",
                columns: table => new
                {
                    login_log_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    user_id = table.Column<long>(type: "bigint", nullable: true),
                    username_tried = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: true),
                    success = table.Column<bool>(type: "boolean", nullable: false),
                    source = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false, defaultValueSql: "'desktop'::character varying"),
                    logged_in_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    logged_out_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("login_log_pkey", x => x.login_log_id);
                    table.ForeignKey(
                        name: "login_log_user_id_fkey",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "user_id");
                });

            migrationBuilder.CreateTable(
                name: "user_company_access",
                columns: table => new
                {
                    user_id = table.Column<long>(type: "bigint", nullable: false),
                    company_id = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("user_company_access_pkey", x => new { x.user_id, x.company_id });
                    table.ForeignKey(
                        name: "user_company_access_company_id_fkey",
                        column: x => x.company_id,
                        principalTable: "companies",
                        principalColumn: "company_id");
                    table.ForeignKey(
                        name: "user_company_access_user_id_fkey",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "user_id");
                });

            migrationBuilder.CreateTable(
                name: "user_permissions",
                columns: table => new
                {
                    user_id = table.Column<long>(type: "bigint", nullable: false),
                    menu_key = table.Column<string>(type: "character varying(60)", maxLength: 60, nullable: false),
                    can_view = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    can_add = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    can_modify = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    can_delete = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("user_permissions_pkey", x => new { x.user_id, x.menu_key });
                    table.ForeignKey(
                        name: "user_permissions_user_id_fkey",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "user_id");
                });

            migrationBuilder.CreateTable(
                name: "user_preferences",
                columns: table => new
                {
                    user_preference_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    user_id = table.Column<long>(type: "bigint", nullable: false),
                    preference_key = table.Column<string>(type: "character varying(60)", maxLength: 60, nullable: false),
                    preference_value = table.Column<string>(type: "text", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("user_preferences_pkey", x => x.user_preference_id);
                    table.ForeignKey(
                        name: "user_preferences_user_id_fkey",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "user_id");
                });

            migrationBuilder.CreateTable(
                name: "data_locks",
                columns: table => new
                {
                    data_lock_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    financial_year_id = table.Column<long>(type: "bigint", nullable: false),
                    voucher_type = table.Column<VoucherType>(type: "voucher_type", nullable: false),
                    locked_upto = table.Column<DateOnly>(type: "date", nullable: false),
                    set_by = table.Column<long>(type: "bigint", nullable: true),
                    set_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("data_locks_pkey", x => x.data_lock_id);
                    table.ForeignKey(
                        name: "data_locks_financial_year_id_fkey",
                        column: x => x.financial_year_id,
                        principalTable: "financial_years",
                        principalColumn: "financial_year_id");
                    table.ForeignKey(
                        name: "fk_data_locks_user",
                        column: x => x.set_by,
                        principalTable: "users",
                        principalColumn: "user_id");
                });

            migrationBuilder.CreateTable(
                name: "journal_vouchers",
                columns: table => new
                {
                    journal_voucher_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    financial_year_id = table.Column<long>(type: "bigint", nullable: false),
                    voucher_no = table.Column<int>(type: "integer", nullable: false),
                    voucher_date = table.Column<DateOnly>(type: "date", nullable: false),
                    reference_no = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: true),
                    narration = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    created_by = table.Column<long>(type: "bigint", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("journal_vouchers_pkey", x => x.journal_voucher_id);
                    table.ForeignKey(
                        name: "journal_vouchers_created_by_fkey",
                        column: x => x.created_by,
                        principalTable: "users",
                        principalColumn: "user_id");
                    table.ForeignKey(
                        name: "journal_vouchers_financial_year_id_fkey",
                        column: x => x.financial_year_id,
                        principalTable: "financial_years",
                        principalColumn: "financial_year_id");
                });

            migrationBuilder.CreateTable(
                name: "item_counts",
                columns: table => new
                {
                    item_count_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    company_id = table.Column<long>(type: "bigint", nullable: false),
                    item_group_id = table.Column<long>(type: "bigint", nullable: true),
                    name = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("item_counts_pkey", x => x.item_count_id);
                    table.ForeignKey(
                        name: "item_counts_company_id_fkey",
                        column: x => x.company_id,
                        principalTable: "companies",
                        principalColumn: "company_id");
                    table.ForeignKey(
                        name: "item_counts_item_group_id_fkey",
                        column: x => x.item_group_id,
                        principalTable: "item_groups",
                        principalColumn: "item_group_id");
                });

            migrationBuilder.CreateTable(
                name: "items",
                columns: table => new
                {
                    item_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    company_id = table.Column<long>(type: "bigint", nullable: false),
                    code = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true),
                    name = table.Column<string>(type: "character varying(60)", maxLength: 60, nullable: false),
                    item_group_id = table.Column<long>(type: "bigint", nullable: true),
                    item_category_id = table.Column<long>(type: "bigint", nullable: true),
                    unit = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false, defaultValueSql: "'Box'::character varying"),
                    labour_rate = table.Column<decimal>(type: "numeric(10,2)", precision: 10, scale: 2, nullable: true, defaultValueSql: "0"),
                    packing_rate = table.Column<decimal>(type: "numeric(10,2)", precision: 10, scale: 2, nullable: true, defaultValueSql: "0"),
                    uses_crate = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    is_active = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("items_pkey", x => x.item_id);
                    table.ForeignKey(
                        name: "items_company_id_fkey",
                        column: x => x.company_id,
                        principalTable: "companies",
                        principalColumn: "company_id");
                    table.ForeignKey(
                        name: "items_item_category_id_fkey",
                        column: x => x.item_category_id,
                        principalTable: "item_categories",
                        principalColumn: "item_category_id");
                    table.ForeignKey(
                        name: "items_item_group_id_fkey",
                        column: x => x.item_group_id,
                        principalTable: "item_groups",
                        principalColumn: "item_group_id");
                });

            migrationBuilder.CreateTable(
                name: "accounts",
                columns: table => new
                {
                    account_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    company_id = table.Column<long>(type: "bigint", nullable: false),
                    code = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    name = table.Column<string>(type: "character varying(80)", maxLength: 80, nullable: false),
                    account_group_id = table.Column<long>(type: "bigint", nullable: false),
                    region_id = table.Column<long>(type: "bigint", nullable: true),
                    salesman_id = table.Column<long>(type: "bigint", nullable: true),
                    address1 = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    address2 = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    city = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    pin_code = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true),
                    phone = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    mobile = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: true),
                    email = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    pan_no = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: true),
                    commission_pct = table.Column<decimal>(type: "numeric(6,3)", precision: 6, scale: 3, nullable: true, defaultValueSql: "0"),
                    vatav_pct = table.Column<decimal>(type: "numeric(6,3)", precision: 6, scale: 3, nullable: true, defaultValueSql: "0"),
                    credit_days = table.Column<int>(type: "integer", nullable: true),
                    credit_limit = table.Column<decimal>(type: "numeric(14,2)", precision: 14, scale: 2, nullable: true),
                    interest_pct = table.Column<decimal>(type: "numeric(6,3)", precision: 6, scale: 3, nullable: true, defaultValueSql: "0"),
                    tds_pct = table.Column<decimal>(type: "numeric(6,3)", precision: 6, scale: 3, nullable: true),
                    tds_threshold = table.Column<decimal>(type: "numeric(14,2)", precision: 14, scale: 2, nullable: true),
                    bank_name = table.Column<string>(type: "character varying(60)", maxLength: 60, nullable: true),
                    bank_branch = table.Column<string>(type: "character varying(60)", maxLength: 60, nullable: true),
                    bank_account_no = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: true),
                    bank_ifsc = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: true),
                    is_apmc = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    is_blocked = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    default_purchase_mode = table.Column<PurchaseMode>(type: "purchase_mode", nullable: true),
                    remarks = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    merged_into = table.Column<long>(type: "bigint", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("accounts_pkey", x => x.account_id);
                    table.ForeignKey(
                        name: "accounts_account_group_id_fkey",
                        column: x => x.account_group_id,
                        principalTable: "account_groups",
                        principalColumn: "account_group_id");
                    table.ForeignKey(
                        name: "accounts_company_id_fkey",
                        column: x => x.company_id,
                        principalTable: "companies",
                        principalColumn: "company_id");
                    table.ForeignKey(
                        name: "accounts_merged_into_fkey",
                        column: x => x.merged_into,
                        principalTable: "accounts",
                        principalColumn: "account_id");
                    table.ForeignKey(
                        name: "accounts_region_id_fkey",
                        column: x => x.region_id,
                        principalTable: "regions",
                        principalColumn: "region_id");
                    table.ForeignKey(
                        name: "accounts_salesman_id_fkey",
                        column: x => x.salesman_id,
                        principalTable: "salesmen",
                        principalColumn: "salesman_id");
                });

            migrationBuilder.CreateTable(
                name: "account_opening_balances",
                columns: table => new
                {
                    account_id = table.Column<long>(type: "bigint", nullable: false),
                    financial_year_id = table.Column<long>(type: "bigint", nullable: false),
                    amount = table.Column<decimal>(type: "numeric(14,2)", precision: 14, scale: 2, nullable: false),
                    side = table.Column<DrCr>(type: "dr_cr", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("account_opening_balances_pkey", x => new { x.account_id, x.financial_year_id });
                    table.ForeignKey(
                        name: "account_opening_balances_account_id_fkey",
                        column: x => x.account_id,
                        principalTable: "accounts",
                        principalColumn: "account_id");
                    table.ForeignKey(
                        name: "account_opening_balances_financial_year_id_fkey",
                        column: x => x.financial_year_id,
                        principalTable: "financial_years",
                        principalColumn: "financial_year_id");
                });

            migrationBuilder.CreateTable(
                name: "cold_storage_transactions",
                columns: table => new
                {
                    cold_txn_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    financial_year_id = table.Column<long>(type: "bigint", nullable: false),
                    txn_no = table.Column<int>(type: "integer", nullable: false),
                    txn_date = table.Column<DateOnly>(type: "date", nullable: false),
                    cold_store_id = table.Column<long>(type: "bigint", nullable: false),
                    party_id = table.Column<long>(type: "bigint", nullable: true),
                    txn_type = table.Column<ColdTxnType>(type: "cold_txn_type", nullable: false),
                    remarks = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    created_by = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("cold_storage_transactions_pkey", x => x.cold_txn_id);
                    table.ForeignKey(
                        name: "cold_storage_transactions_cold_store_id_fkey",
                        column: x => x.cold_store_id,
                        principalTable: "accounts",
                        principalColumn: "account_id");
                    table.ForeignKey(
                        name: "cold_storage_transactions_created_by_fkey",
                        column: x => x.created_by,
                        principalTable: "users",
                        principalColumn: "user_id");
                    table.ForeignKey(
                        name: "cold_storage_transactions_financial_year_id_fkey",
                        column: x => x.financial_year_id,
                        principalTable: "financial_years",
                        principalColumn: "financial_year_id");
                    table.ForeignKey(
                        name: "cold_storage_transactions_party_id_fkey",
                        column: x => x.party_id,
                        principalTable: "accounts",
                        principalColumn: "account_id");
                });

            migrationBuilder.CreateTable(
                name: "crate_transactions",
                columns: table => new
                {
                    crate_txn_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    financial_year_id = table.Column<long>(type: "bigint", nullable: false),
                    txn_no = table.Column<int>(type: "integer", nullable: false),
                    txn_date = table.Column<DateOnly>(type: "date", nullable: false),
                    account_id = table.Column<long>(type: "bigint", nullable: false),
                    txn_type = table.Column<CrateTxnType>(type: "crate_txn_type", nullable: false),
                    truck_no = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    remarks = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    created_by = table.Column<long>(type: "bigint", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("crate_transactions_pkey", x => x.crate_txn_id);
                    table.ForeignKey(
                        name: "crate_transactions_account_id_fkey",
                        column: x => x.account_id,
                        principalTable: "accounts",
                        principalColumn: "account_id");
                    table.ForeignKey(
                        name: "crate_transactions_created_by_fkey",
                        column: x => x.created_by,
                        principalTable: "users",
                        principalColumn: "user_id");
                    table.ForeignKey(
                        name: "crate_transactions_financial_year_id_fkey",
                        column: x => x.financial_year_id,
                        principalTable: "financial_years",
                        principalColumn: "financial_year_id");
                });

            migrationBuilder.CreateTable(
                name: "daybooks",
                columns: table => new
                {
                    daybook_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    company_id = table.Column<long>(type: "bigint", nullable: false),
                    name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    book_type = table.Column<char>(type: "character(1)", maxLength: 1, nullable: false),
                    linked_account_id = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("daybooks_pkey", x => x.daybook_id);
                    table.ForeignKey(
                        name: "daybooks_company_id_fkey",
                        column: x => x.company_id,
                        principalTable: "companies",
                        principalColumn: "company_id");
                    table.ForeignKey(
                        name: "daybooks_linked_account_id_fkey",
                        column: x => x.linked_account_id,
                        principalTable: "accounts",
                        principalColumn: "account_id");
                });

            migrationBuilder.CreateTable(
                name: "import_purchases",
                columns: table => new
                {
                    import_purchase_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    financial_year_id = table.Column<long>(type: "bigint", nullable: false),
                    invoice_no = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    invoice_date = table.Column<DateOnly>(type: "date", nullable: false),
                    supplier_id = table.Column<long>(type: "bigint", nullable: false),
                    country = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    bl_no = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: true),
                    total_usd = table.Column<decimal>(type: "numeric(14,2)", precision: 14, scale: 2, nullable: true),
                    exchange_rate = table.Column<decimal>(type: "numeric(10,4)", precision: 10, scale: 4, nullable: true),
                    cif_value = table.Column<decimal>(type: "numeric(14,2)", precision: 14, scale: 2, nullable: true),
                    duty = table.Column<decimal>(type: "numeric(14,2)", precision: 14, scale: 2, nullable: true, defaultValueSql: "0"),
                    clearing = table.Column<decimal>(type: "numeric(14,2)", precision: 14, scale: 2, nullable: true, defaultValueSql: "0"),
                    freight = table.Column<decimal>(type: "numeric(14,2)", precision: 14, scale: 2, nullable: true, defaultValueSql: "0"),
                    handling = table.Column<decimal>(type: "numeric(14,2)", precision: 14, scale: 2, nullable: true, defaultValueSql: "0"),
                    other_charges = table.Column<decimal>(type: "numeric(14,2)", precision: 14, scale: 2, nullable: true, defaultValueSql: "0"),
                    commission = table.Column<decimal>(type: "numeric(14,2)", precision: 14, scale: 2, nullable: true, defaultValueSql: "0"),
                    net_amount = table.Column<decimal>(type: "numeric(14,2)", precision: 14, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("import_purchases_pkey", x => x.import_purchase_id);
                    table.ForeignKey(
                        name: "import_purchases_financial_year_id_fkey",
                        column: x => x.financial_year_id,
                        principalTable: "financial_years",
                        principalColumn: "financial_year_id");
                    table.ForeignKey(
                        name: "import_purchases_supplier_id_fkey",
                        column: x => x.supplier_id,
                        principalTable: "accounts",
                        principalColumn: "account_id");
                });

            migrationBuilder.CreateTable(
                name: "journal_voucher_lines",
                columns: table => new
                {
                    journal_voucher_line_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    journal_voucher_id = table.Column<long>(type: "bigint", nullable: false),
                    account_id = table.Column<long>(type: "bigint", nullable: false),
                    debit = table.Column<decimal>(type: "numeric(14,2)", precision: 14, scale: 2, nullable: false),
                    credit = table.Column<decimal>(type: "numeric(14,2)", precision: 14, scale: 2, nullable: false),
                    narration = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("journal_voucher_lines_pkey", x => x.journal_voucher_line_id);
                    table.ForeignKey(
                        name: "journal_voucher_lines_account_id_fkey",
                        column: x => x.account_id,
                        principalTable: "accounts",
                        principalColumn: "account_id");
                    table.ForeignKey(
                        name: "journal_voucher_lines_journal_voucher_id_fkey",
                        column: x => x.journal_voucher_id,
                        principalTable: "journal_vouchers",
                        principalColumn: "journal_voucher_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ledger_entries",
                columns: table => new
                {
                    ledger_entry_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    financial_year_id = table.Column<long>(type: "bigint", nullable: false),
                    entry_date = table.Column<DateOnly>(type: "date", nullable: false),
                    account_id = table.Column<long>(type: "bigint", nullable: false),
                    debit = table.Column<decimal>(type: "numeric(14,2)", precision: 14, scale: 2, nullable: false),
                    credit = table.Column<decimal>(type: "numeric(14,2)", precision: 14, scale: 2, nullable: false),
                    voucher_id = table.Column<long>(type: "bigint", nullable: false),
                    voucher_type = table.Column<VoucherType>(type: "voucher_type", nullable: false),
                    bill_no = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    narration = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    cheque_no = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    contra_account_id = table.Column<long>(type: "bigint", nullable: true),
                    quantity = table.Column<decimal>(type: "numeric(12,3)", precision: 12, scale: 3, nullable: true),
                    clearance_date = table.Column<DateOnly>(type: "date", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("ledger_entries_pkey", x => x.ledger_entry_id);
                    table.ForeignKey(
                        name: "ledger_entries_account_id_fkey",
                        column: x => x.account_id,
                        principalTable: "accounts",
                        principalColumn: "account_id");
                    table.ForeignKey(
                        name: "ledger_entries_contra_account_id_fkey",
                        column: x => x.contra_account_id,
                        principalTable: "accounts",
                        principalColumn: "account_id");
                    table.ForeignKey(
                        name: "ledger_entries_financial_year_id_fkey",
                        column: x => x.financial_year_id,
                        principalTable: "financial_years",
                        principalColumn: "financial_year_id");
                });

            migrationBuilder.CreateTable(
                name: "lots",
                columns: table => new
                {
                    lot_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    financial_year_id = table.Column<long>(type: "bigint", nullable: false),
                    lot_no = table.Column<int>(type: "integer", nullable: false),
                    supplier_id = table.Column<long>(type: "bigint", nullable: false),
                    item_id = table.Column<long>(type: "bigint", nullable: false),
                    received_date = table.Column<DateOnly>(type: "date", nullable: false),
                    parent_lot_id = table.Column<long>(type: "bigint", nullable: true),
                    is_closed = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("lots_pkey", x => x.lot_id);
                    table.ForeignKey(
                        name: "lots_financial_year_id_fkey",
                        column: x => x.financial_year_id,
                        principalTable: "financial_years",
                        principalColumn: "financial_year_id");
                    table.ForeignKey(
                        name: "lots_item_id_fkey",
                        column: x => x.item_id,
                        principalTable: "items",
                        principalColumn: "item_id");
                    table.ForeignKey(
                        name: "lots_parent_lot_id_fkey",
                        column: x => x.parent_lot_id,
                        principalTable: "lots",
                        principalColumn: "lot_id");
                    table.ForeignKey(
                        name: "lots_supplier_id_fkey",
                        column: x => x.supplier_id,
                        principalTable: "accounts",
                        principalColumn: "account_id");
                });

            migrationBuilder.CreateTable(
                name: "purchase_bills",
                columns: table => new
                {
                    purchase_bill_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    financial_year_id = table.Column<long>(type: "bigint", nullable: false),
                    bill_no = table.Column<int>(type: "integer", nullable: false),
                    bill_date = table.Column<DateOnly>(type: "date", nullable: false),
                    supplier_id = table.Column<long>(type: "bigint", nullable: false),
                    mode = table.Column<PurchaseMode>(type: "purchase_mode", nullable: false),
                    challan_no = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: true),
                    truck_no = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    mark = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: true),
                    delivery_person = table.Column<string>(type: "character varying(60)", maxLength: 60, nullable: true),
                    gross_amount = table.Column<decimal>(type: "numeric(14,2)", precision: 14, scale: 2, nullable: false),
                    commission_pct = table.Column<decimal>(type: "numeric(6,3)", precision: 6, scale: 3, nullable: false),
                    commission_amount = table.Column<decimal>(type: "numeric(14,2)", precision: 14, scale: 2, nullable: false),
                    market_fee_pct = table.Column<decimal>(type: "numeric(6,3)", precision: 6, scale: 3, nullable: true, defaultValueSql: "0"),
                    market_fee = table.Column<decimal>(type: "numeric(14,2)", precision: 14, scale: 2, nullable: true, defaultValueSql: "0"),
                    freight = table.Column<decimal>(type: "numeric(14,2)", precision: 14, scale: 2, nullable: true, defaultValueSql: "0"),
                    labour = table.Column<decimal>(type: "numeric(14,2)", precision: 14, scale: 2, nullable: true, defaultValueSql: "0"),
                    postage = table.Column<decimal>(type: "numeric(14,2)", precision: 14, scale: 2, nullable: true, defaultValueSql: "0"),
                    packing_material = table.Column<decimal>(type: "numeric(14,2)", precision: 14, scale: 2, nullable: true, defaultValueSql: "0"),
                    cold_store = table.Column<decimal>(type: "numeric(14,2)", precision: 14, scale: 2, nullable: true, defaultValueSql: "0"),
                    vatav = table.Column<decimal>(type: "numeric(14,2)", precision: 14, scale: 2, nullable: true, defaultValueSql: "0"),
                    amanat = table.Column<decimal>(type: "numeric(14,2)", precision: 14, scale: 2, nullable: true, defaultValueSql: "0"),
                    dd_charge = table.Column<decimal>(type: "numeric(14,2)", precision: 14, scale: 2, nullable: true, defaultValueSql: "0"),
                    inam = table.Column<decimal>(type: "numeric(14,2)", precision: 14, scale: 2, nullable: true, defaultValueSql: "0"),
                    other_deduction = table.Column<decimal>(type: "numeric(14,2)", precision: 14, scale: 2, nullable: true, defaultValueSql: "0"),
                    net_amount = table.Column<decimal>(type: "numeric(14,2)", precision: 14, scale: 2, nullable: false),
                    our_freight = table.Column<decimal>(type: "numeric(14,2)", precision: 14, scale: 2, nullable: true, defaultValueSql: "0"),
                    our_labour = table.Column<decimal>(type: "numeric(14,2)", precision: 14, scale: 2, nullable: true, defaultValueSql: "0"),
                    our_market_fee = table.Column<decimal>(type: "numeric(14,2)", precision: 14, scale: 2, nullable: true, defaultValueSql: "0"),
                    our_other = table.Column<decimal>(type: "numeric(14,2)", precision: 14, scale: 2, nullable: true, defaultValueSql: "0"),
                    tds_pct = table.Column<decimal>(type: "numeric(6,3)", precision: 6, scale: 3, nullable: true, defaultValueSql: "0"),
                    tds_amount = table.Column<decimal>(type: "numeric(14,2)", precision: 14, scale: 2, nullable: true, defaultValueSql: "0"),
                    remarks = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    authorised_by = table.Column<string>(type: "character varying(60)", maxLength: 60, nullable: true),
                    created_by = table.Column<long>(type: "bigint", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("purchase_bills_pkey", x => x.purchase_bill_id);
                    table.ForeignKey(
                        name: "purchase_bills_created_by_fkey",
                        column: x => x.created_by,
                        principalTable: "users",
                        principalColumn: "user_id");
                    table.ForeignKey(
                        name: "purchase_bills_financial_year_id_fkey",
                        column: x => x.financial_year_id,
                        principalTable: "financial_years",
                        principalColumn: "financial_year_id");
                    table.ForeignKey(
                        name: "purchase_bills_supplier_id_fkey",
                        column: x => x.supplier_id,
                        principalTable: "accounts",
                        principalColumn: "account_id");
                });

            migrationBuilder.CreateTable(
                name: "sales_bills",
                columns: table => new
                {
                    sales_bill_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    financial_year_id = table.Column<long>(type: "bigint", nullable: false),
                    bill_no = table.Column<int>(type: "integer", nullable: false),
                    bill_date = table.Column<DateOnly>(type: "date", nullable: false),
                    buyer_id = table.Column<long>(type: "bigint", nullable: false),
                    is_weekly = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    period_from = table.Column<DateOnly>(type: "date", nullable: true),
                    period_to = table.Column<DateOnly>(type: "date", nullable: true),
                    total_amount = table.Column<decimal>(type: "numeric(14,2)", precision: 14, scale: 2, nullable: false),
                    net_amount = table.Column<decimal>(type: "numeric(14,2)", precision: 14, scale: 2, nullable: false),
                    created_by = table.Column<long>(type: "bigint", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("sales_bills_pkey", x => x.sales_bill_id);
                    table.ForeignKey(
                        name: "sales_bills_buyer_id_fkey",
                        column: x => x.buyer_id,
                        principalTable: "accounts",
                        principalColumn: "account_id");
                    table.ForeignKey(
                        name: "sales_bills_created_by_fkey",
                        column: x => x.created_by,
                        principalTable: "users",
                        principalColumn: "user_id");
                    table.ForeignKey(
                        name: "sales_bills_financial_year_id_fkey",
                        column: x => x.financial_year_id,
                        principalTable: "financial_years",
                        principalColumn: "financial_year_id");
                });

            migrationBuilder.CreateTable(
                name: "whatsapp_dispatches",
                columns: table => new
                {
                    dispatch_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    account_id = table.Column<long>(type: "bigint", nullable: false),
                    mobile = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: false),
                    document_type = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    period_from = table.Column<DateOnly>(type: "date", nullable: true),
                    period_to = table.Column<DateOnly>(type: "date", nullable: true),
                    pdf_path = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    wa_message_id = table.Column<string>(type: "character varying(80)", maxLength: 80, nullable: true),
                    status = table.Column<DispatchStatus>(type: "dispatch_status", nullable: false),
                    status_updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    error_detail = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    sent_by = table.Column<long>(type: "bigint", nullable: true),
                    sent_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("whatsapp_dispatches_pkey", x => x.dispatch_id);
                    table.ForeignKey(
                        name: "whatsapp_dispatches_account_id_fkey",
                        column: x => x.account_id,
                        principalTable: "accounts",
                        principalColumn: "account_id");
                    table.ForeignKey(
                        name: "whatsapp_dispatches_sent_by_fkey",
                        column: x => x.sent_by,
                        principalTable: "users",
                        principalColumn: "user_id");
                });

            migrationBuilder.CreateTable(
                name: "crate_transaction_items",
                columns: table => new
                {
                    crate_txn_item_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    crate_txn_id = table.Column<long>(type: "bigint", nullable: false),
                    crate_type = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    quantity = table.Column<decimal>(type: "numeric(10)", precision: 10, nullable: false),
                    rate = table.Column<decimal>(type: "numeric(10,2)", precision: 10, scale: 2, nullable: true, defaultValueSql: "0"),
                    amount = table.Column<decimal>(type: "numeric(14,2)", precision: 14, scale: 2, nullable: true, defaultValueSql: "0")
                },
                constraints: table =>
                {
                    table.PrimaryKey("crate_transaction_items_pkey", x => x.crate_txn_item_id);
                    table.ForeignKey(
                        name: "crate_transaction_items_crate_txn_id_fkey",
                        column: x => x.crate_txn_id,
                        principalTable: "crate_transactions",
                        principalColumn: "crate_txn_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "bank_entries",
                columns: table => new
                {
                    bank_entry_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    financial_year_id = table.Column<long>(type: "bigint", nullable: false),
                    entry_no = table.Column<int>(type: "integer", nullable: false),
                    entry_date = table.Column<DateOnly>(type: "date", nullable: false),
                    daybook_id = table.Column<long>(type: "bigint", nullable: false),
                    account_id = table.Column<long>(type: "bigint", nullable: false),
                    amount = table.Column<decimal>(type: "numeric(14,2)", precision: 14, scale: 2, nullable: false),
                    side = table.Column<DrCr>(type: "dr_cr", nullable: false),
                    cheque_no = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    remarks = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    created_by = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("bank_entries_pkey", x => x.bank_entry_id);
                    table.ForeignKey(
                        name: "bank_entries_account_id_fkey",
                        column: x => x.account_id,
                        principalTable: "accounts",
                        principalColumn: "account_id");
                    table.ForeignKey(
                        name: "bank_entries_created_by_fkey",
                        column: x => x.created_by,
                        principalTable: "users",
                        principalColumn: "user_id");
                    table.ForeignKey(
                        name: "bank_entries_daybook_id_fkey",
                        column: x => x.daybook_id,
                        principalTable: "daybooks",
                        principalColumn: "daybook_id");
                    table.ForeignKey(
                        name: "bank_entries_financial_year_id_fkey",
                        column: x => x.financial_year_id,
                        principalTable: "financial_years",
                        principalColumn: "financial_year_id");
                });

            migrationBuilder.CreateTable(
                name: "payments",
                columns: table => new
                {
                    payment_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    financial_year_id = table.Column<long>(type: "bigint", nullable: false),
                    payment_no = table.Column<int>(type: "integer", nullable: false),
                    payment_date = table.Column<DateOnly>(type: "date", nullable: false),
                    account_id = table.Column<long>(type: "bigint", nullable: false),
                    daybook_id = table.Column<long>(type: "bigint", nullable: false),
                    mode = table.Column<PaymentMode>(type: "payment_mode", nullable: false),
                    cheque_no = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    cheque_date = table.Column<DateOnly>(type: "date", nullable: true),
                    bank_name = table.Column<string>(type: "character varying(60)", maxLength: 60, nullable: true),
                    bank_branch = table.Column<string>(type: "character varying(60)", maxLength: 60, nullable: true),
                    amount = table.Column<decimal>(type: "numeric(14,2)", precision: 14, scale: 2, nullable: false),
                    tds_amount = table.Column<decimal>(type: "numeric(14,2)", precision: 14, scale: 2, nullable: false),
                    vatav = table.Column<decimal>(type: "numeric(14,2)", precision: 14, scale: 2, nullable: false),
                    hamali = table.Column<decimal>(type: "numeric(14,2)", precision: 14, scale: 2, nullable: false),
                    discount = table.Column<decimal>(type: "numeric(14,2)", precision: 14, scale: 2, nullable: false),
                    advance = table.Column<decimal>(type: "numeric(14,2)", precision: 14, scale: 2, nullable: false),
                    total_settled = table.Column<decimal>(type: "numeric(14,2)", precision: 14, scale: 2, nullable: false),
                    is_freight_payment = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    clearance_date = table.Column<DateOnly>(type: "date", nullable: true),
                    is_returned = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    returned_date = table.Column<DateOnly>(type: "date", nullable: true),
                    remarks = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    created_by = table.Column<long>(type: "bigint", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("payments_pkey", x => x.payment_id);
                    table.ForeignKey(
                        name: "payments_account_id_fkey",
                        column: x => x.account_id,
                        principalTable: "accounts",
                        principalColumn: "account_id");
                    table.ForeignKey(
                        name: "payments_created_by_fkey",
                        column: x => x.created_by,
                        principalTable: "users",
                        principalColumn: "user_id");
                    table.ForeignKey(
                        name: "payments_daybook_id_fkey",
                        column: x => x.daybook_id,
                        principalTable: "daybooks",
                        principalColumn: "daybook_id");
                    table.ForeignKey(
                        name: "payments_financial_year_id_fkey",
                        column: x => x.financial_year_id,
                        principalTable: "financial_years",
                        principalColumn: "financial_year_id");
                });

            migrationBuilder.CreateTable(
                name: "receipts",
                columns: table => new
                {
                    receipt_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    financial_year_id = table.Column<long>(type: "bigint", nullable: false),
                    receipt_no = table.Column<int>(type: "integer", nullable: false),
                    receipt_date = table.Column<DateOnly>(type: "date", nullable: false),
                    account_id = table.Column<long>(type: "bigint", nullable: false),
                    daybook_id = table.Column<long>(type: "bigint", nullable: false),
                    mode = table.Column<PaymentMode>(type: "payment_mode", nullable: false),
                    cheque_no = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    cheque_date = table.Column<DateOnly>(type: "date", nullable: true),
                    bank_name = table.Column<string>(type: "character varying(60)", maxLength: 60, nullable: true),
                    bank_branch = table.Column<string>(type: "character varying(60)", maxLength: 60, nullable: true),
                    amount = table.Column<decimal>(type: "numeric(14,2)", precision: 14, scale: 2, nullable: false),
                    vatav = table.Column<decimal>(type: "numeric(14,2)", precision: 14, scale: 2, nullable: false),
                    rounding_diff = table.Column<decimal>(type: "numeric(8,2)", precision: 8, scale: 2, nullable: false),
                    total_settled = table.Column<decimal>(type: "numeric(14,2)", precision: 14, scale: 2, nullable: false),
                    clearance_date = table.Column<DateOnly>(type: "date", nullable: true),
                    is_returned = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    returned_date = table.Column<DateOnly>(type: "date", nullable: true),
                    remarks = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    created_by = table.Column<long>(type: "bigint", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("receipts_pkey", x => x.receipt_id);
                    table.ForeignKey(
                        name: "receipts_account_id_fkey",
                        column: x => x.account_id,
                        principalTable: "accounts",
                        principalColumn: "account_id");
                    table.ForeignKey(
                        name: "receipts_created_by_fkey",
                        column: x => x.created_by,
                        principalTable: "users",
                        principalColumn: "user_id");
                    table.ForeignKey(
                        name: "receipts_daybook_id_fkey",
                        column: x => x.daybook_id,
                        principalTable: "daybooks",
                        principalColumn: "daybook_id");
                    table.ForeignKey(
                        name: "receipts_financial_year_id_fkey",
                        column: x => x.financial_year_id,
                        principalTable: "financial_years",
                        principalColumn: "financial_year_id");
                });

            migrationBuilder.CreateTable(
                name: "bank_statement_lines",
                columns: table => new
                {
                    statement_line_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    daybook_id = table.Column<long>(type: "bigint", nullable: false),
                    statement_date = table.Column<DateOnly>(type: "date", nullable: false),
                    description = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    reference = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    debit = table.Column<decimal>(type: "numeric(14,2)", precision: 14, scale: 2, nullable: false),
                    credit = table.Column<decimal>(type: "numeric(14,2)", precision: 14, scale: 2, nullable: false),
                    imported_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    matched_ledger_entry_id = table.Column<long>(type: "bigint", nullable: true),
                    reconciled = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    reconciled_by = table.Column<long>(type: "bigint", nullable: true),
                    reconciled_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("bank_statement_lines_pkey", x => x.statement_line_id);
                    table.ForeignKey(
                        name: "bank_statement_lines_daybook_id_fkey",
                        column: x => x.daybook_id,
                        principalTable: "daybooks",
                        principalColumn: "daybook_id");
                    table.ForeignKey(
                        name: "bank_statement_lines_matched_ledger_entry_id_fkey",
                        column: x => x.matched_ledger_entry_id,
                        principalTable: "ledger_entries",
                        principalColumn: "ledger_entry_id");
                    table.ForeignKey(
                        name: "bank_statement_lines_reconciled_by_fkey",
                        column: x => x.reconciled_by,
                        principalTable: "users",
                        principalColumn: "user_id");
                });

            migrationBuilder.CreateTable(
                name: "cold_storage_transaction_items",
                columns: table => new
                {
                    cold_txn_item_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    cold_txn_id = table.Column<long>(type: "bigint", nullable: false),
                    lot_id = table.Column<long>(type: "bigint", nullable: true),
                    cold_lot_no = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    item_id = table.Column<long>(type: "bigint", nullable: false),
                    quantity = table.Column<decimal>(type: "numeric(12,3)", precision: 12, scale: 3, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("cold_storage_transaction_items_pkey", x => x.cold_txn_item_id);
                    table.ForeignKey(
                        name: "cold_storage_transaction_items_cold_txn_id_fkey",
                        column: x => x.cold_txn_id,
                        principalTable: "cold_storage_transactions",
                        principalColumn: "cold_txn_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "cold_storage_transaction_items_item_id_fkey",
                        column: x => x.item_id,
                        principalTable: "items",
                        principalColumn: "item_id");
                    table.ForeignKey(
                        name: "cold_storage_transaction_items_lot_id_fkey",
                        column: x => x.lot_id,
                        principalTable: "lots",
                        principalColumn: "lot_id");
                });

            migrationBuilder.CreateTable(
                name: "desavar_purchases",
                columns: table => new
                {
                    desavar_purchase_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    financial_year_id = table.Column<long>(type: "bigint", nullable: false),
                    bill_no = table.Column<int>(type: "integer", nullable: false),
                    bill_date = table.Column<DateOnly>(type: "date", nullable: false),
                    origin = table.Column<string>(type: "character varying(60)", maxLength: 60, nullable: true),
                    supplier_id = table.Column<long>(type: "bigint", nullable: false),
                    item_id = table.Column<long>(type: "bigint", nullable: false),
                    lot_id = table.Column<long>(type: "bigint", nullable: true),
                    quantity = table.Column<decimal>(type: "numeric(12,3)", precision: 12, scale: 3, nullable: false),
                    rate = table.Column<decimal>(type: "numeric(12,2)", precision: 12, scale: 2, nullable: false),
                    freight = table.Column<decimal>(type: "numeric(14,2)", precision: 14, scale: 2, nullable: true, defaultValueSql: "0"),
                    other_charges = table.Column<decimal>(type: "numeric(14,2)", precision: 14, scale: 2, nullable: true, defaultValueSql: "0"),
                    net_amount = table.Column<decimal>(type: "numeric(14,2)", precision: 14, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("desavar_purchases_pkey", x => x.desavar_purchase_id);
                    table.ForeignKey(
                        name: "desavar_purchases_financial_year_id_fkey",
                        column: x => x.financial_year_id,
                        principalTable: "financial_years",
                        principalColumn: "financial_year_id");
                    table.ForeignKey(
                        name: "desavar_purchases_item_id_fkey",
                        column: x => x.item_id,
                        principalTable: "items",
                        principalColumn: "item_id");
                    table.ForeignKey(
                        name: "desavar_purchases_lot_id_fkey",
                        column: x => x.lot_id,
                        principalTable: "lots",
                        principalColumn: "lot_id");
                    table.ForeignKey(
                        name: "desavar_purchases_supplier_id_fkey",
                        column: x => x.supplier_id,
                        principalTable: "accounts",
                        principalColumn: "account_id");
                });

            migrationBuilder.CreateTable(
                name: "import_purchase_items",
                columns: table => new
                {
                    import_purchase_item_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    import_purchase_id = table.Column<long>(type: "bigint", nullable: false),
                    lot_id = table.Column<long>(type: "bigint", nullable: true),
                    item_id = table.Column<long>(type: "bigint", nullable: false),
                    count_id = table.Column<long>(type: "bigint", nullable: true),
                    quantity = table.Column<decimal>(type: "numeric(12,3)", precision: 12, scale: 3, nullable: false),
                    rate_usd = table.Column<decimal>(type: "numeric(12,2)", precision: 12, scale: 2, nullable: true),
                    landed_rate = table.Column<decimal>(type: "numeric(12,2)", precision: 12, scale: 2, nullable: true),
                    amount = table.Column<decimal>(type: "numeric(14,2)", precision: 14, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("import_purchase_items_pkey", x => x.import_purchase_item_id);
                    table.ForeignKey(
                        name: "import_purchase_items_count_id_fkey",
                        column: x => x.count_id,
                        principalTable: "item_counts",
                        principalColumn: "item_count_id");
                    table.ForeignKey(
                        name: "import_purchase_items_import_purchase_id_fkey",
                        column: x => x.import_purchase_id,
                        principalTable: "import_purchases",
                        principalColumn: "import_purchase_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "import_purchase_items_item_id_fkey",
                        column: x => x.item_id,
                        principalTable: "items",
                        principalColumn: "item_id");
                    table.ForeignKey(
                        name: "import_purchase_items_lot_id_fkey",
                        column: x => x.lot_id,
                        principalTable: "lots",
                        principalColumn: "lot_id");
                });

            migrationBuilder.CreateTable(
                name: "import_sales",
                columns: table => new
                {
                    import_sale_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    financial_year_id = table.Column<long>(type: "bigint", nullable: false),
                    sale_date = table.Column<DateOnly>(type: "date", nullable: false),
                    lot_id = table.Column<long>(type: "bigint", nullable: true),
                    buyer_id = table.Column<long>(type: "bigint", nullable: false),
                    item_id = table.Column<long>(type: "bigint", nullable: false),
                    quantity = table.Column<decimal>(type: "numeric(12,3)", precision: 12, scale: 3, nullable: false),
                    rate = table.Column<decimal>(type: "numeric(12,2)", precision: 12, scale: 2, nullable: false),
                    amount = table.Column<decimal>(type: "numeric(14,2)", precision: 14, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("import_sales_pkey", x => x.import_sale_id);
                    table.ForeignKey(
                        name: "import_sales_buyer_id_fkey",
                        column: x => x.buyer_id,
                        principalTable: "accounts",
                        principalColumn: "account_id");
                    table.ForeignKey(
                        name: "import_sales_financial_year_id_fkey",
                        column: x => x.financial_year_id,
                        principalTable: "financial_years",
                        principalColumn: "financial_year_id");
                    table.ForeignKey(
                        name: "import_sales_item_id_fkey",
                        column: x => x.item_id,
                        principalTable: "items",
                        principalColumn: "item_id");
                    table.ForeignKey(
                        name: "import_sales_lot_id_fkey",
                        column: x => x.lot_id,
                        principalTable: "lots",
                        principalColumn: "lot_id");
                });

            migrationBuilder.CreateTable(
                name: "lot_operations",
                columns: table => new
                {
                    lot_operation_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    financial_year_id = table.Column<long>(type: "bigint", nullable: false),
                    op_date = table.Column<DateOnly>(type: "date", nullable: false),
                    op_type = table.Column<LotOpType>(type: "lot_op_type", nullable: false),
                    source_lot_id = table.Column<long>(type: "bigint", nullable: false),
                    target_lot_id = table.Column<long>(type: "bigint", nullable: true),
                    target_account_id = table.Column<long>(type: "bigint", nullable: true),
                    quantity = table.Column<decimal>(type: "numeric(12,3)", precision: 12, scale: 3, nullable: true),
                    remarks = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    created_by = table.Column<long>(type: "bigint", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("lot_operations_pkey", x => x.lot_operation_id);
                    table.ForeignKey(
                        name: "lot_operations_created_by_fkey",
                        column: x => x.created_by,
                        principalTable: "users",
                        principalColumn: "user_id");
                    table.ForeignKey(
                        name: "lot_operations_financial_year_id_fkey",
                        column: x => x.financial_year_id,
                        principalTable: "financial_years",
                        principalColumn: "financial_year_id");
                    table.ForeignKey(
                        name: "lot_operations_source_lot_id_fkey",
                        column: x => x.source_lot_id,
                        principalTable: "lots",
                        principalColumn: "lot_id");
                    table.ForeignKey(
                        name: "lot_operations_target_account_id_fkey",
                        column: x => x.target_account_id,
                        principalTable: "accounts",
                        principalColumn: "account_id");
                    table.ForeignKey(
                        name: "lot_operations_target_lot_id_fkey",
                        column: x => x.target_lot_id,
                        principalTable: "lots",
                        principalColumn: "lot_id");
                });

            migrationBuilder.CreateTable(
                name: "purchase_bill_items",
                columns: table => new
                {
                    purchase_bill_item_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    purchase_bill_id = table.Column<long>(type: "bigint", nullable: false),
                    lot_id = table.Column<long>(type: "bigint", nullable: false),
                    item_id = table.Column<long>(type: "bigint", nullable: false),
                    description = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    quantity = table.Column<decimal>(type: "numeric(12,3)", precision: 12, scale: 3, nullable: false),
                    weight = table.Column<decimal>(type: "numeric(12,3)", precision: 12, scale: 3, nullable: true),
                    gross_rate = table.Column<decimal>(type: "numeric(12,2)", precision: 12, scale: 2, nullable: false),
                    net_rate = table.Column<decimal>(type: "numeric(12,2)", precision: 12, scale: 2, nullable: false),
                    amount = table.Column<decimal>(type: "numeric(14,2)", precision: 14, scale: 2, nullable: false),
                    crate_info = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("purchase_bill_items_pkey", x => x.purchase_bill_item_id);
                    table.ForeignKey(
                        name: "purchase_bill_items_item_id_fkey",
                        column: x => x.item_id,
                        principalTable: "items",
                        principalColumn: "item_id");
                    table.ForeignKey(
                        name: "purchase_bill_items_lot_id_fkey",
                        column: x => x.lot_id,
                        principalTable: "lots",
                        principalColumn: "lot_id");
                    table.ForeignKey(
                        name: "purchase_bill_items_purchase_bill_id_fkey",
                        column: x => x.purchase_bill_id,
                        principalTable: "purchase_bills",
                        principalColumn: "purchase_bill_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "sales",
                columns: table => new
                {
                    sale_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    financial_year_id = table.Column<long>(type: "bigint", nullable: false),
                    sale_date = table.Column<DateOnly>(type: "date", nullable: false),
                    lot_id = table.Column<long>(type: "bigint", nullable: false),
                    buyer_id = table.Column<long>(type: "bigint", nullable: false),
                    item_id = table.Column<long>(type: "bigint", nullable: false),
                    sales_bill_id = table.Column<long>(type: "bigint", nullable: true),
                    quantity = table.Column<decimal>(type: "numeric(12,3)", precision: 12, scale: 3, nullable: false),
                    weight = table.Column<decimal>(type: "numeric(12,3)", precision: 12, scale: 3, nullable: true),
                    rate = table.Column<decimal>(type: "numeric(12,2)", precision: 12, scale: 2, nullable: false),
                    amount = table.Column<decimal>(type: "numeric(14,2)", precision: 14, scale: 2, nullable: false),
                    market_fee_pct = table.Column<decimal>(type: "numeric(6,3)", precision: 6, scale: 3, nullable: true, defaultValueSql: "0"),
                    market_fee = table.Column<decimal>(type: "numeric(14,2)", precision: 14, scale: 2, nullable: true, defaultValueSql: "0"),
                    labour_rate = table.Column<decimal>(type: "numeric(10,2)", precision: 10, scale: 2, nullable: true, defaultValueSql: "0"),
                    labour = table.Column<decimal>(type: "numeric(14,2)", precision: 14, scale: 2, nullable: true, defaultValueSql: "0"),
                    packing_rate = table.Column<decimal>(type: "numeric(10,2)", precision: 10, scale: 2, nullable: true, defaultValueSql: "0"),
                    packing = table.Column<decimal>(type: "numeric(14,2)", precision: 14, scale: 2, nullable: true, defaultValueSql: "0"),
                    freight = table.Column<decimal>(type: "numeric(14,2)", precision: 14, scale: 2, nullable: true, defaultValueSql: "0"),
                    tcs_pct = table.Column<decimal>(type: "numeric(6,3)", precision: 6, scale: 3, nullable: true, defaultValueSql: "0"),
                    tcs_amount = table.Column<decimal>(type: "numeric(14,2)", precision: 14, scale: 2, nullable: true, defaultValueSql: "0"),
                    delivered_qty = table.Column<decimal>(type: "numeric(12,3)", precision: 12, scale: 3, nullable: true),
                    delivered_to = table.Column<string>(type: "character varying(60)", maxLength: 60, nullable: true),
                    remarks = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    created_by = table.Column<long>(type: "bigint", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("sales_pkey", x => x.sale_id);
                    table.ForeignKey(
                        name: "sales_buyer_id_fkey",
                        column: x => x.buyer_id,
                        principalTable: "accounts",
                        principalColumn: "account_id");
                    table.ForeignKey(
                        name: "sales_created_by_fkey",
                        column: x => x.created_by,
                        principalTable: "users",
                        principalColumn: "user_id");
                    table.ForeignKey(
                        name: "sales_financial_year_id_fkey",
                        column: x => x.financial_year_id,
                        principalTable: "financial_years",
                        principalColumn: "financial_year_id");
                    table.ForeignKey(
                        name: "sales_item_id_fkey",
                        column: x => x.item_id,
                        principalTable: "items",
                        principalColumn: "item_id");
                    table.ForeignKey(
                        name: "sales_lot_id_fkey",
                        column: x => x.lot_id,
                        principalTable: "lots",
                        principalColumn: "lot_id");
                    table.ForeignKey(
                        name: "sales_sales_bill_id_fkey",
                        column: x => x.sales_bill_id,
                        principalTable: "sales_bills",
                        principalColumn: "sales_bill_id");
                });

            migrationBuilder.CreateTable(
                name: "payment_allocations",
                columns: table => new
                {
                    payment_allocation_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    payment_id = table.Column<long>(type: "bigint", nullable: false),
                    purchase_bill_id = table.Column<long>(type: "bigint", nullable: false),
                    allocated_amount = table.Column<decimal>(type: "numeric(14,2)", precision: 14, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("payment_allocations_pkey", x => x.payment_allocation_id);
                    table.ForeignKey(
                        name: "payment_allocations_payment_id_fkey",
                        column: x => x.payment_id,
                        principalTable: "payments",
                        principalColumn: "payment_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "payment_allocations_purchase_bill_id_fkey",
                        column: x => x.purchase_bill_id,
                        principalTable: "purchase_bills",
                        principalColumn: "purchase_bill_id");
                });

            migrationBuilder.CreateTable(
                name: "tds_purchase_deductions",
                columns: table => new
                {
                    tds_deduction_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    financial_year_id = table.Column<long>(type: "bigint", nullable: false),
                    supplier_id = table.Column<long>(type: "bigint", nullable: false),
                    payment_id = table.Column<long>(type: "bigint", nullable: false),
                    cumulative_before = table.Column<decimal>(type: "numeric(14,2)", precision: 14, scale: 2, nullable: false),
                    taxable_excess = table.Column<decimal>(type: "numeric(14,2)", precision: 14, scale: 2, nullable: false),
                    tds_rate = table.Column<decimal>(type: "numeric(6,3)", precision: 6, scale: 3, nullable: false),
                    tds_amount = table.Column<decimal>(type: "numeric(14,2)", precision: 14, scale: 2, nullable: false),
                    deducted_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("tds_purchase_deductions_pkey", x => x.tds_deduction_id);
                    table.ForeignKey(
                        name: "tds_purchase_deductions_financial_year_id_fkey",
                        column: x => x.financial_year_id,
                        principalTable: "financial_years",
                        principalColumn: "financial_year_id");
                    table.ForeignKey(
                        name: "tds_purchase_deductions_payment_id_fkey",
                        column: x => x.payment_id,
                        principalTable: "payments",
                        principalColumn: "payment_id");
                    table.ForeignKey(
                        name: "tds_purchase_deductions_supplier_id_fkey",
                        column: x => x.supplier_id,
                        principalTable: "accounts",
                        principalColumn: "account_id");
                });

            migrationBuilder.CreateTable(
                name: "receipt_allocations",
                columns: table => new
                {
                    receipt_allocation_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    receipt_id = table.Column<long>(type: "bigint", nullable: false),
                    sales_bill_id = table.Column<long>(type: "bigint", nullable: false),
                    allocated_amount = table.Column<decimal>(type: "numeric(14,2)", precision: 14, scale: 2, nullable: false),
                    vatav_amount = table.Column<decimal>(type: "numeric(14,2)", precision: 14, scale: 2, nullable: false),
                    flagged_by_ai = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("receipt_allocations_pkey", x => x.receipt_allocation_id);
                    table.ForeignKey(
                        name: "receipt_allocations_receipt_id_fkey",
                        column: x => x.receipt_id,
                        principalTable: "receipts",
                        principalColumn: "receipt_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "receipt_allocations_sales_bill_id_fkey",
                        column: x => x.sales_bill_id,
                        principalTable: "sales_bills",
                        principalColumn: "sales_bill_id");
                });

            migrationBuilder.CreateTable(
                name: "desavar_sales",
                columns: table => new
                {
                    desavar_sale_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    financial_year_id = table.Column<long>(type: "bigint", nullable: false),
                    sale_date = table.Column<DateOnly>(type: "date", nullable: false),
                    desavar_purchase_id = table.Column<long>(type: "bigint", nullable: true),
                    lot_id = table.Column<long>(type: "bigint", nullable: true),
                    buyer_id = table.Column<long>(type: "bigint", nullable: false),
                    item_id = table.Column<long>(type: "bigint", nullable: false),
                    quantity = table.Column<decimal>(type: "numeric(12,3)", precision: 12, scale: 3, nullable: false),
                    weight = table.Column<decimal>(type: "numeric(12,3)", precision: 12, scale: 3, nullable: true),
                    rate = table.Column<decimal>(type: "numeric(12,2)", precision: 12, scale: 2, nullable: false),
                    purchase_rate = table.Column<decimal>(type: "numeric(12,2)", precision: 12, scale: 2, nullable: true),
                    amount = table.Column<decimal>(type: "numeric(14,2)", precision: 14, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("desavar_sales_pkey", x => x.desavar_sale_id);
                    table.ForeignKey(
                        name: "desavar_sales_buyer_id_fkey",
                        column: x => x.buyer_id,
                        principalTable: "accounts",
                        principalColumn: "account_id");
                    table.ForeignKey(
                        name: "desavar_sales_desavar_purchase_id_fkey",
                        column: x => x.desavar_purchase_id,
                        principalTable: "desavar_purchases",
                        principalColumn: "desavar_purchase_id");
                    table.ForeignKey(
                        name: "desavar_sales_financial_year_id_fkey",
                        column: x => x.financial_year_id,
                        principalTable: "financial_years",
                        principalColumn: "financial_year_id");
                    table.ForeignKey(
                        name: "desavar_sales_item_id_fkey",
                        column: x => x.item_id,
                        principalTable: "items",
                        principalColumn: "item_id");
                    table.ForeignKey(
                        name: "desavar_sales_lot_id_fkey",
                        column: x => x.lot_id,
                        principalTable: "lots",
                        principalColumn: "lot_id");
                });

            migrationBuilder.CreateIndex(
                name: "account_groups_company_id_name_key",
                table: "account_groups",
                columns: new[] { "company_id", "name" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_account_groups_parent_id",
                table: "account_groups",
                column: "parent_id");

            migrationBuilder.CreateIndex(
                name: "IX_account_opening_balances_financial_year_id",
                table: "account_opening_balances",
                column: "financial_year_id");

            migrationBuilder.CreateIndex(
                name: "accounts_company_id_code_key",
                table: "accounts",
                columns: new[] { "company_id", "code" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "idx_accounts_group",
                table: "accounts",
                column: "account_group_id");

            migrationBuilder.CreateIndex(
                name: "idx_accounts_name",
                table: "accounts",
                columns: new[] { "company_id", "name" });

            migrationBuilder.CreateIndex(
                name: "idx_accounts_region",
                table: "accounts",
                column: "region_id");

            migrationBuilder.CreateIndex(
                name: "IX_accounts_merged_into",
                table: "accounts",
                column: "merged_into");

            migrationBuilder.CreateIndex(
                name: "IX_accounts_salesman_id",
                table: "accounts",
                column: "salesman_id");

            migrationBuilder.CreateIndex(
                name: "IX_attachments_uploaded_by",
                table: "attachments",
                column: "uploaded_by");

            migrationBuilder.CreateIndex(
                name: "idx_audit_log_occurred",
                table: "audit_log",
                column: "occurred_at");

            migrationBuilder.CreateIndex(
                name: "idx_audit_log_table_record",
                table: "audit_log",
                columns: new[] { "table_name", "record_pk" });

            migrationBuilder.CreateIndex(
                name: "IX_audit_log_company_id",
                table: "audit_log",
                column: "company_id");

            migrationBuilder.CreateIndex(
                name: "IX_audit_log_user_id",
                table: "audit_log",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "bank_entries_financial_year_id_entry_no_key",
                table: "bank_entries",
                columns: new[] { "financial_year_id", "entry_no" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_bank_entries_account_id",
                table: "bank_entries",
                column: "account_id");

            migrationBuilder.CreateIndex(
                name: "IX_bank_entries_created_by",
                table: "bank_entries",
                column: "created_by");

            migrationBuilder.CreateIndex(
                name: "IX_bank_entries_daybook_id",
                table: "bank_entries",
                column: "daybook_id");

            migrationBuilder.CreateIndex(
                name: "idx_stmt_daybook_date",
                table: "bank_statement_lines",
                columns: new[] { "daybook_id", "statement_date" });

            migrationBuilder.CreateIndex(
                name: "IX_bank_statement_lines_matched_ledger_entry_id",
                table: "bank_statement_lines",
                column: "matched_ledger_entry_id");

            migrationBuilder.CreateIndex(
                name: "IX_bank_statement_lines_reconciled_by",
                table: "bank_statement_lines",
                column: "reconciled_by");

            migrationBuilder.CreateIndex(
                name: "IX_cold_storage_transaction_items_cold_txn_id",
                table: "cold_storage_transaction_items",
                column: "cold_txn_id");

            migrationBuilder.CreateIndex(
                name: "IX_cold_storage_transaction_items_item_id",
                table: "cold_storage_transaction_items",
                column: "item_id");

            migrationBuilder.CreateIndex(
                name: "IX_cold_storage_transaction_items_lot_id",
                table: "cold_storage_transaction_items",
                column: "lot_id");

            migrationBuilder.CreateIndex(
                name: "cold_storage_transactions_financial_year_id_txn_no_key",
                table: "cold_storage_transactions",
                columns: new[] { "financial_year_id", "txn_no" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_cold_storage_transactions_cold_store_id",
                table: "cold_storage_transactions",
                column: "cold_store_id");

            migrationBuilder.CreateIndex(
                name: "IX_cold_storage_transactions_created_by",
                table: "cold_storage_transactions",
                column: "created_by");

            migrationBuilder.CreateIndex(
                name: "IX_cold_storage_transactions_party_id",
                table: "cold_storage_transactions",
                column: "party_id");

            migrationBuilder.CreateIndex(
                name: "companies_code_key",
                table: "companies",
                column: "code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_crate_transaction_items_crate_txn_id",
                table: "crate_transaction_items",
                column: "crate_txn_id");

            migrationBuilder.CreateIndex(
                name: "crate_transactions_financial_year_id_txn_no_key",
                table: "crate_transactions",
                columns: new[] { "financial_year_id", "txn_no" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "idx_crate_txn_account",
                table: "crate_transactions",
                columns: new[] { "account_id", "txn_date" });

            migrationBuilder.CreateIndex(
                name: "IX_crate_transactions_created_by",
                table: "crate_transactions",
                column: "created_by");

            migrationBuilder.CreateIndex(
                name: "IX_data_locks_financial_year_id",
                table: "data_locks",
                column: "financial_year_id");

            migrationBuilder.CreateIndex(
                name: "IX_data_locks_set_by",
                table: "data_locks",
                column: "set_by");

            migrationBuilder.CreateIndex(
                name: "daybooks_company_id_name_key",
                table: "daybooks",
                columns: new[] { "company_id", "name" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_daybooks_linked_account_id",
                table: "daybooks",
                column: "linked_account_id");

            migrationBuilder.CreateIndex(
                name: "desavar_purchases_financial_year_id_bill_no_key",
                table: "desavar_purchases",
                columns: new[] { "financial_year_id", "bill_no" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_desavar_purchases_item_id",
                table: "desavar_purchases",
                column: "item_id");

            migrationBuilder.CreateIndex(
                name: "IX_desavar_purchases_lot_id",
                table: "desavar_purchases",
                column: "lot_id");

            migrationBuilder.CreateIndex(
                name: "IX_desavar_purchases_supplier_id",
                table: "desavar_purchases",
                column: "supplier_id");

            migrationBuilder.CreateIndex(
                name: "IX_desavar_sales_buyer_id",
                table: "desavar_sales",
                column: "buyer_id");

            migrationBuilder.CreateIndex(
                name: "IX_desavar_sales_desavar_purchase_id",
                table: "desavar_sales",
                column: "desavar_purchase_id");

            migrationBuilder.CreateIndex(
                name: "IX_desavar_sales_financial_year_id",
                table: "desavar_sales",
                column: "financial_year_id");

            migrationBuilder.CreateIndex(
                name: "IX_desavar_sales_item_id",
                table: "desavar_sales",
                column: "item_id");

            migrationBuilder.CreateIndex(
                name: "IX_desavar_sales_lot_id",
                table: "desavar_sales",
                column: "lot_id");

            migrationBuilder.CreateIndex(
                name: "financial_years_company_id_code_key",
                table: "financial_years",
                columns: new[] { "company_id", "code" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_import_purchase_items_count_id",
                table: "import_purchase_items",
                column: "count_id");

            migrationBuilder.CreateIndex(
                name: "IX_import_purchase_items_import_purchase_id",
                table: "import_purchase_items",
                column: "import_purchase_id");

            migrationBuilder.CreateIndex(
                name: "IX_import_purchase_items_item_id",
                table: "import_purchase_items",
                column: "item_id");

            migrationBuilder.CreateIndex(
                name: "IX_import_purchase_items_lot_id",
                table: "import_purchase_items",
                column: "lot_id");

            migrationBuilder.CreateIndex(
                name: "import_purchases_financial_year_id_invoice_no_key",
                table: "import_purchases",
                columns: new[] { "financial_year_id", "invoice_no" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_import_purchases_supplier_id",
                table: "import_purchases",
                column: "supplier_id");

            migrationBuilder.CreateIndex(
                name: "IX_import_sales_buyer_id",
                table: "import_sales",
                column: "buyer_id");

            migrationBuilder.CreateIndex(
                name: "IX_import_sales_financial_year_id",
                table: "import_sales",
                column: "financial_year_id");

            migrationBuilder.CreateIndex(
                name: "IX_import_sales_item_id",
                table: "import_sales",
                column: "item_id");

            migrationBuilder.CreateIndex(
                name: "IX_import_sales_lot_id",
                table: "import_sales",
                column: "lot_id");

            migrationBuilder.CreateIndex(
                name: "item_categories_company_id_name_key",
                table: "item_categories",
                columns: new[] { "company_id", "name" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "item_counts_company_id_item_group_id_name_key",
                table: "item_counts",
                columns: new[] { "company_id", "item_group_id", "name" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_item_counts_item_group_id",
                table: "item_counts",
                column: "item_group_id");

            migrationBuilder.CreateIndex(
                name: "item_groups_company_id_name_key",
                table: "item_groups",
                columns: new[] { "company_id", "name" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "items_company_id_name_key",
                table: "items",
                columns: new[] { "company_id", "name" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_items_item_category_id",
                table: "items",
                column: "item_category_id");

            migrationBuilder.CreateIndex(
                name: "IX_items_item_group_id",
                table: "items",
                column: "item_group_id");

            migrationBuilder.CreateIndex(
                name: "IX_journal_voucher_lines_account_id",
                table: "journal_voucher_lines",
                column: "account_id");

            migrationBuilder.CreateIndex(
                name: "IX_journal_voucher_lines_journal_voucher_id",
                table: "journal_voucher_lines",
                column: "journal_voucher_id");

            migrationBuilder.CreateIndex(
                name: "IX_journal_vouchers_created_by",
                table: "journal_vouchers",
                column: "created_by");

            migrationBuilder.CreateIndex(
                name: "journal_vouchers_financial_year_id_voucher_no_key",
                table: "journal_vouchers",
                columns: new[] { "financial_year_id", "voucher_no" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "idx_ledger_account_date",
                table: "ledger_entries",
                columns: new[] { "account_id", "entry_date" });

            migrationBuilder.CreateIndex(
                name: "idx_ledger_fy_date",
                table: "ledger_entries",
                columns: new[] { "financial_year_id", "entry_date" });

            migrationBuilder.CreateIndex(
                name: "IX_ledger_entries_contra_account_id",
                table: "ledger_entries",
                column: "contra_account_id");

            migrationBuilder.CreateIndex(
                name: "IX_login_log_user_id",
                table: "login_log",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_lot_operations_created_by",
                table: "lot_operations",
                column: "created_by");

            migrationBuilder.CreateIndex(
                name: "IX_lot_operations_financial_year_id",
                table: "lot_operations",
                column: "financial_year_id");

            migrationBuilder.CreateIndex(
                name: "IX_lot_operations_source_lot_id",
                table: "lot_operations",
                column: "source_lot_id");

            migrationBuilder.CreateIndex(
                name: "IX_lot_operations_target_account_id",
                table: "lot_operations",
                column: "target_account_id");

            migrationBuilder.CreateIndex(
                name: "IX_lot_operations_target_lot_id",
                table: "lot_operations",
                column: "target_lot_id");

            migrationBuilder.CreateIndex(
                name: "idx_lots_item",
                table: "lots",
                column: "item_id");

            migrationBuilder.CreateIndex(
                name: "idx_lots_supplier",
                table: "lots",
                column: "supplier_id");

            migrationBuilder.CreateIndex(
                name: "IX_lots_parent_lot_id",
                table: "lots",
                column: "parent_lot_id");

            migrationBuilder.CreateIndex(
                name: "lots_financial_year_id_lot_no_key",
                table: "lots",
                columns: new[] { "financial_year_id", "lot_no" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_payment_allocations_purchase_bill_id",
                table: "payment_allocations",
                column: "purchase_bill_id");

            migrationBuilder.CreateIndex(
                name: "payment_allocations_payment_id_purchase_bill_id_key",
                table: "payment_allocations",
                columns: new[] { "payment_id", "purchase_bill_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "idx_payments_account",
                table: "payments",
                columns: new[] { "account_id", "payment_date" });

            migrationBuilder.CreateIndex(
                name: "IX_payments_created_by",
                table: "payments",
                column: "created_by");

            migrationBuilder.CreateIndex(
                name: "IX_payments_daybook_id",
                table: "payments",
                column: "daybook_id");

            migrationBuilder.CreateIndex(
                name: "payments_financial_year_id_payment_no_key",
                table: "payments",
                columns: new[] { "financial_year_id", "payment_no" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "idx_pbi_lot",
                table: "purchase_bill_items",
                column: "lot_id");

            migrationBuilder.CreateIndex(
                name: "IX_purchase_bill_items_item_id",
                table: "purchase_bill_items",
                column: "item_id");

            migrationBuilder.CreateIndex(
                name: "IX_purchase_bill_items_purchase_bill_id",
                table: "purchase_bill_items",
                column: "purchase_bill_id");

            migrationBuilder.CreateIndex(
                name: "idx_purchase_bills_date",
                table: "purchase_bills",
                columns: new[] { "financial_year_id", "bill_date" });

            migrationBuilder.CreateIndex(
                name: "idx_purchase_bills_supplier",
                table: "purchase_bills",
                columns: new[] { "supplier_id", "bill_date" });

            migrationBuilder.CreateIndex(
                name: "IX_purchase_bills_created_by",
                table: "purchase_bills",
                column: "created_by");

            migrationBuilder.CreateIndex(
                name: "purchase_bills_financial_year_id_bill_no_key",
                table: "purchase_bills",
                columns: new[] { "financial_year_id", "bill_no" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_receipt_allocations_sales_bill_id",
                table: "receipt_allocations",
                column: "sales_bill_id");

            migrationBuilder.CreateIndex(
                name: "receipt_allocations_receipt_id_sales_bill_id_key",
                table: "receipt_allocations",
                columns: new[] { "receipt_id", "sales_bill_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "idx_receipts_account",
                table: "receipts",
                columns: new[] { "account_id", "receipt_date" });

            migrationBuilder.CreateIndex(
                name: "IX_receipts_created_by",
                table: "receipts",
                column: "created_by");

            migrationBuilder.CreateIndex(
                name: "IX_receipts_daybook_id",
                table: "receipts",
                column: "daybook_id");

            migrationBuilder.CreateIndex(
                name: "receipts_financial_year_id_receipt_no_key",
                table: "receipts",
                columns: new[] { "financial_year_id", "receipt_no" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "regions_company_id_code_key",
                table: "regions",
                columns: new[] { "company_id", "code" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "regions_company_id_name_key",
                table: "regions",
                columns: new[] { "company_id", "name" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "idx_sales_buyer",
                table: "sales",
                columns: new[] { "buyer_id", "sale_date" });

            migrationBuilder.CreateIndex(
                name: "idx_sales_date",
                table: "sales",
                columns: new[] { "financial_year_id", "sale_date" });

            migrationBuilder.CreateIndex(
                name: "idx_sales_lot",
                table: "sales",
                column: "lot_id");

            migrationBuilder.CreateIndex(
                name: "IX_sales_created_by",
                table: "sales",
                column: "created_by");

            migrationBuilder.CreateIndex(
                name: "IX_sales_item_id",
                table: "sales",
                column: "item_id");

            migrationBuilder.CreateIndex(
                name: "IX_sales_sales_bill_id",
                table: "sales",
                column: "sales_bill_id");

            migrationBuilder.CreateIndex(
                name: "idx_sales_bills_buyer",
                table: "sales_bills",
                columns: new[] { "buyer_id", "bill_date" });

            migrationBuilder.CreateIndex(
                name: "IX_sales_bills_created_by",
                table: "sales_bills",
                column: "created_by");

            migrationBuilder.CreateIndex(
                name: "sales_bills_financial_year_id_bill_no_key",
                table: "sales_bills",
                columns: new[] { "financial_year_id", "bill_no" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "salesmen_company_id_name_key",
                table: "salesmen",
                columns: new[] { "company_id", "name" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "idx_tds_pur_supplier",
                table: "tds_purchase_deductions",
                columns: new[] { "financial_year_id", "supplier_id" });

            migrationBuilder.CreateIndex(
                name: "IX_tds_purchase_deductions_payment_id",
                table: "tds_purchase_deductions",
                column: "payment_id");

            migrationBuilder.CreateIndex(
                name: "IX_tds_purchase_deductions_supplier_id",
                table: "tds_purchase_deductions",
                column: "supplier_id");

            migrationBuilder.CreateIndex(
                name: "transporters_company_id_name_key",
                table: "transporters",
                columns: new[] { "company_id", "name" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_user_company_access_company_id",
                table: "user_company_access",
                column: "company_id");

            migrationBuilder.CreateIndex(
                name: "idx_user_preferences_key",
                table: "user_preferences",
                columns: new[] { "user_id", "preference_key" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "users_username_key",
                table: "users",
                column: "username",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "idx_wa_dispatch_account",
                table: "whatsapp_dispatches",
                columns: new[] { "account_id", "sent_at" });

            migrationBuilder.CreateIndex(
                name: "IX_whatsapp_dispatches_sent_by",
                table: "whatsapp_dispatches",
                column: "sent_by");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "account_opening_balances");

            migrationBuilder.DropTable(
                name: "attachments");

            migrationBuilder.DropTable(
                name: "audit_log");

            migrationBuilder.DropTable(
                name: "bank_entries");

            migrationBuilder.DropTable(
                name: "bank_statement_lines");

            migrationBuilder.DropTable(
                name: "cold_storage_transaction_items");

            migrationBuilder.DropTable(
                name: "crate_transaction_items");

            migrationBuilder.DropTable(
                name: "data_locks");

            migrationBuilder.DropTable(
                name: "desavar_sales");

            migrationBuilder.DropTable(
                name: "feature_toggles");

            migrationBuilder.DropTable(
                name: "import_purchase_items");

            migrationBuilder.DropTable(
                name: "import_sales");

            migrationBuilder.DropTable(
                name: "journal_voucher_lines");

            migrationBuilder.DropTable(
                name: "login_log");

            migrationBuilder.DropTable(
                name: "lot_operations");

            migrationBuilder.DropTable(
                name: "payment_allocations");

            migrationBuilder.DropTable(
                name: "purchase_bill_items");

            migrationBuilder.DropTable(
                name: "receipt_allocations");

            migrationBuilder.DropTable(
                name: "sales");

            migrationBuilder.DropTable(
                name: "system_parameters");

            migrationBuilder.DropTable(
                name: "tds_purchase_deductions");

            migrationBuilder.DropTable(
                name: "transporters");

            migrationBuilder.DropTable(
                name: "user_company_access");

            migrationBuilder.DropTable(
                name: "user_permissions");

            migrationBuilder.DropTable(
                name: "user_preferences");

            migrationBuilder.DropTable(
                name: "whatsapp_dispatches");

            migrationBuilder.DropTable(
                name: "ledger_entries");

            migrationBuilder.DropTable(
                name: "cold_storage_transactions");

            migrationBuilder.DropTable(
                name: "crate_transactions");

            migrationBuilder.DropTable(
                name: "desavar_purchases");

            migrationBuilder.DropTable(
                name: "item_counts");

            migrationBuilder.DropTable(
                name: "import_purchases");

            migrationBuilder.DropTable(
                name: "journal_vouchers");

            migrationBuilder.DropTable(
                name: "purchase_bills");

            migrationBuilder.DropTable(
                name: "receipts");

            migrationBuilder.DropTable(
                name: "sales_bills");

            migrationBuilder.DropTable(
                name: "payments");

            migrationBuilder.DropTable(
                name: "lots");

            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.DropTable(
                name: "daybooks");

            migrationBuilder.DropTable(
                name: "financial_years");

            migrationBuilder.DropTable(
                name: "items");

            migrationBuilder.DropTable(
                name: "accounts");

            migrationBuilder.DropTable(
                name: "item_categories");

            migrationBuilder.DropTable(
                name: "item_groups");

            migrationBuilder.DropTable(
                name: "account_groups");

            migrationBuilder.DropTable(
                name: "regions");

            migrationBuilder.DropTable(
                name: "salesmen");

            migrationBuilder.DropTable(
                name: "companies");
        }
    }
}
