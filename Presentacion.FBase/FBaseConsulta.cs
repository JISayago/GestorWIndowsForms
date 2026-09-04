using AccesoDatos.Entidades;
using Presentacion.FBase.Helpers;
using Servicios.Helpers.Sistema.Extras;
using Servicios.Helpers.Sistema.FiltrosConsulta;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.Text;
using System.Windows.Forms;

namespace Presentacion.FBase
{
    public partial class FBaseConsulta : FBase
    {
        protected long? entidadID;
        protected bool puedeEjecutarComando;
        private FiltroConsulta ultimoFiltro;

        protected int paginaActual = 1;
        protected int pageSize = 16;
        protected int totalPaginas = 1;

        protected bool _actualizandoFiltros;

        public List<AccionGrid> AccionesPersonalizadas = new List<AccionGrid>();

        protected virtual string TextoLblBuscar => "Buscar:";

        protected virtual string TextoLblCbx1 => "Filtro 1";

        protected virtual string TextoLblCbx2 => "Filtro 2";

        protected virtual string TextoLblCbx3 => "Filtro 3";
        protected virtual string TextoTitular => "Aqui se describe que se esta mostrando";

        

        public FBaseConsulta()
        {
            InitializeComponent();

            BarraLateralBotones.ImageScalingSize = new Size(25, 25);

            btnImprimir.Visible = false;

            btnNuevo.Image = Constantes.Imagenes.ImgNuevo;
            btnModificar.Image = Constantes.Imagenes.ImgModificar;
            btnEliminar.Image = Constantes.Imagenes.ImgEliminar;
            btnImprimir.Image = Constantes.Imagenes.ImgImprimir;
            btnActualizar.Image = Constantes.Imagenes.ImgActualizar;
            btnSalir.Image = Constantes.Imagenes.ImgCerrar;

            entidadID = null;
            puedeEjecutarComando = false;

            dgvGrilla.RowEnter += DgvGrilla_RowEnter;
            dgvGrilla.CellDoubleClick += DgvGrilla_CellDoubleClick;
            dgvGrilla.CellClick += DgvGrilla_CellClick;
            dgvGrilla.MouseDown += DgvGrilla_MouseDown;
            dgvGrilla.Paint += DgvGrilla_PaintFondoVacio;
            dgvGrilla.Resize += (_, __) => AjustarAlturaFilasParaPageSize();
            dgvGrilla.DataBindingComplete += (_, __) => AjustarAlturaFilasParaPageSize();
        }

        #region LOAD

        private void FBaseConsulta_Load(object sender, EventArgs e)
        {
            ConfigurarFiltrosUI();
            ActualizarTextosLabels();
            CargarLogoEnBase();

            ResetearGrilla(dgvGrilla);

            ConfigurarAccionesPersonalizadas();
            CrearBotonesPersonalizados();

            AjustarVisibilidadFiltros();
            AjustarLayoutFooterYFiltros();
            RefrescarGrilla();
        }

        private void CargarLogoEnBase()
        {
            pbxLogo.Image = Constantes.Imagenes.ImgLogoCompuesto;
            pbxLogo.Dock = DockStyle.None;
            pbxLogo.Anchor = AnchorStyles.None;
            pbxLogo.SizeMode = PictureBoxSizeMode.Zoom;
            pbxLogo.Margin = new Padding(8, 4, 8, 4);
            pbxLogo.Size = new Size(154, 60);
        }
        #endregion

        /// <summary>
        /// Estilo visual de consultas (no afecta ABMs). Se aplica después del tema base.
        /// </summary>
        protected override void AplicarTema(Control parent)
        {
            base.AplicarTema(parent);

            if (ReferenceEquals(parent, this))
                AplicarEstiloConsulta();
        }

        private void AplicarEstiloConsulta()
        {
            if (lblContenidoTexto != null)
            {
                lblContenidoTexto.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold);
                lblContenidoTexto.ForeColor = TemaSistema.Primario;
            }

            EstilarLabelFiltro(lblBuscar);
            EstilarLabelFiltro(lblcbx1);
            EstilarLabelFiltro(lblcbx2);
            EstilarLabelFiltro(lblcbx3);
            EstilarLabelFiltro(lblTotalRegistros);
            EstilarLabelFiltro(lblPagina);

            EstilarBotonPrimario(btnBuscar);
            EstilarBotonSecundario(btnLimpiar);
            ForzarTamanoBotonFiltro(btnBuscar);
            ForzarTamanoBotonFiltro(btnLimpiar);

            EstilarBotonPaginacion(btnAnterior);
            EstilarBotonPaginacion(btnSiguiente);

            if (dgvGrilla != null)
            {
                dgvGrilla.Margin = new Padding(6, 2, 18, 4);
                ConfigurarGrillaConsulta();
            }

            ConfigurarFechaCorta(dtpDesde);
            ConfigurarFechaCorta(dtpHasta);

            if (txtBuscar != null)
            {
                txtBuscar.Font = new Font("Segoe UI", 10F);
                if (string.IsNullOrWhiteSpace(txtBuscar.PlaceholderText))
                    txtBuscar.PlaceholderText = "Escribí para buscar...";
            }

            if (BarraLateralBotones != null)
            {
                BarraLateralBotones.ImageScalingSize = new Size(26, 26);
                BarraLateralBotones.Padding = new Padding(4, 2, 4, 4);
                BarraLateralBotones.Font = new Font("Segoe UI Semibold", 9.5F, FontStyle.Bold);
            }

            //if (pbxLogo != null)
            //{
            //    pbxLogo.SizeMode = PictureBoxSizeMode.Zoom;
            //    pbxLogo.Dock = DockStyle.None;
            //    pbxLogo.Anchor = AnchorStyles.None;
            //    pbxLogo.Margin = new Padding(8, 4, 8, 4);
            //    pbxLogo.Size = new Size(154, 60);
            //}

            if (lblTotalRegistros != null)
            {
                lblTotalRegistros.Dock = DockStyle.None;
                lblTotalRegistros.Anchor = AnchorStyles.Left;
                lblTotalRegistros.AutoSize = true;
            }

            AjustarLayoutFooterYFiltros();

            foreach (var chk in new[] { chkBool1, chkBool2, chkUsarFecha, chkUsarRango })
            {
                if (chk == null) continue;
                chk.Font = new Font("Segoe UI", 9.75F);
                chk.ForeColor = TemaSistema.Texto;
            }

            AjustarVisibilidadFiltros();
        }

        private static void EstilarLabelFiltro(Label lbl)
        {
            if (lbl == null) return;
            lbl.ForeColor = TemaSistema.Texto;
            lbl.Font = new Font("Segoe UI Semibold", 10.25F, FontStyle.Bold);
        }

        private static void ConfigurarFechaCorta(DateTimePicker dtp)
        {
            if (dtp == null) return;
            dtp.Format = DateTimePickerFormat.Custom;
            dtp.CustomFormat = "dd/MM/yyyy";
            dtp.Font = new Font("Segoe UI", 9.75F);
        }

        /// <summary>
        /// Footer: logo+total a la izquierda; paginación compacta en zona amarilla (margen derecho).
        /// Filtros: botones alineados y más anchos hacia la izquierda.
        /// </summary>
        private void AjustarLayoutFooterYFiltros()
        {
            //if (tableLayoutPanel15 != null && tableLayoutPanel15.RowStyles.Count > 5)
            //    tableLayoutPanel15.RowStyles[5].Height = 110F;

            //if (tableLayoutPanel10 != null)
            //{
            //    tableLayoutPanel10.MinimumSize = new Size(0, 100);
            //    tableLayoutPanel10.Padding = new Padding(8, 12, 160, 12);
            //    while (tableLayoutPanel10.ColumnStyles.Count < 3)
            //        tableLayoutPanel10.ColumnStyles.Add(new ColumnStyle());
            //    tableLayoutPanel10.ColumnStyles[0].SizeType = SizeType.AutoSize;
            //    tableLayoutPanel10.ColumnStyles[1].SizeType = SizeType.Percent;
            //    tableLayoutPanel10.ColumnStyles[1].Width = 100F;
            //    tableLayoutPanel10.ColumnStyles[2].SizeType = SizeType.AutoSize;
            //}

            //if (tableLayoutPanel9 != null)
            //{
            //    tableLayoutPanel9.Visible = true;
            //    tableLayoutPanel9.AutoSize = true;
            //    tableLayoutPanel9.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            //    tableLayoutPanel9.Anchor = AnchorStyles.Left;
            //}

            //if (tableLayoutPanel11 != null)
            //{
            //    tableLayoutPanel11.AutoSize = true;
            //    tableLayoutPanel11.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            //    tableLayoutPanel11.Anchor = AnchorStyles.None;
            //    tableLayoutPanel11.Margin = new Padding(0);
            //    while (tableLayoutPanel11.ColumnStyles.Count < 3)
            //        tableLayoutPanel11.ColumnStyles.Add(new ColumnStyle());
            //    tableLayoutPanel11.ColumnStyles[0] = new ColumnStyle(SizeType.Absolute, 50F);
            //    tableLayoutPanel11.ColumnStyles[1] = new ColumnStyle(SizeType.AutoSize);
            //    tableLayoutPanel11.ColumnStyles[2] = new ColumnStyle(SizeType.Absolute, 50F);
            //}

            if (tableLayoutPanel4 != null && tableLayoutPanel4.ColumnStyles.Count > 1)
            {
                tableLayoutPanel4.ColumnStyles[1].SizeType = SizeType.Absolute;
                tableLayoutPanel4.ColumnStyles[1].Width = 480F;
            }

            if (tableLayoutPanel14 != null)
            {
                tableLayoutPanel14.MinimumSize = new Size(470, 48);
                tableLayoutPanel14.Padding = Padding.Empty;
                if (tableLayoutPanel14.ColumnStyles.Count >= 2)
                {
                    tableLayoutPanel14.ColumnStyles[0] = new ColumnStyle(SizeType.Percent, 50F);
                    tableLayoutPanel14.ColumnStyles[1] = new ColumnStyle(SizeType.Percent, 50F);
                }
            }
        }

        /// <summary>
        /// Algunos Designer de consultas hijas pisan Size con altos/anchos rotos (ej. 18px).
        /// Dock Fill + mismo Margin = botones alineados.
        /// </summary>
        private static void ForzarTamanoBotonFiltro(Button btn)
        {
            if (btn == null) return;
            btn.Dock = DockStyle.Fill;
            btn.AutoSize = false;
            btn.Margin = new Padding(6, 8, 6, 8);
            btn.MinimumSize = new Size(200, 36);
            btn.Padding = Padding.Empty;
            btn.Font = new Font("Segoe UI Semibold", 9.5F, FontStyle.Bold);
        }

        private static void EstilarBotonPrimario(Button btn)
        {
            if (btn == null) return;
            btn.BackColor = TemaSistema.Primario;
            btn.ForeColor = Color.White;
            btn.FlatStyle = FlatStyle.Flat;
            // Mismo BorderSize que el secundario para alinear alturas visuales.
            btn.FlatAppearance.BorderSize = 1;
            btn.FlatAppearance.BorderColor = TemaSistema.Primario;
            btn.FlatAppearance.MouseOverBackColor = Color.FromArgb(
                Math.Min(TemaSistema.Primario.R + 28, 255),
                Math.Min(TemaSistema.Primario.G + 28, 255),
                Math.Min(TemaSistema.Primario.B + 28, 255));
            btn.Font = new Font("Segoe UI Semibold", 9.5F, FontStyle.Bold);
            btn.Cursor = Cursors.Hand;
            btn.UseVisualStyleBackColor = false;
        }

        private static void EstilarBotonSecundario(Button btn)
        {
            if (btn == null) return;
            btn.BackColor = TemaSistema.Seleccion;
            btn.ForeColor = TemaSistema.Texto;
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 1;
            btn.FlatAppearance.BorderColor = TemaSistema.Borde;
            btn.Font = new Font("Segoe UI Semibold", 9.25F, FontStyle.Bold);
            btn.Cursor = Cursors.Hand;
            btn.UseVisualStyleBackColor = false;
        }

        private static void EstilarBotonPaginacion(Button btn)
        {
            if (btn == null) return;
            EstilarBotonSecundario(btn);
            btn.Dock = DockStyle.None;
            btn.Anchor = AnchorStyles.None;
            btn.Size = new Size(44, 40);
            btn.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold);
            btn.Margin = new Padding(3, 6, 3, 6);
        }

        /// <summary>
        /// Oculta slots de filtro no activados para no dejar huecos vacíos.
        /// </summary>
        private void AjustarVisibilidadFiltros()
        {
            if (tableLayoutPanel2 != null)
                tableLayoutPanel2.Visible = cbx1 != null && cbx1.Enabled;
            if (tableLayoutPanel7 != null)
                tableLayoutPanel7.Visible = cbx2 != null && cbx2.Enabled;
            if (tableLayoutPanel12 != null)
                tableLayoutPanel12.Visible = cbx3 != null && cbx3.Enabled;

            bool usaFechas = chkUsarFecha != null && chkUsarFecha.Enabled;
            if (tableLayoutPanel17 != null)
                tableLayoutPanel17.Visible = usaFechas;

            // Si no hay fechas, dar más espacio a los combos
            if (tableLayoutPanel1 != null && tableLayoutPanel1.ColumnStyles.Count >= 2)
            {
                if (usaFechas)
                {
                    tableLayoutPanel1.ColumnStyles[0].Width = 58F;
                    tableLayoutPanel1.ColumnStyles[1].Width = 42F;
                }
                else
                {
                    tableLayoutPanel1.ColumnStyles[0].Width = 100F;
                    tableLayoutPanel1.ColumnStyles[1].Width = 0F;
                }
            }
        }

        #region ENTER

        protected override void EjecutarEnter()
        {
            if (dgvGrilla.Focused && entidadID.HasValue)
            {
                EjecutarDobleClickFila(entidadID);
                return;
            }

            if (ActiveControl is ComboBox cb && cb.DroppedDown)
                return;

            if (ActiveControl is TextBox tb && tb.Multiline)
                return;

            paginaActual = 1;

            RefrescarGrilla();
        }

        #endregion

        #region CONFIG FILTROS UI

        protected virtual bool UsarCheck1 => false;
        protected virtual bool UsarCheck2 => false;


        protected virtual void ConfigurarFiltrosUI()
        {
            ConfigurarFechas();

            ConfigurarComboBase(cbx1);
            ConfigurarComboBase(cbx2);
            ConfigurarComboBase(cbx3);

            ConfigurarCheckBase(chkBool1);
            ConfigurarCheckBase(chkBool2);

            AsociarEventosCombos();

            if (UsarCheck1)
            {
                ActivarCheck(chkBool1, "Filtro Check 1");
            }
            if (UsarCheck2)
            {
                ActivarCheck(chkBool2, "Filtro Check 2");
            }
        }

        private void ConfigurarFechas()
        {
            if (chkUsarFecha != null)
                chkUsarFecha.Enabled = false;

            if (chkUsarRango != null)
                chkUsarRango.Enabled = false;

            if (dtpDesde != null)
                dtpDesde.Enabled = false;

            if (dtpHasta != null)
                dtpHasta.Enabled = false;
        }

        private void ConfigurarComboBase(ComboBox combo)
        {
            if (combo == null) return;

            combo.Enabled = false;
            combo.DataSource = null;
            combo.SelectedIndex = -1;
        }

        private void ConfigurarCheckBase(CheckBox check)
        {
            if (check == null) return;

            check.Enabled = false;
            check.Visible = false;
            check.Checked = false;
        }

        private void AsociarEventosCombos()
        {
            if (cbx1 != null)
            {
                cbx1.SelectedIndexChanged -= cbxFiltroOpcional_SelectedIndexChanged;
                cbx1.SelectedIndexChanged += cbxFiltroOpcional_SelectedIndexChanged;
            }

            if (cbx2 != null)
            {
                cbx2.SelectedIndexChanged -= cbxFiltroOpcional_SelectedIndexChanged;
                cbx2.SelectedIndexChanged += cbxFiltroOpcional_SelectedIndexChanged;
            }

            if (cbx3 != null)
            {
                cbx3.SelectedIndexChanged -= cbxFiltroOpcional_SelectedIndexChanged;
                cbx3.SelectedIndexChanged += cbxFiltroOpcional_SelectedIndexChanged;
            }
        }

        protected void ActivarFiltroFechas(string textoCheck)
        {
            chkUsarFecha.Enabled = true;
            chkUsarFecha.Visible = true;
            chkUsarFecha.Text = textoCheck;

            chkUsarRango.Enabled = false;
            chkUsarRango.Visible = true;
            chkUsarRango.Checked = false;

            dtpDesde.Enabled = false;
            dtpDesde.Visible = true;
            dtpHasta.Enabled = false;
            dtpHasta.Visible = true;

            if (tableLayoutPanel17 != null)
                tableLayoutPanel17.Visible = true;

            ConfigurarFechaCorta(dtpDesde);
            ConfigurarFechaCorta(dtpHasta);

            chkUsarFecha.CheckedChanged -= chkUsarFecha_CheckedChanged;
            chkUsarFecha.CheckedChanged += chkUsarFecha_CheckedChanged;

            chkUsarRango.CheckedChanged -= chkUsarRango_CheckedChanged;
            chkUsarRango.CheckedChanged += chkUsarRango_CheckedChanged;
        }

        protected void ActivarCombo(ComboBox combo, Label label, object data, string display, string value, string textoLabel)
        {
            if (combo == null) return;

            combo.Enabled = true;
            combo.Visible = true;

            combo.DataSource = data;
            combo.DisplayMember = display;
            combo.ValueMember = value;

            combo.SelectedIndex = -1;

            if (label != null)
            {
                label.Visible = true;
                label.Text = textoLabel;
            }

            if (combo.Parent != null)
                combo.Parent.Visible = true;
        }

        protected void ActivarCheck(CheckBox check, string texto)
        {
            if (check == null) return;

            check.Visible = true;
            check.Enabled = true;
            check.Text = texto;
        }

        protected virtual void ActualizarTextosLabels()
        {
            ActualizarLabel(lblcbx1, TextoLblCbx1);
            ActualizarLabel(lblcbx2, TextoLblCbx2);
            ActualizarLabel(lblcbx3, TextoLblCbx3);
            ActualizarLabel(lblContenidoTexto, TextoTitular);

            if (lblBuscar != null)
                lblBuscar.Text = TextoLblBuscar;
        }

        private void ActualizarLabel(Label label, string texto)
        {
            if (label == null) return;

            label.Text = texto;
        }

        #endregion

        #region FILTROS

        protected virtual string NormalizarTexto(string texto)
        {
            return NormalizadorTextoBusqueda.NormalizarTextoParaBusqueda(texto);
        }
protected virtual string NormalizarTextoBusqueda(string texto)
    {
        if (string.IsNullOrWhiteSpace(texto))
            return string.Empty;

        texto = texto.Trim().ToLowerInvariant().Normalize(NormalizationForm.FormD);

        var sb = new StringBuilder();

        foreach (var c in texto)
        {
            var categoria = CharUnicodeInfo.GetUnicodeCategory(c);
            if (categoria != UnicodeCategory.NonSpacingMark)
                sb.Append(c);
        }

        return sb.ToString().Normalize(NormalizationForm.FormC);
    }
    protected virtual FiltroConsulta ObtenerFiltros()
        {
            return new FiltroConsulta
            {
                TextoBuscar = NormalizarTextoBusqueda(txtBuscar.Text),

                FechaDesde = ObtenerFechaDesdeUI(),
                FechaHasta = ObtenerFechaHastaUI(),

                Filtro1 = ObtenerFiltroCombo(cbx1),
                Filtro2 = ObtenerFiltroCombo(cbx2),
                Filtro3 = ObtenerFiltroCombo(cbx3),

                Bool1 = ObtenerFiltroBool(chkBool1),
                Bool2 = ObtenerFiltroBool(chkBool2),

                Page = paginaActual,
                PageSize = pageSize
            };
        }

        protected void RefrescarGrilla()
        {
            ultimoFiltro = ObtenerFiltros();

            ActualizarDatos(dgvGrilla, ultimoFiltro);

            EvaluarAccionesPorEstado(ultimoFiltro);
            AjustarAlturaFilasParaPageSize();
        }

        protected virtual object ObtenerFiltroCombo(ComboBox combo)
        {
            if (combo == null || !combo.Enabled)
                return null;

            return combo.SelectedValue;
        }

        protected virtual bool ObtenerFiltroBool(CheckBox check)
        {
            if (check == null || !check.Enabled)
                return false;

            return check.Checked;
        }

        protected virtual DateTime? ObtenerFechaDesdeUI()
        {
            if (!chkUsarFecha.Checked)
                return null;

            return dtpDesde.Value.Date;
        }

        protected virtual DateTime? ObtenerFechaHastaUI()
        {
            if (!chkUsarFecha.Checked)
                return null;

            if (!chkUsarRango.Checked)
                return dtpDesde.Value.Date;

            return dtpHasta.Value.Date;
        }

        protected virtual bool EsModoSoloLectura(FiltroConsulta filtro)
        {
            return filtro.Bool1;
        }

        #endregion

        #region DATOS

        public virtual void ActualizarDatos(DataGridView dgv, FiltroConsulta filtros)
        {
            var soloLectura = EsModoSoloLectura(filtros);

            btnEliminar.Enabled = !soloLectura;
            btnNuevo.Enabled = !soloLectura;
            btnModificar.Enabled = !soloLectura;
        }

        public virtual void ResetearGrilla(DataGridView grilla)
        {
            for (int i = 0; i < grilla.ColumnCount; i++)
                grilla.Columns[i].Visible = false;
        }

        /// <summary>
        /// Columna proporcional (modo Fill). No usar Width con Fill a nivel grilla.
        /// </summary>
        protected static void ColumnaFill(
            DataGridViewColumn col,
            float fillWeight,
            int minimumWidth,
            string headerText = null)
        {
            if (col == null) return;
            col.Visible = true;
            col.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            col.FillWeight = fillWeight;
            col.MinimumWidth = Math.Max(1, minimumWidth);
            if (!string.IsNullOrWhiteSpace(headerText))
                col.HeaderText = headerText;
        }

        /// <summary>
        /// Columna de ancho fijo. Usar cuando el resto de columnas son Fill
        /// (o la grilla no está en Fill global).
        /// </summary>
        protected static void ColumnaFija(
            DataGridViewColumn col,
            int width,
            string headerText = null)
        {
            if (col == null) return;
            col.Visible = true;
            col.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            col.Width = width;
            col.MinimumWidth = Math.Max(1, width);
            if (!string.IsNullOrWhiteSpace(headerText))
                col.HeaderText = headerText;
        }

        #endregion

        #region BOTONES BASE

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            EjecutarBtnNuevo();
        }

        public virtual void EjecutarBtnNuevo() { }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            EjecutarBtnModificar();
        }

        public virtual void EjecutarBtnModificar()
        {
            if (!ValidarSeleccion()) return;

            puedeEjecutarComando = true;
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            EjecutarBtnEliminar();
        }

        public virtual void EjecutarBtnEliminar()
        {
            if (!ValidarSeleccion()) return;

            puedeEjecutarComando = true;
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            RefrescarGrilla();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Close();
        }

        #endregion

        #region GRILLA

        /// <summary>
        /// Grilla con scroll vertical/horizontal para resoluciones o ventanas chicas.
        /// El área vacía (menos filas que pageSize) sigue mostrando fondo + marca de agua.
        /// </summary>
        private void ConfigurarGrillaConsulta()
        {
            if (dgvGrilla == null) return;

            dgvGrilla.ScrollBars = ScrollBars.Both;
            dgvGrilla.BackgroundColor = Color.FromArgb(236, 230, 245);
            dgvGrilla.BorderStyle = BorderStyle.None;
            AjustarAlturaFilasParaPageSize();
        }

        /// <summary>
        /// Altura de fila legible: si hay espacio, reparte; si la ventana es chica,
        /// mantiene un mínimo y deja scrollear.
        /// </summary>
        private void AjustarAlturaFilasParaPageSize()
        {
            if (dgvGrilla == null || dgvGrilla.IsDisposed || pageSize <= 0)
                return;

            const int alturaMinima = 28;
            const int alturaMaxima = 44;

            int header = dgvGrilla.ColumnHeadersVisible ? dgvGrilla.ColumnHeadersHeight : 0;
            int available = dgvGrilla.ClientSize.Height - header - 2;
            if (available < 40)
                return;

            int rowH = available / pageSize;
            if (rowH < alturaMinima)
                rowH = alturaMinima; // ventana chica → scroll vertical
            else if (rowH > alturaMaxima)
                rowH = alturaMaxima;

            dgvGrilla.RowTemplate.Height = rowH;

            foreach (DataGridViewRow row in dgvGrilla.Rows)
            {
                if (!row.IsNewRow && row.Height != rowH)
                    row.Height = rowH;
            }
        }

        private void DgvGrilla_PaintFondoVacio(object sender, PaintEventArgs e)
        {
            if (dgvGrilla == null || dgvGrilla.IsDisposed)
                return;

            int top = dgvGrilla.ColumnHeadersVisible ? dgvGrilla.ColumnHeadersHeight : 0;
            if (dgvGrilla.Rows.Count > 0)
            {
                try
                {
                    var last = dgvGrilla.GetRowDisplayRectangle(dgvGrilla.Rows.Count - 1, true);
                    if (last.Height > 0)
                        top = Math.Max(top, last.Bottom);
                }
                catch
                {
                    // Ignorar si la fila aún no está medida.
                }
            }

            if (top >= dgvGrilla.ClientSize.Height - 8)
                return;

            var empty = Rectangle.FromLTRB(0, top, dgvGrilla.ClientSize.Width, dgvGrilla.ClientSize.Height);

            using (var fill = new SolidBrush(Color.FromArgb(236, 230, 245)))
                e.Graphics.FillRectangle(fill, empty);

            // Banda superior suave para separar filas del vacío.
            using (var accent = new SolidBrush(Color.FromArgb(55, TemaSistema.Seleccion)))
                e.Graphics.FillRectangle(accent, empty.Left, empty.Top, empty.Width, Math.Min(6, empty.Height));

            var logo = Constantes.Imagenes.ImgLogoCompuesto;
            if (logo == null || empty.Height < 60 || empty.Width < 80)
                return;

            int maxW = Math.Min(300, empty.Width * 2 / 5);
            int maxH = Math.Min(170, empty.Height - 24);
            if (maxW < 48 || maxH < 48)
                return;

            float scale = Math.Min((float)maxW / logo.Width, (float)maxH / logo.Height);
            int w = Math.Max(1, (int)(logo.Width * scale));
            int h = Math.Max(1, (int)(logo.Height * scale));
            int x = empty.Left + (empty.Width - w) / 2;
            int y = empty.Top + (empty.Height - h) / 2;

            var matrix = new ColorMatrix { Matrix33 = 0.11f };
            using var attrs = new ImageAttributes();
            attrs.SetColorMatrix(matrix);
            e.Graphics.DrawImage(
                logo,
                new Rectangle(x, y, w, h),
                0, 0, logo.Width, logo.Height,
                GraphicsUnit.Pixel,
                attrs);
        }

        private void DgvGrilla_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Right)
                return;

            var hit = dgvGrilla.HitTest(e.X, e.Y);

            if (hit.RowIndex >= 0)
            {
                dgvGrilla.ClearSelection();

                dgvGrilla.Rows[hit.RowIndex].Selected = true;

                RowEnter(new DataGridViewCellEventArgs(0, hit.RowIndex));

                EjecutarClickDerechoFila(entidadID, e.Location);
            }
        }

        private void DgvGrilla_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;
        }

        private void DgvGrilla_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            RowEnter(e);

            EjecutarDobleClickFila(entidadID);
        }

        private void DgvGrilla_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            RowEnter(e);
        }

        public virtual void EjecutarClickDerechoFila(long? id, Point posicionMouse)
        {

        }

        public virtual void EjecutarDobleClickFila(long? id)
        {

        }

        public virtual void RowEnter(DataGridViewCellEventArgs e)
        {
            try
            {
                entidadID = null;

                if (e.RowIndex < 0 || dgvGrilla.RowCount == 0)
                    return;

                if (!dgvGrilla.Columns.Contains("Id"))
                    return;

                var fila = dgvGrilla.Rows[e.RowIndex];

                if (fila?.Cells["Id"].Value == null)
                    return;

                entidadID = Convert.ToInt64(fila.Cells["Id"].Value);
            }
            catch
            {
                entidadID = null;
            }
        }

        #endregion

        #region VALIDACIONES

        private bool ValidarSeleccion()
        {
            if (dgvGrilla.RowCount == 0)
            {
                MessageBox.Show("No hay datos cargados.");
                return false;
            }

            if (!entidadID.HasValue)
            {
                MessageBox.Show("Seleccione un registro.");
                return false;
            }

            return true;
        }

        #endregion

        #region ACCIONES DINAMICAS

        protected virtual void ConfigurarAccionesPersonalizadas()
        {

        }

        protected void AgregarAccion(
            string nombre,
            Image icono,
            Action<long?> ejecutar,
            bool requiereSeleccion = true,
            bool soloSiNoEliminado = false)
        {
            AccionesPersonalizadas.Add(new AccionGrid
            {
                Nombre = nombre,
                Icono = icono,
                Ejecutar = ejecutar,
                RequiereSeleccion = requiereSeleccion,
                SoloSiNoEliminado = soloSiNoEliminado
            });
        }

        private void CrearBotonesPersonalizados()
        {
            foreach (var accion in AccionesPersonalizadas)
            {
                var btn = new ToolStripButton
                {
                    Text = accion.Nombre,
                    Image = accion.Icono,
                    DisplayStyle = ToolStripItemDisplayStyle.ImageAndText,
                    TextImageRelation = TextImageRelation.TextAboveImage,
                    Enabled = true,
                    Tag = accion
                };

                btn.Click += (s, e) =>
                {
                    if (accion.RequiereSeleccion && !entidadID.HasValue)
                    {
                        MessageBox.Show("Seleccione un registro.");
                        return;
                    }

                    accion.Ejecutar?.Invoke(entidadID);
                };

                BarraLateralBotones.Items.Add(new ToolStripSeparator());
                BarraLateralBotones.Items.Add(btn);
            }
        }

        private void EvaluarAccionesPorEstado(FiltroConsulta filtros)
        {
            foreach (ToolStripItem item in BarraLateralBotones.Items)
            {
                if (item is ToolStripButton btn && btn.Tag is AccionGrid accion)
                {
                    btn.Enabled = accion.SoloSiNoEliminado
                        ? !EsModoSoloLectura(filtros)
                        : true;
                }
            }
        }

        #endregion

        #region PAGINADO

        protected void ActualizarPaginacionUI(DatosPaginacion resultado)
        {
            paginaActual = resultado.PaginaActual;
            totalPaginas = resultado.TotalPaginas;

            lblPagina.Text = $"Página {paginaActual} de {totalPaginas}";
            lblTotalRegistros.Text = $"Total: {resultado.CantidadRegistros}";

            btnAnterior.Enabled = paginaActual > 1;
            btnSiguiente.Enabled = paginaActual < totalPaginas;
        }

        private void btnSiguiente_Click(object sender, EventArgs e)
        {
            if (paginaActual < totalPaginas)
            {
                paginaActual++;
                RefrescarGrilla();
            }
        }

        private void btnAnterior_Click(object sender, EventArgs e)
        {
            if (paginaActual > 1)
            {
                paginaActual--;
                RefrescarGrilla();
            }
        }

        #endregion

        #region EVENTOS FILTROS

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            paginaActual = 1;

            RefrescarGrilla();
        }
        private void chkBool2_CheckedChanged(object sender, EventArgs e)
        {
            if (_actualizandoFiltros)
                return;

            paginaActual = 1;
            AccionCheck2();
            RefrescarGrilla();
        }

        private void chkBool1_CheckedChanged(object sender, EventArgs e)
        {
            if (_actualizandoFiltros)
                return;

            paginaActual = 1;
            AccionCheck1();
            RefrescarGrilla();
        }

        protected virtual void AccionCheck1() { }
        protected virtual void AccionCheck2() { }

        private void chkUsarFecha_CheckedChanged(object sender, EventArgs e)
        {
            if (!chkUsarFecha.Checked)
            {
                dtpDesde.Enabled = false;

                dtpHasta.Enabled = false;

                chkUsarRango.Checked = false;

                chkUsarRango.Enabled = false;

                return;
            }

            dtpDesde.Enabled = true;

            chkUsarRango.Enabled = true;

            dtpHasta.Enabled = chkUsarRango.Checked;
        }

        private void chkUsarRango_CheckedChanged(object sender, EventArgs e)
        {
            if (!chkUsarFecha.Checked)
                return;

            dtpHasta.Enabled = chkUsarRango.Checked;
        }

        private void cbxFiltroOpcional_SelectedIndexChanged(object sender, EventArgs e)
        {
            ActualizarLabelBusqueda();
        }

        protected virtual void ActualizarLabelBusqueda()
        {
            if (lblBuscar == null)
                return;

            string texto = string.Empty;

            if (cbx1 != null && cbx1.Enabled && !string.IsNullOrWhiteSpace(cbx1.Text))
                texto = cbx1.Text;
            else if (cbx2 != null && cbx2.Enabled && !string.IsNullOrWhiteSpace(cbx2.Text))
                texto = cbx2.Text;
            else if (cbx3 != null && cbx3.Enabled && !string.IsNullOrWhiteSpace(cbx3.Text))
                texto = cbx3.Text;

            if (string.IsNullOrWhiteSpace(texto) || texto == "Todos")
            {
                lblBuscar.Text = "Buscar:";
            }
            else
            {
                lblBuscar.Text = $"Buscar por: {texto}";
            }
        }

        #endregion

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarFiltros();

            paginaActual = 1;

            RefrescarGrilla();
        }

        protected virtual void LimpiarFiltros()
        {
            LimpiarTexto();
            LimpiarCombos();
            LimpiarChecks();
            LimpiarFechas();

            ActualizarLabelBusqueda();

            LimpiarFiltrosCustom();
        }

        protected virtual void LimpiarFiltrosCustom()
        {
            // para override en hijos
        }

        private void LimpiarTexto()
        {
            if (txtBuscar != null)
                txtBuscar.Text = string.Empty;
        }

        private void LimpiarCombos()
        {
            LimpiarCombo(cbx1);
            LimpiarCombo(cbx2);
            LimpiarCombo(cbx3);
        }

        private void LimpiarCombo(ComboBox combo)
        {
            if (combo == null || !combo.Enabled)
                return;

            combo.SelectedIndex = -1;

            if (combo.Items.Count > 0)
                combo.SelectedIndex = 0;
        }

        private void LimpiarChecks()
        {
            if (chkBool1 != null && chkBool1.Enabled)
                chkBool1.Checked = false;

            if (chkBool2 != null && chkBool2.Enabled)
                chkBool2.Checked = false;
        }

        private void LimpiarFechas()
        {
            if (chkUsarFecha != null)
                chkUsarFecha.Checked = false;

            if (chkUsarRango != null)
                chkUsarRango.Checked = false;

            if (dtpDesde != null)
                dtpDesde.Value = DateTime.Now;

            if (dtpHasta != null)
                dtpHasta.Value = DateTime.Now;
        }

       
    }
}