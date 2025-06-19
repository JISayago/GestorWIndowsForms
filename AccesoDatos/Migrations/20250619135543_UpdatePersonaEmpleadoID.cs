using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AccesoDatos.Migrations
{
    /// <inheritdoc />
    public partial class UpdatePersonaEmpleadoID : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Personas_Empleados_EmpleadoPersonaId",
                table: "Personas");

            migrationBuilder.DropIndex(
                name: "IX_Personas_EmpleadoPersonaId",
                table: "Personas");

            migrationBuilder.DropColumn(
                name: "EmpleadoPersonaId",
                table: "Personas");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "EmpleadoPersonaId",
                table: "Personas",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Personas_EmpleadoPersonaId",
                table: "Personas",
                column: "EmpleadoPersonaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Personas_Empleados_EmpleadoPersonaId",
                table: "Personas",
                column: "EmpleadoPersonaId",
                principalTable: "Empleados",
                principalColumn: "PersonaId");
        }
    }
}
