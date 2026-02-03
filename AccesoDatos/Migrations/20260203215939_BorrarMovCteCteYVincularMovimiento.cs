using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AccesoDatos.Migrations
{
    /// <inheritdoc />
    public partial class BorrarMovCteCteYVincularMovimiento : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MovimientosCuentaCorrientes");

            migrationBuilder.AddColumn<long>(
                name: "id_CuentaCorriente",
                table: "Movimientos",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Movimientos_id_CuentaCorriente",
                table: "Movimientos",
                column: "id_CuentaCorriente");

            migrationBuilder.AddForeignKey(
                name: "FK_Movimientos_CuentasCorrientes_id_CuentaCorriente",
                table: "Movimientos",
                column: "id_CuentaCorriente",
                principalTable: "CuentasCorrientes",
                principalColumn: "CuentaCorrienteId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Movimientos_CuentasCorrientes_id_CuentaCorriente",
                table: "Movimientos");

            migrationBuilder.DropIndex(
                name: "IX_Movimientos_id_CuentaCorriente",
                table: "Movimientos");

            migrationBuilder.DropColumn(
                name: "id_CuentaCorriente",
                table: "Movimientos");

            migrationBuilder.CreateTable(
                name: "MovimientosCuentaCorrientes",
                columns: table => new
                {
                    MovimientoCuentaCorrienteId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CuentaCorrienteId = table.Column<long>(type: "bigint", nullable: false),
                    descripcion = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    fecha = table.Column<DateTime>(type: "date", nullable: false),
                    monto = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    tipo_movimiento = table.Column<int>(type: "int", nullable: false)
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
                name: "IX_MovimientosCuentaCorrientes_CuentaCorrienteId",
                table: "MovimientosCuentaCorrientes",
                column: "CuentaCorrienteId");
        }
    }
}
