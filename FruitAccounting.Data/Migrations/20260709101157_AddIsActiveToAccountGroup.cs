using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FruitAccounting.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddIsActiveToAccountGroup : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "quantity",
                table: "crate_transaction_items",
                type: "numeric(10)",
                precision: 10,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(10,0)",
                oldPrecision: 10);

            migrationBuilder.AddColumn<bool>(
                name: "is_active",
                table: "account_groups",
                type: "boolean",
                nullable: false,
                defaultValue: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "is_active",
                table: "account_groups");

            migrationBuilder.AlterColumn<decimal>(
                name: "quantity",
                table: "crate_transaction_items",
                type: "numeric(10,0)",
                precision: 10,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(10)",
                oldPrecision: 10);
        }
    }
}
