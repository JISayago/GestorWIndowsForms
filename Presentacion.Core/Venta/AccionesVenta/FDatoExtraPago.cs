using AccesoDatos.Entidades;
using Servicios.Helpers.Cliente.DatosPagadorDTO;
using Servicios.Helpers.OpcionesPagos;
using Servicios.LogicaNegocio.Cliente.DTO;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Presentacion.Formularios
{
    public class FDatosPagador : Form
    {
        private Label lblTitulo;
        private Label lblTipoPago;

        private Label lblDocumento;
        private TextBox txtDocumento;

        private Label lblNombre;
        private TextBox txtNombre;

        private Button btnAceptar;
        private Button btnCancelar;

        private readonly TipoDePago _tipoPago;
        private readonly ClienteDTO _cliente;   
        public string datosExtra { get; private set; } // Para información adicional como DNI o nombre del cliente en pagos.

        public FDatosPagador(TipoDePago tipoPago, ClienteDTO cliente)
        {
            _tipoPago = tipoPago;
            _cliente = cliente;

            InicializarFormulario();
            ConfigurarControles();
        }

        private void InicializarFormulario()
        {
            Text = "Datos extra del Pago";
            StartPosition = FormStartPosition.CenterParent;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;

            Width = 450;
            Height = 280;

           

            lblTitulo = new Label
            {
                Text = "Datos extra asignados al pago.",
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                AutoSize = true,
                Location = new Point(20, 15)
            };

            lblTipoPago = new Label
            {
                AutoSize = true,
                Location = new Point(20, 50)
            };

            lblDocumento = new Label
            {
                Text = "Documento",
                AutoSize = true,
                Location = new Point(20, 85)
            };

            txtDocumento = new TextBox
            {
                Width = 250,
                Location = new Point(20, 105)
            };

            lblNombre = new Label
            {
                Text = "Nombre y Apellido",
                AutoSize = true,
                Location = new Point(20, 140)
            };

            txtNombre = new TextBox
            {
                Width = 380,
                Location = new Point(20, 160)
            };


            btnAceptar = new Button
            {
                Text = "Aceptar",
                Width = 100,
                Height = 35,
                Location = new Point(190, 195)
            };

            btnCancelar = new Button
            {
                Text = "Cancelar",
                Width = 100,
                Height = 35,
                Location = new Point(300, 195)
            };

            Controls.Add(lblTitulo);
            Controls.Add(lblTipoPago);
            Controls.Add(lblDocumento);
            Controls.Add(txtDocumento);
            Controls.Add(lblNombre);
            Controls.Add(txtNombre);
            Controls.Add(btnAceptar);
            Controls.Add(btnCancelar);

            if (_cliente.NumeroCliente != "0")
            {
                txtDocumento.Text = _cliente.Dni;
                txtNombre.Text = _cliente.NombreCompleto;
            }
        }

        private void ConfigurarControles()
        {
            lblTipoPago.Text = $"Tipo de Pago: {_tipoPago}";

            btnAceptar.Click += BtnAceptar_Click;
            btnCancelar.Click += BtnCancelar_Click;

            bool solicitarDatos =
                _tipoPago == TipoDePago.Credito ||
                _tipoPago == TipoDePago.Debito ||
                _tipoPago == TipoDePago.Transferencia ||
                _tipoPago == TipoDePago.QR;

            lblDocumento.Visible = solicitarDatos;
            txtDocumento.Visible = solicitarDatos;

            lblNombre.Visible = solicitarDatos;
            txtNombre.Visible = solicitarDatos;


            if (!solicitarDatos)
            {
                Height = 160;
                btnAceptar.Location = new Point(190, 70);
                btnCancelar.Location = new Point(300, 70);
            }
        }

        private void BtnAceptar_Click(object sender, EventArgs e)
        {
            datosExtra = $"DNI: {(string.IsNullOrWhiteSpace(txtDocumento.Text) ? null : txtDocumento.Text.Trim())}, Nombre: {(string.IsNullOrWhiteSpace(txtNombre.Text) ? null : txtNombre.Text.Trim())}";
            DialogResult = DialogResult.OK;
            Close();
        }

        private void BtnCancelar_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}