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
        public long ProductoId { get; set; }
        public int Cantidad { get; set; }
        public string NumeroLote { get; set; } //autogenerado por el sistema
        public string NombreLote { get; set; } //
        public string Descripcion { get; set; }
        public DateTime FechaAlta { get; set; }
        public DateTime? FechaVencimiento { get; set; }
        public bool EstaEliminado { get; set; } //no hace falta en el dto, solo se modifica cuando ya se creó el lote

        public Producto Producto { get; set; }
        public List<Movimiento> Movimientos { get; set; }

    }
}
