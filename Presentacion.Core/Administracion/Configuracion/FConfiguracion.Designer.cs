
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Presentacion.Core.Administracion.Configuracion
{
    partial class FConfiguracion
    {
        private IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
                components.Dispose();

            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            ComponentResourceManager resources = new ComponentResourceManager(typeof(FConfiguracion));

            pnlMarco = new Panel();
            layoutPrincipal = new TableLayoutPanel();
            pnlNavegacion = new Panel();
            layoutNavegacion = new TableLayoutPanel();
            lblTituloConfiguracion = new Label();
            lblSubtituloConfiguracion = new Label();
            lblTituloSecciones = new Label();
            lblSubtituloSecciones = new Label();
            lstSecciones = new ListBox();
            layoutContenido = new TableLayoutPanel();
            lblSeccionTitulo = new Label();
            lblSeccionDescripcion = new Label();
            pnlContenido = new Panel();
            pnlAcciones = new Panel();
            lblCambios = new Label();
            btnGuardarCambios = new Button();
            layoutGeneral = new TableLayoutPanel();
            layoutEncabezado = new TableLayoutPanel();
            ((ISupportInitialize)error).BeginInit();
            pnlMarco.SuspendLayout();
            layoutPrincipal.SuspendLayout();
            pnlNavegacion.SuspendLayout();
            layoutNavegacion.SuspendLayout();
            layoutContenido.SuspendLayout();
            pnlAcciones.SuspendLayout();
            layoutGeneral.SuspendLayout();
            layoutEncabezado.SuspendLayout();
            SuspendLayout();
            // 
            // pnlMarco
            // 
            pnlMarco.BorderStyle = BorderStyle.FixedSingle;
            pnlMarco.Controls.Add(layoutGeneral);
            pnlMarco.Dock = DockStyle.Fill;
            pnlMarco.Location = new Point(0, 0);
            pnlMarco.Name = "pnlMarco";
            pnlMarco.Padding = new Padding(12);
            pnlMarco.Size = new Size(900, 600);
            pnlMarco.TabIndex = 0;
            // 
            // layoutPrincipal
            // 
            layoutPrincipal.ColumnCount = 2;
            layoutPrincipal.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 245F));
            layoutPrincipal.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            layoutPrincipal.Controls.Add(pnlNavegacion, 0, 0);
            layoutPrincipal.Controls.Add(layoutContenido, 1, 0);
            layoutPrincipal.Dock = DockStyle.Fill;
            layoutPrincipal.Location = new Point(0, 82);
            layoutPrincipal.Margin = new Padding(0);
            layoutPrincipal.Name = "layoutPrincipal";
            layoutPrincipal.RowCount = 1;
            layoutPrincipal.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            layoutPrincipal.Size = new Size(874, 492);
            layoutPrincipal.TabIndex = 0;
            // 
            // pnlNavegacion
            // 
            pnlNavegacion.BorderStyle = BorderStyle.FixedSingle;
            pnlNavegacion.Controls.Add(layoutNavegacion);
            pnlNavegacion.Dock = DockStyle.Fill;
            pnlNavegacion.Location = new Point(0, 0);
            pnlNavegacion.Margin = new Padding(0, 0, 16, 0);
            pnlNavegacion.Name = "pnlNavegacion";
            pnlNavegacion.Size = new Size(229, 492);
            pnlNavegacion.TabIndex = 0;
            // 
            // layoutNavegacion
            // 
            layoutNavegacion.ColumnCount = 1;
            layoutNavegacion.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            layoutNavegacion.Controls.Add(lblTituloSecciones, 0, 0);
            layoutNavegacion.Controls.Add(lblSubtituloSecciones, 0, 1);
            layoutNavegacion.Controls.Add(lstSecciones, 0, 2);
            layoutNavegacion.Dock = DockStyle.Fill;
            layoutNavegacion.Location = new Point(0, 0);
            layoutNavegacion.Margin = new Padding(0);
            layoutNavegacion.Name = "layoutNavegacion";
            layoutNavegacion.Padding = new Padding(10, 0, 10, 0);
            layoutNavegacion.RowCount = 3;
            layoutNavegacion.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            layoutNavegacion.RowStyles.Add(new RowStyle(SizeType.Absolute, 38F));
            layoutNavegacion.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            layoutNavegacion.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            layoutNavegacion.Size = new Size(227, 490);
            layoutNavegacion.TabIndex = 0;
            // 
            // lblTituloConfiguracion
            // 
            lblTituloConfiguracion.Dock = DockStyle.Fill;
            lblTituloConfiguracion.Font = new Font("Segoe UI Semibold", 15F, FontStyle.Bold);
            lblTituloConfiguracion.Location = new Point(3, 0);
            lblTituloConfiguracion.Name = "lblTituloConfiguracion";
            lblTituloConfiguracion.Size = new Size(868, 36);
            lblTituloConfiguracion.TabIndex = 0;
            lblTituloConfiguracion.Text = "Configuración";
            lblTituloConfiguracion.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblSubtituloConfiguracion
            // 
            lblSubtituloConfiguracion.Dock = DockStyle.Fill;
            lblSubtituloConfiguracion.Font = new Font("Segoe UI", 9F);
            lblSubtituloConfiguracion.Location = new Point(3, 36);
            lblSubtituloConfiguracion.Name = "lblSubtituloConfiguracion";
            lblSubtituloConfiguracion.Size = new Size(868, 46);
            lblSubtituloConfiguracion.TabIndex = 1;
            lblSubtituloConfiguracion.Text = "Administrá las preferencias y los parámetros del sistema.";
            // 
            // lblTituloSecciones
            // 
            lblTituloSecciones.Dock = DockStyle.Fill;
            lblTituloSecciones.Font = new Font("Segoe UI Semibold", 14F, FontStyle.Bold);
            lblTituloSecciones.Location = new Point(13, 0);
            lblTituloSecciones.Name = "lblTituloSecciones";
            lblTituloSecciones.Size = new Size(201, 30);
            lblTituloSecciones.TabIndex = 2;
            lblTituloSecciones.Tag = "Titulo";
            lblTituloSecciones.Text = "Opciones";
            lblTituloSecciones.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblSubtituloSecciones
            // 
            lblSubtituloSecciones.Dock = DockStyle.Fill;
            lblSubtituloSecciones.Font = new Font("Segoe UI", 8.5F);
            lblSubtituloSecciones.Location = new Point(13, 30);
            lblSubtituloSecciones.Name = "lblSubtituloSecciones";
            lblSubtituloSecciones.Padding = new Padding(3);
            lblSubtituloSecciones.Size = new Size(201, 38);
            lblSubtituloSecciones.TabIndex = 3;
            lblSubtituloSecciones.Text = "Seleccioná una sección para modificarla.";
            lblSubtituloSecciones.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lstSecciones
            // 
            lstSecciones.BorderStyle = BorderStyle.FixedSingle;
            lstSecciones.DisplayMember = "Titulo";
            lstSecciones.Dock = DockStyle.Fill;
            lstSecciones.Font = new Font("Segoe UI", 9.5F);
            lstSecciones.IntegralHeight = false;
            lstSecciones.ItemHeight = 17;
            lstSecciones.Location = new Point(10, 74);
            lstSecciones.Margin = new Padding(0, 6, 0, 0);
            lstSecciones.Name = "lstSecciones";
            lstSecciones.Size = new Size(207, 416);
            lstSecciones.TabIndex = 1;
            lstSecciones.SelectedIndexChanged += OnSeccionSeleccionada;
            // 
            // layoutContenido
            // 
            layoutContenido.ColumnCount = 1;
            layoutContenido.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            layoutContenido.Controls.Add(lblSeccionTitulo, 0, 0);
            layoutContenido.Controls.Add(lblSeccionDescripcion, 0, 1);
            layoutContenido.Controls.Add(pnlContenido, 0, 2);
            layoutContenido.Controls.Add(pnlAcciones, 0, 3);
            layoutContenido.Dock = DockStyle.Fill;
            layoutContenido.Location = new Point(245, 0);
            layoutContenido.Margin = new Padding(0);
            layoutContenido.Name = "layoutContenido";
            layoutContenido.RowCount = 4;
            layoutContenido.RowStyles.Add(new RowStyle(SizeType.Absolute, 38F));
            layoutContenido.RowStyles.Add(new RowStyle(SizeType.Absolute, 48F));
            layoutContenido.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            layoutContenido.RowStyles.Add(new RowStyle());
            layoutContenido.Size = new Size(629, 492);
            layoutContenido.TabIndex = 1;
            // 
            // lblSeccionTitulo
            // 
            lblSeccionTitulo.Dock = DockStyle.Fill;
            lblSeccionTitulo.Font = new Font("Segoe UI Semibold", 14F, FontStyle.Bold);
            lblSeccionTitulo.Location = new Point(3, 0);
            lblSeccionTitulo.Name = "lblSeccionTitulo";
            lblSeccionTitulo.Size = new Size(623, 38);
            lblSeccionTitulo.TabIndex = 0;
            lblSeccionTitulo.Tag = "Titulo";
            lblSeccionTitulo.Text = "Configuración";
            lblSeccionTitulo.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblSeccionDescripcion
            // 
            lblSeccionDescripcion.Dock = DockStyle.Fill;
            lblSeccionDescripcion.Font = new Font("Segoe UI", 9F);
            lblSeccionDescripcion.Location = new Point(0, 38);
            lblSeccionDescripcion.Margin = new Padding(0, 0, 0, 8);
            lblSeccionDescripcion.Name = "lblSeccionDescripcion";
            lblSeccionDescripcion.Size = new Size(629, 40);
            lblSeccionDescripcion.TabIndex = 1;
            lblSeccionDescripcion.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // pnlContenido
            // 
            pnlContenido.BorderStyle = BorderStyle.FixedSingle;
            pnlContenido.Dock = DockStyle.Fill;
            pnlContenido.Location = new Point(0, 86);
            pnlContenido.Margin = new Padding(0);
            pnlContenido.Name = "pnlContenido";
            pnlContenido.Padding = new Padding(8);
            pnlContenido.Size = new Size(629, 350);
            pnlContenido.TabIndex = 4;
            // 
            // pnlAcciones
            // 
            pnlAcciones.Controls.Add(lblCambios);
            pnlAcciones.Controls.Add(btnGuardarCambios);
            pnlAcciones.Dock = DockStyle.Fill;
            pnlAcciones.Location = new Point(0, 436);
            pnlAcciones.Margin = new Padding(0);
            pnlAcciones.Name = "pnlAcciones";
            pnlAcciones.Padding = new Padding(0, 10, 0, 0);
            pnlAcciones.Size = new Size(629, 56);
            pnlAcciones.TabIndex = 5;
            pnlAcciones.Visible = false;
            // 
            // lblCambios
            // 
            lblCambios.Dock = DockStyle.Fill;
            lblCambios.Font = new Font("Segoe UI", 9F);
            lblCambios.Location = new Point(0, 10);
            lblCambios.Name = "lblCambios";
            lblCambios.Size = new Size(459, 46);
            lblCambios.TabIndex = 0;
            lblCambios.Text = "Hay cambios sin guardar";
            lblCambios.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // btnGuardarCambios
            // 
            btnGuardarCambios.Dock = DockStyle.Right;
            btnGuardarCambios.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            btnGuardarCambios.Location = new Point(459, 10);
            btnGuardarCambios.Name = "btnGuardarCambios";
            btnGuardarCambios.Size = new Size(170, 46);
            btnGuardarCambios.TabIndex = 6;
            btnGuardarCambios.Text = "Guardar cambios";
            btnGuardarCambios.UseVisualStyleBackColor = true;
            btnGuardarCambios.Click += OnGuardarCambiosClick;
            // 
            // layoutGeneral
            // 
            layoutGeneral.ColumnCount = 1;
            layoutGeneral.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            layoutGeneral.Controls.Add(layoutEncabezado, 0, 0);
            layoutGeneral.Controls.Add(layoutPrincipal, 0, 1);
            layoutGeneral.Dock = DockStyle.Fill;
            layoutGeneral.Location = new Point(12, 12);
            layoutGeneral.Margin = new Padding(0);
            layoutGeneral.Name = "layoutGeneral";
            layoutGeneral.RowCount = 2;
            layoutGeneral.RowStyles.Add(new RowStyle(SizeType.Absolute, 82F));
            layoutGeneral.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            layoutGeneral.Size = new Size(874, 574);
            layoutGeneral.TabIndex = 0;
            // 
            // layoutEncabezado
            // 
            layoutEncabezado.ColumnCount = 1;
            layoutEncabezado.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            layoutEncabezado.Controls.Add(lblTituloConfiguracion, 0, 0);
            layoutEncabezado.Controls.Add(lblSubtituloConfiguracion, 0, 1);
            layoutEncabezado.Dock = DockStyle.Fill;
            layoutEncabezado.Location = new Point(0, 0);
            layoutEncabezado.Margin = new Padding(0);
            layoutEncabezado.Name = "layoutEncabezado";
            layoutEncabezado.RowCount = 2;
            layoutEncabezado.RowStyles.Add(new RowStyle(SizeType.Absolute, 36F));
            layoutEncabezado.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            layoutEncabezado.Size = new Size(874, 82);
            layoutEncabezado.TabIndex = 0;
            // 
            // FConfiguracion
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(900, 600);
            Controls.Add(pnlMarco);
            ForeColor = Color.FromArgb(31, 26, 43);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "FConfiguracion";
            StartPosition = FormStartPosition.Manual;
            Text = "Configuración del sistema";
            ((ISupportInitialize)error).EndInit();
            pnlMarco.ResumeLayout(false);
            layoutPrincipal.ResumeLayout(false);
            pnlNavegacion.ResumeLayout(false);
            layoutNavegacion.ResumeLayout(false);
            layoutContenido.ResumeLayout(false);
            pnlAcciones.ResumeLayout(false);
            layoutGeneral.ResumeLayout(false);
            layoutEncabezado.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlMarco;
        private TableLayoutPanel layoutPrincipal;

        private Panel pnlNavegacion;
        private TableLayoutPanel layoutNavegacion;
        private Label lblTituloConfiguracion;
        private Label lblSubtituloConfiguracion;
        private Label lblTituloSecciones;
        private Label lblSubtituloSecciones;
        private TableLayoutPanel layoutGeneral;
        private TableLayoutPanel layoutEncabezado;
        private TableLayoutPanel layoutContenido;
        private ListBox lstSecciones;
        private Label lblSeccionTitulo;
        private Label lblSeccionDescripcion;
        private Panel pnlContenido;
        private Panel pnlAcciones;
        private Label lblCambios;
        private Button btnGuardarCambios;
        private Button _btnDescartarCambios;
        private FlowLayoutPanel _panelBotonesAcciones;
    }
}
