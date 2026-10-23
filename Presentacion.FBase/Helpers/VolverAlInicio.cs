using System;
using System.Linq;
using System.Windows.Forms;
// using el namespace donde esté definida la clase Mensajes y TipoMensaje

namespace Presentacion.FBase.Helpers
{
    public class VolverAlInicio
    {
        private readonly Form _formActual;

        public VolverAlInicio(Form formActual)
        {
            _formActual = formActual;
        }

        public void Volver()
        {
            bool confirmado = Mensajes.Confirmar(
                "Está por cerrar todas las Ventanas abiertas para volver a la ventana principal de Stockeate. \n\n " +
                "¿Está Seguro?",
                "Volver al Inicio",
                "Sí", "No",
                TipoMensaje.Pregunta,
                false,
                _formActual);

            if (!confirmado)
                return;

            Form principal = Application.OpenForms
                .Cast<Form>()
                .FirstOrDefault(f => f.GetType().Name == "VentanaPrincipal");

            if (principal == null || principal.IsDisposed)
            {
                Mensajes.Advertencia(
                    "No se encontró la ventana principal de Stockeate.",
                    "Stockeate",
                    owner: _formActual);
                return;
            }

            Form[] ventanas = Application.OpenForms
                .Cast<Form>()
                .Where(f => f != principal && !f.IsDisposed)
                .OrderBy(f => f == _formActual ? 0 : 1)
                .ToArray();

            foreach (Form ventana in ventanas)
            {
                if (!ventana.IsDisposed)
                    ventana.Close();
            }

            if (principal.IsDisposed)
                return;

            if (principal.WindowState == FormWindowState.Minimized)
                principal.WindowState = FormWindowState.Normal;

            principal.Show();
            principal.BringToFront();
            principal.Activate();
        }
    }
}