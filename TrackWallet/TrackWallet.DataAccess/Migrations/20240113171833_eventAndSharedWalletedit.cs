using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrackWallet.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class eventAndSharedWalletedit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EventId",
                table: "SharedWallets",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_SharedWallets_EventId",
                table: "SharedWallets",
                column: "EventId");

            migrationBuilder.AddForeignKey(
                name: "FK_SharedWallets_Events_EventId",
                table: "SharedWallets",
                column: "EventId",
                principalTable: "Events",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SharedWallets_Events_EventId",
                table: "SharedWallets");

            migrationBuilder.DropIndex(
                name: "IX_SharedWallets_EventId",
                table: "SharedWallets");

            migrationBuilder.DropColumn(
                name: "EventId",
                table: "SharedWallets");
        }
    }
}
