using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WinWin.DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class AddRoleUserToEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Permisions_Roles_RoleId",
                table: "Permisions");

            migrationBuilder.DropIndex(
                name: "IX_Permisions_RoleId",
                table: "Permisions");

            migrationBuilder.DropColumn(
                name: "RoleId",
                table: "Permisions");

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Roles",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Roles");

            migrationBuilder.AddColumn<int>(
                name: "RoleId",
                table: "Permisions",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Permisions_RoleId",
                table: "Permisions",
                column: "RoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Permisions_Roles_RoleId",
                table: "Permisions",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "RoleId");
        }
    }
}
