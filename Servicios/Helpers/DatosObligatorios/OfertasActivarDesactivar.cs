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
        .Include(o => o.Productos) // traemos productos por si hay que analizarlos
        .ToList();

    if (!candidatas.Any())
                return new List<string>();

    // Actualizar estado de cada oferta según corresponda (en memoria)
    foreach (var oferta in candidatas)
    {
        var dentroRango = oferta.FechaInicio <= now && (oferta.FechaFin == null || oferta.FechaFin >= now);
        oferta.EstaActiva = dentroRango;
    }

    // Asegurarnos que EF vea los cambios en memoria
    context.ChangeTracker.DetectChanges();

    // Cargar todas las ofertas con sus productos para detectar conflictos entre las activas
    var todasOfertas = context.OfertasDescuentos
        .Include(o => o.Productos) // Productos es la colección de ProductosEnOfertaDescuentos
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

    var mensajes = new List<string>();

    // Resolver cada conflicto: dejar activa una sola oferta por regla de prioridad
    foreach (var grupo in conflictosPorProducto)
    {
        var productoId = grupo.Key;
        var ofertasEnConflicto = grupo.Select(x => x.Oferta).Distinct().ToList();

        // Regla de desempate (ajustable):
        // 1) mayor PorcentajeDescuento
        // 2) fecha de inicio más temprana
        // 3) menor id (fallback)
        var ofertaElegida = ofertasEnConflicto
            .OrderByDescending(o => o.PorcentajeDescuento ?? 0m)
            .ThenBy(o => o.FechaInicio)
            .ThenBy(o => o.OfertaDescuentoId)
            .First();

        var otrasOfertas = ofertasEnConflicto.Where(o => o.OfertaDescuentoId != ofertaElegida.OfertaDescuentoId).ToList();

        // Desactivar las otras ofertas
        foreach (var o in otrasOfertas)
        {
            o.EstaActiva = false;
        }

        var ids = string.Join(", ", ofertasEnConflicto.Select(o => o.OfertaDescuentoId));
        mensajes.Add($"Producto {productoId} estaba en ofertas [{ids}]. Se activa la oferta {ofertaElegida.OfertaDescuentoId}; las demás se desactivaron.");
    }

    // Mostrar mensajes (o loguearlos)
    if (mensajes.Any())
    {
        foreach (var m in mensajes)
            Console.WriteLine(m);
    }

    // Guardar todos los cambios (tanto activaciones/desactivaciones normales como los ajustes por conflicto)
    context.SaveChanges();
            return mensajes;
}



    }
}
