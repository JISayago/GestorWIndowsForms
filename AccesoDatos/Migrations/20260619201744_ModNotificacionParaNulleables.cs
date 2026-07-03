using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AccesoDatos.Migrations
{
    /// <inheritdoc />
    public partial class ModNotificacionParaNulleables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_Notificaciones_Empleados_EmpleadoPersonaId",
            //    table: "Notificaciones");

            migrationBuilder.DropForeignKey(
                name: "FK_Notificaciones_Empleados_empleado_id",
                table: "Notificaciones");

            //migrationBuilder.DropIndex(
            //    name: "IX_Notificaciones_EmpleadoPersonaId",
            //    table: "Notificaciones");

            //migrationBuilder.DropColumn(
            //    name: "EmpleadoPersonaId",
            //    table: "Notificaciones");

            migrationBuilder.AlterColumn<DateTime>(
                name: "fecha_confirmacion",
                table: "Notificaciones",
                type: "datetime",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime");

            migrationBuilder.AlterColumn<long>(
                name: "empleado_id",
                table: "Notificaciones",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddColumn<DateTime>(
                name: "fecha_vencimiento",
                table: "Notificaciones",
                type: "datetime",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Notificaciones_Empleados_empleado_id",
                table: "Notificaciones",
                column: "empleado_id",
                principalTable: "Empleados",
                principalColumn: "PersonaId",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notificaciones_Empleados_empleado_id",
                table: "Notificaciones");

            migrationBuilder.DropColumn(
                name: "fecha_vencimiento",
                table: "Notificaciones");

            migrationBuilder.AlterColumn<DateTime>(
                name: "fecha_confirmacion",
                table: "Notificaciones",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "empleado_id",
                table: "Notificaciones",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddColumn<long>(
                name: "EmpleadoPersonaId",
                table: "Notificaciones",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_Notificaciones_EmpleadoPersonaId",
                table: "Notificaciones",
                column: "EmpleadoPersonaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Notificaciones_Empleados_EmpleadoPersonaId",
                table: "Notificaciones",
                column: "EmpleadoPersonaId",
                principalTable: "Empleados",
                principalColumn: "PersonaId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Notificaciones_Empleados_empleado_id",
                table: "Notificaciones",
                column: "empleado_id",
                principalTable: "Empleados",
                principalColumn: "PersonaId");
        }
    }
}
