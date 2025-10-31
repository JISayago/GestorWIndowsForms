using AccesoDatos;
using AccesoDatos.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios.Helpers.DatosObligatorios
{
    public static class OfertasActivarDesactivar
    {
        public static void Inicializar(GestorContextDB context)
        {
            if (context == null) throw new ArgumentNullException(nameof(context));

            // Obtener la fecha actual
            var now = DateTime.Now;

            // Buscar solo las ofertas que podrían cambiar de estado
            var candidatas = context.OfertasDescuentos
                .Where(o =>
                    (!o.EstaActiva && o.FechaInicio <= now && (o.FechaFin == null || o.FechaFin >= now)) ||
                    (o.EstaActiva && o.FechaFin != null && o.FechaFin < now)
                )
                .ToList();

            // Si no hay candidatas, no hacer nada
            if (!candidatas.Any())
                return;

            // Actualizar estado de cada oferta según corresponda
            foreach (var oferta in candidatas)
            {
                var dentroRango = oferta.FechaInicio <= now && (oferta.FechaFin == null || oferta.FechaFin >= now);
                oferta.EstaActiva = dentroRango;
            }

            // Guardar los cambios realizados
            context.SaveChanges();
        }



    }
}
