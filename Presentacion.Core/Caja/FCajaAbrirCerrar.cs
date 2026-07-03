using Presentacion.FBase.Helpers;
using Presentacion.FormulariosBase.Helpers;
using Servicios.LogicaNegocio.Caja;
using Servicios.LogicaNegocio.Movimiento;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Presentacion.Core.Caja
{
    public partial class FCajaAbrir : FBase.FBase
    {
        //private readonly ICajaServicio _cajaServicio;
        public CajaServicio cajaServicio;

        public FCajaAbrir(TipoOperacion tipo)
        {
            cajaServicio = new CajaServicio();
            InitializeComponent();

            lblUsuarioLogeado.Text = DatosSistema.NombreUsuario;

            if(tipo == TipoOperacion.Abrir)
            {
                this.Text = "Abrir Caja";
                btnAbrirCaja.Text = "Abrir Caja";

                lblConfirmacion.Text = "¿Confirma que desea abrir la caja?";
            }
            if(tipo == TipoOperacion.Cerrar)
            {
                this.Text = "Cerrar Caja";
                btnAbrirCaja.Text = "Cerrar Caja";

                lblConfirmacion.Text = "¿Confirma que desea cerrar la caja?";

                lblMontoApertura.Visible = false;
                txtMontoApertura.Visible = false;
            }
        }

        private void btnAbrirCaja_Click(object sender, EventArgs e)
        {
            string textoLimpio = new string(txtMontoApertura.Text.Where(char.IsDigit).ToArray());

            decimal montoApertura = string.IsNullOrEmpty(textoLimpio) ? 0 : Convert.ToDecimal(textoLimpio);
            // verficar si hay caja abierta, si hay avisar al usuario

            var estadoCaja = cajaServicio.ObtenerEstadoCaja();

            if (estadoCaja)
            {
                cajaServicio.CerrarCaja(DatosSistema.UsuarioId);

                DatosSistema.EstaCajaAbierta = false;
                DatosSistema.CajaId = null;
                DatosSistema.EstaCajaAbierta = false;

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                cajaServicio.AbrirCaja(montoApertura, DatosSistema.UsuarioId);

                DatosSistema.EstaCajaAbierta = true;
                DatosSistema.CajaId = cajaServicio.ObtenerIdCajaAbierta().Value;                
                DatosSistema.EstaCajaAbierta = true;
                
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void txtMontoApertura_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Permitir solo números enteros naturales (0-9) y la tecla de borrar (Backspace)
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true; // Bloquea cualquier otra herradura de teclado (letras, puntos, comas, signos menos)
            }
        }

        private void txtMontoApertura_TextChanged(object sender, EventArgs e)
        {
            if (sender is TextBox txt)
            {
                // Desvinculamos temporalmente el evento para evitar un bucle infinito al modificar el Text
                txt.TextChanged -= txtMontoApertura_TextChanged;

                // Extraemos solo los dígitos numéricos (elimina el $ anterior y separadores viejos)
                string soloNumeros = new string(txt.Text.Where(char.IsDigit).ToArray());

                if (long.TryParse(soloNumeros, out long valor))
                {
                    // "C0" da formato de moneda (Currency) con 0 decimales usando la cultura del sistema ($#,##0)
                    txt.Text = valor.ToString("C0");
                }
                else
                {
                    txt.Text = "$ 0";
                }

                // Mantiene el cursor siempre al final del texto para que no salte al principio al formatear
                txt.SelectionStart = txt.Text.Length;

                // Re-vinculamos el evento
                txt.TextChanged += txtMontoApertura_TextChanged;
            }
        }
    }
}
