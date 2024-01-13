using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrackWallet.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class loanAndDebtEvent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EventId",
                table: "LoanAndDebts",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_LoanAndDebts_EventId",
                table: "LoanAndDebts",
                column: "EventId");

            migrationBuilder.AddForeignKey(
                name: "FK_LoanAndDebts_Events_EventId",
                table: "LoanAndDebts",
                column: "EventId",
                principalTable: "Events",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LoanAndDebts_Events_EventId",
                table: "LoanAndDebts");

            migrationBuilder.DropIndex(
                name: "IX_LoanAndDebts_EventId",
                table: "LoanAndDebts");

            migrationBuilder.DropColumn(
                name: "EventId",
                table: "LoanAndDebts");
        }
    }
}
