using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WinWin.DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class Add_SportId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SportId",
                table: "Tournaments",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SportId",
                table: "Matches",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SportId",
                table: "Tournaments");

            migrationBuilder.DropColumn(
                name: "SportId",
                table: "Matches");
        }
    }
}
