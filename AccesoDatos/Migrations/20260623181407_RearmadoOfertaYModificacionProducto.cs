using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AccesoDatos.Migrations
{
    /// <inheritdoc />
    public partial class RearmadoOfertaYModificacionProducto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OfertasDescuentos_Categorias_id_categoria",
                table: "OfertasDescuentos");

            migrationBuilder.DropForeignKey(
                name: "FK_OfertasDescuentos_Marcas_id_marca",
                table: "OfertasDescuentos");

            migrationBuilder.DropForeignKey(
                name: "FK_OfertasDescuentos_Rubros_id_rubro",
                table: "OfertasDescuentos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductosEnOfertaDescuentos",
                table: "ProductosEnOfertaDescuentos");

            migrationBuilder.DropIndex(
                name: "IX_OfertasDescuentos_id_categoria",
                table: "OfertasDescuentos");

            migrationBuilder.DropIndex(
                name: "IX_OfertasDescuentos_id_marca",
                table: "OfertasDescuentos");

            migrationBuilder.DropIndex(
                name: "IX_OfertasDescuentos_id_rubro",
                table: "OfertasDescuentos");

            migrationBuilder.DropColumn(
                name: "id_ProductosEnOfertaDescuento",
                table: "ProductosEnOfertaDescuentos");

            migrationBuilder.DropColumn(
                name: "cantidad",
                table: "ProductosEnOfertaDescuentos");

            migrationBuilder.DropColumn(
                name: "cantidad_limite_de_stock",
                table: "OfertasDescuentos");

            migrationBuilder.DropColumn(
                name: "cantidad_productos_dentro_oferta",
                table: "OfertasDescuentos");

            migrationBuilder.DropColumn(
                name: "descuento_total_final",
                table: "OfertasDescuentos");

            migrationBuilder.DropColumn(
                name: "detalle",
                table: "OfertasDescuentos");

            migrationBuilder.DropColumn(
                name: "es_oferta_por_grupo",
                table: "OfertasDescuentos");

            migrationBuilder.DropColumn(
                name: "es_un_solo_producto",
                table: "OfertasDescuentos");

            migrationBuilder.DropColumn(
                name: "grupo_nombre",
                table: "OfertasDescuentos");

            migrationBuilder.DropColumn(
                name: "id_categoria",
                table: "OfertasDescuentos");

            migrationBuilder.DropColumn(
                name: "id_marca",
                table: "OfertasDescuentos");

            migrationBuilder.DropColumn(
                name: "id_rubro",
                table: "OfertasDescuentos");

            migrationBuilder.DropColumn(
                name: "precio_original",
                table: "OfertasDescuentos");

            migrationBuilder.DropColumn(
                name: "tiene_limite_de_stock",
                table: "OfertasDescuentos");

            migrationBuilder.RenameColumn(
                name: "precio_original",
                table: "ProductosEnOfertaDescuentos",
                newName: "precio_venta_base");

            migrationBuilder.RenameColumn(
                name: "precio_con_descuento",
                table: "ProductosEnOfertaDescuentos",
                newName: "precio_costo_base");

            migrationBuilder.RenameColumn(
                name: "cantidad_vendida_por_limite",
                table: "ProductosEnOfertaDescuentos",
                newName: "cantidad_requerida");

            migrationBuilder.AddColumn<decimal>(
                name: "limite_venta_producto",
                table: "ProductosEnOfertaDescuentos",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "precio_oferta_base",
                table: "ProductosEnOfertaDescuentos",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "precio_final",
                table: "OfertasDescuentos",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AddColumn<int>(
                name: "tipo_oferta",
                table: "OfertasDescuentos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductosEnOfertaDescuentos",
                table: "ProductosEnOfertaDescuentos",
                columns: new[] { "id_Producto", "id_OfertaDescuento" });

            migrationBuilder.CreateTable(
                name: "OfertaProductoEstadisticas",
                columns: table => new
                {
                    id_OfertaDescuento = table.Column<long>(type: "bigint", nullable: false),
                    id_Producto = table.Column<long>(type: "bigint", nullable: false),
                    cantidad_vendida = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    total_costo_acumulado = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    total_venta_acumulado = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    total_oferta_acumulado = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    fecha_ultima_venta = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OfertaProductoEstadisticas", x => new { x.id_OfertaDescuento, x.id_Producto });
                    table.ForeignKey(
                        name: "FK_OfertaProductoEstadisticas_OfertasDescuentos_id_OfertaDescuento",
                        column: x => x.id_OfertaDescuento,
                        principalTable: "OfertasDescuentos",
                        principalColumn: "id_OfertaDescuento",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OfertaProductoEstadisticas_Productos_id_Producto",
                        column: x => x.id_Producto,
                        principalTable: "Productos",
                        principalColumn: "ProductoId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OfertaProductoEstadisticas_id_OfertaDescuento_id_Producto",
                table: "OfertaProductoEstadisticas",
                columns: new[] { "id_OfertaDescuento", "id_Producto" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_OfertaProductoEstadisticas_id_Producto",
                table: "OfertaProductoEstadisticas",
                column: "id_Producto");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OfertaProductoEstadisticas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductosEnOfertaDescuentos",
                table: "ProductosEnOfertaDescuentos");

            migrationBuilder.DropColumn(
                name: "limite_venta_producto",
                table: "ProductosEnOfertaDescuentos");

            migrationBuilder.DropColumn(
                name: "precio_oferta_base",
                table: "ProductosEnOfertaDescuentos");

            migrationBuilder.DropColumn(
                name: "tipo_oferta",
                table: "OfertasDescuentos");

            migrationBuilder.RenameColumn(
                name: "precio_venta_base",
                table: "ProductosEnOfertaDescuentos",
                newName: "precio_original");

            migrationBuilder.RenameColumn(
                name: "precio_costo_base",
                table: "ProductosEnOfertaDescuentos",
                newName: "precio_con_descuento");

            migrationBuilder.RenameColumn(
                name: "cantidad_requerida",
                table: "ProductosEnOfertaDescuentos",
                newName: "cantidad_vendida_por_limite");

            migrationBuilder.AddColumn<long>(
                name: "id_ProductosEnOfertaDescuento",
                table: "ProductosEnOfertaDescuentos",
                type: "bigint",
                nullable: false,
                defaultValue: 0L)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<decimal>(
                name: "cantidad",
                table: "ProductosEnOfertaDescuentos",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AlterColumn<decimal>(
                name: "precio_final",
                table: "OfertasDescuentos",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "cantidad_limite_de_stock",
                table: "OfertasDescuentos",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "cantidad_productos_dentro_oferta",
                table: "OfertasDescuentos",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "descuento_total_final",
                table: "OfertasDescuentos",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "detalle",
                table: "OfertasDescuentos",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "es_oferta_por_grupo",
                table: "OfertasDescuentos",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "es_un_solo_producto",
                table: "OfertasDescuentos",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "grupo_nombre",
                table: "OfertasDescuentos",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "id_categoria",
                table: "OfertasDescuentos",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "id_marca",
                table: "OfertasDescuentos",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "id_rubro",
                table: "OfertasDescuentos",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "precio_original",
                table: "OfertasDescuentos",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<bool>(
                name: "tiene_limite_de_stock",
                table: "OfertasDescuentos",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductosEnOfertaDescuentos",
                table: "ProductosEnOfertaDescuentos",
                column: "id_ProductosEnOfertaDescuento");

            migrationBuilder.CreateIndex(
                name: "IX_OfertasDescuentos_id_categoria",
                table: "OfertasDescuentos",
                column: "id_categoria");

            migrationBuilder.CreateIndex(
                name: "IX_OfertasDescuentos_id_marca",
                table: "OfertasDescuentos",
                column: "id_marca");

            migrationBuilder.CreateIndex(
                name: "IX_OfertasDescuentos_id_rubro",
                table: "OfertasDescuentos",
                column: "id_rubro");

            migrationBuilder.AddForeignKey(
                name: "FK_OfertasDescuentos_Categorias_id_categoria",
                table: "OfertasDescuentos",
                column: "id_categoria",
                principalTable: "Categorias",
                principalColumn: "CategoriaId",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_OfertasDescuentos_Marcas_id_marca",
                table: "OfertasDescuentos",
                column: "id_marca",
                principalTable: "Marcas",
                principalColumn: "MarcaId",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_OfertasDescuentos_Rubros_id_rubro",
                table: "OfertasDescuentos",
                column: "id_rubro",
                principalTable: "Rubros",
                principalColumn: "RubroId",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
