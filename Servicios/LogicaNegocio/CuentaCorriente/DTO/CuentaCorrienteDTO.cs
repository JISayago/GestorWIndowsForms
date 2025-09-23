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
        public string NombreCuentaCorriente { get; set; }
        public decimal Saldo { get; set; } // puede ser negativo (ej: -500 = debe 500)
        public decimal LimiteDeuda { get; set; } // cuánto puede deber como máximo
        public bool EstaEliminado { get; set; }
        public bool LimiteDeudaActivo { get; set; } // Indica si el límite de deuda está activo
        public DateTime? FechaVencimiento { get; set; }
        public int EstadoCuentaCorriente { get; set; } //
        public List<long> ClienteIds { get; set; } // Lista de IDs de clientes asociados
        public List<long> DniAutorizados { get; set; } // Lista de DNI autorizados
        public List<long> MovimientoCuentaCorrienteIds { get; set; } // Lista de IDs de movimientos asociados
    }
}
