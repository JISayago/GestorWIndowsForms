using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AccesoDatos.Migrations
{
    /// <inheritdoc />
    public partial class CorreccionFechaHoraVentaYSeAgregaClienteId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "fecha_venta",
                table: "Ventas",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "date");

            migrationBuilder.AddColumn<long>(
                name: "id_cliente",
                table: "Ventas",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Ventas_id_cliente",
                table: "Ventas",
                column: "id_cliente");

            migrationBuilder.AddForeignKey(
                name: "FK_Ventas_Clientes_id_cliente",
                table: "Ventas",
                column: "id_cliente",
                principalTable: "Clientes",
                principalColumn: "PersonaId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ventas_Clientes_id_cliente",
                table: "Ventas");

            migrationBuilder.DropIndex(
                name: "IX_Ventas_id_cliente",
                table: "Ventas");

            migrationBuilder.DropColumn(
                name: "id_cliente",
                table: "Ventas");

            migrationBuilder.AlterColumn<DateTime>(
                name: "fecha_venta",
                table: "Ventas",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");
        }
    }
}
