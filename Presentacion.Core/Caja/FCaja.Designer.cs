namespace Presentacion.Core.Caja
{
    partial class FCaja
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FCaja));
            btnConsultarMovimientos = new Button();
            btnConsultarCajas = new Button();
            btnCerrarCaja = new Button();
            btnAbrirCaja = new Button();
            lblEstadoCaja = new Label();
            lblSaldoCaja = new Label();
            tableLayoutPanel1 = new TableLayoutPanel();
            tableLayoutPanel2 = new TableLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)error).BeginInit();
            tableLayoutPanel1.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            SuspendLayout();
            // 
            // btnConsultarMovimientos
            // 
            btnConsultarMovimientos.Anchor = AnchorStyles.Top;
            btnConsultarMovimientos.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnConsultarMovimientos.Location = new Point(51, 223);
            btnConsultarMovimientos.Name = "btnConsultarMovimientos";
            btnConsultarMovimientos.Size = new Size(218, 42);
            btnConsultarMovimientos.TabIndex = 7;
            btnConsultarMovimientos.Text = "Consultar Movimientos";
            btnConsultarMovimientos.UseVisualStyleBackColor = true;
            btnConsultarMovimientos.Click += btnConsultarMovimientos_Click;
            // 
            // btnConsultarCajas
            // 
            btnConsultarCajas.Anchor = AnchorStyles.Top;
            btnConsultarCajas.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnConsultarCajas.Location = new Point(51, 168);
            btnConsultarCajas.Name = "btnConsultarCajas";
            btnConsultarCajas.Size = new Size(218, 42);
            btnConsultarCajas.TabIndex = 6;
            btnConsultarCajas.Text = "Consulta Cajas";
            btnConsultarCajas.UseVisualStyleBackColor = true;
            btnConsultarCajas.Click += btnConsultarCajas_Click;
            // 
            // btnCerrarCaja
            // 
            btnCerrarCaja.Anchor = AnchorStyles.Top;
            btnCerrarCaja.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnCerrarCaja.Location = new Point(182, 3);
            btnCerrarCaja.Name = "btnCerrarCaja";
            btnCerrarCaja.Size = new Size(106, 43);
            btnCerrarCaja.TabIndex = 5;
            btnCerrarCaja.Text = "Cerrar Caja";
            btnCerrarCaja.UseVisualStyleBackColor = true;
            btnCerrarCaja.Click += btnCerrarCaja_Click;
            // 
            // btnAbrirCaja
            // 
            btnAbrirCaja.Anchor = AnchorStyles.Top;
            btnAbrirCaja.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnAbrirCaja.Location = new Point(25, 3);
            btnAbrirCaja.Name = "btnAbrirCaja";
            btnAbrirCaja.Size = new Size(106, 43);
            btnAbrirCaja.TabIndex = 4;
            btnAbrirCaja.Text = "Abrir Caja";
            btnAbrirCaja.UseVisualStyleBackColor = true;
            btnAbrirCaja.Click += btnAbrirCaja_Click;
            // 
            // lblEstadoCaja
            // 
            lblEstadoCaja.Anchor = AnchorStyles.Top;
            lblEstadoCaja.AutoSize = true;
            lblEstadoCaja.Font = new Font("Segoe UI Semibold", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblEstadoCaja.Location = new Point(93, 0);
            lblEstadoCaja.Name = "lblEstadoCaja";
            lblEstadoCaja.Size = new Size(134, 32);
            lblEstadoCaja.TabIndex = 8;
            lblEstadoCaja.Text = "estadoCaja";
            // 
            // lblSaldoCaja
            // 
            lblSaldoCaja.Anchor = AnchorStyles.Top;
            lblSaldoCaja.AutoSize = true;
            lblSaldoCaja.Font = new Font("Segoe UI Semibold", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblSaldoCaja.Location = new Point(114, 55);
            lblSaldoCaja.Name = "lblSaldoCaja";
            lblSaldoCaja.Size = new Size(91, 25);
            lblSaldoCaja.TabIndex = 9;
            lblSaldoCaja.Text = "saldocaja";
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 1;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Controls.Add(tableLayoutPanel2, 0, 2);
            tableLayoutPanel1.Controls.Add(btnConsultarMovimientos, 0, 4);
            tableLayoutPanel1.Controls.Add(lblEstadoCaja, 0, 0);
            tableLayoutPanel1.Controls.Add(btnConsultarCajas, 0, 3);
            tableLayoutPanel1.Controls.Add(lblSaldoCaja, 0, 1);
            tableLayoutPanel1.Location = new Point(12, 12);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 5;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel1.Size = new Size(320, 277);
            tableLayoutPanel1.TabIndex = 10;
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.ColumnCount = 2;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel2.Controls.Add(btnAbrirCaja, 0, 0);
            tableLayoutPanel2.Controls.Add(btnCerrarCaja, 1, 0);
            tableLayoutPanel2.Location = new Point(3, 113);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 1;
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel2.Size = new Size(314, 49);
            tableLayoutPanel2.TabIndex = 11;
            // 
            // FCaja
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(344, 301);
            Controls.Add(tableLayoutPanel1);
            ForeColor = Color.FromArgb(31, 26, 43);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "FCaja";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Caja";
            Load += FCaja_Load;
            ((System.ComponentModel.ISupportInitialize)error).EndInit();
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            tableLayoutPanel2.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Button btnConsultarMovimientos;
        private Button btnConsultarCajas;
        private Button btnCerrarCaja;
        private Button btnAbrirCaja;
        private Label lblEstadoCaja;
        private Label lblSaldoCaja;
        private TableLayoutPanel tableLayoutPanel1;
        private TableLayoutPanel tableLayoutPanel2;
    }
}