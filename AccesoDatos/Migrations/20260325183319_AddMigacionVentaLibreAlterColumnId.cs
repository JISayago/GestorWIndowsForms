using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AccesoDatos.Migrations
{
    /// <inheritdoc />
    public partial class AddMigacionVentaLibreAlterColumnId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "IdVentaLibre",
                table: "VentaPagoDetalles",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "VentasLibres",
                columns: table => new
                {
                    id_VentaLibre = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    id_Empleado = table.Column<long>(type: "bigint", nullable: false),
                    id_Vendedor = table.Column<long>(type: "bigint", nullable: false),
                    id_cliente = table.Column<long>(type: "bigint", nullable: true),
                    numero_venta = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    fecha_venta = table.Column<DateTime>(type: "datetime2", nullable: false),
                    total = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    estado = table.Column<int>(type: "int", nullable: false),
                    detalle = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    monto_adeudado = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    monto_pagado = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VentasLibres", x => x.id_VentaLibre);
                    table.ForeignKey(
                        name: "FK_VentasLibres_Clientes_id_cliente",
                        column: x => x.id_cliente,
                        principalTable: "Clientes",
                        principalColumn: "PersonaId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_VentasLibres_Empleados_id_Empleado",
                        column: x => x.id_Empleado,
                        principalTable: "Empleados",
                        principalColumn: "PersonaId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_VentasLibres_Empleados_id_Vendedor",
                        column: x => x.id_Vendedor,
                        principalTable: "Empleados",
                        principalColumn: "PersonaId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_VentaPagoDetalles_IdVentaLibre",
                table: "VentaPagoDetalles",
                column: "IdVentaLibre");

            migrationBuilder.CreateIndex(
                name: "IX_VentasLibres_id_cliente",
                table: "VentasLibres",
                column: "id_cliente");

            migrationBuilder.CreateIndex(
                name: "IX_VentasLibres_id_Empleado",
                table: "VentasLibres",
                column: "id_Empleado");

            migrationBuilder.CreateIndex(
                name: "IX_VentasLibres_id_Vendedor",
                table: "VentasLibres",
                column: "id_Vendedor");

            migrationBuilder.AddForeignKey(
                name: "FK_VentaPagoDetalles_VentasLibres_IdVentaLibre",
                table: "VentaPagoDetalles",
                column: "IdVentaLibre",
                principalTable: "VentasLibres",
                principalColumn: "id_VentaLibre",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VentaPagoDetalles_VentasLibres_IdVentaLibre",
                table: "VentaPagoDetalles");

            migrationBuilder.DropTable(
                name: "VentasLibres");

            migrationBuilder.DropIndex(
                name: "IX_VentaPagoDetalles_IdVentaLibre",
                table: "VentaPagoDetalles");

            migrationBuilder.DropColumn(
                name: "IdVentaLibre",
                table: "VentaPagoDetalles");
        }
    }
}
