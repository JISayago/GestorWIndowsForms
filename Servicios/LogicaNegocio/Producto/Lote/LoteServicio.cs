using AccesoDatos;
using AccesoDatos.Entidades;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Servicios.LogicaNegocio.Producto.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Servicios.LogicaNegocio.Producto.Lote
{
    public class LoteServicio
    {

        // Implementar métodos para gestionar lotes de productos, como:
        // - Crear un nuevo lote para un producto específico. DONE
        // - Obtener lotes por producto, incluyendo información sobre stock, fecha de vencimiento, etc. DONE
        // - Actualizar el stock de un lote específico.
        // - Eliminar o marcar como inactivo un lote cuando ya no esté disponible. DONE
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
        public AccesoDatos.Entidades.Lote obtenerLoteFEFO(long productoId, GestorContextDB context)
        {
            //traer con el productoId, el lote con la fecha de vencimiento más próxima pero que no esté vencido y que tenga stock disponible
            //aplicamos FEFO (First Expired, First Out) para asegurar que se utilicen primero los lotes que están más próximos a vencer. 

            var loteFefo = context.Lotes
            .Where(l => l.EstaActivo && !l.EstaVencido && l.StockActual > 0)
            .OrderBy(l => l.FechaVencimiento)
            .FirstOrDefault(l => l.IdProducto == productoId);

            //validar que no sea null

            return loteFefo;
        }

        public void actualizarStockLote(decimal cantidadADescontar, long productoId)
        {
            var context = new GestorContextDBFactory().CreateDbContext(null);

            //TODO: verificar si el stock total de los lotes activos es suficiente para cubrir la cantiadad

            var loteFefo = obtenerLoteFEFO(productoId, context);

            while (cantidadADescontar > 0 && loteFefo != null)
            {
                if (loteFefo.StockActual >= cantidadADescontar)
                {
                    //stock suficiente en el lote actual para cubrir la cantidad requerida
                    loteFefo.StockActual -= cantidadADescontar;
                    cantidadADescontar = 0;
                }
                else
                {
                    //stock insuficiente en el lote actual, se consume todo el stock del lote y se busca el siguiente lote por fefo
                    cantidadADescontar -= loteFefo.StockActual;
                    loteFefo.StockActual = 0;
                }
                
                if (cantidadADescontar > 0)
                {
                    loteFefo = obtenerLoteFEFO(productoId, context);
                }
            }
            
            context.SaveChanges();

            //traer el lote por fefo, si la cantidad de la compra supera el stock actual del lote,
            //descontar el stock del lote y traer el siguiente lote por fefo y asi sucesivamente
            //hasta que se complete la cantidad de la compra o no haya mas lotes disponibles.
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
