using MigraDoc.DocumentObjectModel.Internals;
using Presentacion.Core.Administracion;
using Presentacion.FBase.Helpers;
using Presentacion.FormulariosBase.DTO;
using ScottPlot.WinForms;
using System.Text.Json.Nodes;
using System.Collections;
using System.ComponentModel;
using System.Reflection;
using System.Linq;
using Presentacion.FBase.Helpers;

namespace Presentacion.FBase
{
    public enum PosicionMensajeError { Abajo, Derecha }
    public partial class FBase : Form
        {
            // ---- Configuración de validación (los hijos pueden cambiarla) ----
            protected PosicionMensajeError PosicionMensaje { get; set; } = PosicionMensajeError.Abajo;
            protected bool MostrarIconoErrorProvider { get; set; } = false;
            protected Color ColorError { get; set; } = Color.Firebrick;
            protected Color ColorFondoError { get; set; } = Color.FromArgb(255, 220, 220);

            private readonly List<CampoValidable> _campos = new List<CampoValidable>();
            private readonly HashSet<Control> _padresConPaint = new HashSet<Control>();

            public FBase()
            {
                InitializeComponent();
                this.KeyPreview = true;

                // Antes se pisaba "components" con uno nuevo, lo que descartaba el del designer.
                if (components == null) components = new System.ComponentModel.Container();
                if (error == null) error = new System.Windows.Forms.ErrorProvider(components);
                error.BlinkStyle = ErrorBlinkStyle.NeverBlink;

                this.Icon = Icon.ExtractAssociatedIcon(Application.ExecutablePath);
            }
            protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            this.Icon = Icon.ExtractAssociatedIcon(Application.ExecutablePath);
            this.KeyPreview = true;
            AplicarTema(this);
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                EjecutarEscape();
                return true;
            }

            if (keyData == Keys.Enter)
            {
                EjecutarEnter();
                return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        protected virtual void EjecutarEscape()
        {
            this.Close();
        }

        protected virtual void EjecutarEnter()
        {
        }
        private void FBase_Load(object sender, EventArgs e)
        {
        }
        public virtual void DesactivarControles(object obj)
        {
            if (obj is Form)
            {
                foreach (var ctrolForm in ((Form)obj).Controls)
                {
                    if (ctrolForm is TextBox)
                    {
                        ((TextBox)ctrolForm).Enabled = false;
                    }

                    if (ctrolForm is ComboBox)
                    {
                        ((ComboBox)ctrolForm).Enabled = false;
                    }

                    if (ctrolForm is NumericUpDown)
                    {
                        ((NumericUpDown)ctrolForm).Enabled = false;
                    }

                    if (ctrolForm is DateTimePicker)
                    {
                        ((DateTimePicker)ctrolForm).Enabled = false;
                    }

                    if (ctrolForm is Button)
                    {
                        ((Button)ctrolForm).Enabled = false;
                    }

                    if (ctrolForm is Panel)
                    {
                        DesactivarControles(ctrolForm);
                    }
                }
            }
            else if (obj is Panel)
            {
                foreach (var ctrolPanel in ((Panel)obj).Controls)
                {
                    if (ctrolPanel is TextBox)
                    {
                        ((TextBox)ctrolPanel).Enabled = false;
                    }

                    if (ctrolPanel is ComboBox)
                    {
                        ((ComboBox)ctrolPanel).Enabled = false;
                    }

                    if (ctrolPanel is NumericUpDown)
                    {
                        ((NumericUpDown)ctrolPanel).Enabled = false;
                    }

                    if (ctrolPanel is DateTimePicker)
                    {
                        ((DateTimePicker)ctrolPanel).Enabled = false;
                    }

                    if (ctrolPanel is Button)
                    {
                        ((Button)ctrolPanel).Enabled = false;
                    }

                    if (ctrolPanel is Panel)
                    {
                        DesactivarControles(ctrolPanel);
                    }
                }
            }
        }
        public virtual void Limpiar(object obj)
        {
            if (obj is Form)
            {
                foreach (var ctrolForm in ((Form)obj).Controls)
                {
                    if (ctrolForm is TextBox)
                    {
                        ((TextBox)ctrolForm).Clear();
                    }

                    if (ctrolForm is ComboBox)
                    {
                        if (((ComboBox)ctrolForm).Items.Count > 0)
                        {
                            ((ComboBox)ctrolForm).SelectedIndex = 0;
                        }
                    }

                    if (ctrolForm is NumericUpDown)
                    {
                        ((NumericUpDown)ctrolForm).Value = ((NumericUpDown)ctrolForm).Minimum;
                    }

                    if (ctrolForm is DateTimePicker)
                    {
                        ((DateTimePicker)ctrolForm).Value = DateTime.Now;
                    }

                    if (ctrolForm is Panel)
                    {
                        Limpiar(ctrolForm);
                    }
                }
            }
            else if (obj is Panel)
            {
                foreach (var ctrolPanel in ((Panel)obj).Controls)
                {
                    if (ctrolPanel is TextBox)
                    {
                        ((TextBox)ctrolPanel).Clear();
                    }

                    if (ctrolPanel is ComboBox)
                    {
                        if (((ComboBox)ctrolPanel).Items.Count > 0)
                        {
                            ((ComboBox)ctrolPanel).SelectedIndex = 0;
                        }
                    }

                    if (ctrolPanel is NumericUpDown)
                    {
                        ((NumericUpDown)ctrolPanel).Value = ((NumericUpDown)ctrolPanel).Minimum;
                    }

                    if (ctrolPanel is DateTimePicker)
                    {
                        ((DateTimePicker)ctrolPanel).Value = DateTime.Now; // fecha Sistema
                    }

                    if (ctrolPanel is Panel)
                    {
                        Limpiar(ctrolPanel);
                    }

                    if (ctrolPanel is PictureBox)
                    {
                        ((PictureBox)ctrolPanel).Image = Constantes.Imagenes.ImgPerfilUsuario;
                    }
                }
            }
        }
        public virtual void CargarComboBox(ComboBox cmb, object datos, string propiedadMostrar, string propiedadDevolver)
        {
            cmb.DataSource = datos;
            cmb.DisplayMember = propiedadMostrar;
            cmb.ValueMember = propiedadDevolver;
        }
        //public virtual void AgregarControlesObligatorios(object control, string nombreControl)
        //{
        //    _listaControlesObligatorios.Add(new ControlDTO
        //    {
        //        Control = control,
        //        NombreControl = nombreControl
        //    });

        //    AsignarErrorProvider(control);
        //}

        //public virtual void LimpiarControlesObligatorios()
        //{
        //    _listaControlesObligatorios.Clear();
        //    error.Clear();
        //}
        //public virtual bool VerificarDatosObligatorios()
        //{
        //    foreach (var objeto in _listaControlesObligatorios)
        //    {
        //        switch (objeto.Control)
        //        {
        //            case TextBox _:
        //                if (string.IsNullOrEmpty(((TextBox)objeto.Control).Text)) return false;
        //                break;
        //            case RichTextBox _:
        //                if (string.IsNullOrEmpty(((RichTextBox)objeto.Control).Text)) return false;
        //                break;
        //            case NumericUpDown _:
        //                if (string.IsNullOrEmpty(((NumericUpDown)objeto.Control).Text)) return false;
        //                break;
        //            case ComboBox _:
        //                if (((ComboBox)objeto.Control).Items.Count <= 0) return false;
        //                break;
        //        }
        //    }

        //    return true;
        //}
        //public virtual void AsignarErrorProvider(object control)
        //{
        //    if (control is TextBox)
        //    {
        //        ((TextBox)control).Validated += Control_Validated;
        //    }

        //    if (control is RichTextBox)
        //    {
        //        ((RichTextBox)control).Validated += Control_Validated;
        //    }

        //    if (control is ComboBox)
        //    {
        //        ((ComboBox)control).Validated += Control_Validated;
        //    }
        //}

        //public virtual void Control_Validated(object sender, System.EventArgs e)
        //{
        //    if (sender is TextBox)
        //    {
        //        error.SetError(((TextBox)sender),
        //            !string.IsNullOrEmpty(((TextBox)sender).Text)
        //                ? string.Empty
        //                : $"El campo es Obligatorio.");
        //        return;
        //    }

        //    if (sender is RichTextBox)
        //    {
        //        error.SetError(((RichTextBox)sender),
        //            !string.IsNullOrEmpty(((RichTextBox)sender).Text)
        //                ? string.Empty
        //                : $"El campo es Obligatorio.");

        //        return;
        //    }

        //    if (sender is NumericUpDown)
        //    {
        //        error.SetError(((NumericUpDown)sender),
        //            !string.IsNullOrEmpty(((NumericUpDown)sender).Text)
        //                ? string.Empty
        //                : $"El campo es Obligatorio.");

        //        return;
        //    }

        //    if (sender is ComboBox)
        //    {
        //        error.SetError(((ComboBox)sender),
        //            !string.IsNullOrEmpty(((ComboBox)sender).Text)
        //                ? string.Empty
        //                : $"El campo es Obligatorio.");
        //    }
        //}
        // =====================================================================
        //  VALIDACIÓN DE CAMPOS (reemplaza al ErrorProvider simple)
        // =====================================================================

        private class CampoValidable
        {
            public Control Control;
            public string Nombre;
            public List<ReglaValidacion> Reglas = new List<ReglaValidacion>();
            public bool EnError;
            public Label Mensaje;
            public Color BackOriginal;
            public bool BackGuardado;
            public Action Desuscribir;
        }

        /// Campo obligatorio + reglas extra opcionales. Mantiene la firma vieja, así que los hijos no se rompen.
        public virtual void AgregarControlesObligatorios(object control, string nombreControl,
            params ReglaValidacion[] reglasExtra)
        {
            var reglas = new List<ReglaValidacion> { Validaciones.Obligatorio };
            reglas.AddRange(reglasExtra);
            Registrar(control as Control, nombreControl, reglas);
        }

        /// Campo NO obligatorio, pero que si se completa debe cumplir las reglas (ej. email).
        public virtual void AgregarValidacionOpcional(Control control, string nombreCampo,
            params ReglaValidacion[] reglas)
        {
            Registrar(control, nombreCampo, reglas);
        }

        private void Registrar(Control control, string nombre, IEnumerable<ReglaValidacion> reglas)
        {
            if (control == null) return;

            var campo = _campos.FirstOrDefault(x => x.Control == control);
            if (campo == null)
            {
                campo = new CampoValidable { Control = control, Nombre = nombre };
                _campos.Add(campo);
                Suscribir(campo);
            }
            campo.Nombre = nombre;
            campo.Reglas.Clear();
            campo.Reglas.AddRange(reglas);
        }

        private void Suscribir(CampoValidable campo)
        {
            var c = campo.Control;
            EventHandler alSalir = (s, e) => ValidarCampo(campo);
            // Una vez que el campo está en error, se revalida mientras el usuario corrige
            EventHandler alCambiar = (s, e) => { if (campo.EnError) ValidarCampo(campo); };

            c.Validated += alSalir;
            c.TextChanged += alCambiar;
            Action desus = () => { c.Validated -= alSalir; c.TextChanged -= alCambiar; };

            switch (c)
            {
                case ComboBox cmb:
                    cmb.SelectedIndexChanged += alCambiar;
                    desus += () => cmb.SelectedIndexChanged -= alCambiar;
                    break;
                case NumericUpDown nud:
                    nud.ValueChanged += alCambiar;
                    desus += () => nud.ValueChanged -= alCambiar;
                    break;
                case DateTimePicker dtp:
                    dtp.ValueChanged += alCambiar;
                    desus += () => dtp.ValueChanged -= alCambiar;
                    break;
            }
            campo.Desuscribir = desus;
        }

        /// Valida todos los campos registrados, los marca y enfoca el primero con error.
        public virtual bool VerificarDatosObligatorios()
        {
            bool todoOk = true;
            Control primero = null;

            foreach (var campo in _campos)
            {
                if (!campo.Control.Enabled) continue; // campos desactivados no se validan
                if (!ValidarCampo(campo))
                {
                    todoOk = false;
                    if (primero == null) primero = campo.Control;
                }
            }

            primero?.Focus();
            return todoOk;
        }

        /// Solo saca lo visual (rojo, labels, iconos). Mantiene los campos registrados.
        public virtual void LimpiarErroresVisuales()
        {
            foreach (var campo in _campos) OcultarError(campo);
            error.Clear();
        }

        /// Saca todo: errores visuales y registro de campos (para reconfigurar desde cero).
        public virtual void LimpiarControlesObligatorios()
        {
            foreach (var campo in _campos)
            {
                OcultarError(campo);
                campo.Desuscribir?.Invoke();
                if (campo.Mensaje != null)
                {
                    campo.Mensaje.Parent?.Controls.Remove(campo.Mensaje);
                    campo.Mensaje.Dispose();
                }
            }
            _campos.Clear();
            error.Clear();
        }

        private bool ValidarCampo(CampoValidable campo)
        {
            string mensaje = null;
            foreach (var regla in campo.Reglas)
            {
                mensaje = regla(campo.Control, campo.Nombre);
                if (!string.IsNullOrEmpty(mensaje)) break; // primer error gana
            }

            if (string.IsNullOrEmpty(mensaje))
            {
                OcultarError(campo);
                return true;
            }

            MostrarError(campo, mensaje);
            return false;
        }

        private void MostrarError(CampoValidable campo, string mensaje)
        {
            var c = campo.Control;

            // Guardamos el color con el tema ya aplicado, para poder restaurarlo después
            if (!campo.BackGuardado)
            {
                campo.BackOriginal = c.BackColor;
                campo.BackGuardado = true;
            }
            campo.EnError = true;

            if (c is TextBox || c is RichTextBox || c is ComboBox || c is NumericUpDown)
                c.BackColor = ColorFondoError;

            if (MostrarIconoErrorProvider)
                error.SetError(c, mensaje);

            MostrarMensajeLabel(campo, mensaje);

            if (c.Parent != null)
            {
                AsegurarPaintBorde(c.Parent);
                c.Parent.Invalidate();
            }
        }

        private void OcultarError(CampoValidable campo)
        {
            if (!campo.EnError) return;

            var c = campo.Control;
            campo.EnError = false;

            if (campo.BackGuardado)
            {
                c.BackColor = campo.BackOriginal;
                campo.BackGuardado = false;
            }

            error.SetError(c, string.Empty);
            if (campo.Mensaje != null) campo.Mensaje.Visible = false;
            c.Parent?.Invalidate();
        }

        private void MostrarMensajeLabel(CampoValidable campo, string mensaje)
        {
            var c = campo.Control;

            // En un TableLayoutPanel, agregar un control "suelto" desacomoda las celdas.
            // Ahí queda solo el borde rojo + el icono del ErrorProvider con el mensaje en el tooltip.
            if (c.Parent == null || c.Parent is TableLayoutPanel) return;

            if (campo.Mensaje == null)
            {
                campo.Mensaje = new Label
                {
                    AutoSize = true,
                    ForeColor = ColorError,
                    BackColor = Color.Transparent,
                    Font = new Font("Segoe UI", 8F, FontStyle.Regular),
                    Tag = "NoModificarConBase" // para que AplicarEstiloALabels no lo pise
                };
                c.Parent.Controls.Add(campo.Mensaje);
            }

            campo.Mensaje.Text = mensaje;
            campo.Mensaje.Location = PosicionMensaje == PosicionMensajeError.Abajo
                ? new Point(c.Left, c.Bottom + 2)
                : new Point(c.Right + 6, c.Top + (c.Height - campo.Mensaje.Height) / 2);
            campo.Mensaje.Visible = true;
            campo.Mensaje.BringToFront();
        }

        // Borde rojo dibujado en el parent: funciona con CUALQUIER control (DateTimePicker incluido)
        private void AsegurarPaintBorde(Control padre)
        {
            if (padre == null || !_padresConPaint.Add(padre)) return;
            padre.Paint += PadreOnPaint;
        }

        private void PadreOnPaint(object sender, PaintEventArgs e)
        {
            var padre = (Control)sender;
            using (var pen = new Pen(ColorError, 2))
            {
                foreach (var campo in _campos)
                {
                    if (!campo.EnError || campo.Control.Parent != padre || !campo.Control.Visible) continue;
                    var r = campo.Control.Bounds;
                    r.Inflate(2, 2);
                    e.Graphics.DrawRectangle(pen, r);
                }
            }
        }
        /*
               COMIENZO DE SECCION DE TEMA DEL SISTEMA. COLORES Y ASIGNACION DE ESTILOS A LOS CONTROLES
         */
        protected virtual void AplicarTema(Control parent)
        {
            if (parent is Form form)
            {
                form.BackColor = TemaSistema.Fondo;
                form.ForeColor = TemaSistema.Texto;
            }

            foreach (Control control in parent.Controls)
            {
                switch (control)
                {
                    case TableLayoutPanel tlp:
                        ConfugurarTableLayoutPanel(tlp);
                        break;

                    case Button btn:
                        ConfigurarBoton(btn);
                        break;

                    case TextBox txt:
                        ConfigurarTextBox(txt);
                        break;

                    case RichTextBox rtb:
                        ConfigurarRichTextBox(rtb);
                        break;

                    case ComboBox cmb:
                        ConfigurarComboBox(cmb);
                        break;

                    case DateTimePicker dtp:
                        ConfigurarDateTimePicker(dtp);
                        break;

                    case NumericUpDown nud:
                        ConfigurarNumeric(nud);
                        break;

                    case CheckBox chk:
                        ConfigurarCheck(chk);
                        break;

                    case RadioButton rb:
                        ConfigurarRadio(rb);
                        break;

                    case DataGridView dgv:
                        ConfigurarGrilla(dgv);
                        break;

                    case GroupBox gb:
                        gb.ForeColor = TemaSistema.Primario;
                        break;

                    case Panel pnl:
                        // Respeta paneles marcados explícitamente para no ser re-coloreados por el
                        // tema genérico (ej. las "cards" de PanelDatosTurno, que necesitan un color
                        // distinto al fondo general para tener contraste visual).
                        if (!(pnl.Tag is string tagPnl && tagPnl == "card-sin-tema"))
                            pnl.BackColor = TemaSistema.Fondo;
                        break;

                    case MenuStrip ms:
                        ConfigurarMenuStrip(ms);
                        break;

                    case ToolStrip ts:
                        ConfigurarToolStrip(ts);
                        break;

                    case FlatTabControl flatTc:
                        flatTc.AplicarTema();
                        break;

                    case TabControl tc:
                        tc.BackColor = TemaSistema.Fondo;
                        tc.ForeColor = TemaSistema.Texto;

                        foreach (TabPage page in tc.TabPages)
                        {
                            page.BackColor = TemaSistema.Fondo;
                            page.ForeColor = TemaSistema.Texto;
                        }
                        break;

                    case FormsPlot fp:
                        ConfigurarFormPlot(fp);
                        break;
                }

                if (control.HasChildren)
                {
                    AplicarTema(control);
                }
            }
        }
        private void ConfigurarBoton(Button btn)
        {
            btn.BackColor = TemaSistema.Seleccion;
            btn.ForeColor = Color.Black;
            
            btn.FlatStyle = FlatStyle.Flat;

            btn.FlatAppearance.BorderSize = 1;
            btn.FlatAppearance.BorderColor = Color.Black;
        }
        private void ConfigurarTextBox(TextBox txt)
        {
            txt.BackColor = TemaSistema.FondoControl;
            txt.ForeColor = TemaSistema.Texto;
            txt.BorderStyle = BorderStyle.FixedSingle;
        }
        private void ConfigurarRichTextBox(RichTextBox txt)
        {
            txt.BackColor = TemaSistema.FondoControl;
            txt.ForeColor = TemaSistema.Texto;
        }
        private void ConfigurarComboBox(ComboBox cmb)
        {
            cmb.FlatStyle = FlatStyle.Flat;
            cmb.BackColor = TemaSistema.FondoControl;
            cmb.ForeColor = TemaSistema.Texto;
        }
        private void ConfigurarDateTimePicker(DateTimePicker dtp)
        {
            dtp.CalendarForeColor = TemaSistema.Texto;
            dtp.CalendarMonthBackground = TemaSistema.FondoControl;
        }
        private void ConfigurarNumeric(NumericUpDown nud)
        {
            nud.BackColor = TemaSistema.FondoControl;
            nud.ForeColor = TemaSistema.Texto;
        }
        private void ConfigurarCheck(CheckBox chk)
        {
            chk.ForeColor = TemaSistema.Texto;
        }
        private void ConfigurarRadio(RadioButton rb)
        {
            rb.ForeColor = TemaSistema.Texto;
        }
        private void ConfigurarGrilla(DataGridView dgv)
        {
            dgv.EnableHeadersVisualStyles = false;

            dgv.BackgroundColor = TemaSistema.FondoControl;
            dgv.BorderStyle = BorderStyle.None;

            dgv.ColumnHeadersDefaultCellStyle.BackColor = TemaSistema.Oscuro;
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = TemaSistema.Acento;
            dgv.ColumnHeadersDefaultCellStyle.SelectionBackColor = TemaSistema.Oscuro;

            dgv.DefaultCellStyle.BackColor = TemaSistema.FondoControl;
            dgv.DefaultCellStyle.ForeColor = TemaSistema.Texto;

            dgv.DefaultCellStyle.SelectionBackColor = TemaSistema.Seleccion;
            dgv.DefaultCellStyle.SelectionForeColor = TemaSistema.Oscuro;

            dgv.AlternatingRowsDefaultCellStyle.BackColor = TemaSistema.Alternado;

            dgv.GridColor = TemaSistema.Borde;

            dgv.RowHeadersVisible = false;

            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            // Fondo del área vacía (debajo de las filas / sin datos) igual al de las consultas.
            FondoGrillaVacioHelper.Aplicar(dgv);

            // El alto de las filas no debe poder modificarse arrastrando con el mouse (el ancho de
            // las columnas sí). Centralizado acá para que aplique a todos los grids del sistema,
            // incluidos los armados por código. El alto del encabezado también se bloquea, salvo
            // que ya esté en AutoSize (en ese modo el usuario no puede cambiarlo).
            dgv.AllowUserToResizeRows = false;
            if (dgv.ColumnHeadersHeightSizeMode == DataGridViewColumnHeadersHeightSizeMode.EnableResizing)
                dgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;

            // Ordenar al hacer click en el header: la mayoría de los grids del sistema bindean un
            // List<T> plano (dgv.DataSource = resultado.Items), que NO soporta el sort nativo de
            // DataGridView (eso solo funciona con DataTable o listas que implementan IBindingList
            // con SupportsSorting=true). Centralizado acá para que aplique a todos los grids del
            // sistema sin tocar cada formulario individualmente.
            dgv.ColumnHeaderMouseClick -= Grilla_ColumnHeaderMouseClick;
            dgv.ColumnHeaderMouseClick += Grilla_ColumnHeaderMouseClick;
        }

        // Guarda por-grid (no por-form, ya que un mismo form puede tener más de un grid) qué
        // columna y dirección está ordenada actualmente. Se guarda en dgv.Tag porque no está en
        // uso en ningún grid del sistema (verificado) y evita agregar un diccionario global.
        private class OrdenGrillaEstado
        {
            public int ColumnIndex;
            public bool Ascendente;
        }

        private void Grilla_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            var dgv = (DataGridView)sender;

            // Si el DataSource ya soporta ordenamiento nativo (ej. algunos grids bindeados a
            // BindingSource, como los de Roles/Permisos), no interferimos: WinForms ya lo resuelve.
            if (dgv.DataSource is IBindingList blNativo && blNativo.SupportsSorting)
                return;

            if (!(dgv.DataSource is IList lista) || lista.Count == 0)
                return;

            var columna = dgv.Columns[e.ColumnIndex];
            string propiedad = string.IsNullOrEmpty(columna.DataPropertyName) ? columna.Name : columna.DataPropertyName;

            // Tipo real de los elementos, sin necesidad de conocer T en tiempo de compilación
            Type tipoElemento = lista[0].GetType();
            PropertyInfo propInfo = tipoElemento.GetProperty(propiedad, BindingFlags.Public | BindingFlags.Instance);
            if (propInfo == null)
                return; // La columna no mapea a una propiedad real (ej. columna calculada a mano) -> no se ordena

            var estadoAnterior = dgv.Tag as OrdenGrillaEstado;
            bool ascendente = !(estadoAnterior != null && estadoAnterior.ColumnIndex == e.ColumnIndex && estadoAnterior.Ascendente);
            dgv.Tag = new OrdenGrillaEstado { ColumnIndex = e.ColumnIndex, Ascendente = ascendente };

            var ordenado = lista.Cast<object>()
                .OrderBy(x => propInfo.GetValue(x), Comparer<object>.Create(CompararValores))
                .ToList();
            if (!ascendente)
                ordenado.Reverse();

            // Reordenamos la MISMA lista in-place (Clear + Add) en vez de reemplazar dgv.DataSource:
            // así no se pierden columnas ocultas/renombradas a mano (ej.
            // PanelMovimientoVenta.ConfigurarColumnasItems), que se resetearían si se reasigna
            // DataSource con AutoGenerateColumns=true.
            lista.Clear();
            foreach (var item in ordenado)
                lista.Add(item);

            // CurrencyManager.Refresh() releé la lista mutada sin tocar la colección de columnas
            // del grid (a diferencia de volver a asignar dgv.DataSource).
            if (dgv.BindingContext != null && dgv.DataSource != null)
                ((CurrencyManager)dgv.BindingContext[dgv.DataSource]).Refresh();

            foreach (DataGridViewColumn col in dgv.Columns)
            {
                if (col.SortMode != DataGridViewColumnSortMode.NotSortable)
                    col.HeaderCell.SortGlyphDirection = SortOrder.None;
            }

            if (columna.SortMode != DataGridViewColumnSortMode.NotSortable)
                columna.HeaderCell.SortGlyphDirection = ascendente ? SortOrder.Ascending : SortOrder.Descending;
        }

        // Comparador null-safe y tolerante a tipos no-IComparable (ej. si la propiedad es un enum
        // o un objeto sin comparación definida, se compara por texto como último recurso).
        private static int CompararValores(object a, object b)
        {
            if (a == null && b == null) return 0;
            if (a == null) return -1;
            if (b == null) return 1;
            if (a is IComparable comparable) return comparable.CompareTo(b);
            return string.Compare(a.ToString(), b.ToString(), StringComparison.OrdinalIgnoreCase);
        }
        private void ConfigurarToolStrip(ToolStrip ts)
        {
            ts.BackColor = TemaSistema.Seleccion;
            ts.ForeColor = Color.Black;

            ts.Paint -= ToolStrip_PaintBorder;
            ts.Paint += ToolStrip_PaintBorder;

            foreach (ToolStripItem item in ts.Items)
            {
                item.BackColor = TemaSistema.Seleccion;
                item.ForeColor = Color.Black;

                if (item is ToolStripButton btn)
                {
                    btn.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;

                    btn.BackColor = TemaSistema.Seleccion;
                    btn.ForeColor = Color.Black;
                }
            }
        }
        private void ToolStrip_PaintBorder(object sender, PaintEventArgs e)
        {
            var ts = (ToolStrip)sender;

            using var pen = new Pen(Color.Black, 4);

            e.Graphics.DrawLine(
                pen,
                0,
                ts.Height - 1,
                ts.Width,
                ts.Height - 1);
        }
        private void ConfigurarMenuStrip(MenuStrip ms)
        {
            // Le asignamos nuestro mini-dibujante personalizado
            ms.Renderer = new Presentacion.FBase.Helpers.MiniRenderizadorMenu();

            ms.BackColor = TemaSistema.Oscuro;
            ms.ForeColor = TemaSistema.Acento;

            foreach (ToolStripItem item in ms.Items)
            {
                ConfigurarItemMenu(item);
            }
        }

        private void ConfigurarItemMenu(ToolStripItem item)
        {
            item.ForeColor = TemaSistema.Acento;
            //item.Font = new Font(item.Font, FontStyle.Bold);

            if (item is ToolStripMenuItem menuItem)
            {
                // Esto mantiene el fondo del contenedor de la lista desplegable
                menuItem.DropDown.BackColor = TemaSistema.Oscuro;
                menuItem.DropDown.ForeColor = TemaSistema.Acento;

                // Recorremos los sub-ítems
                foreach (ToolStripItem subItem in menuItem.DropDownItems)
                {
                    ConfigurarItemMenu(subItem);
                }
            }
        }
         private void ConfigurarFormPlot(FormsPlot fp)
        {
            fp.BackColor = TemaSistema.Fondo;
            var spColor = ScottPlot.Color.FromHex("#EAEAEA");
            fp.Plot.FigureBackground.Color = spColor;
            fp.Plot.DataBackground.Color = spColor;
        }
        private void ConfugurarTableLayoutPanel(TableLayoutPanel tlp)
        {
            tlp.BackColor = TemaSistema.Fondo;
        }
    }
}
