using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AccesoDatos.Migrations
{
    /// <inheritdoc />
    public partial class Movimientos_Etapa2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                UPDATE m SET numero_movimiento = LEFT(m.numero_movimiento, 80) + '-D' + CAST(m.id_movimiento AS varchar(20))
                FROM Movimientos m
                WHERE EXISTS (SELECT 1 FROM Movimientos o
                              WHERE o.numero_movimiento = m.numero_movimiento AND o.id_movimiento < m.id_movimiento);");

            migrationBuilder.CreateIndex(
                name: "UX_Movimientos_NumeroMovimiento",
                table: "Movimientos",
                column: "numero_movimiento",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "UX_Movimientos_NumeroMovimiento",
                table: "Movimientos");
        }
    }
}
