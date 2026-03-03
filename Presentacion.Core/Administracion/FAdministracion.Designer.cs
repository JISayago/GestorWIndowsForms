namespace Presentacion.Core.Administracion
{
    partial class FAdministracion
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            menuStrip1 = new MenuStrip();
            pRODUCTOToolStripMenuItem = new ToolStripMenuItem();
            sTOCKToolStripMenuItem = new ToolStripMenuItem();
            mARCASToolStripMenuItem = new ToolStripMenuItem();
            cATEGORIASToolStripMenuItem = new ToolStripMenuItem();
            rUBROSToolStripMenuItem = new ToolStripMenuItem();
            eMPLEADOSToolStripMenuItem = new ToolStripMenuItem();
            lISTADOEMPLEADOSToolStripMenuItem = new ToolStripMenuItem();
            rOLESToolStripMenuItem = new ToolStripMenuItem();
            cLIENTESToolStripMenuItem = new ToolStripMenuItem();
            lISTADOCLIENTESToolStripMenuItem = new ToolStripMenuItem();
            cUENTASCORRIENTESToolStripMenuItem = new ToolStripMenuItem();
            oFERTASToolStripMenuItem = new ToolStripMenuItem();
            lISTADOOFERTASToolStripMenuItem = new ToolStripMenuItem();
            aCTIVARDESACTIVARToolStripMenuItem = new ToolStripMenuItem();
            nUEVAOFERTAToolStripMenuItem = new ToolStripMenuItem();
            cONFIGURACIONToolStripMenuItem = new ToolStripMenuItem();
            tIPOPAGOToolStripMenuItem = new ToolStripMenuItem();
            tableLayoutPanel1 = new TableLayoutPanel();
            btnGasto = new Button();
            btnVolver = new Button();
            btnComprobantes = new Button();
            btnMovimientos = new Button();
            tlpBaseFiltrado = new TableLayoutPanel();
            tlpFiltradoMesYAño = new TableLayoutPanel();
            lblMesGraficos = new Label();
            lblAñoGraficos = new Label();
            cbMesGrafico = new ComboBox();
            cbAñoGraficos = new ComboBox();
            tlpFiltradoBotones = new TableLayoutPanel();
            btnFiltrarGraficos = new Button();
            button2 = new Button();
            btnFechaActualGraficos = new Button();
            pnlInfoInicial = new Panel();
            tlpBaseNivel1 = new TableLayoutPanel();
            tlpArribaNivel2 = new TableLayoutPanel();
            tabControlGraficoArriba = new TabControl();
            tabPage1 = new TabPage();
            formsPlot1 = new ScottPlot.WinForms.FormsPlot();
            tabPage2 = new TabPage();
            formsPlot2 = new ScottPlot.WinForms.FormsPlot();
            tlpBajoNivel2 = new TableLayoutPanel();
            tabControl1 = new TabControl();
            tabPage3 = new TabPage();
            tlpBajoNivel3 = new TableLayoutPanel();
            formsPlot4 = new ScottPlot.WinForms.FormsPlot();
            formsPlot3 = new ScottPlot.WinForms.FormsPlot();
            tabPage4 = new TabPage();
            tlpBajoNivel3Pagina2 = new TableLayoutPanel();
            formsPlot6 = new ScottPlot.WinForms.FormsPlot();
            formsPlot5 = new ScottPlot.WinForms.FormsPlot();
            dgvGrilla = new DataGridView();
            sqlCommand1 = new Microsoft.Data.SqlClient.SqlCommand();
            menuStrip1.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            tlpBaseFiltrado.SuspendLayout();
            tlpFiltradoMesYAño.SuspendLayout();
            tlpFiltradoBotones.SuspendLayout();
            pnlInfoInicial.SuspendLayout();
            tlpBaseNivel1.SuspendLayout();
            tlpArribaNivel2.SuspendLayout();
            tabControlGraficoArriba.SuspendLayout();
            tabPage1.SuspendLayout();
            tabPage2.SuspendLayout();
            tlpBajoNivel2.SuspendLayout();
            tabControl1.SuspendLayout();
            tabPage3.SuspendLayout();
            tlpBajoNivel3.SuspendLayout();
            tabPage4.SuspendLayout();
            tlpBajoNivel3Pagina2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvGrilla).BeginInit();
            SuspendLayout();
            // 
            // menuStrip1
            // 
            menuStrip1.Items.AddRange(new ToolStripItem[] { pRODUCTOToolStripMenuItem, eMPLEADOSToolStripMenuItem, cLIENTESToolStripMenuItem, oFERTASToolStripMenuItem, cONFIGURACIONToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(1610, 24);
            menuStrip1.TabIndex = 2;
            menuStrip1.Text = "menuStrip1";
            // 
            // pRODUCTOToolStripMenuItem
            // 
            pRODUCTOToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { sTOCKToolStripMenuItem, mARCASToolStripMenuItem, cATEGORIASToolStripMenuItem, rUBROSToolStripMenuItem });
            pRODUCTOToolStripMenuItem.Name = "pRODUCTOToolStripMenuItem";
            pRODUCTOToolStripMenuItem.Size = new Size(81, 20);
            pRODUCTOToolStripMenuItem.Text = "PRODUCTO";
            // 
            // sTOCKToolStripMenuItem
            // 
            sTOCKToolStripMenuItem.Name = "sTOCKToolStripMenuItem";
            sTOCKToolStripMenuItem.Size = new Size(143, 22);
            sTOCKToolStripMenuItem.Text = "STOCK";
            sTOCKToolStripMenuItem.Click += sTOCKToolStripMenuItem_Click;
            // 
            // mARCASToolStripMenuItem
            // 
            mARCASToolStripMenuItem.Name = "mARCASToolStripMenuItem";
            mARCASToolStripMenuItem.Size = new Size(143, 22);
            mARCASToolStripMenuItem.Text = "MARCAS";
            mARCASToolStripMenuItem.Click += mARCASToolStripMenuItem_Click;
            // 
            // cATEGORIASToolStripMenuItem
            // 
            cATEGORIASToolStripMenuItem.Name = "cATEGORIASToolStripMenuItem";
            cATEGORIASToolStripMenuItem.Size = new Size(143, 22);
            cATEGORIASToolStripMenuItem.Text = "CATEGORIAS";
            cATEGORIASToolStripMenuItem.Click += cATEGORIASToolStripMenuItem_Click;
            // 
            // rUBROSToolStripMenuItem
            // 
            rUBROSToolStripMenuItem.Name = "rUBROSToolStripMenuItem";
            rUBROSToolStripMenuItem.Size = new Size(143, 22);
            rUBROSToolStripMenuItem.Text = "RUBROS";
            rUBROSToolStripMenuItem.Click += rUBROSToolStripMenuItem_Click;
            // 
            // eMPLEADOSToolStripMenuItem
            // 
            eMPLEADOSToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { lISTADOEMPLEADOSToolStripMenuItem, rOLESToolStripMenuItem });
            eMPLEADOSToolStripMenuItem.Name = "eMPLEADOSToolStripMenuItem";
            eMPLEADOSToolStripMenuItem.Size = new Size(86, 20);
            eMPLEADOSToolStripMenuItem.Text = "EMPLEADOS";
            // 
            // lISTADOEMPLEADOSToolStripMenuItem
            // 
            lISTADOEMPLEADOSToolStripMenuItem.Name = "lISTADOEMPLEADOSToolStripMenuItem";
            lISTADOEMPLEADOSToolStripMenuItem.Size = new Size(190, 22);
            lISTADOEMPLEADOSToolStripMenuItem.Text = "LISTADO EMPLEADOS";
            lISTADOEMPLEADOSToolStripMenuItem.Click += lISTADOEMPLEADOSToolStripMenuItem_Click;
            // 
            // rOLESToolStripMenuItem
            // 
            rOLESToolStripMenuItem.Name = "rOLESToolStripMenuItem";
            rOLESToolStripMenuItem.Size = new Size(190, 22);
            rOLESToolStripMenuItem.Text = "ROLES";
            rOLESToolStripMenuItem.Click += rOLESToolStripMenuItem_Click;
            // 
            // cLIENTESToolStripMenuItem
            // 
            cLIENTESToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { lISTADOCLIENTESToolStripMenuItem, cUENTASCORRIENTESToolStripMenuItem });
            cLIENTESToolStripMenuItem.Name = "cLIENTESToolStripMenuItem";
            cLIENTESToolStripMenuItem.Size = new Size(70, 20);
            cLIENTESToolStripMenuItem.Text = "CLIENTES";
            // 
            // lISTADOCLIENTESToolStripMenuItem
            // 
            lISTADOCLIENTESToolStripMenuItem.Name = "lISTADOCLIENTESToolStripMenuItem";
            lISTADOCLIENTESToolStripMenuItem.Size = new Size(196, 22);
            lISTADOCLIENTESToolStripMenuItem.Text = "LISTADO CLIENTES";
            lISTADOCLIENTESToolStripMenuItem.Click += lISTADOCLIENTESToolStripMenuItem_Click;
            // 
            // cUENTASCORRIENTESToolStripMenuItem
            // 
            cUENTASCORRIENTESToolStripMenuItem.Name = "cUENTASCORRIENTESToolStripMenuItem";
            cUENTASCORRIENTESToolStripMenuItem.Size = new Size(196, 22);
            cUENTASCORRIENTESToolStripMenuItem.Text = "CUENTAS CORRIENTES";
            cUENTASCORRIENTESToolStripMenuItem.Click += cUENTASCORRIENTESToolStripMenuItem_Click;
            // 
            // oFERTASToolStripMenuItem
            // 
            oFERTASToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { lISTADOOFERTASToolStripMenuItem, aCTIVARDESACTIVARToolStripMenuItem, nUEVAOFERTAToolStripMenuItem });
            oFERTASToolStripMenuItem.Name = "oFERTASToolStripMenuItem";
            oFERTASToolStripMenuItem.Size = new Size(66, 20);
            oFERTASToolStripMenuItem.Text = "OFERTAS";
            // 
            // lISTADOOFERTASToolStripMenuItem
            // 
            lISTADOOFERTASToolStripMenuItem.Name = "lISTADOOFERTASToolStripMenuItem";
            lISTADOOFERTASToolStripMenuItem.Size = new Size(199, 22);
            lISTADOOFERTASToolStripMenuItem.Text = "LISTADO OFERTAS";
            lISTADOOFERTASToolStripMenuItem.Click += lISTADOOFERTASToolStripMenuItem_Click;
            // 
            // aCTIVARDESACTIVARToolStripMenuItem
            // 
            aCTIVARDESACTIVARToolStripMenuItem.Name = "aCTIVARDESACTIVARToolStripMenuItem";
            aCTIVARDESACTIVARToolStripMenuItem.Size = new Size(199, 22);
            aCTIVARDESACTIVARToolStripMenuItem.Text = "ACTIVAR / DESACTIVAR";
            aCTIVARDESACTIVARToolStripMenuItem.Click += aCTIVARDESACTIVARToolStripMenuItem_Click;
            // 
            // nUEVAOFERTAToolStripMenuItem
            // 
            nUEVAOFERTAToolStripMenuItem.Name = "nUEVAOFERTAToolStripMenuItem";
            nUEVAOFERTAToolStripMenuItem.Size = new Size(199, 22);
            nUEVAOFERTAToolStripMenuItem.Text = "NUEVA OFERTA";
            nUEVAOFERTAToolStripMenuItem.Click += nUEVAOFERTAToolStripMenuItem_Click;
            // 
            // cONFIGURACIONToolStripMenuItem
            // 
            cONFIGURACIONToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { tIPOPAGOToolStripMenuItem });
            cONFIGURACIONToolStripMenuItem.Name = "cONFIGURACIONToolStripMenuItem";
            cONFIGURACIONToolStripMenuItem.Size = new Size(114, 20);
            cONFIGURACIONToolStripMenuItem.Text = "CONFIGURACION";
            // 
            // tIPOPAGOToolStripMenuItem
            // 
            tIPOPAGOToolStripMenuItem.Name = "tIPOPAGOToolStripMenuItem";
            tIPOPAGOToolStripMenuItem.Size = new Size(144, 22);
            tIPOPAGOToolStripMenuItem.Text = "TIPO PAGO??";
            tIPOPAGOToolStripMenuItem.Click += tIPOPAGOToolStripMenuItem_Click;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 5;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 18.281538F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 18.2815361F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 26.8738575F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 18.2815361F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 18.2815361F));
            tableLayoutPanel1.Controls.Add(btnGasto, 1, 0);
            tableLayoutPanel1.Controls.Add(btnVolver, 0, 0);
            tableLayoutPanel1.Controls.Add(btnComprobantes, 4, 0);
            tableLayoutPanel1.Controls.Add(btnMovimientos, 3, 0);
            tableLayoutPanel1.Controls.Add(tlpBaseFiltrado, 2, 0);
            tableLayoutPanel1.Dock = DockStyle.Top;
            tableLayoutPanel1.Location = new Point(0, 24);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 1;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Size = new Size(1610, 94);
            tableLayoutPanel1.TabIndex = 7;
            // 
            // btnGasto
            // 
            btnGasto.Anchor = AnchorStyles.Left;
            btnGasto.Location = new Point(297, 15);
            btnGasto.MaximumSize = new Size(236, 63);
            btnGasto.MinimumSize = new Size(236, 63);
            btnGasto.Name = "btnGasto";
            btnGasto.Size = new Size(236, 63);
            btnGasto.TabIndex = 12;
            btnGasto.Text = "GASTO";
            btnGasto.UseVisualStyleBackColor = true;
            btnGasto.Click += btnGasto_Click;
            // 
            // btnVolver
            // 
            btnVolver.Anchor = AnchorStyles.Right;
            btnVolver.Location = new Point(55, 15);
            btnVolver.MaximumSize = new Size(236, 63);
            btnVolver.MinimumSize = new Size(236, 63);
            btnVolver.Name = "btnVolver";
            btnVolver.Size = new Size(236, 63);
            btnVolver.TabIndex = 13;
            btnVolver.Text = "VOLVER AL INICIO";
            btnVolver.UseVisualStyleBackColor = true;
            btnVolver.Click += btnVolver_Click;
            // 
            // btnComprobantes
            // 
            btnComprobantes.Anchor = AnchorStyles.Left;
            btnComprobantes.Location = new Point(1317, 15);
            btnComprobantes.MaximumSize = new Size(236, 63);
            btnComprobantes.MinimumSize = new Size(236, 63);
            btnComprobantes.Name = "btnComprobantes";
            btnComprobantes.Size = new Size(236, 63);
            btnComprobantes.TabIndex = 15;
            btnComprobantes.Text = "COMPROBANTES";
            btnComprobantes.UseVisualStyleBackColor = true;
            btnComprobantes.Click += btnComprobantes_Click;
            // 
            // btnMovimientos
            // 
            btnMovimientos.Anchor = AnchorStyles.Right;
            btnMovimientos.Location = new Point(1075, 15);
            btnMovimientos.MaximumSize = new Size(236, 63);
            btnMovimientos.MinimumSize = new Size(236, 63);
            btnMovimientos.Name = "btnMovimientos";
            btnMovimientos.Size = new Size(236, 63);
            btnMovimientos.TabIndex = 16;
            btnMovimientos.Text = "MOVIMIENTOS";
            btnMovimientos.UseVisualStyleBackColor = true;
            btnMovimientos.Click += btnMovimientos_Click;
            // 
            // tlpBaseFiltrado
            // 
            tlpBaseFiltrado.ColumnCount = 1;
            tlpBaseFiltrado.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpBaseFiltrado.Controls.Add(tlpFiltradoMesYAño, 0, 0);
            tlpBaseFiltrado.Controls.Add(tlpFiltradoBotones, 0, 1);
            tlpBaseFiltrado.Dock = DockStyle.Fill;
            tlpBaseFiltrado.Location = new Point(591, 3);
            tlpBaseFiltrado.Name = "tlpBaseFiltrado";
            tlpBaseFiltrado.RowCount = 2;
            tlpBaseFiltrado.RowStyles.Add(new RowStyle(SizeType.Percent, 61.363636F));
            tlpBaseFiltrado.RowStyles.Add(new RowStyle(SizeType.Percent, 38.636364F));
            tlpBaseFiltrado.Size = new Size(426, 88);
            tlpBaseFiltrado.TabIndex = 17;
            // 
            // tlpFiltradoMesYAño
            // 
            tlpFiltradoMesYAño.ColumnCount = 2;
            tlpFiltradoMesYAño.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tlpFiltradoMesYAño.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tlpFiltradoMesYAño.Controls.Add(lblMesGraficos, 0, 0);
            tlpFiltradoMesYAño.Controls.Add(lblAñoGraficos, 1, 0);
            tlpFiltradoMesYAño.Controls.Add(cbMesGrafico, 0, 1);
            tlpFiltradoMesYAño.Controls.Add(cbAñoGraficos, 1, 1);
            tlpFiltradoMesYAño.Dock = DockStyle.Fill;
            tlpFiltradoMesYAño.Location = new Point(3, 3);
            tlpFiltradoMesYAño.Name = "tlpFiltradoMesYAño";
            tlpFiltradoMesYAño.RowCount = 2;
            tlpFiltradoMesYAño.RowStyles.Add(new RowStyle(SizeType.Percent, 40F));
            tlpFiltradoMesYAño.RowStyles.Add(new RowStyle(SizeType.Percent, 60F));
            tlpFiltradoMesYAño.Size = new Size(420, 48);
            tlpFiltradoMesYAño.TabIndex = 2;
            // 
            // lblMesGraficos
            // 
            lblMesGraficos.AutoSize = true;
            lblMesGraficos.Dock = DockStyle.Bottom;
            lblMesGraficos.Location = new Point(3, 4);
            lblMesGraficos.Name = "lblMesGraficos";
            lblMesGraficos.Size = new Size(204, 15);
            lblMesGraficos.TabIndex = 0;
            lblMesGraficos.Text = "Mes";
            lblMesGraficos.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblAñoGraficos
            // 
            lblAñoGraficos.AutoSize = true;
            lblAñoGraficos.Dock = DockStyle.Bottom;
            lblAñoGraficos.Location = new Point(213, 4);
            lblAñoGraficos.Name = "lblAñoGraficos";
            lblAñoGraficos.Size = new Size(204, 15);
            lblAñoGraficos.TabIndex = 1;
            lblAñoGraficos.Text = "Año";
            lblAñoGraficos.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // cbMesGrafico
            // 
            cbMesGrafico.Dock = DockStyle.Bottom;
            cbMesGrafico.FormattingEnabled = true;
            cbMesGrafico.Location = new Point(30, 22);
            cbMesGrafico.Margin = new Padding(30, 3, 30, 3);
            cbMesGrafico.Name = "cbMesGrafico";
            cbMesGrafico.Size = new Size(150, 23);
            cbMesGrafico.TabIndex = 4;
            // 
            // cbAñoGraficos
            // 
            cbAñoGraficos.Dock = DockStyle.Bottom;
            cbAñoGraficos.FormattingEnabled = true;
            cbAñoGraficos.Location = new Point(240, 22);
            cbAñoGraficos.Margin = new Padding(30, 3, 30, 3);
            cbAñoGraficos.Name = "cbAñoGraficos";
            cbAñoGraficos.Size = new Size(150, 23);
            cbAñoGraficos.TabIndex = 5;
            // 
            // tlpFiltradoBotones
            // 
            tlpFiltradoBotones.ColumnCount = 3;
            tlpFiltradoBotones.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
            tlpFiltradoBotones.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
            tlpFiltradoBotones.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
            tlpFiltradoBotones.Controls.Add(btnFiltrarGraficos, 1, 0);
            tlpFiltradoBotones.Controls.Add(button2, 0, 0);
            tlpFiltradoBotones.Controls.Add(btnFechaActualGraficos, 2, 0);
            tlpFiltradoBotones.Dock = DockStyle.Fill;
            tlpFiltradoBotones.Location = new Point(3, 57);
            tlpFiltradoBotones.Name = "tlpFiltradoBotones";
            tlpFiltradoBotones.RowCount = 1;
            tlpFiltradoBotones.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpFiltradoBotones.Size = new Size(420, 28);
            tlpFiltradoBotones.TabIndex = 3;
            // 
            // btnFiltrarGraficos
            // 
            btnFiltrarGraficos.Dock = DockStyle.Fill;
            btnFiltrarGraficos.Location = new Point(169, 3);
            btnFiltrarGraficos.Margin = new Padding(30, 3, 30, 3);
            btnFiltrarGraficos.Name = "btnFiltrarGraficos";
            btnFiltrarGraficos.Size = new Size(79, 22);
            btnFiltrarGraficos.TabIndex = 0;
            btnFiltrarGraficos.Text = "Filtrar";
            btnFiltrarGraficos.UseVisualStyleBackColor = true;
            btnFiltrarGraficos.Click += btnFiltrarGraficos_Click;
            // 
            // button2
            // 
            button2.Dock = DockStyle.Fill;
            button2.Location = new Point(15, 3);
            button2.Margin = new Padding(15, 3, 15, 3);
            button2.Name = "button2";
            button2.Size = new Size(109, 22);
            button2.TabIndex = 1;
            button2.Text = "Mes Anterior?";
            button2.UseVisualStyleBackColor = true;
            // 
            // btnFechaActualGraficos
            // 
            btnFechaActualGraficos.Dock = DockStyle.Fill;
            btnFechaActualGraficos.Location = new Point(293, 3);
            btnFechaActualGraficos.Margin = new Padding(15, 3, 15, 3);
            btnFechaActualGraficos.Name = "btnFechaActualGraficos";
            btnFechaActualGraficos.Size = new Size(112, 22);
            btnFechaActualGraficos.TabIndex = 2;
            btnFechaActualGraficos.Text = "Hoy";
            btnFechaActualGraficos.UseVisualStyleBackColor = true;
            btnFechaActualGraficos.Click += btnFechaActualGraficos_Click;
            // 
            // pnlInfoInicial
            // 
            pnlInfoInicial.BackColor = SystemColors.ActiveCaption;
            pnlInfoInicial.Controls.Add(tlpBaseNivel1);
            pnlInfoInicial.Controls.Add(dgvGrilla);
            pnlInfoInicial.Dock = DockStyle.Fill;
            pnlInfoInicial.Location = new Point(0, 118);
            pnlInfoInicial.Name = "pnlInfoInicial";
            pnlInfoInicial.Size = new Size(1610, 872);
            pnlInfoInicial.TabIndex = 24;
            // 
            // tlpBaseNivel1
            // 
            tlpBaseNivel1.BackColor = SystemColors.MenuHighlight;
            tlpBaseNivel1.ColumnCount = 1;
            tlpBaseNivel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpBaseNivel1.Controls.Add(tlpArribaNivel2, 0, 0);
            tlpBaseNivel1.Controls.Add(tlpBajoNivel2, 0, 1);
            tlpBaseNivel1.Dock = DockStyle.Top;
            tlpBaseNivel1.Location = new Point(0, 0);
            tlpBaseNivel1.Name = "tlpBaseNivel1";
            tlpBaseNivel1.RowCount = 2;
            tlpBaseNivel1.RowStyles.Add(new RowStyle(SizeType.Percent, 42.66055F));
            tlpBaseNivel1.RowStyles.Add(new RowStyle(SizeType.Percent, 57.33945F));
            tlpBaseNivel1.Size = new Size(1610, 872);
            tlpBaseNivel1.TabIndex = 24;
            // 
            // tlpArribaNivel2
            // 
            tlpArribaNivel2.BackColor = Color.FromArgb(192, 255, 192);
            tlpArribaNivel2.ColumnCount = 1;
            tlpArribaNivel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpArribaNivel2.Controls.Add(tabControlGraficoArriba, 0, 0);
            tlpArribaNivel2.Dock = DockStyle.Fill;
            tlpArribaNivel2.ForeColor = SystemColors.ButtonShadow;
            tlpArribaNivel2.Location = new Point(3, 3);
            tlpArribaNivel2.Name = "tlpArribaNivel2";
            tlpArribaNivel2.RowCount = 1;
            tlpArribaNivel2.RowStyles.Add(new RowStyle(SizeType.Percent, 100.000008F));
            tlpArribaNivel2.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tlpArribaNivel2.Size = new Size(1604, 366);
            tlpArribaNivel2.TabIndex = 0;
            // 
            // tabControlGraficoArriba
            // 
            tabControlGraficoArriba.Controls.Add(tabPage1);
            tabControlGraficoArriba.Controls.Add(tabPage2);
            tabControlGraficoArriba.Dock = DockStyle.Fill;
            tabControlGraficoArriba.Location = new Point(3, 3);
            tabControlGraficoArriba.Name = "tabControlGraficoArriba";
            tabControlGraficoArriba.SelectedIndex = 0;
            tabControlGraficoArriba.Size = new Size(1598, 360);
            tabControlGraficoArriba.TabIndex = 0;
            // 
            // tabPage1
            // 
            tabPage1.Controls.Add(formsPlot1);
            tabPage1.Location = new Point(4, 24);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3);
            tabPage1.Size = new Size(1590, 332);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "Cajas del Mes";
            tabPage1.UseVisualStyleBackColor = true;
            // 
            // formsPlot1
            // 
            formsPlot1.DisplayScale = 1F;
            formsPlot1.Dock = DockStyle.Fill;
            formsPlot1.Location = new Point(3, 3);
            formsPlot1.Name = "formsPlot1";
            formsPlot1.Size = new Size(1584, 326);
            formsPlot1.TabIndex = 0;
            // 
            // tabPage2
            // 
            tabPage2.Controls.Add(formsPlot2);
            tabPage2.Location = new Point(4, 24);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(3);
            tabPage2.Size = new Size(1590, 332);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "Ultimas 50 Cajas";
            tabPage2.UseVisualStyleBackColor = true;
            // 
            // formsPlot2
            // 
            formsPlot2.DisplayScale = 1F;
            formsPlot2.Dock = DockStyle.Fill;
            formsPlot2.Location = new Point(3, 3);
            formsPlot2.Name = "formsPlot2";
            formsPlot2.Size = new Size(1584, 326);
            formsPlot2.TabIndex = 0;
            // 
            // tlpBajoNivel2
            // 
            tlpBajoNivel2.BackColor = Color.RosyBrown;
            tlpBajoNivel2.ColumnCount = 1;
            tlpBajoNivel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 99.99999F));
            tlpBajoNivel2.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            tlpBajoNivel2.Controls.Add(tabControl1, 0, 0);
            tlpBajoNivel2.Dock = DockStyle.Fill;
            tlpBajoNivel2.Location = new Point(3, 375);
            tlpBajoNivel2.Name = "tlpBajoNivel2";
            tlpBajoNivel2.RowCount = 1;
            tlpBajoNivel2.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpBajoNivel2.Size = new Size(1604, 494);
            tlpBajoNivel2.TabIndex = 1;
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(tabPage3);
            tabControl1.Controls.Add(tabPage4);
            tabControl1.Dock = DockStyle.Fill;
            tabControl1.Location = new Point(3, 3);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(1598, 488);
            tabControl1.TabIndex = 0;
            // 
            // tabPage3
            // 
            tabPage3.Controls.Add(tlpBajoNivel3);
            tabPage3.Font = new Font("Microsoft Sans Serif", 15.75F);
            tabPage3.Location = new Point(4, 24);
            tabPage3.Name = "tabPage3";
            tabPage3.Padding = new Padding(3);
            tabPage3.Size = new Size(1590, 460);
            tabPage3.TabIndex = 0;
            tabPage3.Text = "Por Dia";
            tabPage3.UseVisualStyleBackColor = true;
            // 
            // tlpBajoNivel3
            // 
            tlpBajoNivel3.BackColor = Color.IndianRed;
            tlpBajoNivel3.ColumnCount = 2;
            tlpBajoNivel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tlpBajoNivel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tlpBajoNivel3.Controls.Add(formsPlot4, 1, 0);
            tlpBajoNivel3.Controls.Add(formsPlot3, 0, 0);
            tlpBajoNivel3.Dock = DockStyle.Fill;
            tlpBajoNivel3.Location = new Point(3, 3);
            tlpBajoNivel3.Name = "tlpBajoNivel3";
            tlpBajoNivel3.RowCount = 1;
            tlpBajoNivel3.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpBajoNivel3.Size = new Size(1584, 454);
            tlpBajoNivel3.TabIndex = 0;
            // 
            // formsPlot4
            // 
            formsPlot4.DisplayScale = 1F;
            formsPlot4.Dock = DockStyle.Fill;
            formsPlot4.Location = new Point(795, 3);
            formsPlot4.Name = "formsPlot4";
            formsPlot4.Size = new Size(786, 448);
            formsPlot4.TabIndex = 2;
            // 
            // formsPlot3
            // 
            formsPlot3.DisplayScale = 1F;
            formsPlot3.Dock = DockStyle.Fill;
            formsPlot3.Location = new Point(3, 3);
            formsPlot3.Name = "formsPlot3";
            formsPlot3.Size = new Size(786, 448);
            formsPlot3.TabIndex = 1;
            // 
            // tabPage4
            // 
            tabPage4.Controls.Add(tlpBajoNivel3Pagina2);
            tabPage4.Font = new Font("Microsoft Sans Serif", 15.75F);
            tabPage4.Location = new Point(4, 24);
            tabPage4.Name = "tabPage4";
            tabPage4.Padding = new Padding(3);
            tabPage4.Size = new Size(1590, 460);
            tabPage4.TabIndex = 1;
            tabPage4.Text = "Por Mes";
            tabPage4.UseVisualStyleBackColor = true;
            // 
            // tlpBajoNivel3Pagina2
            // 
            tlpBajoNivel3Pagina2.BackColor = Color.IndianRed;
            tlpBajoNivel3Pagina2.ColumnCount = 2;
            tlpBajoNivel3Pagina2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tlpBajoNivel3Pagina2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tlpBajoNivel3Pagina2.Controls.Add(formsPlot6, 1, 0);
            tlpBajoNivel3Pagina2.Controls.Add(formsPlot5, 0, 0);
            tlpBajoNivel3Pagina2.Dock = DockStyle.Fill;
            tlpBajoNivel3Pagina2.Location = new Point(3, 3);
            tlpBajoNivel3Pagina2.Name = "tlpBajoNivel3Pagina2";
            tlpBajoNivel3Pagina2.RowCount = 1;
            tlpBajoNivel3Pagina2.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpBajoNivel3Pagina2.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tlpBajoNivel3Pagina2.Size = new Size(1584, 454);
            tlpBajoNivel3Pagina2.TabIndex = 0;
            // 
            // formsPlot6
            // 
            formsPlot6.DisplayScale = 1F;
            formsPlot6.Dock = DockStyle.Fill;
            formsPlot6.Location = new Point(795, 3);
            formsPlot6.Name = "formsPlot6";
            formsPlot6.Size = new Size(786, 448);
            formsPlot6.TabIndex = 3;
            // 
            // formsPlot5
            // 
            formsPlot5.DisplayScale = 1F;
            formsPlot5.Dock = DockStyle.Fill;
            formsPlot5.Location = new Point(3, 3);
            formsPlot5.Name = "formsPlot5";
            formsPlot5.Size = new Size(786, 448);
            formsPlot5.TabIndex = 2;
            // 
            // dgvGrilla
            // 
            dgvGrilla.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            dgvGrilla.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvGrilla.Location = new Point(2024, -3);
            dgvGrilla.Name = "dgvGrilla";
            dgvGrilla.Size = new Size(380, 1227);
            dgvGrilla.TabIndex = 17;
            // 
            // sqlCommand1
            // 
            sqlCommand1.CommandTimeout = 30;
            sqlCommand1.EnableOptimizedParameterBinding = false;
            // 
            // FAdministracion
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1610, 990);
            Controls.Add(pnlInfoInicial);
            Controls.Add(tableLayoutPanel1);
            Controls.Add(menuStrip1);
            MaximizeBox = false;
            Name = "FAdministracion";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Panel Administración";
            Load += FAdministracion_Load;
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            tableLayoutPanel1.ResumeLayout(false);
            tlpBaseFiltrado.ResumeLayout(false);
            tlpFiltradoMesYAño.ResumeLayout(false);
            tlpFiltradoMesYAño.PerformLayout();
            tlpFiltradoBotones.ResumeLayout(false);
            pnlInfoInicial.ResumeLayout(false);
            tlpBaseNivel1.ResumeLayout(false);
            tlpArribaNivel2.ResumeLayout(false);
            tabControlGraficoArriba.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            tabPage2.ResumeLayout(false);
            tlpBajoNivel2.ResumeLayout(false);
            tabControl1.ResumeLayout(false);
            tabPage3.ResumeLayout(false);
            tlpBajoNivel3.ResumeLayout(false);
            tabPage4.ResumeLayout(false);
            tlpBajoNivel3Pagina2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvGrilla).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip menuStrip1;
        private ToolStripMenuItem eMPLEADOSToolStripMenuItem;
        private ToolStripMenuItem lISTADOEMPLEADOSToolStripMenuItem;
        private ToolStripMenuItem cLIENTESToolStripMenuItem;
        private ToolStripMenuItem lISTADOCLIENTESToolStripMenuItem;
        private ToolStripMenuItem cUENTASCORRIENTESToolStripMenuItem;
        private ToolStripMenuItem pRODUCTOToolStripMenuItem;
        private ToolStripMenuItem sTOCKToolStripMenuItem;
        private ToolStripMenuItem mARCASToolStripMenuItem;
        private ToolStripMenuItem cATEGORIASToolStripMenuItem;
        private ToolStripMenuItem rUBROSToolStripMenuItem;
        private ToolStripMenuItem oFERTASToolStripMenuItem;
        private ToolStripMenuItem lISTADOOFERTASToolStripMenuItem;
        private ToolStripMenuItem aCTIVARDESACTIVARToolStripMenuItem;
        private ToolStripMenuItem cONFIGURACIONToolStripMenuItem;
        private ToolStripMenuItem tIPOPAGOToolStripMenuItem;
        private TableLayoutPanel tableLayoutPanel1;
        private Button btnGasto;
        private Button btnMovimientos;
        private Button btnComprobantes;
        private Button btnVolver;
        private ToolStripMenuItem rOLESToolStripMenuItem;
        private ToolStripMenuItem nUEVAOFERTAToolStripMenuItem;
        private Panel pnlInfoInicial;
        private DataGridView dgvGrilla;
        private TableLayoutPanel tlpBaseNivel1;
        private Microsoft.Data.SqlClient.SqlCommand sqlCommand1;
        private TableLayoutPanel tlpArribaNivel2;
        private TableLayoutPanel tlpBajoNivel2;
        private TabControl tabControlGraficoArriba;
        private TabPage tabPage1;
        private ScottPlot.WinForms.FormsPlot formsPlot1;
        private TabPage tabPage2;
        private ScottPlot.WinForms.FormsPlot formsPlot2;
        private TableLayoutPanel tlpBaseFiltrado;
        private TableLayoutPanel tlpFiltradoMesYAño;
        private Label lblMesGraficos;
        private Label lblAñoGraficos;
        private ComboBox cbMesGrafico;
        private ComboBox cbAñoGraficos;
        private TableLayoutPanel tlpFiltradoBotones;
        private Button btnFiltrarGraficos;
        private Button button2;
        private Button btnFechaActualGraficos;
        private TabControl tabControl1;
        private TabPage tabPage3;
        private TabPage tabPage4;
        private TableLayoutPanel tlpBajoNivel3;
        private ScottPlot.WinForms.FormsPlot formsPlot3;
        private TableLayoutPanel tlpBajoNivel3Pagina2;
        private ScottPlot.WinForms.FormsPlot formsPlot5;
        private ScottPlot.WinForms.FormsPlot formsPlot4;
        private ScottPlot.WinForms.FormsPlot formsPlot6;
    }
}