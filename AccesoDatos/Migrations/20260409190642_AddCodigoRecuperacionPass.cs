using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AccesoDatos.Migrations
{
    /// <inheritdoc />
    public partial class AddCodigoRecuperacionPass : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CodigosRecuperacionPass",
                columns: table => new
                {
                    CodigoRecuperacionId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UsuarioAsignadoId = table.Column<long>(type: "bigint", nullable: false),
                    Codigo = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaExpiracion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EstaUsado = table.Column<bool>(type: "bit", nullable: false),
                    FechaUso = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CodigosRecuperacionPass", x => x.CodigoRecuperacionId);
                    table.ForeignKey(
                        name: "FK_CodigosRecuperacionPass_Empleados_UsuarioAsignadoId",
                        column: x => x.UsuarioAsignadoId,
                        principalTable: "Empleados",
                        principalColumn: "PersonaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CodigosRecuperacionPass_Codigo",
                table: "CodigosRecuperacionPass",
                column: "Codigo");

            migrationBuilder.CreateIndex(
                name: "IX_CodigosRecuperacionPass_UsuarioAsignadoId",
                table: "CodigosRecuperacionPass",
                column: "UsuarioAsignadoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CodigosRecuperacionPass");
        }
    }
}
