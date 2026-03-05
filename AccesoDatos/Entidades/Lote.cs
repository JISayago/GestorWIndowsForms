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
        public int StockIncial { get; set; }
        public int StockActual { get; set; }
        public string NumeroLote { get; set; } //autogenerado por el sistema
        public string NombreLote { get; set; } //
        public string Descripcion { get; set; }
        public DateTime FechaAlta { get; set; }
        public DateTime? FechaVencimiento { get; set; }
        public bool EstaVencido { get; set; } //se calcula en base a la fecha de vencimiento, no se guarda en la base de datos
        public bool EstaActivo { get; set; } //se calcula en base al stock actual, no se guarda en la base de datos, creo que deberia ya que hay casos en los que no estara activo y tendra stock disponible

        public Producto Producto { get; set; }
        public List<Movimiento> Movimientos { get; set; }
    }
}
