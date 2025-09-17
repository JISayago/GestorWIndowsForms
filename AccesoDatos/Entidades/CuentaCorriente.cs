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
        public string NombreCuentaCorriente { get; set; } //N
        public decimal Saldo { get; set; } // puede ser negativo (ej: -500 = debe 500)
        public decimal LimiteDeuda { get; set; } // cuánto puede deber como máximo //Mod nombre
        public bool EstaEliminado { get; set; }
        public bool LimiteDeudaActivo { get; set; } // Indica si el límite de deuda está activo //N
        public DateTime? FechaVencimiento { get; set; } //venciemiento de la cuenta corriente //N
        public ICollection<Cliente> Clientes { get; set; }

        //public ICollection<MovimientoCuentaCorriente> MovimientosCuentaCorriente { get; set; } // Relación uno a muchos con MovimientosCuentaCorriente
    }
}
