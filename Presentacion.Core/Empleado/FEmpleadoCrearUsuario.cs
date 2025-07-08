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
            /*var respuesta = MessageBox.Show($"¿Desea crear manual o con el formato Jorge Pérez {rolDisponibleSeleccionado.Nombre.ToUpper()} del empleado {empleado.Nombre} {empleado.Apellido} (usuario: {empleado.Username})?",
            "Confirmar acción",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Question);
            if (respuesta == DialogResult.Yes)*/
            // CrearNombreUsuario(_empleadoDto.Apellido, _empleadoDto.Nombre, _empleadoDto.PersonaId);
        }
        private void CrearNombreUsuario(string apellido, string nombre, long empleadoId)
        {
            var response = _usuarioServicio.CrearUsuario(nombre, apellido, empleadoId);
            MessageBox.Show(response.Mensaje, "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void cbxHabilitarEdicionNombre_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void btnCrearUsuario_Click(object sender, EventArgs e)
        {

        }
    }
}
