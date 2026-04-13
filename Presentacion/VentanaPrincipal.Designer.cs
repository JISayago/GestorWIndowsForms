namespace Presentacion
{
    partial class VentanaPrincipal
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            sqlCommand1 = new Microsoft.Data.SqlClient.SqlCommand();
            PnlBotones = new Panel();
            tableLayoutPanel1 = new TableLayoutPanel();
            btnPanelAdmin = new Button();
            btnVenta = new Button();
            btnCaja = new Button();
            btnContraVenta = new Button();
            tlpBaseInfo1 = new TableLayoutPanel();
            tableLayoutPanel2 = new TableLayoutPanel();
            flowLayoutPanel1 = new FlowLayoutPanel();
            lblUsuario = new Label();
            lblNombreUsuario = new Label();
            llbCerrarSesion = new LinkLabel();
            flowLayoutPanel2 = new FlowLayoutPanel();
            lblFechaValor = new Label();
            lblFecha = new Label();
            flowLayoutPanel3 = new FlowLayoutPanel();
            lblHora = new Label();
            lblHoraValor = new Label();
            tlpInoformacion1 = new TableLayoutPanel();
            tcIzquierda = new TabControl();
            tabPage1 = new TabPage();
            tabPage2 = new TabPage();
            tableLayoutPanel3 = new TableLayoutPanel();
            panel2 = new Panel();
            panel1 = new Panel();
            ((System.ComponentModel.ISupportInitialize)error).BeginInit();
            PnlBotones.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            tlpBaseInfo1.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            flowLayoutPanel1.SuspendLayout();
            flowLayoutPanel2.SuspendLayout();
            flowLayoutPanel3.SuspendLayout();
            tlpInoformacion1.SuspendLayout();
            tcIzquierda.SuspendLayout();
            tableLayoutPanel3.SuspendLayout();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // sqlCommand1
            // 
            sqlCommand1.CommandTimeout = 30;
            sqlCommand1.EnableOptimizedParameterBinding = false;
            // 
            // PnlBotones
            // 
            PnlBotones.Controls.Add(tableLayoutPanel1);
            PnlBotones.Dock = DockStyle.Fill;
            PnlBotones.Location = new Point(3, 726);
            PnlBotones.Name = "PnlBotones";
            PnlBotones.Size = new Size(1259, 87);
            PnlBotones.TabIndex = 18;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 4;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tableLayoutPanel1.Controls.Add(btnPanelAdmin, 0, 0);
            tableLayoutPanel1.Controls.Add(btnVenta, 3, 0);
            tableLayoutPanel1.Controls.Add(btnCaja, 1, 0);
            tableLayoutPanel1.Controls.Add(btnContraVenta, 2, 0);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 1;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Size = new Size(1259, 87);
            tableLayoutPanel1.TabIndex = 6;
            // 
            // btnPanelAdmin
            // 
            btnPanelAdmin.Anchor = AnchorStyles.Left;
            btnPanelAdmin.Location = new Point(3, 12);
            btnPanelAdmin.MaximumSize = new Size(236, 63);
            btnPanelAdmin.MinimumSize = new Size(236, 63);
            btnPanelAdmin.Name = "btnPanelAdmin";
            btnPanelAdmin.Size = new Size(236, 63);
            btnPanelAdmin.TabIndex = 15;
            btnPanelAdmin.Text = "PANEL ADMINISTRACIÓN";
            btnPanelAdmin.UseVisualStyleBackColor = true;
            btnPanelAdmin.Click += btnPanelAdmin_Click;
            // 
            // btnVenta
            // 
            btnVenta.Anchor = AnchorStyles.Right;
            btnVenta.Location = new Point(1020, 12);
            btnVenta.MaximumSize = new Size(236, 63);
            btnVenta.MinimumSize = new Size(236, 63);
            btnVenta.Name = "btnVenta";
            btnVenta.Size = new Size(236, 63);
            btnVenta.TabIndex = 12;
            btnVenta.Text = "VENTA";
            btnVenta.UseVisualStyleBackColor = true;
            btnVenta.Click += btnVenta_Click;
            // 
            // btnCaja
            // 
            btnCaja.Anchor = AnchorStyles.Right;
            btnCaja.Location = new Point(389, 12);
            btnCaja.MaximumSize = new Size(236, 63);
            btnCaja.MinimumSize = new Size(236, 63);
            btnCaja.Name = "btnCaja";
            btnCaja.Size = new Size(236, 63);
            btnCaja.TabIndex = 13;
            btnCaja.Text = "CAJA";
            btnCaja.UseVisualStyleBackColor = true;
            btnCaja.Click += btnCaja_Click;
            // 
            // btnContraVenta
            // 
            btnContraVenta.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            btnContraVenta.Location = new Point(631, 12);
            btnContraVenta.MaximumSize = new Size(236, 63);
            btnContraVenta.MinimumSize = new Size(236, 63);
            btnContraVenta.Name = "btnContraVenta";
            btnContraVenta.Size = new Size(236, 63);
            btnContraVenta.TabIndex = 16;
            btnContraVenta.Text = "DEVOLUCIÓN / CONTRAASIENTO";
            btnContraVenta.UseVisualStyleBackColor = true;
            btnContraVenta.Click += btnContraVenta_Click;
            // 
            // tlpBaseInfo1
            // 
            tlpBaseInfo1.BackColor = SystemColors.ActiveCaption;
            tlpBaseInfo1.ColumnCount = 1;
            tlpBaseInfo1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpBaseInfo1.Controls.Add(tableLayoutPanel2, 0, 0);
            tlpBaseInfo1.Controls.Add(PnlBotones, 0, 2);
            tlpBaseInfo1.Controls.Add(tlpInoformacion1, 0, 1);
            tlpBaseInfo1.Dock = DockStyle.Fill;
            tlpBaseInfo1.Location = new Point(0, 0);
            tlpBaseInfo1.Name = "tlpBaseInfo1";
            tlpBaseInfo1.RowCount = 3;
            tlpBaseInfo1.RowStyles.Add(new RowStyle(SizeType.Percent, 7.729405F));
            tlpBaseInfo1.RowStyles.Add(new RowStyle(SizeType.Percent, 80.96402F));
            tlpBaseInfo1.RowStyles.Add(new RowStyle(SizeType.Percent, 11.3065691F));
            tlpBaseInfo1.Size = new Size(1265, 816);
            tlpBaseInfo1.TabIndex = 19;
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.ColumnCount = 3;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
            tableLayoutPanel2.Controls.Add(flowLayoutPanel1, 0, 0);
            tableLayoutPanel2.Controls.Add(flowLayoutPanel2, 1, 0);
            tableLayoutPanel2.Controls.Add(flowLayoutPanel3, 2, 0);
            tableLayoutPanel2.Dock = DockStyle.Bottom;
            tableLayoutPanel2.Location = new Point(3, 16);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.Padding = new Padding(20, 0, 20, 0);
            tableLayoutPanel2.RowCount = 1;
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel2.Size = new Size(1259, 44);
            tableLayoutPanel2.TabIndex = 1;
            // 
            // flowLayoutPanel1
            // 
            flowLayoutPanel1.Controls.Add(lblUsuario);
            flowLayoutPanel1.Controls.Add(lblNombreUsuario);
            flowLayoutPanel1.Controls.Add(llbCerrarSesion);
            flowLayoutPanel1.Location = new Point(23, 3);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            flowLayoutPanel1.Size = new Size(328, 38);
            flowLayoutPanel1.TabIndex = 27;
            // 
            // lblUsuario
            // 
            lblUsuario.AutoSize = true;
            lblUsuario.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblUsuario.Location = new Point(3, 0);
            lblUsuario.Name = "lblUsuario";
            lblUsuario.Size = new Size(125, 20);
            lblUsuario.TabIndex = 26;
            lblUsuario.Text = "Usuario Logeado:";
            // 
            // lblNombreUsuario
            // 
            lblNombreUsuario.AutoSize = true;
            lblNombreUsuario.Location = new Point(134, 0);
            lblNombreUsuario.Name = "lblNombreUsuario";
            lblNombreUsuario.Size = new Size(38, 15);
            lblNombreUsuario.TabIndex = 25;
            lblNombreUsuario.Text = "label3";
            // 
            // llbCerrarSesion
            // 
            llbCerrarSesion.AutoSize = true;
            llbCerrarSesion.Location = new Point(178, 0);
            llbCerrarSesion.Name = "llbCerrarSesion";
            llbCerrarSesion.Size = new Size(76, 15);
            llbCerrarSesion.TabIndex = 28;
            llbCerrarSesion.TabStop = true;
            llbCerrarSesion.Text = "Cerrar Sesión";
            llbCerrarSesion.LinkClicked += llbCerrarSesion_LinkClicked;
            // 
            // flowLayoutPanel2
            // 
            flowLayoutPanel2.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            flowLayoutPanel2.Controls.Add(lblFechaValor);
            flowLayoutPanel2.Controls.Add(lblFecha);
            flowLayoutPanel2.FlowDirection = FlowDirection.RightToLeft;
            flowLayoutPanel2.Location = new Point(501, 3);
            flowLayoutPanel2.Name = "flowLayoutPanel2";
            flowLayoutPanel2.Size = new Size(328, 38);
            flowLayoutPanel2.TabIndex = 28;
            // 
            // lblFechaValor
            // 
            lblFechaValor.AutoSize = true;
            lblFechaValor.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblFechaValor.Location = new Point(198, 0);
            lblFechaValor.Name = "lblFechaValor";
            lblFechaValor.Size = new Size(127, 30);
            lblFechaValor.TabIndex = 25;
            lblFechaValor.Text = "00/00/0000";
            // 
            // lblFecha
            // 
            lblFecha.AutoSize = true;
            lblFecha.Font = new Font("Segoe UI", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblFecha.Location = new Point(120, 0);
            lblFecha.Name = "lblFecha";
            lblFecha.Size = new Size(72, 30);
            lblFecha.TabIndex = 26;
            lblFecha.Text = "Fecha:";
            // 
            // flowLayoutPanel3
            // 
            flowLayoutPanel3.Controls.Add(lblHora);
            flowLayoutPanel3.Controls.Add(lblHoraValor);
            flowLayoutPanel3.Dock = DockStyle.Right;
            error.SetIconAlignment(flowLayoutPanel3, ErrorIconAlignment.MiddleLeft);
            flowLayoutPanel3.Location = new Point(907, 3);
            flowLayoutPanel3.Name = "flowLayoutPanel3";
            flowLayoutPanel3.Size = new Size(329, 38);
            flowLayoutPanel3.TabIndex = 29;
            // 
            // lblHora
            // 
            lblHora.AutoSize = true;
            lblHora.Font = new Font("Segoe UI", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblHora.ImageAlign = ContentAlignment.MiddleLeft;
            lblHora.Location = new Point(3, 0);
            lblHora.Name = "lblHora";
            lblHora.Size = new Size(63, 30);
            lblHora.TabIndex = 27;
            lblHora.Text = "Hora:";
            // 
            // lblHoraValor
            // 
            lblHoraValor.AutoSize = true;
            lblHoraValor.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblHoraValor.ImageAlign = ContentAlignment.MiddleLeft;
            lblHoraValor.Location = new Point(72, 0);
            lblHoraValor.Name = "lblHoraValor";
            lblHoraValor.Size = new Size(97, 30);
            lblHoraValor.TabIndex = 25;
            lblHoraValor.Text = "00:00:00";
            // 
            // tlpInoformacion1
            // 
            tlpInoformacion1.BackColor = Color.SandyBrown;
            tlpInoformacion1.ColumnCount = 2;
            tlpInoformacion1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 66.80027F));
            tlpInoformacion1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.19973F));
            tlpInoformacion1.Controls.Add(tcIzquierda, 0, 0);
            tlpInoformacion1.Controls.Add(tableLayoutPanel3, 1, 0);
            tlpInoformacion1.Dock = DockStyle.Fill;
            tlpInoformacion1.Location = new Point(3, 66);
            tlpInoformacion1.Name = "tlpInoformacion1";
            tlpInoformacion1.RowCount = 1;
            tlpInoformacion1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpInoformacion1.Size = new Size(1259, 654);
            tlpInoformacion1.TabIndex = 19;
            // 
            // tcIzquierda
            // 
            tcIzquierda.Controls.Add(tabPage1);
            tcIzquierda.Controls.Add(tabPage2);
            tcIzquierda.Dock = DockStyle.Fill;
            tcIzquierda.Location = new Point(3, 3);
            tcIzquierda.Name = "tcIzquierda";
            tcIzquierda.SelectedIndex = 0;
            tcIzquierda.Size = new Size(835, 648);
            tcIzquierda.TabIndex = 0;
            // 
            // tabPage1
            // 
            tabPage1.Location = new Point(4, 24);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3);
            tabPage1.Size = new Size(827, 620);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "tabPage1";
            tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            tabPage2.Location = new Point(4, 24);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(3);
            tabPage2.Size = new Size(827, 620);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "tabPage2";
            tabPage2.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel3
            // 
            tableLayoutPanel3.BackColor = Color.SaddleBrown;
            tableLayoutPanel3.ColumnCount = 1;
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel3.Controls.Add(panel2, 0, 1);
            tableLayoutPanel3.Dock = DockStyle.Fill;
            tableLayoutPanel3.Location = new Point(844, 3);
            tableLayoutPanel3.Name = "tableLayoutPanel3";
            tableLayoutPanel3.RowCount = 2;
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel3.Size = new Size(412, 648);
            tableLayoutPanel3.TabIndex = 1;
            // 
            // panel2
            // 
            panel2.BackColor = Color.MistyRose;
            panel2.Dock = DockStyle.Fill;
            panel2.Location = new Point(3, 23);
            panel2.Name = "panel2";
            panel2.Size = new Size(406, 622);
            panel2.TabIndex = 0;
            // 
            // panel1
            // 
            panel1.Controls.Add(tlpBaseInfo1);
            panel1.Dock = DockStyle.Fill;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(1265, 816);
            panel1.TabIndex = 24;
            // 
            // VentanaPrincipal
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            ClientSize = new Size(1265, 816);
            Controls.Add(panel1);
            MaximizeBox = true;
            MinimizeBox = true;
            MinimumSize = new Size(1061, 732);
            Name = "VentanaPrincipal";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Inicio";
            WindowState = FormWindowState.Maximized;
            Load += VentanaPrincipal_Load;
            ((System.ComponentModel.ISupportInitialize)error).EndInit();
            PnlBotones.ResumeLayout(false);
            tableLayoutPanel1.ResumeLayout(false);
            tlpBaseInfo1.ResumeLayout(false);
            tableLayoutPanel2.ResumeLayout(false);
            flowLayoutPanel1.ResumeLayout(false);
            flowLayoutPanel1.PerformLayout();
            flowLayoutPanel2.ResumeLayout(false);
            flowLayoutPanel2.PerformLayout();
            flowLayoutPanel3.ResumeLayout(false);
            flowLayoutPanel3.PerformLayout();
            tlpInoformacion1.ResumeLayout(false);
            tcIzquierda.ResumeLayout(false);
            tableLayoutPanel3.ResumeLayout(false);
            panel1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion
        private Microsoft.Data.SqlClient.SqlCommand sqlCommand1;
        private Panel PnlBotones;
        private TableLayoutPanel tableLayoutPanel1;
        private Button btnPanelAdmin;
        private Button btnVenta;
        private Button btnCaja;
        private Button btnContraVenta;
        private TableLayoutPanel tlpBaseInfo1;
        private Panel panel1;
        private FlowLayoutPanel flowLayoutPanel3;
        private Label lblHora;
        private Label lblHoraValor;
        private LinkLabel llbCerrarSesion;
        private FlowLayoutPanel flowLayoutPanel2;
        private Label lblFechaValor;
        private Label lblFecha;
        private FlowLayoutPanel flowLayoutPanel1;
        private Label lblUsuario;
        private Label lblNombreUsuario;
        private TableLayoutPanel tableLayoutPanel2;
        private TableLayoutPanel tlpInoformacion1;
        private TabControl tcIzquierda;
        private TabPage tabPage1;
        private TabPage tabPage2;
        private TableLayoutPanel tableLayoutPanel3;
        private Panel panel2;
    }
}
