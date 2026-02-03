using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatos.Entidades
{
    public class CuentaCorriente
    {
        [Key]
        public long CuentaCorrienteId { get; set; }
        public long ClienteId { get; set; }
        public string NombreCuentaCorriente { get; set; } //N
        public decimal Saldo { get; set; } // puede ser negativo (ej: -500 = debe 500)
        public decimal LimiteDeuda { get; set; } // cuánto puede deber como máximo
        public bool EstaEliminado { get; set; }
        public bool LimiteDeudaActivo { get; set; } // Indica si el límite de deuda está activo
        public DateTime? FechaVencimiento { get; set; } //venciemiento de la cuenta corriente
        public int EstadoCuentaCorriente { get; set; } = 0; // 

        public Cliente Cliente { get; set; } 
        public ICollection<Movimiento> Movimientos { get; set; }
        public ICollection<CuentaCorrienteAutorizado> CuentaCorrienteAutorizado { get; set; } // Dni autorizados a usar la cuenta corriente
    }
}
