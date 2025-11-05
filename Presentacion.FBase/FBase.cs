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
                _listaControlesObligatorios = new List<ControlDTO>();
            this.components = new System.ComponentModel.Container();
            this.error = new System.Windows.Forms.ErrorProvider(this.components);
       //     this.BackColor = System.Drawing.ColorTranslator.FromHtml(ColorFondo); Pensado para config
           


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

    }
}
