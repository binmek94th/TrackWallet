using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrackWallet.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class walletLoanAndDebtEdit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_LoanAndDebts_LoanAndDebtId",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_Transactions_LoanAndDebtId",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "LoanAndDebtId",
                table: "Transactions");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LoanAndDebtId",
                table: "Transactions",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_LoanAndDebtId",
                table: "Transactions",
                column: "LoanAndDebtId");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_LoanAndDebts_LoanAndDebtId",
                table: "Transactions",
                column: "LoanAndDebtId",
                principalTable: "LoanAndDebts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
