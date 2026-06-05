using Presentacion.FBase.Helpers;
using Presentacion.FormulariosBase.DTO;
using System.Text.Json.Nodes;

namespace Presentacion.FBase
{
    public partial class FBase : Form
    {
        //   private string ColorFondo = "#d4a925";Pensado para config
        private readonly List<ControlDTO> _listaControlesObligatorios;
        public FBase()
        {
            InitializeComponent();
            this.KeyPreview = true;

            _listaControlesObligatorios = new List<ControlDTO>();
            this.components = new System.ComponentModel.Container();
            this.error = new System.Windows.Forms.ErrorProvider(this.components);
        }
       //     this.BackColor = System.Drawing.ColorTranslator.FromHtml(ColorFondo); Pensado para config



               protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
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
        public virtual void AgregarControlesObligatorios(object control, string nombreControl)
        {
            _listaControlesObligatorios.Add(new ControlDTO
            {
                Control = control,
                NombreControl = nombreControl
            });

            AsignarErrorProvider(control);
        }

        public virtual void LimpiarControlesObligatorios()
        {
            _listaControlesObligatorios.Clear();
            error.Clear();
        }
        public virtual bool VerificarDatosObligatorios()
        {
            foreach (var objeto in _listaControlesObligatorios)
            {
                switch (objeto.Control)
                {
                    case TextBox _:
                        if (string.IsNullOrEmpty(((TextBox)objeto.Control).Text)) return false;
                        break;
                    case RichTextBox _:
                        if (string.IsNullOrEmpty(((RichTextBox)objeto.Control).Text)) return false;
                        break;
                    case NumericUpDown _:
                        if (string.IsNullOrEmpty(((NumericUpDown)objeto.Control).Text)) return false;
                        break;
                    case ComboBox _:
                        if (((ComboBox)objeto.Control).Items.Count <= 0) return false;
                        break;
                }
            }

            return true;
        }
        public virtual void AsignarErrorProvider(object control)
        {
            if (control is TextBox)
            {
                ((TextBox)control).Validated += Control_Validated;
            }

            if (control is RichTextBox)
            {
                ((RichTextBox)control).Validated += Control_Validated;
            }

            if (control is ComboBox)
            {
                ((ComboBox)control).Validated += Control_Validated;
            }
        }

       public virtual void Control_Validated(object sender, System.EventArgs e)
        {
            if (sender is TextBox)
            {
                error.SetError(((TextBox)sender),
                    !string.IsNullOrEmpty(((TextBox)sender).Text)
                        ? string.Empty
                        : $"El campo es Obligatorio.");
                return;
            }

            if (sender is RichTextBox)
            {
                error.SetError(((RichTextBox)sender),
                    !string.IsNullOrEmpty(((RichTextBox)sender).Text)
                        ? string.Empty
                        : $"El campo es Obligatorio.");

                return;
            }

            if (sender is NumericUpDown)
            {
                error.SetError(((NumericUpDown)sender),
                    !string.IsNullOrEmpty(((NumericUpDown)sender).Text)
                        ? string.Empty
                        : $"El campo es Obligatorio.");

                return;
            }

            if (sender is ComboBox)
            {
                error.SetError(((ComboBox)sender),
                    !string.IsNullOrEmpty(((ComboBox)sender).Text)
                        ? string.Empty
                        : $"El campo es Obligatorio.");
            }
        }

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
                        pnl.BackColor = TemaSistema.Fondo;
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
            btn.BackColor = TemaSistema.Primario;
            btn.ForeColor = Color.White;

            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.Cursor = Cursors.Hand;
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
        }

    }
}
