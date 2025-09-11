using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios.LogicaNegocio.CuentaCorriente.DTO
{
    public class CuentaCorrienteDTO
    {
        public long CuentaCorrienteId { get; set; }
        public string nombreCuentaCorriente { get; set; }
        public decimal Saldo { get; set; }
        public decimal LimiteCredito { get; set; }
        public bool EstaEliminado { get; set; }
        public List<long> ClienteIds { get; set; } // Lista de IDs de clientes asociados

    }
}
