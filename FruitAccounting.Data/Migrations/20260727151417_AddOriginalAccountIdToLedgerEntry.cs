using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FruitAccounting.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddOriginalAccountIdToLedgerEntry : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "OriginalAccountId",
                table: "ledger_entries",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ledger_entries_OriginalAccountId",
                table: "ledger_entries",
                column: "OriginalAccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_ledger_entries_accounts_OriginalAccountId",
                table: "ledger_entries",
                column: "OriginalAccountId",
                principalTable: "accounts",
                principalColumn: "account_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ledger_entries_accounts_OriginalAccountId",
                table: "ledger_entries");

            migrationBuilder.DropIndex(
                name: "IX_ledger_entries_OriginalAccountId",
                table: "ledger_entries");

            migrationBuilder.DropColumn(
                name: "OriginalAccountId",
                table: "ledger_entries");
        }
    }
}
