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

            lblFechaVTO.Text = DateTime.Now.ToString();

            //var clientes = _clienteServicio.ObtenerClientes(filtros).Items;



            AgregarControlesObligatorios(txtNombreCC, "Nombre Cuenta Corriente");
            AgregarControlesObligatorios(txtSaldo, "Saldo");
        }
        private void FCuentaCorrienteABM_Load(object sender, EventArgs e)
        {
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

            // 🔹 Mapeo directo a la lista del ListBox sin dar vueltas con celdas
            _dnisAutorizadosLista.Clear();
            foreach (var dni in cuentacorriente.DniAutorizados)
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
            if (!decimal.TryParse(txtSaldo.Text?.Trim(), out var saldo))
            {
                MessageBox.Show("Saldo inválido.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            decimal.TryParse(txtLimiteDeuda.Text?.Trim(), out var limiteDeuda); // devuelve 0 si falla


            var tipoVencimiento = rbVencimientoMensual.Checked
                ? TipoVencimientoCuentaCorriente.Mensual
                : TipoVencimientoCuentaCorriente.Manual;

            var cantidadMeses = rbVencimientoMensual.Checked
                ? 1
                : (int)nudCantidadMeses.Value;

            var nuevoCuentaCorriente = new CuentaCorrienteDTO
            {
                ClienteId = EntidadID.Value, // Asumimos que el ID del cliente se pasa al formulario y se usa para crear la cuenta corriente
                NombreCuentaCorriente = txtNombreCC.Text,
                Saldo = saldo,
                //FechaVencimiento = dtpFechaVencimiento.Value,
                LimiteDeudaActivo = chkLimiteDeuda.Checked,
                LimiteDeuda = chkLimiteDeuda.Checked && decimal.TryParse(txtLimiteDeuda.Text, out var l) ? l : 0m,
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
            if (TipoOperacion == TipoOperacion.Modificar)
            {
                var cuentacorrienteEditar = new CuentaCorrienteDTO
                {
                    NombreCuentaCorriente = txtNombreCC.Text,
                    Saldo = Convert.ToDecimal(txtSaldo.Text),
                    //FechaVencimiento = dtpFechaVencimiento.Value,
                    LimiteDeudaActivo = chkLimiteDeuda.Checked,
                    LimiteDeuda = Convert.ToDecimal(txtLimiteDeuda.Text),

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

        private void chkbLimiteDeuda_CheckedChanged(object sender, EventArgs e)
        {
            if (chkLimiteDeuda.Checked)
            {
                txtLimiteDeuda.Enabled = true;
                if (string.IsNullOrWhiteSpace(txtLimiteDeuda.Text))
                    txtLimiteDeuda.Text = "0";
            }
            else
            {
                txtLimiteDeuda.Enabled = false;
                txtLimiteDeuda.Text = "0";
            }
        }

        private void rbVencimientoMensual_CheckedChanged(object sender, EventArgs e)
        {
            if (!rbVencimientoMensual.Checked)
                return;

            nudCantidadMeses.Enabled = false;
            nudCantidadMeses.Value = 1;
        }

        private void rbVencimientoManual_CheckedChanged(object sender, EventArgs e)
        {
            if (!rbVencimientoManual.Checked)
                return;

            nudCantidadMeses.Enabled = true;

            if (nudCantidadMeses.Value < 2)
                nudCantidadMeses.Value = 2;

        }
    }
}