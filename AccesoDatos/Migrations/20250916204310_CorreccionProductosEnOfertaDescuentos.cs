using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AccesoDatos.Migrations
{
    /// <inheritdoc />
    public partial class CorreccionProductosEnOfertaDescuentos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "descuento_porcentaje",
                table: "ProductosEnOfertaDescuentos");

            migrationBuilder.DropColumn(
                name: "precio_unitario_oferta",
                table: "ProductosEnOfertaDescuentos");

            migrationBuilder.RenameColumn(
                name: "CantidadVendidaPorLimite",
                table: "ProductosEnOfertaDescuentos",
                newName: "cantidad_vendida_por_limite");

            migrationBuilder.AddColumn<decimal>(
                name: "precio_con_descuento",
                table: "ProductosEnOfertaDescuentos",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "precio_original",
                table: "ProductosEnOfertaDescuentos",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "precio_con_descuento",
                table: "ProductosEnOfertaDescuentos");

            migrationBuilder.DropColumn(
                name: "precio_original",
                table: "ProductosEnOfertaDescuentos");

            migrationBuilder.RenameColumn(
                name: "cantidad_vendida_por_limite",
                table: "ProductosEnOfertaDescuentos",
                newName: "CantidadVendidaPorLimite");

            migrationBuilder.AddColumn<decimal>(
                name: "descuento_porcentaje",
                table: "ProductosEnOfertaDescuentos",
                type: "decimal(5,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "precio_unitario_oferta",
                table: "ProductosEnOfertaDescuentos",
                type: "decimal(18,2)",
                nullable: true);
        }
    }
}
