using Presentacion.Core.Empleado.Rol;
using Presentacion.Core.Presentacion.Core.Helpers;
using Presentacion.FBase;
using Presentacion.FBase.Helpers;
using Presentacion.FormulariosBase.Helpers;
using Servicios.Helpers.Empleado;
using Servicios.Helpers.Sistema.FiltrosConsulta;
using Servicios.LogicaNegocio.Empleado;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Presentacion.Core.Empleado
{
    public partial class FEmpleadoConsulta : FBaseConsulta
    {
        private readonly IEmpleadoServicio _empleadoServicio;
        private readonly IUsuarioServicio _usuarioServicio;

        private readonly ICollection<long> _empleadosConUsuariosBloqueados = new List<long>();

        public long? empleadoSeleccionado = null;

        public bool soloSeleccion;

        public bool vieneDeCargaVendedor = false;

        private bool estaInhabilitado = false;

        private long logeadoID;

        public FEmpleadoConsulta(long logeadoid)
            : this(new EmpleadoServicio(), new UsuarioServicio())
        {
            InitializeComponent();

            soloSeleccion = false;

            logeadoID = logeadoid;
        }

        public FEmpleadoConsulta(
            IEmpleadoServicio empleadoServicio,
            IUsuarioServicio usuarioServicio)
        {
            _empleadoServicio = empleadoServicio;
            _usuarioServicio = usuarioServicio;

            InitializeComponent();

            soloSeleccion = false;

            _empleadosConUsuariosBloqueados =
                _usuarioServicio.ObtenerEmpleadosSinPassIDs();
        }

        public FEmpleadoConsulta(bool _vieneDeCargaVendedor)
            : this(new EmpleadoServicio(), new UsuarioServicio())
        {
            InitializeComponent();

            vieneDeCargaVendedor = _vieneDeCargaVendedor;
        }

        #region ACCIONES DINAMICAS

        protected override void ConfigurarAccionesPersonalizadas()
        {
            AgregarAccion(
                "Roles",
                Constantes.Imagenes.ImgRoles,
                AbrirAsignacionRoles,
                true
            );

            AgregarAccion(
                "Crear Usuario",
                Constantes.Imagenes.ImgCrearUsuario,
                AbrirCrearUsuario,
                true
            );

            AgregarAccion(
                "Asignar Vendedor",
                Constantes.Imagenes.ImgAsignarVendedor,
                AsignarVendedor,
                true
            );

            AgregarAccion(
                "Resetear Contraseña",
                Constantes.Imagenes.ImgResetearContraseña,
                ResetarContraseniaUsuario,
                true
            );
        }

        private void AbrirAsignacionRoles(long? id)
        {
            if (!id.HasValue)
                return;

            var f = new FAsignacionRolesEmpleados(
                TipoAsignacionRol.Existente,
                (long)id);

            f.ShowDialog();
        }

        private void AbrirCrearUsuario(long? id)
        {
            if (!id.HasValue)
                return;

            var f = new FEmpleadoCrearUsuario(id);

            f.ShowDialog();
        }

        private void ResetarContraseniaUsuario(long? id)
        {
            if (!id.HasValue)
                return;

            var resultado = MessageBox.Show(
                "¿Confirma que desea resetear la contraseña del usuario?",
                "Confirmar acción",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (resultado != DialogResult.Yes)
                return;

            var adminId = logeadoID;

            var usuarioDesbloquearID = id.Value;

            var resp = _usuarioServicio.ResetearContra(
                adminId,
                usuarioDesbloquearID);

            MessageBox.Show(resp.Mensaje);
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

        #region FILTROS

        protected override void ConfigurarFiltrosUI()
        {
            base.ConfigurarFiltrosUI();

         
            // =========================================================
            // COMBO BUSQUEDA
            // =========================================================

            var opcionesBusqueda = new List<OpcionFiltro>
    {
        new OpcionFiltro { Texto = "Todos", Valor = "" },
        new OpcionFiltro { Texto = "Nombre", Valor = "ApyNom" },
        new OpcionFiltro { Texto = "Legajo", Valor = "Legajo" },
        new OpcionFiltro { Texto = "Nombre Usuario", Valor = "Usuario" },
        new OpcionFiltro { Texto = "Documento", Valor = "Dni" }
    };

            ActivarCombo(
                cbx1,
                lblcbx1,
                opcionesBusqueda,
                "Texto",
                "Valor",
                "Buscar empleado por:"
            );

            // =========================================================
            // FECHAS (checkbox + rango)
            // =========================================================

            ActivarFiltroFechas("Filtrar por fecha");

            // =========================================================
            // COMBO ESTADO (cbx2)
            // =========================================================

            var estados = new List<OpcionFiltro>
    {
        new OpcionFiltro { Texto = "Todos", Valor = "" },

        new OpcionFiltro
        {
            Texto = "Habilitado",
            Valor = ((int)EstadoEmpleado.Habilitado).ToString()
        },

        new OpcionFiltro
        {
            Texto = "Inhabilitado",
            Valor = ((int)EstadoEmpleado.Inhablitado).ToString()
        },

        new OpcionFiltro
        {
            Texto = "Sin Contraseña",
            Valor = ((int)EstadoEmpleado.SinPass).ToString()
        }
    };

            ActivarCombo(
                cbx2,
                lblcbx2,
                estados,
                "Texto",
                "Valor",
                "Estado"
            );

            // =========================================================
            // COMBO TIPO FECHA (cbx3)
            // =========================================================

            var tiposFecha = new List<OpcionFiltro>
    {
        new OpcionFiltro { Texto = "Todos", Valor = "" },

        new OpcionFiltro
        {
            Texto = "Fecha Ingreso",
            Valor = "Ingreso"
        },

        new OpcionFiltro
        {
            Texto = "Fecha Egreso",
            Valor = "Egreso"
        }
    };

            ActivarCombo(
                cbx3,
                lblcbx3,
                tiposFecha,
                "Texto",
                "Valor",
                "Tipo Fecha"
            );

            // =========================================================
            // DEFAULTS
            // =========================================================

            cbx1.SelectedValue = "";
            cbx2.SelectedValue = "";
            cbx3.SelectedValue = "";
            ActivarCheck(chkBool1, "Ver eliminados/Inhabilitados");
            ActivarCheck(chkBool2, "Ver empleados sin contraseña");
        }

        protected override string TextoLblBuscar
    => "Buscar Empleado:";

        protected override string TextoLblCbx1
            => "Filtrar por Propiedad";

        protected override string TextoLblCbx2
            => "Filtrar por Estado";

        protected override string TextoLblCbx3
            => "Filtrar por Fecha";
        protected override string TextoTitular
       => "Listado de los Empleados";

        protected override FiltroConsulta ObtenerFiltros()
        {
            var filtros = base.ObtenerFiltros();

            if (filtros.Filtro1 == null ||
                string.IsNullOrWhiteSpace(filtros.Filtro1.ToString()))
            {
                filtros.Filtro1 = "ApyNom";
            }

            return filtros;
        }

        protected override void AccionCheck2()
        {
            // VER TODOS

            if (chkBool2.Checked)
            {
                _actualizandoFiltros = true;

                chkBool1.Checked = false; // no tiene sentido combinar

                _actualizandoFiltros = false;

                LimpiarFiltrosModoGlobal();
            }

        }

        protected override void AccionCheck1()
        {
            // VER ELIMINADOS

            if (chkBool1.Checked)
            {
                _actualizandoFiltros = true;

                chkBool2.Checked = false;

                _actualizandoFiltros = false;

                LimpiarFiltrosModoEliminados();
            }

        }
        private void LimpiarFiltrosModoGlobal()
        {
            _actualizandoFiltros = true;

            txtBuscar.Clear();

            if (cbx1.Enabled)
                cbx1.SelectedIndex = 0;

            if (cbx2.Enabled)
                cbx2.SelectedIndex = 0;

            if (cbx3.Enabled)
                cbx3.SelectedIndex = 0;

            chkUsarFecha.Checked = false;

            _actualizandoFiltros = false;
        }

        private void LimpiarFiltrosModoEliminados()
        {
            _actualizandoFiltros = true;

            txtBuscar.Clear();

            if (cbx1.Enabled)
                cbx1.SelectedIndex = 0;

            if (cbx2.Enabled)
                cbx2.SelectedIndex = 0;

            if (cbx3.Enabled)
                cbx3.SelectedIndex = 0;

            chkUsarFecha.Checked = false;

            _actualizandoFiltros = false;
        }

        protected override bool EsModoSoloLectura(FiltroConsulta filtro)
        {
            return filtro.Bool1;
        }

        #endregion

        #region ACTUALIZAR DATOS

        public override void ActualizarDatos(
            DataGridView dgv,
            FiltroConsulta filtros)
        {
            base.ActualizarDatos(dgv, filtros);

            var resultado =
                _empleadoServicio.ObtenerEmpleados(filtros);

            dgv.DataSource = resultado.Items;

            ResetearGrilla(dgv);

            var paginacion = new DatosPaginacion
            {
                PaginaActual = resultado.Page,
                PageSize = resultado.PageSize,
                CantidadRegistros = resultado.TotalRegistros,
            };

            ActualizarPaginacionUI(paginacion);

            BarraLateralBotones.Enabled = !filtros.Bool1;
        }

        #endregion

        #region BOTONES BASE

        public override void EjecutarBtnNuevo()
        {
            var f = new FEmpleadoABM(TipoOperacion.Nuevo);

            f.ShowDialog();

            if (f.RealizoAlgunaOperacion)
                RefrescarGrilla();
        }

        public override void EjecutarBtnModificar()
        {
            base.EjecutarBtnModificar();

            if (!puedeEjecutarComando)
                return;

            var f = new FEmpleadoABM(
                TipoOperacion.Modificar,
                entidadID);

            f.ShowDialog();

            if (f.RealizoAlgunaOperacion)
                RefrescarGrilla();
        }

        public override void EjecutarBtnEliminar()
        {
            base.EjecutarBtnEliminar();

            if (!puedeEjecutarComando)
                return;

            var f = new FEmpleadoABM(
                TipoOperacion.Eliminar,
                entidadID);

            f.ShowDialog();

            if (f.RealizoAlgunaOperacion)
                RefrescarGrilla();
        }

        #endregion

        #region GRILLA

        public override void ResetearGrilla(DataGridView grilla)
        {
            base.ResetearGrilla(grilla);

            if (grilla.Columns.Count == 0)
                return;

            if (grilla.Columns.Contains("PersonaId"))
            {
                grilla.Columns["PersonaId"].Visible = false;
                grilla.Columns["PersonaId"].Name = "Id";
            }

            if (grilla.Columns.Contains("Legajo"))
            {
                grilla.Columns["Legajo"].Visible = true;
                grilla.Columns["Legajo"].Width = 80;
            }

            if (grilla.Columns.Contains("Nombre"))
            {
                grilla.Columns["Nombre"].Visible = true;
                grilla.Columns["Nombre"].Width = 100;
            }

            if (grilla.Columns.Contains("Apellido"))
            {
                grilla.Columns["Apellido"].Visible = true;
                grilla.Columns["Apellido"].Width = 100;
            }

            if (grilla.Columns.Contains("Username"))
            {
                grilla.Columns["Username"].Visible = true;
                grilla.Columns["Username"].HeaderText = "Usuario";
                grilla.Columns["Username"].AutoSizeMode =
                    DataGridViewAutoSizeColumnMode.Fill;
            }

            if (grilla.Columns.Contains("DNI"))
            {
                grilla.Columns["DNI"].Visible = true;
                grilla.Columns["DNI"].Width = 100;
            }

            if (grilla.Columns.Contains("Email"))
            {
                grilla.Columns["Email"].Visible = true;
                grilla.Columns["Email"].Width = 130;
            }

            if (grilla.Columns.Contains("Telefono"))
            {
                grilla.Columns["Telefono"].Visible = true;
                grilla.Columns["Telefono"].Width = 100;
            }

            if (grilla.Columns.Contains("EstadoDescripcion"))
            {
                grilla.Columns["EstadoDescripcion"].Visible = true;
                grilla.Columns["EstadoDescripcion"].Width = 100;
                grilla.Columns["EstadoDescripcion"].HeaderText = "Estado";
            }
        }

        #endregion
    }
}