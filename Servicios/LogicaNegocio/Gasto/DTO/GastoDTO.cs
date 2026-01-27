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

        public DateTime FechaGasto { get; set; }
        public DateTime FechaRegistro { get; set; }

        public decimal MontoTotal { get; set; }
        public decimal MontoPagado { get; set; }

        public int EstadoGasto { get; set; }

        public string? Detalle { get; set; }
    }

}
