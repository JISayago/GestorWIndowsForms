using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AccesoDatos.Migrations
{
    /// <inheritdoc />
    public partial class SeAgregaCategoriaProducto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Productos_Categorias_CategoriaId",
                table: "Productos");

            migrationBuilder.DropForeignKey(
                name: "FK_Productos_Categorias_id_Categoria",
                table: "Productos");

            migrationBuilder.DropIndex(
                name: "IX_Productos_CategoriaId",
                table: "Productos");

            migrationBuilder.DropIndex(
                name: "IX_Productos_id_Categoria",
                table: "Productos");

            migrationBuilder.DropColumn(
                name: "CategoriaId",
                table: "Productos");

            migrationBuilder.DropColumn(
                name: "id_Categoria",
                table: "Productos");

            migrationBuilder.CreateTable(
                name: "Categorias_Productos",
                columns: table => new
                {
                    CategoriaProductoId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdProducto = table.Column<long>(type: "bigint", nullable: false),
                    IdCategoria = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categorias_Productos", x => x.CategoriaProductoId);
                    table.ForeignKey(
                        name: "FK_Categorias_Productos_Categorias_IdCategoria",
                        column: x => x.IdCategoria,
                        principalTable: "Categorias",
                        principalColumn: "CategoriaId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Categorias_Productos_Productos_IdProducto",
                        column: x => x.IdProducto,
                        principalTable: "Productos",
                        principalColumn: "ProductoId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Categorias_Productos_IdCategoria",
                table: "Categorias_Productos",
                column: "IdCategoria");

            migrationBuilder.CreateIndex(
                name: "IX_Categorias_Productos_IdProducto",
                table: "Categorias_Productos",
                column: "IdProducto");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Categorias_Productos");

            migrationBuilder.AddColumn<long>(
                name: "CategoriaId",
                table: "Productos",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "id_Categoria",
                table: "Productos",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Productos_CategoriaId",
                table: "Productos",
                column: "CategoriaId");

            migrationBuilder.CreateIndex(
                name: "IX_Productos_id_Categoria",
                table: "Productos",
                column: "id_Categoria");

            migrationBuilder.AddForeignKey(
                name: "FK_Productos_Categorias_CategoriaId",
                table: "Productos",
                column: "CategoriaId",
                principalTable: "Categorias",
                principalColumn: "CategoriaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Productos_Categorias_id_Categoria",
                table: "Productos",
                column: "id_Categoria",
                principalTable: "Categorias",
                principalColumn: "CategoriaId",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
