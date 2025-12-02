using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios.LogicaNegocio.Caja.DTO
{
    public class CajaDTO
    {
        public long CajaId { get; set; }

        public decimal SaldoInicial { get; set; }
        public decimal SaldoActual { get; set; }

        public DateTime FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }

        public decimal TotalIngresos { get; set; }
        public decimal TotalEgresos { get; set; }
        public decimal BalanceFinal { get; set; }

        public long EmpleadoApertura { get; set; }
        public long? EmpleadoCierre { get; set; }

        public bool EstaCerrada { get; set; }

        public ICollection<long> MovimientoIds { get; set; } // Hace falta tener disponibles los movimientos en el dto?
    }
}
