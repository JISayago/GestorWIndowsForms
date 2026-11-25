
using Presentacion.Core.Configuracion;
using Presentacion.FBase.Helpers;
using Presentacion.FormulariosBase.Helpers;
using Servicios.LogicaNegocio.Configuracion;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Presentacion.Core.Administracion.Configuracion
{
    public partial class FConfiguracion : FBase.FBase
    {
        private readonly IConfiguracionServicio _servicio = new ConfiguracionServicio();

        private readonly Dictionary<SeccionConfiguracion, SeccionBase> _cargadas = new();
        private bool _esContenidoEmbebido;

        private bool _hayCambios;
        private bool _suspenderCambios;


        public FConfiguracion()
        {
            InitializeComponent();

            lblSeccionDescripcion.ForeColor = TemaSistema.TextoSecundario;
            lblCambios.ForeColor = TemaSistema.TextoSecundario;

            foreach (var definicion in RegistroSecciones.Todas)
                lstSecciones.Items.Add(definicion);

            ConfigurarBarraAcciones();

            Load += OnCargar;
            FormClosing += OnCerrando;

            FormClosed += (_, _) =>
            {
                foreach (var seccion in _cargadas.Values)
                    seccion.Dispose();
            };
        }
        private void ConfigurarBarraAcciones()
        {
            pnlAcciones.SuspendLayout();

            try
            {
                // Desvincular los controles de su ubicación anterior.
                lblCambios.Parent?.Controls.Remove(lblCambios);
                btnGuardarCambios.Parent?.Controls.Remove(btnGuardarCambios);

                pnlAcciones.Controls.Clear();

                pnlAcciones.Visible = true;
                pnlAcciones.Padding = new Padding(8, 4, 8, 4);

                // Distribución general de la barra.
                var layoutAcciones = new TableLayoutPanel
                {
                    Dock = DockStyle.Fill,
                    ColumnCount = 2,
                    RowCount = 1,
                    Margin = Padding.Empty,
                    Padding = Padding.Empty
                };

                layoutAcciones.ColumnStyles.Add(
                    new ColumnStyle(SizeType.Percent, 100F));

                layoutAcciones.ColumnStyles.Add(
                    new ColumnStyle(SizeType.Absolute, 330F));

                layoutAcciones.RowStyles.Add(
                    new RowStyle(SizeType.Percent, 100F));

                // Texto de estado.
                lblCambios.Dock = DockStyle.Fill;
                lblCambios.TextAlign = ContentAlignment.MiddleLeft;
                lblCambios.Visible = true;

                // Contenedor de botones.
                _panelBotonesAcciones = new FlowLayoutPanel
                {
                    Dock = DockStyle.Fill,
                    FlowDirection = FlowDirection.RightToLeft,
                    WrapContents = false,
                    Margin = Padding.Empty,
                    Padding = Padding.Empty
                };

                // Botón guardar existente.
                btnGuardarCambios.Text = "Guardar cambios";
                btnGuardarCambios.Visible = true;
                btnGuardarCambios.Size = new Size(145, 38);
                btnGuardarCambios.Margin = new Padding(4, 3, 4, 3);

                btnGuardarCambios.Click -= OnGuardarCambiosClick;
                btnGuardarCambios.Click += OnGuardarCambiosClick;

                // Nuevo botón descartar.
                _btnDescartarCambios = new Button
                {
                    Name = "btnDescartarCambios",
                    Text = "Descartar cambios",
                    Size = new Size(155, 38),
                    Font = btnGuardarCambios.Font,
                    FlatStyle = btnGuardarCambios.FlatStyle,
                    BackColor = btnGuardarCambios.BackColor,
                    ForeColor = btnGuardarCambios.ForeColor,
                    UseVisualStyleBackColor =
                        btnGuardarCambios.UseVisualStyleBackColor,
                    Margin = new Padding(4, 3, 4, 3)
                };

                _btnDescartarCambios.Click += OnDescartarCambiosClick;

                // El botón guardar queda a la derecha.
                _panelBotonesAcciones.Controls.Add(btnGuardarCambios);
                _panelBotonesAcciones.Controls.Add(_btnDescartarCambios);

                layoutAcciones.Controls.Add(lblCambios, 0, 0);
                layoutAcciones.Controls.Add(_panelBotonesAcciones, 1, 0);

                pnlAcciones.Controls.Add(layoutAcciones);

                ActualizarBarraAcciones();
            }
            finally
            {
                pnlAcciones.ResumeLayout(true);
            }
        }
        public bool TieneCambiosPendientes => _hayCambios;

        private void OnCargar(object? sender, EventArgs e)
        {
            if (lstSecciones.Items.Count > 0 && lstSecciones.SelectedIndex < 0)
                lstSecciones.SelectedIndex = 0;
        }

        /// <summary>
        /// Prepara el formulario para mostrarse dentro de FAdministracion.
        /// </summary>
        public void PrepararComoContenidoEmbebido()
        {
            _esContenidoEmbebido = true;

            // Convertir realmente el formulario en un control embebido.
            TopLevel = false;
            FormBorderStyle = FormBorderStyle.None;
            WindowState = FormWindowState.Normal;
            ShowInTaskbar = false;
            ControlBox = false;
            MaximizeBox = false;
            MinimizeBox = false;
            Dock = DockStyle.Fill;

            AutoSize = false;
            MinimumSize = Size.Empty;
            MaximumSize = Size.Empty;
            Padding = Padding.Empty;

            BackColor = TemaSistema.Fondo;

            pnlMarco.BorderStyle = BorderStyle.None;
            pnlMarco.Padding = new Padding(24, 16, 24, 16);

            pnlContenido.BorderStyle = BorderStyle.None;
            pnlContenido.Padding = Padding.Empty;

            lstSecciones.BorderStyle = BorderStyle.None;
            lstSecciones.BackColor = TemaSistema.Fondo;

            pnlAcciones.Visible = true;
        }
        private void ActualizarBarraAcciones()
        {
            pnlAcciones.Visible = true;

            lblCambios.Text = _hayCambios
                ? "Hay cambios pendientes de guardar."
                : "No hay cambios pendientes.";

            btnGuardarCambios.Visible = true;
            btnGuardarCambios.Enabled = _hayCambios;

            if (_btnDescartarCambios != null)
            {
                _btnDescartarCambios.Visible = true;
                _btnDescartarCambios.Enabled = _hayCambios;
            }
        }
        //// =========================================================
        //// BARRA INFERIOR DE ACCIONES
        //// =========================================================

     

        //private void ActualizarBarraAcciones()
        //{
        //    // No ocultar el panel: debe conservar siempre su espacio.
        //    pnlAcciones.Visible = true;

        //    lblCambios.Text = _hayCambios
        //        ? "Hay cambios pendientes de guardar."
        //        : "No hay cambios pendientes.";

        //    btnGuardarCambios.Enabled = _hayCambios;

        //    if (_btnDescartarCambios != null)
        //        _btnDescartarCambios.Enabled = _hayCambios;
        //}

        private void MarcarCambios()
        {
            if (_suspenderCambios)
                return;

            _hayCambios = true;
            ActualizarBarraAcciones();
        }

        private void LimpiarCambios()
        {
            _hayCambios = false;
            ActualizarBarraAcciones();
        }

        private void OnDescartarCambiosClick(object? sender, EventArgs e)
        {
            DescartarCambios();
        }

        /// <summary>
        /// Descarta las modificaciones de todas las secciones abiertas
        /// y vuelve a cargar los valores persistidos.
        /// </summary>
        private void DescartarCambios()
        {
            if (!_hayCambios)
                return;

            var respuesta = MessageBox.Show(
                "Se descartarán todos los cambios pendientes de las secciones abiertas " +
                "y se volverán a cargar los últimos valores guardados.\n\n" +
                "¿Desea continuar?",
                "Descartar cambios",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (respuesta != DialogResult.Yes)
                return;

            int indiceSeleccionado = lstSecciones.SelectedIndex;

            _suspenderCambios = true;
            pnlContenido.SuspendLayout();

            try
            {
                // Retiramos los controles que muestran los valores modificados.
                pnlContenido.Controls.Clear();

                // Descartamos las instancias que contienen los cambios en memoria.
                foreach (var seccion in _cargadas.Values.ToArray())
                    seccion.Dispose();

                _cargadas.Clear();

                // Actualizamos el estado de la barra.
                LimpiarCambios();

                // Volvemos a seleccionar la sección actual.
                // Esto provoca que se cree una instancia nueva y se ejecute Cargar().
                if (indiceSeleccionado >= 0 &&
                    indiceSeleccionado < lstSecciones.Items.Count)
                {
                    lstSecciones.SelectedIndex = -1;
                    lstSecciones.SelectedIndex = indiceSeleccionado;
                }
                else
                {
                    lblSeccionTitulo.Text = string.Empty;
                    lblSeccionDescripcion.Text = string.Empty;
                    _suspenderCambios = false;
                }
            }
            finally
            {
                pnlContenido.ResumeLayout(true);

                if (indiceSeleccionado < 0)
                    _suspenderCambios = false;
            }
        }

        // =========================================================
        // CARGA DE SECCIONES
        // =========================================================

        private void OnSeccionSeleccionada(object? sender, EventArgs e)
        {
            if (lstSecciones.SelectedItem is not DefinicionSeccion definicion)
                return;

            var esNueva = false;

            if (!_cargadas.TryGetValue(definicion.Clave, out var seccion))
            {
                esNueva = true;

                // La carga inicial no debe contabilizarse como modificación.
                _suspenderCambios = true;

                seccion = definicion.Crear();
                seccion.Cargar();

                if (seccion.DetectaCambiosAutomaticamente)
                    EngancharCambios(seccion);

                seccion.Modificada += (_, _) => MarcarCambios();

                _cargadas[definicion.Clave] = seccion;
            }

            pnlContenido.SuspendLayout();

            try
            {
                pnlContenido.Controls.Clear();

                seccion.Dock = DockStyle.Fill;
                seccion.Margin = Padding.Empty;

                pnlContenido.Controls.Add(seccion);
                seccion.BringToFront();

                lblSeccionTitulo.Text = definicion.Titulo;
                lblSeccionDescripcion.Text = seccion.Descripcion;
            }
            finally
            {
                pnlContenido.ResumeLayout(true);

                if (esNueva)
                {
                    if (IsHandleCreated)
                        BeginInvoke(new Action(() => _suspenderCambios = false));
                    else
                        _suspenderCambios = false;
                }
            }
        }

        // =========================================================
        // DETECCIÓN AUTOMÁTICA DE CAMBIOS
        // =========================================================

        private void EngancharCambios(Control control)
        {
            switch (control)
            {
                case TextBoxBase t:
                    t.TextChanged += OnControlModificado;
                    break;

                case CheckBox c:
                    c.CheckedChanged += OnControlModificado;
                    break;

                case RadioButton r:
                    r.CheckedChanged += OnControlModificado;
                    break;

                case ComboBox cb:
                    cb.SelectedIndexChanged += OnControlModificado;

                    if (cb.DropDownStyle != ComboBoxStyle.DropDownList)
                        cb.TextChanged += OnControlModificado;

                    break;

                case NumericUpDown n:
                    n.ValueChanged += OnControlModificado;
                    break;

                case DateTimePicker d:
                    d.ValueChanged += OnControlModificado;
                    break;

                case DataGridView g:
                    g.CellValueChanged += (_, _) => MarcarCambios();
                    g.RowsRemoved += (_, _) => MarcarCambios();
                    break;
            }

            foreach (Control hijo in control.Controls)
                EngancharCambios(hijo);

            control.ControlAdded += (_, e) =>
                EngancharCambios(e.Control);
        }

        private void OnControlModificado(object? sender, EventArgs e)
        {
            MarcarCambios();
        }

        // =========================================================
        // CIERRE DEL FORMULARIO
        // =========================================================

        public bool PuedeCerrar()
        {
            if (!_hayCambios)
                return true;

            var respuesta = MessageBox.Show(
                "Hay cambios sin guardar en la configuración. " +
                "¿Desea guardarlos antes de salir?",
                "Atención",
                MessageBoxButtons.YesNoCancel,
                MessageBoxIcon.Question);

            if (respuesta == DialogResult.Cancel)
                return false;

            if (respuesta == DialogResult.Yes)
                return GuardarCambios();

            return true;
        }

        private void OnCerrando(object? sender, FormClosingEventArgs e)
        {
            if (_esContenidoEmbebido ||
                e.CloseReason != CloseReason.UserClosing)
                return;

            if (!PuedeCerrar())
                e.Cancel = true;
        }

        // =========================================================
        // GUARDADO
        // =========================================================

        private void OnGuardarCambiosClick(object? sender, EventArgs e)
        {
            GuardarCambios();
        }

        public bool GuardarCambios()
        {
            // Validamos todas las secciones que fueron abiertas.
            foreach (var seccion in _cargadas.Values)
            {
                var mensajeError = seccion.Validar();

                if (mensajeError == null)
                    continue;

                lstSecciones.SelectedItem = RegistroSecciones.Todas
                    .First(d => d.Clave == seccion.Clave);

                MessageBox.Show(
                    mensajeError,
                    "Atención",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return false;
            }

            var lote = new LoteConfiguracion();

            foreach (var seccion in _cargadas.Values)
                seccion.Guardar(lote);

            var resultado = _servicio.Guardar(lote);

            if (!resultado.Exitoso)
            {
                MessageBox.Show(
                    "No se pudo guardar la configuración: " + resultado.Mensaje,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                return false;
            }

            // Los ajustes posteriores al guardado no cuentan como modificaciones.
            _suspenderCambios = true;

            try
            {
                foreach (var seccion in _cargadas.Values)
                    seccion.DespuesDeGuardar();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "La configuración se guardó, pero falló un cambio local: " +
                    ex.Message,
                    "Atención",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
            finally
            {
                _suspenderCambios = false;
            }

            LimpiarCambios();

            MessageBox.Show(
                "La configuración se guardó correctamente.",
                "Atención",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);

            return true;
        }
    }
}
