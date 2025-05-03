using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WinWin.DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class AddUsersRolesPermission : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Permisions_Roles_RoleId",
                table: "Permisions");

            migrationBuilder.DropColumn(
                name: "PermisionName",
                table: "Permisions");

            migrationBuilder.RenameColumn(
                name: "PermisionId",
                table: "Permisions",
                newName: "PermissonId");

            migrationBuilder.AlterColumn<int>(
                name: "RoleId",
                table: "Permisions",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "ParentId",
                table: "Permisions",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PermissonTitle",
                table: "Permisions",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "RolePermisions",
                columns: table => new
                {
                    RpI = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    PermissonId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RolePermisions", x => x.RpI);
                    table.ForeignKey(
                        name: "FK_RolePermisions_Permisions_PermissonId",
                        column: x => x.PermissonId,
                        principalTable: "Permisions",
                        principalColumn: "PermissonId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RolePermisions_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "RoleId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Permisions_ParentId",
                table: "Permisions",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_RolePermisions_PermissonId",
                table: "RolePermisions",
                column: "PermissonId");

            migrationBuilder.CreateIndex(
                name: "IX_RolePermisions_RoleId",
                table: "RolePermisions",
                column: "RoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Permisions_Permisions_ParentId",
                table: "Permisions",
                column: "ParentId",
                principalTable: "Permisions",
                principalColumn: "PermissonId");

            migrationBuilder.AddForeignKey(
                name: "FK_Permisions_Roles_RoleId",
                table: "Permisions",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "RoleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Permisions_Permisions_ParentId",
                table: "Permisions");

            migrationBuilder.DropForeignKey(
                name: "FK_Permisions_Roles_RoleId",
                table: "Permisions");

            migrationBuilder.DropTable(
                name: "RolePermisions");

            migrationBuilder.DropIndex(
                name: "IX_Permisions_ParentId",
                table: "Permisions");

            migrationBuilder.DropColumn(
                name: "ParentId",
                table: "Permisions");

            migrationBuilder.DropColumn(
                name: "PermissonTitle",
                table: "Permisions");

            migrationBuilder.RenameColumn(
                name: "PermissonId",
                table: "Permisions",
                newName: "PermisionId");

            migrationBuilder.AlterColumn<int>(
                name: "RoleId",
                table: "Permisions",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PermisionName",
                table: "Permisions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_Permisions_Roles_RoleId",
                table: "Permisions",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "RoleId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
