using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrackWallet.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class budgetEvent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EventId",
                table: "Budgets",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Budgets_EventId",
                table: "Budgets",
                column: "EventId");

            migrationBuilder.AddForeignKey(
                name: "FK_Budgets_Events_EventId",
                table: "Budgets",
                column: "EventId",
                principalTable: "Events",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Budgets_Events_EventId",
                table: "Budgets");

            migrationBuilder.DropIndex(
                name: "IX_Budgets_EventId",
                table: "Budgets");

            migrationBuilder.DropColumn(
                name: "EventId",
                table: "Budgets");
        }
    }
}
