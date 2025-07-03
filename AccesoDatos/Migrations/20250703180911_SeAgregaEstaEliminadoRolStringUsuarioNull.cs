using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AccesoDatos.Migrations
{
    /// <inheritdoc />
    public partial class SeAgregaEstaEliminadoRolStringUsuarioNull : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Productos_Marcas_MarcaId",
                table: "Productos");

            migrationBuilder.RenameColumn(
                name: "MarcaId",
                table: "Productos",
                newName: "MarcaCategoriaId");

            migrationBuilder.RenameIndex(
                name: "IX_Productos_MarcaId",
                table: "Productos",
                newName: "IX_Productos_MarcaCategoriaId");

            migrationBuilder.RenameColumn(
                name: "MarcaId",
                table: "Marcas",
                newName: "CategoriaId");

            migrationBuilder.AddColumn<bool>(
                name: "EstaEliminado",
                table: "TiposPago",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<string>(
                name: "detalleRol",
                table: "Roles",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<string>(
                name: "codigo_rol",
                table: "Roles",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddColumn<bool>(
                name: "EstaEliminado",
                table: "Roles",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "EstaEliminado",
                table: "Movimientos",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "EstaEliminado",
                table: "Marcas",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<string>(
                name: "username",
                table: "Empleados",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "pass",
                table: "Empleados",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.AddColumn<bool>(
                name: "EstaEliminado",
                table: "Categorias",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddForeignKey(
                name: "FK_Productos_Marcas_MarcaCategoriaId",
                table: "Productos",
                column: "MarcaCategoriaId",
                principalTable: "Marcas",
                principalColumn: "CategoriaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Productos_Marcas_MarcaCategoriaId",
                table: "Productos");

            migrationBuilder.DropColumn(
                name: "EstaEliminado",
                table: "TiposPago");

            migrationBuilder.DropColumn(
                name: "EstaEliminado",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "EstaEliminado",
                table: "Movimientos");

            migrationBuilder.DropColumn(
                name: "EstaEliminado",
                table: "Marcas");

            migrationBuilder.DropColumn(
                name: "EstaEliminado",
                table: "Categorias");

            migrationBuilder.RenameColumn(
                name: "MarcaCategoriaId",
                table: "Productos",
                newName: "MarcaId");

            migrationBuilder.RenameIndex(
                name: "IX_Productos_MarcaCategoriaId",
                table: "Productos",
                newName: "IX_Productos_MarcaId");

            migrationBuilder.RenameColumn(
                name: "CategoriaId",
                table: "Marcas",
                newName: "MarcaId");

            migrationBuilder.AlterColumn<long>(
                name: "detalleRol",
                table: "Roles",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<long>(
                name: "codigo_rol",
                table: "Roles",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "username",
                table: "Empleados",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "pass",
                table: "Empleados",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200,
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Productos_Marcas_MarcaId",
                table: "Productos",
                column: "MarcaId",
                principalTable: "Marcas",
                principalColumn: "MarcaId");
        }
    }
}
