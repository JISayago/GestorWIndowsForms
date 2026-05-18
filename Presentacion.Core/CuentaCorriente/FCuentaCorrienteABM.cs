using AccesoDatos.Entidades;
using Presentacion.FBase;
using Presentacion.FormulariosBase.Helpers;
using Servicios.Helpers.Cliente;
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

            dtpFechaVencimiento.MinDate = DateTime.Now;

            var filtros = new FiltroConsulta
            {
                TextoBuscar = null,
                Filtro1 = null,
                Filtro2 = ((int)TipoFiltroCliente.Activo).ToString(),
                Bool1 = false,
                Bool2 = false,
                FechaDesde = null,
                FechaHasta = null,
                Filtro3 = null,
                Page = 1,
                PageSize = 50
            };

            var clientes = _clienteServicio.ObtenerClientes(filtros).Items;

            cmbClientes.DisplayMember = "NombreCompleto";
            cmbClientes.ValueMember = "PersonaId";
            cmbClientes.DataSource = clientes;

            cmbClientes.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cmbClientes.AutoCompleteSource = AutoCompleteSource.ListItems;
            cmbClientes.DropDownStyle = ComboBoxStyle.DropDown;

            AgregarControlesObligatorios(txtNombreCC, "Nombre Cuenta Corriente");
            AgregarControlesObligatorios(txtSaldo, "Saldo");
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
            var filtros = new FiltroConsulta
            {
                TextoBuscar = null,
                Filtro1 = null,
                Filtro2 = ((int)TipoFiltroCliente.Activo).ToString(),
                Bool1 = false,
                Bool2 = false,
                FechaDesde = null,
                FechaHasta = null,
                Filtro3 = null,
                Page = 1,
                PageSize = 50
            };

            var resultado = _clienteServicio.ObtenerClientes(filtros);
            var clienteDeCuentaCorriente = resultado.Items.FirstOrDefault();

            txtNombreCC.Text = cuentacorriente.NombreCuentaCorriente;
            txtSaldo.Text = cuentacorriente.Saldo.ToString();
            dtpFechaVencimiento.Value = (DateTime)cuentacorriente.FechaVencimiento;
            chkLimiteDeuda.Checked = cuentacorriente.LimiteDeudaActivo;
            txtLimiteDeuda.Text = cuentacorriente.LimiteDeuda.ToString();
            txtLimiteDeuda.Enabled = cuentacorriente.LimiteDeudaActivo;
            cmbClientes.DisplayMember = "NombreCompleto";
            cmbClientes.ValueMember = "PersonaId";
            cmbClientes.DataSource = clienteDeCuentaCorriente;
            cmbClientes.Enabled = false;

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

            var nuevoCuentaCorriente = new CuentaCorrienteDTO
            {
                NombreCuentaCorriente = txtNombreCC.Text,
                Saldo = Convert.ToDecimal(txtSaldo.Text),
                FechaVencimiento = dtpFechaVencimiento.Value,
                LimiteDeudaActivo = chkLimiteDeuda.Checked,
                LimiteDeuda = Convert.ToDecimal(txtLimiteDeuda.Text),
                ClienteId = (long)cmbClientes.SelectedValue,

                // 🔹 Directamente le pasamos la lista limpia convertida a List<long>
                DniAutorizados = _dnisAutorizadosLista.ToList(),
                EstaEliminado = false,
            };

            var response = _cuentacorrienteServicio.Insertar(nuevoCuentaCorriente);

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
                    FechaVencimiento = dtpFechaVencimiento.Value,
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

        private void FCuentaCorrienteABM_Load(object sender, EventArgs e)
        {
        }
    }
}