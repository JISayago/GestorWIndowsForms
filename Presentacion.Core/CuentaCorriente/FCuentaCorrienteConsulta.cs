using Presentacion.FBase;
using Presentacion.FBase.Helpers;
using Presentacion.FormulariosBase.Helpers;
using Servicios.LogicaNegocio.CuentaCorriente;
using System.Windows.Forms;

namespace Presentacion.Core.CuentaCorriente
{
    public partial class FCuentaCorrienteConsulta : FBaseConsulta
    {
        private readonly ICuentaCorrienteServicio _cuentacorrienteServicio;

        public FCuentaCorrienteConsulta() : this(new CuentaCorrienteServicio())
        {
            InitializeComponent();
        }

        public FCuentaCorrienteConsulta(ICuentaCorrienteServicio cuentacorrienteServicio)
        {
            _cuentacorrienteServicio = cuentacorrienteServicio;
            InitializeComponent();
        }

        #region 🔷 GRILLA

        public override void ResetearGrilla(DataGridView grilla)
        {
            base.ResetearGrilla(grilla);

            if (grilla.Columns.Contains("CuentaCorrienteId"))
            {
                grilla.Columns["CuentaCorrienteId"].Visible = false;
                grilla.Columns["CuentaCorrienteId"].Name = "Id";
            }

            grilla.Columns["NombreCuentaCorriente"].Visible = true;
            grilla.Columns["NombreCuentaCorriente"].Width = 150;
            grilla.Columns["NombreCuentaCorriente"].HeaderText = "Nombre CC";

            grilla.Columns["FechaVencimiento"].Visible = true;
            grilla.Columns["FechaVencimiento"].Width = 120;
            grilla.Columns["FechaVencimiento"].HeaderText = "Fecha Vencimiento";

            grilla.Columns["EstadoCuentaCorriente"].Visible = true;
            grilla.Columns["EstadoCuentaCorriente"].Width = 120;
            grilla.Columns["EstadoCuentaCorriente"].HeaderText = "Estado CC";
        }

        #endregion

        #region 🔥 ACTUALIZAR DATOS (NUEVO SISTEMA)

        public override void ActualizarDatos(DataGridView dgv, FiltroConsulta filtros)
        {
            base.ActualizarDatos(dgv, filtros);

            if (filtros.VerEliminados)
            {
                dgv.DataSource = _cuentacorrienteServicio.ObtenerCuentaCorrientesEliminada(filtros.TextoBuscar);
                BarraLateralBotones.Enabled = false;
            }
            else
            {
                dgv.DataSource = _cuentacorrienteServicio.ObtenerCuentaCorrientes(filtros.TextoBuscar);
                BarraLateralBotones.Enabled = true;
            }
        }

        #endregion

        #region 🔷 BOTONES BASE

        public override void EjecutarBtnNuevo()
        {
            var f = new FCuentaCorrienteABM(TipoOperacion.Nuevo);
            f.ShowDialog();

            if (f.RealizoAlgunaOperacion)
                Recargar();
        }

        public override void EjecutarBtnModificar()
        {
            base.EjecutarBtnModificar();
            if (!puedeEjecutarComando) return;

            var f = new FCuentaCorrienteABM(TipoOperacion.Modificar, entidadID);
            f.ShowDialog();

            if (f.RealizoAlgunaOperacion)
                Recargar();
        }

        public override void EjecutarBtnEliminar()
        {
            base.EjecutarBtnEliminar();
            if (!puedeEjecutarComando) return;

            var f = new FCuentaCorrienteABM(TipoOperacion.Eliminar, entidadID);
            f.ShowDialog();

            if (f.RealizoAlgunaOperacion)
                Recargar();
        }

        private void Recargar()
        {
         //   btnActualizar_Click_Base();
        }

        #endregion
    }
}
