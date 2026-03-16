using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatos.Entidades
{
    public class Lote
    {
        public long LoteId { get; set; }
        public long IdProducto { get; set; }
        public decimal StockIncial { get; set; }
        public decimal StockActual { get; set; }
        public string NumeroLote { get; set; } //autogenerado por el sistema
        public string NombreLote { get; set; } //autogenerado?
        public string Descripcion { get; set; }
        public DateOnly FechaAlta { get; set; }
        public DateOnly? FechaVencimiento { get; set; }
        public bool EstaVencido { get; set; }
        public bool EstaActivo { get; set; }

        public Producto Producto { get; set; }
        public List<Movimiento> Movimientos { get; set; }
    }
}
