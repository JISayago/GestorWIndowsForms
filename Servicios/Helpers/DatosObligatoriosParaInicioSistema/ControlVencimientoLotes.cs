using AccesoDatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios.Helpers.DatosObligatoriosParaInicioSistema
{
    public class ControlVencimientoLotes
    {
        public static void Inicializar(GestorContextDB context)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));
            var now = DateTime.Now;
            var lotesVencidos = context.Lotes
                .Where(l => l.EstaActivo && l.FechaVencimiento.HasValue && l.FechaVencimiento.Value < now)
                .ToList();
            foreach (var lote in lotesVencidos)
            {
                lote.EstaActivo = false;
                lote.EstaVencido = true;
            }
            context.SaveChanges();
        }
    }
}
