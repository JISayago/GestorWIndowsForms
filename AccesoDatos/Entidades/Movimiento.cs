using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatos.Entidades
{
    public class Movimiento
    {
        [Key]
        public long MovimientoId { get; set; }
        public string NumeroMovimiento { get; set; } = string.Empty;
        public long IdCaja { get; set; }
        public long? IdVenta { get; set; }     // Nullable si no siempre hay venta asociada
        // long? IdGasto { get; set; }     // Nullable si no siempre hay gasto asociado
        public int TipoMovimiento { get; set; }
        //public int Movimiento { get; set; }
        public decimal Monto { get; set; }
        public DateTime FechaMovimiento { get; set; }
        public bool EstaEliminado { get; set; }

        //public long CajaId { get; set; }  // Si se necesita relacionar con Caja, lo hacemos?

        // Navegación
        public Venta Venta { get; set; }
        public Caja Caja { get; set; } //estoy obligado a tener la referencia a caja? no puedo solo tener el id?

    }
}
