using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AccesoDatos.Migrations
{
    /// <inheritdoc />
    public partial class CorreccionOfertaDescuento : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
                name: "MarcaId",
                table: "OfertasDescuentos");

            migrationBuilder.DropColumn(
                name: "RubroId",
                table: "OfertasDescuentos");

            migrationBuilder.RenameColumn(
                name: "IdRubro",
                table: "OfertasDescuentos",
                newName: "id_rubro");

            migrationBuilder.RenameColumn(
                name: "IdMarca",
                table: "OfertasDescuentos",
                newName: "id_marca");

            migrationBuilder.RenameColumn(
                name: "IdCategoria",
                table: "OfertasDescuentos",
                newName: "id_categoria");

            migrationBuilder.RenameColumn(
                name: "GrupoNombre",
                table: "OfertasDescuentos",
                newName: "grupo_nombre");

            migrationBuilder.AddColumn<decimal>(
                name: "CantidadVendidaPorLimite",
                table: "ProductosEnOfertaDescuentos",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AlterColumn<string>(
                name: "detalle",
                table: "OfertasDescuentos",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<string>(
                name: "grupo_nombre",
                table: "OfertasDescuentos",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "cantidad_limite_de_stock",
                table: "OfertasDescuentos",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "tiene_limite_de_stock",
                table: "OfertasDescuentos",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_OfertasDescuentos_id_categoria",
                table: "OfertasDescuentos",
                column: "id_categoria");

            migrationBuilder.CreateIndex(
                name: "IX_OfertasDescuentos_id_marca",
                table: "OfertasDescuentos",
                column: "id_marca");

            migrationBuilder.CreateIndex(
                name: "IX_OfertasDescuentos_id_rubro",
                table: "OfertasDescuentos",
                column: "id_rubro");

            migrationBuilder.AddForeignKey(
                name: "FK_OfertasDescuentos_Categorias_id_categoria",
                table: "OfertasDescuentos",
                column: "id_categoria",
                principalTable: "Categorias",
                principalColumn: "CategoriaId",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_OfertasDescuentos_Marcas_id_marca",
                table: "OfertasDescuentos",
                column: "id_marca",
                principalTable: "Marcas",
                principalColumn: "MarcaId",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_OfertasDescuentos_Rubros_id_rubro",
                table: "OfertasDescuentos",
                column: "id_rubro",
                principalTable: "Rubros",
                principalColumn: "RubroId",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OfertasDescuentos_Categorias_id_categoria",
                table: "OfertasDescuentos");

            migrationBuilder.DropForeignKey(
                name: "FK_OfertasDescuentos_Marcas_id_marca",
                table: "OfertasDescuentos");

            migrationBuilder.DropForeignKey(
                name: "FK_OfertasDescuentos_Rubros_id_rubro",
                table: "OfertasDescuentos");

            migrationBuilder.DropIndex(
                name: "IX_OfertasDescuentos_id_categoria",
                table: "OfertasDescuentos");

            migrationBuilder.DropIndex(
                name: "IX_OfertasDescuentos_id_marca",
                table: "OfertasDescuentos");

            migrationBuilder.DropIndex(
                name: "IX_OfertasDescuentos_id_rubro",
                table: "OfertasDescuentos");

            migrationBuilder.DropColumn(
                name: "CantidadVendidaPorLimite",
                table: "ProductosEnOfertaDescuentos");

            migrationBuilder.DropColumn(
                name: "cantidad_limite_de_stock",
                table: "OfertasDescuentos");

            migrationBuilder.DropColumn(
                name: "tiene_limite_de_stock",
                table: "OfertasDescuentos");

            migrationBuilder.RenameColumn(
                name: "id_rubro",
                table: "OfertasDescuentos",
                newName: "IdRubro");

            migrationBuilder.RenameColumn(
                name: "id_marca",
                table: "OfertasDescuentos",
                newName: "IdMarca");

            migrationBuilder.RenameColumn(
                name: "id_categoria",
                table: "OfertasDescuentos",
                newName: "IdCategoria");

            migrationBuilder.RenameColumn(
                name: "grupo_nombre",
                table: "OfertasDescuentos",
                newName: "GrupoNombre");

            migrationBuilder.AlterColumn<bool>(
                name: "detalle",
                table: "OfertasDescuentos",
                type: "bit",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<string>(
                name: "GrupoNombre",
                table: "OfertasDescuentos",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200,
                oldNullable: true);

            migrationBuilder.AddColumn<long>(
                name: "CategoriaId",
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
    }
}
