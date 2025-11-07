using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AccesoDatos.Migrations
{
    /// <inheritdoc />
    public partial class ModMoviemiento : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Movimientos_Ventas_IdVenta",
                table: "Movimientos");

            migrationBuilder.RenameColumn(
                name: "TipoMovimiento",
                table: "Movimientos",
                newName: "tipo_movimiento");

            migrationBuilder.RenameColumn(
                name: "IdVenta",
                table: "Movimientos",
                newName: "id_Venta");

            migrationBuilder.RenameColumn(
                name: "EstaEliminado",
                table: "Movimientos",
                newName: "esta_eliminado");

            migrationBuilder.RenameIndex(
                name: "IX_Movimientos_IdVenta",
                table: "Movimientos",
                newName: "IX_Movimientos_id_Venta");

            migrationBuilder.AddColumn<DateTime>(
                name: "fecha_movimiento",
                table: "Movimientos",
                type: "date",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<decimal>(
                name: "monto",
                table: "Movimientos",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "numero_movimiento",
                table: "Movimientos",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_Movimientos_Ventas_id_Venta",
                table: "Movimientos",
                column: "id_Venta",
                principalTable: "Ventas",
                principalColumn: "id_Venta",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Movimientos_Ventas_id_Venta",
                table: "Movimientos");

            migrationBuilder.DropColumn(
                name: "fecha_movimiento",
                table: "Movimientos");

            migrationBuilder.DropColumn(
                name: "monto",
                table: "Movimientos");

            migrationBuilder.DropColumn(
                name: "numero_movimiento",
                table: "Movimientos");

            migrationBuilder.RenameColumn(
                name: "tipo_movimiento",
                table: "Movimientos",
                newName: "TipoMovimiento");

            migrationBuilder.RenameColumn(
                name: "id_Venta",
                table: "Movimientos",
                newName: "IdVenta");

            migrationBuilder.RenameColumn(
                name: "esta_eliminado",
                table: "Movimientos",
                newName: "EstaEliminado");

            migrationBuilder.RenameIndex(
                name: "IX_Movimientos_id_Venta",
                table: "Movimientos",
                newName: "IX_Movimientos_IdVenta");

            migrationBuilder.AddForeignKey(
                name: "FK_Movimientos_Ventas_IdVenta",
                table: "Movimientos",
                column: "IdVenta",
                principalTable: "Ventas",
                principalColumn: "id_Venta",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
