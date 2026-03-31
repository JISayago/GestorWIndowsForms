using Presentacion.FBase;
using Presentacion.FBase.Helpers;
using Presentacion.FormulariosBase.Helpers;
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

            if (grilla.Columns.Contains("PersonaId"))
            {
                grilla.Columns["PersonaId"].Visible = false;
                grilla.Columns["PersonaId"].Name = "Id";
            }

            grilla.Columns["Nombre"].Visible = true;
            grilla.Columns["Nombre"].Width = 100;

            grilla.Columns["Apellido"].Visible = true;
            grilla.Columns["Apellido"].Width = 100;

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
                dgv.DataSource = _clienteServicio.ObtenerClientesEliminados(filtros.TextoBuscar);
                BarraLateralBotones.Enabled = false;
            }
            else
            {
                dgv.DataSource = _clienteServicio.ObtenerClientes(filtros.TextoBuscar);
                BarraLateralBotones.Enabled = true;
            }
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
