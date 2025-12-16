using AccesoDatos.Entidades;
using Microsoft.IdentityModel.Tokens;
using Presentacion.AccesoAlSistema;
using Presentacion.Core.Cliente;
using Presentacion.Core.Empleado;
using Presentacion.Core.Oferta;
using Presentacion.Core.Producto;
using Servicios.Helpers;
using Servicios.LogicaNegocio.Cliente;
using Servicios.LogicaNegocio.Cliente.DTO;
using Servicios.LogicaNegocio.CuentaCorriente;
using Servicios.LogicaNegocio.Empleado;
using Servicios.LogicaNegocio.Movimiento;
using Servicios.LogicaNegocio.Producto;
using Servicios.LogicaNegocio.Venta;
using Servicios.LogicaNegocio.Venta.DTO;
using Servicios.LogicaNegocio.Venta.Oferta;
using System.ComponentModel;

namespace Presentacion.Core.Venta
{
    public partial class FVenta : FBase.FBase
    {
        private readonly IVentaServicio _ventaServicio;
        private readonly IEmpleadoServicio _empleadoServicio;
        private readonly IOfertaServicio _ofertaServicio;
        private readonly IMovimientoServicio _movimientoServicio;

        private long idVendedor = 0;
        private VentaDTO _venta;
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
        private List<FormaPago> tipoDePagosVenta;
        private bool _actualizandoGrilla = false;
        private bool cargarOferta = false;
        private long idCliente = -1;


        public FVenta(long usuarioLogeadoID)
        {
            InitializeComponent();
            _ventaServicio = new CajaServicio();

            _movimientoServicio = new MovimientoServicio();

            _empleadoServicio = new EmpleadoServicio();
            _cuerpoDetalleVenta = new CuerpoDetalleVenta
            {
                tiposDePago = new List<FormaPago>(),
                pagoParcial = false,
                saldoPendiente = 0.00m,
            };
            _ofertaServicio = new OfertaServicio();
            _usuarioLogeadoID = usuarioLogeadoID;

            // Inicializamos itemsVenta como BindingList
            itemsVenta = new BindingList<ItemVentaDTO>();
        }

        private void FVenta_Load(object sender, EventArgs e)
        {
            _ventaServicio.GenerateNextNumeroVenta();
            lblNro.Text = _ventaServicio.GenerateNextNumeroVenta().ToString();
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
            idVendedor = _usuarioLogeadoID; // Asignamos el ID del usuario logueado como vendedor por defecto
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
            if(idCliente < 0)
            {
                cbxIncluirCtaCte.Checked = false;
                cbxIncluirCtaCte.Enabled = false;
            }
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
            if (esConsumidorFinal)
            {
                cbxIncluirCtaCte.Checked = false;
                cbxIncluirCtaCte.Enabled = false;
            }
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
                    idVendedor = vendedor.PersonaId;

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

            // Si ya se confirmó antes, registrar la venta directamente
            if (finalizarVenta)
            {
                this.DialogResult = DialogResult.OK;
                _venta = new VentaDTO
                {
                    NumeroVenta = lblNro.Text,
                    IdEmpleado = _usuarioLogeadoID,
                    IdVendedor = idVendedor,
                    FechaVenta = DateTime.Now,
                    Total = _totalVenta,
                    TotalSinDescuento = _totalVenta, // actualizar cuando maneje descuentos
                    Descuento = _porcentajeDescuento,
                    Detalle = Convert.ToString(_cuerpoDetalleVenta.CuerpoDelTextoFinal(descripcionVenta)),
                    Items = itemsVenta.ToList(),
                    TiposDePagoSeleccionado = tipoDePagosVenta,
                };

                // Impactar cuenta corriente si corresponde
                _venta.TiposDePagoSeleccionado.ForEach(tp =>
                {
                    if (tp.TipoDePago == TipoDePago.CtaCte)
                    {
                        var ctaCteServicio = new CuentaCorrienteServicio();
                        var ctacte = ctaCteServicio.ObtenerCuentaCorrientePorClienteId(idCliente);

                        if (ctacte != null)
                        {
                            ctaCteServicio.RegistrarCompra(ctacte.CuentaCorrienteId, tp.Monto);
                        }
                    }
                });
                //Deberia juntar movimiento ctacte con movimiento venta en uno solo.

                _ventaServicio.NuevaVenta(_venta);
                //_movimientoServicio.CrearMovimientoVenta(_venta);

                MessageBox.Show("Venta confirmada exitosamente.");
                this.Close();
                return;
            }

            // Primer paso: elegir 1 pago / múltiples
            var fSeleccionCantidad = new FSeleccionCantidadPagos();
            if (fSeleccionCantidad.ShowDialog() != DialogResult.OK)
                return; // usuario canceló

            bool esMultiples = fSeleccionCantidad.multiplePagos;
            int cantidadPagos = fSeleccionCantidad.CantidadPagos; // en 1-pago será 1

            DatosVenta datosVenta = new DatosVenta
            {
                Total = _totalVenta,
                IncluirCtaCte = _incluirCtaCte
            };

            // --- caso: múltiples pagos -> abrir FPagoMultiple para ingresar montos y formas ---
            if (esMultiples)
            {
                using var fPagoMultiple = new FPagoMultiple(cantidadPagos, _totalVenta, _incluirCtaCte, idCliente);
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

                // Seguir con detalle final (igual que antes)
                if (tipoDePagosVenta.Count > 0)
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

                // seguir con el detalle final (igual que antes)
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
            if (!cargarOferta)
            {
                var fProductos = new FProductoConsulta(true);

                if (fProductos.ShowDialog() == DialogResult.OK && fProductos.productoSeleccionado.HasValue)
                {

                    var idProducto = fProductos.productoSeleccionado.Value;
                    var cantidad = 0.0m;

                    //var producto = new ProductoServicio().ObtenerProductoPorId(idProducto);
                    var ofertaDesc = new ProductoServicio().ControlarProductoEstaEnOfertaPorId(idProducto);
                    if (ofertaDesc == null) return;
                    var esOF = false;
                    if (ofertaDesc.Oferta != null) { esOF = true; } else { esOF = false;}
                    var fCantidad = new FCantidadItem();

                    if (fCantidad.ShowDialog() == DialogResult.OK && fCantidad.cantidad > 0)
                    {
                        if(fCantidad.cantidad > ofertaDesc.Producto.Stock)
                        {
                            cantidad = ofertaDesc.Producto.Stock;
                            MessageBox.Show($"La cantidad solicitada supera el stock disponible. Por lo que se pondra la cantidad máxima disponible: {cantidad}.");
                        }
                        else
                        {
                            cantidad = fCantidad.cantidad;
                        }
                        var itemVenta = new ItemVentaDTO
                        {
                            ItemId = ofertaDesc.Producto.ProductoId,
                            Descripcion = ofertaDesc.Producto.Descripcion,
                            PrecioVenta = ofertaDesc.Producto.PrecioVenta,
                            PrecioOferta = ofertaDesc.PrecioEnOferta,
                            Cantidad = cantidad,
                            Medida = ofertaDesc.Producto.Medida,
                            UnidadMedida = ofertaDesc.Producto.UnidadMedida,
                            EsOferta = esOF
                        };

                        itemsVenta.Add(itemVenta);  // Solo agregamos a la BindingList
                        txtProductoCargado.Text = $"{itemVenta.Descripcion}";
                        // No necesitas reasignar DataSource ni resetear grilla acá
                        CalcularTotal();
                    }
                }
            }
            else
            {
                var Fofertas = new FOfertaConsulta(true);

                if (Fofertas.ShowDialog() == DialogResult.OK && Fofertas.ofertaSeleccionada.HasValue)
                {
                    var idOferta = Fofertas.ofertaSeleccionada.Value;

                    var Oferta = _ofertaServicio.ObtenerOfertaPorId(idOferta);
                    var OfertaVenta = new ItemVentaDTO
                    {
                        ItemId = Oferta.OfertaDescuentoId,
                        Descripcion = Oferta.Descripcion,
                        PrecioVenta = Oferta.PrecioOriginal,
                        PrecioOferta = Oferta.PrecioFinal,
                        Cantidad = (decimal)Oferta.CantidadProductosDentroOferta,
                        Medida = string.Empty,
                        UnidadMedida = string.Empty,
                        EsOferta = true
                    };

                    itemsVenta.Add(OfertaVenta);  // Solo agregamos a la BindingList
                    txtProductoCargado.Text = $"{OfertaVenta.Descripcion}";
                    // No necesitas reasignar DataSource ni resetear grilla acá
                    CalcularTotal();

                }

            }
        }
        private void CalcularTotal()
        {
            if (dgvProductos.RowCount > 0)
            {
                decimal total = 0m;

                foreach (var item in itemsVenta)
                {
                    decimal subtotal;

                    if (item.EsOferta)
                    {
                        // Caso oferta → el precio ya es total de la oferta, no depende de Cantidad
                        subtotal = item.PrecioOferta;
                    }
                    else
                    {
                        // Caso producto normal → precio * cantidad
                        subtotal = item.PrecioVenta * item.Cantidad;
                    }

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

            grilla.Columns["PrecioOferta"].Visible = true;
            grilla.Columns["PrecioOferta"].Width = 100;
            grilla.Columns["PrecioOferta"].DisplayIndex = 5;


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

        private void cbxEnOferta_CheckedChanged(object sender, EventArgs e)
        {
            cargarOferta = cbxEnOferta.Checked;
            btnCargarProducto.Text = cargarOferta ? "Cargar Oferta" : "Cargar Producto";
        }

        private void btnCargarCliente_Click(object sender, EventArgs e)
        {
            var cliente = new FClienteConsulta(true);
            if (cliente.ShowDialog() == DialogResult.OK && cliente.clienteSeleccionado.HasValue)
            {
                idCliente = cliente.clienteSeleccionado.Value;
                var _clienteCargado = new ClienteServicio().ObtenerClientePorId(idCliente);
                txtCliente.Text = $"{_clienteCargado.Nombre} {_clienteCargado.Apellido}";
                cbxIncluirCtaCte.Enabled = true;
                cbxIncluirCtaCte.Checked = true;
            }
        }
    }
}
