using Presentacion.Core.Administracion;

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(VentanaPrincipal));
            sqlCommand1 = new Microsoft.Data.SqlClient.SqlCommand();
            PnlBotones = new Panel();
            tableLayoutPanel1 = new TableLayoutPanel();
            btnVenta = new Button();
            btnContraVenta = new Button();
            btnCaja = new Button();
            btnPanelAdmin = new Button();
            tlpBaseInfo1 = new TableLayoutPanel();
            tableLayoutPanel2 = new TableLayoutPanel();
            tableLayoutPanel4 = new TableLayoutPanel();
            tableLayoutPanel3 = new TableLayoutPanel();
            flowHeaderUsuario = new FlowLayoutPanel();
            lblNombreUsuario = new Label();
            lblUsuario = new Label();
            llbCerrarSesion = new LinkLabel();
            tableLayoutPanel5 = new TableLayoutPanel();
            tableLayoutPanel6 = new TableLayoutPanel();
            flowLayoutPanel2 = new FlowLayoutPanel();
            lblFecha = new Label();
            lblFechaValor = new Label();
            lblHora = new Label();
            lblHoraValor = new Label();
            tlpPanelBaseTabControlYNotis = new TableLayoutPanel();
            tcIzquierda = new FlatTabControl();
            tabPage1 = new TabPage();
            tabPage2 = new TabPage();
            tlpNotificaciones0 = new TableLayoutPanel();
            flowLayoutNotificaciones = new FlowLayoutPanel();
            btnRefresh = new Button();
            flowLayoutPanel3 = new FlowLayoutPanel();
            panel1 = new Panel();
            ((System.ComponentModel.ISupportInitialize)error).BeginInit();
            PnlBotones.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            tlpBaseInfo1.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            tableLayoutPanel4.SuspendLayout();
            tableLayoutPanel3.SuspendLayout();
            flowHeaderUsuario.SuspendLayout();
            tableLayoutPanel5.SuspendLayout();
            tableLayoutPanel6.SuspendLayout();
            flowLayoutPanel2.SuspendLayout();
            tlpPanelBaseTabControlYNotis.SuspendLayout();
            tcIzquierda.SuspendLayout();
            tlpNotificaciones0.SuspendLayout();
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
            PnlBotones.Location = new Point(3, 728);
            PnlBotones.Name = "PnlBotones";
            PnlBotones.Padding = new Padding(20, 0, 20, 0);
            PnlBotones.Size = new Size(1259, 85);
            PnlBotones.TabIndex = 18;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 6;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 3.06927F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 23.4653645F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 23.4653645F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 23.4653645F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 23.4653645F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 3.06927F));
            tableLayoutPanel1.Controls.Add(btnVenta, 4, 0);
            tableLayoutPanel1.Controls.Add(btnContraVenta, 3, 0);
            tableLayoutPanel1.Controls.Add(btnCaja, 2, 0);
            tableLayoutPanel1.Controls.Add(btnPanelAdmin, 1, 0);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(20, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 1;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Size = new Size(1219, 85);
            tableLayoutPanel1.TabIndex = 6;
            // 
            // btnVenta
            // 
            btnVenta.Anchor = AnchorStyles.Right;
            btnVenta.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold);
            btnVenta.Location = new Point(942, 11);
            btnVenta.MaximumSize = new Size(236, 63);
            btnVenta.MinimumSize = new Size(236, 63);
            btnVenta.Name = "btnVenta";
            btnVenta.Size = new Size(236, 63);
            btnVenta.TabIndex = 12;
            btnVenta.Text = "VENTA";
            btnVenta.UseVisualStyleBackColor = true;
            btnVenta.Click += btnVenta_Click;
            // 
            // btnContraVenta
            // 
            btnContraVenta.Anchor = AnchorStyles.None;
            btnContraVenta.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold);
            btnContraVenta.Location = new Point(634, 11);
            btnContraVenta.MaximumSize = new Size(236, 63);
            btnContraVenta.MinimumSize = new Size(236, 63);
            btnContraVenta.Name = "btnContraVenta";
            btnContraVenta.Size = new Size(236, 63);
            btnContraVenta.TabIndex = 16;
            btnContraVenta.Text = "DEVOLUCIÓN / CONTRAASIENTO";
            btnContraVenta.UseVisualStyleBackColor = true;
            btnContraVenta.Click += btnContraVenta_Click;
            // 
            // btnCaja
            // 
            btnCaja.Anchor = AnchorStyles.None;
            btnCaja.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold);
            btnCaja.Location = new Point(348, 11);
            btnCaja.MaximumSize = new Size(236, 63);
            btnCaja.MinimumSize = new Size(236, 63);
            btnCaja.Name = "btnCaja";
            btnCaja.Size = new Size(236, 63);
            btnCaja.TabIndex = 13;
            btnCaja.Text = "CAJA";
            btnCaja.UseVisualStyleBackColor = true;
            btnCaja.Click += btnCaja_Click;
            // 
            // btnPanelAdmin
            // 
            btnPanelAdmin.Anchor = AnchorStyles.Left;
            btnPanelAdmin.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold);
            btnPanelAdmin.Location = new Point(40, 11);
            btnPanelAdmin.MaximumSize = new Size(236, 63);
            btnPanelAdmin.MinimumSize = new Size(236, 63);
            btnPanelAdmin.Name = "btnPanelAdmin";
            btnPanelAdmin.Size = new Size(236, 63);
            btnPanelAdmin.TabIndex = 15;
            btnPanelAdmin.Text = "PANEL ADMINISTRACIÓN";
            btnPanelAdmin.UseVisualStyleBackColor = true;
            btnPanelAdmin.Click += btnPanelAdmin_Click;
            // 
            // tlpBaseInfo1
            // 
            tlpBaseInfo1.BackColor = SystemColors.ButtonFace;
            tlpBaseInfo1.ColumnCount = 1;
            tlpBaseInfo1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpBaseInfo1.Controls.Add(tableLayoutPanel2, 0, 0);
            tlpBaseInfo1.Controls.Add(PnlBotones, 0, 2);
            tlpBaseInfo1.Controls.Add(tlpPanelBaseTabControlYNotis, 0, 1);
            tlpBaseInfo1.Dock = DockStyle.Fill;
            tlpBaseInfo1.Location = new Point(0, 0);
            tlpBaseInfo1.Name = "tlpBaseInfo1";
            tlpBaseInfo1.RowCount = 3;
            tlpBaseInfo1.RowStyles.Add(new RowStyle(SizeType.Percent, 10F));
            tlpBaseInfo1.RowStyles.Add(new RowStyle(SizeType.Percent, 79F));
            tlpBaseInfo1.RowStyles.Add(new RowStyle(SizeType.Percent, 11F));
            tlpBaseInfo1.Size = new Size(1265, 816);
            tlpBaseInfo1.TabIndex = 19;
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.ColumnCount = 3;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 55.85366F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 34.94668F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 9.269894F));
            tableLayoutPanel2.Controls.Add(tableLayoutPanel4, 1, 0);
            tableLayoutPanel2.Controls.Add(llbCerrarSesion, 2, 0);
            tableLayoutPanel2.Controls.Add(tableLayoutPanel5, 0, 0);
            tableLayoutPanel2.Dock = DockStyle.Fill;
            tableLayoutPanel2.Location = new Point(3, 3);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.Padding = new Padding(20, 0, 20, 0);
            tableLayoutPanel2.RowCount = 1;
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel2.Size = new Size(1259, 75);
            tableLayoutPanel2.TabIndex = 1;
            // 
            // tableLayoutPanel4
            // 
            tableLayoutPanel4.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tableLayoutPanel4.ColumnCount = 1;
            tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel4.Controls.Add(tableLayoutPanel3, 0, 0);
            tableLayoutPanel4.Location = new Point(703, 3);
            tableLayoutPanel4.Name = "tableLayoutPanel4";
            tableLayoutPanel4.RowCount = 2;
            tableLayoutPanel4.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel4.RowStyles.Add(new RowStyle(SizeType.Absolute, 8F));
            tableLayoutPanel4.Size = new Size(419, 69);
            tableLayoutPanel4.TabIndex = 29;
            // 
            // tableLayoutPanel3
            // 
            tableLayoutPanel3.ColumnCount = 1;
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel3.Controls.Add(flowHeaderUsuario, 0, 1);
            tableLayoutPanel3.Dock = DockStyle.Fill;
            tableLayoutPanel3.Location = new Point(3, 3);
            tableLayoutPanel3.Name = "tableLayoutPanel3";
            tableLayoutPanel3.RowCount = 3;
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 15F));
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 70F));
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 15F));
            tableLayoutPanel3.Size = new Size(413, 55);
            tableLayoutPanel3.TabIndex = 1;
            // 
            // flowHeaderUsuario
            // 
            flowHeaderUsuario.Controls.Add(lblNombreUsuario);
            flowHeaderUsuario.Controls.Add(lblUsuario);
            flowHeaderUsuario.FlowDirection = FlowDirection.RightToLeft;
            flowHeaderUsuario.Location = new Point(3, 11);
            flowHeaderUsuario.Name = "flowHeaderUsuario";
            flowHeaderUsuario.Size = new Size(407, 32);
            flowHeaderUsuario.TabIndex = 0;
            flowHeaderUsuario.WrapContents = false;
            // 
            // lblNombreUsuario
            // 
            lblNombreUsuario.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            lblNombreUsuario.AutoSize = true;
            lblNombreUsuario.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblNombreUsuario.Location = new Point(335, 0);
            lblNombreUsuario.Margin = new Padding(0);
            lblNombreUsuario.Name = "lblNombreUsuario";
            lblNombreUsuario.Size = new Size(72, 30);
            lblNombreUsuario.TabIndex = 25;
            lblNombreUsuario.Text = "label3";
            lblNombreUsuario.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblUsuario
            // 
            lblUsuario.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            lblUsuario.AutoSize = true;
            lblUsuario.Font = new Font("Segoe UI", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblUsuario.Location = new Point(155, 0);
            lblUsuario.Margin = new Padding(0, 0, 6, 0);
            lblUsuario.Name = "lblUsuario";
            lblUsuario.Size = new Size(174, 30);
            lblUsuario.TabIndex = 26;
            lblUsuario.Text = "Usuario Logeado:";
            lblUsuario.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // llbCerrarSesion
            // 
            llbCerrarSesion.Anchor = AnchorStyles.Right;
            llbCerrarSesion.AutoSize = true;
            llbCerrarSesion.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            llbCerrarSesion.Location = new Point(1160, 30);
            llbCerrarSesion.Name = "llbCerrarSesion";
            llbCerrarSesion.Size = new Size(76, 15);
            llbCerrarSesion.TabIndex = 28;
            llbCerrarSesion.TabStop = true;
            llbCerrarSesion.Text = "Cerrar Sesión";
            llbCerrarSesion.TextAlign = ContentAlignment.MiddleCenter;
            llbCerrarSesion.LinkClicked += llbCerrarSesion_LinkClicked;
            // 
            // tableLayoutPanel5
            // 
            tableLayoutPanel5.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tableLayoutPanel5.ColumnCount = 1;
            tableLayoutPanel5.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel5.Controls.Add(tableLayoutPanel6, 0, 0);
            tableLayoutPanel5.Location = new Point(23, 3);
            tableLayoutPanel5.Name = "tableLayoutPanel5";
            tableLayoutPanel5.RowCount = 1;
            tableLayoutPanel5.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel5.Size = new Size(674, 69);
            tableLayoutPanel5.TabIndex = 30;
            // 
            // tableLayoutPanel6
            // 
            tableLayoutPanel6.ColumnCount = 1;
            tableLayoutPanel6.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel6.Controls.Add(flowLayoutPanel2, 0, 1);
            tableLayoutPanel6.Dock = DockStyle.Left;
            tableLayoutPanel6.Location = new Point(3, 3);
            tableLayoutPanel6.Name = "tableLayoutPanel6";
            tableLayoutPanel6.RowCount = 3;
            tableLayoutPanel6.RowStyles.Add(new RowStyle(SizeType.Percent, 15F));
            tableLayoutPanel6.RowStyles.Add(new RowStyle(SizeType.Percent, 70F));
            tableLayoutPanel6.RowStyles.Add(new RowStyle(SizeType.Percent, 15F));
            tableLayoutPanel6.Size = new Size(668, 63);
            tableLayoutPanel6.TabIndex = 1;
            // 
            // flowLayoutPanel2
            // 
            flowLayoutPanel2.Anchor = AnchorStyles.Left;
            flowLayoutPanel2.AutoSize = true;
            flowLayoutPanel2.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            flowLayoutPanel2.Controls.Add(lblFecha);
            flowLayoutPanel2.Controls.Add(lblFechaValor);
            flowLayoutPanel2.Controls.Add(lblHora);
            flowLayoutPanel2.Controls.Add(lblHoraValor);
            flowLayoutPanel2.Location = new Point(3, 12);
            flowLayoutPanel2.Name = "flowLayoutPanel2";
            flowLayoutPanel2.Padding = new Padding(0, 8, 0, 0);
            flowLayoutPanel2.Size = new Size(393, 38);
            flowLayoutPanel2.TabIndex = 28;
            flowLayoutPanel2.WrapContents = false;
            // 
            // lblFecha
            // 
            lblFecha.Anchor = AnchorStyles.Left;
            lblFecha.AutoSize = true;
            lblFecha.Font = new Font("Segoe UI", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblFecha.Location = new Point(3, 8);
            lblFecha.Name = "lblFecha";
            lblFecha.Size = new Size(72, 30);
            lblFecha.TabIndex = 26;
            lblFecha.Text = "Fecha:";
            // 
            // lblFechaValor
            // 
            lblFechaValor.Anchor = AnchorStyles.Left;
            lblFechaValor.AutoSize = true;
            lblFechaValor.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblFechaValor.Location = new Point(81, 8);
            lblFechaValor.Name = "lblFechaValor";
            lblFechaValor.Size = new Size(127, 30);
            lblFechaValor.TabIndex = 25;
            lblFechaValor.Text = "00/00/0000";
            // 
            // lblHora
            // 
            lblHora.Anchor = AnchorStyles.Left;
            lblHora.AutoSize = true;
            lblHora.Font = new Font("Segoe UI", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblHora.ImageAlign = ContentAlignment.MiddleLeft;
            lblHora.Location = new Point(227, 8);
            lblHora.Margin = new Padding(16, 0, 0, 0);
            lblHora.Name = "lblHora";
            lblHora.Size = new Size(63, 30);
            lblHora.TabIndex = 27;
            lblHora.Text = "Hora:";
            // 
            // lblHoraValor
            // 
            lblHoraValor.Anchor = AnchorStyles.Left;
            lblHoraValor.AutoSize = true;
            lblHoraValor.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblHoraValor.ImageAlign = ContentAlignment.MiddleLeft;
            lblHoraValor.Location = new Point(293, 8);
            lblHoraValor.Name = "lblHoraValor";
            lblHoraValor.Size = new Size(97, 30);
            lblHoraValor.TabIndex = 25;
            lblHoraValor.Text = "00:00:00";
            // 
            // tlpPanelBaseTabControlYNotis
            // 
            tlpPanelBaseTabControlYNotis.BackColor = SystemColors.ButtonFace;
            tlpPanelBaseTabControlYNotis.ColumnCount = 2;
            tlpPanelBaseTabControlYNotis.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 62.5F));
            tlpPanelBaseTabControlYNotis.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 37.5F));
            tlpPanelBaseTabControlYNotis.Controls.Add(tcIzquierda, 0, 0);
            tlpPanelBaseTabControlYNotis.Controls.Add(tlpNotificaciones0, 1, 0);
            tlpPanelBaseTabControlYNotis.Dock = DockStyle.Fill;
            tlpPanelBaseTabControlYNotis.Location = new Point(3, 84);
            tlpPanelBaseTabControlYNotis.Name = "tlpPanelBaseTabControlYNotis";
            tlpPanelBaseTabControlYNotis.Padding = new Padding(20, 0, 20, 0);
            tlpPanelBaseTabControlYNotis.RowCount = 1;
            tlpPanelBaseTabControlYNotis.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpPanelBaseTabControlYNotis.Size = new Size(1259, 638);
            tlpPanelBaseTabControlYNotis.TabIndex = 19;
            // 
            // tcIzquierda
            // 
            tcIzquierda.Appearance = TabAppearance.FlatButtons;
            tcIzquierda.Controls.Add(tabPage1);
            tcIzquierda.Controls.Add(tabPage2);
            tcIzquierda.Dock = DockStyle.Fill;
            tcIzquierda.DrawMode = TabDrawMode.OwnerDrawFixed;
            tcIzquierda.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold);
            tcIzquierda.HeaderBackColor = Color.FromArgb(236, 236, 236);
            tcIzquierda.Location = new Point(23, 3);
            tcIzquierda.Name = "tcIzquierda";
            tcIzquierda.SelectedIndex = 0;
            tcIzquierda.Size = new Size(755, 632);
            tcIzquierda.SizeMode = TabSizeMode.Fixed;
            tcIzquierda.TabIndex = 0;
            // 
            // tabPage1
            // 
            tabPage1.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            tabPage1.Location = new Point(0, 25);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3);
            tabPage1.Size = new Size(755, 607);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "Acceso Rapido";
            tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            tabPage2.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold);
            tabPage2.Location = new Point(0, 25);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(3);
            tabPage2.Size = new Size(755, 607);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "Info Turno";
            tabPage2.UseVisualStyleBackColor = true;
            // 
            // tlpNotificaciones0
            // 
            tlpNotificaciones0.BackColor = SystemColors.ButtonFace;
            tlpNotificaciones0.ColumnCount = 1;
            tlpNotificaciones0.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpNotificaciones0.Controls.Add(flowLayoutNotificaciones, 0, 1);
            tlpNotificaciones0.Controls.Add(btnRefresh, 0, 0);
            tlpNotificaciones0.Dock = DockStyle.Fill;
            tlpNotificaciones0.Location = new Point(784, 3);
            tlpNotificaciones0.Name = "tlpNotificaciones0";
            tlpNotificaciones0.RowCount = 2;
            tlpNotificaciones0.RowStyles.Add(new RowStyle(SizeType.Absolute, 31F));
            tlpNotificaciones0.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpNotificaciones0.Size = new Size(452, 632);
            tlpNotificaciones0.TabIndex = 1;
            tlpNotificaciones0.Paint += tlpNotificaciones0_Paint;
            // 
            // flowLayoutNotificaciones
            // 
            flowLayoutNotificaciones.AutoScroll = true;
            flowLayoutNotificaciones.BackColor = SystemColors.ButtonFace;
            flowLayoutNotificaciones.Dock = DockStyle.Fill;
            flowLayoutNotificaciones.FlowDirection = FlowDirection.TopDown;
            flowLayoutNotificaciones.Location = new Point(3, 34);
            flowLayoutNotificaciones.Name = "flowLayoutNotificaciones";
            flowLayoutNotificaciones.Size = new Size(446, 595);
            flowLayoutNotificaciones.TabIndex = 0;
            flowLayoutNotificaciones.WrapContents = false;
            // 
            // btnRefresh
            // 
            btnRefresh.Dock = DockStyle.Right;
            btnRefresh.Location = new Point(304, 3);
            btnRefresh.Margin = new Padding(3, 3, 25, 3);
            btnRefresh.Name = "btnRefresh";
            btnRefresh.Size = new Size(123, 25);
            btnRefresh.TabIndex = 28;
            btnRefresh.Text = "RECARGAR ";
            btnRefresh.UseVisualStyleBackColor = true;
            btnRefresh.Click += btnRefresh_Click;
            // 
            // flowLayoutPanel3
            // 
            flowLayoutPanel3.AutoSize = true;
            flowLayoutPanel3.Dock = DockStyle.Fill;
            error.SetIconAlignment(flowLayoutPanel3, ErrorIconAlignment.MiddleLeft);
            flowLayoutPanel3.Location = new Point(907, 3);
            flowLayoutPanel3.Name = "flowLayoutPanel3";
            flowLayoutPanel3.Size = new Size(329, 51);
            flowLayoutPanel3.TabIndex = 29;
            flowLayoutPanel3.Visible = false;
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
            ClientSize = new Size(1265, 816);
            Controls.Add(panel1);
            ForeColor = Color.FromArgb(31, 26, 43);
            Icon = (Icon)resources.GetObject("$this.Icon");
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
            tableLayoutPanel2.PerformLayout();
            tableLayoutPanel4.ResumeLayout(false);
            tableLayoutPanel3.ResumeLayout(false);
            flowHeaderUsuario.ResumeLayout(false);
            flowHeaderUsuario.PerformLayout();
            tableLayoutPanel5.ResumeLayout(false);
            tableLayoutPanel6.ResumeLayout(false);
            tableLayoutPanel6.PerformLayout();
            flowLayoutPanel2.ResumeLayout(false);
            flowLayoutPanel2.PerformLayout();
            tlpPanelBaseTabControlYNotis.ResumeLayout(false);
            tcIzquierda.ResumeLayout(false);
            tlpNotificaciones0.ResumeLayout(false);
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
        private FlowLayoutPanel flowLayoutPanel2;
        private Label lblFechaValor;
        private Label lblFecha;
        private TableLayoutPanel tableLayoutPanel2;
        private TableLayoutPanel tlpPanelBaseTabControlYNotis;
        private FlatTabControl tcIzquierda;
        private TabPage tabPage1;
        private TabPage tabPage2;
        private TableLayoutPanel tlpNotificaciones0;
        private FlowLayoutPanel flowLayoutNotificaciones;
        private TableLayoutPanel tableLayoutPanel3;
        private Button btnRefresh;
        private LinkLabel llbCerrarSesion;
        private FlowLayoutPanel flowHeaderUsuario;
        private Label lblUsuario;
        private Label lblNombreUsuario;
        private TableLayoutPanel tableLayoutPanel4;
        private TableLayoutPanel tableLayoutPanel5;
        private TableLayoutPanel tableLayoutPanel6;
    }
}
