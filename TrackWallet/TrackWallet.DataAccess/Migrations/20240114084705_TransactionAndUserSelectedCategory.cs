using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrackWallet.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class TransactionAndUserSelectedCategory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserSelectedCategoryId",
                table: "Transactions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "USCategoryId",
                table: "BillAndReminder",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_UserSelectedCategoryId",
                table: "Transactions",
                column: "UserSelectedCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_UserSelectedCategories_UserSelectedCategoryId",
                table: "Transactions",
                column: "UserSelectedCategoryId",
                principalTable: "UserSelectedCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_UserSelectedCategories_UserSelectedCategoryId",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_Transactions_UserSelectedCategoryId",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "UserSelectedCategoryId",
                table: "Transactions");

            migrationBuilder.AlterColumn<int>(
                name: "USCategoryId",
                table: "BillAndReminder",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }
    }
}
