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

        // --- Etapa 1 del refactor de Movimientos (campos nuevos, todavía sin uso) ---

        /// <summary>true = el movimiento entra en el Libro de caja (suma a Ingresos/Egresos de una caja).</summary>
        public bool AfectaCaja { get; set; }

        /// <summary>Caja a la que pertenece el movimiento. Obligatorio si AfectaCaja, prohibido si no.</summary>
        public long? CajaId { get; set; }
        public Caja? Caja { get; set; }

        /// <summary>Tipo de pago del movimiento (Efectivo, Transferencia, CtaCte, ...).</summary>
        public long? TipoPagoId { get; set; }
        public TipoPago? TipoPago { get; set; }

        /// <summary>En una reversa/cancelación: el movimiento original que revierte.</summary>
        public long? MovimientoOrigenId { get; set; }
        public Movimiento? MovimientoOrigen { get; set; }

    }
}
