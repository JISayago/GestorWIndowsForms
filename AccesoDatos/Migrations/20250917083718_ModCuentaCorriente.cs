using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AccesoDatos.Migrations
{
    /// <inheritdoc />
    public partial class ModCuentaCorriente : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "limite_credito",
                table: "CuentasCorrientes",
                newName: "limite_deuda");

            migrationBuilder.AddColumn<DateTime>(
                name: "fecha_vencimiento",
                table: "CuentasCorrientes",
                type: "date",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "limite_deuda_activo",
                table: "CuentasCorrientes",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "nombre_cuenta_corriente",
                table: "CuentasCorrientes",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "fecha_vencimiento",
                table: "CuentasCorrientes");

            migrationBuilder.DropColumn(
                name: "limite_deuda_activo",
                table: "CuentasCorrientes");

            migrationBuilder.DropColumn(
                name: "nombre_cuenta_corriente",
                table: "CuentasCorrientes");

            migrationBuilder.RenameColumn(
                name: "limite_deuda",
                table: "CuentasCorrientes",
                newName: "limite_credito");
        }
    }
}
