using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios.LogicaNegocio.Producto.DTO
{
    public class LoteDTO
    {
        public long Id { get; set; }
        public long IdProducto { get; set; }
        public decimal StockInicial { get; set; }
        public decimal StockActual { get; set; }
        public string NumeroLote { get; set; }
        public string NombreLote { get; set; }
        public string Descripcion { get; set; }
        public DateOnly FechaAlta { get; set; }
        public DateOnly? FechaVencimiento { get; set; }
        public bool EstaVencido { get; set; }
        public bool EstaActivo { get; set; }
    }
}
