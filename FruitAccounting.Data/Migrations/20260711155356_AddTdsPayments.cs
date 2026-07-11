using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace FruitAccounting.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddTdsPayments : Migration
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
                .Annotation("Npgsql:Enum:voucher_type", "purchase_bill,sales_bill,receipt,payment,journal,bank_entry,crate,cold_storage,desavar_purchase,desavar_sale,import_purchase,import_sale,opening_balance,tds_payment")
                .OldAnnotation("Npgsql:Enum:cold_txn_type", "inward,outward")
                .OldAnnotation("Npgsql:Enum:crate_txn_type", "issue,return,amount_conversion")
                .OldAnnotation("Npgsql:Enum:dispatch_status", "queued,sent,delivered,read,failed")
                .OldAnnotation("Npgsql:Enum:dr_cr", "debit,credit")
                .OldAnnotation("Npgsql:Enum:lot_op_type", "split,merge,transfer")
                .OldAnnotation("Npgsql:Enum:payment_mode", "cash,bank,cheque")
                .OldAnnotation("Npgsql:Enum:purchase_mode", "with_commission,trading,without_commission")
                .OldAnnotation("Npgsql:Enum:user_role", "admin,operator,readonly")
                .OldAnnotation("Npgsql:Enum:voucher_type", "purchase_bill,sales_bill,receipt,payment,journal,bank_entry,crate,cold_storage,desavar_purchase,desavar_sale,import_purchase,import_sale,opening_balance");

            migrationBuilder.AddColumn<long>(
                name: "tds_payment_id",
                table: "tds_purchase_deductions",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "tds_payments",
                columns: table => new
                {
                    tds_payment_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    financial_year_id = table.Column<long>(type: "bigint", nullable: false),
                    tds_payment_no = table.Column<int>(type: "integer", nullable: false),
                    payment_date = table.Column<DateOnly>(type: "date", nullable: false),
                    tds_account_id = table.Column<long>(type: "bigint", nullable: false),
                    daybook_id = table.Column<long>(type: "bigint", nullable: false),
                    bsr_code = table.Column<string>(type: "character varying(7)", maxLength: 7, nullable: true),
                    challan_serial_no = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true),
                    interest_rate_pct = table.Column<decimal>(type: "numeric(6,3)", precision: 6, scale: 3, nullable: false, defaultValueSql: "0"),
                    tax_amount = table.Column<decimal>(type: "numeric(14,2)", precision: 14, scale: 2, nullable: false, defaultValueSql: "0"),
                    interest_amount = table.Column<decimal>(type: "numeric(14,2)", precision: 14, scale: 2, nullable: false, defaultValueSql: "0"),
                    fees_amount = table.Column<decimal>(type: "numeric(14,2)", precision: 14, scale: 2, nullable: false, defaultValueSql: "0"),
                    penalty_amount = table.Column<decimal>(type: "numeric(14,2)", precision: 14, scale: 2, nullable: false, defaultValueSql: "0"),
                    interest_account_id = table.Column<long>(type: "bigint", nullable: true),
                    fees_account_id = table.Column<long>(type: "bigint", nullable: true),
                    penalty_account_id = table.Column<long>(type: "bigint", nullable: true),
                    total_amount = table.Column<decimal>(type: "numeric(14,2)", precision: 14, scale: 2, nullable: false, defaultValueSql: "0"),
                    remarks = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    created_by = table.Column<long>(type: "bigint", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("tds_payments_pkey", x => x.tds_payment_id);
                    table.ForeignKey(
                        name: "tds_payments_created_by_fkey",
                        column: x => x.created_by,
                        principalTable: "users",
                        principalColumn: "user_id");
                    table.ForeignKey(
                        name: "tds_payments_daybook_id_fkey",
                        column: x => x.daybook_id,
                        principalTable: "daybooks",
                        principalColumn: "daybook_id");
                    table.ForeignKey(
                        name: "tds_payments_fees_account_id_fkey",
                        column: x => x.fees_account_id,
                        principalTable: "accounts",
                        principalColumn: "account_id");
                    table.ForeignKey(
                        name: "tds_payments_financial_year_id_fkey",
                        column: x => x.financial_year_id,
                        principalTable: "financial_years",
                        principalColumn: "financial_year_id");
                    table.ForeignKey(
                        name: "tds_payments_interest_account_id_fkey",
                        column: x => x.interest_account_id,
                        principalTable: "accounts",
                        principalColumn: "account_id");
                    table.ForeignKey(
                        name: "tds_payments_penalty_account_id_fkey",
                        column: x => x.penalty_account_id,
                        principalTable: "accounts",
                        principalColumn: "account_id");
                    table.ForeignKey(
                        name: "tds_payments_tds_account_id_fkey",
                        column: x => x.tds_account_id,
                        principalTable: "accounts",
                        principalColumn: "account_id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_tds_purchase_deductions_tds_payment_id",
                table: "tds_purchase_deductions",
                column: "tds_payment_id");

            migrationBuilder.CreateIndex(
                name: "IX_tds_payments_created_by",
                table: "tds_payments",
                column: "created_by");

            migrationBuilder.CreateIndex(
                name: "IX_tds_payments_daybook_id",
                table: "tds_payments",
                column: "daybook_id");

            migrationBuilder.CreateIndex(
                name: "IX_tds_payments_fees_account_id",
                table: "tds_payments",
                column: "fees_account_id");

            migrationBuilder.CreateIndex(
                name: "IX_tds_payments_interest_account_id",
                table: "tds_payments",
                column: "interest_account_id");

            migrationBuilder.CreateIndex(
                name: "IX_tds_payments_penalty_account_id",
                table: "tds_payments",
                column: "penalty_account_id");

            migrationBuilder.CreateIndex(
                name: "IX_tds_payments_tds_account_id",
                table: "tds_payments",
                column: "tds_account_id");

            migrationBuilder.CreateIndex(
                name: "tds_payments_financial_year_id_tds_payment_no_key",
                table: "tds_payments",
                columns: new[] { "financial_year_id", "tds_payment_no" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "tds_purchase_deductions_tds_payment_id_fkey",
                table: "tds_purchase_deductions",
                column: "tds_payment_id",
                principalTable: "tds_payments",
                principalColumn: "tds_payment_id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "tds_purchase_deductions_tds_payment_id_fkey",
                table: "tds_purchase_deductions");

            migrationBuilder.DropTable(
                name: "tds_payments");

            migrationBuilder.DropIndex(
                name: "IX_tds_purchase_deductions_tds_payment_id",
                table: "tds_purchase_deductions");

            migrationBuilder.DropColumn(
                name: "tds_payment_id",
                table: "tds_purchase_deductions");

            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:Enum:cold_txn_type", "inward,outward")
                .Annotation("Npgsql:Enum:crate_txn_type", "issue,return,amount_conversion")
                .Annotation("Npgsql:Enum:dispatch_status", "queued,sent,delivered,read,failed")
                .Annotation("Npgsql:Enum:dr_cr", "debit,credit")
                .Annotation("Npgsql:Enum:lot_op_type", "split,merge,transfer")
                .Annotation("Npgsql:Enum:payment_mode", "cash,bank,cheque")
                .Annotation("Npgsql:Enum:purchase_mode", "with_commission,trading,without_commission")
                .Annotation("Npgsql:Enum:user_role", "admin,operator,readonly")
                .Annotation("Npgsql:Enum:voucher_type", "purchase_bill,sales_bill,receipt,payment,journal,bank_entry,crate,cold_storage,desavar_purchase,desavar_sale,import_purchase,import_sale,opening_balance")
                .OldAnnotation("Npgsql:Enum:cold_txn_type", "inward,outward")
                .OldAnnotation("Npgsql:Enum:crate_txn_type", "issue,return,amount_conversion")
                .OldAnnotation("Npgsql:Enum:dispatch_status", "queued,sent,delivered,read,failed")
                .OldAnnotation("Npgsql:Enum:dr_cr", "debit,credit")
                .OldAnnotation("Npgsql:Enum:lot_op_type", "split,merge,transfer")
                .OldAnnotation("Npgsql:Enum:payment_mode", "cash,bank,cheque")
                .OldAnnotation("Npgsql:Enum:purchase_mode", "with_commission,trading,without_commission")
                .OldAnnotation("Npgsql:Enum:user_role", "admin,operator,readonly")
                .OldAnnotation("Npgsql:Enum:voucher_type", "purchase_bill,sales_bill,receipt,payment,journal,bank_entry,crate,cold_storage,desavar_purchase,desavar_sale,import_purchase,import_sale,opening_balance,tds_payment");
        }
    }
}
