using AccesoDatos;
using AccesoDatos.Entidades;
using Servicios.Helpers.Movimiento;
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
        public void CrearMovimientoVenta(
            long ventaId,
            decimal monto,
            int estado,
            TipoMovimientoDetalle detalleTipo,
            TipoEntidadMovimiento tipoEntidad,
            GestorContextDB context)
        {
            if (ventaId <= 0)
                throw new Exception("El movimiento no puede crearse porque la venta no tiene un ID válido.");

            try
            {
                // 🔥 1. Tipo de movimiento (Ingreso / Egreso)
                var esEgreso = estado == 99;

                var tipoMovimiento = esEgreso
                    ? TipoMovimiento.Egreso
                    : TipoMovimiento.Ingreso;

                // 🔥 2. Prefijo de operación
                var prefijoOperacion = esEgreso ? "CAN" : "MOV";

                // 🔥 3. Prefijo de entidad
                string prefijoEntidad;

                switch (tipoEntidad)
                {
                    case TipoEntidadMovimiento.Venta:
                        prefijoEntidad = "VENTA";
                        break;

                    case TipoEntidadMovimiento.VentaLibre:
                        prefijoEntidad = "VENTALIBRE";
                        break;

                    default:
                        throw new Exception("Tipo de entidad no válido para movimientos de venta.");
                }

                // 🔥 4. Número de movimiento
                var numeroMovimiento = $"{prefijoOperacion}-{prefijoEntidad}-{ventaId}-{DateTime.Now:yyyyMMddHHmmss}";

                // 🔥 5. Crear movimiento
                var movimiento = new AccesoDatos.Entidades.Movimiento
                {
                    NumeroMovimiento = numeroMovimiento,
                    EntidadId = ventaId,
                    TipoEntidad = (int)tipoEntidad,
                    TipoMovimiento = (int)tipoMovimiento,
                    TipoMovimientoDetalle = (int)detalleTipo,
                    Monto = Math.Abs(monto), // siempre positivo
                    FechaMovimiento = DateTime.Now,
                    EstaEliminado = false
                };

                context.Movimientos.Add(movimiento);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al crear movimiento de venta: {ex}");
                throw;
            }
        }

        public void CrearMovimientoCtaCte(decimal total, long cajaId, long cuentaCorrienteId, TipoMovimientoDetalle detalleTipo, GestorContextDB context = null)
        {
            //el return del id lo puse para caja pero al fnal no se usa, podria no devolver nada

            bool crearContextoLocal = (context == null);

            if (crearContextoLocal)
                context = new GestorContextDBFactory().CreateDbContext(null);

            try
            {
                var movimiento = new AccesoDatos.Entidades.Movimiento
                {
                    NumeroMovimiento = $"MOV{total}CTACTE",
                    TipoMovimiento = (int)TipoMovimiento.Ingreso,// tiene los dos ingeso y egreso?
                    TipoMovimientoDetalle = (int)detalleTipo,
                    Monto = total,
                    FechaMovimiento = DateTime.Now,
                    EstaEliminado = false,
                    EntidadId = cuentaCorrienteId,
                    TipoEntidad = (int)TipoEntidadMovimiento.CuentaCorriente
                };

                context.Movimientos.Add(movimiento);

                //Si el contexto es local, guardamos los cambios directamente
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
            using var context = new GestorContextDBFactory().CreateDbContext(null);

            var movimiento = context.Movimientos
                .FirstOrDefault(m => m.MovimientoId == movimientoId && !m.EstaEliminado);

            if (movimiento == null)
                return null;

            return new MovimientoDTO
            {
                MovimientoId = movimiento.MovimientoId,
                NumeroMovimiento = movimiento.NumeroMovimiento,
                EntidadId = movimiento.EntidadId,
                TipoEntidad = movimiento.TipoEntidad,
                TipoMovimiento = movimiento.TipoMovimiento,
                TipoMovimientoDetalle = movimiento.TipoMovimientoDetalle,
                Monto = movimiento.Monto,
                FechaMovimiento = movimiento.FechaMovimiento,
                EstaEliminado = movimiento.EstaEliminado
            };
        }
        public void CrearMovimientoGasto(
    long gastoId,
    decimal monto,
    TipoMovimientoDetalle detalleTipo,
    GestorContextDB context)
        {
            if (gastoId <= 0)
                throw new Exception("El movimiento no puede crearse porque el gasto no tiene un ID válido.");

            try
            {
                var numeroMovimiento = $"MOV-GASTO-{gastoId}-{DateTime.Now:yyyyMMddHHmmss}";

                var movimiento = new AccesoDatos.Entidades.Movimiento
                {
                    NumeroMovimiento = numeroMovimiento,
                    EntidadId = gastoId,
                    TipoEntidad = (int)TipoEntidadMovimiento.Gasto,
                    TipoMovimiento = (int)TipoMovimiento.Egreso, // 🔴 importante
                    TipoMovimientoDetalle = (int)detalleTipo,
                    Monto = monto,
                    FechaMovimiento = DateTime.Now,
                    EstaEliminado = false
                };

                context.Movimientos.Add(movimiento);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al crear movimiento de gasto: {ex}");
                throw;
            }
        }
        public IEnumerable<MovimientoDTO> ObtenerMovimiento(string cadenaBuscar)
        {
            using var context = new GestorContextDBFactory().CreateDbContext(null);

            return context.Movimientos
                .Where(x => !x.EstaEliminado && x.NumeroMovimiento.Contains(cadenaBuscar))
                .Select(x => new MovimientoDTO
                {
                    MovimientoId = x.MovimientoId,
                    NumeroMovimiento = x.NumeroMovimiento,
                    TipoMovimiento = x.TipoMovimiento,
                    TipoMovimientoDetalle = x.TipoMovimientoDetalle,
                    Monto = x.Monto,
                    FechaMovimiento = x.FechaMovimiento,
                    EstaEliminado = x.EstaEliminado,
                    EntidadId = x.EntidadId,
                    TipoEntidad = x.TipoEntidad
                })
                .ToList();
        }

        public IEnumerable<MovimientoDTO> ObtenerMovimientoEliminado(string cadenaBuscar)
        {
            using var context = new GestorContextDBFactory().CreateDbContext(null);

            return context.Movimientos
                .Where(x => x.EstaEliminado &&
                       (string.IsNullOrEmpty(cadenaBuscar) || x.NumeroMovimiento.Contains(cadenaBuscar)))
                .Select(x => new MovimientoDTO
                {
                    MovimientoId = x.MovimientoId,
                    NumeroMovimiento = x.NumeroMovimiento,
                    TipoMovimiento = x.TipoMovimiento,
                    TipoMovimientoDetalle = x.TipoMovimientoDetalle,
                    Monto = x.Monto,
                    FechaMovimiento = x.FechaMovimiento,
                    EstaEliminado = x.EstaEliminado,
                    EntidadId = x.EntidadId,
                    TipoEntidad = x.TipoEntidad
                })
                .ToList();
        }

        public (EmpleadoDTO empleado, VentaDTO venta, MovimientoDTO movimiento, List<ProductoDTO> productos) CargarDatosMovimiento(long movimientoId)
        {
            var movimientoService = new MovimientoServicio();
            var ventaService = new VentaServicio();
            var empleadoService = new EmpleadoServicio();
            var productoServicio = new ProductoServicio();

            var movimiento = movimientoService.ObtenerMovimientoPorId(movimientoId);

            VentaDTO venta = null;
            EmpleadoDTO empleado = null;
            List<ProductoDTO> productos = new List<ProductoDTO>();

            // verificar si el movimiento corresponde a una venta
            if (movimiento != null &&
                movimiento.TipoEntidad == (int)TipoEntidadMovimiento.Venta &&
                movimiento.EntidadId.HasValue)
            {
                venta = ventaService.ObtenerVentaDetalle(movimiento.EntidadId.Value);

                if (venta != null)
                {
                    empleado = empleadoService.ObtenerEmpleadoPorId(venta.IdEmpleado);

                    if (venta.Items != null && venta.Items.Any())
                    {
                        productos = venta.Items
                            .Select(item => productoServicio.ObtenerProductoPorId(item.ItemId))
                            .ToList();
                    }
                }
            }

            return (empleado: empleado, venta: venta, movimiento: movimiento, productos: productos);
        }
    }
}
