using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrackWallet.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class recurringTransactionEdit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "next",
                table: "RecurringTransactions");

            migrationBuilder.RenameColumn(
                name: "Date",
                table: "RecurringTransactions",
                newName: "nextDate");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "nextDate",
                table: "RecurringTransactions",
                newName: "Date");

            migrationBuilder.AddColumn<string>(
                name: "next",
                table: "RecurringTransactions",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
