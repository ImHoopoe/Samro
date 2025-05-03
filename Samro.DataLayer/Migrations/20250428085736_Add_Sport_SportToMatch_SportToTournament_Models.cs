using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WinWin.DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class Add_Sport_SportToMatch_SportToTournament_Models : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Sports",
                columns: table => new
                {
                    SportId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SportName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ParentId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sports", x => x.SportId);
                    table.ForeignKey(
                        name: "FK_Sports_Sports_ParentId",
                        column: x => x.ParentId,
                        principalTable: "Sports",
                        principalColumn: "SportId");
                });

            migrationBuilder.CreateTable(
                name: "SportToMatch",
                columns: table => new
                {
                    SportToMatchId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SportId = table.Column<int>(type: "int", nullable: false),
                    MatchId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SportToMatch", x => x.SportToMatchId);
                    table.ForeignKey(
                        name: "FK_SportToMatch_Matches_MatchId",
                        column: x => x.MatchId,
                        principalTable: "Matches",
                        principalColumn: "MatchId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SportToMatch_Sports_SportId",
                        column: x => x.SportId,
                        principalTable: "Sports",
                        principalColumn: "SportId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SportToTournaments",
                columns: table => new
                {
                    SportToTournamentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SportId = table.Column<int>(type: "int", nullable: false),
                    TournamentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SportToTournaments", x => x.SportToTournamentId);
                    table.ForeignKey(
                        name: "FK_SportToTournaments_Sports_SportId",
                        column: x => x.SportId,
                        principalTable: "Sports",
                        principalColumn: "SportId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SportToTournaments_Tournaments_TournamentId",
                        column: x => x.TournamentId,
                        principalTable: "Tournaments",
                        principalColumn: "TournamentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Sports_ParentId",
                table: "Sports",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_SportToMatch_MatchId",
                table: "SportToMatch",
                column: "MatchId");

            migrationBuilder.CreateIndex(
                name: "IX_SportToMatch_SportId",
                table: "SportToMatch",
                column: "SportId");

            migrationBuilder.CreateIndex(
                name: "IX_SportToTournaments_SportId",
                table: "SportToTournaments",
                column: "SportId");

            migrationBuilder.CreateIndex(
                name: "IX_SportToTournaments_TournamentId",
                table: "SportToTournaments",
                column: "TournamentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SportToMatch");

            migrationBuilder.DropTable(
                name: "SportToTournaments");

            migrationBuilder.DropTable(
                name: "Sports");
        }
    }
}
