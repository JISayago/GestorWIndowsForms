using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatos.Entidades
{
    public class Gasto
    {
        [Key]
        public long GastoId { get; set; }

        public string NumeroGasto { get; set; }

        public long IdEmpleado { get; set; }
        public Empleado Empleado { get; set; }

        public int CategoriaGasto { get; set; } // Corregido: propiedad de tipo enum

        public DateTime FechaGasto { get; set; }
        public DateTime FechaRegistro { get; set; }

        public decimal MontoTotal { get; set; }
        public decimal MontoPagado { get; set; }

        public int EstadoGasto { get; set; } //enum GastoEstado

        public string Detalle { get; set; }

        public ICollection<VentaPagoDetalle> VentaPagoDetalles { get; set; }
    }

}
