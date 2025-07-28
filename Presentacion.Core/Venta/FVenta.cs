using AccesoDatos.Entidades;
using Microsoft.VisualBasic.Devices;
using Presentacion.AccesoAlSistema;
using Presentacion.Core.Empleado;
using Presentacion.Core.Venta.TipoPago;
using Servicios.Helpers;
using Servicios.LogicaNegocio.Empleado;
using Servicios.LogicaNegocio.Empleado.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Presentacion.Core.Venta
{
    public partial class FVenta : FBase.FBase
    {
        private readonly IEmpleadoServicio _empleadoServicio;
        private bool finalizarVenta = false;
        private CuerpoDetalleVenta _cuerpoDetalleVenta;
        private readonly long _usuarioLogeadoID;
        private UsuarioLogeado _usuarioLogeado;
        private bool esUsuarioLogeado;
        private bool esConsumidorFinal;
        private System.Windows.Forms.Timer timer;
        private decimal _totalVenta = 9240.12m;
        private decimal _subTotalVenta;
        private string descripcionVenta = "";
        //configuracion tomada del json de config.
        private bool _incluirIva = true;
        private bool _incluirCtaCte = true;

        public FVenta(long usuarioLogeadoID)
        {
            InitializeComponent();

            _empleadoServicio = new EmpleadoServicio();
            _cuerpoDetalleVenta = new CuerpoDetalleVenta
            {
                tiposDePago = new List<Pago>(),
                pagoParcial = false,
                saldoPendiente = 0.00m,
            };
        
            _usuarioLogeadoID = usuarioLogeadoID;
        }

        private void FVenta_Load(object sender, EventArgs e)
        {
            System.Windows.Forms.Timer MyTimer = new System.Windows.Forms.Timer();
            MyTimer.Interval = 1000;
            MyTimer.Tick += new EventHandler(MyTimer_Tick);
            MyTimer.Start();


            var usuarioLogeado = _empleadoServicio.ObtenerEmpleadoPorId(_usuarioLogeadoID);
            _usuarioLogeado = new UsuarioLogeado
            {
                PersonaId = usuarioLogeado.PersonaId,
                Username = usuarioLogeado.Username,
                Nombre = usuarioLogeado.Nombre,
                Apellido = usuarioLogeado.Apellido,
            };
            lblUsuarioLogeadoName.Text = _usuarioLogeado.Username;
            cbxConsumidorFinal.Checked = true;
            esConsumidorFinal = true;
            cbxUsuarioLogeado.Checked = true;
            esUsuarioLogeado = true;
            btnCargarVendedor.Enabled = false;
            btnCargarCliente.Enabled = false;
            cbxIncluirCtaCte.Checked = true; 
            cbxIncluirIva.Checked = true;
            txtTotal.Text = _totalVenta.ToString("C2");
            ActualizarCamposInicio();
        }
        private void MyTimer_Tick(object sender, EventArgs e)
        {
            lblFechaHoy.Text = DateTime.Now.ToString("dd/MM/yyyy");
            lblHoraMinutos.Text = DateTime.Now.ToString("HH:mm:ss");
        }

        private void ActualizarCamposInicio()
        {
            //Tiene que ser la carga del cliente cuando exista de momento se va a crear automticamente. Por defecto es el consumidor final
            txtCliente.Text = esConsumidorFinal ? "Consumidor Final" : "";
            txtVendedorAsignado.Text = esUsuarioLogeado
                ? $"{_usuarioLogeado.Username} ({_usuarioLogeado.Nombre} {_usuarioLogeado.Apellido})"
                : "";
        }


        private void cbxUsuarioLogeado_CheckedChanged(object sender, EventArgs e)
        {
            esUsuarioLogeado = !esUsuarioLogeado;
            ActualizarCamposInicio();
            btnCargarVendedor.Enabled = !esUsuarioLogeado;
        }

        private void cbxConsumidorFinal_CheckedChanged(object sender, EventArgs e)
        {
            esConsumidorFinal = !esConsumidorFinal;
            ActualizarCamposInicio();
            btnCargarCliente.Enabled = !esConsumidorFinal;
        }

        private void btnCargarVendedor_Click(object sender, EventArgs e)
        {
            if (!cbxUsuarioLogeado.Checked)
            {
                btnCargarVendedor.Enabled = true;
                var fEmpleado = new FEmpleadoConsulta(true);

                if (fEmpleado.ShowDialog() == DialogResult.OK && fEmpleado.empleadoSeleccionado.HasValue)
                {
                    var idEmpleado = fEmpleado.empleadoSeleccionado.Value;


                    var vendedor = new EmpleadoServicio().ObtenerEmpleadoPorId(idEmpleado);

                    txtVendedorAsignado.Text = $"{vendedor.Username} ({vendedor.Nombre} {vendedor.Apellido})";

                }
            }
        }
        private void btnConfirmarYFPago_Click(object sender, EventArgs e)
        {
            if (finalizarVenta)
            {
                this.DialogResult = DialogResult.OK;
                // funcion de guardar venta
                this.Close();
                return;
            }
            DatosVenta datosVenta = new DatosVenta
            {
                Total = _totalVenta,
                IncluirIva = _incluirIva,
                IncluirCtaCte = _incluirCtaCte
            };
            var fConfirmarVenta = new FConfirmacionVenta(datosVenta);
            if (fConfirmarVenta.ShowDialog() == DialogResult.OK)
            {

                var tipoPagosSeleccionados = fConfirmarVenta.pagos;
                if (tipoPagosSeleccionados.Count == 0)
                {
                    MessageBox.Show("Debe seleccionar al menos un tipo de pago.");
                    return;
                }
                _cuerpoDetalleVenta.tiposDePago = tipoPagosSeleccionados;
                _cuerpoDetalleVenta.saldoPendiente = fConfirmarVenta.MontoPendiente;
                if (fConfirmarVenta.MontoPendiente > 0.00m)
                {
                    _cuerpoDetalleVenta.pagoParcial = true;
                }

                txtAreaDetallesVenta.Text = _cuerpoDetalleVenta.CuerpoDelTextoTP();

                if (tipoPagosSeleccionados.Count > 0)
                {
                    var fConfirmarDetalle = new FDetalleVenta();
                    if (fConfirmarDetalle.ShowDialog() == DialogResult.OK)
                    {
                        if (fConfirmarDetalle.confirmarDetalle)
                        {
                            btnConfirmarYFPago.Text = "Confirmar Venta";
                            finalizarVenta = true;
                        }
                        if (!string.IsNullOrWhiteSpace(fConfirmarDetalle.descripcionDetalle))
                        {
                            descripcionVenta = fConfirmarDetalle.descripcionDetalle.Trim();
                        }
                        else
                        {
                            descripcionVenta = "Sin detalles adicionales.";
                        }
                        txtAreaDetallesVenta.Text = _cuerpoDetalleVenta.CuerpoDelTextoFinal(descripcionVenta);

                    }
                }
            }

        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cbxIncluirCtaCte_CheckedChanged(object sender, EventArgs e)
        {
            _incluirCtaCte = cbxIncluirCtaCte.Checked;
            ActualizarCamposInicio();
        }

        private void cbxIncluirIva_CheckedChanged(object sender, EventArgs e)
        {
            _incluirIva = cbxIncluirIva.Checked;
            ActualizarCamposInicio();
        }
    }
}
