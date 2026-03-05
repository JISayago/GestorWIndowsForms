using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AccesoDatos.Migrations
{
    /// <inheritdoc />
    public partial class AddColumnOfertaDescuentoIdDEtallesVentaTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "id_Producto",
                table: "DetallesVenta",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddColumn<long>(
                name: "id_OfertaDescuento",
                table: "DetallesVenta",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_DetallesVenta_id_OfertaDescuento",
                table: "DetallesVenta",
                column: "id_OfertaDescuento");

            migrationBuilder.AddForeignKey(
                name: "FK_DetallesVenta_OfertasDescuentos_id_OfertaDescuento",
                table: "DetallesVenta",
                column: "id_OfertaDescuento",
                principalTable: "OfertasDescuentos",
                principalColumn: "id_OfertaDescuento",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DetallesVenta_OfertasDescuentos_id_OfertaDescuento",
                table: "DetallesVenta");

            migrationBuilder.DropIndex(
                name: "IX_DetallesVenta_id_OfertaDescuento",
                table: "DetallesVenta");

            migrationBuilder.DropColumn(
                name: "id_OfertaDescuento",
                table: "DetallesVenta");

            migrationBuilder.AlterColumn<long>(
                name: "id_Producto",
                table: "DetallesVenta",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);
        }
    }
}
