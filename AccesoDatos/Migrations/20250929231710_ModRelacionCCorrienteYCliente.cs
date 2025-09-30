using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AccesoDatos.Migrations
{
    /// <inheritdoc />
    public partial class ModRelacionCCorrienteYCliente : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Clientes_CuentasCorrientes_CuentaCorrienteId",
                table: "Clientes");

            migrationBuilder.DropIndex(
                name: "IX_Clientes_CuentaCorrienteId",
                table: "Clientes");

            migrationBuilder.AddColumn<long>(
                name: "ClienteId",
                table: "CuentasCorrientes",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_CuentasCorrientes_ClienteId",
                table: "CuentasCorrientes",
                column: "ClienteId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_CuentasCorrientes_Clientes_ClienteId",
                table: "CuentasCorrientes",
                column: "ClienteId",
                principalTable: "Clientes",
                principalColumn: "PersonaId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CuentasCorrientes_Clientes_ClienteId",
                table: "CuentasCorrientes");

            migrationBuilder.DropIndex(
                name: "IX_CuentasCorrientes_ClienteId",
                table: "CuentasCorrientes");

            migrationBuilder.DropColumn(
                name: "ClienteId",
                table: "CuentasCorrientes");

            migrationBuilder.CreateIndex(
                name: "IX_Clientes_CuentaCorrienteId",
                table: "Clientes",
                column: "CuentaCorrienteId");

            migrationBuilder.AddForeignKey(
                name: "FK_Clientes_CuentasCorrientes_CuentaCorrienteId",
                table: "Clientes",
                column: "CuentaCorrienteId",
                principalTable: "CuentasCorrientes",
                principalColumn: "CuentaCorrienteId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
