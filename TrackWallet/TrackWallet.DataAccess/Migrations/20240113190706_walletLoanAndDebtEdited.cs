using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrackWallet.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class walletLoanAndDebtEdited : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "WalletId",
                table: "LoanAndDebts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_LoanAndDebts_WalletId",
                table: "LoanAndDebts",
                column: "WalletId");

            migrationBuilder.AddForeignKey(
                name: "FK_LoanAndDebts_Wallets_WalletId",
                table: "LoanAndDebts",
                column: "WalletId",
                principalTable: "Wallets",
                principalColumn: "WalletId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LoanAndDebts_Wallets_WalletId",
                table: "LoanAndDebts");

            migrationBuilder.DropIndex(
                name: "IX_LoanAndDebts_WalletId",
                table: "LoanAndDebts");

            migrationBuilder.DropColumn(
                name: "WalletId",
                table: "LoanAndDebts");
        }
    }
}
