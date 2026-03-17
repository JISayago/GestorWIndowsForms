using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AccesoDatos.Migrations
{
    /// <inheritdoc />
    public partial class CambiosMovimientosYFK : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Movimientos_Cajas_id_Caja",
                table: "Movimientos");

            migrationBuilder.DropForeignKey(
                name: "FK_Movimientos_CuentasCorrientes_id_CuentaCorriente",
                table: "Movimientos");

            migrationBuilder.DropForeignKey(
                name: "FK_Movimientos_Ventas_id_Venta",
                table: "Movimientos");

            migrationBuilder.DropIndex(
                name: "IX_Movimientos_id_Caja",
                table: "Movimientos");

            migrationBuilder.DropIndex(
                name: "IX_Movimientos_id_Venta",
                table: "Movimientos");

            migrationBuilder.DropIndex(
                name: "IX_Movimientos_id_CuentaCorriente",
                table: "Movimientos");

            migrationBuilder.DropColumn(
                name: "id_Caja",
                table: "Movimientos");

            migrationBuilder.DropColumn(
                name: "id_CuentaCorriente",
                table: "Movimientos");

            migrationBuilder.RenameColumn(
                name: "id_Movimiento",
                table: "Movimientos",
                newName: "id_movimiento");

            migrationBuilder.RenameColumn(
                name: "id_Venta",
                table: "Movimientos",
                newName: "entidad_id");

            migrationBuilder.AlterColumn<DateTime>(
                name: "fecha_movimiento",
                table: "Movimientos",
                type: "datetime",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "date");

            migrationBuilder.AddColumn<int>(
                name: "tipo_entidad",
                table: "Movimientos",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Movimientos_tipo_entidad_entidad_id",
                table: "Movimientos",
                columns: new[] { "tipo_entidad", "entidad_id" });
        }
    }
}
