using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WinWin.DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTournamentModel_AddProperties_Time_Players_Registered : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MatchDate",
                table: "Tournaments");

            migrationBuilder.RenameColumn(
                name: "EventType",
                table: "Tournaments",
                newName: "TournamentType");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "WeighInDate",
                table: "Tournaments",
                type: "datetimeoffset",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "FaceToFaceDate",
                table: "Tournaments",
                type: "datetimeoffset",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<bool>(
                name: "IsFull",
                table: "Tournaments",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsTimeEnds",
                table: "Tournaments",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "MaximnumPlayers",
                table: "Tournaments",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "RegsiterEndsAt",
                table: "Tournaments",
                type: "datetimeoffset",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<DateTime>(
                name: "RegsiterStartsAt",
                table: "Tournaments",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Thumbnail",
                table: "Tournaments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "TournamentUsers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TournamentId = table.Column<int>(type: "int", nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TournamentUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TournamentUsers_Tournaments_TournamentId",
                        column: x => x.TournamentId,
                        principalTable: "Tournaments",
                        principalColumn: "TournamentId");
                    table.ForeignKey(
                        name: "FK_TournamentUsers_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_TournamentUsers_TournamentId",
                table: "TournamentUsers",
                column: "TournamentId");

            migrationBuilder.CreateIndex(
                name: "IX_TournamentUsers_UserId",
                table: "TournamentUsers",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TournamentUsers");

            migrationBuilder.DropColumn(
                name: "IsFull",
                table: "Tournaments");

            migrationBuilder.DropColumn(
                name: "IsTimeEnds",
                table: "Tournaments");

            migrationBuilder.DropColumn(
                name: "MaximnumPlayers",
                table: "Tournaments");

            migrationBuilder.DropColumn(
                name: "RegsiterEndsAt",
                table: "Tournaments");

            migrationBuilder.DropColumn(
                name: "RegsiterStartsAt",
                table: "Tournaments");

            migrationBuilder.DropColumn(
                name: "Thumbnail",
                table: "Tournaments");

            migrationBuilder.RenameColumn(
                name: "TournamentType",
                table: "Tournaments",
                newName: "EventType");

            migrationBuilder.AlterColumn<DateTime>(
                name: "WeighInDate",
                table: "Tournaments",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset");

            migrationBuilder.AlterColumn<DateTime>(
                name: "FaceToFaceDate",
                table: "Tournaments",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset");

            migrationBuilder.AddColumn<DateTime>(
                name: "MatchDate",
                table: "Tournaments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
