using AccesoDatos.Entidades;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Presentacion.AccesoAlSistema;
using Presentacion.Core.Cliente;
using Presentacion.Core.Empleado;
using Presentacion.Core.Oferta;
using Presentacion.Core.Producto;
using Presentacion.FBase.Helpers;
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
using System.Globalization;

namespace Presentacion.Core.Venta
{
    public partial class FVenta : FBase.FBase
    {
        private readonly IVentaServicio _ventaServicio;
        private readonly IEmpleadoServicio _empleadoServicio;
        private readonly IOfertaServicio _ofertaServicio;
        private readonly IMovimientoServicio _movimientoServicio;
        private readonly IClienteServicio _clienteServicio;


        private long? VENTAID = null;
        private VentaDTO VENTAELIMINAR;
        private long idVendedor = 0;
        private VentaDTO _venta;
        private bool finalizarVenta = false;
        private CuerpoDetalleVenta _cuerpoDetalleVenta;
        private readonly long _usuarioLogeadoID;
        private UsuarioLogeado _usuarioLogeado;
        private ClienteDTO _clienteVenta;
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
        private long idCliente;


        public FVenta(long UsuarioLogeadoId, long? VentaId = null)
        {
            InitializeComponent();
            _ventaServicio = new VentaServicio();
            _movimientoServicio = new MovimientoServicio();
            _usuarioLogeadoID = UsuarioLogeadoId;
            _empleadoServicio = new EmpleadoServicio();
            _ofertaServicio = new OfertaServicio();
            _clienteServicio = new ClienteServicio();

            VENTAID = VentaId;

            if (VentaId != null)
            {
                var ventaCargada = _ventaServicio.ObtenerVentaDetalle((long)VENTAID);

                if (ventaCargada == null)
                {
                    MessageBox.Show("No se pudo cargar la venta.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                VENTAELIMINAR = new VentaDTO
                {
                    VentaId = ventaCargada.VentaId,
                    NumeroVenta = ventaCargada.NumeroVenta,
                    IdEmpleado = ventaCargada.IdEmpleado,
                    IdVendedor = ventaCargada.IdVendedor,
                    IdCliente = ventaCargada.IdCliente,
                    FechaVenta = ventaCargada.FechaVenta,
                    Total = ventaCargada.Total,
                    TotalSinDescuento = ventaCargada.TotalSinDescuento,
                    Descuento = ventaCargada.Descuento,
                    Estado = 0, // q pingo hacemo aca
                    Detalle = ventaCargada.Detalle,
                    TiposDePagoSeleccionado = ventaCargada.TiposDePagoSeleccionado
                        .Select(p => new FormaPago
                        {
                            TipoDePago = p.TipoDePago,
                            Monto = p.Monto
                        }).ToList(),
                    Items = ventaCargada.Items
                        .Select(i => new ItemVentaDTO
                        {
                            ItemId = i.ItemId,
                            Cantidad = i.Cantidad,
                            PrecioVenta = i.PrecioVenta,
                            Descripcion = i.Descripcion
                        }).ToList()
                };
                itemsVenta = new BindingList<ItemVentaDTO>(ventaCargada.Items);

            }
            else
            {

                _cuerpoDetalleVenta = new CuerpoDetalleVenta
                {
                    tiposDePago = new List<FormaPago>(),
                    pagoParcial = false,
                    saldoPendiente = 0.00m,
                };
                itemsVenta = new BindingList<ItemVentaDTO>();
            }
        }

        private void FVenta_Load(object sender, EventArgs e)
        {
            InicializarYLimpiarCampos(VENTAID);
            dgvProductos.CellFormatting += dgvProductos_CellFormatting;
        }

        private void MyTimer_Tick(object sender, EventArgs e)
        {
            var ahora = DateTime.Now;
            var cultura = new CultureInfo("es-ES");

            lblFechaHoy.Text = ahora.ToString("dd/MM/yyyy");
            lblHoraMinutos.Text = ahora.ToString("HH:mm:ss");
            lblDiaAbrev.Text = cultura.TextInfo.ToTitleCase(ahora.ToString("ddd", cultura)) + ".";

        }

        private void ActualizarCamposInicio(long? ventaId)
        {
            if (ventaId != null)
            {
                var vendedor = _empleadoServicio.ObtenerEmpleadoPorId(VENTAELIMINAR.IdVendedor);
                txtCliente.Text = _clienteVenta != null ? $"{_clienteVenta.Nombre} {_clienteVenta.Apellido}" : "Consumidor Final";
                lblVendedorAsignado.Text = $"{vendedor.Nombre} {vendedor.Apellido}";
            }
            else
            {
                txtCliente.Text = $"{_clienteVenta.Nombre} {_clienteVenta.Apellido}";
                lblVendedorAsignado.Text = esUsuarioLogeado
                    ? $"{_usuarioLogeado.Nombre} {_usuarioLogeado.Apellido}"
                    : "";
            }
            if (esConsumidorFinal)
            {
                cbxIncluirCtaCte.Checked = false;
                cbxIncluirCtaCte.Enabled = false;
            }
            if (_usuarioLogeado != null)
            {
                lblVendedorAsignado.Text = $"{_usuarioLogeado.Nombre} {_usuarioLogeado.Apellido}";
            }
        }


        private void cbxUsuarioLogeado_CheckedChanged(object sender, EventArgs e)
        {
            esUsuarioLogeado = !esUsuarioLogeado;
            ActualizarCamposInicio(VENTAID);
            btnCambiarVendedor.Enabled = !esUsuarioLogeado;
        }

        private void cbxConsumidorFinal_CheckedChanged(object sender, EventArgs e)
        {
            esConsumidorFinal = !esConsumidorFinal;
            ActualizarCamposInicio(VENTAID);
            btnCargarCliente.Enabled = !esConsumidorFinal;
        }

        private void btnConfirmarYFPago_Click(object sender, EventArgs e)
        {
            if (DatosSistema.CajaId.HasValue) //Verificamos antes que nada que tengamos una caja abierta
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
                        IdCliente = VENTAID != null ? (long?)idCliente : _clienteVenta.PersonaId,
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

                    var m = _ventaServicio.NuevaVenta(_venta);
                    MessageBox.Show($"{m.Mensaje}");
                    //_movimientoServicio.CrearMovimientoVenta(_venta);


                    //MessageBox.Show("Venta confirmada exitosamente.");
                    this.Close();
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
            else
            {
                MessageBox.Show("No hay una caja abierta. No se puede registrar la venta.");
            }

            
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

        private void cbxIncluirCtaCte_CheckedChanged(object sender, EventArgs e)
        {
            _incluirCtaCte = cbxIncluirCtaCte.Checked;
            ActualizarCamposInicio(VENTAID);
        }

        private void btnCargarProducto_Click(object sender, EventArgs e)
        {
            var fProductos = new FProductoConsulta(true);

            if (fProductos.ShowDialog() == DialogResult.OK && fProductos.productoSeleccionado.HasValue)
            {

                var idProducto = fProductos.productoSeleccionado.Value;
                var cantidad = 0.0m;

                //var producto = new ProductoServicio().ObtenerProductoPorId(idProducto);
                var ofertaDesc = new ProductoServicio().ControlarProductoEstaEnOfertaPorId(idProducto);
                if (ofertaDesc == null)
                {
                    MessageBox.Show("El producto seleccionado no está disponible.");
                    return;
                }
                var esOF = false;
                if (ofertaDesc.Oferta != null) { esOF = true; } else { esOF = false; }
                var fCantidad = new FCantidadItem();

                if (fCantidad.ShowDialog() == DialogResult.OK && fCantidad.cantidad > 0)
                {
                    if (fCantidad.cantidad > ofertaDesc.Producto.Stock)
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
                                                //txtProductoCargado.Text = $"{itemVenta.Descripcion}";
                                                // No necesitas reasignar DataSource ni resetear grilla acá
                    if (cbxDescEfectivo.Checked)
                    {
                        ValidarCantidadySiEsOferta();
                        AplicarDescuentoEfectivo();
                    }
                    CalcularTotal();
                }
            }
        }
        private void CalcularTotal()
        {
            //replantar oferta en gral
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
            grilla.Columns.Clear();
            grilla.AutoGenerateColumns = false;

            // 🔒 ID interno (oculto)
            grilla.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "ItemId",
                DataPropertyName = "ItemId",
                Visible = false
            });

            // 📦 Producto
            grilla.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Descripcion",
                HeaderText = "Producto",
                DataPropertyName = "Descripcion",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            });

            // 🔖 Estado oferta (string en el DTO)
            grilla.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "EstadoOferta",
                HeaderText = "Oferta",
                DataPropertyName = "EstadoOferta",
                Width = 180
            });

            // 📏 Medida
            grilla.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Medida",
                HeaderText = "Medida",
                DataPropertyName = "Medida",
                Width = 100
            });

            // 📐 Unidad de medida
            grilla.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "UnidadMedida",
                HeaderText = "Unidad",
                DataPropertyName = "UnidadMedida",
                Width = 120
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

            // 🔥 Precio oferta
            grilla.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "PrecioOferta",
                HeaderText = "Precio Oferta",
                DataPropertyName = "PrecioOferta",
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

        /*private void cbxEnOferta_CheckedChanged(object sender, EventArgs e)
        {
            cargarOferta = cbxEnOferta.Checked;
            btnCargarProducto.Text = cargarOferta ? "Cargar Oferta" : "Cargar Producto";
        }*/

        private void btnCargarCliente_Click(object sender, EventArgs e)
        {
            var cliente = new FClienteConsulta(true);
            if (cliente.ShowDialog() == DialogResult.OK && cliente.clienteSeleccionado.HasValue)
            {
                idCliente = cliente.clienteSeleccionado.Value;
                var _clienteCargado = new ClienteServicio().ObtenerClientePorId(idCliente);
                _clienteVenta = new ClienteDTO
                {
                    Nombre = _clienteCargado.Nombre,
                    Apellido = _clienteCargado.Apellido
                };
                cbxIncluirCtaCte.Enabled = true;
                cbxIncluirCtaCte.Checked = true;
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            if (VENTAID != null)
            {
                var result = MessageBox.Show(
                    "¿Está seguro que desea cancelar esta venta?\n\nEsta acción no se puede deshacer.",
                    "Confirmar cancelación",
                    MessageBoxButtons.OKCancel,
                    MessageBoxIcon.Warning
                );

                if (result == DialogResult.OK)
                {
                    CancelarVenta(VENTAID);
                }
            }
            else
            {
                InicializarYLimpiarCampos(VENTAID);

            }

        }

        private void CancelarVenta(long? VenId)
        {
            var respuesta = _ventaServicio.CancelacionVentaPorId((long)VenId);
            MessageBox.Show(respuesta.Mensaje);
        }

        private void InicializarYLimpiarCampos(long? ventaId)
        {

            if (ventaId != null)
            {
                itemsVenta = new BindingList<ItemVentaDTO>(VENTAELIMINAR.Items);
                dgvProductos.DataSource = itemsVenta;
                var usuarioLogeado = _empleadoServicio.ObtenerEmpleadoPorId(VENTAELIMINAR.IdEmpleado);
                if (VENTAELIMINAR.IdCliente != null)
                {
                    var cid = (long)VENTAELIMINAR.IdCliente;
                    var clienteVenta = _clienteServicio.ObtenerClientePorId(cid);
                    _clienteVenta = new ClienteDTO
                    {
                        Nombre = clienteVenta.Nombre,
                        Apellido = clienteVenta.Apellido
                    };

                }
                _usuarioLogeado = new UsuarioLogeado
                {
                    PersonaId = usuarioLogeado.PersonaId,
                    Username = usuarioLogeado.Username,
                    Nombre = usuarioLogeado.Nombre,
                    Apellido = usuarioLogeado.Apellido,
                };
                //cbxConsumidorFinal.Checked = true;
                //esConsumidorFinal = true;
                //esUsuarioLogeado = true;
                idVendedor = VENTAELIMINAR.IdVendedor; // Asignamos el ID del usuario logueado como vendedor por defecto
                ResetearGrilla(dgvProductos);
                ActualizarCamposInicio(VENTAID);
                txtTotal.Text = VENTAELIMINAR.Total.ToString("C2");
                txtSubtotal.Text = VENTAELIMINAR.TotalSinDescuento.ToString("C2");
                txtDescuentoEfectivo.Text = VENTAELIMINAR.TotalSinDescuento.ToString("C2");
                lblNro.Text = VENTAELIMINAR.NumeroVenta;
                txtAreaDetallesVenta.Text = VENTAELIMINAR.Detalle;
                //cbxIncluirCtaCte.Checked = true;
                dgvProductos.AllowUserToAddRows = false;
                //cbxDescEfectivo.Checked = false;
                cbxIncluirCtaCte.Checked = false;
                cbxIncluirCtaCte.Enabled = false;
                lblFechaHoy.Text = VENTAELIMINAR.FechaVenta.ToString("dd/MM/yyyy");
                lblDiaAbrev.Text = (VENTAELIMINAR.FechaVenta.ToString("ddd")).ToUpper() + ".";
                lblHoraMinutos.Text = VENTAELIMINAR.FechaVenta.ToString("HH:mm:ss");
                btnLimpiar.Text = "Contrasiento";
                if (VENTAELIMINAR.IdCliente != null)
                {
                    txtCliente.Text = $"{_clienteVenta.Nombre} {_clienteVenta.Apellido}";

                }
                dgvProductos.Enabled = false;
                btnConfirmarYFPago.Enabled = false;
                btnConfirmarYFPago.Visible = false;
                txtAreaDetallesVenta.Enabled = false;
                txtSubtotal.Enabled = false;
                txtTotal.Enabled = false;
                cbxConsumidorFinal.Enabled = false;
                cbxConsumidorFinal.Visible = false;
                cbxDescEfectivo.Enabled = false;
                cbxDescEfectivo.Visible = false;
                btnCargarCliente.Enabled = false;
                btnCargarCliente.Text = "Cliente";
                //btnCargarCliente.Visible = false;
                btnCargarOferta.Enabled = false;
                btnCargarOferta.Visible = false;
                btnCargarProducto.Enabled = false;
                btnCargarProducto.Visible = false;
                btnCambiarVendedor.Enabled = false;
                btnCambiarVendedor.Visible = false;

            }
            else
            {
                var clienteDefault = _clienteServicio.ObtenerClientePorNumero("0");
                _clienteVenta = new ClienteDTO
                {
                    PersonaId = clienteDefault.PersonaId,
                    Nombre = clienteDefault.Nombre,
                    Apellido = clienteDefault.Apellido
                };
                itemsVenta = new BindingList<ItemVentaDTO>();
                dgvProductos.DataSource = itemsVenta;  // bind directo
                ResetearGrilla(dgvProductos);
                ActualizarCamposInicio(VENTAID);
                CalcularTotal();
                //_ventaServicio.GenerateNextNumeroVenta();
                //lblNro.Text = _ventaServicio.GenerateNextNumeroVenta().ToString();
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
                cbxConsumidorFinal.Checked = true;
                esConsumidorFinal = true;
                esUsuarioLogeado = true;
                idVendedor = _usuarioLogeadoID; // Asignamos el ID del usuario logueado como vendedor por defecto
                btnCargarCliente.Enabled = false;
                cbxIncluirCtaCte.Checked = true;
                dgvProductos.AllowUserToAddRows = false;
                cbxDescEfectivo.Checked = false;
                txtSubtotal.Text = _subTotalVenta.ToString("C2");
                txtTotal.Text = _totalVenta.ToString("C2");
                if (idCliente < 0)
                {
                    cbxIncluirCtaCte.Checked = false;
                    cbxIncluirCtaCte.Enabled = false;
                }
            }
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
            if (itemsVenta.Count < 1)
            {
                _suspendCbxDesc = true;
                cbxDescEfectivo.Checked = false;               // no volverá a ejecutar la lógica porque la bandera lo evita
                _suspendCbxDesc = false;
                MessageBox.Show("Debe cargar al menos un producto para aplicar un descuento.");
                return;
            }

            if (itemsVenta.Any(iv => iv.EsOferta))
            {
                _suspendCbxDesc = true;
                cbxDescEfectivo.Checked = false;
                txtDescuentoEfectivo.Text = string.Empty;
                txtDescuentoEfectivo.Enabled = false;
                _suspendCbxDesc = false;
                MessageBox.Show("No se puede aplicar descuento por efectivo cuando hay ofertas en la venta.");
                return;
            }
        }

        private void txtDescuentoEfectivo_TextChanged(object sender, EventArgs e)
        {
            AplicarDescuentoEfectivo();
        }
        private void AplicarDescuentoEfectivo()
        {
            if (!decimal.TryParse(txtDescuentoEfectivo.Text, out decimal porcentajeDesc))
                return;

            if (porcentajeDesc <= 0 || porcentajeDesc > 100)
                return;

            decimal totalVenta = itemsVenta.Sum(i => i.PrecioVenta * i.Cantidad); // o la propiedad que uses
            decimal descuento = totalVenta * (porcentajeDesc / 100m);
            decimal totalConDescuento = totalVenta - descuento;

            txtTotal.Text = totalConDescuento.ToString("N2");
            _totalVenta = totalConDescuento;

        }

        private void btnCambiarVendedor_Click(object sender, EventArgs e)
        {
            var fEmpleado = new FEmpleadoConsulta(true);

            if (fEmpleado.ShowDialog() == DialogResult.OK && fEmpleado.empleadoSeleccionado.HasValue)
            {
                var idEmpleado = fEmpleado.empleadoSeleccionado.Value;

                var vendedor = new EmpleadoServicio().ObtenerEmpleadoPorId(idEmpleado);
                idVendedor = vendedor.PersonaId;

                lblVendedorAsignado.Text = $"{vendedor.Nombre} {vendedor.Apellido}";

            }
        }

        private void btnCargarOferta_Click(object sender, EventArgs e)
        {
            var Fofertas = new FOfertaConsulta(true);

            if (Fofertas.ShowDialog() == DialogResult.OK && Fofertas.ofertaSeleccionada.HasValue)
            {
                var idOferta = Fofertas.ofertaSeleccionada.Value;

                var Oferta = _ofertaServicio.ObtenerOfertaActivaPorId(idOferta);
                if (Oferta == null)
                {
                    MessageBox.Show("La oferta seleccionada esta Inactiva. Si se trata de un error comunicarse con un Administrador para activarla.");

                }
                else
                {

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
                                                  //txtProductoCargado.Text = $"{OfertaVenta.Descripcion}";
                                                  // No necesitas reasignar DataSource ni resetear grilla acá
                    ValidarCantidadySiEsOferta();
                    CalcularTotal();
                }
            }
        }

        private void dgvProductos_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            var grilla = (DataGridView)sender;

            if (grilla.Columns[e.ColumnIndex].Name == "EsOferta" && e.Value != null)
            {
                bool esOferta;

                if (e.Value is bool b)
                {
                    esOferta = b;
                }
                else if (e.Value is string s)
                {
                    esOferta = s.Equals("true", StringComparison.OrdinalIgnoreCase);
                }
                else
                {
                    return;
                }

                e.Value = esOferta ? "En Oferta Activa" : "Sin Oferta";
                e.FormattingApplied = true;
            }

        }

        private void dgvProductos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            var grilla = (DataGridView)sender;
            var item = (ItemVentaDTO)grilla.Rows[e.RowIndex].DataBoundItem;

            if (grilla.Columns[e.ColumnIndex].Name == "BtnEliminar")
            {
                EliminarItem(item);
            }
            else if (grilla.Columns[e.ColumnIndex].Name == "BtnEditar")
            {
                EditarCantidad(item);
            }
        }
        private void EliminarItem(ItemVentaDTO item)
        {
            var confirm = MessageBox.Show(
                $"¿Eliminar {item.Descripcion}?",
                "Confirmar",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (confirm == DialogResult.Yes)
            {
                itemsVenta.Remove(item);   // lista bindada
                dgvProductos.Refresh();
                CalcularTotal();
            }
        }
        private void EditarCantidad(ItemVentaDTO item)
        {
            var input = Microsoft.VisualBasic.Interaction.InputBox(
                "Ingrese nueva cantidad:",
                "Modificar cantidad",
                item.Cantidad.ToString()
            );

            if (int.TryParse(input, out int nuevaCantidad) && nuevaCantidad > 0)
            {
                item.Cantidad = nuevaCantidad;
                dgvProductos.Refresh();
                CalcularTotal();
            }
        }
    }
}
