using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatos.Entidades
{
    public class Movimiento
    {
        [Key]
        public long MovimientoId { get; set; }

        public string NumeroMovimiento { get; set; } = string.Empty;

        public int TipoMovimiento { get; set; }

        public int TipoMovimientoDetalle { get; set; }

        public decimal Monto { get; set; }

        public DateTime FechaMovimiento { get; set; }

        public bool EstaEliminado { get; set; }

        // referencia genérica a cualquier entidad del sistema
        public int? TipoEntidad { get; set; }
        public long? EntidadId { get; set; }

    }
}
