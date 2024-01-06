using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrackWallet.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class BudgetEdited : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Budgets_Categories_CategoryId",
                table: "Budgets");

            migrationBuilder.RenameColumn(
                name: "CategoryId",
                table: "Budgets",
                newName: "USCategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_Budgets_CategoryId",
                table: "Budgets",
                newName: "IX_Budgets_USCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Budgets_UserSelectedCategories_USCategoryId",
                table: "Budgets",
                column: "USCategoryId",
                principalTable: "UserSelectedCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Budgets_UserSelectedCategories_USCategoryId",
                table: "Budgets");

            migrationBuilder.RenameColumn(
                name: "USCategoryId",
                table: "Budgets",
                newName: "CategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_Budgets_USCategoryId",
                table: "Budgets",
                newName: "IX_Budgets_CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Budgets_Categories_CategoryId",
                table: "Budgets",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
