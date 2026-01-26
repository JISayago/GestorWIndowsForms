using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AccesoDatos.Migrations
{
    /// <inheritdoc />
    public partial class AgregarGastosYModificarVentaPagoDetalle : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "id_Venta",
                table: "VentaPagoDetalles",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddColumn<long>(
                name: "id_Gasto",
                table: "VentaPagoDetalles",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Gastos",
                columns: table => new
                {
                    id_Gasto = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    numero_gasto = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    id_Empleado = table.Column<long>(type: "bigint", nullable: false),
                    categoria_gasto = table.Column<int>(type: "int", nullable: false),
                    fecha_gasto = table.Column<DateTime>(type: "datetime2", nullable: false),
                    fecha_registro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    monto_total = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    monto_pagado = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    estado_gasto = table.Column<int>(type: "int", nullable: false),
                    detalle = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gastos", x => x.id_Gasto);
                    table.ForeignKey(
                        name: "FK_Gastos_Empleados_id_Empleado",
                        column: x => x.id_Empleado,
                        principalTable: "Empleados",
                        principalColumn: "PersonaId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_VentaPagoDetalles_id_Gasto",
                table: "VentaPagoDetalles",
                column: "id_Gasto");

            migrationBuilder.CreateIndex(
                name: "IX_Gastos_categoria_gasto",
                table: "Gastos",
                column: "categoria_gasto");

            migrationBuilder.CreateIndex(
                name: "IX_Gastos_estado_gasto",
                table: "Gastos",
                column: "estado_gasto");

            migrationBuilder.CreateIndex(
                name: "IX_Gastos_fecha_gasto",
                table: "Gastos",
                column: "fecha_gasto");

            migrationBuilder.CreateIndex(
                name: "IX_Gastos_id_Empleado",
                table: "Gastos",
                column: "id_Empleado");

            migrationBuilder.AddForeignKey(
                name: "FK_VentaPagoDetalles_Gastos_id_Gasto",
                table: "VentaPagoDetalles",
                column: "id_Gasto",
                principalTable: "Gastos",
                principalColumn: "id_Gasto",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VentaPagoDetalles_Gastos_id_Gasto",
                table: "VentaPagoDetalles");

            migrationBuilder.DropTable(
                name: "Gastos");

            migrationBuilder.DropIndex(
                name: "IX_VentaPagoDetalles_id_Gasto",
                table: "VentaPagoDetalles");

            migrationBuilder.DropColumn(
                name: "id_Gasto",
                table: "VentaPagoDetalles");

            migrationBuilder.AlterColumn<long>(
                name: "id_Venta",
                table: "VentaPagoDetalles",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);
        }
    }
}
