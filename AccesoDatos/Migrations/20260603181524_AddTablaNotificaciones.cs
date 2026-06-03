using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AccesoDatos.Migrations
{
    /// <inheritdoc />
    public partial class AddTablaNotificaciones : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Notificaciones",
                columns: table => new
                {
                    id_notificacion = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    titulo = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    descripcion = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    mensaje = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    empleado_id = table.Column<long>(type: "bigint", nullable: false),
                    fecha_creacion = table.Column<DateTime>(type: "datetime", nullable: false),
                    fecha_confirmacion = table.Column<DateTime>(type: "datetime", nullable: false),
                    esta_leida = table.Column<bool>(type: "bit", nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notificaciones", x => x.id_notificacion);
                    table.ForeignKey(
                        name: "FK_Notificaciones_Empleados_empleado_id",
                        column: x => x.empleado_id,
                        principalTable: "Empleados",
                        principalColumn: "PersonaId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Notificaciones_empleado_id",
                table: "Notificaciones",
                column: "empleado_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Notificaciones");
        }
    }
}
