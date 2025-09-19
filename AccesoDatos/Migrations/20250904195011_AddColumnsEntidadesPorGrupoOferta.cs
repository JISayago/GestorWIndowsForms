using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AccesoDatos.Migrations
{
    /// <inheritdoc />
    public partial class AddColumnsEntidadesPorGrupoOferta : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "CategoriaId",
                table: "OfertasDescuentos",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GrupoNombre",
                table: "OfertasDescuentos",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "IdCategoria",
                table: "OfertasDescuentos",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "IdMarca",
                table: "OfertasDescuentos",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "IdRubro",
                table: "OfertasDescuentos",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "MarcaId",
                table: "OfertasDescuentos",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "RubroId",
                table: "OfertasDescuentos",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_OfertasDescuentos_CategoriaId",
                table: "OfertasDescuentos",
                column: "CategoriaId");

            migrationBuilder.CreateIndex(
                name: "IX_OfertasDescuentos_MarcaId",
                table: "OfertasDescuentos",
                column: "MarcaId");

            migrationBuilder.CreateIndex(
                name: "IX_OfertasDescuentos_RubroId",
                table: "OfertasDescuentos",
                column: "RubroId");

            migrationBuilder.AddForeignKey(
                name: "FK_OfertasDescuentos_Categorias_CategoriaId",
                table: "OfertasDescuentos",
                column: "CategoriaId",
                principalTable: "Categorias",
                principalColumn: "CategoriaId");

            migrationBuilder.AddForeignKey(
                name: "FK_OfertasDescuentos_Marcas_MarcaId",
                table: "OfertasDescuentos",
                column: "MarcaId",
                principalTable: "Marcas",
                principalColumn: "MarcaId");

            migrationBuilder.AddForeignKey(
                name: "FK_OfertasDescuentos_Rubros_RubroId",
                table: "OfertasDescuentos",
                column: "RubroId",
                principalTable: "Rubros",
                principalColumn: "RubroId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OfertasDescuentos_Categorias_CategoriaId",
                table: "OfertasDescuentos");

            migrationBuilder.DropForeignKey(
                name: "FK_OfertasDescuentos_Marcas_MarcaId",
                table: "OfertasDescuentos");

            migrationBuilder.DropForeignKey(
                name: "FK_OfertasDescuentos_Rubros_RubroId",
                table: "OfertasDescuentos");

            migrationBuilder.DropIndex(
                name: "IX_OfertasDescuentos_CategoriaId",
                table: "OfertasDescuentos");

            migrationBuilder.DropIndex(
                name: "IX_OfertasDescuentos_MarcaId",
                table: "OfertasDescuentos");

            migrationBuilder.DropIndex(
                name: "IX_OfertasDescuentos_RubroId",
                table: "OfertasDescuentos");

            migrationBuilder.DropColumn(
                name: "CategoriaId",
                table: "OfertasDescuentos");

            migrationBuilder.DropColumn(
                name: "GrupoNombre",
                table: "OfertasDescuentos");

            migrationBuilder.DropColumn(
                name: "IdCategoria",
                table: "OfertasDescuentos");

            migrationBuilder.DropColumn(
                name: "IdMarca",
                table: "OfertasDescuentos");

            migrationBuilder.DropColumn(
                name: "IdRubro",
                table: "OfertasDescuentos");

            migrationBuilder.DropColumn(
                name: "MarcaId",
                table: "OfertasDescuentos");

            migrationBuilder.DropColumn(
                name: "RubroId",
                table: "OfertasDescuentos");
        }
    }
}
