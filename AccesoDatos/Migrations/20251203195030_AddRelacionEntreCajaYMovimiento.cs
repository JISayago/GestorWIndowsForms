using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AccesoDatos.Migrations
{
    /// <inheritdoc />
    public partial class AddRelacionEntreCajaYMovimiento : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            /*migrationBuilder.DropForeignKey(
                name: "FK_Movimientos_Cajas_CajaId",
                table: "Movimientos");

            migrationBuilder.DropIndex(
                name: "IX_Movimientos_CajaId",
                table: "Movimientos");

            migrationBuilder.DropColumn(
                name: "CajaId",
                table: "Movimientos");*/
            
            migrationBuilder.CreateIndex(
                name: "IX_Movimientos_id_Caja",
                table: "Movimientos",
                column: "id_Caja");

            migrationBuilder.AddForeignKey(
                name: "FK_Movimientos_Cajas_id_Caja",
                table: "Movimientos",
                column: "id_Caja",
                principalTable: "Cajas",
                principalColumn: "CajaId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Movimientos_Cajas_id_Caja",
                table: "Movimientos");

            migrationBuilder.DropIndex(
                name: "IX_Movimientos_id_Caja",
                table: "Movimientos");

            /*migrationBuilder.AddColumn<long>(
                name: "CajaId",
                table: "Movimientos",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_Movimientos_CajaId",
                table: "Movimientos",
                column: "CajaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Movimientos_Cajas_CajaId",
                table: "Movimientos",
                column: "CajaId",
                principalTable: "Cajas",
                principalColumn: "CajaId",
                onDelete: ReferentialAction.Cascade);*/
        }
    }
}
