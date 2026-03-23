using AccesoDatos;
using AccesoDatos.Entidades;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Servicios.Helpers;
using Servicios.LogicaNegocio.Articulo.Marca.DTO;
using Servicios.LogicaNegocio.Producto.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Servicios.LogicaNegocio.Producto.Lote
{
    public class LoteServicio : ILoteServicio
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
        
        public EstadoOperacion CrearLote(LoteDTO loteACrear)
        {
            var context = new GestorContextDBFactory().CreateDbContext(null);

            if (context.Lotes.Any(p => p.NumeroLote == loteACrear.NumeroLote))
                return new EstadoOperacion
                {
                    Exitoso = false,
                    Mensaje = "Ya existe una lote igual"
                };
            
            //el numero de lote y el nombre debeerian ser autogenerados, y con la posibiiidad de cambiar

            var nuevoLote = new AccesoDatos.Entidades.Lote
            {
                IdProducto = loteACrear.IdProducto,
                StockIncial = loteACrear.StockInicial,
                StockActual = loteACrear.StockActual,
                NombreLote = loteACrear.NombreLote,
                NumeroLote = loteACrear.NumeroLote,
                Descripcion = loteACrear.Descripcion,
                FechaAlta = loteACrear.FechaAlta,
                FechaVencimiento = loteACrear.FechaVencimiento,
                EstaVencido = loteACrear.EstaVencido,
                EstaActivo = loteACrear.EstaActivo
            };
            
            context.Lotes.Add(nuevoLote);

            ActualizarStockEnProductoConLotes(loteACrear.IdProducto, context, nuevoLote.StockActual);

            context.SaveChanges();

            return new EstadoOperacion
            {
                Exitoso = true,
                Mensaje = "Lote creado correctamente.",
                EntidadId = nuevoLote.LoteId
            };
        }
        
        public void ModficiarLote(long loteId) 
        {

        }

        public void EliminarLote(long loteId)
        {
            //eliminado logico
        }
        
        public LoteDTO ObtenerLotePorId(long loteId)
        {
            var context = new GestorContextDBFactory().CreateDbContext(null);

            var lote = context.Lotes.FirstOrDefault(l => l.LoteId == loteId);

            //TODO: validar que no sea null
            var loteDTO = new LoteDTO
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
            return loteDTO;
        }

        public List<LoteDTO> ObtenerLotesDeUnProducto(long productoId)
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
        
        public void DescontarStockLoteFifoLifo(decimal cantidadADescontar, long productoId, bool tieneFechaVencimiento)
        {
            var context = new GestorContextDBFactory().CreateDbContext(null);

            var lote = ObtenerLoteFefoLifo(productoId, tieneFechaVencimiento, context);

            //TODO: verificar si el stock total de los lotes activos es suficiente para cubrir la cantiadad
            while (cantidadADescontar > 0 && lote != null)
            {
                if (lote.StockActual >= cantidadADescontar)
                {
                    //stock suficiente en el lote actual para cubrir la cantidad requerida
                    lote.StockActual -= cantidadADescontar;
                    cantidadADescontar = 0;
                }
                else
                {
                    //stock insuficiente en el lote actual, se consume todo el stock del lote y se busca el siguiente lote por fefo
                    cantidadADescontar -= lote.StockActual;
                    lote.StockActual = 0;
                }

                if (cantidadADescontar > 0)
                {
                    ///////TODO: validar que no traiga el mismo lote1!??!!?!?!?!?!///////////////////////////////
                    lote = ObtenerLoteFefoLifo(productoId, tieneFechaVencimiento, context);
                }
            }

            context.SaveChanges();
        }

        public void RestaurarStockLoteFifoLifo(decimal cantidadARestaurar, List<long> loteId, bool tieneFechaVencimiento)
        {
            //TO DO
            //agregar a ventdaDetalle, DetalleVentaLoteId para guardar el lote del que se descontó el stock, asi en caso de cancelaciones o devoluciones.

            //recibir la cantidad a restaurar, el productoId y el loteId del que se descontó el stock, para poder restaurar el stock del lote correcto.

            var context = new GestorContextDBFactory().CreateDbContext(null);

            foreach (var id in loteId)
            {
                var lote = context.Lotes.FirstOrDefault(l => l.LoteId == id);
                if (lote != null)
                {
                    lote.StockActual += cantidadARestaurar;
                }
            }
            
            context.SaveChanges();
        }

        public void ControlarVencimientosLotes()
        {
            //se podria usar al inciar el sistema para controlar los vencimientos y actualizar ventana principal o enviar notificaciones,
            //ademas de actualizar el estado de los lotes vencidos.
        }

        public void ActualizarStockEnProductoConLotes(long productoId, GestorContextDB context, decimal? stockLoteParaAgregar = null)
        {
            var producto = context.Productos.FirstOrDefault(p => p.ProductoId == productoId);

            producto.Stock = context.Lotes.Where(l => l.IdProducto == productoId && l.EstaActivo).Sum(l => l.StockActual);

            if(stockLoteParaAgregar.HasValue) //si estamos creando un nuevo lote
            {
                producto.Stock += stockLoteParaAgregar.Value;
            }

            context.SaveChanges();
        }
        public string GenerarNumeroLote()
        {
            var context = new GestorContextDBFactory().CreateDbContext(null);
            string numeroLoteGenerado;
            // Generar un número de lote único utilizando un prefijo y un contador incremental
            return numeroLoteGenerado = $"LOTE-{context.Lotes.Count() + 1:0000}";
        }

        public AccesoDatos.Entidades.Lote ObtenerLoteFefoLifo(long productoId, bool tieneFechaVencimiento, GestorContextDB context)
        {
            if (tieneFechaVencimiento)
            {
                // FEFO (First Expired, First Out) para asegurar que se utilicen primero los lotes que están más próximos a vencer. 

                //mejorar llamada a db para que no traiga todos los lotes a memoria y luego ordene, sino que ordene y filtre en la consulta a la db
                var loteFefo = context.Lotes
                .Where(l => l.EstaActivo && !l.EstaVencido && l.StockActual > 0)
                .OrderBy(l => l.FechaVencimiento)
                .FirstOrDefault(l => l.IdProducto == productoId);

                //TODO: validar que no sea null
                return loteFefo;
            }
            else
            {
                // LIFO (Last In, First Out - Último en Entrar, Primero en Salir)

                //mejorar llamada a db para que no traiga todos los lotes a memoria y luego ordene, sino que ordene y filtre en la consulta a la db
                var loteLifo = context.Lotes
                .Where(l => l.EstaActivo && l.FechaVencimiento == null && l.StockActual > 0)
                .OrderBy(l => l.FechaAlta)
                .FirstOrDefault(l => l.IdProducto == productoId);

                //TODO: validar que no sea null

                return loteLifo;
            }
        }

    }
}
