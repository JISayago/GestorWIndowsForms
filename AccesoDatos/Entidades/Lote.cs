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
        public string NumeroLote { get; set; } //autogenerado por el sistema y el usuario puede cambiarlo si lo desea, o dejarlo como esta. El formato del numero de lote puede ser: "LOTE-0001", "LOTE-0002"
        public string NombreLote { get; set; } //autogenerado por el sistema y el usuario puede cambiarlo si lo desea, o dejarlo como esta. El formato del nombre de lote puede ser: "Lote 1", "Lote
        public string Descripcion { get; set; }
        public DateOnly FechaAlta { get; set; }
        public DateOnly? FechaVencimiento { get; set; }
        public bool EstaVencido { get; set; }
        public bool EstaActivo { get; set; }

        public Producto Producto { get; set; }
        public List<Movimiento> Movimientos { get; set; }
    }
}
