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

namespace Presentacion.Core.CuentaCorriente
{
    public partial class FCuentaCorrienteABM : FBaseABM
    {
        private readonly ICuentaCorrienteServicio _cuentacorrienteServicio;
        private readonly IClienteServicio _clienteServicio;
        private long ClienteID;

        // 🔹 Reemplazamos el DataGridView por una BindingList en memoria
        private BindingList<long> _dnisAutorizadosLista;

        public FCuentaCorrienteABM()
        {
            InitializeComponent();
            InicializarListaDni();
        }

        public FCuentaCorrienteABM(TipoOperacion tipoOperacion, long? entidadID = null) : base(tipoOperacion, entidadID)
        {
            InitializeComponent();
            _cuentacorrienteServicio = new CuentaCorrienteServicio();
            _clienteServicio = new ClienteServicio();
            ClienteID = entidadID ?? 0; // Asignar un valor predeterminado si es null
            InicializarListaDni();

            if (tipoOperacion == TipoOperacion.Eliminar || tipoOperacion == TipoOperacion.Modificar)
            {
                CargarDatos(entidadID);
            }

            if (tipoOperacion == TipoOperacion.Eliminar)
            {
                DesactivarControles(this);
                // Deshabilitar controles de carga de DNI en modo eliminación
                txtNuevoDni.Enabled = false;
                btnAgregarDni.Enabled = false;
                btnEliminarDni.Enabled = false;
            }
            txtLimiteDeuda.Enabled = false; // Deshabilitar el TextBox de límite de deuda al inicio
           
            //lblFechaVTO.Text = DateTime.Now.ToString();

            //var clientes = _clienteServicio.ObtenerClientes(filtros).Items;



            AgregarControlesObligatorios(txtNombreCC, "Nombre Cuenta Corriente");
            AgregarControlesObligatorios(txtSaldo, "Saldo");
        }
        private void FCuentaCorrienteABM_Load(object sender, EventArgs e)
        {
            txtSaldo.Text = "0";
            var cliente = _clienteServicio.ObtenerClientePorId(ClienteID);

            lblNombreCliente.Text = cliente.NombreCompleto;

            if (cliente != null)
            {
                var inicialNombre = string.IsNullOrWhiteSpace(cliente.Nombre)
     ? ""
     : cliente.Nombre.Trim()[0].ToString().ToUpper();

                var apellido = (cliente.Apellido ?? "")
                    .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                    .FirstOrDefault() ?? "";

                var codigo = DateTime.Now.ToString("HHmmss");

                txtNombreCC.Text = $"{inicialNombre}{apellido} - {codigo}";
            }

            if (cliente != null && !string.IsNullOrEmpty(cliente.Dni))
            {

                _dnisAutorizadosLista.Add(long.Parse(cliente.Dni));
            }

            ActualizarPantalla();
        }
        // 🔹 Método para enlazar la lista al ListBox
        private void InicializarListaDni()
        {
            _dnisAutorizadosLista = new BindingList<long>();
            lstDnis.DataSource = _dnisAutorizadosLista;
        }

        public override void FBaseABM_Load(object sender, EventArgs e)
        {
            base.FBaseABM_Load(sender, e);
            Inicializador(EntidadID);
        }

        public override void Inicializador(long? entidadId)
        {
        }

        public override void CargarDatos(long? entidadId)
        {
            if (!entidadId.HasValue)
            {
                MessageBox.Show(@"Ocurrio un Error Grave", @"Error Grave", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                this.Close();
                return;
            }

            if (TipoOperacion == TipoOperacion.Eliminar)
            {
                btnLimpiar.Enabled = false;
            }

            var cuentacorriente = _cuentacorrienteServicio.ObtenerCuentaCorrientePorId(entidadId.Value);

            txtNombreCC.Text = cuentacorriente.NombreCuentaCorriente;
            txtSaldo.Text = cuentacorriente.Saldo.ToString();
            //dtpFechaVencimiento.Value = (DateTime)cuentacorriente.FechaVencimiento;
            chkLimiteDeuda.Checked = cuentacorriente.LimiteDeudaActivo;
            txtLimiteDeuda.Text = cuentacorriente.LimiteDeuda.ToString();
            txtLimiteDeuda.Enabled = cuentacorriente.LimiteDeudaActivo;
            rbVencimientoMensual.Checked = cuentacorriente.TipoVencimiento == (int)TipoVencimientoCuentaCorriente.Mensual;

            rbVencimientoManual.Checked = cuentacorriente.TipoVencimiento == (int)TipoVencimientoCuentaCorriente.Manual;

            nudCantidadMeses.Value =
                cuentacorriente.CantidadMesesVencimiento;
            // 🔹 Mapeo directo a la lista del ListBox sin dar vueltas con celdas
            _dnisAutorizadosLista.Clear();
            foreach (var dni in cuentacorriente.DniAutorizados)
            {
                _dnisAutorizadosLista.Add(dni);
            }
            ActualizarPantalla();
        }

        public override bool EjecutarComandoNuevo()
        {
            if (string.IsNullOrEmpty(txtSaldo.Text))
            {
                txtSaldo.Text = "0";
            }
            if (!VerificarDatosObligatorios())
            {
                MessageBox.Show(@"Por favor ingrese los campos Obligatorios.", @"Atención", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (!decimal.TryParse(txtSaldo.Text?.Trim(), out var saldo))
            {
                MessageBox.Show("Saldo inválido.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            decimal? limite = null;

            if (chkLimiteDeuda.Checked)
            {
                if (decimal.TryParse(txtLimiteDeuda.Text, out var l))
                    limite = l;
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
                ClienteId = EntidadID.Value, // Asumimos que el ID del cliente se pasa al formulario y se usa para crear la cuenta corriente
                NombreCuentaCorriente = txtNombreCC.Text,
                Saldo = saldo,
                TipoVencimiento = (int)tipoVencimiento,
                CantidadMesesVencimiento = cantidadMeses,
                LimiteDeudaActivo = chkLimiteDeuda.Checked,
                LimiteDeuda = limite ?? 0,
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
            if (!EntidadID.HasValue)
            {
                MessageBox.Show(@"´Por favor seleccione un cuentacorriente válido.", @"Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }
            if (TipoOperacion == TipoOperacion.Eliminar)
            {
                var response = _cuentacorrienteServicio.Eliminar((long)EntidadID);
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
            if (!EntidadID.HasValue)
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

            decimal? limite = null;

            if (chkLimiteDeuda.Checked)
            {
                if (decimal.TryParse(txtLimiteDeuda.Text, out var l))
                    limite = l;
            }

            if (TipoOperacion == TipoOperacion.Modificar)
            {
                var cuentacorrienteEditar = new CuentaCorrienteDTO
                {
                    NombreCuentaCorriente = txtNombreCC.Text,
                    Saldo = Convert.ToDecimal(txtSaldo.Text),
                    //FechaVencimiento = dtpFechaVencimiento.Value,
                    LimiteDeudaActivo = chkLimiteDeuda.Checked,
                    LimiteDeuda = limite ?? 0,
                    TipoVencimiento = (int)tipoVencimiento,
                    CantidadMesesVencimiento = cantidadMeses,

                    // 🔹 Al modificar también usamos la lista del ListBox directamente
                    DniAutorizados = _dnisAutorizadosLista.ToList(),
                    EstaEliminado = false
                };

                var response = _cuentacorrienteServicio.Modificar(cuentacorrienteEditar, EntidadID);

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
            int meses = rbVencimientoMensual.Checked
                ? 1
                : (int)nudCantidadMeses.Value;

            var fechaBase = DateTime.Today;

            var proximo = fechaBase.AddMonths(meses);

            lblFechaVTO.Text = $"Próximo vencimiento: {proximo:dd/MM/yyyy}";
        }
        private void ActualizarPantalla()
        {
            txtLimiteDeuda.Enabled = chkLimiteDeuda.Checked;
            nudCantidadMeses.Enabled = rbVencimientoManual.Checked;

            nudCantidadMeses.Minimum = rbVencimientoMensual.Checked ? 1 : 2;

            if (rbVencimientoMensual.Checked)
            {
                nudCantidadMeses.Value = 1;
            }
            else if (nudCantidadMeses.Value < 2)
            {
                nudCantidadMeses.Value = 2;
            }

            ActualizarProximoVencimiento();
        }

        private void rbVencimientoMensual_CheckedChanged(object sender, EventArgs e)
        {
            ActualizarPantalla();
        }

        private void rbVencimientoManual_CheckedChanged(object sender, EventArgs e)
        {
            ActualizarPantalla();
        }


        private bool ValidarSaldoYLimite()
        {
            decimal.TryParse(txtSaldo.Text, out var saldo);

            decimal.TryParse(txtLimiteDeuda.Text, out var limite);

            bool tieneSaldoAFavor = saldo > 0;

            bool deudaInfinita =
                chkLimiteDeuda.Checked &&
                string.IsNullOrWhiteSpace(txtLimiteDeuda.Text);

            bool deudaConLimite =
                chkLimiteDeuda.Checked &&
                limite > 0;

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
    }
}