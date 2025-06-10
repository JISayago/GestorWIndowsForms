using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AccesoDatos.Migrations
{
    /// <inheritdoc />
    public partial class PrimeraMigracion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categorias",
                columns: table => new
                {
                    CategoriaId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categorias", x => x.CategoriaId);
                });

            migrationBuilder.CreateTable(
                name: "Marcas",
                columns: table => new
                {
                    MarcaId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Marcas", x => x.MarcaId);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    id_Rol = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    detalleRol = table.Column<long>(type: "bigint", nullable: false),
                    codigo_rol = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.id_Rol);
                });

            migrationBuilder.CreateTable(
                name: "TiposPago",
                columns: table => new
                {
                    id_TipoPago = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    detalle = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    codigo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TiposPago", x => x.id_TipoPago);
                });

            migrationBuilder.CreateTable(
                name: "Productos",
                columns: table => new
                {
                    ProductoId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    id_Categoria = table.Column<long>(type: "bigint", nullable: true),
                    id_Marca = table.Column<long>(type: "bigint", nullable: true),
                    stock = table.Column<int>(type: "int", nullable: false),
                    precio_costo = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    precio_venta = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    descripcion = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    esta_eliminado = table.Column<bool>(type: "bit", nullable: false),
                    estado = table.Column<int>(type: "int", nullable: false),
                    medida = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    unidad_medida = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CategoriaId = table.Column<long>(type: "bigint", nullable: true),
                    MarcaId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Productos", x => x.ProductoId);
                    table.ForeignKey(
                        name: "FK_Productos_Categorias_CategoriaId",
                        column: x => x.CategoriaId,
                        principalTable: "Categorias",
                        principalColumn: "CategoriaId");
                    table.ForeignKey(
                        name: "FK_Productos_Categorias_id_Categoria",
                        column: x => x.id_Categoria,
                        principalTable: "Categorias",
                        principalColumn: "CategoriaId",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Productos_Marcas_MarcaId",
                        column: x => x.MarcaId,
                        principalTable: "Marcas",
                        principalColumn: "MarcaId");
                    table.ForeignKey(
                        name: "FK_Productos_Marcas_id_Marca",
                        column: x => x.id_Marca,
                        principalTable: "Marcas",
                        principalColumn: "MarcaId",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "DetallesVenta",
                columns: table => new
                {
                    id_DetalleVenta = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    id_Venta = table.Column<long>(type: "bigint", nullable: false),
                    id_Producto = table.Column<long>(type: "bigint", nullable: false),
                    cantidad = table.Column<int>(type: "int", nullable: false),
                    subtotal = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DetallesVenta", x => x.id_DetalleVenta);
                    table.ForeignKey(
                        name: "FK_DetallesVenta_Productos_id_Producto",
                        column: x => x.id_Producto,
                        principalTable: "Productos",
                        principalColumn: "ProductoId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Empleados",
                columns: table => new
                {
                    PersonaId = table.Column<long>(type: "bigint", nullable: false),
                    legajo = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    fechaIngreso = table.Column<DateTime>(type: "date", nullable: false),
                    fechaEgreso = table.Column<DateTime>(type: "date", nullable: true),
                    estado = table.Column<int>(type: "int", nullable: false),
                    username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    pass = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    usuarioestahabilitado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Empleados", x => x.PersonaId);
                });

            migrationBuilder.CreateTable(
                name: "Empleados_Roles",
                columns: table => new
                {
                    EmpleadoRolId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdEmpleado = table.Column<long>(type: "bigint", nullable: false),
                    IdRol = table.Column<long>(type: "bigint", nullable: false),
                    FechaAsignacion = table.Column<DateTime>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Empleados_Roles", x => x.EmpleadoRolId);
                    table.ForeignKey(
                        name: "FK_Empleados_Roles_Empleados_IdEmpleado",
                        column: x => x.IdEmpleado,
                        principalTable: "Empleados",
                        principalColumn: "PersonaId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Empleados_Roles_Roles_IdRol",
                        column: x => x.IdRol,
                        principalTable: "Roles",
                        principalColumn: "id_Rol",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Personas",
                columns: table => new
                {
                    PersonaId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Apellido = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Dni = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Cuil = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Telefono = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Telefono2 = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Direccion = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    FechaNacimiento = table.Column<DateTime>(type: "date", nullable: true),
                    EstaEliminado = table.Column<bool>(type: "bit", nullable: false),
                    EmpleadoPersonaId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Personas", x => x.PersonaId);
                    table.ForeignKey(
                        name: "FK_Personas_Empleados_EmpleadoPersonaId",
                        column: x => x.EmpleadoPersonaId,
                        principalTable: "Empleados",
                        principalColumn: "PersonaId");
                });

            migrationBuilder.CreateTable(
                name: "Ventas",
                columns: table => new
                {
                    id_Venta = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    id_Empleado = table.Column<long>(type: "bigint", nullable: false),
                    numeroVenta = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    fecha_venta = table.Column<DateTime>(type: "date", nullable: false),
                    total = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    estado = table.Column<int>(type: "int", nullable: false),
                    detalle = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ventas", x => x.id_Venta);
                    table.ForeignKey(
                        name: "FK_Ventas_Empleados_id_Empleado",
                        column: x => x.id_Empleado,
                        principalTable: "Empleados",
                        principalColumn: "PersonaId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Movimientos",
                columns: table => new
                {
                    MovimientoId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdVenta = table.Column<long>(type: "bigint", nullable: true),
                    TipoMovimiento = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movimientos", x => x.MovimientoId);
                    table.ForeignKey(
                        name: "FK_Movimientos_Ventas_IdVenta",
                        column: x => x.IdVenta,
                        principalTable: "Ventas",
                        principalColumn: "id_Venta",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "VentaPagoDetalles",
                columns: table => new
                {
                    id_VentaPagoDetalle = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    id_Venta = table.Column<long>(type: "bigint", nullable: false),
                    id_TipoPago = table.Column<long>(type: "bigint", nullable: false),
                    monto = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VentaPagoDetalles", x => x.id_VentaPagoDetalle);
                    table.ForeignKey(
                        name: "FK_VentaPagoDetalles_TiposPago_id_TipoPago",
                        column: x => x.id_TipoPago,
                        principalTable: "TiposPago",
                        principalColumn: "id_TipoPago",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_VentaPagoDetalles_Ventas_id_Venta",
                        column: x => x.id_Venta,
                        principalTable: "Ventas",
                        principalColumn: "id_Venta",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DetallesVenta_id_Producto",
                table: "DetallesVenta",
                column: "id_Producto");

            migrationBuilder.CreateIndex(
                name: "IX_DetallesVenta_id_Venta",
                table: "DetallesVenta",
                column: "id_Venta");

            migrationBuilder.CreateIndex(
                name: "IX_Empleados_Roles_IdEmpleado",
                table: "Empleados_Roles",
                column: "IdEmpleado");

            migrationBuilder.CreateIndex(
                name: "IX_Empleados_Roles_IdRol",
                table: "Empleados_Roles",
                column: "IdRol");

            migrationBuilder.CreateIndex(
                name: "IX_Movimientos_IdVenta",
                table: "Movimientos",
                column: "IdVenta");

            migrationBuilder.CreateIndex(
                name: "IX_Personas_EmpleadoPersonaId",
                table: "Personas",
                column: "EmpleadoPersonaId");

            migrationBuilder.CreateIndex(
                name: "IX_Productos_CategoriaId",
                table: "Productos",
                column: "CategoriaId");

            migrationBuilder.CreateIndex(
                name: "IX_Productos_id_Categoria",
                table: "Productos",
                column: "id_Categoria");

            migrationBuilder.CreateIndex(
                name: "IX_Productos_id_Marca",
                table: "Productos",
                column: "id_Marca");

            migrationBuilder.CreateIndex(
                name: "IX_Productos_MarcaId",
                table: "Productos",
                column: "MarcaId");

            migrationBuilder.CreateIndex(
                name: "IX_Roles_codigo_rol",
                table: "Roles",
                column: "codigo_rol",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_VentaPagoDetalles_id_TipoPago",
                table: "VentaPagoDetalles",
                column: "id_TipoPago");

            migrationBuilder.CreateIndex(
                name: "IX_VentaPagoDetalles_id_Venta",
                table: "VentaPagoDetalles",
                column: "id_Venta");

            migrationBuilder.CreateIndex(
                name: "IX_Ventas_id_Empleado",
                table: "Ventas",
                column: "id_Empleado");

            migrationBuilder.AddForeignKey(
                name: "FK_DetallesVenta_Ventas_id_Venta",
                table: "DetallesVenta",
                column: "id_Venta",
                principalTable: "Ventas",
                principalColumn: "id_Venta",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Empleados_Personas_PersonaId",
                table: "Empleados",
                column: "PersonaId",
                principalTable: "Personas",
                principalColumn: "PersonaId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Empleados_Personas_PersonaId",
                table: "Empleados");

            migrationBuilder.DropTable(
                name: "DetallesVenta");

            migrationBuilder.DropTable(
                name: "Empleados_Roles");

            migrationBuilder.DropTable(
                name: "Movimientos");

            migrationBuilder.DropTable(
                name: "VentaPagoDetalles");

            migrationBuilder.DropTable(
                name: "Productos");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "TiposPago");

            migrationBuilder.DropTable(
                name: "Ventas");

            migrationBuilder.DropTable(
                name: "Categorias");

            migrationBuilder.DropTable(
                name: "Marcas");

            migrationBuilder.DropTable(
                name: "Personas");

            migrationBuilder.DropTable(
                name: "Empleados");
        }
    }
}
