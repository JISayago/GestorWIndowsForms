using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AccesoDatos.Migrations
{
    /// <inheritdoc />
    public partial class MigracionDetalleVentaAddColumnsTotales : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "descripcion",
                table: "DetallesVenta",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "es_oferta",
                table: "DetallesVenta",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "es_oferta_por_grupo",
                table: "DetallesVenta",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<decimal>(
                name: "precio_unitario_final",
                table: "DetallesVenta",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "precio_unitario_original",
                table: "DetallesVenta",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "descripcion",
                table: "DetallesVenta");

            migrationBuilder.DropColumn(
                name: "es_oferta",
                table: "DetallesVenta");

            migrationBuilder.DropColumn(
                name: "es_oferta_por_grupo",
                table: "DetallesVenta");

            migrationBuilder.DropColumn(
                name: "precio_unitario_final",
                table: "DetallesVenta");

            migrationBuilder.DropColumn(
                name: "precio_unitario_original",
                table: "DetallesVenta");
        }
    }
}
