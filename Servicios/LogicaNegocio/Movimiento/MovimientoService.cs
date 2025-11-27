using AccesoDatos;
using AccesoDatos.Entidades;
using Servicios.Helpers;
using Servicios.LogicaNegocio.Articulo.Marca.DTO;
using Servicios.LogicaNegocio.Cliente.DTO;
using Servicios.LogicaNegocio.Empleado;
using Servicios.LogicaNegocio.Empleado.DTO;
using Servicios.LogicaNegocio.Movimiento.DTO;
using Servicios.LogicaNegocio.Producto;
using Servicios.LogicaNegocio.Producto.DTO;
using Servicios.LogicaNegocio.Producto.Rubro.DTO;
using Servicios.LogicaNegocio.Venta;
using Servicios.LogicaNegocio.Venta.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Servicios.LogicaNegocio.Movimiento
{
    public class MovimientoServicio : IMovimientoServicio
    {
        /*
         * 
         * crearMovimiento(Venta venta)
         * obtenerMovimientoPorId
         * obtenerMovimientos
        */

        /*
         * CREAR FUNCION PARA BUSCAR LOS NOMBRES DE LOS PRODUCTOS EN EL MOVIMIENTO DETALLADO,
         * USANOD LOS ID DE LOS PRODUCTOS QUE TENGO EN DETALLEvENTA BUSCAR LOS NOMBRE USANDO SERIVCE DE PRODDUCTO Y CREAR UNA LISTA DE PRODUCTOS CON NOMBRE
         * 
         */
        public void CrearMovimientoVenta(VentaDTO ventaDto, GestorContextDB context = null)
        {
            bool crearContextoLocal = (context == null);

            if (crearContextoLocal)
                context = new GestorContextDBFactory().CreateDbContext(null);

            try
            {
                var movimiento = new AccesoDatos.Entidades.Movimiento
                {
                    NumeroMovimiento = $"MOV{ventaDto.NumeroVenta}VENTA",
                    IdVenta = ventaDto.VentaId,
                    TipoMovimiento = 1, // 1 = Ingreso, por ejemplo
                    Monto = ventaDto.Total,
                    FechaMovimiento = DateTime.Now,
                    EstaEliminado = false
                };

                context.Movimientos.Add(movimiento);

                // Si el contexto es local, guardamos los cambios directamente
                if (crearContextoLocal)
                    context.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al crear movimiento: {ex.Message}");
                throw; // Re-lanzamos para que el servicio que lo llame lo maneje
            }
            finally
            {
                if (crearContextoLocal)
                    context.Dispose();
            }
        }
        /*
        public void CrearMovimientoVenta(VentaDTO ventaDto)
        {
            using var context = new GestorContextDBFactory().CreateDbContext(null);

            var nuevoMovimiento = new AccesoDatos.Entidades.Movimiento
            {
                NumeroMovimiento = "MOV" + ventaDto.NumeroVenta + "VENTA", // Ejemplo de generación de número de movimiento
                IdVenta = ventaDto.VentaId,
                TipoMovimiento = 1, // Ingreso
                Monto = ventaDto.Total,
                FechaMovimiento = DateTime.Now,
                EstaEliminado = false
            };

            context.Movimientos.Add(nuevoMovimiento);
            context.SaveChanges(); // Guarda en la DB
        }*/

        public MovimientoDTO ObtenerMovimientoPorId(long movimientoId)
        {
            var context = new GestorContextDBFactory().CreateDbContext(null);
            var movimiento = context.Movimientos.FirstOrDefault(m => m.MovimientoId == movimientoId && !m.EstaEliminado);

            return new MovimientoDTO
            {
                MovimientoId = movimiento.MovimientoId,
                NumeroMovimiento = movimiento.NumeroMovimiento,
                IdVenta = movimiento.IdVenta,
                TipoMovimiento = movimiento.TipoMovimiento,
                Monto = movimiento.Monto,
                FechaMovimiento = movimiento.FechaMovimiento,
                EstaEliminado = movimiento.EstaEliminado
            };
        }
        /*
        public IEnumerable<MovimientoDTO> ObtenerMovimientos()
        {
            var context = new GestorContextDBFactory().CreateDbContext(null);
            var movimientos = context.Movimientos.Where(m => !m.EstaEliminado).ToList();

            return movimientos.Select(m => new MovimientoDTO
            {
                MovimientoId = m.MovimientoId,
                NumeroMovimiento = m.NumeroMovimiento,
                IdVenta = m.IdVenta,
                TipoMovimiento = m.TipoMovimiento,
                Monto = m.Monto,
                FechaMovimiento = m.FechaMovimiento,
                EstaEliminado = m.EstaEliminado
            });
        }*/
        public IEnumerable<MovimientoDTO> ObtenerMovimiento(string cadenaBuscar)
        {
            using var context = new GestorContextDBFactory().CreateDbContext(null);

            return context.Movimientos
                .Where(x => !x.EstaEliminado && x.NumeroMovimiento.Contains(cadenaBuscar))
                .Select(x => new MovimientoDTO
                {
                    MovimientoId = x.MovimientoId,
                    FechaMovimiento = x.FechaMovimiento,
                    TipoMovimiento = x.TipoMovimiento,
                    Monto = x.Monto,
                    NumeroMovimiento = x.NumeroMovimiento,
                    IdVenta = x.IdVenta,
                    EstaEliminado = x.EstaEliminado
                })
                .ToList();
        }

        public IEnumerable<MovimientoDTO> ObtenerMovimientoEliminado(string cadenaBuscar)
        {
            using var context = new GestorContextDBFactory().CreateDbContext(null);

            return context.Movimientos
                .Where(x => x.EstaEliminado && x.NumeroMovimiento.Contains(cadenaBuscar))
                .Select(x => new MovimientoDTO
                {
                    MovimientoId = x.MovimientoId,
                    FechaMovimiento = x.FechaMovimiento,
                    TipoMovimiento = x.TipoMovimiento,
                    Monto = x.Monto,
                    NumeroMovimiento = x.NumeroMovimiento,
                    IdVenta = x.IdVenta,
                    EstaEliminado = x.EstaEliminado
                })
                .ToList();
        }

        public (EmpleadoDTO empleado, VentaDTO venta, MovimientoDTO movimiento, List<ProductoDTO> productos) CargarDatosMovimiento(long entidadId)
        {
            var context = new GestorContextDBFactory().CreateDbContext(null);

            var movimientoService = new MovimientoServicio();
            var ventaService = new VentaServicio();
            var empleadoService = new EmpleadoServicio();
            var productoServicio = new ProductoServicio();


            var movimiento = movimientoService.ObtenerMovimientoPorId(entidadId);

            var venta = movimiento != null ? 
                ventaService.ObtenerVentaDetalle(movimiento.IdVenta.Value) : null;

            var empleado = venta != null ? 
                empleadoService.ObtenerEmpleadoPorId(venta.IdEmpleado) : null;

            var productos = (venta != null && venta.Items != null)
                ? venta.Items.Select(item => productoServicio.ObtenerProductoPorId(item.ItemId)).ToList()
                : new List<ProductoDTO>();

            return (empleado: empleado, venta: venta, movimiento: movimiento, productos: productos);

        }
    }
}
