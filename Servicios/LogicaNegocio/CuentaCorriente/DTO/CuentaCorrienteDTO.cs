using Servicios.Helpers.Cliente;
using Servicios.Helpers.Cliente.CtaCte;
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
        public int EstadoCtaCte { get; set; } //
        public string EstadoDescripcionCtaCte =>
         EstadoCtaCte switch
         {
             (int)EstadoCuentaCorriente.Activa => "Activo",
             (int)EstadoCuentaCorriente.Suspendida=> "Vencido",
             (int)EstadoCuentaCorriente.Cerrada => "Cerrado",
             _ => "Desconocido"
         };

        public long ClienteId { get; set; } // Lista de ID de cliente asociado
        public string NombreCliente { get; set; } // Nombre del cliente asociado
        public List<long> DniAutorizados { get; set; } // Lista de DNI autorizados
    }
}
