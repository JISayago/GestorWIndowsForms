using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AccesoDatos.Migrations
{
    /// <inheritdoc />
    public partial class AddPermisosRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Permisos",
                columns: table => new
                {
                    id_permiso = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    codigo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    descripcion = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permisos", x => x.id_permiso);
                });

            migrationBuilder.CreateTable(
                name: "Roles_Permisos",
                columns: table => new
                {
                    id_rol = table.Column<long>(type: "bigint", nullable: false),
                    id_permiso = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles_Permisos", x => new { x.id_rol, x.id_permiso });
                    table.ForeignKey(
                        name: "FK_Roles_Permisos_Permisos_id_permiso",
                        column: x => x.id_permiso,
                        principalTable: "Permisos",
                        principalColumn: "id_permiso",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Roles_Permisos_Roles_id_rol",
                        column: x => x.id_rol,
                        principalTable: "Roles",
                        principalColumn: "id_Rol",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Permisos_codigo",
                table: "Permisos",
                column: "codigo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Roles_Permisos_id_permiso",
                table: "Roles_Permisos",
                column: "id_permiso");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Roles_Permisos");

            migrationBuilder.DropTable(
                name: "Permisos");
        }
    }
}
