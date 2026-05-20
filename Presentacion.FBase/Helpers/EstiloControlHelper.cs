using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace Presentacion.FormulariosBase.Helpers
{
    public static class EstiloControlHelper
    {
        /// <summary>
        /// Aplica de forma recursiva el estilo visual estándar (Segoe UI, Gris Antracita)
        /// a todos los Labels y CheckBoxes dentro de un contenedor.
        /// </summary>
        public static void AplicarEstiloALabels(Control contenedor) //Aplica estilado a Labels y Checkboxs, agregar Tag NoModificarConBase los elementos que queremos ignorar
        {
            if (contenedor == null) return;

            foreach (Control control in contenedor.Controls)
            {
                // 1. Tratamiento para Labels
                // 1. Aplicamos el estilo al control actual si corresponde
                if (control is Label lbl && lbl.Tag?.ToString() != "NoModificarConBase")
                {
                    lbl.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
                    lbl.ForeColor = Color.FromArgb(50, 51, 53);

                    //if (lbl.Tag?.ToString() == "NoModificarConBase")
                    //{
                    //    //lbl.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
                    //    //lbl.ForeColor = Color.FromArgb(30, 30, 30);
                    //}
                }

                else if (control is CheckBox chk)
                {
                    chk.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
                    chk.ForeColor = Color.FromArgb(50, 51, 53);
                }

                else if (control is RadioButton rdb)
                {
                    //// Mismo control por si querés saltear alguno en el futuro usando el Tag
                    //if (rdb.Tag?.ToString() == "NoModificarConBase") continue;

                    // Aplicamos la misma línea estética: Segoe UI, 9.75pt, Bold y Gris Oscuro neutro
                    rdb.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
                    rdb.ForeColor = Color.FromArgb(50, 51, 53);
                }

                // 3. RECURSIÓN: Si el control tiene hijos (Paneles, GroupBox, TabPages), entra a estilar adentro
                if (control.HasChildren)
                {
                    AplicarEstiloALabels(control);
                }
            }
        }
    }
}