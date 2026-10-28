using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AccesoDatos.Migrations
{
    /// <inheritdoc />
    public partial class Movimientos_Etapa1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "id_VentaOriginal",
                table: "VentasLibres",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "id_VentaOriginal",
                table: "Ventas",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "afecta_caja",
                table: "Movimientos",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<long>(
                name: "id_TipoPago",
                table: "Movimientos",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "id_caja",
                table: "Movimientos",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "id_movimiento_origen",
                table: "Movimientos",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "origen_pago",
                table: "Gastos",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "UX_VentasLibres_VentaOriginalId",
                table: "VentasLibres",
                column: "id_VentaOriginal",
                unique: true,
                filter: "[id_VentaOriginal] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "UX_Ventas_VentaOriginalId",
                table: "Ventas",
                column: "id_VentaOriginal",
                unique: true,
                filter: "[id_VentaOriginal] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Movimientos_CajaId_FechaMovimiento",
                table: "Movimientos",
                columns: new[] { "id_caja", "fecha_movimiento" });

            migrationBuilder.CreateIndex(
                name: "IX_Movimientos_id_movimiento_origen",
                table: "Movimientos",
                column: "id_movimiento_origen");

            migrationBuilder.CreateIndex(
                name: "IX_Movimientos_id_TipoPago",
                table: "Movimientos",
                column: "id_TipoPago");

            migrationBuilder.CreateIndex(
                name: "IX_Movimientos_Operacion_FechaMovimiento",
                table: "Movimientos",
                columns: new[] { "tipo_movimiento_detalle", "fecha_movimiento" });

            migrationBuilder.CreateIndex(
                name: "IX_Movimientos_TipoEntidad_EntidadId",
                table: "Movimientos",
                columns: new[] { "tipo_entidad", "entidad_id" });

            migrationBuilder.AddForeignKey(
                name: "FK_Movimientos_Cajas_id_caja",
                table: "Movimientos",
                column: "id_caja",
                principalTable: "Cajas",
                principalColumn: "CajaId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Movimientos_Movimientos_id_movimiento_origen",
                table: "Movimientos",
                column: "id_movimiento_origen",
                principalTable: "Movimientos",
                principalColumn: "id_movimiento",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Movimientos_TiposPago_id_TipoPago",
                table: "Movimientos",
                column: "id_TipoPago",
                principalTable: "TiposPago",
                principalColumn: "id_TipoPago",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Ventas_Ventas_id_VentaOriginal",
                table: "Ventas",
                column: "id_VentaOriginal",
                principalTable: "Ventas",
                principalColumn: "id_Venta",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_VentasLibres_VentasLibres_id_VentaOriginal",
                table: "VentasLibres",
                column: "id_VentaOriginal",
                principalTable: "VentasLibres",
                principalColumn: "id_VentaLibre",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Movimientos_Cajas_id_caja",
                table: "Movimientos");

            migrationBuilder.DropForeignKey(
                name: "FK_Movimientos_Movimientos_id_movimiento_origen",
                table: "Movimientos");

            migrationBuilder.DropForeignKey(
                name: "FK_Movimientos_TiposPago_id_TipoPago",
                table: "Movimientos");

            migrationBuilder.DropForeignKey(
                name: "FK_Ventas_Ventas_id_VentaOriginal",
                table: "Ventas");

            migrationBuilder.DropForeignKey(
                name: "FK_VentasLibres_VentasLibres_id_VentaOriginal",
                table: "VentasLibres");

            migrationBuilder.DropIndex(
                name: "UX_VentasLibres_VentaOriginalId",
                table: "VentasLibres");

            migrationBuilder.DropIndex(
                name: "UX_Ventas_VentaOriginalId",
                table: "Ventas");

            migrationBuilder.DropIndex(
                name: "IX_Movimientos_CajaId_FechaMovimiento",
                table: "Movimientos");

            migrationBuilder.DropIndex(
                name: "IX_Movimientos_id_movimiento_origen",
                table: "Movimientos");

            migrationBuilder.DropIndex(
                name: "IX_Movimientos_id_TipoPago",
                table: "Movimientos");

            migrationBuilder.DropIndex(
                name: "IX_Movimientos_Operacion_FechaMovimiento",
                table: "Movimientos");

            migrationBuilder.DropIndex(
                name: "IX_Movimientos_TipoEntidad_EntidadId",
                table: "Movimientos");

            migrationBuilder.DropColumn(
                name: "id_VentaOriginal",
                table: "VentasLibres");

            migrationBuilder.DropColumn(
                name: "id_VentaOriginal",
                table: "Ventas");

            migrationBuilder.DropColumn(
                name: "afecta_caja",
                table: "Movimientos");

            migrationBuilder.DropColumn(
                name: "id_TipoPago",
                table: "Movimientos");

            migrationBuilder.DropColumn(
                name: "id_caja",
                table: "Movimientos");

            migrationBuilder.DropColumn(
                name: "id_movimiento_origen",
                table: "Movimientos");

            migrationBuilder.DropColumn(
                name: "origen_pago",
                table: "Gastos");
        }
    }
}
