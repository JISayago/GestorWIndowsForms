using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatos.Entidades
{
    public class Lotes
    {
        public long LoteId { get; set; }
        public long ProductoId { get; set; }
        public string NumeroLote { get; set; } //autogenerado por el sistema
        public string NombreLote { get; set; } //
        public string Descripcion { get; set; }
        public DateTime fechaAlta { get; set; }
        public DateTime? fechaVencimiento { get; set; }
        public bool EstaEliminado { get; set; }

        public Producto Producto { get; set; }
        public List<Movimiento> Movimientos { get; set; }

    }
}
