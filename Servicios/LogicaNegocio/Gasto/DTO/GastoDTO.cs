using Servicios.Helpers.Gasto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios.LogicaNegocio.Gasto.DTO
{
    public class GastoDTO
    {
        public long GastoId { get; set; }

        public string NumeroGasto { get; set; }

        public long IdEmpleado { get; set; }
        public string? NombreEmpleado { get; set; } // solo texto, sin entidad

        public int CategoriaGasto { get; set; }
        public string CategoriaGastoDescripcion
        {
            get
            {
                return ((CategoriaGasto)CategoriaGasto) switch
                {
                    Helpers.Gasto.CategoriaGasto.Servicios => "Servicios",
                    Helpers.Gasto.CategoriaGasto.Alquiler => "Alquiler",
                    Helpers.Gasto.CategoriaGasto.Sueldos => "Sueldos",
                    Helpers.Gasto.CategoriaGasto.Impuestos => "Impuestos",
                    Helpers.Gasto.CategoriaGasto.Insumos => "Insumos",
                    Helpers.Gasto.CategoriaGasto.Mantenimiento => "Mantenimiento",
                    Helpers.Gasto.CategoriaGasto.Otros => "Otros",
                    _ => "Desconocido"
                };
            }
        }
        public DateTime? FechaGasto { get; set; }
        public string FechaGastoDescripcion
        {
            get
            {
                return FechaGasto.HasValue
                    ? FechaGasto.Value.ToString("dd/MM/yyyy")
                    : "Pendiente";
            }
        }
        public DateTime FechaRegistro { get; set; }

        public decimal MontoTotal { get; set; }
        public decimal MontoPagado { get; set; }

        public int EstadoGasto { get; set; }
        public string EstadoGastoDescripcion
        {
            get
            {
                return ((Helpers.Gasto.EstadoGasto)EstadoGasto) switch
                {
                    Helpers.Gasto.EstadoGasto.Pendiente => "Pendiente",
                    Helpers.Gasto.EstadoGasto.Pagado => "Pagado",
                    Helpers.Gasto.EstadoGasto.Anulado => "Anulado",
                    _ => "Desconocido"
                };
            }
        }

        public string? Detalle { get; set; }

        /// <summary>OrigenPagoGasto (Caja=1, Otro=2). Se informa al pagar; null mientras está pendiente.</summary>
        public int? OrigenPago { get; set; }

        /// <summary>TipoDePago con el que se paga (solo se usa con origen Otro; con origen Caja es siempre Efectivo).</summary>
        public int? TipoPago { get; set; }

        /// <summary>Texto libre opcional del pago (por ejemplo, el banco).</summary>
        public string? ExtraDescripcionPago { get; set; }
    }

}
