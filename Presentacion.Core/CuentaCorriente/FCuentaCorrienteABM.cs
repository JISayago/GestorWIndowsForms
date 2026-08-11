using AccesoDatos.Entidades;
using Presentacion.FBase;
using Presentacion.FBase.Helpers;
using Presentacion.FormulariosBase.Helpers;
using Servicios.Helpers.Cliente;
using Servicios.Helpers.Cliente.CtaCte;
using Servicios.Helpers.Sistema.FiltrosConsulta;
using Servicios.LogicaNegocio.Cliente;
using Servicios.LogicaNegocio.Cliente.DTO;
using Servicios.LogicaNegocio.CuentaCorriente;
using Servicios.LogicaNegocio.CuentaCorriente.DTO;
using Servicios.LogicaNegocio.Movimiento;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TuProyecto.Presentacion;

namespace Presentacion.Core.CuentaCorriente
{
    public partial class FCuentaCorrienteABM : FBaseABM
    {
        private readonly ICuentaCorrienteServicio _cuentacorrienteServicio;
        private readonly IClienteServicio _clienteServicio;
        private long? CuentaCorrienteId;
        private long? ClienteID;
        private CuentaCorrienteDTO _cuentaCorriente;
        private long? movimientoId;
        private bool EsCuentaNueva =>
      TipoOperacion == TipoOperacion.Nuevo;

        private bool CuentaCreada =>
            CuentaCorrienteId.HasValue;
        private int paginaActual = 1;
        private int totalPaginas = 1;
        private int totalRegistros = 0;
        private const int PageSize = 20;

        // 🔹 Reemplazamos el DataGridView por una BindingList en memoria
        private BindingList<string> _dnisAutorizadosLista;
        private string _dniDueno;

        private decimal saldoInicial = 0;
        private decimal limiteDeuda = 0;

        // Nuevas variables
        private decimal saldoOriginal = 0;
        private decimal limiteOriginal = 0;


        public FCuentaCorrienteABM()
        {
            InitializeComponent();
            InicializarListaDni();
        }
        private void InicializarListaDni()
        {
            _dnisAutorizadosLista = new BindingList<string>();
            lstDnis.DataSource = _dnisAutorizadosLista;
        }

        public FCuentaCorrienteABM(
  TipoOperacion tipoOperacion,
  long? clienteId = null,
  long? ctacteId = null)
  : base(tipoOperacion, null)
        {
            if (tipoOperacion == TipoOperacion.Nuevo && !clienteId.HasValue)
                throw new ArgumentException("Para crear una cuenta es obligatorio indicar el cliente.");

            if (tipoOperacion != TipoOperacion.Nuevo && !ctacteId.HasValue)
                throw new ArgumentException("Para modificar o eliminar es obligatorio indicar la cuenta corriente.");
            InitializeComponent();

            _cuentacorrienteServicio = new CuentaCorrienteServicio();
            _clienteServicio = new ClienteServicio();

            InicializarListaDni();

            if (clienteId.HasValue)
            {
                ClienteID = clienteId.Value;
            }

            if (ctacteId.HasValue)
            {
                CuentaCorrienteId = ctacteId.Value;
            }

            AgregarControlesObligatorios(txtNombreCC, "Nombre Cuenta Corriente");
        }

        protected override void AplicarTema(Control parent)
        {
            base.AplicarTema(parent);

            if (!ReferenceEquals(parent, this))
                return;

            AplicarEstiloCuentaCorriente();
        }

        private void AplicarEstiloCuentaCorriente()
        {
            EstilarLabelDestacado(lblCliente);
            EstilarLabelDestacado(lblNombreCliente);
            EstilarLabelDestacado(lblEstadoTitulo);
            EstilarLabelDestacado(lblEstado);
            EstilarLabelDestacado(lblFechaCreacionTitulo);
            EstilarLabelDestacado(lblFechaCreacion);
            EstilarLabelDestacado(lblFechaUltimaActivacionTitulo);
            EstilarLabelDestacado(lblFechaUltimaActivacion);
            EstilarLabelDestacado(lblListadoMovimientos);
            EstilarLabelDestacado(lblDni);

            if (lblNombreCliente != null)
                lblNombreCliente.ForeColor = TemaSistema.Primario;

            EstilarBotonPrimario(btnActivar);
            EstilarBotonPeligro(btnCerrarCtacte);
            EstilarBotonSecundario(btnCargarSaldoCtaCte);
            EstilarBotonSecundario(btnCargarLimite);
            EstilarBotonPrimario(btnAgregarDni);
            EstilarBotonSecundario(btnEliminarDni);
            EstilarBotonSecundario(btnAnterior);
            EstilarBotonSecundario(btnSiguiente);

            if (dgvGrilla != null)
            {
                dgvGrilla.BorderStyle = BorderStyle.FixedSingle;
                dgvGrilla.BackgroundColor = TemaSistema.FondoControl;
            }

            if (lstDnis != null)
            {
                lstDnis.BorderStyle = BorderStyle.FixedSingle;
                lstDnis.BackColor = TemaSistema.FondoControl;
                lstDnis.ForeColor = TemaSistema.Texto;
            }
        }

        private static void EstilarLabelDestacado(Label lbl)
        {
            if (lbl == null) return;
            lbl.ForeColor = TemaSistema.Texto;
            float size = lbl.Font?.Size >= 14F ? 10F : (lbl.Font?.Size ?? 10F);
            lbl.Font = new Font("Segoe UI", size, FontStyle.Bold);
        }

        private static void EstilarBotonPrimario(Button btn)
        {
            if (btn == null) return;
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.BackColor = TemaSistema.Primario;
            btn.ForeColor = Color.White;
            btn.Cursor = Cursors.Hand;
            btn.UseVisualStyleBackColor = false;
        }

        private static void EstilarBotonSecundario(Button btn)
        {
            if (btn == null) return;
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 1;
            btn.FlatAppearance.BorderColor = TemaSistema.Borde;
            btn.BackColor = TemaSistema.Seleccion;
            btn.ForeColor = TemaSistema.Texto;
            btn.Cursor = Cursors.Hand;
            btn.UseVisualStyleBackColor = false;
        }

        private static void EstilarBotonPeligro(Button btn)
        {
            if (btn == null) return;
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.BackColor = Color.FromArgb(160, 40, 40);
            btn.ForeColor = Color.White;
            btn.Cursor = Cursors.Hand;
            btn.UseVisualStyleBackColor = false;
        }

        private void FCuentaCorrienteABM_Load(object sender, EventArgs e)
        {
            ConfigurarFormulario();

            if (CuentaCreada)
            {
                CargarDatosCuenta();
                CargarMovimientos();
            }
            else
            {
                CargarDatosCliente();
            }

            ActualizarPantalla();
        }
        private void CargarDatosCliente()
        {
            if (!ClienteID.HasValue)
                return;

            var cliente = _clienteServicio.ObtenerClientePorId(ClienteID.Value);

            if (cliente == null)
                return;

            lblNombreCliente.Text = $"{cliente.Nombre} {cliente.Apellido}";

            txtNombreCC.Text = GenerarNombreCuentaCorriente(cliente);

            saldoInicial = 0;
            limiteDeuda = 0;
            chkLimiteDeuda.Checked = false;

            rbVencimientoAutomatico.Checked = true;
            nudCantidadMeses.Value = 1;

            _dniDueno = cliente.Dni?.Trim();
            _dnisAutorizadosLista.Clear();
            AsegurarDniDuenoEnLista();

            ActualizarPantalla();
        }

        private void ConfigurarFormulario()
        {
            ConfigurarTabs();
            ConfigurarTabConfiguracion();
            ConfigurarTabMovimientos();
            ConfigurarTabDnis();
            ConfigurarBotones();
            MejorarLayoutDnis();
        }

        private void ConfigurarTabs()
        {
            tbpMovimientos.Parent = EsCuentaNueva ? null : tbcBase;

            // Yo esta la dejaría siempre
            tbpDnis.Parent = tbcBase;
        }
        private void ConfigurarTabConfiguracion()
        {
            bool creada = CuentaCreada;

            //lblSaldo.Visible = creada;
            //btnCargarSaldoCtaCte.Visible = creada;

            lblEstado.Visible = creada;
            lblEstadoTitulo.Visible = creada;

            //lblFechaCreacion.Visible = creada;
            //lblFechaCreacionTitulo.Visible = creada;

            //lblFechaVencimiento.Visible = creada;
            //lblFechaVencimientoTitulo.Visible = creada;
            btnCerrarCtacte.Visible = creada;
            btnActivar.Visible = creada;
        }
        private void ConfigurarTabMovimientos()
        {
            dgvGrilla.Enabled = !EsCuentaNueva;

            btnAnterior.Enabled = !EsCuentaNueva;
            btnSiguiente.Enabled = !EsCuentaNueva;

            lblPagina.Visible = !EsCuentaNueva;
            lblTotalRegistros.Visible = !EsCuentaNueva;
        }

        private void ConfigurarBotones()
        {
            ActualizarBotones();
        }
        private void CargarDatosCuenta()
        {
            if (CuentaCorrienteId == null)
            {
                MessageBox.Show(@"Ocurrio un Error Grave", @"Error Grave", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                this.Close();
                return;
            }

            _cuentaCorriente = _cuentacorrienteServicio.ObtenerCuentaCorrientePorId(CuentaCorrienteId.Value);

            saldoInicial = _cuentaCorriente.Saldo;
            saldoOriginal = _cuentaCorriente.Saldo;

            limiteDeuda = _cuentaCorriente.LimiteDeuda;
            limiteOriginal = _cuentaCorriente.LimiteDeuda;

            txtNombreCC.Text = _cuentaCorriente.NombreCuentaCorriente;
            lblSaldo.Text = saldoInicial.ToString("C");
            lblLimiteDeuda.Text = limiteDeuda.ToString("C");
            chkLimiteDeuda.Checked = _cuentaCorriente.LimiteDeudaActivo;
            btnCargarLimite.Enabled = _cuentaCorriente.LimiteDeudaActivo;
            lblNombreCliente.Text = _cuentaCorriente.NombreCliente;

            if (!ClienteID.HasValue)
                ClienteID = _cuentaCorriente.ClienteId;

            var cliente = _clienteServicio.ObtenerClientePorId(_cuentaCorriente.ClienteId);
            _dniDueno = cliente?.Dni?.Trim();

            rbVencimientoAutomatico.Checked = _cuentaCorriente.TipoVencimiento == (int)TipoVencimientoCuentaCorriente.Automatico;
            rbVencimientoManual.Checked = _cuentaCorriente.TipoVencimiento == (int)TipoVencimientoCuentaCorriente.Manual;
            if(_cuentaCorriente.EstadoCtaCte == (int)EstadoCuentaCorriente.Activa)
            {
                btnActivar.Enabled = false;
            }
            nudCantidadMeses.Value =
                _cuentaCorriente.CantidadMesesVencimiento;
            // 🔹 Mapeo directo a la lista del ListBox sin dar vueltas con celdas
            _dnisAutorizadosLista.Clear();

            foreach (var dni in _cuentaCorriente.DniAutorizados)
            {
                _dnisAutorizadosLista.Add(dni);
            }

            AsegurarDniDuenoEnLista();

        }

        public override bool EjecutarComandoNuevo()
        {
            if (!VerificarDatosObligatorios())
            {
                MessageBox.Show(@"Por favor ingrese los campos Obligatorios.", @"Atención", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (!ClienteID.HasValue)
            {
                MessageBox.Show(
                    "No se encontró el cliente.",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                return false;
            }
            if(_dnisAutorizadosLista.Count < 1)
            {
                MessageBox.Show(@"Debe ingresar al menos un DNI autorizado.", @"Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            var tipoVencimiento = rbVencimientoAutomatico.Checked
                ? TipoVencimientoCuentaCorriente.Automatico
                : TipoVencimientoCuentaCorriente.Manual;

            var cantidadMeses = rbVencimientoAutomatico.Checked
                ? (int)nudCantidadMeses.Value
                : 1;

            if (!ValidarSaldoYLimite())
                return false;

            AsegurarDniDuenoEnLista();

            var nuevoCuentaCorriente = new CuentaCorrienteDTO
            {
                ClienteId = ClienteID.Value, // Asumimos que el ID del cliente se pasa al formulario y se usa para crear la cuenta corriente
                NombreCuentaCorriente = txtNombreCC.Text,
                Saldo = saldoInicial,
                TipoVencimiento = (int)tipoVencimiento,
                CantidadMesesVencimiento = cantidadMeses,
                LimiteDeudaActivo = chkLimiteDeuda.Checked,
                LimiteDeuda = limiteDeuda,
                FechaCreacion = DateTime.Now,
                FechaActivacion = DateTime.Now,// evaluar si va con creacion activacion automatica o no, por ahora lo dejamos asi
                FechaVencimiento = DateTime.Now.AddMonths(cantidadMeses),
                // 🔹 Directamente le pasamos la lista limpia convertida a List<long>
                DniAutorizados = _dnisAutorizadosLista.ToList(),
                EstaEliminado = false,
            };

            var response = _cuentacorrienteServicio.Insertar(nuevoCuentaCorriente);

            if (response.Exitoso)
            {
                MessageBox.Show($"{response.Mensaje}", @"Atención", MessageBoxButtons.OK, MessageBoxIcon.Information);
                RealizoAlgunaOperacion = true;
                DialogResult = DialogResult.OK;
                this.Close();
                return true;

            }
            else
            {
                MessageBox.Show($"{response.Mensaje}", @"Atención", MessageBoxButtons.OK, MessageBoxIcon.Error);
                RealizoAlgunaOperacion = false;
                DialogResult = DialogResult.Cancel;
                this.Close();
                return false;

            }
        }

        public override bool EjecutarComandoEliminar()
        {
            if (CuentaCorrienteId == null)
            {
                MessageBox.Show(@"´Por favor seleccione un cuentacorriente válido.", @"Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }
            if (TipoOperacion == TipoOperacion.Eliminar)
            {
                var response = _cuentacorrienteServicio.Eliminar(CuentaCorrienteId.Value);
                if (response.Exitoso)
                {
                    MessageBox.Show($"{response.Mensaje}", @"Atención", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return true;
                }
                else
                {
                    MessageBox.Show($"{response.Mensaje}", @"Atención", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
            return false;
        }

        public override bool EjecutarComandoModificar()
        {
            if (CuentaCorrienteId == null)
            {
                MessageBox.Show(@"´Por favor seleccione un cuentacorriente válido.", @"Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }
            if (!VerificarDatosObligatorios())
                return false;

            if (!ValidarSaldoYLimite())
                return false;
            if(_dnisAutorizadosLista.Count < 1)
            {
                MessageBox.Show(@"Debe ingresar al menos un DNI autorizado.", @"Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            var tipoVencimiento = rbVencimientoAutomatico.Checked
                ? TipoVencimientoCuentaCorriente.Automatico
                : TipoVencimientoCuentaCorriente.Manual;

            var cantidadMeses = rbVencimientoAutomatico.Checked
                 ? (int)nudCantidadMeses.Value
                 : 1;


            if (TipoOperacion == TipoOperacion.Modificar)
            {
                AsegurarDniDuenoEnLista();

                bool vencimientoCambio =
                    _cuentaCorriente != null &&
                    (_cuentaCorriente.TipoVencimiento != (int)tipoVencimiento ||
                     _cuentaCorriente.CantidadMesesVencimiento != cantidadMeses);

                DateTime? fechaVencimiento =
                    vencimientoCambio || _cuentaCorriente?.FechaVencimiento == null
                        ? DateTime.Now.AddMonths(cantidadMeses)
                        : _cuentaCorriente.FechaVencimiento;

                var cuentacorrienteEditar = new CuentaCorrienteDTO
                {
                    NombreCuentaCorriente = txtNombreCC.Text,
                    Saldo = saldoInicial,
                    MontoCargaSaldo = saldoInicial - saldoOriginal,
                    LimiteDeuda = limiteDeuda,
                    LimiteDeudaActivo = chkLimiteDeuda.Checked,
                    TipoVencimiento = (int)tipoVencimiento,
                    CantidadMesesVencimiento = cantidadMeses,
                    FechaVencimiento = fechaVencimiento,

                    // 🔹 Al modificar también usamos la lista del ListBox directamente
                    DniAutorizados = _dnisAutorizadosLista.ToList(),
                    EstaEliminado = false
                };
                // 🔹 Directamente le pasamos la lista limpia convertida a List<long>



                var response = _cuentacorrienteServicio.Modificar(cuentacorrienteEditar, CuentaCorrienteId.Value);

                if (response.Exitoso)
                {
                    MessageBox.Show($"{response.Mensaje}", @"Atención", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return true;
                }
                else
                {
                    MessageBox.Show($"{response.Mensaje}", @"Atención", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
            return false;
        }

        // 🔹 EVENTO: Botón Agregar DNI
        private void btnAgregarDni_Click(object sender, EventArgs e)
        {
            var dni = txtNuevoDni.Text.Trim();
            if (string.IsNullOrWhiteSpace(dni))
            {
                MessageBox.Show("Ingrese un DNI.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNuevoDni.Focus();
                return;
            }

            if (_dnisAutorizadosLista.Contains(dni))
            {
                MessageBox.Show("Este DNI ya está en la lista.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            _dnisAutorizadosLista.Add(dni);
            txtNuevoDni.Clear();
            txtNuevoDni.Focus();
        }

        // 🔹 EVENTO: Botón Eliminar DNI seleccionado
        private void btnEliminarDni_Click(object sender, EventArgs e)
        {
            if (lstDnis.SelectedItem == null)
            {
                MessageBox.Show("Seleccione un DNI de la lista para eliminarlo.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var dniSeleccionado = (string)lstDnis.SelectedItem;
            if (!string.IsNullOrWhiteSpace(_dniDueno) &&
                string.Equals(dniSeleccionado, _dniDueno, StringComparison.OrdinalIgnoreCase))
            {
                MessageBox.Show("No se puede quitar el DNI del titular de la cuenta.", "Atención",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            _dnisAutorizadosLista.Remove(dniSeleccionado);
        }

        private void AsegurarDniDuenoEnLista()
        {
            if (string.IsNullOrWhiteSpace(_dniDueno))
                return;

            if (!_dnisAutorizadosLista.Any(d =>
                    string.Equals(d, _dniDueno, StringComparison.OrdinalIgnoreCase)))
            {
                _dnisAutorizadosLista.Insert(0, _dniDueno);
            }
        }

        private void MejorarLayoutDnis()
        {
            if (tbpDnis == null || lstDnis == null)
                return;

            tbpDnis.SuspendLayout();
            tbpDnis.Controls.Clear();
            tbpDnis.Padding = new Padding(16);

            lblDni.Dock = DockStyle.Top;
            lblDni.AutoSize = false;
            lblDni.Height = 28;
            lblDni.TextAlign = ContentAlignment.MiddleLeft;

            var pnlAcciones = new Panel
            {
                Dock = DockStyle.Top,
                Height = 36,
                Margin = new Padding(0, 0, 0, 8)
            };

            txtNuevoDni.Location = new Point(0, 4);
            txtNuevoDni.Size = new Size(240, 25);
            txtNuevoDni.Anchor = AnchorStyles.Top | AnchorStyles.Left;

            btnAgregarDni.Location = new Point(250, 2);
            btnAgregarDni.Size = new Size(90, 30);
            btnEliminarDni.Location = new Point(348, 2);
            btnEliminarDni.Size = new Size(90, 30);

            pnlAcciones.Controls.Add(txtNuevoDni);
            pnlAcciones.Controls.Add(btnAgregarDni);
            pnlAcciones.Controls.Add(btnEliminarDni);

            lstDnis.Dock = DockStyle.Fill;
            lstDnis.BorderStyle = BorderStyle.FixedSingle;

            // Orden Dock: Fill primero, luego Top (último Top queda arriba).
            tbpDnis.Controls.Add(lstDnis);
            tbpDnis.Controls.Add(pnlAcciones);
            tbpDnis.Controls.Add(lblDni);
            tbpDnis.ResumeLayout(true);
        }

        private void ActualizarProximoVencimiento()
        {
            if (_cuentaCorriente == null)
            {
                if (EsCuentaNueva)
                {
                    var meses = (int)nudCantidadMeses.Value;
                    lblFechaVencimiento.Text =
                        $"Próximo vencimiento: {DateTime.Today.AddMonths(meses):dd/MM/yyyy}";
                }
                return;
            }

            if(_cuentaCorriente.ConDeuda && _cuentaCorriente.EstadoCtaCte == (int)EstadoCuentaCorriente.Suspendida)
            {
                lblFechaVencimiento.Text = "Cuenta vencida con deuda.\n" +
                    " Por favor pague la deuda para registrar el próximo vencimiento";
                return;
            }
            if(_cuentaCorriente.EstadoCtaCte == (int)EstadoCuentaCorriente.Suspendida && _cuentaCorriente.TipoVencimiento == (int)TipoVencimientoCuentaCorriente.Manual && _cuentaCorriente.Saldo >= 0)
            {
                lblFechaVencimiento.Text = "Cuenta suspendida sin deuda y/o con saldo a favor.\n" +
                    " Por favor seleccione las condiciones para el próximo vencimiento";
                return;
            }

            var mesesCalc = (int)nudCantidadMeses.Value;

            var proximo = DateTime.Today.AddMonths(mesesCalc);

            lblFechaVencimiento.Text =
                $"Próximo vencimiento: {proximo:dd/MM/yyyy}";
        }
        private void ActualizarPantalla()
        {
            ActualizarSaldo();

            ActualizarLimite();

            ActualizarEstado();

            ActualizarProximoVencimiento();

            ActualizarBotones();
        }
        
        private void ActualizarLimite()
        {
            btnCargarLimite.Enabled = chkLimiteDeuda.Checked;

            if (!chkLimiteDeuda.Checked)
            {
                lblLimiteDeuda.Text = "No habilitado";
                return;
            }

            if (limiteDeuda == 0)
            {
                lblLimiteDeuda.Text = "Habilitado sin límite asignado";
                return;
            }

            lblLimiteDeuda.Text = limiteDeuda.ToString("C");
        }
        private void rbVencimientoMensual_CheckedChanged(object sender, EventArgs e)
        {
            ActualizarPantalla();
        }

        private void ConfigurarTabDnis()
        {
            lstDnis.Enabled = TipoOperacion != TipoOperacion.Eliminar;

            txtNuevoDni.Enabled = TipoOperacion != TipoOperacion.Eliminar;

            btnAgregarDni.Enabled = TipoOperacion != TipoOperacion.Eliminar;
            btnEliminarDni.Enabled = TipoOperacion != TipoOperacion.Eliminar;
        }
        private void rbVencimientoManual_CheckedChanged(object sender, EventArgs e)
        {
            ActualizarPantalla();
        }
        private void ActualizarSaldo()
        {
            lblSaldo.Text = saldoInicial.ToString("C");
        }

        private void ActualizarEstado()
        {
            if (EsCuentaNueva)
            {
                lblEstado.Text = "Pendiente de creación";
                lblFechaCreacion.Text = DateTime.Today.ToString("dd/MM/yyyy");
                lblFechaUltimaActivacion.Text = "-";
                lblFechaVencimiento.Text = "-";
                return;
            }
            if (_cuentaCorriente == null)
                return;

            if (CuentaCreada)
            {
                if (_cuentaCorriente.EstadoCtaCte == (int)EstadoCuentaCorriente.Activa)
                {
                    btnCerrarCtacte.Text = "Cerrar Cuenta";
                    btnActivar.Enabled = false;
                }
                else if (_cuentaCorriente.EstadoCtaCte == (int)EstadoCuentaCorriente.Cerrada)
                {
                    btnActivar.Enabled = false;
                    btnCerrarCtacte.Text = "Reabrir Cuenta";
                }
                else
                {
                    btnActivar.Enabled = true;
                    btnCerrarCtacte.Text = "Cerrar Cuenta";
                }
            }

            lblEstado.Text = _cuentaCorriente.EstadoDescripcionCtaCte;

            lblFechaCreacion.Text =
                _cuentaCorriente.FechaCreacion.HasValue
                    ? _cuentaCorriente.FechaCreacion.Value.ToString("dd/MM/yyyy")
                    : "-";

            lblFechaVencimiento.Text =
                _cuentaCorriente.FechaVencimiento.HasValue
                    ? _cuentaCorriente.FechaVencimiento.Value.ToString("dd/MM/yyyy")
                    : "-";
            lblFechaUltimaActivacion.Text =
                _cuentaCorriente.FechaActivacion.HasValue
                    ? _cuentaCorriente.FechaActivacion.Value.ToString("dd/MM/yyyy")
                    : "-";
        }
        private string GenerarNombreCuentaCorriente(ClienteDTO cliente)
        {
            var inicial = cliente.Nombre.Trim()[0].ToString().ToUpper();
            var apellido = cliente.Apellido.Trim();

            return $"{inicial}{apellido} - {DateTime.Now:ddMMyyHHmmss}";
        }
        private bool ValidarSaldoYLimite()
        {
            decimal saldo = saldoInicial;
            decimal limite = limiteDeuda;

            // No permite una cuenta sin saldo y sin deuda.
            if (!chkLimiteDeuda.Checked && saldo <= 0)
            {
                MessageBox.Show(
                    "La cuenta debe tener saldo a favor o permitir deuda.",
                    "Atención",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return false;
            }

            // Deuda ilimitada (límite = 0)
            if (chkLimiteDeuda.Checked && limite == 0)
                return true;

            // Si tiene límite, la deuda inicial no puede superarlo.
            if (chkLimiteDeuda.Checked &&
                saldo < 0 &&
                Math.Abs(saldo) > limite)
            {
                MessageBox.Show(
                    "La deuda inicial supera el límite permitido.",
                    "Atención",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return false;
            }

            return true;
        }

        private void nudCantidadMeses_ValueChanged_1(object sender, EventArgs e)
        {
            ActualizarPantalla();
        }

        private void chkLimiteDeuda_CheckedChanged_1(object sender, EventArgs e)
        {
            ActualizarPantalla();
        }


        private void btnCargarSaldoCtaCte_Click(object sender, EventArgs e)
        {
            using (var f = new FCargaSaldoCtaCte(
                saldoInicial,
                HelperFormularioCargaSaldoCtaCte.Saldo))
            {
                if (f.ShowDialog() != DialogResult.OK)
                    return;

                if (!CuentaCreada)
                {
                    // Todavía no existe la CtaCte (estamos en el alta), no hay Id para pegarle al service.
                    // MontoIngresado ya es el saldo resultante absoluto (actual + carga).
                    saldoInicial = f.MontoIngresado;

                    ActualizarPantalla();
                    return;
                }

                var respuesta = _cuentacorrienteServicio.CargarSaldoCuentaCorriente(
                    CuentaCorrienteId.Value,
                    f.MontoIngresado);

                if (!respuesta.Exitoso)
                {
                    MessageBox.Show(respuesta.Mensaje, "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                lblSaldo.Text = respuesta.DatoExtra;

                MessageBox.Show(respuesta.Mensaje, "Atención", MessageBoxButtons.OK, MessageBoxIcon.Information);

                CargarDatosCuenta();
                ActualizarPantalla();
            }
        }
        private void btnCargarLimite_Click(object sender, EventArgs e)
        {
            using (var f = new FCargaSaldoCtaCte(
                limiteDeuda,
                HelperFormularioCargaSaldoCtaCte.LimiteDeuda))
            {
                if (f.ShowDialog() != DialogResult.OK)
                    return;

                limiteDeuda = f.MontoIngresado;

                ActualizarLimite();
            }
        }

        private void CargarMovimientos()
        {
            if (!CuentaCorrienteId.HasValue)
                return;

            var filtros = new FiltroConsulta
            {
                Page = paginaActual,
                PageSize = PageSize,

                Bool1 = false,
                Bool2 = false,

                TextoBuscar = string.Empty,

                FechaDesde = null,
                FechaHasta = null,

                Filtro1 = null,
                Filtro2 = null,
                Filtro3 = null
            };
            var resultado = _cuentacorrienteServicio.ObtenerMovimientosPorCuentaCorriente(CuentaCorrienteId.Value, filtros);

            dgvGrilla.DataSource = resultado.Items;

            ResetearGrilla(dgvGrilla);

            totalRegistros = resultado.TotalRegistros;

            totalPaginas = Math.Max(1, (int)Math.Ceiling((double)totalRegistros / resultado.PageSize));

            ActualizarBotones();
        }
        private void DgvGrilla_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            RowEnter(e);
        }
        public virtual void RowEnter(DataGridViewCellEventArgs e)
        {
            try
            {
                movimientoId = null;

                if (e.RowIndex < 0 || dgvGrilla.RowCount == 0)
                    return;

                if (!dgvGrilla.Columns.Contains("Id"))
                    return;

                var fila = dgvGrilla.Rows[e.RowIndex];

                if (fila?.Cells["Id"].Value == null)
                    return;

                movimientoId = Convert.ToInt64(fila.Cells["Id"].Value);
            }
            catch
            {
                movimientoId = null;
            }

        }

        private void DgvGrilla_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;
        }

        private void EjecutarClickDerechoFila(long? id, Point posicionMouse)
        {
            if (!id.HasValue)
                return;

            ContextMenuStrip menu = new ContextMenuStrip();

            menu.Items.Add("Ver Detalle", null, (s, e) =>
            {
                if (!id.HasValue)
                    return;

                var f = new FMovimientoDetallado(id.Value);

                f.ShowDialog();
            });


            menu.Show(dgvGrilla, posicionMouse);
        }



        public virtual void ResetearGrilla(DataGridView grilla)
        {
            for (int i = 0; i < grilla.ColumnCount; i++)
                grilla.Columns[i].Visible = false;

            if (grilla.Columns.Count == 0)
                return;

            grilla.ReadOnly = true;

            // IMPORTANTE: usar Fill real
            grilla.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // =========================================
            // ID (OCULTO)
            // =========================================
            if (grilla.Columns.Contains("MovimientoId"))
            {
                grilla.Columns["MovimientoId"].Visible = false;
                grilla.Columns["MovimientoId"].Name = "Id";
            }

            // =========================================
            // NUMERO (GRANDE)
            // =========================================
            if (grilla.Columns.Contains("NumeroMovimiento"))
            {
                var col = grilla.Columns["NumeroMovimiento"];

                col.Visible = true;
                col.HeaderText = "Número";

                col.FillWeight = 150;   // 🔥 grande
                //col.MinimumWidth = 130;
            }

            // =========================================
            // FECHA (MEDIO)
            // =========================================
            if (grilla.Columns.Contains("FechaMovimiento"))
            {
                var col = grilla.Columns["FechaMovimiento"];

                col.Visible = true;
                col.HeaderText = "Fecha";

                col.DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";

                col.FillWeight = 150;
                //col.MinimumWidth = 130;
            }

            // =========================================
            // MOVIMIENTO (CHICO)
            // =========================================
            if (grilla.Columns.Contains("TipoMovimientoDescripcion"))
            {
                var col = grilla.Columns["TipoMovimientoDescripcion"];

                col.Visible = true;
                col.HeaderText = "Movimiento";

                col.FillWeight = 100;
                //col.MinimumWidth = 90;
            }

            if (grilla.Columns.Contains("TipoMovimiento"))
            {
                grilla.Columns["TipoMovimiento"].Visible = false;
            }

            // =========================================
            // TIPO DETALLE (MEDIO)
            // =========================================
            if (grilla.Columns.Contains("TipoMovimientoDetalleDescripcion"))
            {
                grilla.Columns["TipoMovimientoDetalleDescripcion"].Visible = false;
            }

            if (grilla.Columns.Contains("TipoMovimientoDetalle"))
            {
                grilla.Columns["TipoMovimientoDetalle"].Visible = false;
            }

            // =========================================
            // MONTO (GRANDE)
            // =========================================
            if (grilla.Columns.Contains("Monto"))
            {
                var col = grilla.Columns["Monto"];

                col.Visible = true;
                col.HeaderText = "Monto";

                col.DefaultCellStyle.Format = "C2";

                col.FillWeight = 150;   // 🔥 grande
                //col.MinimumWidth = 150;
            }

            // =========================================
            // OCULTOS
            // =========================================
            if (grilla.Columns.Contains("EntidadId"))
                grilla.Columns["EntidadId"].Visible = false;

            if (grilla.Columns.Contains("TipoEntidad"))
                grilla.Columns["TipoEntidad"].Visible = false;

            if (grilla.Columns.Contains("EstaEliminado"))
                grilla.Columns["EstaEliminado"].Visible = false;
        }
        private void ActualizarBotones()
        {
            //btnCargarSaldoCtaCte.Enabled =
            //   CuentaCreada;

            //btnCargarLimite.Enabled =
            //    CuentaCreada && chkLimiteDeuda.Checked;
     

            lblPagina.Text = $"Página {paginaActual} de {totalPaginas}";
            lblTotalRegistros.Text = $"Total: {totalRegistros}";

            btnAnterior.Enabled = paginaActual > 1;
            btnSiguiente.Enabled = paginaActual < totalPaginas;
        }

        private void btnAnterior_Click(object sender, EventArgs e)
        {
            if (paginaActual <= 1)
                return;

            paginaActual--;

            CargarMovimientos();
        }

        private void btnSiguiente_Click(object sender, EventArgs e)
        {
            if (paginaActual >= totalPaginas)
                return;

            paginaActual++;

            CargarMovimientos();
        }

        private void dgvGrilla_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Right)
                return;

            var hit = dgvGrilla.HitTest(e.X, e.Y);

            if (hit.RowIndex >= 0)
            {
                dgvGrilla.ClearSelection();

                dgvGrilla.Rows[hit.RowIndex].Selected = true;

                RowEnter(new DataGridViewCellEventArgs(0, hit.RowIndex));

                EjecutarClickDerechoFila(movimientoId, e.Location);
            }
        }

        private void btnActivar_Click(object sender, EventArgs e)
        {
            if (_cuentaCorriente == null || !CuentaCorrienteId.HasValue)
                return;
            if (_cuentaCorriente.EstadoCtaCte == (int)EstadoCuentaCorriente.Activa)
                return;
            var msjee = MessageBox.Show("¿Está seguro que desea Activar la cuenta corriente? Esta acción no se puede deshacer.", "Confirmar cierre", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (msjee != DialogResult.Yes)
                return;
            var respuesta = _cuentacorrienteServicio.ActivarCuentaCorriente(CuentaCorrienteId.Value);
            if (respuesta.Exitoso)
            {
                MessageBox.Show($"{respuesta.Mensaje}", @"Atención", MessageBoxButtons.OK, MessageBoxIcon.Information);
                CargarDatosCuenta();
                ActualizarPantalla();
            }
            else
            {
                MessageBox.Show($"{respuesta.Mensaje}", @"Atención", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            
        }

        private void btnCerrarCtacte_Click(object sender, EventArgs e)
        {
            if (_cuentaCorriente == null || !CuentaCorrienteId.HasValue)
                return;
            if (_cuentaCorriente.EstadoCtaCte == (int)EstadoCuentaCorriente.Cerrada)
            {
                var msje = MessageBox.Show("¿Esta seguro que desea reabrir la cuenta corriente?", "Confirmar reapertura", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (msje == DialogResult.Yes)
                {
                    var res = _cuentacorrienteServicio.ReabrirCuentaCorriente(CuentaCorrienteId.Value);
                    if (res.Exitoso)
                    {
                        MessageBox.Show($"{res.Mensaje}", @"Atención", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        CargarDatosCuenta();
                        ActualizarPantalla();
                    }
                    else
                    {
                        MessageBox.Show($"{res.Mensaje}", @"Atención", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                return;
            }   
            var msjee = MessageBox.Show("¿Está seguro que desea cerrar la cuenta corriente? Esta acción no se puede deshacer.", "Confirmar cierre", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (msjee != DialogResult.Yes)
                return;
            var respuesta = _cuentacorrienteServicio.CerrarCuentaCorriente(CuentaCorrienteId.Value);
            if (respuesta.Exitoso)
            {
                MessageBox.Show($"{respuesta.Mensaje}", @"Atención", MessageBoxButtons.OK, MessageBoxIcon.Information);
                CargarDatosCuenta();
                ActualizarPantalla();
            }
            else
            {
                MessageBox.Show($"{respuesta.Mensaje}", @"Atención", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

    }
}