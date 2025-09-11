using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatos.Entidades
{
    public class CuentaCorriente
    {
        [Key]
        public long CuentaCorrienteId { get; set; }
        public string nombreCuentaCorriente { get; set; }
        public decimal Saldo { get; set; }
        public decimal LimiteCredito { get; set; }
        public bool EstaEliminado { get; set; }
        public ICollection<Cliente> Clientes { get; set; }

        //public ICollection<MovimientoCuentaCorriente> MovimientosCuentaCorriente { get; set; } // Relación uno a muchos con MovimientosCuentaCorriente
    }
}
