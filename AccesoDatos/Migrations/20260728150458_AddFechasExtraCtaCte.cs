using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AccesoDatos.Migrations
{
    /// <inheritdoc />
    public partial class AddFechasExtraCtaCte : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "fecha_activacion",
                table: "CuentasCorrientes",
                type: "datetime",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "fecha_creacion",
                table: "CuentasCorrientes",
                type: "datetime",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "fecha_activacion",
                table: "CuentasCorrientes");

            migrationBuilder.DropColumn(
                name: "fecha_creacion",
                table: "CuentasCorrientes");
        }
    }
}
