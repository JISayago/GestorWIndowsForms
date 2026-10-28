using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Stockeate
{
    /// <summary>
    /// Pantalla completa que tapa y bloquea a la ventana principal mientras la sesión secundaria está abierta.
    /// No se puede cerrar a mano: se cierra sola cuando el proceso secundario termina (también si se cae).
    /// Se muestra con ShowDialog(owner), así que es modal respecto de toda la ventana principal.
    /// </summary>
    internal sealed partial class FSesionSecundariaEnCurso : Form
    {
        private volatile bool _puedeCerrar;

        public FSesionSecundariaEnCurso(Process proceso)
        {
            FormBorderStyle = FormBorderStyle.None;
            WindowState = FormWindowState.Maximized;
            ShowInTaskbar = false;
            ControlBox = false;
            BackColor = Color.FromArgb(32, 38, 48);

            Controls.Add(new Label
            {
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter,
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 20F, FontStyle.Regular),
                Text = "Hay una sesión secundaria abierta.\r\n\r\n" +
                       "Esta ventana se habilita cuando la sesión secundaria se cierre."
            });

            // Exited se dispara en un hilo del pool: se vuelve al hilo de la UI con BeginInvoke.
            proceso.EnableRaisingEvents = true;
            proceso.Exited += (_, _) => Liberar();

            // Si el proceso ya terminó antes de mostrarse el modal, no hay quien dispare Exited.
            Shown += (_, _) =>
            {
                if (proceso.HasExited) Liberar();
            };
        }

        private void Liberar()
        {
            _puedeCerrar = true;

            try
            {
                if (IsHandleCreated && !IsDisposed)
                    BeginInvoke(new Action(Close));
            }
            catch (ObjectDisposedException) { }
            catch (InvalidOperationException) { }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            // Bloquea Alt+F4 y cualquier cierre manual mientras la secundaria siga viva.
            if (!_puedeCerrar)
                e.Cancel = true;

            base.OnFormClosing(e);
        }
    }
}
