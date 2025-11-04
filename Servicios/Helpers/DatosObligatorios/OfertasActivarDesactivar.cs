using AccesoDatos;
using AccesoDatos.Entidades;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios.Helpers.DatosObligatorios
{
    public static class OfertasActivarDesactivar
    {
        public static List<string> Inicializar(GestorContextDB context)
        {
            if (context == null) throw new ArgumentNullException(nameof(context));

            var now = DateTime.Now;

            // Buscar solo las ofertas que podrían cambiar de estado
            var candidatas = context.OfertasDescuentos
                .Where(o =>
                    (!o.EstaActiva && o.FechaInicio <= now && (o.FechaFin == null || o.FechaFin >= now)) ||
                    (o.EstaActiva && o.FechaFin != null && o.FechaFin < now)
                )
                .Include(o => o.Productos)
                .ToList();

            if (!candidatas.Any())
                return new List<string>();

            // Actualizar estado de cada oferta según corresponda (en memoria)
            foreach (var oferta in candidatas)
            {
                var dentroRango = oferta.FechaInicio <= now && (oferta.FechaFin == null || oferta.FechaFin >= now);
                oferta.EstaActiva = dentroRango;
            }

            context.ChangeTracker.DetectChanges();

            // Cargar todas las ofertas con sus productos para detectar conflictos entre las activas
            var todasOfertas = context.OfertasDescuentos
                .Include(o => o.Productos)
                .ToList();

            // Filtrar ofertas activas que tengan productos
            var ofertasActivasConProductos = todasOfertas
                .Where(o => o.EstaActiva && o.Productos != null && o.Productos.Any())
                .ToList();

            // Crear pares (productoId, oferta)
            var paresProductoOferta = ofertasActivasConProductos
                .SelectMany(o => o.Productos.Select(p => new { Oferta = o, ProductoId = p.ProductoId }))
                .ToList();

            // Agrupar por producto y quedarnos con aquellos que aparecen en >1 oferta activa
            var conflictosPorProducto = paresProductoOferta
                .GroupBy(x => x.ProductoId)
                .Where(g => g.Select(x => x.Oferta.OfertaDescuentoId).Distinct().Count() > 1)
                .ToList();

            // Si no hay conflictos, guardar cambios y salir
            if (!conflictosPorProducto.Any())
            {
                context.SaveChanges();
                return new List<string>();
            }

            // Mantener el conjunto de ofertas elegidas (puede ser más de una si hay conflictos distintos)
            var ofertasElegidasIds = new HashSet<long>();
            var ofertasElegidasCodigos = new HashSet<string>();

            // Para evitar procesar el mismo conjunto de ofertas varias veces, usamos una clave por conjunto (opcional)
            var procesadosConjuntos = new HashSet<string>();

            foreach (var grupo in conflictosPorProducto)
            {
                var ofertasEnConflicto = grupo.Select(x => x.Oferta).Distinct().ToList();

                // clave única del conjunto de ofertas (ordenada)
                var key = string.Join("_", ofertasEnConflicto.Select(o => o.OfertaDescuentoId).OrderBy(id => id));
                if (procesadosConjuntos.Contains(key))
                    continue; // ya procesamos este conjunto de ofertas en otro producto

                procesadosConjuntos.Add(key);

                // Regla de desempate:
                // 1) mayor PorcentajeDescuento
                // 2) fecha de inicio más temprana
                // 3) menor id (fallback)
                var ofertaElegida = ofertasEnConflicto
                    .OrderByDescending(o => o.PorcentajeDescuento ?? 0m)
                    .ThenBy(o => o.FechaInicio)
                    .ThenBy(o => o.OfertaDescuentoId)
                    .First();

                // Desactivar las otras ofertas
                foreach (var o in ofertasEnConflicto.Where(o => o.OfertaDescuentoId != ofertaElegida.OfertaDescuentoId))
                {
                    o.EstaActiva = false;
                }

                ofertasElegidasIds.Add(ofertaElegida.OfertaDescuentoId);
                if (!string.IsNullOrEmpty(ofertaElegida.Codigo))
                    ofertasElegidasCodigos.Add(ofertaElegida.Codigo);
            }

            // Guardar los cambios realizados
            context.SaveChanges();

            // Construir un único mensaje de advertencia si hubo conflictos
            var mensajes = new List<string>();
            if (ofertasElegidasCodigos.Any())
            {
                var codigos = string.Join(", ", ofertasElegidasCodigos);
                if (ofertasElegidasCodigos.Count == 1)
                {
                    mensajes.Add($"Conflictos con productos en múltiples ofertas. Se activa la oferta {codigos}; las demás se desactivaron. Para mayor control usar ventana de ofertas.");
                }
                else
                {
                    mensajes.Add($"Conflictos con productos en múltiples ofertas. Se activaron las ofertas {codigos}; las demás se desactivaron. Para mayor control usar ventana de ofertas.");
                }
            }
            else
            {
                // Si no hay códigos (caso raro), devolvemos un mensaje genérico
                mensajes.Add("Conflictos con productos en múltiples ofertas. Se ajustaron las activaciones/desactivaciones. Para mayor control usar ventana de ofertas.");
            }

            // Devolver solo UN mensaje (o ninguno si no hubo conflictos)
            return mensajes;
        }



    }
}
