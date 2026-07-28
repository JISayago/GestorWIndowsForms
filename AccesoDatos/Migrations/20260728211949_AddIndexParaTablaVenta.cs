using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AccesoDatos.Migrations
{
    /// <inheritdoc />
    public partial class AddIndexParaTablaVenta : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Ventas_Estado_FechaVenta",
                table: "Ventas",
                columns: new[] { "estado", "fecha_venta" });

            migrationBuilder.CreateIndex(
                name: "IX_Ventas_FechaVenta",
                table: "Ventas",
                column: "fecha_venta");

            migrationBuilder.CreateIndex(
                name: "IX_Ventas_NumeroVenta",
                table: "Ventas",
                column: "numero_venta");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Ventas_Estado_FechaVenta",
                table: "Ventas");

            migrationBuilder.DropIndex(
                name: "IX_Ventas_FechaVenta",
                table: "Ventas");

            migrationBuilder.DropIndex(
                name: "IX_Ventas_NumeroVenta",
                table: "Ventas");
        }
    }
}
