using System.ComponentModel.DataAnnotations;

namespace AccesoDatos.Entidades
{
    public class CuentaCorrienteAutorizado
    {
        [Key]
        public long CuentaCorrienteAutorizadoId { get; set; }
        public long CuentaCorrienteId { get; set; }
        public long Dni { get; set; } //Dni autorizado a usar la cuenta corriente
        public CuentaCorriente CuentaCorriente { get; set; } //estoy obligado a tener esta propiedad? o ef me tiran error el el dbcontext 


    }
}
