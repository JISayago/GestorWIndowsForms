using AccesoDatos;
using AccesoDatos.Entidades;
using Servicios.LogicaNegocio.Producto.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios.LogicaNegocio.Producto.Lote
{
    public class LoteServicio
    {

        // Implementar métodos para gestionar lotes de productos, como:
        // - Crear un nuevo lote para un producto específico.
        // - Obtener lotes por producto, incluyendo información sobre stock, fecha de vencimiento, etc.
        // - Actualizar el stock de un lote específico.
        // - Eliminar o marcar como inactivo un lote cuando ya no esté disponible.
        // - Controlar el vencimiento de los lotes y notificar cuando un lote esté próximo a vencer o ya haya vencido.
        // - Integrar la gestión de lotes con el sistema de ventas para asegurar que se descuenten los lotes correctos al realizar una venta.
        // - Implementar validaciones para asegurar que no se puedan crear lotes con fechas de vencimiento pasadas o con stock negativo.
        // - Integrar la gestión de lotes con el sistema de inventario para asegurar que el stock se actualice correctamente al recibir nuevos lotes o al realizar ventas.

        public void crearLote(LoteDTO loteACrear)
        {
            var context = new GestorContextDBFactory().CreateDbContext(null);
            
            var nuevoLote = new AccesoDatos.Entidades.Lote
            {
                IdProducto = loteACrear.IdProducto,
                StockIncial = loteACrear.StockInicial,
                StockActual = loteACrear.StockActual,
                NumeroLote = loteACrear.NumeroLote,
                NombreLote = loteACrear.NombreLote,
                Descripcion = loteACrear.Descripcion,
                FechaAlta = loteACrear.FechaAlta,
                FechaVencimiento = loteACrear.FechaVencimiento,
                EstaVencido = loteACrear.EstaVencido,
                EstaActivo = loteACrear.EstaActivo
            };

            context.Lotes.Add(nuevoLote);
        }

        public List<LoteDTO> obtenerLotesDeUnProducto(long productoId)
        {
           var context = new GestorContextDBFactory().CreateDbContext(null);

           var lotes = context.Lotes.Where(l => l.IdProducto == productoId).ToList();
            //validar que no sea null

            List<LoteDTO> listaLotes = lotes.Select(l => new LoteDTO
            {
                Id = l.LoteId,
                IdProducto = l.IdProducto,
                StockInicial = l.StockIncial,
                StockActual = l.StockActual,
                NumeroLote = l.NumeroLote,
                NombreLote = l.NombreLote,
                Descripcion = l.Descripcion,
                FechaAlta = l.FechaAlta,
                FechaVencimiento = l.FechaVencimiento,
                EstaVencido = l.EstaVencido,
                EstaActivo = l.EstaActivo
            }).ToList();

            return listaLotes;
        }
        public LoteDTO obtenerLoteFEFO(long productoId)
        {
            //traer con el productoId, el lote con la fecha de vencimiento más próxima pero que no esté vencido y que tenga stock disponible
            //aplicamos FEFO (First Expired, First Out) para asegurar que se utilicen primero los lotes que están más próximos a vencer.

            var context = new GestorContextDBFactory().CreateDbContext(null);

            var lote = context.Lotes 
                .Where(l => l.EstaActivo && !l.EstaVencido && l.StockActual > 0)
                .OrderBy(l => l.FechaVencimiento)
                .FirstOrDefault(l => l.IdProducto == productoId);

            //validar que no sea null

            return new LoteDTO
            {
                Id = lote.LoteId,
                IdProducto = lote.IdProducto,
                StockInicial = lote.StockIncial,
                StockActual = lote.StockActual,
                NumeroLote = lote.NumeroLote,
                NombreLote = lote.NombreLote,
                Descripcion = lote.Descripcion,
                FechaAlta = lote.FechaAlta,
                FechaVencimiento = lote.FechaVencimiento,
                EstaVencido = lote.EstaVencido,
                EstaActivo = lote.EstaActivo
            };
        }

        public void actualizarStockLote(int cantidad, long productoId)
        {
            var context = new GestorContextDBFactory().CreateDbContext(null);

            var loteFefo = obtenerLoteFEFO(productoId);

            while(cantidad > 0 && loteFefo != null)
            {
                if (loteFefo.StockActual >= cantidad)
                {
                    loteFefo.StockActual -= cantidad;
                    cantidad = 0;
                }
                else
                {
                    cantidad -= (int)loteFefo.StockActual;
                    loteFefo.StockActual = 0;
                }
                var loteEntity = context.Lotes.Find(loteFefo.Id);
                if (loteEntity != null)
                {
                    loteEntity.StockActual = loteFefo.StockActual;
                    context.SaveChanges();
                }
                if (cantidad > 0)
                {
                    loteFefo = obtenerLoteFEFO(productoId);
                }
            }
            //if (lote != null)
            //{
            //    lote.StockActual -= cantidad;

            //    var loteEntity = context.Lotes.Find(lote.Id);
            //    if (loteEntity != null)
            //    {
            //        loteEntity.StockActual = lote.StockActual;
            //        context.SaveChanges();
            //    }
            //}


            //traer el lote por fefo, si la cantidad de la compra supera el stock actual del lote,
            //descontar el stock del lote y traer el siguiente lote por fefo y asi sucesivamente
            //hasta que se complete la cantidad de la compra o no haya más lotes disponibles.
        }

        public void loteEstaActivo(long loteId, bool estaActivo)
        {
            var context = new GestorContextDBFactory().CreateDbContext(null);

            var lote = context.Lotes.Find(loteId);

            //validar que no sea null

            lote.EstaActivo = estaActivo;

            context.SaveChanges();
        }

        public void controlarVencimientosLotes()
        {
            //creo que no hace falta, capaz podria usarse para control de venciomientos en ventana principal
        }
        public void integrarGestionLotesConVentas()
        {
            //aca tambien deberia descontar el stock el stock que tiene producto tambien?

            //recibir la cantidad de la venta, el productoId y descontar el stock del lote por fefo
        }

    }
}
