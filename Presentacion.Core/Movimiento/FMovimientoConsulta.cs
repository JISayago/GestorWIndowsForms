using Presentacion.FBase;
using Servicios.LogicaNegocio.Movimiento;
using Servicios.LogicaNegocio.Producto.Rubro;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Presentacion.Core.Movimiento
{
    public partial class FMovimientoConsulta : FBaseConsulta
    {
        private readonly IMovimientoServicio _movimientoServicio;
        //protected long? entidadID;

        public FMovimientoConsulta() : this(new MovimientoServicio())
        {
            InitializeComponent();
        }

        public FMovimientoConsulta(IMovimientoServicio movimientoServicio)
        {
            _movimientoServicio = movimientoServicio;
        }

        private void FMovimientoConsulta_Load(object sender, EventArgs e)
        {
            //BarraLateralBotones.Enabled = false;
        }

        public override void ResetearGrilla(DataGridView grilla)
        {
            base.ResetearGrilla(grilla);

            grilla.Columns["MovimientoId"].Visible = false;
            grilla.Columns["MovimientoId"].Name = "Id";

            grilla.Columns["NumeroMovimiento"].Visible = true;
            grilla.Columns["NumeroMovimiento"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            grilla.Columns["NumeroMovimiento"].HeaderText = "Numero Movimiento";

            grilla.Columns["FechaMovimiento"].Visible = true;
            grilla.Columns["FechaMovimiento"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            grilla.Columns["FechaMovimiento"].HeaderText = "Fecha";

        }

        public override void ActualizarDatos(DataGridView grilla, string cadenaBuscar, CheckBox check, ToolStrip toolStrip)
        {

            base.ActualizarDatos(grilla, cadenaBuscar, check, toolStrip);

            if (check.Checked)
            {
                grilla.DataSource = _movimientoServicio.ObtenerMovimientoEliminado(cadenaBuscar);
                toolStrip.Enabled = false;
            }
            else
            {
                grilla.DataSource = _movimientoServicio.ObtenerMovimiento(cadenaBuscar);
                toolStrip.Enabled = false;
            }
        }

        private void lblAbrir_Click(object sender, EventArgs e)
        {
            var FMovimientoDetalle = new FMovimientoDetallado(entidadID);
            FMovimientoDetalle.ShowDialog();
        }
    }
}
