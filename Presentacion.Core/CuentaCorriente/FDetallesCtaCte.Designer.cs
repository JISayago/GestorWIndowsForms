namespace Presentacion.Core.CuentaCorriente
{
    partial class FDetallesCtaCte
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
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FDetallesCtaCte));
            lblNombreCliente = new Label();
            lblCliente = new Label();
            dgvGrilla = new DataGridView();
            lblListadoMovimientos = new Label();
            tableLayoutPanel10 = new TableLayoutPanel();
            tableLayoutPanel9 = new TableLayoutPanel();
            tableLayoutPanel11 = new TableLayoutPanel();
            lblPagina = new Label();
            btnAnterior = new Button();
            btnSiguiente = new Button();
            lblTotalRegistros = new Label();
            pbxLogo = new PictureBox();
            lblSaldoCuenta = new Label();
            label2 = new Label();
            lblFechaCreacion = new Label();
            label5 = new Label();
            lblNombreCuenta = new Label();
            label7 = new Label();
            label8 = new Label();
            lblFechaVto = new Label();
            label10 = new Label();
            lstDnis = new ListBox();
            lblDetallesExtra = new Label();
            lblEstado = new Label();
            label4 = new Label();
            ((System.ComponentModel.ISupportInitialize)error).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvGrilla).BeginInit();
            tableLayoutPanel10.SuspendLayout();
            tableLayoutPanel9.SuspendLayout();
            tableLayoutPanel11.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pbxLogo).BeginInit();
            SuspendLayout();
            // 
            // lblNombreCliente
            // 
            lblNombreCliente.AutoSize = true;
            lblNombreCliente.Font = new Font("Segoe UI Semibold", 18F, FontStyle.Bold);
            lblNombreCliente.Location = new Point(128, 9);
            lblNombreCliente.Name = "lblNombreCliente";
            lblNombreCliente.Size = new Size(114, 32);
            lblNombreCliente.TabIndex = 23;
            lblNombreCliente.Tag = "NoModificarConBase";
            lblNombreCliente.Text = "**********";
            // 
            // lblCliente
            // 
            lblCliente.AutoSize = true;
            lblCliente.Font = new Font("Segoe UI Semibold", 18F, FontStyle.Bold);
            lblCliente.Location = new Point(26, 9);
            lblCliente.Name = "lblCliente";
            lblCliente.Size = new Size(96, 32);
            lblCliente.TabIndex = 22;
            lblCliente.Tag = "NoModificarConBase";
            lblCliente.Text = "Cliente:";
            // 
            // dgvGrilla
            // 
            dgvGrilla.AllowUserToAddRows = false;
            dgvGrilla.AllowUserToDeleteRows = false;
            dgvGrilla.Anchor = AnchorStyles.None;
            dgvGrilla.BackgroundColor = SystemColors.ButtonFace;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = SystemColors.Control;
            dataGridViewCellStyle1.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            dataGridViewCellStyle1.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            dgvGrilla.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            dgvGrilla.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = SystemColors.Window;
            dataGridViewCellStyle2.Font = new Font("Segoe UI Semibold", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            dataGridViewCellStyle2.ForeColor = Color.FromArgb(31, 26, 43);
            dataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.False;
            dgvGrilla.DefaultCellStyle = dataGridViewCellStyle2;
            dgvGrilla.EditMode = DataGridViewEditMode.EditProgrammatically;
            dgvGrilla.Location = new Point(12, 278);
            dgvGrilla.Name = "dgvGrilla";
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = SystemColors.Control;
            dataGridViewCellStyle3.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dataGridViewCellStyle3.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.True;
            dgvGrilla.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            dgvGrilla.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.AutoSizeToDisplayedHeaders;
            dataGridViewCellStyle4.BackColor = Color.White;
            dataGridViewCellStyle4.Padding = new Padding(2, 4, 2, 4);
            dgvGrilla.RowsDefaultCellStyle = dataGridViewCellStyle4;
            dgvGrilla.RowTemplate.Height = 40;
            dgvGrilla.Size = new Size(569, 228);
            dgvGrilla.TabIndex = 24;
            // 
            // lblListadoMovimientos
            // 
            lblListadoMovimientos.AutoSize = true;
            lblListadoMovimientos.Font = new Font("Segoe UI Semibold", 18F, FontStyle.Bold);
            lblListadoMovimientos.Location = new Point(12, 234);
            lblListadoMovimientos.Name = "lblListadoMovimientos";
            lblListadoMovimientos.Size = new Size(155, 32);
            lblListadoMovimientos.TabIndex = 25;
            lblListadoMovimientos.Tag = "NoModificarConBase";
            lblListadoMovimientos.Text = "Movimientos";
            // 
            // tableLayoutPanel10
            // 
            tableLayoutPanel10.Anchor = AnchorStyles.None;
            tableLayoutPanel10.ColumnCount = 2;
            tableLayoutPanel10.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
            tableLayoutPanel10.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 66.66667F));
            tableLayoutPanel10.Controls.Add(tableLayoutPanel9, 1, 0);
            tableLayoutPanel10.Controls.Add(pbxLogo, 0, 0);
            tableLayoutPanel10.Location = new Point(12, 512);
            tableLayoutPanel10.Name = "tableLayoutPanel10";
            tableLayoutPanel10.RowCount = 1;
            tableLayoutPanel10.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel10.Size = new Size(569, 58);
            tableLayoutPanel10.TabIndex = 26;
            // 
            // tableLayoutPanel9
            // 
            tableLayoutPanel9.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tableLayoutPanel9.ColumnCount = 2;
            tableLayoutPanel9.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40F));
            tableLayoutPanel9.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 60F));
            tableLayoutPanel9.Controls.Add(tableLayoutPanel11, 1, 0);
            tableLayoutPanel9.Controls.Add(lblTotalRegistros, 0, 0);
            tableLayoutPanel9.Location = new Point(192, 3);
            tableLayoutPanel9.Name = "tableLayoutPanel9";
            tableLayoutPanel9.RowCount = 1;
            tableLayoutPanel9.RowStyles.Add(new RowStyle(SizeType.Percent, 70F));
            tableLayoutPanel9.Size = new Size(374, 52);
            tableLayoutPanel9.TabIndex = 9;
            // 
            // tableLayoutPanel11
            // 
            tableLayoutPanel11.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tableLayoutPanel11.ColumnCount = 3;
            tableLayoutPanel11.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tableLayoutPanel11.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel11.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tableLayoutPanel11.Controls.Add(lblPagina, 1, 0);
            tableLayoutPanel11.Controls.Add(btnAnterior, 0, 0);
            tableLayoutPanel11.Controls.Add(btnSiguiente, 2, 0);
            tableLayoutPanel11.Location = new Point(152, 3);
            tableLayoutPanel11.Name = "tableLayoutPanel11";
            tableLayoutPanel11.RowCount = 1;
            tableLayoutPanel11.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel11.Size = new Size(219, 46);
            tableLayoutPanel11.TabIndex = 7;
            // 
            // lblPagina
            // 
            lblPagina.Anchor = AnchorStyles.None;
            lblPagina.AutoSize = true;
            lblPagina.Font = new Font("Segoe UI Black", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblPagina.Location = new Point(74, 10);
            lblPagina.Name = "lblPagina";
            lblPagina.Size = new Size(69, 25);
            lblPagina.TabIndex = 1;
            lblPagina.Text = "label2";
            lblPagina.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // btnAnterior
            // 
            btnAnterior.Anchor = AnchorStyles.None;
            btnAnterior.Font = new Font("Segoe UI Black", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnAnterior.Location = new Point(3, 3);
            btnAnterior.Name = "btnAnterior";
            btnAnterior.Size = new Size(48, 40);
            btnAnterior.TabIndex = 2;
            btnAnterior.Text = "<";
            btnAnterior.UseVisualStyleBackColor = true;
            btnAnterior.Click += btnAnterior_Click;
            // 
            // btnSiguiente
            // 
            btnSiguiente.Anchor = AnchorStyles.None;
            btnSiguiente.Font = new Font("Segoe UI Black", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnSiguiente.Location = new Point(166, 3);
            btnSiguiente.Name = "btnSiguiente";
            btnSiguiente.Size = new Size(50, 40);
            btnSiguiente.TabIndex = 7;
            btnSiguiente.Text = ">";
            btnSiguiente.UseVisualStyleBackColor = true;
            btnSiguiente.Click += btnSiguiente_Click;
            // 
            // lblTotalRegistros
            // 
            lblTotalRegistros.Anchor = AnchorStyles.None;
            lblTotalRegistros.AutoSize = true;
            lblTotalRegistros.Font = new Font("Segoe UI Black", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTotalRegistros.Location = new Point(41, 13);
            lblTotalRegistros.Name = "lblTotalRegistros";
            lblTotalRegistros.Size = new Size(67, 25);
            lblTotalRegistros.TabIndex = 0;
            lblTotalRegistros.Text = "label1";
            lblTotalRegistros.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // pbxLogo
            // 
            pbxLogo.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            pbxLogo.Location = new Point(80, 3);
            pbxLogo.Margin = new Padding(80, 3, 3, 3);
            pbxLogo.Name = "pbxLogo";
            pbxLogo.Size = new Size(106, 52);
            pbxLogo.SizeMode = PictureBoxSizeMode.Zoom;
            pbxLogo.TabIndex = 8;
            pbxLogo.TabStop = false;
            // 
            // lblSaldoCuenta
            // 
            lblSaldoCuenta.AutoSize = true;
            lblSaldoCuenta.Font = new Font("Segoe UI Semibold", 18F, FontStyle.Bold);
            lblSaldoCuenta.Location = new Point(449, 234);
            lblSaldoCuenta.Name = "lblSaldoCuenta";
            lblSaldoCuenta.Size = new Size(40, 32);
            lblSaldoCuenta.TabIndex = 28;
            lblSaldoCuenta.Tag = "NoModificarConBase";
            lblSaldoCuenta.Text = "$0";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI Semibold", 18F, FontStyle.Bold);
            label2.Location = new Point(340, 234);
            label2.Name = "label2";
            label2.Size = new Size(80, 32);
            label2.TabIndex = 27;
            label2.Tag = "NoModificarConBase";
            label2.Text = "Saldo:";
            // 
            // lblFechaCreacion
            // 
            lblFechaCreacion.AutoSize = true;
            lblFechaCreacion.Font = new Font("Segoe UI Semibold", 18F, FontStyle.Bold);
            lblFechaCreacion.Location = new Point(688, 74);
            lblFechaCreacion.Name = "lblFechaCreacion";
            lblFechaCreacion.Size = new Size(135, 32);
            lblFechaCreacion.TabIndex = 34;
            lblFechaCreacion.Tag = "NoModificarConBase";
            lblFechaCreacion.Text = "01/08/2026";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI Semibold", 18F, FontStyle.Bold);
            label5.Location = new Point(497, 74);
            label5.Name = "label5";
            label5.Size = new Size(185, 32);
            label5.TabIndex = 33;
            label5.Tag = "NoModificarConBase";
            label5.Text = "Fecha Creación:";
            // 
            // lblNombreCuenta
            // 
            lblNombreCuenta.AutoSize = true;
            lblNombreCuenta.Font = new Font("Segoe UI Semibold", 18F, FontStyle.Bold);
            lblNombreCuenta.Location = new Point(135, 74);
            lblNombreCuenta.Name = "lblNombreCuenta";
            lblNombreCuenta.Size = new Size(114, 32);
            lblNombreCuenta.TabIndex = 36;
            lblNombreCuenta.Tag = "NoModificarConBase";
            lblNombreCuenta.Text = "**********";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Segoe UI Semibold", 18F, FontStyle.Bold);
            label7.Location = new Point(26, 74);
            label7.Name = "label7";
            label7.Size = new Size(98, 32);
            label7.TabIndex = 35;
            label7.Tag = "NoModificarConBase";
            label7.Text = "Cuenta:";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new Font("Segoe UI Semibold", 18F, FontStyle.Bold);
            label8.Location = new Point(430, 116);
            label8.Name = "label8";
            label8.Size = new Size(251, 32);
            label8.TabIndex = 37;
            label8.Tag = "NoModificarConBase";
            label8.Text = "Próximo Vencimiento:";
            // 
            // lblFechaVto
            // 
            lblFechaVto.AutoSize = true;
            lblFechaVto.Font = new Font("Segoe UI Semibold", 18F, FontStyle.Bold);
            lblFechaVto.Location = new Point(688, 116);
            lblFechaVto.Name = "lblFechaVto";
            lblFechaVto.Size = new Size(135, 32);
            lblFechaVto.TabIndex = 38;
            lblFechaVto.Tag = "NoModificarConBase";
            lblFechaVto.Text = "01/08/2026";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Font = new Font("Segoe UI Semibold", 18F, FontStyle.Bold);
            label10.Location = new Point(592, 234);
            label10.Name = "label10";
            label10.Size = new Size(197, 32);
            label10.TabIndex = 39;
            label10.Tag = "NoModificarConBase";
            label10.Text = "Dnis Habilitados:";
            // 
            // lstDnis
            // 
            lstDnis.FormattingEnabled = true;
            lstDnis.ItemHeight = 15;
            lstDnis.Location = new Point(592, 278);
            lstDnis.Name = "lstDnis";
            lstDnis.Size = new Size(235, 229);
            lstDnis.TabIndex = 40;
            // 
            // lblDetallesExtra
            // 
            lblDetallesExtra.AutoSize = true;
            lblDetallesExtra.Font = new Font("Segoe UI Semibold", 18F, FontStyle.Bold);
            lblDetallesExtra.Location = new Point(26, 202);
            lblDetallesExtra.Name = "lblDetallesExtra";
            lblDetallesExtra.Size = new Size(711, 32);
            lblDetallesExtra.TabIndex = 41;
            lblDetallesExtra.Tag = "NoModificarConBase";
            lblDetallesExtra.Text = "Se le permite tener saldo negativo. Monto de deuda maximo 0$";
            // 
            // lblEstado
            // 
            lblEstado.AutoSize = true;
            lblEstado.Font = new Font("Segoe UI Semibold", 18F, FontStyle.Bold);
            lblEstado.Location = new Point(135, 126);
            lblEstado.Name = "lblEstado";
            lblEstado.Size = new Size(114, 32);
            lblEstado.TabIndex = 43;
            lblEstado.Tag = "NoModificarConBase";
            lblEstado.Text = "**********";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI Semibold", 18F, FontStyle.Bold);
            label4.Location = new Point(26, 126);
            label4.Name = "label4";
            label4.Size = new Size(92, 32);
            label4.TabIndex = 42;
            label4.Tag = "NoModificarConBase";
            label4.Text = "Estado:";
            // 
            // FDetallesCtaCte
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1513, 581);
            Controls.Add(lblEstado);
            Controls.Add(label4);
            Controls.Add(lblDetallesExtra);
            Controls.Add(lstDnis);
            Controls.Add(label10);
            Controls.Add(lblFechaVto);
            Controls.Add(label8);
            Controls.Add(lblNombreCuenta);
            Controls.Add(label7);
            Controls.Add(lblFechaCreacion);
            Controls.Add(label5);
            Controls.Add(lblSaldoCuenta);
            Controls.Add(label2);
            Controls.Add(tableLayoutPanel10);
            Controls.Add(lblListadoMovimientos);
            Controls.Add(dgvGrilla);
            Controls.Add(lblNombreCliente);
            Controls.Add(lblCliente);
            ForeColor = Color.FromArgb(31, 26, 43);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "FDetallesCtaCte";
            Text = "FDetallesCtaCte";
            Load += FDetallesCtaCte_Load;
            ((System.ComponentModel.ISupportInitialize)error).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvGrilla).EndInit();
            tableLayoutPanel10.ResumeLayout(false);
            tableLayoutPanel9.ResumeLayout(false);
            tableLayoutPanel9.PerformLayout();
            tableLayoutPanel11.ResumeLayout(false);
            tableLayoutPanel11.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pbxLogo).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblNombreCliente;
        private Label lblCliente;
        protected DataGridView dgvGrilla;
        private Label lblListadoMovimientos;
        private TableLayoutPanel tableLayoutPanel10;
        private TableLayoutPanel tableLayoutPanel9;
        private TableLayoutPanel tableLayoutPanel11;
        protected Label lblPagina;
        private Button btnAnterior;
        private Button btnSiguiente;
        protected Label lblTotalRegistros;
        private PictureBox pbxLogo;
        private Label lblSaldoCuenta;
        private Label label2;
        private Label lblFechaCreacion;
        private Label label5;
        private Label lblNombreCuenta;
        private Label label7;
        private Label label8;
        private Label lblFechaVto;
        private Label label10;
        private ListBox lstDnis;
        private Label lblDetallesExtra;
        private Label lblEstado;
        private Label label4;
    }
}