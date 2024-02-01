using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrackWallet.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class TransactionRecurringTransaction : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RecurringTransactionId",
                table: "Transactions",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_RecurringTransactionId",
                table: "Transactions",
                column: "RecurringTransactionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_RecurringTransactions_RecurringTransactionId",
                table: "Transactions",
                column: "RecurringTransactionId",
                principalTable: "RecurringTransactions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_RecurringTransactions_RecurringTransactionId",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_Transactions_RecurringTransactionId",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "RecurringTransactionId",
                table: "Transactions");
        }
    }
}
