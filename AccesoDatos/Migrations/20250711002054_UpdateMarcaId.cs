using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AccesoDatos.Migrations
{
    /// <inheritdoc />
    public partial class UpdateMarcaId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Productos_Marcas_MarcaCategoriaId",
                table: "Productos");

            migrationBuilder.RenameColumn(
                name: "MarcaCategoriaId",
                table: "Productos",
                newName: "MarcaId");

            migrationBuilder.RenameIndex(
                name: "IX_Productos_MarcaCategoriaId",
                table: "Productos",
                newName: "IX_Productos_MarcaId");

            migrationBuilder.RenameColumn(
                name: "CategoriaId",
                table: "Marcas",
                newName: "MarcaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Productos_Marcas_MarcaId",
                table: "Productos",
                column: "MarcaId",
                principalTable: "Marcas",
                principalColumn: "MarcaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Productos_Marcas_MarcaId",
                table: "Productos");

            migrationBuilder.RenameColumn(
                name: "MarcaId",
                table: "Productos",
                newName: "MarcaCategoriaId");

            migrationBuilder.RenameIndex(
                name: "IX_Productos_MarcaId",
                table: "Productos",
                newName: "IX_Productos_MarcaCategoriaId");

            migrationBuilder.RenameColumn(
                name: "MarcaId",
                table: "Marcas",
                newName: "CategoriaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Productos_Marcas_MarcaCategoriaId",
                table: "Productos",
                column: "MarcaCategoriaId",
                principalTable: "Marcas",
                principalColumn: "CategoriaId");
        }
    }
}
