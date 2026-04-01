using AccesoDatos;
using AccesoDatos.Entidades;
using Microsoft.EntityFrameworkCore;
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
                NumeroLote = loteACrear.NumeroLote,
                Descripcion = loteACrear.Descripcion,
                FechaAlta = loteACrear.FechaAlta,
                FechaVencimiento = loteACrear.FechaVencimiento,
                EstaVencido = loteACrear.EstaVencido,
                EstaActivo = loteACrear.EstaActivo,
                EstaEliminado = false
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

        public IEnumerable<LoteDTO> ObtenerLote(string cadenaBuscar)
        {
            using var context = new GestorContextDBFactory().CreateDbContext(null);

            return context.Lotes
                .Where(x => x.NumeroLote.Contains(cadenaBuscar) && !x.EstaEliminado)
                .Select(x => new LoteDTO
                {
                    Id = x.LoteId,
                    IdProducto = x.IdProducto,
                    StockInicial = x.StockIncial,
                    StockActual = x.StockActual,
                    NumeroLote = x.NumeroLote,
                    Descripcion = x.Descripcion,
                    FechaAlta = x.FechaAlta,
                    FechaVencimiento = x.FechaVencimiento,
                    EstaVencido = x.EstaVencido,
                    EstaActivo = x.EstaActivo,
                    NombreProducto = x.Producto.Descripcion
                })
                .ToList();
        }

        public EstadoOperacion ModficiarLote(LoteDTO loteDto, long loteId) 
        {
            var context = new GestorContextDBFactory().CreateDbContext(null);
            var loteEditar = context.Lotes
                    .Include(x => x.Producto)
                    .FirstOrDefault(x => x.LoteId == loteId);

            if (loteEditar == null)
            {
                return new EstadoOperacion
                {
                    Exitoso = false,
                    Mensaje = "Lote no encontrado."
                };
            }

            loteEditar.NumeroLote = loteDto.NumeroLote;
            loteEditar.StockIncial = loteDto.StockInicial;
            loteEditar.StockActual = loteDto.StockActual;
            loteEditar.Descripcion = loteDto.Descripcion;
            loteEditar.FechaVencimiento = loteDto.FechaVencimiento;
            loteEditar.EstaVencido = loteDto.EstaVencido;
            loteEditar.EstaActivo = loteDto.EstaActivo;

            context.SaveChanges();
            return new EstadoOperacion
            {
                Exitoso = true,
                Mensaje = $"El lote {loteEditar.NumeroLote} de {loteEditar.Producto.Descripcion} fue modificado correctamente."
            };

        }

        public EstadoOperacion EliminarLote(long loteId)
        {
            var context = new GestorContextDBFactory().CreateDbContext(null);
            var loteEliminar = context.Lotes
                    .Include(x => x.Producto)
                    .FirstOrDefault(x => x.LoteId == loteId);

            if (loteEliminar == null)
            {
                return new EstadoOperacion
                {
                    Exitoso = false,
                    Mensaje = "Lote no encontrado."
                };
            }

            loteEliminar.EstaEliminado = true;

            context.SaveChanges();
            return new EstadoOperacion
            {
                Exitoso = true,
                Mensaje = $"El lote {loteEliminar.NumeroLote} de {loteEliminar.Producto.Descripcion} fue eliminado correctamente."
            };
        }

        public IEnumerable<LoteDTO> ObtenerLotesEliminados(string cadenabuscar, string columna)
        {
            using var context = new GestorContextDBFactory().CreateDbContext(null);

            var query = context.Lotes
                .Include(e => e.Producto)
                .AsNoTracking()
                .Where(e => e.EstaEliminado);

            /*if (!string.IsNullOrWhiteSpace(cadenabuscar))
            {
                switch (columna)
                {
                    case "MarcaNombre":
                        query = query.Where(e => e.Marca.Nombre.Contains(cadenabuscar));
                        break;

                    case "RubroNombre":
                        query = query.Where(e => e.Rubro.Nombre.Contains(cadenabuscar));
                        break;

                    case "Codigo":
                        query = query.Where(e => e.Codigo.Contains(cadenabuscar));
                        break;

                    case "CodigoBarra":
                        query = query.Where(e => e.CodigoBarra.Contains(cadenabuscar));
                        break;

                    default:
                        query = query.Where(e => e.Descripcion.Contains(cadenabuscar));
                        break;
                }
            }*/

            return query
                .ToList()
                .Select(e => new LoteDTO
                {
                    Id = e.LoteId,
                    IdProducto = e.IdProducto,
                    StockInicial = e.StockIncial,
                    StockActual = e.StockActual,
                    NumeroLote = e.NumeroLote,
                    Descripcion = e.Descripcion,
                    FechaAlta = e.FechaAlta,
                    FechaVencimiento = e.FechaVencimiento,
                    EstaVencido = e.EstaVencido,
                    EstaActivo = e.EstaActivo,
                    NombreProducto = e.Producto.Descripcion
                })
                .ToList();
        }

        public LoteDTO ObtenerLotePorId(long loteId)
        {
            var context = new GestorContextDBFactory().CreateDbContext(null);

            var lote = context.Lotes.Include(l => l.Producto)
                .FirstOrDefault(l => l.LoteId == loteId);

            //TODO: validar que no sea null
            var loteDTO = new LoteDTO
            {
                Id = lote.LoteId,
                IdProducto = lote.IdProducto,
                StockInicial = lote.StockIncial,
                StockActual = lote.StockActual,
                NumeroLote = lote.NumeroLote,
                Descripcion = lote.Descripcion,
                FechaAlta = lote.FechaAlta,
                FechaVencimiento = lote.FechaVencimiento,
                EstaVencido = lote.EstaVencido,
                EstaActivo = lote.EstaActivo,
                NombreProducto = lote.Producto.Descripcion
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

                context.SaveChanges(); //guardar cambios después de actualizar cada lote

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

                //TODO: mejorar llamada a db para que no traiga todos los lotes a memoria y luego ordene, sino que ordene y filtre en la consulta a la db
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

                //TODO: mejorar llamada a db para que no traiga todos los lotes a memoria y luego ordene, sino que ordene y filtre en la consulta a la db
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
