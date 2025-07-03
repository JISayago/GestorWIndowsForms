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
        public DbSet<EmpleadoRol> EmpleadoRoles { get; set; }
        public DbSet<VentaPagoDetalle> VentaPagosDetalles { get; set; }
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

                entity.Property(p => p.IdCategoria)
                    .HasColumnName("id_Categoria");

                entity.Property(p => p.IdMarca)
                    .HasColumnName("id_Marca");

                entity.Property(p => p.Stock)
                    .HasColumnName("stock")
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

                // 🔗 Relaciones con Marca y Categoría
                entity.HasOne(p => p.Categoria)
                    .WithMany()
                    .HasForeignKey(p => p.IdCategoria)
                    .OnDelete(DeleteBehavior.SetNull);

                entity.HasOne(p => p.Marca)
                    .WithMany()
                    .HasForeignKey(p => p.IdMarca)
                    .OnDelete(DeleteBehavior.SetNull);

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


            //VENTA
            modelBuilder.Entity<Venta>(entity =>
            {
                entity.ToTable("Ventas");

                entity.HasKey(e => e.VentaId);

                entity.Property(e => e.VentaId)
                    .HasColumnName("id_Venta");

                entity.Property(e => e.IdEmpleado)
                    .HasColumnName("id_Empleado")
                    .IsRequired();

                entity.Property(e => e.NumeroVenta)
                    .HasColumnName("numeroVenta")
                    .HasMaxLength(200);

                entity.Property(e => e.FechaVenta)
                    .HasColumnName("fecha_venta")
                    .HasColumnType("date")
                    .IsRequired();

                entity.Property(e => e.Total)
                    .HasColumnName("total")
                    .HasColumnType("decimal(18,2)")
                    .IsRequired();

                entity.Property(e => e.Estado)
                    .HasColumnName("estado")
                    .IsRequired();

                entity.Property(e => e.Detalle)
                    .HasColumnName("detalle")
                    .HasMaxLength(500);

                // 🔗 Relación con Empleado
                entity.HasOne(e => e.Empleado)
                    .WithMany(emp => emp.Ventas)
                    .HasForeignKey(e => e.IdEmpleado)
                    .OnDelete(DeleteBehavior.Restrict);

                // 🔗 Relación uno a muchos con DetallesVenta
                entity.HasMany(e => e.DetallesVentas)
                    .WithOne(dv => dv.Venta)
                    .HasForeignKey(dv => dv.IdVenta)
                    .OnDelete(DeleteBehavior.Cascade);

                // 🔗 Relación uno a muchos con VentasPagos (para múltiples métodos de pago)
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

                entity.HasKey(e => e.CategoriaId);
                entity.Property(e => e.Nombre)
                      .IsRequired()
                      .HasMaxLength(100);
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
        }


    }
}
