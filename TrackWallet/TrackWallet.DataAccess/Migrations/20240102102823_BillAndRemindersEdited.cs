using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrackWallet.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class BillAndRemindersEdited : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BillAndReminders_UserSelectedCategories_CategoryId",
                table: "BillAndReminders");

            migrationBuilder.RenameColumn(
                name: "CategoryId",
                table: "BillAndReminders",
                newName: "USCategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_BillAndReminders_CategoryId",
                table: "BillAndReminders",
                newName: "IX_BillAndReminders_USCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_BillAndReminders_UserSelectedCategories_USCategoryId",
                table: "BillAndReminders",
                column: "USCategoryId",
                principalTable: "UserSelectedCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BillAndReminders_UserSelectedCategories_USCategoryId",
                table: "BillAndReminders");

            migrationBuilder.RenameColumn(
                name: "USCategoryId",
                table: "BillAndReminders",
                newName: "CategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_BillAndReminders_USCategoryId",
                table: "BillAndReminders",
                newName: "IX_BillAndReminders_CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_BillAndReminders_UserSelectedCategories_CategoryId",
                table: "BillAndReminders",
                column: "CategoryId",
                principalTable: "UserSelectedCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
