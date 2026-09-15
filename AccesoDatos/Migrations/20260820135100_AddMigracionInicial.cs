using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AccesoDatos.Migrations
{
    /// <inheritdoc />
    public partial class AddMigracionInicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.CreateTable(
                name: "Categorias",
                columns: table => new
                {
                    CategoriaId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    EstaEliminado = table.Column<bool>(type: "bit", nullable: false)
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
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    EstaEliminado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Marcas", x => x.MarcaId);
                });

            migrationBuilder.CreateTable(
                name: "Movimientos",
                columns: table => new
                {
                    id_movimiento = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    numero_movimiento = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    tipo_movimiento = table.Column<int>(type: "int", nullable: false),
                    tipo_movimiento_detalle = table.Column<int>(type: "int", nullable: false),
                    monto = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    fecha_movimiento = table.Column<DateTime>(type: "datetime", nullable: false),
                    esta_eliminado = table.Column<bool>(type: "bit", nullable: false),
                    tipo_entidad = table.Column<int>(type: "int", nullable: true),
                    entidad_id = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movimientos", x => x.id_movimiento);
                });

            migrationBuilder.CreateTable(
                name: "NotasRapidas",
                columns: table => new
                {
                    id_Nota = table.Column<int>(type: "int", nullable: false),
                    cuerpo = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    fecha_modificacion = table.Column<DateTime>(type: "datetime", nullable: false),
                    usuario_nombre = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotasRapidas", x => x.id_Nota);
                });

            migrationBuilder.CreateTable(
                name: "OfertasDescuentos",
                columns: table => new
                {
                    id_OfertaDescuento = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    descripcion = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    codigo = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    fecha_inicio = table.Column<DateTime>(type: "date", nullable: false),
                    fecha_fin = table.Column<DateTime>(type: "date", nullable: true),
                    esta_activa = table.Column<bool>(type: "bit", nullable: false),
                    tipo_oferta = table.Column<int>(type: "int", nullable: false),
                    porcentaje_descuento = table.Column<decimal>(type: "decimal(5,2)", nullable: true),
                    precio_final = table.Column<decimal>(type: "decimal(18,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OfertasDescuentos", x => x.id_OfertaDescuento);
                });

            migrationBuilder.CreateTable(
                name: "Permisos",
                columns: table => new
                {
                    id_permiso = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    codigo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    descripcion = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permisos", x => x.id_permiso);
                });

            migrationBuilder.CreateTable(
                name: "Personas",
                columns: table => new
                {
                    PersonaId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Apellido = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Dni = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    Cuil = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    Telefono = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Telefono2 = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Direccion = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    FechaNacimiento = table.Column<DateTime>(type: "date", nullable: true),
                    EstaEliminado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Personas", x => x.PersonaId);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    id_Rol = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    detalleRol = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    codigo_rol = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    EstaEliminado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.id_Rol);
                });

            migrationBuilder.CreateTable(
                name: "Rubros",
                columns: table => new
                {
                    RubroId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    EstaEliminado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rubros", x => x.RubroId);
                });

            migrationBuilder.CreateTable(
                name: "TiposPago",
                columns: table => new
                {
                    id_TipoPago = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    detalle = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    codigo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    numero_referencia = table.Column<int>(type: "int", nullable: false),
                    metodo_pago_habilitado = table.Column<bool>(type: "bit", nullable: false),
                    EstaEliminado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TiposPago", x => x.id_TipoPago);
                });

            migrationBuilder.CreateTable(
                name: "Clientes",
                columns: table => new
                {
                    PersonaId = table.Column<long>(type: "bigint", nullable: false),
                    CuentaCorrienteId = table.Column<long>(type: "bigint", nullable: true),
                    numero_cliente = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    fecha_alta = table.Column<DateTime>(type: "date", nullable: false),
                    fecha_baja = table.Column<DateTime>(type: "date", nullable: true),
                    estado = table.Column<int>(type: "int", nullable: false),
                    estado_descripcion = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clientes", x => x.PersonaId);
                    table.ForeignKey(
                        name: "FK_Clientes_Personas_PersonaId",
                        column: x => x.PersonaId,
                        principalTable: "Personas",
                        principalColumn: "PersonaId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Empleados",
                columns: table => new
                {
                    PersonaId = table.Column<long>(type: "bigint", nullable: false),
                    legajo = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    fechaIngreso = table.Column<DateTime>(type: "date", nullable: false),
                    fechaEgreso = table.Column<DateTime>(type: "date", nullable: true),
                    estado = table.Column<int>(type: "int", nullable: false),
                    username = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    pass = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    usuarioestahabilitado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Empleados", x => x.PersonaId);
                    table.ForeignKey(
                        name: "FK_Empleados_Personas_PersonaId",
                        column: x => x.PersonaId,
                        principalTable: "Personas",
                        principalColumn: "PersonaId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Roles_Permisos",
                columns: table => new
                {
                    id_rol = table.Column<long>(type: "bigint", nullable: false),
                    id_permiso = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles_Permisos", x => new { x.id_rol, x.id_permiso });
                    table.ForeignKey(
                        name: "FK_Roles_Permisos_Permisos_id_permiso",
                        column: x => x.id_permiso,
                        principalTable: "Permisos",
                        principalColumn: "id_permiso",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Roles_Permisos_Roles_id_rol",
                        column: x => x.id_rol,
                        principalTable: "Roles",
                        principalColumn: "id_Rol",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Productos",
                columns: table => new
                {
                    ProductoId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    id_Marca = table.Column<long>(type: "bigint", nullable: false),
                    id_Rubro = table.Column<long>(type: "bigint", nullable: false),
                    control_por_lote = table.Column<bool>(type: "bit", nullable: false),
                    codigo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    codigo_barra = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    stock = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    es_fraccionable = table.Column<bool>(type: "bit", nullable: false),
                    precio_costo = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    precio_venta = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    iva_inluido_precio_final = table.Column<bool>(type: "bit", nullable: false),
                    descripcion = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    esta_eliminado = table.Column<bool>(type: "bit", nullable: false),
                    estado = table.Column<int>(type: "int", nullable: false),
                    medida = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    unidad_medida = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    tiene_vencimiento = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Productos", x => x.ProductoId);
                    table.ForeignKey(
                        name: "FK_Productos_Marcas_id_Marca",
                        column: x => x.id_Marca,
                        principalTable: "Marcas",
                        principalColumn: "MarcaId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Productos_Rubros_id_Rubro",
                        column: x => x.id_Rubro,
                        principalTable: "Rubros",
                        principalColumn: "RubroId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CuentasCorrientes",
                columns: table => new
                {
                    CuentaCorrienteId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClienteId = table.Column<long>(type: "bigint", nullable: false),
                    nombre_cuenta_corriente = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    saldo = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    limite_deuda = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    esta_eliminado = table.Column<bool>(type: "bit", nullable: false),
                    limite_deuda_activo = table.Column<bool>(type: "bit", nullable: false),
                    fecha_vencimiento = table.Column<DateTime>(type: "date", nullable: true),
                    EstadoCuentaCorriente = table.Column<int>(type: "int", nullable: false),
                    fecha_creacion = table.Column<DateTime>(type: "date", nullable: true),
                    fecha_activacion = table.Column<DateTime>(type: "date", nullable: true),
                    tipo_vencimiento = table.Column<int>(type: "int", nullable: false),
                    cantidad_meses_vencimiento = table.Column<int>(type: "int", nullable: false),
                    con_deuda = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CuentasCorrientes", x => x.CuentaCorrienteId);
                    table.ForeignKey(
                        name: "FK_CuentasCorrientes_Clientes_ClienteId",
                        column: x => x.ClienteId,
                        principalTable: "Clientes",
                        principalColumn: "PersonaId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CodigosRecuperacionPass",
                columns: table => new
                {
                    CodigoRecuperacionId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UsuarioAsignadoId = table.Column<long>(type: "bigint", nullable: false),
                    Codigo = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaExpiracion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EstaUsado = table.Column<bool>(type: "bit", nullable: false),
                    FechaUso = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CodigosRecuperacionPass", x => x.CodigoRecuperacionId);
                    table.ForeignKey(
                        name: "FK_CodigosRecuperacionPass_Empleados_UsuarioAsignadoId",
                        column: x => x.UsuarioAsignadoId,
                        principalTable: "Empleados",
                        principalColumn: "PersonaId",
                        onDelete: ReferentialAction.Cascade);
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
                name: "Gastos",
                columns: table => new
                {
                    id_Gasto = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    numero_gasto = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    id_Empleado = table.Column<long>(type: "bigint", nullable: false),
                    categoria_gasto = table.Column<int>(type: "int", nullable: false),
                    fecha_gasto = table.Column<DateTime>(type: "datetime2", nullable: true),
                    fecha_registro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    monto_total = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    monto_pagado = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    estado_gasto = table.Column<int>(type: "int", nullable: false),
                    detalle = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gastos", x => x.id_Gasto);
                    table.ForeignKey(
                        name: "FK_Gastos_Empleados_id_Empleado",
                        column: x => x.id_Empleado,
                        principalTable: "Empleados",
                        principalColumn: "PersonaId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Notificaciones",
                columns: table => new
                {
                    id_notificacion = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    titulo = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    descripcion = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    mensaje = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    empleado_id = table.Column<long>(type: "bigint", nullable: true),
                    fecha_vencimiento = table.Column<DateTime>(type: "datetime", nullable: true),
                    fecha_creacion = table.Column<DateTime>(type: "datetime", nullable: false),
                    fecha_confirmacion = table.Column<DateTime>(type: "datetime", nullable: true),
                    esta_leida = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notificaciones", x => x.id_notificacion);
                    table.ForeignKey(
                        name: "FK_Notificaciones_Empleados_empleado_id",
                        column: x => x.empleado_id,
                        principalTable: "Empleados",
                        principalColumn: "PersonaId",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios_Sesiones",
                columns: table => new
                {
                    UsuarioSesionId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UsuarioId = table.Column<long>(type: "bigint", nullable: false),
                    FechaLogin = table.Column<DateTime>(type: "datetime", nullable: false),
                    FechaLogout = table.Column<DateTime>(type: "datetime", nullable: true),
                    Activa = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios_Sesiones", x => x.UsuarioSesionId);
                    table.ForeignKey(
                        name: "FK_Usuarios_Sesiones_Empleados_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Empleados",
                        principalColumn: "PersonaId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Ventas",
                columns: table => new
                {
                    id_Venta = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    id_Empleado = table.Column<long>(type: "bigint", nullable: false),
                    id_Vendedor = table.Column<long>(type: "bigint", nullable: false),
                    id_cliente = table.Column<long>(type: "bigint", nullable: true),
                    numero_venta = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    fecha_venta = table.Column<DateTime>(type: "datetime2", nullable: false),
                    total = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    total_sin_descuento = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    descuento = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    estado = table.Column<int>(type: "int", nullable: false),
                    detalle = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    monto_adeudado = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    monto_pagado = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ventas", x => x.id_Venta);
                    table.ForeignKey(
                        name: "FK_Ventas_Clientes_id_cliente",
                        column: x => x.id_cliente,
                        principalTable: "Clientes",
                        principalColumn: "PersonaId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Ventas_Empleados_id_Empleado",
                        column: x => x.id_Empleado,
                        principalTable: "Empleados",
                        principalColumn: "PersonaId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Ventas_Empleados_id_Vendedor",
                        column: x => x.id_Vendedor,
                        principalTable: "Empleados",
                        principalColumn: "PersonaId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "VentasLibres",
                columns: table => new
                {
                    id_VentaLibre = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    id_Empleado = table.Column<long>(type: "bigint", nullable: false),
                    id_Vendedor = table.Column<long>(type: "bigint", nullable: false),
                    id_cliente = table.Column<long>(type: "bigint", nullable: true),
                    numero_venta = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    fecha_venta = table.Column<DateTime>(type: "datetime2", nullable: false),
                    total = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    estado = table.Column<int>(type: "int", nullable: false),
                    detalle = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    monto_adeudado = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    monto_pagado = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VentasLibres", x => x.id_VentaLibre);
                    table.ForeignKey(
                        name: "FK_VentasLibres_Clientes_id_cliente",
                        column: x => x.id_cliente,
                        principalTable: "Clientes",
                        principalColumn: "PersonaId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_VentasLibres_Empleados_id_Empleado",
                        column: x => x.id_Empleado,
                        principalTable: "Empleados",
                        principalColumn: "PersonaId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_VentasLibres_Empleados_id_Vendedor",
                        column: x => x.id_Vendedor,
                        principalTable: "Empleados",
                        principalColumn: "PersonaId",
                        onDelete: ReferentialAction.Restrict);
                });

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

            migrationBuilder.CreateTable(
                name: "Lotes",
                columns: table => new
                {
                    id_Lote = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    id_Producto = table.Column<long>(type: "bigint", nullable: false),
                    stock_inicial = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    stock_actual = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    numero_lote = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    descripcion = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    fecha_alta = table.Column<DateTime>(type: "datetime2", nullable: false),
                    fecha_vencimiento = table.Column<DateTime>(type: "datetime2", nullable: true),
                    esta_vencido = table.Column<bool>(type: "bit", nullable: false),
                    esta_activo = table.Column<bool>(type: "bit", nullable: false),
                    esta_eliminado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lotes", x => x.id_Lote);
                    table.ForeignKey(
                        name: "FK_Lotes_Productos_id_Producto",
                        column: x => x.id_Producto,
                        principalTable: "Productos",
                        principalColumn: "ProductoId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OfertaProductoEstadisticas",
                columns: table => new
                {
                    id_OfertaDescuento = table.Column<long>(type: "bigint", nullable: false),
                    id_Producto = table.Column<long>(type: "bigint", nullable: false),
                    cantidad_vendida = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    total_costo_acumulado = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    total_venta_acumulado = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    total_oferta_acumulado = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    fecha_ultima_venta = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OfertaProductoEstadisticas", x => new { x.id_OfertaDescuento, x.id_Producto });
                    table.ForeignKey(
                        name: "FK_OfertaProductoEstadisticas_OfertasDescuentos_id_OfertaDescuento",
                        column: x => x.id_OfertaDescuento,
                        principalTable: "OfertasDescuentos",
                        principalColumn: "id_OfertaDescuento",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OfertaProductoEstadisticas_Productos_id_Producto",
                        column: x => x.id_Producto,
                        principalTable: "Productos",
                        principalColumn: "ProductoId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProductosEnOfertaDescuentos",
                columns: table => new
                {
                    id_Producto = table.Column<long>(type: "bigint", nullable: false),
                    id_OfertaDescuento = table.Column<long>(type: "bigint", nullable: false),
                    cantidad_requerida = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    precio_venta_base = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    precio_costo_base = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    precio_oferta_base = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    limite_venta_producto = table.Column<decimal>(type: "decimal(18,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductosEnOfertaDescuentos", x => new { x.id_Producto, x.id_OfertaDescuento });
                    table.ForeignKey(
                        name: "FK_ProductosEnOfertaDescuentos_OfertasDescuentos_id_OfertaDescuento",
                        column: x => x.id_OfertaDescuento,
                        principalTable: "OfertasDescuentos",
                        principalColumn: "id_OfertaDescuento",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductosEnOfertaDescuentos_Productos_id_Producto",
                        column: x => x.id_Producto,
                        principalTable: "Productos",
                        principalColumn: "ProductoId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CuentaCorrienteAutorizados",
                columns: table => new
                {
                    CuentaCorrienteAutorizadoId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CuentaCorrienteId = table.Column<long>(type: "bigint", nullable: false),
                    dni = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CuentaCorrienteAutorizados", x => x.CuentaCorrienteAutorizadoId);
                    table.ForeignKey(
                        name: "FK_CuentaCorrienteAutorizados_CuentasCorrientes_CuentaCorrienteId",
                        column: x => x.CuentaCorrienteId,
                        principalTable: "CuentasCorrientes",
                        principalColumn: "CuentaCorrienteId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DetallesVenta",
                columns: table => new
                {
                    id_DetalleVenta = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    id_Venta = table.Column<long>(type: "bigint", nullable: false),
                    id_Producto = table.Column<long>(type: "bigint", nullable: true),
                    id_OfertaDescuento = table.Column<long>(type: "bigint", nullable: true),
                    cantidad = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    precio_unitario_original = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    precio_unitario_final = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    subtotal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    es_oferta = table.Column<bool>(type: "bit", nullable: false),
                    es_oferta_por_grupo = table.Column<bool>(type: "bit", nullable: false),
                    descripcion = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DetallesVenta", x => x.id_DetalleVenta);
                    table.ForeignKey(
                        name: "FK_DetallesVenta_OfertasDescuentos_id_OfertaDescuento",
                        column: x => x.id_OfertaDescuento,
                        principalTable: "OfertasDescuentos",
                        principalColumn: "id_OfertaDescuento",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DetallesVenta_Productos_id_Producto",
                        column: x => x.id_Producto,
                        principalTable: "Productos",
                        principalColumn: "ProductoId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DetallesVenta_Ventas_id_Venta",
                        column: x => x.id_Venta,
                        principalTable: "Ventas",
                        principalColumn: "id_Venta",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VentaPagoDetalles",
                columns: table => new
                {
                    id_VentaPagoDetalle = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    extra_descripcion_pago = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    id_Venta = table.Column<long>(type: "bigint", nullable: true),
                    IdVentaLibre = table.Column<long>(type: "bigint", nullable: true),
                    id_Gasto = table.Column<long>(type: "bigint", nullable: true),
                    id_TipoPago = table.Column<long>(type: "bigint", nullable: false),
                    monto = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VentaPagoDetalles", x => x.id_VentaPagoDetalle);
                    table.ForeignKey(
                        name: "FK_VentaPagoDetalles_Gastos_id_Gasto",
                        column: x => x.id_Gasto,
                        principalTable: "Gastos",
                        principalColumn: "id_Gasto",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_VentaPagoDetalles_TiposPago_id_TipoPago",
                        column: x => x.id_TipoPago,
                        principalTable: "TiposPago",
                        principalColumn: "id_TipoPago",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_VentaPagoDetalles_VentasLibres_IdVentaLibre",
                        column: x => x.IdVentaLibre,
                        principalTable: "VentasLibres",
                        principalColumn: "id_VentaLibre",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VentaPagoDetalles_Ventas_id_Venta",
                        column: x => x.id_Venta,
                        principalTable: "Ventas",
                        principalColumn: "id_Venta",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DetalleVentaLote",
                columns: table => new
                {
                    DetalleVentaLoteId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    id_Producto = table.Column<long>(type: "bigint", nullable: false),
                    id_Venta = table.Column<long>(type: "bigint", nullable: false),
                    id_Lote = table.Column<long>(type: "bigint", nullable: false),
                    cantidad = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DetalleVentaLote", x => x.DetalleVentaLoteId);
                    table.ForeignKey(
                        name: "FK_DetalleVentaLote_Lotes_id_Lote",
                        column: x => x.id_Lote,
                        principalTable: "Lotes",
                        principalColumn: "id_Lote",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DetalleVentaLote_Productos_id_Producto",
                        column: x => x.id_Producto,
                        principalTable: "Productos",
                        principalColumn: "ProductoId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DetalleVentaLote_Ventas_id_Venta",
                        column: x => x.id_Venta,
                        principalTable: "Ventas",
                        principalColumn: "id_Venta",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cajas_EstaCerrada",
                table: "Cajas",
                column: "esta_cerrada");

            migrationBuilder.CreateIndex(
                name: "IX_Cajas_FechaInicio_Desc",
                table: "Cajas",
                column: "fecha_apertura",
                descending: new bool[0]);

            migrationBuilder.CreateIndex(
                name: "IX_Categorias_Productos_IdCategoria",
                table: "Categorias_Productos",
                column: "IdCategoria");

            migrationBuilder.CreateIndex(
                name: "IX_Categorias_Productos_IdProducto",
                table: "Categorias_Productos",
                column: "IdProducto");

            migrationBuilder.CreateIndex(
                name: "IX_CodigosRecuperacionPass_Codigo",
                table: "CodigosRecuperacionPass",
                column: "Codigo");

            migrationBuilder.CreateIndex(
                name: "IX_CodigosRecuperacionPass_UsuarioAsignadoId",
                table: "CodigosRecuperacionPass",
                column: "UsuarioAsignadoId");

            migrationBuilder.CreateIndex(
                name: "IX_CuentaCorrienteAutorizados_CuentaCorrienteId",
                table: "CuentaCorrienteAutorizados",
                column: "CuentaCorrienteId");

            migrationBuilder.CreateIndex(
                name: "IX_CuentasCorrientes_ClienteId",
                table: "CuentasCorrientes",
                column: "ClienteId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DetallesVenta_id_OfertaDescuento",
                table: "DetallesVenta",
                column: "id_OfertaDescuento");

            migrationBuilder.CreateIndex(
                name: "IX_DetallesVenta_id_Producto",
                table: "DetallesVenta",
                column: "id_Producto");

            migrationBuilder.CreateIndex(
                name: "IX_DetallesVenta_id_Venta",
                table: "DetallesVenta",
                column: "id_Venta");

            migrationBuilder.CreateIndex(
                name: "IX_DetalleVentaLote_id_Lote",
                table: "DetalleVentaLote",
                column: "id_Lote");

            migrationBuilder.CreateIndex(
                name: "IX_DetalleVentaLote_id_Producto",
                table: "DetalleVentaLote",
                column: "id_Producto");

            migrationBuilder.CreateIndex(
                name: "IX_DetalleVentaLote_id_Venta",
                table: "DetalleVentaLote",
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
                name: "IX_Gastos_categoria_gasto",
                table: "Gastos",
                column: "categoria_gasto");

            migrationBuilder.CreateIndex(
                name: "IX_Gastos_EstadoGasto",
                table: "Gastos",
                column: "estado_gasto")
                .Annotation("SqlServer:Include", new[] { "fecha_gasto", "fecha_registro", "numero_gasto", "id_Empleado", "monto_total", "monto_pagado", "categoria_gasto" });

            migrationBuilder.CreateIndex(
                name: "IX_Gastos_fecha_gasto",
                table: "Gastos",
                column: "fecha_gasto");

            migrationBuilder.CreateIndex(
                name: "IX_Gastos_id_Empleado",
                table: "Gastos",
                column: "id_Empleado");

            migrationBuilder.CreateIndex(
                name: "IX_Lotes_id_Producto",
                table: "Lotes",
                column: "id_Producto");

            migrationBuilder.CreateIndex(
                name: "IX_Movimientos_FechaMovimiento_Desc",
                table: "Movimientos",
                column: "fecha_movimiento",
                descending: new bool[0]);

            migrationBuilder.CreateIndex(
                name: "IX_Notificaciones_empleado_id",
                table: "Notificaciones",
                column: "empleado_id");

            migrationBuilder.CreateIndex(
                name: "IX_OfertaProductoEstadisticas_id_OfertaDescuento_id_Producto",
                table: "OfertaProductoEstadisticas",
                columns: new[] { "id_OfertaDescuento", "id_Producto" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_OfertaProductoEstadisticas_id_Producto",
                table: "OfertaProductoEstadisticas",
                column: "id_Producto");

            migrationBuilder.CreateIndex(
                name: "IX_Permisos_codigo",
                table: "Permisos",
                column: "codigo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Productos_id_Marca",
                table: "Productos",
                column: "id_Marca");

            migrationBuilder.CreateIndex(
                name: "IX_Productos_id_Rubro",
                table: "Productos",
                column: "id_Rubro");

            migrationBuilder.CreateIndex(
                name: "IX_ProductosEnOfertaDescuentos_id_OfertaDescuento",
                table: "ProductosEnOfertaDescuentos",
                column: "id_OfertaDescuento");

            migrationBuilder.CreateIndex(
                name: "IX_ProductosEnOfertaDescuentos_id_Producto",
                table: "ProductosEnOfertaDescuentos",
                column: "id_Producto");

            migrationBuilder.CreateIndex(
                name: "IX_Roles_codigo_rol",
                table: "Roles",
                column: "codigo_rol",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Roles_Permisos_id_permiso",
                table: "Roles_Permisos",
                column: "id_permiso");

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_Sesiones_UsuarioId",
                table: "Usuarios_Sesiones",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_VentaPagoDetalles_id_Gasto",
                table: "VentaPagoDetalles",
                column: "id_Gasto");

            migrationBuilder.CreateIndex(
                name: "IX_VentaPagoDetalles_id_TipoPago",
                table: "VentaPagoDetalles",
                column: "id_TipoPago");

            migrationBuilder.CreateIndex(
                name: "IX_VentaPagoDetalles_id_Venta",
                table: "VentaPagoDetalles",
                column: "id_Venta");

            migrationBuilder.CreateIndex(
                name: "IX_VentaPagoDetalles_IdVentaLibre",
                table: "VentaPagoDetalles",
                column: "IdVentaLibre");

            migrationBuilder.CreateIndex(
                name: "IX_Ventas_Estado_FechaVenta",
                table: "Ventas",
                columns: new[] { "estado", "fecha_venta" });

            migrationBuilder.CreateIndex(
                name: "IX_Ventas_FechaVenta_Desc",
                table: "Ventas",
                column: "fecha_venta",
                descending: new bool[0])
                .Annotation("SqlServer:Include", new[] { "estado", "numero_venta", "total", "detalle", "id_cliente" });

            migrationBuilder.CreateIndex(
                name: "IX_Ventas_id_cliente",
                table: "Ventas",
                column: "id_cliente");

            migrationBuilder.CreateIndex(
                name: "IX_Ventas_id_Empleado",
                table: "Ventas",
                column: "id_Empleado");

            migrationBuilder.CreateIndex(
                name: "IX_Ventas_id_Vendedor",
                table: "Ventas",
                column: "id_Vendedor");

            migrationBuilder.CreateIndex(
                name: "IX_Ventas_NumeroVenta",
                table: "Ventas",
                column: "numero_venta");

            migrationBuilder.CreateIndex(
                name: "IX_VentasLibres_FechaVenta_Desc",
                table: "VentasLibres",
                column: "fecha_venta",
                descending: new bool[0])
                .Annotation("SqlServer:Include", new[] { "estado", "numero_venta", "total", "id_cliente" });

            migrationBuilder.CreateIndex(
                name: "IX_VentasLibres_id_cliente",
                table: "VentasLibres",
                column: "id_cliente");

            migrationBuilder.CreateIndex(
                name: "IX_VentasLibres_id_Empleado",
                table: "VentasLibres",
                column: "id_Empleado");

            migrationBuilder.CreateIndex(
                name: "IX_VentasLibres_id_Vendedor",
                table: "VentasLibres",
                column: "id_Vendedor");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cajas");

            migrationBuilder.DropTable(
                name: "Categorias_Productos");

            migrationBuilder.DropTable(
                name: "CodigosRecuperacionPass");

            migrationBuilder.DropTable(
                name: "CuentaCorrienteAutorizados");

            migrationBuilder.DropTable(
                name: "DetallesVenta");

            migrationBuilder.DropTable(
                name: "DetalleVentaLote");

            migrationBuilder.DropTable(
                name: "Empleados_Roles");

            migrationBuilder.DropTable(
                name: "Movimientos");

            migrationBuilder.DropTable(
                name: "NotasRapidas");

            migrationBuilder.DropTable(
                name: "Notificaciones");

            migrationBuilder.DropTable(
                name: "OfertaProductoEstadisticas");

            migrationBuilder.DropTable(
                name: "ProductosEnOfertaDescuentos");

            migrationBuilder.DropTable(
                name: "Roles_Permisos");

            migrationBuilder.DropTable(
                name: "Usuarios_Sesiones");

            migrationBuilder.DropTable(
                name: "VentaPagoDetalles");

            migrationBuilder.DropTable(
                name: "Categorias");

            migrationBuilder.DropTable(
                name: "CuentasCorrientes");

            migrationBuilder.DropTable(
                name: "Lotes");

            migrationBuilder.DropTable(
                name: "OfertasDescuentos");

            migrationBuilder.DropTable(
                name: "Permisos");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Gastos");

            migrationBuilder.DropTable(
                name: "TiposPago");

            migrationBuilder.DropTable(
                name: "VentasLibres");

            migrationBuilder.DropTable(
                name: "Ventas");

            migrationBuilder.DropTable(
                name: "Productos");

            migrationBuilder.DropTable(
                name: "Clientes");

            migrationBuilder.DropTable(
                name: "Empleados");

            migrationBuilder.DropTable(
                name: "Marcas");

            migrationBuilder.DropTable(
                name: "Rubros");

            migrationBuilder.DropTable(
                name: "Personas");
        }
    }
}
