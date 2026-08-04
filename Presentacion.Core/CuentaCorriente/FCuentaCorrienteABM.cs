using AccesoDatos.Entidades;
using Presentacion.FBase;
using Presentacion.FormulariosBase.Helpers;
using Servicios.Helpers.Cliente;
using Servicios.Helpers.Cliente.CtaCte;
using Servicios.Helpers.Sistema.FiltrosConsulta;
using Servicios.LogicaNegocio.Cliente;
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
        private decimal saldoInicial = 0; // Variable para almacenar el saldo actual del cliente
        private decimal limiteDeuda = 0; // Variable para almacenar el límite de deuda del cliente
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
        private BindingList<long> _dnisAutorizadosLista;

        public FCuentaCorrienteABM()
        {
            InitializeComponent();
            InicializarListaDni();
        }
        private void InicializarListaDni()
        {
            _dnisAutorizadosLista = new BindingList<long>();
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
        private void FCuentaCorrienteABM_Load(object sender, EventArgs e)
        {
            ConfigurarFormulario();

            if (CuentaCreada)
            {
                CargarDatosCuenta();
                CargarMovimientos();
            }

            ActualizarPantalla();
        }

        private void ConfigurarFormulario()
        {
            ConfigurarTabs();
            ConfigurarTabConfiguracion();
            ConfigurarTabMovimientos();
            ConfigurarTabDnis();
            ConfigurarBotones();
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

            lblSaldo.Visible = creada;
            btnCargarSaldoCtaCte.Visible = creada;

            lblEstado.Visible = creada;
            lblEstadoTitulo.Visible = creada;

            lblFechaCreacion.Visible = creada;
            lblFechaCreacionTitulo.Visible = creada;

            lblFechaVencimiento.Visible = creada;
            lblFechaVencimientoTitulo.Visible = creada;

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
            limiteDeuda = _cuentaCorriente.LimiteDeuda;

            txtNombreCC.Text = _cuentaCorriente.NombreCuentaCorriente;
            lblSaldo.Text = saldoInicial.ToString("C");
            lblLimiteDeuda.Text = limiteDeuda.ToString("C");
            chkLimiteDeuda.Checked = _cuentaCorriente.LimiteDeudaActivo;
            btnCargarLimite.Enabled = _cuentaCorriente.LimiteDeudaActivo;



            rbVencimientoMensual.Checked = _cuentaCorriente.TipoVencimiento == (int)TipoVencimientoCuentaCorriente.Mensual;
            rbVencimientoManual.Checked = _cuentaCorriente.TipoVencimiento == (int)TipoVencimientoCuentaCorriente.Manual;

            nudCantidadMeses.Value =
                _cuentaCorriente.CantidadMesesVencimiento;
            // 🔹 Mapeo directo a la lista del ListBox sin dar vueltas con celdas
            _dnisAutorizadosLista.Clear();

            foreach (var dni in _cuentaCorriente.DniAutorizados)
            {
                _dnisAutorizadosLista.Add(dni);
            }

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
            var tipoVencimiento = rbVencimientoMensual.Checked
                ? TipoVencimientoCuentaCorriente.Mensual
                : TipoVencimientoCuentaCorriente.Manual;

            var cantidadMeses = rbVencimientoMensual.Checked
                ? 1
                : (int)nudCantidadMeses.Value;

            if (!ValidarSaldoYLimite())
                return false;
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
            var tipoVencimiento = rbVencimientoMensual.Checked
          ? TipoVencimientoCuentaCorriente.Mensual
          : TipoVencimientoCuentaCorriente.Manual;

            var cantidadMeses = rbVencimientoMensual.Checked
               ? 1
               : (int)nudCantidadMeses.Value;


            if (TipoOperacion == TipoOperacion.Modificar)
            {
                var cuentacorrienteEditar = new CuentaCorrienteDTO
                {
                    NombreCuentaCorriente = txtNombreCC.Text,
                    Saldo = saldoInicial,
                    //FechaVencimiento = dtpFechaVencimiento.Value,
                    LimiteDeudaActivo = chkLimiteDeuda.Checked,
                    LimiteDeuda = limiteDeuda,
                    TipoVencimiento = (int)tipoVencimiento,
                    CantidadMesesVencimiento = cantidadMeses,

                    // 🔹 Al modificar también usamos la lista del ListBox directamente
                    DniAutorizados = _dnisAutorizadosLista.ToList(),
                    EstaEliminado = false
                };

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
            if (long.TryParse(txtNuevoDni.Text.Trim(), out long dni))
            {
                if (_dnisAutorizadosLista.Contains(dni))
                {
                    MessageBox.Show("Este DNI ya está en la lista.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                _dnisAutorizadosLista.Add(dni);
                txtNuevoDni.Clear();
                txtNuevoDni.Focus();
            }
            else
            {
                MessageBox.Show("Por favor, ingrese un número de DNI válido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // 🔹 EVENTO: Botón Eliminar DNI seleccionado
        private void btnEliminarDni_Click(object sender, EventArgs e)
        {
            if (lstDnis.SelectedItem != null)
            {
                var dniSeleccionado = (long)lstDnis.SelectedItem;
                _dnisAutorizadosLista.Remove(dniSeleccionado);
            }
            else
            {
                MessageBox.Show("Seleccione un DNI de la lista para eliminarlo.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void ActualizarProximoVencimiento()
        {
            if (CuentaCreada)
                return;
            int meses = rbVencimientoMensual.Checked
                ? 1
                : (int)nudCantidadMeses.Value;

            var fechaBase = DateTime.Today;

            var proximo = fechaBase.AddMonths(meses);

            lblFechaVencimiento.Text = $"Próximo vencimiento: {proximo:dd/MM/yyyy}";
        }
        private void ActualizarPantalla()
        {
            ActualizarSaldo();

            ActualizarLimite();

            ActualizarEstado();

            ActualizarProximoVencimiento();

            ActualizarBotones();

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
        private void ActualizarLimite()
        {
            btnCargarLimite.Enabled = chkLimiteDeuda.Checked;

            lblLimiteDeuda.Text = chkLimiteDeuda.Checked
                ? limiteDeuda.ToString("C")
                : "Sin límite";
        }

        private void ActualizarEstado()
        {
            if (EsCuentaNueva)
            {
                lblEstado.Text = "Pendiente de creación";
                lblFechaCreacion.Text = "-";
                lblFechaVencimiento.Text = "-";
                return;
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
        }

        private bool ValidarSaldoYLimite()
        {
            decimal saldo = saldoInicial;

            decimal limite = limiteDeuda;

            bool tieneSaldoAFavor = saldo > 0;

            bool deudaInfinita =
                chkLimiteDeuda.Checked && limite == 0;

            bool deudaConLimite =
                chkLimiteDeuda.Checked && limite > 0;

            if (!tieneSaldoAFavor &&
                !deudaInfinita &&
                !deudaConLimite)
            {
                MessageBox.Show(
                    "La cuenta debe tener saldo a favor o permitir deuda.",
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

                saldoInicial = f.MontoIngresado;

                lblSaldo.Text = saldoInicial.ToString("C");
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

                lblLimiteDeuda.Text = limiteDeuda.ToString("C");
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
            btnCargarSaldoCtaCte.Enabled =
               CuentaCreada;

            btnCargarLimite.Enabled =
                CuentaCreada && chkLimiteDeuda.Checked;

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
            if (_cuentaCorriente.EstadoCtaCte == (int)EstadoCuentaCorriente.Cerrada)
                return;
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