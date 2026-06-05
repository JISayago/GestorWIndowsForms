using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AccesoDatos.Migrations
{
    /// <inheritdoc />
    public partial class AddColumnaEnVentaPagoDetalle : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "extra_descripcion_pago",
                table: "VentaPagoDetalles",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "Sin especificar");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "extra_descripcion_pago",
                table: "VentaPagoDetalles");
        }
    }
}
