using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AccesoDatos.Migrations
{
    /// <inheritdoc />
    public partial class ColumnasSesionUsuarioSecundario : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "EsSecundaria",
                table: "Usuarios_Sesiones",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<long>(
                name: "SesionPrincipalId",
                table: "Usuarios_Sesiones",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_Sesiones_SesionPrincipalId",
                table: "Usuarios_Sesiones",
                column: "SesionPrincipalId");

            migrationBuilder.AddForeignKey(
                name: "FK_Usuarios_Sesiones_Usuarios_Sesiones_SesionPrincipalId",
                table: "Usuarios_Sesiones",
                column: "SesionPrincipalId",
                principalTable: "Usuarios_Sesiones",
                principalColumn: "UsuarioSesionId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Usuarios_Sesiones_Usuarios_Sesiones_SesionPrincipalId",
                table: "Usuarios_Sesiones");

            migrationBuilder.DropIndex(
                name: "IX_Usuarios_Sesiones_SesionPrincipalId",
                table: "Usuarios_Sesiones");

            migrationBuilder.DropColumn(
                name: "EsSecundaria",
                table: "Usuarios_Sesiones");

            migrationBuilder.DropColumn(
                name: "SesionPrincipalId",
                table: "Usuarios_Sesiones");
        }
    }
}
