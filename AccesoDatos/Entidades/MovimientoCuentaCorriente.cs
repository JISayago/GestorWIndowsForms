using System.ComponentModel.DataAnnotations;

namespace AccesoDatos.Entidades
{
    public class MovimientoCuentaCorriente
    {
        [Key]
        public long MovimientoCuentaCorrienteId { get; set; }
        public DateTime Fecha { get; set; } = DateTime.Now;
        public decimal Monto { get; set; }  // Positivo = depósito, Negativo = compra/pago
        public string Descripcion { get; set; }
        public int TipoMovimientoCCorriente { get; set; }

        public long CuentaCorrienteId { get; set; }
        public CuentaCorriente CuentaCorriente { get; set; }
    }

}
