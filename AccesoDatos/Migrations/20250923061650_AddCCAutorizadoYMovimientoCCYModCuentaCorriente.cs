using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AccesoDatos.Migrations
{
    /// <inheritdoc />
    public partial class AddCCAutorizadoYMovimientoCCYModCuentaCorriente : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EstadoCuentaCorriente",
                table: "CuentasCorrientes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "CuentaCorrienteAutorizados",
                columns: table => new
                {
                    CuentaCorrienteAutorizadoId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CuentaCorrienteId = table.Column<long>(type: "bigint", nullable: false),
                    dni = table.Column<long>(type: "bigint", maxLength: 15, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CuentaCorrienteAutorizados", x => x.CuentaCorrienteAutorizadoId);
                    table.ForeignKey(
                        name: "FK_CuentaCorrienteAutorizados_CuentasCorrientes_CuentaCorrienteId",
                        column: x => x.CuentaCorrienteId,
                        principalTable: "CuentasCorrientes",
                        principalColumn: "CuentaCorrienteId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MovimientosCuentaCorrientes",
                columns: table => new
                {
                    MovimientoCuentaCorrienteId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    fecha = table.Column<DateTime>(type: "date", nullable: false),
                    monto = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    descripcion = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    tipo_movimiento = table.Column<int>(type: "int", nullable: false),
                    CuentaCorrienteId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovimientosCuentaCorrientes", x => x.MovimientoCuentaCorrienteId);
                    table.ForeignKey(
                        name: "FK_MovimientosCuentaCorrientes_CuentasCorrientes_CuentaCorrienteId",
                        column: x => x.CuentaCorrienteId,
                        principalTable: "CuentasCorrientes",
                        principalColumn: "CuentaCorrienteId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CuentaCorrienteAutorizados_CuentaCorrienteId",
                table: "CuentaCorrienteAutorizados",
                column: "CuentaCorrienteId");

            migrationBuilder.CreateIndex(
                name: "IX_MovimientosCuentaCorrientes_CuentaCorrienteId",
                table: "MovimientosCuentaCorrientes",
                column: "CuentaCorrienteId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CuentaCorrienteAutorizados");

            migrationBuilder.DropTable(
                name: "MovimientosCuentaCorrientes");

            migrationBuilder.DropColumn(
                name: "EstadoCuentaCorriente",
                table: "CuentasCorrientes");
        }
    }
}
