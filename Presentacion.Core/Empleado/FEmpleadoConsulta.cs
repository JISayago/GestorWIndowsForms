using Presentacion.Core.Empleado.Rol;
using Presentacion.FBase;
using Presentacion.FBase.Helpers;
using Presentacion.FormulariosBase.Helpers;
using Servicios.LogicaNegocio.Empleado;
using System.Drawing;
using System.Windows.Forms;

namespace Presentacion.Core.Empleado
{
    public partial class FEmpleadoConsulta : FBaseConsulta
    {
        private readonly IEmpleadoServicio _empleadoServicio;

        public long? empleadoSeleccionado = null;
        public bool soloSeleccion;
        public bool vieneDeCargaVendedor = false;

        public FEmpleadoConsulta() : this(new EmpleadoServicio())
        {
            InitializeComponent();
            soloSeleccion = false;
        }

        public FEmpleadoConsulta(IEmpleadoServicio empleadoServicio)
        {
            _empleadoServicio = empleadoServicio;
            InitializeComponent();
            soloSeleccion = false;
        }

        public FEmpleadoConsulta(bool _vieneDeCargaVendedor) : this(new EmpleadoServicio())
        {
            InitializeComponent();

            vieneDeCargaVendedor = _vieneDeCargaVendedor;

        }

        #region 🔷 ACCIONES DINAMICAS NUEVAS

        protected override void ConfigurarAccionesPersonalizadas()
        {
            AgregarAccion(
                "Roles",
                Constantes.Imagenes.ImgModificar,
                AbrirAsignacionRoles,
                true
            );

            AgregarAccion(
                "Crear Usuario",
                Constantes.Imagenes.ImgNuevo,
                AbrirCrearUsuario,
                true
            );
            if (vieneDeCargaVendedor)
            {

            AgregarAccion(
                "Asignar Vendedor",
                Constantes.Imagenes.ImgNuevo,
                AsignarVendedor,
                true
            );
            }
        }

        private void AbrirAsignacionRoles(long? id)
        {
            if (!id.HasValue) return;

            var f = new FAsignacionRolesEmpleados(TipoAsignacionRol.Existente, id);
            f.ShowDialog();
        }

        private void AbrirCrearUsuario(long? id)
        {
            if (!id.HasValue) return;

            var f = new FEmpleadoCrearUsuario(id);
            f.ShowDialog();
        }
        private void AsignarVendedor(long? id)
        {
            if (!entidadID.HasValue)
            {
                MessageBox.Show("Seleccione un empleado.");
                return;
            }

            empleadoSeleccionado = entidadID;
            DialogResult = DialogResult.OK;
            Close();
        }

        #endregion

        #region 🔷 GRILLA

        public override void ResetearGrilla(DataGridView grilla)
        {
            base.ResetearGrilla(grilla);

            if (grilla.Columns.Contains("PersonaId"))
            {
                grilla.Columns["PersonaId"].Visible = false;
                grilla.Columns["PersonaId"].Name = "Id";
            }

            grilla.Columns["Legajo"].Visible = true;
            grilla.Columns["Legajo"].Width = 80;

            grilla.Columns["Nombre"].Visible = true;
            grilla.Columns["Nombre"].Width = 100;

            grilla.Columns["Apellido"].Visible = true;
            grilla.Columns["Apellido"].Width = 100;

            grilla.Columns["Username"].Visible = true;
            grilla.Columns["Username"].HeaderText = "Usuario";
            grilla.Columns["Username"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            grilla.Columns["DNI"].Visible = true;
            grilla.Columns["DNI"].Width = 100;

            grilla.Columns["Email"].Visible = true;
            grilla.Columns["Email"].Width = 130;

            grilla.Columns["Telefono"].Visible = true;
            grilla.Columns["Telefono"].Width = 100;

            grilla.Columns["EstadoDescripcion"].Visible = true;
            grilla.Columns["EstadoDescripcion"].Width = 100;
            grilla.Columns["EstadoDescripcion"].HeaderText = "Estado";
        }

        #endregion

        #region 🔥 ACTUALIZAR DATOS (NUEVO SISTEMA)

        public override void ActualizarDatos(DataGridView dgv, FiltroConsulta filtros)
        {
            base.ActualizarDatos(dgv, filtros);

            if (filtros.VerEliminados)
            {
                dgv.DataSource = _empleadoServicio.ObtenerEmpleadosEliminados(filtros.TextoBuscar);
                BarraLateralBotones.Enabled = false;
            }
            else
            {
                dgv.DataSource = _empleadoServicio.ObtenerEmpleados(filtros.TextoBuscar);
                BarraLateralBotones.Enabled = true;
            }
        }

        #endregion

        #region 🔷 BOTONES BASE

        public override void EjecutarBtnNuevo()
        {
            var f = new FEmpleadoABM(TipoOperacion.Nuevo);
            f.ShowDialog();

            if (f.RealizoAlgunaOperacion)
                Recargar();
        }

        public override void EjecutarBtnModificar()
        {
            base.EjecutarBtnModificar();
            if (!puedeEjecutarComando) return;

            var f = new FEmpleadoABM(TipoOperacion.Modificar, entidadID);
            f.ShowDialog();

            if (f.RealizoAlgunaOperacion)
                Recargar();
        }

        public override void EjecutarBtnEliminar()
        {
            base.EjecutarBtnEliminar();
            if (!puedeEjecutarComando) return;

            var f = new FEmpleadoABM(TipoOperacion.Eliminar, entidadID);
            f.ShowDialog();

            if (f.RealizoAlgunaOperacion)
                Recargar();
        }

        private void Recargar()
        {
            //btnActualizar_Click_Base();
        }

        #endregion

    }
}
