using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios.LogicaNegocio.Producto.DTO
{
    public class MovilizacionStockDTO
    {
        public decimal Monto { get; set; }
        public int TipoMovimientoStock { get; set; }
        public long ProductoId { get; set; }
        public string Motivo { get; set; }
    }
}
