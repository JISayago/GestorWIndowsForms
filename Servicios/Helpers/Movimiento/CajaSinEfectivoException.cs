using System;
using System.Globalization;

namespace Servicios.Helpers.Movimiento
{
    /// <summary>
    /// Se lanza cuando hay que devolver efectivo (cancelación de una venta) y la caja abierta
    /// no tiene efectivo suficiente. Los servicios la atrapan y devuelven una advertencia
    /// con el mensaje ya listo para mostrarle al usuario.
    /// </summary>
    public class CajaSinEfectivoException : Exception
    {
        public decimal EfectivoNecesario { get; }
        public decimal EfectivoDisponible { get; }
        public decimal Faltante => EfectivoNecesario - EfectivoDisponible;

        public CajaSinEfectivoException(decimal efectivoNecesario, decimal efectivoDisponible)
            : base(ArmarMensaje(efectivoNecesario, efectivoDisponible))
        {
            EfectivoNecesario = efectivoNecesario;
            EfectivoDisponible = efectivoDisponible;
        }

        private static string ArmarMensaje(decimal necesario, decimal disponible)
        {
            var cultura = CultureInfo.CurrentCulture;

            return
                "La caja no tiene efectivo suficiente para devolver el dinero de esta cancelación." + Environment.NewLine +
                Environment.NewLine +
                $"Efectivo a devolver: {necesario.ToString("C2", cultura)}" + Environment.NewLine +
                $"Efectivo disponible en caja: {disponible.ToString("C2", cultura)}" + Environment.NewLine +
                $"Faltan: {(necesario - disponible).ToString("C2", cultura)}" + Environment.NewLine +
                Environment.NewLine +
                "Agregá saldo a la caja desde la pantalla Caja (botón \"Agregar saldo\") y volvé a intentar la cancelación.";
        }
    }
}
