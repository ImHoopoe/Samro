using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Samro.DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class AddParticipents : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TournamentParticipant_Roles_RoleId",
                table: "TournamentParticipant");

            migrationBuilder.DropForeignKey(
                name: "FK_TournamentParticipant_Tournaments_TournamentId",
                table: "TournamentParticipant");

            migrationBuilder.DropForeignKey(
                name: "FK_TournamentParticipant_Users_UserId",
                table: "TournamentParticipant");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TournamentParticipant",
                table: "TournamentParticipant");

            migrationBuilder.RenameTable(
                name: "TournamentParticipant",
                newName: "TournamentParticipants");

            migrationBuilder.RenameIndex(
                name: "IX_TournamentParticipant_UserId",
                table: "TournamentParticipants",
                newName: "IX_TournamentParticipants_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_TournamentParticipant_TournamentId",
                table: "TournamentParticipants",
                newName: "IX_TournamentParticipants_TournamentId");

            migrationBuilder.RenameIndex(
                name: "IX_TournamentParticipant_RoleId",
                table: "TournamentParticipants",
                newName: "IX_TournamentParticipants_RoleId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TournamentParticipants",
                table: "TournamentParticipants",
                column: "TournamentParticipantId");

            migrationBuilder.AddForeignKey(
                name: "FK_TournamentParticipants_Roles_RoleId",
                table: "TournamentParticipants",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "RoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_TournamentParticipants_Tournaments_TournamentId",
                table: "TournamentParticipants",
                column: "TournamentId",
                principalTable: "Tournaments",
                principalColumn: "TournamentId");

            migrationBuilder.AddForeignKey(
                name: "FK_TournamentParticipants_Users_UserId",
                table: "TournamentParticipants",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TournamentParticipants_Roles_RoleId",
                table: "TournamentParticipants");

            migrationBuilder.DropForeignKey(
                name: "FK_TournamentParticipants_Tournaments_TournamentId",
                table: "TournamentParticipants");

            migrationBuilder.DropForeignKey(
                name: "FK_TournamentParticipants_Users_UserId",
                table: "TournamentParticipants");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TournamentParticipants",
                table: "TournamentParticipants");

            migrationBuilder.RenameTable(
                name: "TournamentParticipants",
                newName: "TournamentParticipant");

            migrationBuilder.RenameIndex(
                name: "IX_TournamentParticipants_UserId",
                table: "TournamentParticipant",
                newName: "IX_TournamentParticipant_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_TournamentParticipants_TournamentId",
                table: "TournamentParticipant",
                newName: "IX_TournamentParticipant_TournamentId");

            migrationBuilder.RenameIndex(
                name: "IX_TournamentParticipants_RoleId",
                table: "TournamentParticipant",
                newName: "IX_TournamentParticipant_RoleId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TournamentParticipant",
                table: "TournamentParticipant",
                column: "TournamentParticipantId");

            migrationBuilder.AddForeignKey(
                name: "FK_TournamentParticipant_Roles_RoleId",
                table: "TournamentParticipant",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "RoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_TournamentParticipant_Tournaments_TournamentId",
                table: "TournamentParticipant",
                column: "TournamentId",
                principalTable: "Tournaments",
                principalColumn: "TournamentId");

            migrationBuilder.AddForeignKey(
                name: "FK_TournamentParticipant_Users_UserId",
                table: "TournamentParticipant",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId");
        }
    }
}
