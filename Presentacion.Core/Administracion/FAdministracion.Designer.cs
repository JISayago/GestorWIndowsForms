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
            btnMovimientos = new Button();
            btnVolver = new Button();
            btnComprobantes = new Button();
            pnlInfoInicial = new Panel();
            tlpBase = new TableLayoutPanel();
            tlpFiltradoGrafico4 = new TableLayoutPanel();
            tlpFiltradoGrafico3 = new TableLayoutPanel();
            tlpFiltradoGrafico2 = new TableLayoutPanel();
            formsPlot1 = new ScottPlot.WinForms.FormsPlot();
            formsPlot4 = new ScottPlot.WinForms.FormsPlot();
            formsPlot2 = new ScottPlot.WinForms.FormsPlot();
            formsPlot3 = new ScottPlot.WinForms.FormsPlot();
            tlpFiltradoGraficos1 = new TableLayoutPanel();
            dgvGrilla = new DataGridView();
            sqlCommand1 = new Microsoft.Data.SqlClient.SqlCommand();
            menuStrip1.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            pnlInfoInicial.SuspendLayout();
            tlpBase.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvGrilla).BeginInit();
            SuspendLayout();
            // 
            // menuStrip1
            // 
            menuStrip1.Items.AddRange(new ToolStripItem[] { pRODUCTOToolStripMenuItem, eMPLEADOSToolStripMenuItem, cLIENTESToolStripMenuItem, oFERTASToolStripMenuItem, cONFIGURACIONToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(1354, 24);
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
            tableLayoutPanel1.ColumnCount = 4;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tableLayoutPanel1.Controls.Add(btnGasto, 1, 0);
            tableLayoutPanel1.Controls.Add(btnMovimientos, 2, 0);
            tableLayoutPanel1.Controls.Add(btnVolver, 0, 0);
            tableLayoutPanel1.Controls.Add(btnComprobantes, 3, 0);
            tableLayoutPanel1.Dock = DockStyle.Top;
            tableLayoutPanel1.Location = new Point(0, 24);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 1;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Size = new Size(1354, 94);
            tableLayoutPanel1.TabIndex = 7;
            // 
            // btnGasto
            // 
            btnGasto.Anchor = AnchorStyles.Left;
            btnGasto.Location = new Point(341, 15);
            btnGasto.MaximumSize = new Size(236, 63);
            btnGasto.MinimumSize = new Size(236, 63);
            btnGasto.Name = "btnGasto";
            btnGasto.Size = new Size(236, 63);
            btnGasto.TabIndex = 12;
            btnGasto.Text = "GASTO";
            btnGasto.UseVisualStyleBackColor = true;
            btnGasto.Click += btnGasto_Click;
            // 
            // btnMovimientos
            // 
            btnMovimientos.Anchor = AnchorStyles.Right;
            btnMovimientos.Location = new Point(775, 15);
            btnMovimientos.MaximumSize = new Size(236, 63);
            btnMovimientos.MinimumSize = new Size(236, 63);
            btnMovimientos.Name = "btnMovimientos";
            btnMovimientos.Size = new Size(236, 63);
            btnMovimientos.TabIndex = 16;
            btnMovimientos.Text = "MOVIMIENTOS";
            btnMovimientos.UseVisualStyleBackColor = true;
            btnMovimientos.Click += btnMovimientos_Click;
            // 
            // btnVolver
            // 
            btnVolver.Anchor = AnchorStyles.Right;
            btnVolver.Location = new Point(99, 15);
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
            btnComprobantes.Location = new Point(1017, 15);
            btnComprobantes.MaximumSize = new Size(236, 63);
            btnComprobantes.MinimumSize = new Size(236, 63);
            btnComprobantes.Name = "btnComprobantes";
            btnComprobantes.Size = new Size(236, 63);
            btnComprobantes.TabIndex = 15;
            btnComprobantes.Text = "COMPROBANTES";
            btnComprobantes.UseVisualStyleBackColor = true;
            btnComprobantes.Click += btnComprobantes_Click;
            // 
            // pnlInfoInicial
            // 
            pnlInfoInicial.BackColor = SystemColors.ActiveCaption;
            pnlInfoInicial.Controls.Add(tlpBase);
            pnlInfoInicial.Controls.Add(dgvGrilla);
            pnlInfoInicial.Dock = DockStyle.Fill;
            pnlInfoInicial.Location = new Point(0, 118);
            pnlInfoInicial.Name = "pnlInfoInicial";
            pnlInfoInicial.Size = new Size(1354, 650);
            pnlInfoInicial.TabIndex = 24;
            pnlInfoInicial.Paint += pnlInfoInicial_Paint;
            // 
            // tlpBase
            // 
            tlpBase.BackColor = SystemColors.MenuHighlight;
            tlpBase.ColumnCount = 2;
            tlpBase.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tlpBase.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tlpBase.Controls.Add(tlpFiltradoGrafico4, 1, 2);
            tlpBase.Controls.Add(tlpFiltradoGrafico3, 0, 2);
            tlpBase.Controls.Add(tlpFiltradoGrafico2, 1, 0);
            tlpBase.Controls.Add(formsPlot1, 0, 1);
            tlpBase.Controls.Add(formsPlot4, 1, 3);
            tlpBase.Controls.Add(formsPlot2, 1, 1);
            tlpBase.Controls.Add(formsPlot3, 0, 3);
            tlpBase.Controls.Add(tlpFiltradoGraficos1, 0, 0);
            tlpBase.Dock = DockStyle.Fill;
            tlpBase.Location = new Point(0, 0);
            tlpBase.Name = "tlpBase";
            tlpBase.RowCount = 4;
            tlpBase.RowStyles.Add(new RowStyle(SizeType.Percent, 11.0389624F));
            tlpBase.RowStyles.Add(new RowStyle(SizeType.Percent, 38.9610367F));
            tlpBase.RowStyles.Add(new RowStyle(SizeType.Percent, 11.0389624F));
            tlpBase.RowStyles.Add(new RowStyle(SizeType.Percent, 38.9610329F));
            tlpBase.Size = new Size(1354, 650);
            tlpBase.TabIndex = 24;
            // 
            // tlpFiltradoGrafico4
            // 
            tlpFiltradoGrafico4.BackColor = SystemColors.Info;
            tlpFiltradoGrafico4.ColumnCount = 2;
            tlpFiltradoGrafico4.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tlpFiltradoGrafico4.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tlpFiltradoGrafico4.Dock = DockStyle.Fill;
            tlpFiltradoGrafico4.Location = new Point(680, 327);
            tlpFiltradoGrafico4.Name = "tlpFiltradoGrafico4";
            tlpFiltradoGrafico4.RowCount = 1;
            tlpFiltradoGrafico4.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpFiltradoGrafico4.Size = new Size(671, 65);
            tlpFiltradoGrafico4.TabIndex = 27;
            // 
            // tlpFiltradoGrafico3
            // 
            tlpFiltradoGrafico3.BackColor = SystemColors.Info;
            tlpFiltradoGrafico3.ColumnCount = 2;
            tlpFiltradoGrafico3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tlpFiltradoGrafico3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tlpFiltradoGrafico3.Dock = DockStyle.Fill;
            tlpFiltradoGrafico3.Location = new Point(3, 327);
            tlpFiltradoGrafico3.Name = "tlpFiltradoGrafico3";
            tlpFiltradoGrafico3.RowCount = 1;
            tlpFiltradoGrafico3.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpFiltradoGrafico3.Size = new Size(671, 65);
            tlpFiltradoGrafico3.TabIndex = 26;
            // 
            // tlpFiltradoGrafico2
            // 
            tlpFiltradoGrafico2.BackColor = SystemColors.Info;
            tlpFiltradoGrafico2.ColumnCount = 2;
            tlpFiltradoGrafico2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tlpFiltradoGrafico2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tlpFiltradoGrafico2.Dock = DockStyle.Fill;
            tlpFiltradoGrafico2.Location = new Point(680, 3);
            tlpFiltradoGrafico2.Name = "tlpFiltradoGrafico2";
            tlpFiltradoGrafico2.RowCount = 1;
            tlpFiltradoGrafico2.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpFiltradoGrafico2.Size = new Size(671, 65);
            tlpFiltradoGrafico2.TabIndex = 25;
            // 
            // formsPlot1
            // 
            formsPlot1.DisplayScale = 1F;
            formsPlot1.Dock = DockStyle.Fill;
            formsPlot1.Location = new Point(3, 74);
            formsPlot1.Name = "formsPlot1";
            formsPlot1.Size = new Size(671, 247);
            formsPlot1.TabIndex = 20;
            formsPlot1.Load += formsPlot1_Load;
            // 
            // formsPlot4
            // 
            formsPlot4.DisplayScale = 1F;
            formsPlot4.Dock = DockStyle.Fill;
            formsPlot4.Location = new Point(680, 398);
            formsPlot4.Name = "formsPlot4";
            formsPlot4.Size = new Size(671, 249);
            formsPlot4.TabIndex = 23;
            // 
            // formsPlot2
            // 
            formsPlot2.DisplayScale = 1F;
            formsPlot2.Dock = DockStyle.Fill;
            formsPlot2.Location = new Point(680, 74);
            formsPlot2.Name = "formsPlot2";
            formsPlot2.Size = new Size(671, 247);
            formsPlot2.TabIndex = 21;
            // 
            // formsPlot3
            // 
            formsPlot3.DisplayScale = 1F;
            formsPlot3.Dock = DockStyle.Fill;
            formsPlot3.Location = new Point(3, 398);
            formsPlot3.Name = "formsPlot3";
            formsPlot3.Size = new Size(671, 249);
            formsPlot3.TabIndex = 22;
            // 
            // tlpFiltradoGraficos1
            // 
            tlpFiltradoGraficos1.BackColor = SystemColors.Info;
            tlpFiltradoGraficos1.ColumnCount = 2;
            tlpFiltradoGraficos1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tlpFiltradoGraficos1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tlpFiltradoGraficos1.Dock = DockStyle.Fill;
            tlpFiltradoGraficos1.Location = new Point(3, 3);
            tlpFiltradoGraficos1.Name = "tlpFiltradoGraficos1";
            tlpFiltradoGraficos1.RowCount = 1;
            tlpFiltradoGraficos1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpFiltradoGraficos1.Size = new Size(671, 65);
            tlpFiltradoGraficos1.TabIndex = 24;
            // 
            // dgvGrilla
            // 
            dgvGrilla.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            dgvGrilla.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvGrilla.Location = new Point(1768, -3);
            dgvGrilla.Name = "dgvGrilla";
            dgvGrilla.Size = new Size(380, 1005);
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
            ClientSize = new Size(1354, 768);
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
            pnlInfoInicial.ResumeLayout(false);
            tlpBase.ResumeLayout(false);
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
        private ScottPlot.WinForms.FormsPlot formsPlot2;
        private ScottPlot.WinForms.FormsPlot formsPlot1;
        private ScottPlot.WinForms.FormsPlot formsPlot3;
        private ScottPlot.WinForms.FormsPlot formsPlot4;
        private TableLayoutPanel tlpBase;
        private Microsoft.Data.SqlClient.SqlCommand sqlCommand1;
        private TableLayoutPanel tlpFiltradoGraficos1;
        private TableLayoutPanel tlpFiltradoGrafico4;
        private TableLayoutPanel tlpFiltradoGrafico3;
        private TableLayoutPanel tlpFiltradoGrafico2;
    }
}