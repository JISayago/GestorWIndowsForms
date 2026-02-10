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
            tableLayoutPanel2 = new TableLayoutPanel();
            formsPlot2 = new ScottPlot.WinForms.FormsPlot();
            formsPlot1 = new ScottPlot.WinForms.FormsPlot();
            tableLayoutPanel3 = new TableLayoutPanel();
            formsPlot3 = new ScottPlot.WinForms.FormsPlot();
            formsPlot4 = new ScottPlot.WinForms.FormsPlot();
            formsPlot5 = new ScottPlot.WinForms.FormsPlot();
            formsPlot6 = new ScottPlot.WinForms.FormsPlot();
            dgvGrilla = new DataGridView();
            sqlCommand1 = new Microsoft.Data.SqlClient.SqlCommand();
            menuStrip1.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            pnlInfoInicial.SuspendLayout();
            tlpBase.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            tableLayoutPanel3.SuspendLayout();
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
            tableLayoutPanel1.Size = new Size(1610, 94);
            tableLayoutPanel1.TabIndex = 7;
            // 
            // btnGasto
            // 
            btnGasto.Anchor = AnchorStyles.Left;
            btnGasto.Location = new Point(405, 15);
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
            btnMovimientos.Location = new Point(967, 15);
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
            btnVolver.Location = new Point(163, 15);
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
            btnComprobantes.Location = new Point(1209, 15);
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
            pnlInfoInicial.Size = new Size(1610, 872);
            pnlInfoInicial.TabIndex = 24;
            pnlInfoInicial.Paint += pnlInfoInicial_Paint;
            // 
            // tlpBase
            // 
            tlpBase.BackColor = SystemColors.MenuHighlight;
            tlpBase.ColumnCount = 1;
            tlpBase.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpBase.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            tlpBase.Controls.Add(tableLayoutPanel2, 0, 0);
            tlpBase.Controls.Add(tableLayoutPanel3, 0, 1);
            tlpBase.Dock = DockStyle.Top;
            tlpBase.Location = new Point(0, 0);
            tlpBase.Name = "tlpBase";
            tlpBase.RowCount = 2;
            tlpBase.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpBase.RowStyles.Add(new RowStyle(SizeType.Absolute, 563F));
            tlpBase.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tlpBase.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tlpBase.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tlpBase.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tlpBase.Size = new Size(1610, 872);
            tlpBase.TabIndex = 24;
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.BackColor = Color.FromArgb(192, 255, 192);
            tableLayoutPanel2.ColumnCount = 1;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel2.Controls.Add(formsPlot2, 0, 0);
            tableLayoutPanel2.Controls.Add(formsPlot1, 0, 1);
            tableLayoutPanel2.Dock = DockStyle.Fill;
            tableLayoutPanel2.ForeColor = SystemColors.ButtonShadow;
            tableLayoutPanel2.Location = new Point(3, 3);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 2;
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 25.10917F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 74.89083F));
            tableLayoutPanel2.Size = new Size(1604, 303);
            tableLayoutPanel2.TabIndex = 0;
            // 
            // formsPlot2
            // 
            formsPlot2.DisplayScale = 1F;
            formsPlot2.Location = new Point(3, 3);
            formsPlot2.Name = "formsPlot2";
            formsPlot2.Size = new Size(1598, 70);
            formsPlot2.TabIndex = 21;
            // 
            // formsPlot1
            // 
            formsPlot1.DisplayScale = 1F;
            formsPlot1.Dock = DockStyle.Fill;
            formsPlot1.Location = new Point(3, 79);
            formsPlot1.Name = "formsPlot1";
            formsPlot1.Size = new Size(1598, 221);
            formsPlot1.TabIndex = 20;
            formsPlot1.Load += formsPlot1_Load;
            // 
            // tableLayoutPanel3
            // 
            tableLayoutPanel3.BackColor = Color.RosyBrown;
            tableLayoutPanel3.ColumnCount = 2;
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 49.82332F));
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50.1766777F));
            tableLayoutPanel3.Controls.Add(formsPlot3, 0, 1);
            tableLayoutPanel3.Controls.Add(formsPlot4, 1, 1);
            tableLayoutPanel3.Controls.Add(formsPlot5, 0, 3);
            tableLayoutPanel3.Controls.Add(formsPlot6, 1, 3);
            tableLayoutPanel3.Dock = DockStyle.Fill;
            tableLayoutPanel3.Location = new Point(3, 312);
            tableLayoutPanel3.Name = "tableLayoutPanel3";
            tableLayoutPanel3.RowCount = 4;
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 12.2641516F));
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 37.73585F));
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 12.2641516F));
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 37.73585F));
            tableLayoutPanel3.Size = new Size(1604, 557);
            tableLayoutPanel3.TabIndex = 1;
            // 
            // formsPlot3
            // 
            formsPlot3.DisplayScale = 1F;
            formsPlot3.Dock = DockStyle.Fill;
            formsPlot3.Location = new Point(3, 71);
            formsPlot3.Name = "formsPlot3";
            formsPlot3.Size = new Size(793, 204);
            formsPlot3.TabIndex = 22;
            // 
            // formsPlot4
            // 
            formsPlot4.DisplayScale = 1F;
            formsPlot4.Dock = DockStyle.Fill;
            formsPlot4.Location = new Point(802, 71);
            formsPlot4.Name = "formsPlot4";
            formsPlot4.Size = new Size(799, 204);
            formsPlot4.TabIndex = 23;
            // 
            // formsPlot5
            // 
            formsPlot5.DisplayScale = 1F;
            formsPlot5.Dock = DockStyle.Fill;
            formsPlot5.Location = new Point(3, 349);
            formsPlot5.Name = "formsPlot5";
            formsPlot5.Size = new Size(793, 205);
            formsPlot5.TabIndex = 28;
            // 
            // formsPlot6
            // 
            formsPlot6.DisplayScale = 1F;
            formsPlot6.Dock = DockStyle.Fill;
            formsPlot6.Location = new Point(802, 349);
            formsPlot6.Name = "formsPlot6";
            formsPlot6.Size = new Size(799, 205);
            formsPlot6.TabIndex = 29;
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
            pnlInfoInicial.ResumeLayout(false);
            tlpBase.ResumeLayout(false);
            tableLayoutPanel2.ResumeLayout(false);
            tableLayoutPanel3.ResumeLayout(false);
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
        private ScottPlot.WinForms.FormsPlot formsPlot5;
        private ScottPlot.WinForms.FormsPlot formsPlot6;
        private TableLayoutPanel tableLayoutPanel2;
        private TableLayoutPanel tableLayoutPanel3;
    }
}