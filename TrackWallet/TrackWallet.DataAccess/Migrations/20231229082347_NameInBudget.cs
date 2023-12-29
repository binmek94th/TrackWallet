using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrackWallet.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class NameInBudget : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Budgets",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Budgets");
        }
    }
}
