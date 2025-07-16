using AccesoDatos.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios.LogicaNegocio.Venta.DTO
{
    public  class VentaDTO
    {
        public long VentaId { get; set; }
        public long IdEmpleadoCaja { get; set; }
        public long IdEmpleadoVenta { get; set; }
        public string NumeroVenta { get; set; }
        public DateTime FechaVenta { get; set; }
        public decimal Total { get; set; }
        public int Estado { get; set; }
        public string Detalle { get; set; }

    }
}
