using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrackWallet.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class fixedTL : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LoanAndDebts_Transactions_TransactionId",
                table: "LoanAndDebts");

            migrationBuilder.DropIndex(
                name: "IX_LoanAndDebts_TransactionId",
                table: "LoanAndDebts");

            migrationBuilder.DropColumn(
                name: "TransactionId",
                table: "LoanAndDebts");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<int>(
                name: "TransactionId",
                table: "LoanAndDebts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_LoanAndDebts_TransactionId",
                table: "LoanAndDebts",
                column: "TransactionId");

            migrationBuilder.AddForeignKey(
                name: "FK_LoanAndDebts_Transactions_TransactionId",
                table: "LoanAndDebts",
                column: "TransactionId",
                principalTable: "Transactions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
