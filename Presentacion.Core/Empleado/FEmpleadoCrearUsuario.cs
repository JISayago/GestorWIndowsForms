using Servicios.LogicaNegocio.Empleado.Rol;
using Servicios.LogicaNegocio.Empleado;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Presentacion.FBase.Helpers;
using Servicios.LogicaNegocio.Empleado.DTO;
using AccesoDatos.Entidades;
using Presentacion.Core.Empleado;
using Servicios.Helpers;

namespace Presentacion.Core.Empleado
{
    public partial class FEmpleadoCrearUsuario : FBase.FBase
    {
        protected long? EntidadID;
        public bool RealizoAlgunaOperacion;
        private EmpleadoDTO _empleadoDto;

        private readonly IEmpleadoServicio _empleadoServicio;
        private readonly IUsuarioServicio _usuarioServicio;
        public FEmpleadoCrearUsuario(long? entidadID = null)
        {
            _empleadoServicio = new EmpleadoServicio();
            _usuarioServicio = new UsuarioServicio();
            EntidadID = entidadID;
            RealizoAlgunaOperacion = false;

            _empleadoDto = _empleadoServicio.ObtenerEmpleadoPorId((long)EntidadID);

            InitializeComponent();
        }

        private void FEmpleadoCrearUsuario_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(_empleadoDto.Username))
            {
                MessageBox.Show($"El empleado {_empleadoDto.Nombre} {_empleadoDto.Apellido} ya cuenta con el usuario asignado: {_empleadoDto.Username}", "Usuario Existente", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }

            var respuesta = _usuarioServicio.GeneracionNombreUsuario(_empleadoDto.Nombre,_empleadoDto.Apellido,_empleadoDto.PersonaId);
            if(respuesta.Estado != 0)
            {
                txtNombreUsuario.Enabled = false;
                txtNombreUsuario.Text = respuesta.Username;
            }
            
        }
             private void cbxHabilitarEdicionNombre_CheckedChanged(object sender, EventArgs e)
        {
            txtNombreUsuario.Enabled = true;
        }

        private void btnCrearUsuario_Click(object sender, EventArgs e)
        {
            CrearUsuario();
        }
        public void CrearUsuario()
        {
            if (string.IsNullOrEmpty(txtNombreUsuario.Text))
            {
                MessageBox.Show("Por favor ingrese un nombre de usuario.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (_usuarioServicio.ExisteUsuario(txtNombreUsuario.Text))
            {
                MessageBox.Show("El nombre de usuario ya existe. Por favor, elija otro.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            var usuarioDto = new UsuarioDTO
            {
                PersonaId = _empleadoDto.PersonaId,
                Username = txtNombreUsuario.Text,
                Pass = "123456789",
                Estado = 0
            };
            var estadoOperacion = _usuarioServicio.CrearUsuario(usuarioDto);
            if (estadoOperacion.Exitoso)
            {
                RealizoAlgunaOperacion = true;
                MessageBox.Show("Usuario creado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                MessageBox.Show("Recuerde que debe actualizar la contraseña en el primer ingreso!.", "Recordatorio", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            else
            {
                MessageBox.Show(estadoOperacion.Mensaje, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
