using Presentacion.Core.Empleado.Rol.Permisos;
using Presentacion.Core.Presentacion.Core.Helpers;
using Presentacion.FBase;
using Presentacion.FBase.Helpers;
using Presentacion.FormulariosBase.Helpers;
using Servicios.Helpers.Sistema.FiltrosConsulta;
using Servicios.LogicaNegocio.Empleado.Rol;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Presentacion.Core.Empleado.Rol
{
    public partial class FRolConsulta : FBaseConsulta
    {
        private readonly IRolServicio _rolServicio;

        public long? rolSeleccionado = null;

        public bool soloSeleccion;

        public FRolConsulta() : this(new RolServicio())
        {
            InitializeComponent();

            soloSeleccion = false;
        }

        public FRolConsulta(IRolServicio rolServicio)
        {
            _rolServicio = rolServicio;

            InitializeComponent();

            soloSeleccion = false;
        }

        public FRolConsulta(bool soloSeleccion)
        {
            InitializeComponent();

            this.soloSeleccion = soloSeleccion;

            _rolServicio = new RolServicio();

            if (soloSeleccion)
            {
                MessageBox.Show("Seleccione el rol con doble click");
            }
        }

        #region 🔥 ACCIONES DINAMICAS

        protected override void ConfigurarAccionesPersonalizadas()
        {
            //AgregarAccion(
            //    "Asignar Roles",
            //    SystemIcons.Shield.ToBitmap(),
            //    (id) =>
            //    {
            //        var f = new FAsignacionRolesEmpleados(TipoAsignacionRol.Nuevo);

            //        f.ShowDialog();
            //    },
            //    false
            //);

            AgregarAccion(
                "Asignar Permisos",
                Constantes.Imagenes.ImgPermisos,
                AbrirAsignacionPermisos,
                true
            );
        }

        private void AbrirAsignacionPermisos(long? id)
        {
            if (!id.HasValue)
            {
                MessageBox.Show("Seleccione un rol.");
                return;
            }

            var f = new FAsignacionPermisosRol(id);

            f.ShowDialog();
        }

        #endregion

        #region 🔷 FILTROS
        protected override string TextoLblBuscar
     => "Buscar Rol:";

        protected override string TextoLblCbx1
            => "Filtrar por Propiedad";

        protected override string TextoLblCbx2
            => "Filtrar por";

        protected override string TextoLblCbx3
            => "Filtrar por";
        protected override string TextoTitular
          => "Listado de los Roles";
        protected override void ConfigurarFiltrosUI()
        {
            base.ConfigurarFiltrosUI();

            var opciones = new List<OpcionFiltro>
            {
                new OpcionFiltro
                {
                    Texto = "Todos",
                    Valor = ""
                },

                new OpcionFiltro
                {
                    Texto = "Nombre",
                    Valor = "Nombre"
                },

                new OpcionFiltro
                {
                    Texto = "Detalle",
                    Valor = "DetalleRol"
                },

                new OpcionFiltro
                {
                    Texto = "Código",
                    Valor = "CodigoRol"
                }
            };

            ActivarCombo(
                cbx1,
                lblcbx1,
                opciones,
                "Texto",
                "Valor",
                "Buscar rol por"
            );

            ActivarCheck(
                chkBool1,
                "Ver eliminados"
            );

            cbx1.SelectedValue = "";
        }

        #endregion

        #region 🔥 DATOS

        public override void ActualizarDatos(DataGridView dgv, FiltroConsulta filtros)
        {
            base.ActualizarDatos(dgv, filtros);

            var resultado = _rolServicio.ObtenerRoles(filtros);

            dgv.DataSource = resultado.Items;

            ResetearGrilla(dgv);

            var paginacion = new DatosPaginacion
            {
                PaginaActual = resultado.Page,
                PageSize = resultado.PageSize,
                CantidadRegistros = resultado.TotalRegistros
            };

            ActualizarPaginacionUI(paginacion);

            BarraLateralBotones.Enabled = !filtros.Bool1;
        }

        #endregion

        #region 🔷 GRILLA

        public override void ResetearGrilla(DataGridView grilla)
        {
            base.ResetearGrilla(grilla);

            if (grilla.Columns.Count == 0)
                return;

            if (grilla.Columns.Contains("RolId"))
            {
                grilla.Columns["RolId"].Visible = false;
                grilla.Columns["RolId"].Name = "Id";
            }

            if (grilla.Columns.Contains("Nombre"))
            {
                grilla.Columns["Nombre"].Visible = true;
                grilla.Columns["Nombre"].HeaderText = "Rol";
                grilla.Columns["Nombre"].Width = 180;
            }

            if (grilla.Columns.Contains("CodigoRol"))
            {
                grilla.Columns["CodigoRol"].Visible = true;
                grilla.Columns["CodigoRol"].HeaderText = "Código";
                grilla.Columns["CodigoRol"].Width = 140;
            }

            if (grilla.Columns.Contains("DetalleRol"))
            {
                grilla.Columns["DetalleRol"].Visible = true;
                grilla.Columns["DetalleRol"].HeaderText = "Detalle";
                grilla.Columns["DetalleRol"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
        }

        #endregion

        #region 🔷 BOTONES BASE

        public override void EjecutarBtnNuevo()
        {
            var f = new FRolABM(TipoOperacion.Nuevo);

            f.ShowDialog();

            if (f.RealizoAlgunaOperacion)
            {
                RefrescarGrilla();
            }
        }

        public override void EjecutarBtnModificar()
        {
            base.EjecutarBtnModificar();

            if (!puedeEjecutarComando)
                return;

            var f = new FRolABM(TipoOperacion.Modificar, entidadID);

            f.ShowDialog();

            if (f.RealizoAlgunaOperacion)
            {
                RefrescarGrilla();
            }
        }

        public override void EjecutarBtnEliminar()
        {
            base.EjecutarBtnEliminar();

            if (!puedeEjecutarComando)
                return;

            var f = new FRolABM(TipoOperacion.Eliminar, entidadID);

            f.ShowDialog();

            if (f.RealizoAlgunaOperacion)
            {
                RefrescarGrilla();
            }
        }

        #endregion

        #region 🔷 DOBLE CLICK

        public override void EjecutarDobleClickFila(long? id)
        {
            if (!soloSeleccion)
                return;

            if (!id.HasValue)
                return;

            rolSeleccionado = id;

            DialogResult = DialogResult.OK;

            Close();
        }

        #endregion
    }
}