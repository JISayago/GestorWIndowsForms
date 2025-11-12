using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AccesoDatos;
using Servicios.LogicaNegocio.Movimiento.DTO;
using Servicios.LogicaNegocio.Venta.DTO;


namespace Servicios.LogicaNegocio.Movimiento
{
    public class MovimientoService
    {
        /*
         * 
         * crearMovimiento(Venta venta)
         * obtenerMovimientoPorId
         * obtenerMovimientos
        */

        public void CrearMovimientoVenta(VentaDTO ventaDto) 
        {
            // Lógica para crear un movimiento basado en la venta proporcionada
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
            context.SaveChanges();
        }

        public MovimientoDTO ObtenerMovimientoPorId(long movimientoId)
        {
            // Lógica para obtener un movimiento por su ID
            var context = new GestorContextDBFactory().CreateDbContext(null);
            var movimiento = context.Movimientos.FirstOrDefault(m => m.MovimientoId == movimientoId && !m.EstaEliminado);
            // Retornar o procesar el movimiento según sea necesario
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

        public IEnumerable<MovimientoDTO> ObtenerMovimientos()
        {
            // Lógica para obtener todos los movimientos
            var context = new GestorContextDBFactory().CreateDbContext(null);
            var movimientos = context.Movimientos.Where(m => !m.EstaEliminado).ToList();
            // Retornar o procesar la lista de movimientos según sea necesario
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
        }
    }
}
