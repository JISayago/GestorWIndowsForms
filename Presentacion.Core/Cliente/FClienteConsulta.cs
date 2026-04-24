using Presentacion.Core.Presentacion.Core.Helpers;
using Presentacion.FBase;
using Presentacion.FBase.Helpers;
using Presentacion.FormulariosBase.Helpers;
using Servicios.Helpers.Cliente;
using Servicios.Helpers.Producto;
using Servicios.Helpers.Sistema.FiltrosConsulta;
using Servicios.LogicaNegocio.Cliente;
using System;
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

        #region 🔷 GRILLA

        public override void ResetearGrilla(DataGridView grilla)
        {
            base.ResetearGrilla(grilla);

            if (grilla.Columns.Count == 0) return;

            if (grilla.Columns.Contains("PersonaId"))
            {
                grilla.Columns["PersonaId"].Visible = false;
                grilla.Columns["PersonaId"].Name = "Id";
            }

            if (grilla.Columns.Contains("Nombre"))
            {
                grilla.Columns["Nombre"].Visible = true;
                grilla.Columns["Nombre"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }

            if (grilla.Columns.Contains("Apellido"))
            {
                grilla.Columns["Apellido"].Visible = true;
                grilla.Columns["Apellido"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }

            if (grilla.Columns.Contains("Dni"))
            {
                grilla.Columns["Dni"].Visible = true;
                grilla.Columns["Dni"].Width = 100;
            }

            if (grilla.Columns.Contains("Email"))
            {
                grilla.Columns["Email"].Visible = true;
                grilla.Columns["Email"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
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

        #region 🔥 ACTUALIZAR DATOS (NUEVO SISTEMA)

        public override void ActualizarDatos(DataGridView dgv, FiltroConsulta filtros)
        {
            base.ActualizarDatos(dgv, filtros);

            filtros.Extra ??= "ApyNom";

            var resultado = _clienteServicio.ObtenerClientes(filtros);

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



        #region 🔷 BOTONES BASE

        public override void EjecutarBtnNuevo()
        {
            var f = new FClienteABM(TipoOperacion.Nuevo);
            f.ShowDialog();

            if (f.RealizoAlgunaOperacion)
                Recargar();
        }

        public override void EjecutarBtnModificar()
        {
            base.EjecutarBtnModificar();
            if (!puedeEjecutarComando) return;

            var f = new FClienteABM(TipoOperacion.Modificar, entidadID);
            f.ShowDialog();

            if (f.RealizoAlgunaOperacion)
                Recargar();
        }

        public override void EjecutarBtnEliminar()
        {
            base.EjecutarBtnEliminar();
            if (!puedeEjecutarComando) return;

            var f = new FClienteABM(TipoOperacion.Eliminar, entidadID);
            f.ShowDialog();

            if (f.RealizoAlgunaOperacion)
                Recargar();
        }

        private void Recargar()
        {
           // btnActualizar_Click_Base();
        }

        #endregion

        #region 🔷 CONTROL SELECCION

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

        #region 🔷 LOAD

        private void FClienteConsulta_Load(object sender, EventArgs e)
        {
        }

        #endregion
        protected override void ConfigurarFiltrosUI()
        {
            base.ConfigurarFiltrosUI();

            var opciones = new List<OpcionFiltro>
            {
                new OpcionFiltro { Texto = "Todos", Valor = "" },
                new OpcionFiltro { Texto = "Nombre", Valor = "ApyNom" },
                new OpcionFiltro { Texto = "Documento", Valor = "Dni" },
                new OpcionFiltro { Texto = "Teléfono", Valor = "Telefono" },
                new OpcionFiltro { Texto = "Email", Valor = "Email" }
            };

            ActivarFiltroCombo(opciones, "Texto", "Valor");


            ActivarFiltroFechas("Filtrar por:");

            var tiposFecha = new List<OpcionFiltro>
            {
                new OpcionFiltro { Texto = "Todas", Valor = "" },
                new OpcionFiltro { Texto = "Fecha Alta", Valor = ((int)TipoFiltroCliente.FechaAlta).ToString() },
                new OpcionFiltro { Texto = "Fecha Baja", Valor = ((int)TipoFiltroCliente.FechaBaja).ToString() },
                new OpcionFiltro { Texto = "Con Cuenta Corriente", Valor =((int)TipoFiltroCliente.ConCtaCte).ToString() },
                new OpcionFiltro { Texto = "Sin Cuenta Corriente", Valor = ((int)TipoFiltroCliente.SinCtaCte).ToString() },
                new OpcionFiltro { Texto = "Inhabilitado", Valor = ((int)TipoFiltroCliente.Inhabilitado).ToString() },

            };

            ActivarComboOpcional(tiposFecha, "Texto", "Valor");

            cbxFiltroOpcional.SelectedValue = "";
            cbxFiltroExtraEstado.SelectedValue = "";
        }

        protected override void ConfigurarAccionesPersonalizadas()
        {
            // BOTON Seleccionar
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
            if (!puedeEjecutarComando) return;

            clienteSeleccionado = id;
            DialogResult = DialogResult.OK;
            Close();
        }

    }
}
