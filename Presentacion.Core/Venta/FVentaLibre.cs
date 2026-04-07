using AccesoDatos.Entidades;
using Microsoft.Extensions.Logging;
using Presentacion.AccesoAlSistema;
using Presentacion.Core.Cliente;
using Presentacion.Core.Venta.HelpersVenta;
using Presentacion.FBase.Helpers;
using Servicios.Helpers.OpcionesPagos;
using Servicios.LogicaNegocio.Cliente;
using Servicios.LogicaNegocio.Cliente.DTO;
using Servicios.LogicaNegocio.CuentaCorriente;
using Servicios.LogicaNegocio.Empleado;
using Servicios.LogicaNegocio.Movimiento;
using Servicios.LogicaNegocio.Venta;
using Servicios.LogicaNegocio.Venta.DTO;
using Servicios.LogicaNegocio.Venta.Oferta;
using Servicios.LogicaNegocio.Venta.VentaLibre;
using Servicios.LogicaNegocio.Venta.VentaLibre.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Presentacion.Core.Venta
{
    public partial class FVentaLibre : Form
    {
        private readonly IVentaServicio _ventaServicio;
        private readonly IVentaLibreServicio _ventaLibreServicio;
        private readonly IEmpleadoServicio _empleadoServicio;
        private readonly IOfertaServicio _ofertaServicio;
        private readonly IMovimientoServicio _movimientoServicio;
        private readonly IClienteServicio _clienteServicio;

        private BindingList<ItemVentaDTO> _itemsVenta;
        private long _usuarioLogeadoID;
        private long idCliente;
        private ClienteDTO _clienteVenta;
        private bool esConsumidorFinal;
        private UsuarioLogeado _usuarioLogeado;
        private long idVendedor;    
        private bool esUsuarioLogeado = false;
        private decimal _totalVenta = 0.00m;
        private decimal _subTotalVenta = 0.00m;
        private bool _incluirCtaCte = false;
        private bool finalizarVenta = false;
        private CuerpoDetalleVenta _cuerpoDetalleVenta;
        private List<FormaPago> tipoDePagosVenta;
        private string descripcionVenta = "";
        private VentaLibreDTO  _ventaLibreDto;
        public FVentaLibre(long UsuarioLogeadoId)
        {
            InitializeComponent();
            _ventaServicio = new VentaServicio();
            _ventaLibreServicio = new VentaLibreServicio();
            _movimientoServicio = new MovimientoServicio();
            _usuarioLogeadoID = UsuarioLogeadoId;
            _empleadoServicio = new EmpleadoServicio();
            _ofertaServicio = new OfertaServicio();
            _clienteServicio = new ClienteServicio();

            _itemsVenta = new BindingList<ItemVentaDTO>();
            _cuerpoDetalleVenta = new CuerpoDetalleVenta
            {
                tiposDePago = new List<FormaPago>(),
                pagoParcial = false,
                saldoPendiente = 0.00m,
            };
        }
        private void MyTimer_Tick(object sender, EventArgs e)
        {
            var ahora = DateTime.Now;
            var cultura = new CultureInfo("es-ES");

            lblFechaHoy.Text = ahora.ToString("dd/MM/yyyy");
            lblHoraMinutos.Text = ahora.ToString("HH:mm:ss");
            lblDiaAbrev.Text = cultura.TextInfo.ToTitleCase(ahora.ToString("ddd", cultura)) + ".";

        }
        private void btnAgregarProducto_Click(object sender, EventArgs e)
        {
            var itemVenta = new ItemVentaDTO()
            {
                ItemId = 1,
                Cantidad = Convert.ToDecimal(txtCantidad.Text),
                Descripcion = txtDescripcion.Text,
                EsOferta = false,
                Medida = "Unidad",
                PrecioOferta = 0,
                PrecioVenta = Convert.ToDecimal(txtPrecio.Text),
                UnidadMedida = "Unidad"
            };
            _itemsVenta.Add(itemVenta);

            txtDescripcion.Text = string.Empty;
            txtCodigoProducto.Text = string.Empty;
            txtCantidad.Text = string.Empty;
            txtPrecio.Text = string.Empty;
            CalcularTotal();
        }

        private void FVentaLibre_Load(object sender, EventArgs e)
        {

            InicializarYLimpiarCampos();
            ResetearGrilla(dgvProductos);
            dgvProductos.DataSource = _itemsVenta;
        }


        public virtual void IniciarGrilla(DataGridView grilla)
        {
            for (int i = 0; i < grilla.ColumnCount; i++)
            {
                grilla.Columns[i].Visible = false;
            }
        }
        private void ActualizarCamposInicio()
        {
            // Cliente
            if (_clienteVenta != null)
            {
                txtCliente.Text = $"{_clienteVenta.Nombre} {_clienteVenta.Apellido}";
            }
            else
            {
                txtCliente.Text = string.Empty;
            }

            // Vendedor
            if (_usuarioLogeado != null)
            {
                lblVendedorAsignado.Text = $"{_usuarioLogeado.Nombre} {_usuarioLogeado.Apellido}";
            }
            else
            {
                lblVendedorAsignado.Text = string.Empty;
            }

            // Cuenta corriente según tipo cliente
            if (esConsumidorFinal)
            {
                cbxIncluirCtaCte.Checked = false;
                cbxIncluirCtaCte.Enabled = false;
            }
            else
            {
                cbxIncluirCtaCte.Enabled = true;
            }
        }

        private void InicializarYLimpiarCampos()
        {
            var clienteDefault = _clienteServicio.ObtenerClientePorNumero("0");

            _clienteVenta = new ClienteDTO
            {
                PersonaId = clienteDefault.PersonaId,
                Nombre = clienteDefault.Nombre,
                Apellido = clienteDefault.Apellido
            };

            _itemsVenta = new BindingList<ItemVentaDTO>();
            dgvProductos.DataSource = _itemsVenta;

            ResetearGrilla(dgvProductos);
            CalcularTotal();

            lblNro.Text = "(Pendiente)";

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

            txtDescuentoEfectivo.Text = string.Empty;

            // 🔥 TU LÓGICA
            esConsumidorFinal = true;
            cbxConsumidorFinal.Checked = true;

            btnCargarCliente.Enabled = false; // SOLO se habilita si destildás

            idVendedor = _usuarioLogeadoID;

            cbxIncluirCtaCte.Checked = false;
            cbxIncluirCtaCte.Enabled = false;

            dgvProductos.AllowUserToAddRows = false;
            cbxDescEfectivo.Checked = false;

            txtSubtotal.Text = _subTotalVenta.ToString("C2");
            txtTotal.Text = _totalVenta.ToString("C2");

            ActualizarCamposInicio();
        }

        private void CalcularTotal()
        {
            if (dgvProductos.RowCount > 0)
            {
                decimal total = 0m;

                foreach (var item in _itemsVenta)
                {
                    decimal subtotal;

                        subtotal = item.PrecioVenta * item.Cantidad;

                    total += subtotal;
                }

                _subTotalVenta = total;
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

        private void ResetearGrilla(DataGridView grilla)
        {
            grilla.Columns.Clear();
            grilla.AutoGenerateColumns = false;

            // 🔒 ID interno (oculto)
            // 📦 Producto
            grilla.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Descripcion",
                HeaderText = "Producto",
                DataPropertyName = "Descripcion",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            });

            // 🔢 Cantidad
            grilla.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Cantidad",
                HeaderText = "Cantidad",
                DataPropertyName = "Cantidad",
                Width = 120
            });

            // 💲 Precio normal
            grilla.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "PrecioVenta",
                HeaderText = "Precio",
                DataPropertyName = "PrecioVenta",
                Width = 150,
                DefaultCellStyle = { Format = "C2" }
            });

            grilla.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Subtotal",
                HeaderText = "Subtotal",
                DataPropertyName = "Subtotal",
                Width = 150,
                DefaultCellStyle = { Format = "C2" }
            });

            // ✏️ Botón editar
            grilla.Columns.Add(new DataGridViewButtonColumn
            {
                Name = "BtnEditar",
                HeaderText = "Editar",
                Text = "✏ Editar",
                UseColumnTextForButtonValue = true,
                Width = 130
            });

            // 🗑 Botón eliminar
            grilla.Columns.Add(new DataGridViewButtonColumn
            {
                Name = "BtnEliminar",
                HeaderText = "Eliminar",
                Text = "🗑 Eliminar",
                UseColumnTextForButtonValue = true,
                Width = 130
            });
        }


        private void dgvProductos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;

            var fila = dgvProductos.Rows[e.RowIndex];
            if (fila == null || fila.IsNewRow) return;

            // Aquí tu código seguro para manejar el click
        }

        private void dgvProductos_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvProductos.CurrentRow == null) return;
            if (dgvProductos.CurrentRow.IsNewRow) return;

            if (!dgvProductos.Columns.Contains("ItemId")) return;

            var val = dgvProductos.CurrentRow.Cells["ItemId"].Value;
            if (val == null || val == DBNull.Value) return;

            // Lógica segura con val
        }

        private void dgvProductos_RowEnter(object sender, DataGridViewCellEventArgs e)
        {

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

        private void btnCargarCliente_Click(object sender, EventArgs e)
        {
            var cliente = new FClienteConsulta(true);

            if (cliente.ShowDialog() == DialogResult.OK && cliente.clienteSeleccionado.HasValue)
            {
                idCliente = cliente.clienteSeleccionado.Value;

                var _clienteCargado = new ClienteServicio().ObtenerClientePorId(idCliente);

                _clienteVenta = new ClienteDTO
                {
                    PersonaId = _clienteCargado.PersonaId,
                    Nombre = _clienteCargado.Nombre,
                    Apellido = _clienteCargado.Apellido
                };

                esConsumidorFinal = false;
                cbxConsumidorFinal.Checked = false;

                ActualizarCamposInicio(); 
            }
        }

        private void cbxConsumidorFinal_CheckedChanged(object sender, EventArgs e)
        {
            esConsumidorFinal = cbxConsumidorFinal.Checked;

            btnCargarCliente.Enabled = !esConsumidorFinal;

            if (esConsumidorFinal)
            {
               
                var clienteDefault = _clienteServicio.ObtenerClientePorNumero("0");

                _clienteVenta = new ClienteDTO
                {
                    PersonaId = clienteDefault.PersonaId,
                    Nombre = clienteDefault.Nombre,
                    Apellido = clienteDefault.Apellido
                };
            }

            ActualizarCamposInicio();
        }

        private void cbxIncluirCtaCte_CheckedChanged(object sender, EventArgs e)
        {
            _incluirCtaCte = cbxIncluirCtaCte.Checked;
            ActualizarCamposInicio();
        }


        private bool _suspendCbxDesc = false;

        private void cbxDescEfectivo_CheckedChanged(object sender, EventArgs e)
        {
            if (_suspendCbxDesc) return;

            ValidarCantidadySiEsOferta();

            txtDescuentoEfectivo.Enabled = cbxDescEfectivo.Checked;
        }

        private void ValidarCantidadySiEsOferta()
        {
            if (_itemsVenta.Count < 1)
            {
                _suspendCbxDesc = true;
                cbxDescEfectivo.Checked = false;
                _suspendCbxDesc = false;
                MessageBox.Show("Debe cargar al menos un producto para aplicar un descuento.");
                return;
            }

        }

        private void btnConfirmarYFPago_Click(object sender, EventArgs e)
        {
            if (_totalVenta == 0)
            {
                MessageBox.Show("Debe cargar al menos un producto antes de confirmar la venta.");
                return;
            }
            if (!DatosSistema.CajaId.HasValue)
            {
                MessageBox.Show("No hay una caja abierta. No se puede registrar la venta.");
                return;
            }
            // Si ya se confirmó antes, registrar la venta directamente
            if (finalizarVenta)
            {
                FinalizacionVenta();
                return;
            }

            DatosVenta datosVenta = new DatosVenta
            {
                Total = _totalVenta,
                IncluirCtaCte = _incluirCtaCte,
                DescuentoEfectivo = cbxDescEfectivo.Checked
            };

            // Primer paso: elegir 1 pago / múltiples
            var fSeleccionCantidad = new FSeleccionCantidadPagos();
            if (fSeleccionCantidad.ShowDialog() != DialogResult.OK)
                return; // usuario canceló

            bool esMultiples = fSeleccionCantidad.multiplePagos;
            int cantidadPagos = fSeleccionCantidad.CantidadPagos; // en 1-pago será 1

            // --- caso: múltiples pagos -> abrir FPagoMultiple para ingresar montos y formas ---
            if (esMultiples)
            {
                using var fPagoMultiple = new FPagoMultiple(cantidadPagos, _totalVenta, datosVenta, idCliente);
                var drMulti = fPagoMultiple.ShowDialog();
                if (drMulti != DialogResult.OK)
                {
                    // usuario canceló el ingreso de múltiples pagos -> volvemos al flujo
                    return;
                }

                var pagosSeleccionados = fPagoMultiple.ResultPagos;
                if (pagosSeleccionados == null || pagosSeleccionados.Count == 0)
                {
                    MessageBox.Show("Debe ingresar al menos un pago válido.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Asignar a los objetos usados por el resto del formulario
                tipoDePagosVenta = pagosSeleccionados;
                _cuerpoDetalleVenta.tiposDePago = pagosSeleccionados;

                // Calcular pendiente
                var suma = pagosSeleccionados.Sum(p => p.Monto);
                _cuerpoDetalleVenta.saldoPendiente = _totalVenta - suma;
                _cuerpoDetalleVenta.pagoParcial = _cuerpoDetalleVenta.saldoPendiente > 0m;
                txtAreaDetallesVenta.Text = _cuerpoDetalleVenta.CuerpoDelTextoTP();

                GenerarDetalleVenta(tipoDePagosVenta);
                return;
            }
            // 1 pago

            var fConfirmarVenta = new FConfirmacionVenta(datosVenta, idCliente)
            {
                PermitirMultiplesPagos = false
            };

            if (fConfirmarVenta.ShowDialog() == DialogResult.OK)
            {
                var tipoPagosSeleccionados = fConfirmarVenta.pagos;
                if (tipoPagosSeleccionados == null || tipoPagosSeleccionados.Count == 0)
                {
                    MessageBox.Show("Debe seleccionar al menos un tipo de pago.");
                    return;
                }

                _cuerpoDetalleVenta.tiposDePago = tipoPagosSeleccionados;
                tipoDePagosVenta = tipoPagosSeleccionados;

                _cuerpoDetalleVenta.saldoPendiente = fConfirmarVenta.MontoPendiente;

                if (fConfirmarVenta.MontoPendiente > 0.00m)
                {
                    _cuerpoDetalleVenta.pagoParcial = true;
                }

                txtAreaDetallesVenta.Text = _cuerpoDetalleVenta.CuerpoDelTextoTP();

                GenerarDetalleVenta(tipoPagosSeleccionados);
            }
        }
        private void FinalizacionVenta()
        {
            if (!finalizarVenta) return;

            this.DialogResult = DialogResult.OK;

            _ventaLibreDto = new VentaLibreDTO()
            {
                NumeroVenta = lblNro.Text,
                IdEmpleado = _usuarioLogeadoID,
                IdVendedor = idVendedor,

                IdCliente = esConsumidorFinal
                    ? (long?)_clienteVenta.PersonaId
                    : idCliente,

                ClienteNombreCompleto = $"{_clienteVenta.Nombre} {_clienteVenta.Apellido}",

                EmpleadoNombreCompleto = $"{_usuarioLogeado.Nombre} {_usuarioLogeado.Apellido}",
                VendedorNombreCompleto = $"{_usuarioLogeado.Nombre} {_usuarioLogeado.Apellido}",

                FechaVenta = DateTime.Now,
                Total = _totalVenta,

                Estado = 1, // o el enum que uses

                Detalle = Convert.ToString(
                    _cuerpoDetalleVenta.CuerpoDelTextoFinal(descripcionVenta)
                ),

                MontoPagado = tipoDePagosVenta.Sum(x => x.Monto),
                MontoAdeudado = _totalVenta - tipoDePagosVenta.Sum(x => x.Monto),
                TiposDePagoSeleccionado = tipoDePagosVenta,

            };
            _ventaLibreDto.TiposDePagoSeleccionado.ForEach(tp =>
            {
                if (tp.TipoDePago == TipoDePago.CtaCte)
                {
                    var ctaCteServicio = new CuentaCorrienteServicio();
                    var ctacte = ctaCteServicio.ObtenerCuentaCorrientePorClienteId(idCliente);

                    if (ctacte != null)
                    {
                        ctaCteServicio.RegistrarCompra(ctacte.CuentaCorrienteId, tp.Monto, DatosSistema.CajaId.Value);
                    }
                }
            });

            foreach (var tp in tipoDePagosVenta)
            {
                if (tp.TipoDePago == TipoDePago.CtaCte)
                {
                    var ctaCteServicio = new CuentaCorrienteServicio();
                    var ctacte = ctaCteServicio.ObtenerCuentaCorrientePorClienteId(idCliente);

                    if (ctacte != null)
                    {
                        ctaCteServicio.RegistrarCompra(
                            ctacte.CuentaCorrienteId,
                            tp.Monto,
                            DatosSistema.CajaId.Value
                        );
                    }
                }
            }

            // 🔥 Guardar venta
            var resultado = _ventaLibreServicio.NuevaVentaLibre(_ventaLibreDto);

            if (resultado.Exitoso)
            {
                MessageBox.Show("Venta confirmada exitosamente.", "Éxito",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show($"Error al finalizar la venta: {resultado.Mensaje}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            this.Close();
        }
        private void GenerarDetalleVenta(List<FormaPago> tipoPagosSeleccionados)
        {
            if (tipoPagosSeleccionados.Count > 0)
            {
                var fConfirmarDetalle = new FDetalleVenta();
                if (fConfirmarDetalle.ShowDialog() == DialogResult.OK)
                {
                    if (fConfirmarDetalle.confirmarDetalle)
                    {
                        btnConfirmarYFPago.Text = "Finalizar";
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
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
                InicializarYLimpiarCampos();
        }
    }
}
