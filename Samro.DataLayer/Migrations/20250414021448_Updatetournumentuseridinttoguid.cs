using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WinWin.DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class Updatetournumentuseridinttoguid : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tournaments_Users_CreatedByUserUserId",
                table: "Tournaments");

            migrationBuilder.DropIndex(
                name: "IX_Tournaments_CreatedByUserUserId",
                table: "Tournaments");

            migrationBuilder.DropColumn(
                name: "CreatedByUserUserId",
                table: "Tournaments");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "RegsiterStartsAt",
                table: "Tournaments",
                type: "datetimeoffset",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "CreatedByUserId",
                table: "Tournaments",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Tournaments_CreatedByUserId",
                table: "Tournaments",
                column: "CreatedByUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tournaments_Users_CreatedByUserId",
                table: "Tournaments",
                column: "CreatedByUserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tournaments_Users_CreatedByUserId",
                table: "Tournaments");

            migrationBuilder.DropIndex(
                name: "IX_Tournaments_CreatedByUserId",
                table: "Tournaments");

            migrationBuilder.AlterColumn<DateTime>(
                name: "RegsiterStartsAt",
                table: "Tournaments",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedByUserId",
                table: "Tournaments",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedByUserUserId",
                table: "Tournaments",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Tournaments_CreatedByUserUserId",
                table: "Tournaments",
                column: "CreatedByUserUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tournaments_Users_CreatedByUserUserId",
                table: "Tournaments",
                column: "CreatedByUserUserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
