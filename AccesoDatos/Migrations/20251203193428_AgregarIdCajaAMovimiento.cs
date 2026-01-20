using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AccesoDatos.Migrations
{
    /// <inheritdoc />
    public partial class AgregarIdCajaAMovimiento : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            /*migrationBuilder.DropForeignKey(
                name: "FK_Movimientos_Cajas_CajaId",
                table: "Movimientos");*/

           /* migrationBuilder.AlterColumn<long>(
                name: "CajaId",
                table: "Movimientos",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);*/

            migrationBuilder.AddColumn<long>(
                name: "id_Caja",
                table: "Movimientos",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            /*migrationBuilder.AddForeignKey(
                name: "FK_Movimientos_Cajas_CajaId",
                table: "Movimientos",
                column: "CajaId",
                principalTable: "Cajas",
                principalColumn: "CajaId",
                onDelete: ReferentialAction.Cascade);*/
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            /*migrationBuilder.DropForeignKey(
                name: "FK_Movimientos_Cajas_CajaId",
                table: "Movimientos");*/

            migrationBuilder.DropColumn(
                name: "id_Caja",
                table: "Movimientos");

            /*migrationBuilder.AlterColumn<long>(
                name: "CajaId",
                table: "Movimientos",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddForeignKey(
                name: "FK_Movimientos_Cajas_CajaId",
                table: "Movimientos",
                column: "CajaId",
                principalTable: "Cajas",
                principalColumn: "CajaId");*/
        }
    }
}
