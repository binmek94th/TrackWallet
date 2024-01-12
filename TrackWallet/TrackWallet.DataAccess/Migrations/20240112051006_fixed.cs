using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrackWallet.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class @fixed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Budgets_Occasions_OccasionId",
                table: "Budgets");

            migrationBuilder.AddForeignKey(
                name: "FK_Budgets_Occasions_OccasionId",
                table: "Budgets",
                column: "OccasionId",
                principalTable: "Occasions",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Budgets_Occasions_OccasionId",
                table: "Budgets");

            migrationBuilder.AddForeignKey(
                name: "FK_Budgets_Occasions_OccasionId",
                table: "Budgets",
                column: "OccasionId",
                principalTable: "Occasions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
