using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatos.Entidades
{
    public class Cliente
    {
        [Key]
        public long PersonaId { get; set; }  // Mismo ID que la persona
        public long? CuentaCorrienteId { get; set; } // HACER NULLEABLE EN DB TAMB
        public string NumeroCliente { get; set; } //Usarlo para mostrarlo al cliente en casos como movimientos, para no mostrar datos sensible
        public DateTime FechaAlta { get; set; }
        public DateTime? FechaBaja { get; set; } // Puede ser null si sigue siendo cliente
        public int Estado { get; set; }
        public string EstadoDescripcion { get; set; } = string.Empty;

        // Relaciones
        public Persona Persona { get; set; }        // Uno a uno con Persona
        public CuentaCorriente CuentaCorriente { get; set; } // Muchos a uno con CuentaCorriente // deberia ser una collection? o los dejamos 1:N
    }
}
