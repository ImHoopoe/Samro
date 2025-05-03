using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WinWin.DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class Remove_SportToTournament : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SportToTournaments");

            migrationBuilder.CreateIndex(
                name: "IX_Tournaments_SportId",
                table: "Tournaments",
                column: "SportId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tournaments_Sports_SportId",
                table: "Tournaments",
                column: "SportId",
                principalTable: "Sports",
                principalColumn: "SportId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tournaments_Sports_SportId",
                table: "Tournaments");

            migrationBuilder.DropIndex(
                name: "IX_Tournaments_SportId",
                table: "Tournaments");

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
                name: "IX_SportToTournaments_SportId",
                table: "SportToTournaments",
                column: "SportId");

            migrationBuilder.CreateIndex(
                name: "IX_SportToTournaments_TournamentId",
                table: "SportToTournaments",
                column: "TournamentId");
        }
    }
}
