using AccesoDatos;
using AccesoDatos.Entidades;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Servicios.Helpers.DatosObligatorios
{
    public static class OfertasActivarDesactivar
    {
        public static List<string> Inicializar(GestorContextDB context)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            var now = DateTime.Now;

            // =========================================================
            // DESACTIVAR OFERTAS VENCIDAS
            // =========================================================

            var ofertasVencidas = context.OfertasDescuentos
                .Where(o =>
                    o.EstaActiva &&
                    o.FechaFin.HasValue &&
                    o.FechaFin.Value < now)
                .ToList();

            foreach (var oferta in ofertasVencidas)
            {
                oferta.EstaActiva = false;

                // TODO:
                // Crear notificación:
                // "La oferta {oferta.Codigo} fue desactivada porque finalizó su vigencia."
            }

            // =========================================================
            // CARGAR OFERTAS ACTIVAS
            // =========================================================

            var ofertasActivas = context.OfertasDescuentos
                .Where(o => o.EstaActiva)
                .Include(o => o.Productos)
                .Include(o => o.Estadisticas)
                .ToList();

            // =========================================================
            // DESACTIVAR OFERTAS QUE ALCANZARON EL LÍMITE
            // =========================================================

            foreach (var oferta in ofertasActivas)
            {
                if (!OfertaAlcanzoLimite(oferta))
                    continue;

                oferta.EstaActiva = false;

                // TODO:
                // Crear notificación:
                // "La oferta {oferta.Codigo} fue desactivada porque alcanzó el límite de ventas."
            }

            // =========================================================
            // QUEDARSE SOLO CON LAS OFERTAS ACTIVAS
            // =========================================================

            var ofertasActivasConProductos = ofertasActivas
                .Where(o => o.EstaActiva && o.Productos.Any())
                .ToList();

            // =========================================================
            // DETECTAR CONFLICTOS
            // =========================================================

            var conflictosPorProducto = ofertasActivasConProductos
                .SelectMany(o => o.Productos.Select(p => new
                {
                    Oferta = o,
                    p.ProductoId
                }))
                .GroupBy(x => x.ProductoId)
                .Where(g => g
                    .Select(x => x.Oferta.OfertaDescuentoId)
                    .Distinct()
                    .Count() > 1)
                .ToList();

            if (!conflictosPorProducto.Any())
            {
                context.SaveChanges();
                return new List<string>();
            }

            // =========================================================
            // RESOLVER CONFLICTOS
            // =========================================================

            var ofertasElegidasCodigos = new HashSet<string>();
            var conjuntosProcesados = new HashSet<string>();

            foreach (var conflicto in conflictosPorProducto)
            {
                var ofertas = conflicto
                    .Select(x => x.Oferta)
                    .Distinct()
                    .ToList();

                var clave = string.Join("_",
                    ofertas
                        .Select(x => x.OfertaDescuentoId)
                        .OrderBy(x => x));

                if (!conjuntosProcesados.Add(clave))
                    continue;

                var ofertaGanadora = ofertas
                    .OrderByDescending(x => x.PorcentajeDescuento ?? 0m)
                    .ThenBy(x => x.FechaInicio)
                    .ThenBy(x => x.OfertaDescuentoId)
                    .First();

                foreach (var oferta in ofertas.Where(x =>
                             x.OfertaDescuentoId != ofertaGanadora.OfertaDescuentoId))
                {
                    oferta.EstaActiva = false;

                    // TODO:
                    // Crear notificación:
                    // "La oferta {oferta.Codigo} fue desactivada por conflicto con la oferta {ofertaGanadora.Codigo}."
                }

                if (!string.IsNullOrWhiteSpace(ofertaGanadora.Codigo))
                    ofertasElegidasCodigos.Add(ofertaGanadora.Codigo);
            }

            // =========================================================
            // GUARDAR CAMBIOS
            // =========================================================

            context.SaveChanges();

            // =========================================================
            // MENSAJES
            // =========================================================

            var mensajes = new List<string>();

            if (ofertasElegidasCodigos.Any())
            {
                var codigos = string.Join(", ", ofertasElegidasCodigos);

                mensajes.Add(
                    ofertasElegidasCodigos.Count == 1
                        ? $"Conflictos con productos en múltiples ofertas. Se activó la oferta {codigos}; las demás se desactivaron. Para mayor control usar la ventana de ofertas."
                        : $"Conflictos con productos en múltiples ofertas. Se activaron las ofertas {codigos}; las demás se desactivaron. Para mayor control usar la ventana de ofertas.");
            }

            return mensajes;
        }

        private static bool OfertaAlcanzoLimite(OfertaDescuento oferta)
        {
            if (!oferta.Productos.Any() || !oferta.Estadisticas.Any())
                return false;

            foreach (var producto in oferta.Productos)
            {
                var estadistica = oferta.Estadisticas
                    .FirstOrDefault(e => e.ProductoId == producto.ProductoId);

                if (estadistica == null)
                    continue;

                if (estadistica.CantidadVendida >= producto.LimiteVentaProducto.GetValueOrDefault())
                    return true;
            }

            return false;
        }
    }
}