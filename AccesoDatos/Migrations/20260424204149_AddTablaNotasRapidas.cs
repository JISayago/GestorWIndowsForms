using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AccesoDatos.Migrations
{
    /// <inheritdoc />
    public partial class AddTablaNotasRapidas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "NotasRapidas",
                columns: table => new
                {
                    id_Nota = table.Column<int>(type: "int", nullable: false),
                    cuerpo = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    fecha_modificacion = table.Column<DateTime>(type: "datetime", nullable: false),
                    usuario_nombre = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotasRapidas", x => x.id_Nota);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NotasRapidas");
        }
    }
}
