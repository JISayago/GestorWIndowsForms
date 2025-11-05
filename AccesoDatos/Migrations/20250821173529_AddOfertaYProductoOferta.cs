using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AccesoDatos.Migrations
{
    /// <inheritdoc />
    public partial class AddOfertaYProductoOferta : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OfertasDescuentos",
                columns: table => new
                {
                    id_OfertaDescuento = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    descripcion = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    precio_final = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    precio_original = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    descuento_total_final = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    porcentaje_descuento = table.Column<decimal>(type: "decimal(5,2)", nullable: true),
                    fecha_inicio = table.Column<DateTime>(type: "date", nullable: false),
                    fecha_fin = table.Column<DateTime>(type: "date", nullable: true),
                    cantidad_productos_dentro_oferta = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    esta_activa = table.Column<bool>(type: "bit", nullable: false),
                    es_un_solo_producto = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OfertasDescuentos", x => x.id_OfertaDescuento);
                });

            migrationBuilder.CreateTable(
                name: "ProductosEnOfertaDescuentos",
                columns: table => new
                {
                    id_ProductosEnOfertaDescuento = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    id_OfertaDescuento = table.Column<long>(type: "bigint", nullable: false),
                    id_Producto = table.Column<long>(type: "bigint", nullable: false),
                    cantidad = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    precio_unitario_oferta = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    descuento_porcentaje = table.Column<decimal>(type: "decimal(5,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductosEnOfertaDescuentos", x => x.id_ProductosEnOfertaDescuento);
                    table.ForeignKey(
                        name: "FK_ProductosEnOfertaDescuentos_OfertasDescuentos_id_OfertaDescuento",
                        column: x => x.id_OfertaDescuento,
                        principalTable: "OfertasDescuentos",
                        principalColumn: "id_OfertaDescuento",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductosEnOfertaDescuentos_Productos_id_Producto",
                        column: x => x.id_Producto,
                        principalTable: "Productos",
                        principalColumn: "ProductoId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductosEnOfertaDescuentos_id_OfertaDescuento",
                table: "ProductosEnOfertaDescuentos",
                column: "id_OfertaDescuento");

            migrationBuilder.CreateIndex(
                name: "IX_ProductosEnOfertaDescuentos_id_Producto",
                table: "ProductosEnOfertaDescuentos",
                column: "id_Producto");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductosEnOfertaDescuentos");

            migrationBuilder.DropTable(
                name: "OfertasDescuentos");
        }
    }
}
