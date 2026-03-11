using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AccesoDatos.Migrations
{
    /// <inheritdoc />
    public partial class AddTablaUsuarioSesion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Usuarios_Sesiones",
                columns: table => new
                {
                    UsuarioSesionId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UsuarioId = table.Column<long>(type: "bigint", nullable: false),
                    FechaLogin = table.Column<DateTime>(type: "datetime", nullable: false),
                    FechaLogout = table.Column<DateTime>(type: "datetime", nullable: true),
                    Activa = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios_Sesiones", x => x.UsuarioSesionId);
                    table.ForeignKey(
                        name: "FK_Usuarios_Sesiones_Empleados_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Empleados",
                        principalColumn: "PersonaId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_Sesiones_UsuarioId",
                table: "Usuarios_Sesiones",
                column: "UsuarioId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Usuarios_Sesiones");
        }
    }
}
