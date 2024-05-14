using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class asdg : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_User_BasketPosition_BasketPositionId",
                table: "User");

            migrationBuilder.DropIndex(
                name: "IX_User_BasketPositionId",
                table: "User");

            migrationBuilder.DropColumn(
                name: "BasketPositionId",
                table: "User");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "BasketPosition",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_BasketPosition_UserId",
                table: "BasketPosition",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_BasketPosition_User_UserId",
                table: "BasketPosition",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BasketPosition_User_UserId",
                table: "BasketPosition");

            migrationBuilder.DropIndex(
                name: "IX_BasketPosition_UserId",
                table: "BasketPosition");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "BasketPosition");

            migrationBuilder.AddColumn<int>(
                name: "BasketPositionId",
                table: "User",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_User_BasketPositionId",
                table: "User",
                column: "BasketPositionId");

            migrationBuilder.AddForeignKey(
                name: "FK_User_BasketPosition_BasketPositionId",
                table: "User",
                column: "BasketPositionId",
                principalTable: "BasketPosition",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
