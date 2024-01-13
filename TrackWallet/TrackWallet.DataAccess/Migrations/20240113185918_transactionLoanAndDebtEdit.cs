using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrackWallet.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class transactionLoanAndDebtEdit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
        }
    }
}
