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

        public int TipoMovimiento { get; set; } // Ingreso - Egreso, se puede utilizar un enum para definir los tipos de movimiento, esto es importante para el manejo de caja y reportes financieros.

        public int TipoMovimientoDetalle { get; set; } // Para aclarar el Area del movimiento. principalmente para filtrar el tipo de movimiento.

        public decimal Monto { get; set; }

        public DateTime FechaMovimiento { get; set; }

        public bool EstaEliminado { get; set; }

        // referencia genérica a cualquier entidad del sistema
        public int? TipoEntidad { get; set; } // TipoEntidadMovimiento enum, para identificar a que entidad se refiere el movimiento nombre representativo de la carpeta de entidades. (venta, producto, caja, etc)
        public long? EntidadId { get; set; }

    }
}
