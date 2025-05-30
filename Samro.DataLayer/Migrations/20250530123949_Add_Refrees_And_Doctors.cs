using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WinWin.DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class Add_Refrees_And_Doctors : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TournamentDoctorId",
                table: "Tournaments",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TournamentRefereeId",
                table: "Tournaments",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "TournamentDoctor",
                columns: table => new
                {
                    TournamentDoctorId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TournamentId = table.Column<int>(type: "int", nullable: true),
                    IsAdmin = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TournamentDoctor", x => x.TournamentDoctorId);
                    table.ForeignKey(
                        name: "FK_TournamentDoctor_Tournaments_TournamentId",
                        column: x => x.TournamentId,
                        principalTable: "Tournaments",
                        principalColumn: "TournamentId");
                    table.ForeignKey(
                        name: "FK_TournamentDoctor_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "TournamentReferees",
                columns: table => new
                {
                    TournamentRefereesId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TournamentId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TournamentReferees", x => x.TournamentRefereesId);
                    table.ForeignKey(
                        name: "FK_TournamentReferees_Tournaments_TournamentId",
                        column: x => x.TournamentId,
                        principalTable: "Tournaments",
                        principalColumn: "TournamentId");
                    table.ForeignKey(
                        name: "FK_TournamentReferees_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_TournamentDoctor_TournamentId",
                table: "TournamentDoctor",
                column: "TournamentId");

            migrationBuilder.CreateIndex(
                name: "IX_TournamentDoctor_UserId",
                table: "TournamentDoctor",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_TournamentReferees_TournamentId",
                table: "TournamentReferees",
                column: "TournamentId");

            migrationBuilder.CreateIndex(
                name: "IX_TournamentReferees_UserId",
                table: "TournamentReferees",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TournamentDoctor");

            migrationBuilder.DropTable(
                name: "TournamentReferees");

            migrationBuilder.DropColumn(
                name: "TournamentDoctorId",
                table: "Tournaments");

            migrationBuilder.DropColumn(
                name: "TournamentRefereeId",
                table: "Tournaments");
        }
    }
}
