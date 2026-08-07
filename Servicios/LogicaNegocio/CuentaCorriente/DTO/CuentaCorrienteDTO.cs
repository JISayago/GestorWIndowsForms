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
        public DateTime? FechaCreacion { get; set; }
        public DateTime? FechaActivacion { get; set; }
        public int TipoVencimiento { get; set; }
        public int CantidadMesesVencimiento { get; set; } // Cantidad de meses para el vencimiento de la cuenta corriente
        public bool ConDeuda { get; set; } // Indica si la cuenta corriente tiene deuda pendiente
        public int EstadoCtaCte { get; set; } //

        public decimal MontoCargaSaldo { get; set; }
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
        public List<string> DniAutorizados { get; set; } // Lista de DNI autorizados

        // Datos de contacto del cliente (ya existían en Persona/Cliente, no se usaban en este panel)
        public string NumeroCliente { get; set; } // Identificador no sensible, pensado para mostrar en pantallas de movimiento
        public string TelefonoCliente { get; set; }
        public string EmailCliente { get; set; }

        // Tipo del movimiento padre (Ingreso/Egreso) que llevó a este detalle de CtaCte -> permite
        // distinguir "Carga de saldo" (Ingreso) de "Compra a cuenta" (Egreso) sin necesitar vincular
        // la venta puntual (eso sí requeriría una migración).
        public int TipoMovimientoPadre { get; set; }
        public string TipoMovimientoPadreDescripcion =>
            TipoMovimientoPadre == (int)Servicios.Helpers.Movimiento.TipoMovimiento.Ingreso
                ? "Carga de saldo"
                : "Compra a cuenta";

        // Últimos movimientos de esta misma cuenta (excluyendo el actual), para dar contexto
        // temporal sin necesitar vincular la venta puntual que originó este movimiento.
        public List<MovimientoResumenCtaCteDTO> HistorialReciente { get; set; } = new List<MovimientoResumenCtaCteDTO>();
    }

    // DTO liviano solo para el mini-historial: evita traer el MovimientoHelperDTO completo
    // (con Venta/Gasto anidados) para algo que solo necesita 4 campos por fila.
    public class MovimientoResumenCtaCteDTO
    {
        public string NumeroMovimiento { get; set; }
        public DateTime FechaMovimiento { get; set; }
        public decimal Monto { get; set; }
        public int TipoMovimiento { get; set; }

        public string TipoMovimientoDescripcion =>
            TipoMovimiento == (int)Servicios.Helpers.Movimiento.TipoMovimiento.Ingreso
                ? "Carga de saldo"
                : "Compra a cuenta";
    }
}
