using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrackWallet.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class recurringTransactionEvent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EventId",
                table: "RecurringTransactions",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_RecurringTransactions_EventId",
                table: "RecurringTransactions",
                column: "EventId");

            migrationBuilder.AddForeignKey(
                name: "FK_RecurringTransactions_Events_EventId",
                table: "RecurringTransactions",
                column: "EventId",
                principalTable: "Events",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RecurringTransactions_Events_EventId",
                table: "RecurringTransactions");

            migrationBuilder.DropIndex(
                name: "IX_RecurringTransactions_EventId",
                table: "RecurringTransactions");

            migrationBuilder.DropColumn(
                name: "EventId",
                table: "RecurringTransactions");
        }
    }
}
