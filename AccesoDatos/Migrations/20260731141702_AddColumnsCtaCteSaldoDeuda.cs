using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AccesoDatos.Migrations
{
    /// <inheritdoc />
    public partial class AddColumnsCtaCteSaldoDeuda : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "cantidad_meses_vencimiento",
                table: "CuentasCorrientes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "con_deuda",
                table: "CuentasCorrientes",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "tipo_vencimiento",
                table: "CuentasCorrientes",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "cantidad_meses_vencimiento",
                table: "CuentasCorrientes");

            migrationBuilder.DropColumn(
                name: "permitir_vencimiento_negativo",
                table: "CuentasCorrientes");

            migrationBuilder.DropColumn(
                name: "tipo_vencimiento",
                table: "CuentasCorrientes");
        }
    }
}
