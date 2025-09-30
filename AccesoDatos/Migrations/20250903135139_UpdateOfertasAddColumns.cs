using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AccesoDatos.Migrations
{
    /// <inheritdoc />
    public partial class UpdateOfertasAddColumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "codigo",
                table: "OfertasDescuentos",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "detalle",
                table: "OfertasDescuentos",
                type: "bit",
                maxLength: 200,
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "es_oferta_por_grupo",
                table: "OfertasDescuentos",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "codigo",
                table: "OfertasDescuentos");

            migrationBuilder.DropColumn(
                name: "detalle",
                table: "OfertasDescuentos");

            migrationBuilder.DropColumn(
                name: "es_oferta_por_grupo",
                table: "OfertasDescuentos");
        }
    }
}
