using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AccesoDatos.Migrations
{
    /// <inheritdoc />
    public partial class ModCuentaCorrienteOnDeleteBehavior : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CuentaCorrienteAutorizados_CuentasCorrientes_CuentaCorrienteId",
                table: "CuentaCorrienteAutorizados");

            migrationBuilder.AddForeignKey(
                name: "FK_CuentaCorrienteAutorizados_CuentasCorrientes_CuentaCorrienteId",
                table: "CuentaCorrienteAutorizados",
                column: "CuentaCorrienteId",
                principalTable: "CuentasCorrientes",
                principalColumn: "CuentaCorrienteId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CuentaCorrienteAutorizados_CuentasCorrientes_CuentaCorrienteId",
                table: "CuentaCorrienteAutorizados");

            migrationBuilder.AddForeignKey(
                name: "FK_CuentaCorrienteAutorizados_CuentasCorrientes_CuentaCorrienteId",
                table: "CuentaCorrienteAutorizados",
                column: "CuentaCorrienteId",
                principalTable: "CuentasCorrientes",
                principalColumn: "CuentaCorrienteId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
