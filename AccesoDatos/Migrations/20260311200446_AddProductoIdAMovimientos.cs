using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AccesoDatos.Migrations
{
    /// <inheritdoc />
    public partial class AddProductoIdAMovimientos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "EstaEliminado",
                table: "Lotes",
                newName: "EstaVencido");

            migrationBuilder.RenameColumn(
                name: "Cantidad",
                table: "Lotes",
                newName: "StockIncial");

            migrationBuilder.AddColumn<long>(
                name: "id_Producto",
                table: "Movimientos",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "EstaActivo",
                table: "Lotes",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "StockActual",
                table: "Lotes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Movimientos_id_Producto",
                table: "Movimientos",
                column: "id_Producto");

            migrationBuilder.AddForeignKey(
                name: "FK_Movimientos_Productos_id_Producto",
                table: "Movimientos",
                column: "id_Producto",
                principalTable: "Productos",
                principalColumn: "ProductoId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Movimientos_Productos_id_Producto",
                table: "Movimientos");

            migrationBuilder.DropIndex(
                name: "IX_Movimientos_id_Producto",
                table: "Movimientos");

            migrationBuilder.DropColumn(
                name: "id_Producto",
                table: "Movimientos");

            migrationBuilder.DropColumn(
                name: "EstaActivo",
                table: "Lotes");

            migrationBuilder.DropColumn(
                name: "StockActual",
                table: "Lotes");

            migrationBuilder.RenameColumn(
                name: "StockIncial",
                table: "Lotes",
                newName: "Cantidad");

            migrationBuilder.RenameColumn(
                name: "EstaVencido",
                table: "Lotes",
                newName: "EstaEliminado");
        }
    }
}
