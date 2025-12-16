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
            lblUsuarioLogueado = new Label();
            lblNombreUsuario = new Label();
            dgvGrilla = new DataGridView();
            PnlBotones = new Panel();
            tableLayoutPanel1 = new TableLayoutPanel();
            btnVenta = new Button();
            btnMovimientos = new Button();
            btnPanelAdmin = new Button();
            btnCaja = new Button();
            lblFecha = new Label();
            lblHora = new Label();
            lblFechaValor = new Label();
            lblHoraValor = new Label();
            pnlInfoInicial = new Panel();
            label2 = new Label();
            pnlTextos = new Panel();
            label1 = new Label();
            panel1 = new Panel();
            ((System.ComponentModel.ISupportInitialize)error).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvGrilla).BeginInit();
            PnlBotones.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            pnlInfoInicial.SuspendLayout();
            pnlTextos.SuspendLayout();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // lblUsuarioLogueado
            // 
            lblUsuarioLogueado.AutoSize = true;
            lblUsuarioLogueado.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblUsuarioLogueado.Location = new Point(125, 37);
            lblUsuarioLogueado.Name = "lblUsuarioLogueado";
            lblUsuarioLogueado.Size = new Size(144, 21);
            lblUsuarioLogueado.TabIndex = 2;
            lblUsuarioLogueado.Text = "Usuario Logueado :";
            // 
            // lblNombreUsuario
            // 
            lblNombreUsuario.AutoSize = true;
            lblNombreUsuario.Font = new Font("Segoe UI", 11.25F);
            lblNombreUsuario.Location = new Point(269, 38);
            lblNombreUsuario.Name = "lblNombreUsuario";
            lblNombreUsuario.Size = new Size(0, 20);
            lblNombreUsuario.TabIndex = 3;
            // 
            // dgvGrilla
            // 
            dgvGrilla.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            dgvGrilla.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvGrilla.Location = new Point(614, -3);
            dgvGrilla.Name = "dgvGrilla";
            dgvGrilla.Size = new Size(380, 455);
            dgvGrilla.TabIndex = 17;
            // 
            // PnlBotones
            // 
            PnlBotones.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            PnlBotones.Controls.Add(tableLayoutPanel1);
            PnlBotones.Location = new Point(0, 495);
            PnlBotones.Name = "PnlBotones";
            PnlBotones.Size = new Size(1000, 82);
            PnlBotones.TabIndex = 18;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 4;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tableLayoutPanel1.Controls.Add(btnVenta, 1, 0);
            tableLayoutPanel1.Controls.Add(btnMovimientos, 2, 0);
            tableLayoutPanel1.Controls.Add(btnPanelAdmin, 0, 0);
            tableLayoutPanel1.Controls.Add(btnCaja, 3, 0);
            tableLayoutPanel1.Dock = DockStyle.Top;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 1;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Size = new Size(1000, 94);
            tableLayoutPanel1.TabIndex = 6;
            // 
            // btnVenta
            // 
            btnVenta.Anchor = AnchorStyles.Left;
            btnVenta.Location = new Point(253, 15);
            btnVenta.MaximumSize = new Size(236, 63);
            btnVenta.MinimumSize = new Size(236, 63);
            btnVenta.Name = "btnVenta";
            btnVenta.Size = new Size(236, 63);
            btnVenta.TabIndex = 12;
            btnVenta.Text = "VENTA";
            btnVenta.UseVisualStyleBackColor = true;
            // 
            // btnMovimientos
            // 
            btnMovimientos.Anchor = AnchorStyles.Right;
            btnMovimientos.Location = new Point(511, 15);
            btnMovimientos.MaximumSize = new Size(236, 63);
            btnMovimientos.MinimumSize = new Size(236, 63);
            btnMovimientos.Name = "btnMovimientos";
            btnMovimientos.Size = new Size(236, 63);
            btnMovimientos.TabIndex = 16;
            btnMovimientos.Text = "MOVIMIENTOS";
            btnMovimientos.UseVisualStyleBackColor = true;
            // 
            // btnPanelAdmin
            // 
            btnPanelAdmin.Anchor = AnchorStyles.Left;
            btnPanelAdmin.Location = new Point(3, 15);
            btnPanelAdmin.MaximumSize = new Size(236, 63);
            btnPanelAdmin.MinimumSize = new Size(236, 63);
            btnPanelAdmin.Name = "btnPanelAdmin";
            btnPanelAdmin.Size = new Size(236, 63);
            btnPanelAdmin.TabIndex = 15;
            btnPanelAdmin.Text = "PANEL ADMINISTRACIÓN";
            btnPanelAdmin.UseVisualStyleBackColor = true;
            btnPanelAdmin.Click += btnPanelAdmin_Click;
            // 
            // btnCaja
            // 
            btnCaja.Anchor = AnchorStyles.Right;
            btnCaja.Location = new Point(761, 15);
            btnCaja.MaximumSize = new Size(236, 63);
            btnCaja.MinimumSize = new Size(236, 63);
            btnCaja.Name = "btnCaja";
            btnCaja.Size = new Size(236, 63);
            btnCaja.TabIndex = 13;
            btnCaja.Text = "CAJA";
            btnCaja.UseVisualStyleBackColor = true;
            // 
            // lblFecha
            // 
            lblFecha.AutoSize = true;
            lblFecha.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblFecha.Location = new Point(587, 36);
            lblFecha.Name = "lblFecha";
            lblFecha.Size = new Size(57, 21);
            lblFecha.TabIndex = 19;
            lblFecha.Text = "Fecha :";
            // 
            // lblHora
            // 
            lblHora.AutoSize = true;
            lblHora.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblHora.Location = new Point(795, 36);
            lblHora.Name = "lblHora";
            lblHora.Size = new Size(51, 21);
            lblHora.TabIndex = 20;
            lblHora.Text = "Hora :";
            // 
            // lblFechaValor
            // 
            lblFechaValor.AutoSize = true;
            lblFechaValor.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblFechaValor.Location = new Point(640, 34);
            lblFechaValor.Name = "lblFechaValor";
            lblFechaValor.Size = new Size(94, 25);
            lblFechaValor.TabIndex = 21;
            lblFechaValor.Text = "00/00/00";
            // 
            // lblHoraValor
            // 
            lblHoraValor.AutoSize = true;
            lblHoraValor.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblHoraValor.Location = new Point(841, 33);
            lblHoraValor.Name = "lblHoraValor";
            lblHoraValor.Size = new Size(88, 25);
            lblHoraValor.TabIndex = 22;
            lblHoraValor.Text = "00:00:00";
            // 
            // pnlInfoInicial
            // 
            pnlInfoInicial.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            pnlInfoInicial.BackColor = SystemColors.ActiveCaption;
            pnlInfoInicial.Controls.Add(label2);
            pnlInfoInicial.Controls.Add(pnlTextos);
            pnlInfoInicial.Controls.Add(dgvGrilla);
            pnlInfoInicial.Location = new Point(3, 3);
            pnlInfoInicial.Name = "pnlInfoInicial";
            pnlInfoInicial.Size = new Size(997, 455);
            pnlInfoInicial.TabIndex = 23;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = SystemColors.ButtonFace;
            label2.Location = new Point(663, 65);
            label2.Name = "label2";
            label2.Size = new Size(170, 15);
            label2.TabIndex = 1;
            label2.Text = "Grilla de deudores o ver de que";
            // 
            // pnlTextos
            // 
            pnlTextos.AllowDrop = true;
            pnlTextos.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            pnlTextos.BackColor = SystemColors.ActiveCaptionText;
            pnlTextos.Controls.Add(label1);
            pnlTextos.Location = new Point(3, 3);
            pnlTextos.Name = "pnlTextos";
            pnlTextos.Size = new Size(605, 449);
            pnlTextos.TabIndex = 18;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = SystemColors.ButtonFace;
            label1.Location = new Point(122, 85);
            label1.Name = "label1";
            label1.Size = new Size(180, 15);
            label1.TabIndex = 0;
            label1.Text = "Contenedor de texto informativo";
            // 
            // panel1
            // 
            panel1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            panel1.Controls.Add(PnlBotones);
            panel1.Controls.Add(pnlInfoInicial);
            panel1.Location = new Point(30, 62);
            panel1.Name = "panel1";
            panel1.Size = new Size(1003, 580);
            panel1.TabIndex = 24;
            // 
            // VentanaPrincipal
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1045, 693);
            Controls.Add(panel1);
            Controls.Add(lblHoraValor);
            Controls.Add(lblFechaValor);
            Controls.Add(lblHora);
            Controls.Add(lblFecha);
            Controls.Add(lblNombreUsuario);
            Controls.Add(lblUsuarioLogueado);
            MinimizeBox = true;
            MinimumSize = new Size(1061, 732);
            Name = "VentanaPrincipal";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Inicio";
            WindowState = FormWindowState.Maximized;
            Load += VentanaPrincipal_Load;
            ((System.ComponentModel.ISupportInitialize)error).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvGrilla).EndInit();
            PnlBotones.ResumeLayout(false);
            tableLayoutPanel1.ResumeLayout(false);
            pnlInfoInicial.ResumeLayout(false);
            pnlInfoInicial.PerformLayout();
            pnlTextos.ResumeLayout(false);
            pnlTextos.PerformLayout();
            panel1.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Label lblUsuarioLogueado;
        private Label lblNombreUsuario;
        private DataGridView dgvGrilla;
        private Panel PnlBotones;
        private Label lblFecha;
        private Label lblHora;
        private Label lblFechaValor;
        private Label lblHoraValor;
        private Panel pnlInfoInicial;
        private Panel pnlTextos;
        private Panel panel1;
        private TableLayoutPanel tableLayoutPanel1;
        private Button btnVenta;
        private Button btnMovimientos;
        private Button btnPanelAdmin;
        private Button btnCaja;
        private Label label2;
        private Label label1;
    }
}
