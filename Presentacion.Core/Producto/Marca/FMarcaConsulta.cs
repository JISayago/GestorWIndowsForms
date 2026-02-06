using Presentacion.FBase;
using Presentacion.FormulariosBase.Helpers;
using Servicios.LogicaNegocio.Articulo.Marca;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Presentacion.Core.Articulo.Marca
{
    public partial class FMarcaConsulta : FBaseConsulta
    {
        private readonly IMarcaServicio _marcaServicio;
        public long? marcaSeleccionada = null;

        public FMarcaConsulta() : this(new MarcaServicio())
        {
            InitializeComponent();
        }

        public FMarcaConsulta(IMarcaServicio marcaServicio)
        {
            _marcaServicio = marcaServicio;
        }

        // 🔥 BOTONES EXTRA DINAMICOS
        protected override void ConfigurarAccionesPersonalizadas()
        {
            // Botón de prueba: cambiar color
            AgregarAccion(
                "Color Test",
                SystemIcons.Information.ToBitmap(),   // icono simple
                CambiarColorFormulario,
                false // no requiere selección
            );

            // ejemplo si mañana querés uno que requiera fila seleccionada
            AgregarAccion(
                "Test con fila",
                SystemIcons.Warning.ToBitmap(),
                (id) =>
                {
                    MessageBox.Show($"ID seleccionado: {id}");
                },
                true
            );
        }

        private void CambiarColorFormulario(long? id)
        {
            // solo para test visual
            if (BackColor == Color.FromArgb(45, 45, 48))
                BackColor = Color.WhiteSmoke;
            else
                BackColor = Color.FromArgb(45, 45, 48);
        }

        #region GRILLA

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

        #endregion

        #region BOTONES BASE

        public override void EjecutarBtnEliminar()
        {
            base.EjecutarBtnEliminar();
            if (!puedeEjecutarComando) return;

            var f = new FMarcaABM(TipoOperacion.Eliminar, entidadID);
            f.ShowDialog();

            ActualizarSegunOperacion(f.RealizoAlgunaOperacion);
        }

        public override void EjecutarBtnModificar()
        {
            base.EjecutarBtnModificar();
            if (!puedeEjecutarComando) return;

            var f = new FMarcaABM(TipoOperacion.Modificar, entidadID);
            f.ShowDialog();

            ActualizarSegunOperacion(f.RealizoAlgunaOperacion);
        }

        public override void EjecutarBtnNuevo()
        {
            var f = new FMarcaABM(TipoOperacion.Nuevo);
            f.ShowDialog();

            ActualizarSegunOperacion(f.RealizoAlgunaOperacion);
        }

        private void ActualizarSegunOperacion(bool realizoOperacion)
        {
            if (realizoOperacion)
                btnActualizar_Click_Base(); // mejor que performclick
        }

        #endregion

        #region SELECCIONAR

        private void btnSeleccionarMarca_Click(object sender, EventArgs e)
        {
            if (!entidadID.HasValue) return;

            marcaSeleccionada = entidadID;
            DialogResult = DialogResult.OK;
            Close();
        }

        #endregion
    }
}
