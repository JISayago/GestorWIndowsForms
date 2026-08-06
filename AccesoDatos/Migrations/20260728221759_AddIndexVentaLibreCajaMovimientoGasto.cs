using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AccesoDatos.Migrations
{
    /// <inheritdoc />
    public partial class AddIndexVentaLibreCajaMovimientoGasto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Gastos_estado_gasto",
                table: "Gastos");

            migrationBuilder.CreateIndex(
                name: "IX_VentasLibres_FechaVenta_Desc",
                table: "VentasLibres",
                column: "fecha_venta",
                descending: new bool[0])
                .Annotation("SqlServer:Include", new[] { "estado", "numero_venta", "total", "id_cliente" });

            migrationBuilder.CreateIndex(
                name: "IX_Movimientos_FechaMovimiento_Desc",
                table: "Movimientos",
                column: "fecha_movimiento",
                descending: new bool[0]);

            migrationBuilder.CreateIndex(
                name: "IX_Gastos_EstadoGasto",
                table: "Gastos",
                column: "estado_gasto")
                .Annotation("SqlServer:Include", new[] { "fecha_gasto", "fecha_registro", "numero_gasto", "id_Empleado", "monto_total", "monto_pagado", "categoria_gasto" });

            migrationBuilder.CreateIndex(
                name: "IX_Cajas_EstaCerrada",
                table: "Cajas",
                column: "esta_cerrada");

            migrationBuilder.CreateIndex(
                name: "IX_Cajas_FechaInicio_Desc",
                table: "Cajas",
                column: "fecha_apertura",
                descending: new bool[0]);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_VentasLibres_FechaVenta_Desc",
                table: "VentasLibres");

            migrationBuilder.DropIndex(
                name: "IX_Movimientos_FechaMovimiento_Desc",
                table: "Movimientos");

            migrationBuilder.DropIndex(
                name: "IX_Gastos_EstadoGasto",
                table: "Gastos");

            migrationBuilder.DropIndex(
                name: "IX_Cajas_EstaCerrada",
                table: "Cajas");

            migrationBuilder.DropIndex(
                name: "IX_Cajas_FechaInicio_Desc",
                table: "Cajas");

            migrationBuilder.CreateIndex(
                name: "IX_Gastos_estado_gasto",
                table: "Gastos",
                column: "estado_gasto");
        }
    }
}
