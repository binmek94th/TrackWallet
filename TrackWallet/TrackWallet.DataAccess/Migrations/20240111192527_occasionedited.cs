using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrackWallet.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class occasionedited : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "USCategoryId",
                table: "Budgets",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Budgets",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "OccasionId",
                table: "Budgets",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Budgets_OccasionId",
                table: "Budgets",
                column: "OccasionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Budgets_Occasions_OccasionId",
                table: "Budgets",
                column: "OccasionId",
                principalTable: "Occasions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Budgets_Occasions_OccasionId",
                table: "Budgets");

            migrationBuilder.DropIndex(
                name: "IX_Budgets_OccasionId",
                table: "Budgets");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Budgets");

            migrationBuilder.DropColumn(
                name: "OccasionId",
                table: "Budgets");

            migrationBuilder.AlterColumn<int>(
                name: "USCategoryId",
                table: "Budgets",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }
    }
}
