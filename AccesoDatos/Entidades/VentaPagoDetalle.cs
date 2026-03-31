using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatos.Entidades
{
    public class VentaPagoDetalle
    {//cambiar por la forma de entidadID y entidad GEnerica
        [Key]
        public long VentaPagoDetalleId { get; set; }

        public long? IdVenta { get; set; }
        public Venta? Venta { get; set; }
        public long? IdVentaLibre { get; set; }
        public VentaLibre? VentaLibre { get; set; }
        public long? IdGasto { get; set; }
        public Gasto? Gasto { get; set; }
        public long IdTipoPago { get; set; }
        public TipoPago TipoPago { get; set; }
        public decimal Monto { get; set; }

    }
}
