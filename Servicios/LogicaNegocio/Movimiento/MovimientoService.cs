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
        public void CrearMovimientoVenta(VentaDTO ventaDto, long cajaId, GestorContextDB context = null)
        {
            //el return del id lo puse para caja pero al fnal no se usa, podria no devolver nada

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
                    EstaEliminado = false,
                    IdCaja = cajaId
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
            var movimientoService = new MovimientoServicio();
            var ventaService = new CajaServicio();
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

            //arreglar funcion asi no hace mill llamadas a db por cada producto

            return (empleado: empleado, venta: venta, movimiento: movimiento, productos: productos);

            //crear movimientoDto para no mandadr tantos dtos al pedo
        }
    }
}
