using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WinWin.DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class UpdateMatchTornumentModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EventType",
                table: "Tournaments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "Matches",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Location",
                table: "Matches",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "MatchRole",
                columns: table => new
                {
                    MatchRoleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MatchRoleName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MatchRoleDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MatchId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MatchRole", x => x.MatchRoleId);
                    table.ForeignKey(
                        name: "FK_MatchRole_Matches_MatchId",
                        column: x => x.MatchId,
                        principalTable: "Matches",
                        principalColumn: "MatchId");
                });

            migrationBuilder.CreateTable(
                name: "MatchRound",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MatchId = table.Column<int>(type: "int", nullable: false),
                    RoundNumber = table.Column<int>(type: "int", nullable: false),
                    IsCompleted = table.Column<bool>(type: "bit", nullable: false),
                    Duration = table.Column<TimeSpan>(type: "time", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MatchRound", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MatchRound_Matches_MatchId",
                        column: x => x.MatchId,
                        principalTable: "Matches",
                        principalColumn: "MatchId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MatchScore",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MatchId = table.Column<int>(type: "int", nullable: false),
                    PlayerRedScore = table.Column<int>(type: "int", nullable: false),
                    PlayerBlueScore = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MatchScore", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MatchScore_Matches_MatchId",
                        column: x => x.MatchId,
                        principalTable: "Matches",
                        principalColumn: "MatchId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MatchWarning",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MatchId = table.Column<int>(type: "int", nullable: false),
                    PlayerNumber = table.Column<int>(type: "int", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Reason = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IssuedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MatchWarning", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MatchWarning_Matches_MatchId",
                        column: x => x.MatchId,
                        principalTable: "Matches",
                        principalColumn: "MatchId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MatchRole_MatchId",
                table: "MatchRole",
                column: "MatchId");

            migrationBuilder.CreateIndex(
                name: "IX_MatchRound_MatchId",
                table: "MatchRound",
                column: "MatchId");

            migrationBuilder.CreateIndex(
                name: "IX_MatchScore_MatchId",
                table: "MatchScore",
                column: "MatchId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MatchWarning_MatchId",
                table: "MatchWarning",
                column: "MatchId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MatchRole");

            migrationBuilder.DropTable(
                name: "MatchRound");

            migrationBuilder.DropTable(
                name: "MatchScore");

            migrationBuilder.DropTable(
                name: "MatchWarning");

            migrationBuilder.DropColumn(
                name: "EventType",
                table: "Tournaments");

            migrationBuilder.DropColumn(
                name: "Date",
                table: "Matches");

            migrationBuilder.DropColumn(
                name: "Location",
                table: "Matches");
        }
    }
}
