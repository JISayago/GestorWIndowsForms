using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AccesoDatos.Migrations
{
    /// <inheritdoc />
    public partial class FixCtaCteFechas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.AddColumn<DateTime>(
            //    name: "fecha_activacion",
            //    table: "CuentasCorrientes",
            //    type: "date",
            //    nullable: true);

            //migrationBuilder.AddColumn<DateTime>(
            //    name: "fecha_creacion",
            //    table: "CuentasCorrientes",
            //    type: "date",
            //    nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropColumn(
            //    name: "fecha_activacion",
            //    table: "CuentasCorrientes");
            
            //migrationBuilder.DropColumn(
            //    name: "fecha_creacion",
            //    table: "CuentasCorrientes");
        }
    }
}
