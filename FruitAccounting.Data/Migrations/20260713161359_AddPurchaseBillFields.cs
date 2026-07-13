using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FruitAccounting.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddPurchaseBillFields : Migration
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
                .Annotation("Npgsql:Enum:purchase_mode", "with_commission,trading,without_commission,direct")
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
                .OldAnnotation("Npgsql:Enum:voucher_type", "purchase_bill,sales_bill,receipt,payment,journal,bank_entry,crate,cold_storage,desavar_purchase,desavar_sale,import_purchase,import_sale,opening_balance,tds_payment");

            migrationBuilder.AlterColumn<long>(
                name: "payment_id",
                table: "tds_purchase_deductions",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddColumn<long>(
                name: "purchase_bill_id",
                table: "tds_purchase_deductions",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "amanat_party_id",
                table: "purchase_bills",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "crate_party_id",
                table: "purchase_bills",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "freight_rate",
                table: "purchase_bills",
                type: "numeric(10,2)",
                precision: 10,
                scale: 2,
                nullable: true,
                defaultValueSql: "0");

            migrationBuilder.AddColumn<decimal>(
                name: "labour_rate",
                table: "purchase_bills",
                type: "numeric(10,2)",
                precision: 10,
                scale: 2,
                nullable: true,
                defaultValueSql: "0");

            migrationBuilder.AddColumn<decimal>(
                name: "vatav_pct",
                table: "purchase_bills",
                type: "numeric(6,3)",
                precision: 6,
                scale: 3,
                nullable: true,
                defaultValueSql: "0");

            migrationBuilder.CreateIndex(
                name: "IX_tds_purchase_deductions_purchase_bill_id",
                table: "tds_purchase_deductions",
                column: "purchase_bill_id");

            migrationBuilder.CreateIndex(
                name: "IX_purchase_bills_amanat_party_id",
                table: "purchase_bills",
                column: "amanat_party_id");

            migrationBuilder.CreateIndex(
                name: "IX_purchase_bills_crate_party_id",
                table: "purchase_bills",
                column: "crate_party_id");

            migrationBuilder.AddForeignKey(
                name: "purchase_bills_amanat_party_id_fkey",
                table: "purchase_bills",
                column: "amanat_party_id",
                principalTable: "accounts",
                principalColumn: "account_id");

            migrationBuilder.AddForeignKey(
                name: "purchase_bills_crate_party_id_fkey",
                table: "purchase_bills",
                column: "crate_party_id",
                principalTable: "accounts",
                principalColumn: "account_id");

            migrationBuilder.AddForeignKey(
                name: "tds_purchase_deductions_purchase_bill_id_fkey",
                table: "tds_purchase_deductions",
                column: "purchase_bill_id",
                principalTable: "purchase_bills",
                principalColumn: "purchase_bill_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "purchase_bills_amanat_party_id_fkey",
                table: "purchase_bills");

            migrationBuilder.DropForeignKey(
                name: "purchase_bills_crate_party_id_fkey",
                table: "purchase_bills");

            migrationBuilder.DropForeignKey(
                name: "tds_purchase_deductions_purchase_bill_id_fkey",
                table: "tds_purchase_deductions");

            migrationBuilder.DropIndex(
                name: "IX_tds_purchase_deductions_purchase_bill_id",
                table: "tds_purchase_deductions");

            migrationBuilder.DropIndex(
                name: "IX_purchase_bills_amanat_party_id",
                table: "purchase_bills");

            migrationBuilder.DropIndex(
                name: "IX_purchase_bills_crate_party_id",
                table: "purchase_bills");

            migrationBuilder.DropColumn(
                name: "purchase_bill_id",
                table: "tds_purchase_deductions");

            migrationBuilder.DropColumn(
                name: "amanat_party_id",
                table: "purchase_bills");

            migrationBuilder.DropColumn(
                name: "crate_party_id",
                table: "purchase_bills");

            migrationBuilder.DropColumn(
                name: "freight_rate",
                table: "purchase_bills");

            migrationBuilder.DropColumn(
                name: "labour_rate",
                table: "purchase_bills");

            migrationBuilder.DropColumn(
                name: "vatav_pct",
                table: "purchase_bills");

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
                .OldAnnotation("Npgsql:Enum:purchase_mode", "with_commission,trading,without_commission,direct")
                .OldAnnotation("Npgsql:Enum:user_role", "admin,operator,readonly")
                .OldAnnotation("Npgsql:Enum:voucher_type", "purchase_bill,sales_bill,receipt,payment,journal,bank_entry,crate,cold_storage,desavar_purchase,desavar_sale,import_purchase,import_sale,opening_balance,tds_payment");

            migrationBuilder.AlterColumn<long>(
                name: "payment_id",
                table: "tds_purchase_deductions",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);
        }
    }
}
