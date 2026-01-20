using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatos.Entidades
{
    public class Caja
    {
        [Key]
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

        public bool EstaCerrada { get; set; } //default false

        public ICollection<Movimiento> Movimientos { get; set; } //collection o list
    }
}
