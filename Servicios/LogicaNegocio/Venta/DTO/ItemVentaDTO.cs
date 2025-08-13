using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios.LogicaNegocio.Venta.DTO
{
    public class ItemVentaDTO
    {
        public long ItemId { get; set; }
        public decimal Cantidad { get; set; }
        public decimal PrecioVenta { get; set; }
        public string Descripcion { get; set; }
        public string Medida { get; set; }
        public string UnidadMedida { get; set; }
        public bool EsOferta { get; set; }

    }
}
