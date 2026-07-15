using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FruitAccounting.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddSalesFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "inv_no",
                table: "sales",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "net_amount",
                table: "sales",
                type: "numeric(14,2)",
                precision: 14,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "veh_no",
                table: "sales",
                type: "character varying(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "apmc_pct",
                table: "companies",
                type: "numeric(5,3)",
                precision: 5,
                scale: 3,
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "idx_sales_invno",
                table: "sales",
                columns: new[] { "financial_year_id", "inv_no" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "idx_sales_invno",
                table: "sales");

            migrationBuilder.DropColumn(
                name: "inv_no",
                table: "sales");

            migrationBuilder.DropColumn(
                name: "net_amount",
                table: "sales");

            migrationBuilder.DropColumn(
                name: "veh_no",
                table: "sales");

            migrationBuilder.DropColumn(
                name: "apmc_pct",
                table: "companies");
        }
    }
}
