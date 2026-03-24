using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AccesoDatos.Migrations
{
    /// <inheritdoc />
    public partial class CreamosDetalleVentaLote : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DetalleVentaLote",
                columns: table => new
                {
                    DetalleVentaLoteId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    id_Producto = table.Column<long>(type: "bigint", nullable: false),
                    id_Venta = table.Column<long>(type: "bigint", nullable: false),
                    id_Lote = table.Column<long>(type: "bigint", nullable: false),
                    cantidad = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DetalleVentaLote", x => x.DetalleVentaLoteId);
                    table.ForeignKey(
                        name: "FK_DetalleVentaLote_Lotes_id_Lote",
                        column: x => x.id_Lote,
                        principalTable: "Lotes",
                        principalColumn: "id_Lote",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DetalleVentaLote_Productos_id_Producto",
                        column: x => x.id_Producto,
                        principalTable: "Productos",
                        principalColumn: "ProductoId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DetalleVentaLote_Ventas_id_Venta",
                        column: x => x.id_Venta,
                        principalTable: "Ventas",
                        principalColumn: "id_Venta",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DetalleVentaLote_id_Lote",
                table: "DetalleVentaLote",
                column: "id_Lote");

            migrationBuilder.CreateIndex(
                name: "IX_DetalleVentaLote_id_Producto",
                table: "DetalleVentaLote",
                column: "id_Producto");

            migrationBuilder.CreateIndex(
                name: "IX_DetalleVentaLote_id_Venta",
                table: "DetalleVentaLote",
                column: "id_Venta");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DetalleVentaLote");
        }
    }
}
