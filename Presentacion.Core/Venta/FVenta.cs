using AccesoDatos.Entidades;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualBasic.Devices;
using Presentacion.AccesoAlSistema;
using Presentacion.Core.Empleado;
using Presentacion.Core.Producto;
using Presentacion.Core.Venta.TipoPago;
using Servicios.Helpers;
using Servicios.LogicaNegocio.Empleado;
using Servicios.LogicaNegocio.Empleado.DTO;
using Servicios.LogicaNegocio.Empleado.Rol.DTO;
using Servicios.LogicaNegocio.Producto;
using Servicios.LogicaNegocio.Producto.DTO;
using Servicios.LogicaNegocio.Venta.DTO;
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
        private decimal _totalVenta = 0.00m;
        private decimal _subTotalVenta = 0.00m;
        private decimal _porcentajeDescuento = 0.00m;
        private string descripcionVenta = "";
        //configuracion tomada del json de config.
        private bool _incluirCtaCte = true;
        //private List<ItemVentaDTO> itemsVenta;
          private BindingList<ItemVentaDTO> itemsVenta;
        private bool _actualizandoGrilla = false;

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

            // Inicializamos itemsVenta como BindingList
            itemsVenta = new BindingList<ItemVentaDTO>();
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
            dgvProductos.AllowUserToAddRows = false;
            CalcularTotal();
            txtSubtotal.Text = _subTotalVenta.ToString("C2");
            txtTotal.Text = _totalVenta.ToString("C2");
            ActualizarCamposInicio();
            itemsVenta = new BindingList<ItemVentaDTO>();
            dgvProductos.DataSource = itemsVenta;  // bind directo
            ResetearGrilla(dgvProductos);
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
            if (_totalVenta == 0)
            {
                MessageBox.Show("Debe cargar al menos un producto antes de confirmar la venta.");
                return;
            }
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

        private void btnCargarProducto_Click(object sender, EventArgs e)
        {
            if (!cbxEnOferta.Checked)
            {
                var fProductos = new FProductoConsulta(true);

                if (fProductos.ShowDialog() == DialogResult.OK && fProductos.productoSeleccionado.HasValue)
                {

                    var idProducto = fProductos.productoSeleccionado.Value;

                    var producto = new ProductoServicio().ObtenerProductoPorId(idProducto);
                    if (producto == null) return;

                    var fCantidad = new FCantidadItem();
                    if (fCantidad.ShowDialog() == DialogResult.OK && fCantidad.cantidad > 0)
                    {
                        var cantidad = fCantidad.cantidad;
                        var itemVenta = new ItemVentaDTO
                        {
                            ItemId = producto.ProductoId,
                            Descripcion = producto.Descripcion,
                            PrecioVenta = producto.PrecioVenta,
                            Cantidad = cantidad,
                            Medida = producto.Medida,
                            UnidadMedida = producto.UnidadMedida,
                            EsOferta = false
                        };

                        itemsVenta.Add(itemVenta);  // Solo agregamos a la BindingList
                        txtProductoCargado.Text = $"{itemVenta.Descripcion}";
                        // No necesitas reasignar DataSource ni resetear grilla acá
                        CalcularTotal();
                    }
                }
            }
        }
        private void CalcularTotal()
        {
            if (dgvProductos.RowCount > 0)
            {
                _subTotalVenta = itemsVenta.Sum(p => p.PrecioVenta * p.Cantidad);

            }
            else
            {
                _subTotalVenta = 0.00m;
            }
            _totalVenta = _subTotalVenta; //iria el descuento por porcentaje.
            txtTotal.Text = _totalVenta.ToString("C2");
            txtSubtotal.Text = _subTotalVenta.ToString("C2");
            txtTotal.Text = _totalVenta.ToString("C2");
        }

        private void ActualizarGrillas()
        {
            dgvProductos.Refresh(); // refresca la visualización
        }



        public virtual void IniciarGrilla(DataGridView grilla)
        {
            for (int i = 0; i < grilla.ColumnCount; i++)
            {
                grilla.Columns[i].Visible = false;
            }
        }

        private void ResetearGrilla(DataGridView grilla)
        {
            for (int i = 0; i < grilla.ColumnCount; i++)
            {
                grilla.Columns[i].Visible = false;
            }
            grilla.Columns["ItemId"].Visible = false;
            grilla.Columns["ItemId"].DisplayIndex = 0;

            grilla.Columns["Descripcion"].Visible = true;
            grilla.Columns["Descripcion"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            grilla.Columns["Descripcion"].DisplayIndex = 1;

            grilla.Columns["Medida"].Visible = true;
            grilla.Columns["Medida"].Width = 100;
            grilla.Columns["Medida"].DisplayIndex = 2;

            grilla.Columns["UnidadMedida"].Visible = true;
            grilla.Columns["UnidadMedida"].Width = 100;
            grilla.Columns["UnidadMedida"].DisplayIndex = 3;

            grilla.Columns["Cantidad"].Visible = true;
            grilla.Columns["Cantidad"].Width = 100;
            grilla.Columns["Cantidad"].DisplayIndex = 4;

            grilla.Columns["PrecioVenta"].Visible = true;
            grilla.Columns["PrecioVenta"].Width = 100;
            grilla.Columns["PrecioVenta"].DisplayIndex = 5;


        }

        private void cbxAplicarDescuento_CheckedChanged(object sender, EventArgs e)
        {
            if (cbxAplicarDescuento.Checked)
            {
                if (string.IsNullOrWhiteSpace(txtDescuento.Text))
                {
                    MessageBox.Show("Ingrese un porcentaje de descuento.");
                    cbxAplicarDescuento.Checked = false;
                    return;
                }

                if (decimal.TryParse(txtDescuento.Text, out decimal porcentaje))
                {
                    if (porcentaje < 1 || porcentaje > 100)
                    {
                        MessageBox.Show("El descuento debe estar entre 1% y 100%.");
                        cbxAplicarDescuento.Checked = false;
                        return;
                    }

                    _porcentajeDescuento = porcentaje / 100;

                    CalcularTotal();
                }
                else
                {
                    MessageBox.Show("Ingrese un número válido.");
                    cbxAplicarDescuento.Checked = false;
                }
            }
            else
            {
                // Si se desactiva el descuento, volver a los valores originales
                txtDescuento.Text = string.Empty;
                CalcularTotal();

            }
        }


        private void txtDescuento_TextChanged(object sender, EventArgs e)
        {
            if (!txtDescuento.Text.IsNullOrEmpty())
            {
                cbxAplicarDescuento.Enabled = true;
            }
            else
            {
                cbxAplicarDescuento.Enabled = false;
            }
        }

        private void dgvProductos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (_actualizandoGrilla) return;
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;

            var fila = dgvProductos.Rows[e.RowIndex];
            if (fila == null || fila.IsNewRow) return;

            // Aquí tu código seguro para manejar el click
        }

        private void dgvProductos_SelectionChanged(object sender, EventArgs e)
        {
            if (_actualizandoGrilla) return;
            if (dgvProductos.CurrentRow == null) return;
            if (dgvProductos.CurrentRow.IsNewRow) return;

            if (!dgvProductos.Columns.Contains("ItemId")) return;

            var val = dgvProductos.CurrentRow.Cells["ItemId"].Value;
            if (val == null || val == DBNull.Value) return;

            // Lógica segura con val
        }

        private void dgvProductos_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (_actualizandoGrilla) return;

            if (e.RowIndex < 0 || e.RowIndex >= dgvProductos.Rows.Count)
                return;

            var fila = dgvProductos.Rows[e.RowIndex];
            if (fila == null || fila.IsNewRow)
                return;

            if (!dgvProductos.Columns.Contains("ItemId"))
                return;

            var celda = fila.Cells["ItemId"];
            if (celda?.Value == null || celda.Value == DBNull.Value)
                return;

            long entidadID = Convert.ToInt64(celda.Value);
            // tu lógica...
        }


    }

}
