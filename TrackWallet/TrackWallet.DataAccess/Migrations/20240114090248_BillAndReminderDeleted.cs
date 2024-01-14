using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrackWallet.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class BillAndReminderDeleted : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_BillAndReminder_BillAndReminderId",
                table: "Transactions");

            migrationBuilder.DropTable(
                name: "BillAndReminder");

            migrationBuilder.DropIndex(
                name: "IX_Transactions_BillAndReminderId",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "BillAndReminderId",
                table: "Transactions");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BillAndReminderId",
                table: "Transactions",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "BillAndReminder",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EventId = table.Column<int>(type: "int", nullable: true),
                    USCategoryId = table.Column<int>(type: "int", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    amount = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BillAndReminder", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BillAndReminder_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BillAndReminder_Events_EventId",
                        column: x => x.EventId,
                        principalTable: "Events",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BillAndReminder_UserSelectedCategories_USCategoryId",
                        column: x => x.USCategoryId,
                        principalTable: "UserSelectedCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_BillAndReminderId",
                table: "Transactions",
                column: "BillAndReminderId");

            migrationBuilder.CreateIndex(
                name: "IX_BillAndReminder_EventId",
                table: "BillAndReminder",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_BillAndReminder_USCategoryId",
                table: "BillAndReminder",
                column: "USCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_BillAndReminder_UserId",
                table: "BillAndReminder",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_BillAndReminder_BillAndReminderId",
                table: "Transactions",
                column: "BillAndReminderId",
                principalTable: "BillAndReminder",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
