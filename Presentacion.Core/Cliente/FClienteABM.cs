using Presentacion.FBase;
using Presentacion.FormulariosBase.Helpers;
using Servicios.LogicaNegocio.Cliente;
using Servicios.LogicaNegocio.Cliente.DTO;

namespace Presentacion.Core.Cliente
{
    public partial class FClienteABM : FBaseABM
    {
        public FClienteABM()
        {
            InitializeComponent();
        }
        private readonly IClienteServicio _clienteServicio;

        public override void FBaseABM_Load(object sender, EventArgs e)
        {
            base.FBaseABM_Load(sender, e);
            Inicializador(EntidadID);
        }
        public FClienteABM(TipoOperacion tipoOperacion, long? entidadID = null) : base(tipoOperacion, entidadID)
        {
            InitializeComponent();
            _clienteServicio = new ClienteServicio();

            if (tipoOperacion == TipoOperacion.Eliminar || tipoOperacion == TipoOperacion.Modificar)
            {
                CargarDatos(entidadID);
            }

            if (tipoOperacion == TipoOperacion.Eliminar)
            {
                DesactivarControles(this);
            }

            AgregarControlesObligatorios(txtApellido, "Apellido");
            AgregarControlesObligatorios(txtNombre, "Nombre");
            AgregarControlesObligatorios(txtDni, "Dni");
            AgregarControlesObligatorios(txtTelefono, "Telefono");
            AgregarControlesObligatorios(txtCuil, "Cuil");
            AgregarControlesObligatorios(txtEmail, "Email");
            AgregarControlesObligatorios(txtDireccion, "Direccion");
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
            }

            if (TipoOperacion == TipoOperacion.Eliminar)
            {
                btnLimpiar.Enabled = false;
            }

            var cliente = _clienteServicio.ObtenerClientePorId(entidadId.Value);

            // Datos Personales
            txtApellido.Text = cliente.Apellido;
            txtNombre.Text = cliente.Nombre;
            txtDireccion.Text = cliente.Direccion;
            txtCelular.Text = cliente.Telefono2;
            txtCuil.Text = cliente.Cuil;
            txtDni.Text = cliente.Dni;
            txtEmail.Text = cliente.Email;
            dtpFNacimiento.Value = (DateTime)cliente.FechaNacimiento;
            txtTelefono.Text = cliente.Telefono;
        }


        public override bool EjecutarComandoNuevo()
        {

            if (!VerificarDatosObligatorios())
            {
                MessageBox.Show(@"Por favor ingrese los campos Obligatorios.", @"Atención", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return false;
            }

            var nuevoCliente = new ClienteDTO
            {
                Apellido = txtApellido.Text,
                Nombre = txtNombre.Text,
                Direccion = txtDireccion.Text,
                Telefono = txtCelular.Text,
                Cuil = txtCuil.Text,
                Dni = txtDni.Text,
                Email = txtEmail.Text,
                FechaNacimiento = dtpFNacimiento.Value,
                Telefono2 = txtCelular.Text,
                NumeroCliente = "0", //Asignar un valor adecuado si es necesario
                Estado = 1, //Activo por defecto
                EstaEliminado = false,
            };

            var response = _clienteServicio.Insertar(nuevoCliente);

            if (response.Exitoso)
            {
                MessageBox.Show($"{response.Mensaje}", @"Atención", MessageBoxButtons.OK,
                   MessageBoxIcon.Information);
                return true;
            }
            else
            {
                MessageBox.Show($"{response.Mensaje}", @"Atención", MessageBoxButtons.OK,
                   MessageBoxIcon.Error);
                return false;
            }
        }

        public override bool EjecutarComandoEliminar()
        {
            if (!EntidadID.HasValue)
            {
                MessageBox.Show(@"´Por favor seleccione un cliente válido.", @"Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;

            }
            if (TipoOperacion == TipoOperacion.Eliminar)
            {
                var response = _clienteServicio.Eliminar((long)EntidadID);
                if (response.Exitoso)
                {
                    MessageBox.Show($"{response.Mensaje}", @"Atención", MessageBoxButtons.OK,
                       MessageBoxIcon.Information);
                    return true;
                }
                else
                {
                    MessageBox.Show($"{response.Mensaje}", @"Atención", MessageBoxButtons.OK,
                       MessageBoxIcon.Error);
                    return false;
                }

            }
            return false;
        }
        public override bool EjecutarComandoModificar()
        {
            if (!EntidadID.HasValue)
            {
                MessageBox.Show(@"´Por favor seleccione un cliente válido.", @"Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;

            }
            if (TipoOperacion == TipoOperacion.Modificar)
            {

                var clienteEditar = new ClienteDTO
                {
                    Apellido = txtApellido.Text,
                    Nombre = txtNombre.Text,
                    Direccion = txtDireccion.Text,
                    Telefono = txtCelular.Text,
                    Cuil = txtCuil.Text,
                    Dni = txtDni.Text,
                    Email = txtEmail.Text,
                    FechaNacimiento = dtpFNacimiento.Value,
                    Telefono2 = txtCelular.Text,
                    //Estado = chkEstado,  agregar estado en front al modificar cliente
                    EstaEliminado = false
                };

                var response = _clienteServicio.Modificar(clienteEditar, EntidadID);

                if (response.Exitoso)
                {
                    MessageBox.Show($"{response.Mensaje}", @"Atención", MessageBoxButtons.OK,
                       MessageBoxIcon.Information);
                    return true;
                }
                else
                {
                    MessageBox.Show($"{response.Mensaje}", @"Atención", MessageBoxButtons.OK,
                       MessageBoxIcon.Error);
                    return false;
                }

            }
            return false;
        }
    }
}
