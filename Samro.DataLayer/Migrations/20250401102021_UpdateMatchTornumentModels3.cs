using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WinWin.DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class UpdateMatchTornumentModels3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreatedByUserId",
                table: "Tournaments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedByUserUserId",
                table: "Tournaments",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "MatchRole",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tournaments_CreatedByUserUserId",
                table: "Tournaments",
                column: "CreatedByUserUserId");

            migrationBuilder.CreateIndex(
                name: "IX_MatchRole_UserId",
                table: "MatchRole",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_MatchRole_Users_UserId",
                table: "MatchRole",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tournaments_Users_CreatedByUserUserId",
                table: "Tournaments",
                column: "CreatedByUserUserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MatchRole_Users_UserId",
                table: "MatchRole");

            migrationBuilder.DropForeignKey(
                name: "FK_Tournaments_Users_CreatedByUserUserId",
                table: "Tournaments");

            migrationBuilder.DropIndex(
                name: "IX_Tournaments_CreatedByUserUserId",
                table: "Tournaments");

            migrationBuilder.DropIndex(
                name: "IX_MatchRole_UserId",
                table: "MatchRole");

            migrationBuilder.DropColumn(
                name: "CreatedByUserId",
                table: "Tournaments");

            migrationBuilder.DropColumn(
                name: "CreatedByUserUserId",
                table: "Tournaments");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "MatchRole");
        }
    }
}
