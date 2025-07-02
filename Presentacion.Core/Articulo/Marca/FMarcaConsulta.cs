using Presentacion.FBase;
using Presentacion.FormulariosBase.Helpers;
using Servicios.Marca;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Presentacion.Core.Marca
{
    public partial class FMarcaConsulta : FBaseConsulta
    {
        private readonly IMarcaServicio _marcaServicio;

        public FMarcaConsulta() : this(new MarcaServicio())
        {
            InitializeComponent();
        }

        public FMarcaConsulta(IMarcaServicio marcaServicio)
        {
            _marcaServicio = marcaServicio;
        }

        public override void ResetearGrilla(DataGridView grilla)
        {
            base.ResetearGrilla(grilla);


            grilla.Columns["Nombre"].Visible = true;
            grilla.Columns["Nombre"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            grilla.Columns["Nombre"].HeaderText = "Marca";

        }

        public override void ActualizarDatos(DataGridView grilla, string cadenaBuscar, CheckBox check, ToolStrip toolStrip)
        {

            base.ActualizarDatos(grilla, cadenaBuscar, check, toolStrip);

            if (check.Checked)
            {
                grilla.DataSource = _marcaServicio.ObtenerMarcaEliminada(cadenaBuscar);
                toolStrip.Enabled = false;
            }
            else
            {
                grilla.DataSource = _marcaServicio.ObtenerMarca(cadenaBuscar);
                toolStrip.Enabled = true;
            }
        }

        public override void EjecutarBtnEliminar()
        {
            base.EjecutarBtnEliminar();
            if (puedeEjecutarComando)
            {
                var FormularioABMMarca = new FMarcaABM(TipoOperacion.Eliminar, entidadID);
                FormularioABMMarca.ShowDialog();
                ActualizarSegunOperacion(FormularioABMMarca.RealizoAlgunaOperacion);
            }
        }

        private void ActualizarSegunOperacion(bool realizoOperacion)
        {
            if (realizoOperacion)
            {
                ActualizarDatos(dgvGrilla, string.Empty, cbxEstaEliminado, BarraLateralBotones);
            }
        }

        public override void EjecutarBtnModificar()
        {
            base.EjecutarBtnModificar();
            if (puedeEjecutarComando)
            {
                var FormularioABMMarca = new FMarcaABM(TipoOperacion.Modificar, entidadID);
                FormularioABMMarca.ShowDialog();
                ActualizarSegunOperacion(FormularioABMMarca.RealizoAlgunaOperacion);
            }
        }

        public override void EjecutarBtnNuevo()
        {
            var FormularioABMMarca = new FMarcaABM(TipoOperacion.Nuevo);
            FormularioABMMarca.ShowDialog();
            ActualizarSegunOperacion(FormularioABMMarca.RealizoAlgunaOperacion);
        }
    }
}
