using Presentacion.Core.Presentacion.Core.Helpers;
using Presentacion.FBase;
using Presentacion.FBase.Helpers;
using Presentacion.FormulariosBase.Helpers;
using Servicios.Helpers.Producto;
using Servicios.Helpers.Sistema.FiltrosConsulta;
using Servicios.LogicaNegocio.Empleado.Rol;
using System;
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
            soloSeleccion = false;
        }

        public FRolConsulta(bool soloSeleccion)
        {
            InitializeComponent();
            this.soloSeleccion = soloSeleccion;
            _rolServicio = new RolServicio();

            if (soloSeleccion)
                MessageBox.Show("Seleccione el rol con doble click");
        }

        #region 🔥 ACCIONES DINAMICAS (si querés migrar botones al lateral nuevo)

        protected override void ConfigurarAccionesPersonalizadas()
        {
            AgregarAccion(
                "Asignar Roles",
                SystemIcons.Shield.ToBitmap(),
                (id) =>
                {
                    var f = new FAsignacionRolesEmpleados(TipoAsignacionRol.Nuevo);
                    f.ShowDialog();
                },
                false // no requiere fila seleccionada
            );
        }

        #endregion

        #region BOTONES BASE

        public override void EjecutarBtnNuevo()
        {
            var f = new FRolABM(TipoOperacion.Nuevo);
            f.ShowDialog();
            ActualizarGrillaBase();
        }

        public override void EjecutarBtnModificar()
        {
            base.EjecutarBtnModificar();
            if (!puedeEjecutarComando) return;

            var f = new FRolABM(TipoOperacion.Modificar, entidadID);
            f.ShowDialog();
            ActualizarSegunOperacion(f.RealizoAlgunaOperacion);
        }

        public override void EjecutarBtnEliminar()
        {
            base.EjecutarBtnEliminar();
            if (!puedeEjecutarComando) return;

            var f = new FRolABM(TipoOperacion.Eliminar, entidadID);
            f.ShowDialog();
            ActualizarSegunOperacion(f.RealizoAlgunaOperacion);
        }

        #endregion

        #region GRILLA

        public override void ResetearGrilla(DataGridView grilla)
        {
            base.ResetearGrilla(grilla);

            if (grilla.Columns.Count == 0) return;

            if (grilla.Columns.Contains("RolId"))
            {
                grilla.Columns["RolId"].Visible = false;
                grilla.Columns["RolId"].Name = "Id";
            }

            if (grilla.Columns.Contains("Nombre"))
            {
                grilla.Columns["Nombre"].Visible = true;
                grilla.Columns["Nombre"].HeaderText = "Rol";
                grilla.Columns["Nombre"].Width = 120;
            }

            if (grilla.Columns.Contains("CodigoRol"))
            {
                grilla.Columns["CodigoRol"].Visible = true;
                grilla.Columns["CodigoRol"].Width = 120;
                grilla.Columns["CodigoRol"].HeaderText = "Código";
            }

            if (grilla.Columns.Contains("DetalleRol"))
            {
                grilla.Columns["DetalleRol"].Visible = true;
                grilla.Columns["DetalleRol"].HeaderText = "Detalle";
                grilla.Columns["DetalleRol"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
        }


        public override void ActualizarDatos(DataGridView dgv, FiltroConsulta filtros)
        {
            base.ActualizarDatos(dgv, filtros);

            filtros.Extra ??= "ApyNom";

            var resultado = _rolServicio.ObtenerRoles(filtros);

            dgv.DataSource = resultado.Items;

            // 🔴 CLAVE: volver a aplicar formato
            ResetearGrilla(dgv);

            var paginacion = new DatosPaginacion
            {
                PaginaActual = resultado.Page,
                PageSize = resultado.PageSize,
                CantidadRegistros = resultado.TotalRegistros,
            };

            ActualizarPaginacionUI(paginacion);

            BarraLateralBotones.Enabled = !filtros.VerEliminados;
        }

        #endregion

        #region REFRESH

        private void ActualizarSegunOperacion(bool realizoOperacion)
        {
            if (realizoOperacion)
                ActualizarGrillaBase();
        }

        private void ActualizarGrillaBase()
        {
            //ActualizarDatos(dgvGrilla, txtBuscar?.Text ?? "", cbxEstaEliminado, BarraLateralBotones);
        }

        #endregion
        protected override void ConfigurarFiltrosUI()
        {

            base.ConfigurarFiltrosUI();

            ActivarFiltroEliminados("Mostrar roles eliminados.");

            var opciones = new List<OpcionFiltro>
            {
                new OpcionFiltro { Texto = "Todos", Valor = "" },
                new OpcionFiltro { Texto = "Nombre", Valor = "Nombre" },
                new OpcionFiltro { Texto = "Detalle", Valor = "DetalleRol" },
                new OpcionFiltro { Texto = "Código", Valor = "CodigoRol" }
            };

            ActivarFiltroCombo(opciones, "Texto", "Valor");


            cbxFiltroOpcional.SelectedValue = "";
        }

        protected override string ObtenerTextoLabelFiltroOpcional()
        {
            return "Buscar rol por:";
        }

        protected override string ObtenerTextoLabelBusqueda()
        {
            return "Buscar rol:";
        }

    }
}
