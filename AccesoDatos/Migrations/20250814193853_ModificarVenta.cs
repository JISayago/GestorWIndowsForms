using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AccesoDatos.Migrations
{
    /// <inheritdoc />
    public partial class ModificarVenta : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "numeroVenta",
                table: "Ventas",
                newName: "numero_venta");

            migrationBuilder.AddColumn<decimal>(
                name: "descuento",
                table: "Ventas",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<long>(
                name: "id_Vendedor",
                table: "Ventas",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<decimal>(
                name: "monto_adeudado",
                table: "Ventas",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "monto_pagado",
                table: "Ventas",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "total_sin_descuento",
                table: "Ventas",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateIndex(
                name: "IX_Ventas_id_Vendedor",
                table: "Ventas",
                column: "id_Vendedor");

            migrationBuilder.AddForeignKey(
                name: "FK_Ventas_Empleados_id_Vendedor",
                table: "Ventas",
                column: "id_Vendedor",
                principalTable: "Empleados",
                principalColumn: "PersonaId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ventas_Empleados_id_Vendedor",
                table: "Ventas");

            migrationBuilder.DropIndex(
                name: "IX_Ventas_id_Vendedor",
                table: "Ventas");

            migrationBuilder.DropColumn(
                name: "descuento",
                table: "Ventas");

            migrationBuilder.DropColumn(
                name: "id_Vendedor",
                table: "Ventas");

            migrationBuilder.DropColumn(
                name: "monto_adeudado",
                table: "Ventas");

            migrationBuilder.DropColumn(
                name: "monto_pagado",
                table: "Ventas");

            migrationBuilder.DropColumn(
                name: "total_sin_descuento",
                table: "Ventas");

            migrationBuilder.RenameColumn(
                name: "numero_venta",
                table: "Ventas",
                newName: "numeroVenta");
        }
    }
}
