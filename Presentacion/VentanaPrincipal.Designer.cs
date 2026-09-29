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
            tblUsuario = new TableLayoutPanel();
            btnCerarSesion = new Button();
            flpUsuarioLogeado = new FlowLayoutPanel();
            lblNombreUsuario = new Label();
            lblUsuario = new Label();
            tableLayoutPanel5 = new TableLayoutPanel();
            tlpTopLeft = new TableLayoutPanel();
            flpDatosFechaHora = new FlowLayoutPanel();
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
            tblUsuario.SuspendLayout();
            flpUsuarioLogeado.SuspendLayout();
            tableLayoutPanel5.SuspendLayout();
            tlpTopLeft.SuspendLayout();
            flpDatosFechaHora.SuspendLayout();
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
            tableLayoutPanel2.ColumnCount = 2;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 55.85366F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 34.94668F));
            tableLayoutPanel2.Controls.Add(tableLayoutPanel4, 1, 0);
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
            tableLayoutPanel4.Controls.Add(tblUsuario, 0, 0);
            tableLayoutPanel4.Location = new Point(772, 3);
            tableLayoutPanel4.Name = "tableLayoutPanel4";
            tableLayoutPanel4.RowCount = 2;
            tableLayoutPanel4.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel4.RowStyles.Add(new RowStyle(SizeType.Absolute, 8F));
            tableLayoutPanel4.Size = new Size(464, 69);
            tableLayoutPanel4.TabIndex = 29;
            // 
            // tblUsuario
            // 
            tblUsuario.ColumnCount = 2;
            tblUsuario.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 66.6666641F));
            tblUsuario.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333359F));
            tblUsuario.Controls.Add(btnCerarSesion, 1, 1);
            tblUsuario.Controls.Add(flpUsuarioLogeado, 0, 1);
            tblUsuario.Dock = DockStyle.Fill;
            tblUsuario.Location = new Point(3, 3);
            tblUsuario.Name = "tblUsuario";
            tblUsuario.RowCount = 2;
            tblUsuario.RowStyles.Add(new RowStyle(SizeType.Percent, 10.4712086F));
            tblUsuario.RowStyles.Add(new RowStyle(SizeType.Percent, 89.52879F));
            tblUsuario.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tblUsuario.Size = new Size(458, 55);
            tblUsuario.TabIndex = 1;
            // 
            // btnCerarSesion
            // 
            btnCerarSesion.Anchor = AnchorStyles.None;
            btnCerarSesion.Location = new Point(321, 14);
            btnCerarSesion.Name = "btnCerarSesion";
            btnCerarSesion.Size = new Size(121, 31);
            btnCerarSesion.TabIndex = 29;
            btnCerarSesion.Text = "Cerrar Sesión";
            btnCerarSesion.UseVisualStyleBackColor = true;
            btnCerarSesion.Click += btnCerarSesion_Click;
            // 
            // flpUsuarioLogeado
            // 
            flpUsuarioLogeado.Anchor = AnchorStyles.Right;
            flpUsuarioLogeado.Controls.Add(lblNombreUsuario);
            flpUsuarioLogeado.Controls.Add(lblUsuario);
            flpUsuarioLogeado.FlowDirection = FlowDirection.RightToLeft;
            flpUsuarioLogeado.Location = new Point(3, 14);
            flpUsuarioLogeado.Name = "flpUsuarioLogeado";
            flpUsuarioLogeado.Size = new Size(299, 31);
            flpUsuarioLogeado.TabIndex = 0;
            flpUsuarioLogeado.WrapContents = false;
            // 
            // lblNombreUsuario
            // 
            lblNombreUsuario.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            lblNombreUsuario.AutoSize = true;
            lblNombreUsuario.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblNombreUsuario.Location = new Point(227, 0);
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
            lblUsuario.Location = new Point(47, 0);
            lblUsuario.Margin = new Padding(0, 0, 6, 0);
            lblUsuario.Name = "lblUsuario";
            lblUsuario.Size = new Size(174, 30);
            lblUsuario.TabIndex = 26;
            lblUsuario.Text = "Usuario Logeado:";
            lblUsuario.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // tableLayoutPanel5
            // 
            tableLayoutPanel5.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tableLayoutPanel5.ColumnCount = 1;
            tableLayoutPanel5.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel5.Controls.Add(tlpTopLeft, 0, 0);
            tableLayoutPanel5.Location = new Point(23, 3);
            tableLayoutPanel5.Name = "tableLayoutPanel5";
            tableLayoutPanel5.RowCount = 1;
            tableLayoutPanel5.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel5.Size = new Size(743, 69);
            tableLayoutPanel5.TabIndex = 30;
            // 
            // tlpTopLeft
            // 
            tlpTopLeft.ColumnCount = 1;
            tlpTopLeft.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpTopLeft.Controls.Add(flpDatosFechaHora, 0, 0);
            tlpTopLeft.Dock = DockStyle.Left;
            tlpTopLeft.Location = new Point(3, 3);
            tlpTopLeft.Name = "tlpTopLeft";
            tlpTopLeft.RowCount = 2;
            tlpTopLeft.RowStyles.Add(new RowStyle(SizeType.Percent, 85F));
            tlpTopLeft.RowStyles.Add(new RowStyle(SizeType.Percent, 15F));
            tlpTopLeft.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tlpTopLeft.Size = new Size(668, 63);
            tlpTopLeft.TabIndex = 1;
            // 
            // flpDatosFechaHora
            // 
            flpDatosFechaHora.Anchor = AnchorStyles.Left;
            flpDatosFechaHora.AutoSize = true;
            flpDatosFechaHora.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            flpDatosFechaHora.Controls.Add(lblFecha);
            flpDatosFechaHora.Controls.Add(lblFechaValor);
            flpDatosFechaHora.Controls.Add(lblHora);
            flpDatosFechaHora.Controls.Add(lblHoraValor);
            flpDatosFechaHora.Location = new Point(3, 7);
            flpDatosFechaHora.Name = "flpDatosFechaHora";
            flpDatosFechaHora.Padding = new Padding(0, 8, 0, 0);
            flpDatosFechaHora.Size = new Size(393, 38);
            flpDatosFechaHora.TabIndex = 28;
            flpDatosFechaHora.WrapContents = false;
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
            tableLayoutPanel4.ResumeLayout(false);
            tblUsuario.ResumeLayout(false);
            flpUsuarioLogeado.ResumeLayout(false);
            flpUsuarioLogeado.PerformLayout();
            tableLayoutPanel5.ResumeLayout(false);
            tlpTopLeft.ResumeLayout(false);
            tlpTopLeft.PerformLayout();
            flpDatosFechaHora.ResumeLayout(false);
            flpDatosFechaHora.PerformLayout();
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
        private FlowLayoutPanel flpDatosFechaHora;
        private Label lblFechaValor;
        private Label lblFecha;
        private TableLayoutPanel tableLayoutPanel2;
        private TableLayoutPanel tlpPanelBaseTabControlYNotis;
        private FlatTabControl tcIzquierda;
        private TabPage tabPage1;
        private TabPage tabPage2;
        private TableLayoutPanel tlpNotificaciones0;
        private FlowLayoutPanel flowLayoutNotificaciones;
        private TableLayoutPanel tblUsuario;
        private Button btnRefresh;
        private FlowLayoutPanel flpUsuarioLogeado;
        private Label lblUsuario;
        private Label lblNombreUsuario;
        private TableLayoutPanel tableLayoutPanel4;
        private TableLayoutPanel tableLayoutPanel5;
        private TableLayoutPanel tlpTopLeft;
        private Button btnCerarSesion;
    }
}
