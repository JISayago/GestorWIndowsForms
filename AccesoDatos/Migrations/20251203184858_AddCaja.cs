using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AccesoDatos.Migrations
{
    /// <inheritdoc />
    public partial class AddCaja : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "CajaId",
                table: "Movimientos",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Cajas",
                columns: table => new
                {
                    CajaId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    saldo_inicial = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    saldo_actual = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    fecha_apertura = table.Column<DateTime>(type: "datetime", nullable: false),
                    fecha_cierre = table.Column<DateTime>(type: "datetime", nullable: true),
                    total_ingresos = table.Column<decimal>(type: "decimal(18,0)", nullable: false),
                    total_egresos = table.Column<decimal>(type: "decimal(18,0)", nullable: false),
                    balance_final = table.Column<decimal>(type: "decimal(18,0)", nullable: false),
                    empleado_apertura = table.Column<long>(type: "bigint", nullable: false),
                    empleado_cierre = table.Column<long>(type: "bigint", nullable: true),
                    esta_cerrada = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cajas", x => x.CajaId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Movimientos_CajaId",
                table: "Movimientos",
                column: "CajaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Movimientos_Cajas_CajaId",
                table: "Movimientos",
                column: "CajaId",
                principalTable: "Cajas",
                principalColumn: "CajaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Movimientos_Cajas_CajaId",
                table: "Movimientos");

            migrationBuilder.DropTable(
                name: "Cajas");

            migrationBuilder.DropIndex(
                name: "IX_Movimientos_CajaId",
                table: "Movimientos");

            migrationBuilder.DropColumn(
                name: "CajaId",
                table: "Movimientos");
        }
    }
}
