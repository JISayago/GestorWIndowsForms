using Servicios.LogicaNegocio.Venta.Oferta;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentacion.Core.Presentacion.Core.Helpers
{
    public static class CodigoOfertaHelper
    {
        public static string ObtenerCodigo(OfertaServicio ofertaServicio, IWin32Window owner)
        {
            var respuesta = MessageBox.Show(
                owner,
                "¿Desea ingresar el código de la oferta manualmente?",
                "Tipo de Código",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (respuesta == DialogResult.No)
            {
                return "*"; // 🔥 automático
            }

            while (true)
            {
                string codigoManual = MostrarInputCodigo(owner);

                if (codigoManual == null) // Canceló
                    return null;

                if (string.IsNullOrWhiteSpace(codigoManual))
                {
                    MessageBox.Show(owner, "El código no puede estar vacío.");
                    continue;
                }

                if (ofertaServicio.ExisteOfertaPorCodigo(codigoManual))
                {
                    MessageBox.Show(
                        owner,
                        $"Ya existe una oferta con el código '{codigoManual}'. Ingrese otro.",
                        "Código duplicado",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    continue;
                }

                return codigoManual.Trim();
            }
        }

        private static string MostrarInputCodigo(IWin32Window owner)
        {
            using (Form prompt = new Form())
            {
                prompt.Width = 350;
                prompt.Height = 170;
                prompt.FormBorderStyle = FormBorderStyle.FixedDialog;
                prompt.Text = "Ingrese Código Manual";
                prompt.StartPosition = FormStartPosition.CenterParent;
                prompt.MinimizeBox = false;
                prompt.MaximizeBox = false;

                Label lbl = new Label()
                {
                    Left = 20,
                    Top = 20,
                    Text = "Código:",
                    AutoSize = true
                };

                TextBox txt = new TextBox()
                {
                    Left = 20,
                    Top = 45,
                    Width = 280
                };

                Button btnOk = new Button()
                {
                    Text = "Aceptar",
                    Left = 140,
                    Width = 75,
                    Top = 80,
                    DialogResult = DialogResult.OK
                };

                Button btnCancel = new Button()
                {
                    Text = "Cancelar",
                    Left = 225,
                    Width = 75,
                    Top = 80,
                    DialogResult = DialogResult.Cancel
                };

                prompt.Controls.Add(lbl);
                prompt.Controls.Add(txt);
                prompt.Controls.Add(btnOk);
                prompt.Controls.Add(btnCancel);

                prompt.AcceptButton = btnOk;
                prompt.CancelButton = btnCancel;

                return prompt.ShowDialog(owner) == DialogResult.OK
                    ? txt.Text
                    : null;
            }
        }
    }
}
