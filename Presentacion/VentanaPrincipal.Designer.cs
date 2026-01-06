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
            dgvGrilla = new DataGridView();
            PnlBotones = new Panel();
            tableLayoutPanel1 = new TableLayoutPanel();
            btnPanelAdmin = new Button();
            btnVenta = new Button();
            btnCaja = new Button();
            btnContraVenta = new Button();
            pnlInfoInicial = new Panel();
            label2 = new Label();
            pnlTextos = new Panel();
            label1 = new Label();
            panel1 = new Panel();
            lblNombreUsuario = new Label();
            lblUsuario = new Label();
            flowLayoutPanel1 = new FlowLayoutPanel();
            tableLayoutPanel2 = new TableLayoutPanel();
            flowLayoutPanel2 = new FlowLayoutPanel();
            lblFechaValor = new Label();
            lblFecha = new Label();
            flowLayoutPanel3 = new FlowLayoutPanel();
            lblHora = new Label();
            lblHoraValor = new Label();
            ((System.ComponentModel.ISupportInitialize)error).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvGrilla).BeginInit();
            PnlBotones.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            pnlInfoInicial.SuspendLayout();
            pnlTextos.SuspendLayout();
            panel1.SuspendLayout();
            flowLayoutPanel1.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            flowLayoutPanel2.SuspendLayout();
            flowLayoutPanel3.SuspendLayout();
            SuspendLayout();
            // 
            // dgvGrilla
            // 
            dgvGrilla.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            dgvGrilla.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvGrilla.Location = new Point(679, -3);
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
            PnlBotones.Size = new Size(1044, 82);
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
            tableLayoutPanel1.Dock = DockStyle.Top;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 1;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Size = new Size(1044, 94);
            tableLayoutPanel1.TabIndex = 6;
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
            // btnVenta
            // 
            btnVenta.Anchor = AnchorStyles.Right;
            btnVenta.Location = new Point(805, 15);
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
            btnCaja.Location = new Point(283, 15);
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
            btnContraVenta.Location = new Point(525, 15);
            btnContraVenta.MaximumSize = new Size(236, 63);
            btnContraVenta.MinimumSize = new Size(236, 63);
            btnContraVenta.Name = "btnContraVenta";
            btnContraVenta.Size = new Size(236, 63);
            btnContraVenta.TabIndex = 16;
            btnContraVenta.Text = "DEVOLUCIÓN / CONTRAASIENTO";
            btnContraVenta.UseVisualStyleBackColor = true;
            btnContraVenta.Click += btnContraVenta_Click;
            // 
            // pnlInfoInicial
            // 
            pnlInfoInicial.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            pnlInfoInicial.BackColor = SystemColors.ActiveCaption;
            pnlInfoInicial.Controls.Add(label2);
            pnlInfoInicial.Controls.Add(pnlTextos);
            pnlInfoInicial.Controls.Add(dgvGrilla);
            pnlInfoInicial.Location = new Point(-18, 3);
            pnlInfoInicial.Name = "pnlInfoInicial";
            pnlInfoInicial.Size = new Size(1062, 455);
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
            pnlTextos.Size = new Size(670, 449);
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
            panel1.Size = new Size(1047, 580);
            panel1.TabIndex = 24;
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
            // flowLayoutPanel1
            // 
            flowLayoutPanel1.Controls.Add(lblUsuario);
            flowLayoutPanel1.Controls.Add(lblNombreUsuario);
            flowLayoutPanel1.Location = new Point(3, 3);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            flowLayoutPanel1.Size = new Size(328, 38);
            flowLayoutPanel1.TabIndex = 27;
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            tableLayoutPanel2.ColumnCount = 3;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
            tableLayoutPanel2.Controls.Add(flowLayoutPanel1, 0, 0);
            tableLayoutPanel2.Controls.Add(flowLayoutPanel2, 1, 0);
            tableLayoutPanel2.Controls.Add(flowLayoutPanel3, 2, 0);
            tableLayoutPanel2.Location = new Point(33, 12);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 1;
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel2.Size = new Size(1047, 44);
            tableLayoutPanel2.TabIndex = 1;
            // 
            // flowLayoutPanel2
            // 
            flowLayoutPanel2.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            flowLayoutPanel2.Controls.Add(lblFechaValor);
            flowLayoutPanel2.Controls.Add(lblFecha);
            flowLayoutPanel2.FlowDirection = FlowDirection.RightToLeft;
            flowLayoutPanel2.Location = new Point(367, 3);
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
            flowLayoutPanel3.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            flowLayoutPanel3.Controls.Add(lblHora);
            flowLayoutPanel3.Controls.Add(lblHoraValor);
            error.SetIconAlignment(flowLayoutPanel3, ErrorIconAlignment.MiddleLeft);
            flowLayoutPanel3.Location = new Point(715, 3);
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
            // VentanaPrincipal
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            ClientSize = new Size(1089, 693);
            Controls.Add(tableLayoutPanel2);
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
            ((System.ComponentModel.ISupportInitialize)dgvGrilla).EndInit();
            PnlBotones.ResumeLayout(false);
            tableLayoutPanel1.ResumeLayout(false);
            pnlInfoInicial.ResumeLayout(false);
            pnlInfoInicial.PerformLayout();
            pnlTextos.ResumeLayout(false);
            pnlTextos.PerformLayout();
            panel1.ResumeLayout(false);
            flowLayoutPanel1.ResumeLayout(false);
            flowLayoutPanel1.PerformLayout();
            tableLayoutPanel2.ResumeLayout(false);
            flowLayoutPanel2.ResumeLayout(false);
            flowLayoutPanel2.PerformLayout();
            flowLayoutPanel3.ResumeLayout(false);
            flowLayoutPanel3.PerformLayout();
            ResumeLayout(false);
        }

        #endregion
        private DataGridView dgvGrilla;
        private Panel PnlBotones;
        private Panel pnlInfoInicial;
        private Panel pnlTextos;
        private Panel panel1;
        private TableLayoutPanel tableLayoutPanel1;
        private Button btnVenta;
        private Button btnContraVenta;
        private Button btnPanelAdmin;
        private Button btnCaja;
        private Label label2;
        private Label label1;
        private Label lblNombreUsuario;
        private Label lblUsuario;
        private FlowLayoutPanel flowLayoutPanel1;
        private TableLayoutPanel tableLayoutPanel2;
        private FlowLayoutPanel flowLayoutPanel2;
        private Label lblFecha;
        private Label lblFechaValor;
        private FlowLayoutPanel flowLayoutPanel3;
        private Label lblHoraValor;
        private Label lblHora;
    }
}
