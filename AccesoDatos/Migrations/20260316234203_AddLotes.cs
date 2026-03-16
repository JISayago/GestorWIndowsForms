using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AccesoDatos.Migrations
{
    /// <inheritdoc />
    public partial class AddLotes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Lotes_Productos_ProductoId",
                table: "Lotes");

            migrationBuilder.DropIndex(
                name: "IX_Lotes_ProductoId",
                table: "Lotes");

            migrationBuilder.DropColumn(
                name: "ProductoId",
                table: "Lotes");

            migrationBuilder.RenameColumn(
                name: "Descripcion",
                table: "Lotes",
                newName: "descripcion");

            migrationBuilder.RenameColumn(
                name: "StockIncial",
                table: "Lotes",
                newName: "stock_inicial");

            migrationBuilder.RenameColumn(
                name: "StockActual",
                table: "Lotes",
                newName: "stock_actual");

            migrationBuilder.RenameColumn(
                name: "NumeroLote",
                table: "Lotes",
                newName: "numero_lote");

            migrationBuilder.RenameColumn(
                name: "NombreLote",
                table: "Lotes",
                newName: "nombre_lote");

            migrationBuilder.RenameColumn(
                name: "IdProducto",
                table: "Lotes",
                newName: "id_Producto");

            migrationBuilder.RenameColumn(
                name: "FechaVencimiento",
                table: "Lotes",
                newName: "fecha_vencimiento");

            migrationBuilder.RenameColumn(
                name: "FechaAlta",
                table: "Lotes",
                newName: "fecha_alta");

            migrationBuilder.RenameColumn(
                name: "EstaVencido",
                table: "Lotes",
                newName: "esta_vencido");

            migrationBuilder.RenameColumn(
                name: "EstaActivo",
                table: "Lotes",
                newName: "esta_activo");

            migrationBuilder.RenameColumn(
                name: "LoteId",
                table: "Lotes",
                newName: "id_Lote");

            migrationBuilder.AlterColumn<string>(
                name: "descripcion",
                table: "Lotes",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<decimal>(
                name: "stock_inicial",
                table: "Lotes",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<decimal>(
                name: "stock_actual",
                table: "Lotes",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "numero_lote",
                table: "Lotes",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "nombre_lote",
                table: "Lotes",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "fecha_vencimiento",
                table: "Lotes",
                type: "date",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "fecha_alta",
                table: "Lotes",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.CreateIndex(
                name: "IX_Lotes_id_Producto",
                table: "Lotes",
                column: "id_Producto");

            migrationBuilder.AddForeignKey(
                name: "FK_Lotes_Productos_id_Producto",
                table: "Lotes",
                column: "id_Producto",
                principalTable: "Productos",
                principalColumn: "ProductoId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Lotes_Productos_id_Producto",
                table: "Lotes");

            migrationBuilder.DropIndex(
                name: "IX_Lotes_id_Producto",
                table: "Lotes");

            migrationBuilder.RenameColumn(
                name: "descripcion",
                table: "Lotes",
                newName: "Descripcion");

            migrationBuilder.RenameColumn(
                name: "stock_inicial",
                table: "Lotes",
                newName: "StockIncial");

            migrationBuilder.RenameColumn(
                name: "stock_actual",
                table: "Lotes",
                newName: "StockActual");

            migrationBuilder.RenameColumn(
                name: "numero_lote",
                table: "Lotes",
                newName: "NumeroLote");

            migrationBuilder.RenameColumn(
                name: "nombre_lote",
                table: "Lotes",
                newName: "NombreLote");

            migrationBuilder.RenameColumn(
                name: "id_Producto",
                table: "Lotes",
                newName: "IdProducto");

            migrationBuilder.RenameColumn(
                name: "fecha_vencimiento",
                table: "Lotes",
                newName: "FechaVencimiento");

            migrationBuilder.RenameColumn(
                name: "fecha_alta",
                table: "Lotes",
                newName: "FechaAlta");

            migrationBuilder.RenameColumn(
                name: "esta_vencido",
                table: "Lotes",
                newName: "EstaVencido");

            migrationBuilder.RenameColumn(
                name: "esta_activo",
                table: "Lotes",
                newName: "EstaActivo");

            migrationBuilder.RenameColumn(
                name: "id_Lote",
                table: "Lotes",
                newName: "LoteId");

            migrationBuilder.AlterColumn<string>(
                name: "Descripcion",
                table: "Lotes",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500);

            migrationBuilder.AlterColumn<int>(
                name: "StockIncial",
                table: "Lotes",
                type: "int",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<int>(
                name: "StockActual",
                table: "Lotes",
                type: "int",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<string>(
                name: "NumeroLote",
                table: "Lotes",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<string>(
                name: "NombreLote",
                table: "Lotes",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<DateTime>(
                name: "FechaVencimiento",
                table: "Lotes",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "date",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "FechaAlta",
                table: "Lotes",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "date");

            migrationBuilder.AddColumn<long>(
                name: "ProductoId",
                table: "Lotes",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_Lotes_ProductoId",
                table: "Lotes",
                column: "ProductoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Lotes_Productos_ProductoId",
                table: "Lotes",
                column: "ProductoId",
                principalTable: "Productos",
                principalColumn: "ProductoId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
