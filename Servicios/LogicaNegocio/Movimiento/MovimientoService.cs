using AccesoDatos;
using Servicios.LogicaNegocio.Movimiento.DTO;
using Servicios.LogicaNegocio.Producto.Rubro.DTO;
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

        public void CrearMovimientoVenta(VentaDTO ventaDto) 
        {
            var context = new GestorContextDBFactory().CreateDbContext(null);
            var movimiento = new AccesoDatos.Entidades.Movimiento
            {
                NumeroMovimiento = "MOV" + ventaDto.NumeroVenta + "VENTA", // Ejemplo de generación de número de movimiento
                IdVenta = ventaDto.VentaId,
                TipoMovimiento = 1, // Ingreso
                Monto = ventaDto.Total,
                FechaMovimiento = DateTime.Now,
                EstaEliminado = false
            };

            context.Movimientos.Add(movimiento);
            try 
            {
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
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
    }
}
