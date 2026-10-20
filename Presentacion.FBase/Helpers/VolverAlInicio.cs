using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentacion.FBase.Helpers
{
    public class VolverAlInicio
    {
        private readonly Form _formActual;

        public VolverAlInicio(Form formactual) {
            _formActual = formactual;
        }

        public void Volver()
        {
            var resp = MessageBox.Show(
                "¿Desea volver a la ventana principal de Stockeate?",
                "Stockeate",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (resp != DialogResult.Yes)
                return;
            if (resp == DialogResult.Yes)
            {
                // Buscar la instancia existente de la ventana principal.
                Form principal = Application.OpenForms
                    .Cast<Form>()
                    .FirstOrDefault(f => f.GetType().Name == "VentanaPrincipal");

                if (principal == null || principal.IsDisposed)
                {
                    MessageBox.Show(
                        "No se encontró la ventana principal de Stockeate.",
                        "Stockeate",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    return;
                }

                // Tomar una copia porque los formularios se van a cerrar.
                Form[] ventanas = Application.OpenForms
                    .Cast<Form>()
                    .Where(f => f != principal && !f.IsDisposed)
                    // Cerrar primero la ventana de consulta actual.
                    .OrderBy(f => f == _formActual ? 0 : 1)
                    .ToArray();

                foreach (Form ventana in ventanas)
                {
                    if (!ventana.IsDisposed)
                    {
                        ventana.Close();
                    }
                }

                // Dejar visible y activa la ventana principal existente.
                if (principal.IsDisposed)
                    return;

                if (principal.WindowState == FormWindowState.Minimized)
                {
                    principal.WindowState = FormWindowState.Normal;
                }

                principal.Show();
                principal.BringToFront();
                principal.Activate();
            }
             }

        }
}
