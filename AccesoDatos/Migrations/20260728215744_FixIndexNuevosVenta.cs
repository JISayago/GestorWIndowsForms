using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AccesoDatos.Migrations
{
    /// <inheritdoc />
    public partial class FixIndexNuevosVenta : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Ventas_FechaVenta",
                table: "Ventas");

            migrationBuilder.CreateIndex(
                name: "IX_Ventas_FechaVenta_Desc",
                table: "Ventas",
                column: "fecha_venta",
                descending: new bool[0])
                .Annotation("SqlServer:Include", new[] { "estado", "numero_venta", "total", "detalle", "id_cliente" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Ventas_FechaVenta_Desc",
                table: "Ventas");

            migrationBuilder.CreateIndex(
                name: "IX_Ventas_FechaVenta",
                table: "Ventas",
                column: "fecha_venta");
        }
    }
}
