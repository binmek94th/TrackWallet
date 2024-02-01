using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrackWallet.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class transactionAndOcassion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OccasionId",
                table: "Transactions",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_OccasionId",
                table: "Transactions",
                column: "OccasionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Occasions_OccasionId",
                table: "Transactions",
                column: "OccasionId",
                principalTable: "Occasions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Occasions_OccasionId",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_Transactions_OccasionId",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "OccasionId",
                table: "Transactions");
        }
    }
}
