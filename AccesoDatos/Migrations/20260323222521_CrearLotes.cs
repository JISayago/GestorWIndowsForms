using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AccesoDatos.Migrations
{
    /// <inheritdoc />
    public partial class CrearLotes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            /*migrationBuilder.DropForeignKey(
                name: "FK_Movimientos_Cajas_CajaId",
                table: "Movimientos");

            migrationBuilder.DropForeignKey(
                name: "FK_Movimientos_CuentasCorrientes_CuentaCorrienteId",
                table: "Movimientos");

            migrationBuilder.DropIndex(
                name: "IX_Movimientos_CajaId",
                table: "Movimientos");

            migrationBuilder.DropIndex(
                name: "IX_Movimientos_CuentaCorrienteId",
                table: "Movimientos");

            migrationBuilder.DropColumn(
                name: "CajaId",
                table: "Movimientos");

            migrationBuilder.DropColumn(
                name: "CuentaCorrienteId",
                table: "Movimientos");*/

            /*migrationBuilder.AddColumn<bool>(
                name: "control_por_lote",
                table: "Productos",
                type: "bit",
                nullable: false,
                defaultValue: false); MONCHO DESCOMENTAME NO TENES CONTROL POR LOTE EN PRODUCTOSSSSS*/

            migrationBuilder.CreateTable(
                name: "Lotes",
                columns: table => new
                {
                    id_Lote = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    id_Producto = table.Column<long>(type: "bigint", nullable: false),
                    stock_inicial = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    stock_actual = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    numero_lote = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    descripcion = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    fecha_alta = table.Column<DateTime>(type: "datetime2", nullable: false),
                    fecha_vencimiento = table.Column<DateTime>(type: "datetime2", nullable: true),
                    esta_vencido = table.Column<bool>(type: "bit", nullable: false),
                    esta_activo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lotes", x => x.id_Lote);
                    table.ForeignKey(
                        name: "FK_Lotes_Productos_id_Producto",
                        column: x => x.id_Producto,
                        principalTable: "Productos",
                        principalColumn: "ProductoId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Lotes_id_Producto",
                table: "Lotes",
                column: "id_Producto");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Lotes");

            /*migrationBuilder.DropColumn(
                name: "control_por_lote",
                table: "Productos"); MONCHO DESCOMENTAME PORFAVORRRRR*/

            /*migrationBuilder.AddColumn<long>(
                name: "CajaId",
                table: "Movimientos",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "CuentaCorrienteId",
                table: "Movimientos",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Movimientos_CajaId",
                table: "Movimientos",
                column: "CajaId");

            migrationBuilder.CreateIndex(
                name: "IX_Movimientos_CuentaCorrienteId",
                table: "Movimientos",
                column: "CuentaCorrienteId");

            migrationBuilder.AddForeignKey(
                name: "FK_Movimientos_Cajas_CajaId",
                table: "Movimientos",
                column: "CajaId",
                principalTable: "Cajas",
                principalColumn: "CajaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Movimientos_CuentasCorrientes_CuentaCorrienteId",
                table: "Movimientos",
                column: "CuentaCorrienteId",
                principalTable: "CuentasCorrientes",
                principalColumn: "CuentaCorrienteId");*/
        }
    }
}
