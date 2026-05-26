using Presentacion.Core.Presentacion.Core.Helpers;
using Presentacion.FBase;
using Presentacion.FBase.Helpers;
using Presentacion.FormulariosBase.Helpers;
using Servicios.Helpers.Cliente;
using Servicios.Helpers.Producto;
using Servicios.Helpers.Sistema.FiltrosConsulta;
using Servicios.LogicaNegocio.Cliente;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Presentacion.Core.Cliente
{
    public partial class FClienteConsulta : FBaseConsulta
    {
        private readonly IClienteServicio _clienteServicio;

        private bool vieneDeCargaCliente = false;

        public long? clienteSeleccionado = null;

        public FClienteConsulta() : this(new ClienteServicio())
        {
            InitializeComponent();
        }

        public FClienteConsulta(IClienteServicio clienteServicio)
        {
            _clienteServicio = clienteServicio;

            InitializeComponent();
        }

        public FClienteConsulta(bool _vieneDeCargaCliente) : this(new ClienteServicio())
        {
            vieneDeCargaCliente = _vieneDeCargaCliente;

            InitializeComponent();
        }

        #region LOAD

        private void FClienteConsulta_Load(object sender, EventArgs e)
        {

        }

        #endregion

        #region CONFIG FILTROS
        protected override string TextoLblBuscar
    => "Buscar Cliente:";

        protected override string TextoLblCbx1
            => "Filtrar por Propiedad";

        protected override string TextoLblCbx2
            => "Filtrar por Estado";

        protected override string TextoLblCbx3
            => "Filtrar por Fecha";
        protected override void ConfigurarFiltrosUI()
        {
            base.ConfigurarFiltrosUI();

            // =========================
            // COMBO BUSQUEDA
            // =========================
            ActivarCheck(chkBool1, "Mostrar Eliminados");
            ActivarCheck(chkBool2, "Mostrar Todos los Clientes");
            var opcionesBusqueda = new List<OpcionFiltro>
            {
                new OpcionFiltro { Texto = "Todos", Valor = "" },
                new OpcionFiltro { Texto = "Nombre", Valor = "ApyNom" },
                new OpcionFiltro { Texto = "Documento", Valor = "Dni" },
                new OpcionFiltro { Texto = "Teléfono", Valor = "Telefono" },
                new OpcionFiltro { Texto = "Email", Valor = "Email" }
            };

            ActivarCombo(
                cbx1,
                lblcbx1,
                opcionesBusqueda,
                "Texto",
                "Valor",
                "Buscar cliente por:"
            );

            // =========================
            // FILTRO FECHAS
            // =========================

            ActivarFiltroFechas("Filtrar por fecha");

            // =========================
            // COMBO TIPO FILTRO
            // =========================

            var estado = new List<OpcionFiltro>
             {
                 new OpcionFiltro { Texto = "Todas", Valor = "" },
             
                 new OpcionFiltro
                 {
                     Texto = "Activos",
                     Valor = ((int)TipoFiltroCliente.Activo).ToString()
                 },
                 new OpcionFiltro
                 {
                     Texto = "Baja",
                     Valor = ((int)TipoFiltroCliente.Baja).ToString()
                 },
             
                 new OpcionFiltro
                 {
                     Texto = "Inhabilitado",
                     Valor = ((int)TipoFiltroCliente.Inhabilitado).ToString()
                 },
             
                 new OpcionFiltro
                 {
                     Texto = "Con Cuenta Corriente",
                     Valor = ((int)TipoFiltroCliente.ConCtaCte).ToString()
                 },
             
                 new OpcionFiltro
                 {
                     Texto = "Sin Cuenta Corriente",
                     Valor = ((int)TipoFiltroCliente.SinCtaCte).ToString()
                 }
             };

            ActivarCombo(
                cbx2,
                lblcbx2,
                estado,
                "Texto",
                "Valor",
                "Tipo de filtrado:"
            );

            var fechaFiltro = new List<OpcionFiltro>
            {
                new OpcionFiltro { Texto = "Todas", Valor = "" },

                new OpcionFiltro
                {
                    Texto = "Fecha Alta",
                    Valor = ((int)TipoFiltroCliente.FechaAlta).ToString()
                },

                new OpcionFiltro
                {
                    Texto = "Fecha Baja",
                    Valor = ((int)TipoFiltroCliente.FechaBaja).ToString()
                },
            };
             ActivarCombo(
                cbx3,
                lblcbx3,
                fechaFiltro,
                "Texto",
                "Valor",
                "Tipo de Fecha:"
            );

            // =========================
            // CHECK ELIMINADOS
            // =========================

            ActivarCheck(chkBool1, "Ver eliminados");

            // =========================
            // VALORES DEFAULT
            // =========================

            cbx1.SelectedValue = "";
            cbx2.SelectedValue = "";
            cbx3.SelectedValue = "";
        }

        #endregion
        protected override void AccionCheck2()
        {
            if (chkBool2.Checked)
            {
                _actualizandoFiltros = true;

                chkBool1.Checked = false;

                _actualizandoFiltros = false;

                LimpiarFiltrosEspeciales();
            }
        }
        protected override void AccionCheck1()
        {
            if (chkBool1.Checked)
            {
                _actualizandoFiltros = true;

                chkBool2.Checked = false;

                _actualizandoFiltros = false;

                LimpiarFiltrosEspeciales();
            }
        }

        private void LimpiarFiltrosEspeciales()
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
        #region FILTROS

        protected override FiltroConsulta ObtenerFiltros()
        {
            var filtros = base.ObtenerFiltros();

            // Si no selecciona nada en combo búsqueda
            // usar ApyNom por defecto

            if (filtros.Filtro1 == null ||
                string.IsNullOrWhiteSpace(filtros.Filtro1.ToString()))
            {
                filtros.Filtro1 = "ApyNom";
            }

            return filtros;
        }

        protected override bool EsModoSoloLectura(FiltroConsulta filtro)
        {
            return filtro.Bool1;
        }

        #endregion

        #region ACTUALIZAR DATOS

        public override void ActualizarDatos(DataGridView dgv, FiltroConsulta filtros)
        {
            base.ActualizarDatos(dgv, filtros);

            var resultado = _clienteServicio.ObtenerClientes(filtros);

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

        #region RESET GRILLA

        public override void ResetearGrilla(DataGridView grilla)
        {
            base.ResetearGrilla(grilla);

            if (grilla.Columns.Count == 0)
                return;

            grilla.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // =========================
            // ID (oculto + alias)
            // =========================
            if (grilla.Columns.Contains("PersonaId"))
            {
                grilla.Columns["PersonaId"].Visible = false;
                grilla.Columns["PersonaId"].Name = "Id";
            }

            // =========================
            // NOMBRE
            // =========================
            if (grilla.Columns.Contains("Nombre"))
            {
                var col = grilla.Columns["Nombre"];
                col.Visible = true;
                col.FillWeight = 120;
                col.MinimumWidth = 120;
            }

            // =========================
            // APELLIDO
            // =========================
            if (grilla.Columns.Contains("Apellido"))
            {
                var col = grilla.Columns["Apellido"];
                col.Visible = true;
                col.FillWeight = 120;
                col.MinimumWidth = 120;
            }

            // =========================
            // DNI
            // =========================
            if (grilla.Columns.Contains("Dni"))
            {
                var col = grilla.Columns["Dni"];
                col.Visible = true;
                col.Width = 110;
            }

            // =========================
            // EMAIL
            // =========================
            if (grilla.Columns.Contains("Email"))
            {
                var col = grilla.Columns["Email"];
                col.Visible = true;
                col.FillWeight = 160;
                col.MinimumWidth = 150;
            }

            // =========================
            // TELÉFONO
            // =========================
            if (grilla.Columns.Contains("Telefono"))
            {
                var col = grilla.Columns["Telefono"];
                col.Visible = true;
                col.Width = 110;
            }

            // =========================
            // ESTADO (DESCRIPCIÓN)
            // =========================
            if (grilla.Columns.Contains("EstadoDescripcion"))
            {
                var col = grilla.Columns["EstadoDescripcion"];
                col.Visible = true;
                col.HeaderText = "Estado";
                col.Width = 110;
            }

            // =========================
            // OCULTAR CAMPOS CRUDOS
            // =========================
            if (grilla.Columns.Contains("Estado"))
                grilla.Columns["Estado"].Visible = false;

            if (grilla.Columns.Contains("EstaEliminado"))
                grilla.Columns["EstaEliminado"].Visible = false;
        }
        #endregion

        #region BOTONES BASE

        public override void EjecutarBtnNuevo()
        {
            var f = new FClienteABM(TipoOperacion.Nuevo);

            f.ShowDialog();

            if (f.RealizoAlgunaOperacion)
                RefrescarGrilla();
        }

        public override void EjecutarBtnModificar()
        {
            base.EjecutarBtnModificar();

            if (!puedeEjecutarComando)
                return;

            var f = new FClienteABM(TipoOperacion.Modificar, entidadID);

            f.ShowDialog();

            if (f.RealizoAlgunaOperacion)
                RefrescarGrilla();
        }

        public override void EjecutarBtnEliminar()
        {
            base.EjecutarBtnEliminar();

            if (!puedeEjecutarComando)
                return;

            var f = new FClienteABM(TipoOperacion.Eliminar, entidadID);

            f.ShowDialog();

            if (f.RealizoAlgunaOperacion)
                RefrescarGrilla();
        }

        #endregion

        #region ACCIONES PERSONALIZADAS

        protected override void ConfigurarAccionesPersonalizadas()
        {
            if (vieneDeCargaCliente)
            {
                AgregarAccion(
                    "Seleccionar Cliente",
                    Constantes.Imagenes.ImgPerfilUsuario,
                    SeleccionarCliente,
                    true
                );
            }
        }

        private void SeleccionarCliente(long? id)
        {
            ControlCargaExistencaDatos();

            if (!puedeEjecutarComando)
                return;

            clienteSeleccionado = id;

            DialogResult = DialogResult.OK;

            Close();
        }

        #endregion

        #region VALIDACIONES

        public void ControlCargaExistencaDatos()
        {
            if (dgvGrilla.RowCount == 0)
            {
                MessageBox.Show("No hay datos cargados.");

                puedeEjecutarComando = false;

                return;
            }

            if (!entidadID.HasValue)
            {
                MessageBox.Show("Seleccione un registro.");

                puedeEjecutarComando = false;

                return;
            }

            puedeEjecutarComando = true;
        }

        #endregion
    }
}