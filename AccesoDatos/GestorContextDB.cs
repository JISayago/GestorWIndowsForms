using AccesoDatos.Entidades;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatos
{
    public class GestorContextDB : DbContext
    {
       
        public GestorContextDB(DbContextOptions<GestorContextDB> options) : base(options) { }
        public DbSet<Persona> Personas { get; set; }
        public DbSet<Empleado> Empleados { get; set; }
        public DbSet<DetallesVenta> DetallesVentas { get; set; }
        public DbSet<Venta> Ventas { get; set; }
        public DbSet<Rol> Roles { get; set; }
        public DbSet<TipoPago> TiposPago { get; set; }
        public DbSet<Producto> Productos { get; set; }
        public DbSet<Movimiento> Movimientos { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Marca> Marcas { get; set; }
        public DbSet<Rubro> Rubros { get; set; }
        public DbSet<EmpleadoRol> EmpleadoRoles { get; set; }
        public DbSet<CategoriaProducto> CategoriasProductos { get; set; }
        public DbSet<VentaPagoDetalle> VentaPagosDetalles { get; set; }
        public DbSet<OfertaDescuento> OfertasDescuentos{ get; set; }
        public DbSet<ProductosEnOfertaDescuentos> ProductosEnOfertasDescuentos { get; set; }
        public DbSet<Cliente> Cliente { get; set; }
        public DbSet<CuentaCorriente> CuentaCorriente { get; set; }
        public DbSet<MovimientoCuentaCorriente> MovimientoCuentaCorriente { get; set; }
        public DbSet<CuentaCorrienteAutorizado> CuentaCorrienteAutorizados { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
            {

                base.OnModelCreating(modelBuilder);

            // PERSONA
            modelBuilder.Entity<Persona>(entity =>
            {
                entity.ToTable("Personas");

                entity.HasKey(p => p.PersonaId);

                entity.Property(p => p.PersonaId)
                      .ValueGeneratedOnAdd();
                entity.Property(p => p.Nombre).HasMaxLength(50).IsRequired();
                entity.Property(p => p.Apellido).HasMaxLength(50).IsRequired();
                entity.Property(p => p.Dni).HasMaxLength(15).IsRequired();
                entity.Property(p => p.Cuil).HasMaxLength(15).IsRequired();
                entity.Property(p => p.Telefono).HasMaxLength(20).IsRequired();

                entity.Property(p => p.Telefono2).HasMaxLength(20);
                entity.Property(p => p.Email).HasMaxLength(100);
                entity.Property(p => p.Direccion).HasMaxLength(100);

                entity.Property(p => p.FechaNacimiento).HasColumnType("date");
                entity.Property(p => p.EstaEliminado).IsRequired();

                /*entity.HasOne(p => p.Empleado)
                      .WithOne(e => e.Persona)
                      .HasForeignKey<Empleado>(e => e.PersonaId)
                      .OnDelete(DeleteBehavior.Restrict);*/

                
            });

            // EMPLEADO
            modelBuilder.Entity<Empleado>(entity =>
            {
                entity.ToTable("Empleados");
                entity.HasKey(e => e.PersonaId);
                entity.Property(e => e.PersonaId).HasColumnName("PersonaId");
                entity.Property(e => e.Legajo).HasColumnName("legajo").HasMaxLength(20);
                entity.Property(e => e.FechaIngreso).HasColumnName("fechaIngreso").HasColumnType("date");
                entity.Property(e => e.FechaEgreso).HasColumnName("fechaEgreso").HasColumnType("date");
                entity.Property(e => e.Estado).HasColumnName("estado").IsRequired();
                entity.Property(e => e.Username).HasColumnName("username").HasMaxLength(100);
                entity.Property(e => e.Pass).HasColumnName("pass").HasMaxLength(200);
                entity.Property(e => e.UsuarioEstaHabilitado).HasColumnName("usuarioestahabilitado");

                // Relación 1 a 1 con Persona
                entity.HasOne(e => e.Persona)
                      .WithOne()
                      .HasForeignKey<Empleado>(e => e.PersonaId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            //PRODUCTO
            modelBuilder.Entity<Producto>(entity =>
            {
                entity.ToTable("Productos");

                entity.HasKey(p => p.ProductoId);

                entity.Property(p => p.ProductoId)
                    .HasColumnName("ProductoId");

                entity.Property(p => p.Stock)
                    .HasColumnName("stock")
                    .HasColumnType("decimal(18,2)")
                    .IsRequired();

                entity.Property(p => p.PrecioCosto)
                    .HasColumnName("precio_costo")
                    .HasColumnType("decimal(18,2)")
                    .IsRequired();

                entity.Property(p => p.PrecioVenta)
                    .HasColumnName("precio_venta")
                    .HasColumnType("decimal(18,2)")
                    .IsRequired();

                entity.Property(p => p.Descripcion)
                    .HasColumnName("descripcion")
                    .HasMaxLength(1000);

                entity.Property(p => p.EstaEliminado)
                    .HasColumnName("esta_eliminado")
                    .IsRequired();

                entity.Property(p => p.Estado)
                    .HasColumnName("estado");

                entity.Property(p => p.Medida)
                    .HasColumnName("medida")
                    .HasMaxLength(20);

                entity.Property(p => p.UnidadMedida)
                    .HasColumnName("unidad_medida")
                    .HasMaxLength(50);

                entity.Property(p => p.Codigo)
                    .HasColumnName("codigo")
                    .HasMaxLength(50);

                entity.Property(p => p.CodigoBarra)
                    .HasColumnName("codigo_barra")
                    .HasMaxLength(50);

                entity.Property(p => p.IvaIncluidoPrecioFinal)
                    .HasColumnName("iva_inluido_precio_final")
                    .IsRequired();

                entity.Property(p => p.EsFraccionable)
                    .HasColumnName("es_fraccionable")
                    .IsRequired();

                entity.Property(p => p.IdMarca)
                    .HasColumnName("id_Marca");

                entity.Property(p => p.IdRubro)
                    .HasColumnName("id_Rubro");

                // 🔗 Relaciones con Marca
                entity.HasOne(p => p.Marca)
                    .WithMany(m => m.Productos)
                    .HasForeignKey(p => p.IdMarca);

                // 🔗 Relaciones con Rubro
                entity.HasOne(p => p.Rubro)
                    .WithMany(r => r.Productos)
                    .HasForeignKey(p => p.IdRubro);

                // 🔗 Relación con DetallesVenta
                entity.HasMany(p => p.DetallesVentas)
                    .WithOne(dv => dv.Producto)
                    .HasForeignKey(dv => dv.IdProducto)
                    .OnDelete(DeleteBehavior.Restrict);
            });


            // DETALLEVENTA
            modelBuilder.Entity<DetallesVenta>(entity =>
            {
                entity.ToTable("DetallesVenta");

                entity.HasKey(e => e.DetalleVentaId);

                entity.Property(e => e.DetalleVentaId)
                    .HasColumnName("id_DetalleVenta");

                entity.Property(e => e.IdVenta)
                    .HasColumnName("id_Venta")
                    .IsRequired();

                entity.Property(e => e.IdProducto)
                    .HasColumnName("id_Producto")
                    .IsRequired();

                entity.Property(e => e.Cantidad)
                    .HasColumnName("cantidad")
                    .HasColumnType("decimal(18,2)")
                    .IsRequired();

                entity.Property(e => e.Subtotal)
                    .HasColumnName("subtotal")
                    .HasColumnType("decimal(18,2)")
                    .IsRequired();

                entity.HasOne(d => d.Venta)
                    .WithMany(v => v.DetallesVentas)
                    .HasForeignKey(d => d.IdVenta)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(d => d.Producto)
                    .WithMany(p => p.DetallesVentas)
                    .HasForeignKey(d => d.IdProducto)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // VENTA
            modelBuilder.Entity<Venta>(entity =>
            {
                entity.ToTable("Ventas");

                entity.HasKey(e => e.VentaId);

                entity.Property(e => e.VentaId)
                    .HasColumnName("id_Venta");

                entity.Property(e => e.IdEmpleado)
                    .HasColumnName("id_Empleado")
                    .IsRequired();

                entity.Property(e => e.IdVendedor)
                    .HasColumnName("id_Vendedor")
                    .IsRequired();

                entity.Property(e => e.NumeroVenta)
                    .HasColumnName("numero_venta")
                    .HasMaxLength(200);

                entity.Property(e => e.FechaVenta)
                    .HasColumnName("fecha_venta")
                    .HasColumnType("date")
                    .IsRequired();

                entity.Property(e => e.Total)
                    .HasColumnName("total")
                    .HasColumnType("decimal(18,2)")
                    .IsRequired();

                entity.Property(e => e.TotalSinDescuento)
                    .HasColumnName("total_sin_descuento")
                    .HasColumnType("decimal(18,2)")
                    .IsRequired();

                entity.Property(e => e.Descuento)
                    .HasColumnName("descuento")
                    .HasColumnType("decimal(18,2)")
                    .IsRequired();

                entity.Property(e => e.Estado)
                    .HasColumnName("estado")
                    .IsRequired();

                entity.Property(e => e.Detalle)
                    .HasColumnName("detalle")
                    .HasMaxLength(500);

                entity.Property(e => e.MontoAdeudado)
                    .HasColumnName("monto_adeudado")
                    .HasColumnType("decimal(18,2)")
                    .IsRequired();

                entity.Property(e => e.MontoPagado)
                    .HasColumnName("monto_pagado")
                    .HasColumnType("decimal(18,2)")
                    .IsRequired();

                // Relaciones
                entity.HasOne(e => e.Empleado)
                    .WithMany(emp => emp.Ventas)
                    .HasForeignKey(e => e.IdEmpleado)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.Vendedor)
                    .WithMany() // o .WithMany(emp => emp.VentasComoVendedor) si definís la colección
                    .HasForeignKey(e => e.IdVendedor)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasMany(e => e.DetallesVentas)
                    .WithOne(dv => dv.Venta)
                    .HasForeignKey(dv => dv.IdVenta)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasMany(e => e.VentaPagoDetalles)
                    .WithOne(vp => vp.Venta)
                    .HasForeignKey(vp => vp.IdVenta)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            //VENTA PAGO DETALLE
            modelBuilder.Entity<VentaPagoDetalle>(entity =>
            {
                entity.ToTable("VentaPagoDetalles");

                entity.HasKey(e => e.VentaPagoDetalleId);

                entity.Property(e => e.VentaPagoDetalleId)
                    .HasColumnName("id_VentaPagoDetalle");

                entity.Property(e => e.IdVenta)
                    .HasColumnName("id_Venta")
                    .IsRequired();

                entity.Property(e => e.IdTipoPago)
                    .HasColumnName("id_TipoPago")
                    .IsRequired();

                entity.Property(e => e.Monto)
                    .HasColumnName("monto")
                    .HasColumnType("decimal(18,2)")
                    .IsRequired();

                entity.HasOne(e => e.Venta)
                    .WithMany(v => v.VentaPagoDetalles)
                    .HasForeignKey(e => e.IdVenta)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.TipoPago)
                    .WithMany()
                    .HasForeignKey(e => e.IdTipoPago)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            //TIPO PAGO
            modelBuilder.Entity<TipoPago>(entity =>
            {
                entity.ToTable("TiposPago");

                entity.HasKey(tp => tp.TipoPagoId);

                entity.Property(tp => tp.TipoPagoId)
                    .HasColumnName("id_TipoPago");

                entity.Property(tp => tp.Nombre)
                    .HasColumnName("nombre")
                    .HasMaxLength(100)
                    .IsRequired();
                entity.Property(tp => tp.NumeroReferencia)
                .HasColumnName("numero_referencia")
                .IsRequired();

                entity.Property(tp => tp.MetodoPagoHabilitado)
                            .HasColumnName("metodo_pago_habilitado");

                entity.Property(tp => tp.Detalle)
                    .HasColumnName("detalle")
                    .HasMaxLength(250);

                entity.Property(tp => tp.Codigo)
                    .HasColumnName("codigo")
                    .HasMaxLength(50);
            });

            //ROL
            modelBuilder.Entity<Rol>(entity =>
            {
                entity.ToTable("Roles");

                entity.HasKey(e => e.RolId);

                entity.Property(e => e.RolId)
                    .HasColumnName("id_Rol");

                entity.Property(e => e.Nombre)
                    .HasColumnName("nombre")
                    .HasMaxLength(100)
                    .IsRequired();

                entity.Property(e => e.DetalleRol)
                    .HasColumnName("detalleRol")
                    .HasMaxLength(200)
                    .IsRequired();

                entity.Property(e => e.CodigoRol)
                    .HasColumnName("codigo_rol")
                    .HasMaxLength(50)
                    .IsRequired();

                entity.HasIndex(e => e.CodigoRol)
                    .IsUnique();
            });

            // MOVIMIENTO
            modelBuilder.Entity<Movimiento>(entity =>
            {
                entity.ToTable("Movimientos");

                entity.HasKey(e => e.MovimientoId);

                entity.Property(e => e.TipoMovimiento).IsRequired();

                // Relación con Venta (si tenés definida la entidad Venta)
                entity.HasOne(e => e.Venta)
                      .WithMany() // o .WithMany(v => v.Movimientos) si agregás la colección en Venta
                      .HasForeignKey(e => e.IdVenta)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            // CATEGORIA
            modelBuilder.Entity<Categoria>(entity =>
            {
                entity.ToTable("Categorias");

                entity.HasKey(e => e.CategoriaId);
                entity.Property(e => e.Nombre)
                      .IsRequired()
                      .HasMaxLength(100);
            });

            // MARCA
            modelBuilder.Entity<Marca>(entity =>
            {
                entity.ToTable("Marcas");

                entity.HasKey(e => e.MarcaId);
                entity.Property(e => e.Nombre)
                      .IsRequired()
                      .HasMaxLength(100);
                entity.HasMany(m => m.Productos)
                      .WithOne(p => p.Marca)
                      .HasForeignKey(p => p.IdMarca);
            });

            // RUBRO
            modelBuilder.Entity<Rubro>(entity =>
            {
                entity.ToTable("Rubros");

                entity.HasKey(e => e.RubroId);
                entity.Property(e => e.Nombre)
                      .IsRequired()
                      .HasMaxLength(100);
                entity.HasMany(m => m.Productos)
                      .WithOne(p => p.Rubro)
                      .HasForeignKey(p => p.IdRubro);
            });

            //EMPELADO ROL
            modelBuilder.Entity<EmpleadoRol>(entity =>
            {
                entity.ToTable("Empleados_Roles");

                entity.HasKey(e => e.EmpleadoRolId);

                entity.Property(e => e.FechaAsignacion)
                      .HasColumnType("date");

                entity.HasOne(e => e.Empleado)
                      .WithMany(e => e.EmpleadoRoles)
                      .HasForeignKey(e => e.IdEmpleado)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.Rol)
                      .WithMany(r => r.EmpleadosRoles)
                      .HasForeignKey(e => e.IdRol)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            //CATEGORIA PRODUCTO
            modelBuilder.Entity<CategoriaProducto>(entity =>
            {
                entity.ToTable("Categorias_Productos");

                entity.HasKey(e => e.CategoriaProductoId);

                entity.HasOne(cp => cp.Producto)
                      .WithMany(p => p.CategoriasProductos)
                      .HasForeignKey(cp => cp.IdProducto)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(cp => cp.Categoria)
                      .WithMany(c => c.CategoriasProductos)
                      .HasForeignKey(cp => cp.IdCategoria)
                      .OnDelete(DeleteBehavior.Restrict);
            });
            // OFERTA DESCUENTO
            modelBuilder.Entity<OfertaDescuento>(entity =>
            {
                entity.ToTable("OfertasDescuentos");

                entity.HasKey(o => o.OfertaDescuentoId);

                entity.Property(o => o.OfertaDescuentoId)
                      .HasColumnName("id_OfertaDescuento");

                entity.Property(o => o.Descripcion)
                      .HasColumnName("descripcion")
                      .HasMaxLength(500)
                      .IsRequired();

                entity.Property(o => o.PrecioFinal)
                      .HasColumnName("precio_final")
                      .HasColumnType("decimal(18,2)")
                      .IsRequired();

                entity.Property(o => o.PrecioOriginal)
                      .HasColumnName("precio_original")
                      .HasColumnType("decimal(18,2)")
                      .IsRequired();

                entity.Property(o => o.DescuentoTotalFinal)
                      .HasColumnName("descuento_total_final")
                      .HasColumnType("decimal(18,2)");

                entity.Property(o => o.PorcentajeDescuento)
                      .HasColumnName("porcentaje_descuento")
                      .HasColumnType("decimal(5,2)");

                entity.Property(o => o.FechaInicio)
                      .HasColumnName("fecha_inicio")
                      .HasColumnType("date")
                      .IsRequired();

                entity.Property(o => o.FechaFin)
                      .HasColumnName("fecha_fin")
                      .HasColumnType("date");

                entity.Property(o => o.CantidadProductosDentroOferta)
                      .HasColumnName("cantidad_productos_dentro_oferta")
                      .HasColumnType("decimal(18,2)");

                entity.Property(o => o.EstaActiva)
                      .HasColumnName("esta_activa")
                      .IsRequired();

                entity.Property(o => o.EsUnSoloProducto)
                      .HasColumnName("es_un_solo_producto")
                      .IsRequired();

                // 🔗 Relación 1:N con ProductosEnOfertaDescuentos
                entity.HasMany(o => o.Productos)
                      .WithOne(po => po.Oferta)
                      .HasForeignKey(po => po.OfertaId)
                      .OnDelete(DeleteBehavior.Cascade);
            });


            // PRODUCTOS EN OFERTA DESCUENTOS
            modelBuilder.Entity<ProductosEnOfertaDescuentos>(entity =>
            {
                entity.ToTable("ProductosEnOfertaDescuentos");

                entity.HasKey(po => po.ProductosEnOfertaDescuentosId);

                entity.Property(po => po.ProductosEnOfertaDescuentosId)
                      .HasColumnName("id_ProductosEnOfertaDescuento");

                entity.Property(po => po.OfertaId)
                      .HasColumnName("id_OfertaDescuento")
                      .IsRequired();

                entity.Property(po => po.ProductoId)
                      .HasColumnName("id_Producto")
                      .IsRequired();

                entity.Property(po => po.Cantidad)
                      .HasColumnName("cantidad")
                      .HasColumnType("decimal(18,2)")
                      .IsRequired();

                entity.Property(po => po.PrecioUnitarioOferta)
                      .HasColumnName("precio_unitario_oferta")
                      .HasColumnType("decimal(18,2)");

                entity.Property(po => po.DescuentoPorcentaje)
                      .HasColumnName("descuento_porcentaje")
                      .HasColumnType("decimal(5,2)");

                // 🔗 Relación con Producto
                entity.HasOne(po => po.Producto)
                      .WithMany() // si querés, podés agregar ICollection<ProductosEnOfertaDescuentos> en Producto
                      .HasForeignKey(po => po.ProductoId)
                      .OnDelete(DeleteBehavior.Restrict);

                // 🔗 Relación con OfertaDescuento
                entity.HasOne(po => po.Oferta)
                      .WithMany(o => o.Productos)
                      .HasForeignKey(po => po.OfertaId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // CUENTA CORRIENTE
            modelBuilder.Entity<CuentaCorriente>(entity =>
            {
                entity.ToTable("CuentasCorrientes");
                entity.HasKey(cc => cc.CuentaCorrienteId);
                entity.Property(cc => cc.CuentaCorrienteId)
                      .HasColumnName("CuentaCorrienteId")
                      .ValueGeneratedOnAdd();
                entity.Property(cc => cc.ClienteId)
                      .HasColumnName("ClienteId")
                      .IsRequired();
                entity.Property(cc => cc.NombreCuentaCorriente)
                      .HasColumnName("nombre_cuenta_corriente")
                      .HasMaxLength(100)
                      .IsRequired();
                entity.Property(cc => cc.Saldo)
                      .HasColumnName("saldo")
                      .HasColumnType("decimal(18,2)")
                      .IsRequired();
                entity.Property(cc => cc.LimiteDeuda)
                      .HasColumnName("limite_deuda")
                      .HasColumnType("decimal(18,2)")
                      .IsRequired();
                entity.Property(cc => cc.LimiteDeudaActivo)
                      .HasColumnName("limite_deuda_activo")
                      .IsRequired();
                entity.Property(cc => cc.FechaVencimiento)
                      .HasColumnName("fecha_vencimiento")
                      .HasColumnType("date");
                entity.Property(cc => cc.EstaEliminado)
                      .HasColumnName("esta_eliminado")
                      .IsRequired();
                // Relación uno a uno con Cliente
                entity.HasOne(cc => cc.Cliente)
                      .WithOne(c => c.CuentaCorriente)
                      .HasForeignKey<Cliente>(c => c.CuentaCorrienteId)
                      .OnDelete(DeleteBehavior.Restrict);
                // Relación uno a muchos con MovimientoCuentaCorriente
                entity.HasMany(cc => cc.MovimientosCuentaCorriente)
                      .WithOne(mc => mc.CuentaCorriente)
                      .HasForeignKey(mc => mc.CuentaCorrienteId)
                      .OnDelete(DeleteBehavior.Restrict);
                // Relación uno a muchos con CuentaCorrienteAutorizado
                entity.HasMany(c => c.CuentaCorrienteAutorizado)
                      .WithOne() // sin navegación inversa
                      .HasForeignKey(a => a.CuentaCorrienteId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // CLIENTE
            modelBuilder.Entity<Cliente>(entity =>
            {
                entity.ToTable("Clientes");
                entity.HasKey(c => c.PersonaId);
                entity.Property(c => c.PersonaId)
                      .HasColumnName("PersonaId");
                entity.Property(c => c.CuentaCorrienteId)
                      .HasColumnName("CuentaCorrienteId");
                entity.Property(c => c.NumeroCliente)
                      .HasColumnName("numero_cliente")
                      .HasMaxLength(50)
                      .IsRequired();
                entity.Property(c => c.FechaAlta)
                      .HasColumnName("fecha_alta")
                      .HasColumnType("date")
                      .IsRequired();
                entity.Property(c => c.FechaBaja)
                      .HasColumnName("fecha_baja")
                      .HasColumnType("date");
                entity.Property(c => c.Estado)
                      .HasColumnName("estado")
                      .IsRequired();
                entity.Property(c => c.EstadoDescripcion)
                      .HasColumnName("estado_descripcion")
                      .HasMaxLength(200);
                // Relación 1 a 1 con Persona
                entity.HasOne(c => c.Persona)
                      .WithOne()
                      .HasForeignKey<Cliente>(c => c.PersonaId)
                      .OnDelete(DeleteBehavior.Restrict);
                // Relación uno a uno con CuentaCorriente
                entity.HasOne(c => c.CuentaCorriente)
                      .WithOne(cc => cc.Cliente)
                      .HasForeignKey<CuentaCorriente>(c => c.ClienteId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            // MOVIMIENTO CUENTA CORRIENTE
            modelBuilder.Entity<MovimientoCuentaCorriente>(entity =>
            {
                entity.ToTable("MovimientosCuentaCorrientes");
                entity.HasKey(mc => mc.MovimientoCuentaCorrienteId);
                entity.Property(mc => mc.MovimientoCuentaCorrienteId)
                      .HasColumnName("MovimientoCuentaCorrienteId")
                      .ValueGeneratedOnAdd();
                entity.Property(mc => mc.CuentaCorrienteId)
                      .HasColumnName("CuentaCorrienteId")
                      .IsRequired();
                entity.Property(mc => mc.TipoMovimientoCCorriente)
                      .HasColumnName("tipo_movimiento")
                      .IsRequired();
                entity.Property(mc => mc.Monto)
                      .HasColumnName("monto")
                      .HasColumnType("decimal(18,2)")
                      .IsRequired();
                entity.Property(mc => mc.Fecha)
                      .HasColumnName("fecha")
                      .HasColumnType("date")
                      .IsRequired();
                entity.Property(mc => mc.Descripcion)
                      .HasColumnName("descripcion")
                      .HasMaxLength(500);
                // Relación muchos a uno con CuentaCorriente
                entity.HasOne(mc => mc.CuentaCorriente)
                      .WithMany(cc => cc.MovimientosCuentaCorriente)
                      .HasForeignKey(mc => mc.CuentaCorrienteId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            // CUENTA CORRIENTE AUTORIZADO
            modelBuilder.Entity<CuentaCorrienteAutorizado>(entity =>
            {
                entity.ToTable("CuentaCorrienteAutorizados");
                entity.HasKey(cca => cca.CuentaCorrienteAutorizadoId);

                entity.Property(cca => cca.CuentaCorrienteAutorizadoId)
                      .HasColumnName("CuentaCorrienteAutorizadoId")
                      .ValueGeneratedOnAdd();
                entity.Property(cca => cca.CuentaCorrienteId)
                      .HasColumnName("CuentaCorrienteId")
                      .IsRequired();
                entity.Property(cca => cca.Dni)
                      .HasColumnName("dni")
                      .IsRequired();
            });
        }
    }
}
