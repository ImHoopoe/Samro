using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WinWin.DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class AddTornuments : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MatchRole_Matches_MatchId",
                table: "MatchRole");

            migrationBuilder.DropForeignKey(
                name: "FK_MatchRole_Users_UserId",
                table: "MatchRole");

            migrationBuilder.DropForeignKey(
                name: "FK_MatchRound_Matches_MatchId",
                table: "MatchRound");

            migrationBuilder.DropForeignKey(
                name: "FK_MatchScore_Matches_MatchId",
                table: "MatchScore");

            migrationBuilder.DropForeignKey(
                name: "FK_MatchWarning_Matches_MatchId",
                table: "MatchWarning");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MatchWarning",
                table: "MatchWarning");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MatchScore",
                table: "MatchScore");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MatchRound",
                table: "MatchRound");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MatchRole",
                table: "MatchRole");

            migrationBuilder.RenameTable(
                name: "MatchWarning",
                newName: "MatchWarnings");

            migrationBuilder.RenameTable(
                name: "MatchScore",
                newName: "MatchScores");

            migrationBuilder.RenameTable(
                name: "MatchRound",
                newName: "MatchRounds");

            migrationBuilder.RenameTable(
                name: "MatchRole",
                newName: "MatchRoles");

            migrationBuilder.RenameIndex(
                name: "IX_MatchWarning_MatchId",
                table: "MatchWarnings",
                newName: "IX_MatchWarnings_MatchId");

            migrationBuilder.RenameIndex(
                name: "IX_MatchScore_MatchId",
                table: "MatchScores",
                newName: "IX_MatchScores_MatchId");

            migrationBuilder.RenameIndex(
                name: "IX_MatchRound_MatchId",
                table: "MatchRounds",
                newName: "IX_MatchRounds_MatchId");

            migrationBuilder.RenameIndex(
                name: "IX_MatchRole_UserId",
                table: "MatchRoles",
                newName: "IX_MatchRoles_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_MatchRole_MatchId",
                table: "MatchRoles",
                newName: "IX_MatchRoles_MatchId");

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "MatchWarnings",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "MatchScores",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "MatchRounds",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "MatchRoles",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_MatchWarnings",
                table: "MatchWarnings",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MatchScores",
                table: "MatchScores",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MatchRounds",
                table: "MatchRounds",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MatchRoles",
                table: "MatchRoles",
                column: "MatchRoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_MatchRoles_Matches_MatchId",
                table: "MatchRoles",
                column: "MatchId",
                principalTable: "Matches",
                principalColumn: "MatchId");

            migrationBuilder.AddForeignKey(
                name: "FK_MatchRoles_Users_UserId",
                table: "MatchRoles",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_MatchRounds_Matches_MatchId",
                table: "MatchRounds",
                column: "MatchId",
                principalTable: "Matches",
                principalColumn: "MatchId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MatchScores_Matches_MatchId",
                table: "MatchScores",
                column: "MatchId",
                principalTable: "Matches",
                principalColumn: "MatchId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MatchWarnings_Matches_MatchId",
                table: "MatchWarnings",
                column: "MatchId",
                principalTable: "Matches",
                principalColumn: "MatchId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MatchRoles_Matches_MatchId",
                table: "MatchRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_MatchRoles_Users_UserId",
                table: "MatchRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_MatchRounds_Matches_MatchId",
                table: "MatchRounds");

            migrationBuilder.DropForeignKey(
                name: "FK_MatchScores_Matches_MatchId",
                table: "MatchScores");

            migrationBuilder.DropForeignKey(
                name: "FK_MatchWarnings_Matches_MatchId",
                table: "MatchWarnings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MatchWarnings",
                table: "MatchWarnings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MatchScores",
                table: "MatchScores");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MatchRounds",
                table: "MatchRounds");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MatchRoles",
                table: "MatchRoles");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "MatchWarnings");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "MatchScores");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "MatchRounds");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "MatchRoles");

            migrationBuilder.RenameTable(
                name: "MatchWarnings",
                newName: "MatchWarning");

            migrationBuilder.RenameTable(
                name: "MatchScores",
                newName: "MatchScore");

            migrationBuilder.RenameTable(
                name: "MatchRounds",
                newName: "MatchRound");

            migrationBuilder.RenameTable(
                name: "MatchRoles",
                newName: "MatchRole");

            migrationBuilder.RenameIndex(
                name: "IX_MatchWarnings_MatchId",
                table: "MatchWarning",
                newName: "IX_MatchWarning_MatchId");

            migrationBuilder.RenameIndex(
                name: "IX_MatchScores_MatchId",
                table: "MatchScore",
                newName: "IX_MatchScore_MatchId");

            migrationBuilder.RenameIndex(
                name: "IX_MatchRounds_MatchId",
                table: "MatchRound",
                newName: "IX_MatchRound_MatchId");

            migrationBuilder.RenameIndex(
                name: "IX_MatchRoles_UserId",
                table: "MatchRole",
                newName: "IX_MatchRole_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_MatchRoles_MatchId",
                table: "MatchRole",
                newName: "IX_MatchRole_MatchId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MatchWarning",
                table: "MatchWarning",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MatchScore",
                table: "MatchScore",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MatchRound",
                table: "MatchRound",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MatchRole",
                table: "MatchRole",
                column: "MatchRoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_MatchRole_Matches_MatchId",
                table: "MatchRole",
                column: "MatchId",
                principalTable: "Matches",
                principalColumn: "MatchId");

            migrationBuilder.AddForeignKey(
                name: "FK_MatchRole_Users_UserId",
                table: "MatchRole",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_MatchRound_Matches_MatchId",
                table: "MatchRound",
                column: "MatchId",
                principalTable: "Matches",
                principalColumn: "MatchId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MatchScore_Matches_MatchId",
                table: "MatchScore",
                column: "MatchId",
                principalTable: "Matches",
                principalColumn: "MatchId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MatchWarning_Matches_MatchId",
                table: "MatchWarning",
                column: "MatchId",
                principalTable: "Matches",
                principalColumn: "MatchId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
