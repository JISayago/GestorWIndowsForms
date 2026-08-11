namespace Presentacion.Core.Caja
{
    partial class FCajaAbrir
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
            tlpRoot = new TableLayoutPanel();
            lblTitulo = new Label();
            lblUsuario = new Label();
            lblUsuarioLogeado = new Label();
            lblConfirmacion = new Label();
            lblMontoApertura = new Label();
            txtMontoApertura = new TextBox();
            lblSaldoActualTitulo = new Label();
            lblSaldoActual = new Label();
            pnlBotones = new Panel();
            btnCancelar = new Button();
            btnAbrirCaja = new Button();
            ((System.ComponentModel.ISupportInitialize)error).BeginInit();
            tlpRoot.SuspendLayout();
            pnlBotones.SuspendLayout();
            SuspendLayout();
            // 
            // tlpRoot
            // 
            tlpRoot.ColumnCount = 2;
            tlpRoot.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 140F));
            tlpRoot.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpRoot.Controls.Add(lblTitulo, 0, 0);
            tlpRoot.Controls.Add(lblUsuario, 0, 1);
            tlpRoot.Controls.Add(lblUsuarioLogeado, 1, 1);
            tlpRoot.Controls.Add(lblConfirmacion, 0, 2);
            tlpRoot.Controls.Add(lblMontoApertura, 0, 3);
            tlpRoot.Controls.Add(txtMontoApertura, 1, 3);
            tlpRoot.Controls.Add(lblSaldoActualTitulo, 0, 4);
            tlpRoot.Controls.Add(lblSaldoActual, 1, 4);
            tlpRoot.Controls.Add(pnlBotones, 0, 5);
            tlpRoot.Dock = DockStyle.Fill;
            tlpRoot.Name = "tlpRoot";
            tlpRoot.Padding = new Padding(20, 16, 20, 16);
            tlpRoot.RowCount = 6;
            tlpRoot.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            tlpRoot.RowStyles.Add(new RowStyle(SizeType.Absolute, 32F));
            tlpRoot.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            tlpRoot.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            tlpRoot.RowStyles.Add(new RowStyle(SizeType.Absolute, 36F));
            tlpRoot.RowStyles.Add(new RowStyle(SizeType.Absolute, 56F));
            tlpRoot.TabIndex = 0;
            // 
            // lblTitulo
            // 
            tlpRoot.SetColumnSpan(lblTitulo, 2);
            lblTitulo.Dock = DockStyle.Fill;
            lblTitulo.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Text = "Caja";
            lblTitulo.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblUsuario
            // 
            lblUsuario.Dock = DockStyle.Fill;
            lblUsuario.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblUsuario.Name = "lblUsuario";
            lblUsuario.Text = "Usuario:";
            lblUsuario.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblUsuarioLogeado
            // 
            lblUsuarioLogeado.Dock = DockStyle.Fill;
            lblUsuarioLogeado.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblUsuarioLogeado.ForeColor = Color.FromArgb(46, 125, 50);
            lblUsuarioLogeado.Name = "lblUsuarioLogeado";
            lblUsuarioLogeado.Text = "-";
            lblUsuarioLogeado.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblConfirmacion
            // 
            tlpRoot.SetColumnSpan(lblConfirmacion, 2);
            lblConfirmacion.Dock = DockStyle.Fill;
            lblConfirmacion.Font = new Font("Segoe UI", 10F);
            lblConfirmacion.Name = "lblConfirmacion";
            lblConfirmacion.Text = "¿Confirma la operación?";
            lblConfirmacion.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblMontoApertura
            // 
            lblMontoApertura.Dock = DockStyle.Fill;
            lblMontoApertura.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblMontoApertura.Name = "lblMontoApertura";
            lblMontoApertura.Text = "Monto apertura:";
            lblMontoApertura.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // txtMontoApertura
            // 
            txtMontoApertura.Dock = DockStyle.Fill;
            txtMontoApertura.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            txtMontoApertura.Margin = new Padding(0, 6, 0, 6);
            txtMontoApertura.Name = "txtMontoApertura";
            txtMontoApertura.TextAlign = HorizontalAlignment.Right;
            txtMontoApertura.TabIndex = 0;
            txtMontoApertura.TextChanged += txtMontoApertura_TextChanged;
            txtMontoApertura.KeyPress += txtMontoApertura_KeyPress;
            // 
            // lblSaldoActualTitulo
            // 
            lblSaldoActualTitulo.Dock = DockStyle.Fill;
            lblSaldoActualTitulo.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblSaldoActualTitulo.Name = "lblSaldoActualTitulo";
            lblSaldoActualTitulo.Text = "Saldo en caja:";
            lblSaldoActualTitulo.TextAlign = ContentAlignment.MiddleLeft;
            lblSaldoActualTitulo.Visible = false;
            // 
            // lblSaldoActual
            // 
            lblSaldoActual.Dock = DockStyle.Fill;
            lblSaldoActual.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblSaldoActual.Name = "lblSaldoActual";
            lblSaldoActual.Text = "$ 0";
            lblSaldoActual.TextAlign = ContentAlignment.MiddleLeft;
            lblSaldoActual.Visible = false;
            // 
            // pnlBotones
            // 
            tlpRoot.SetColumnSpan(pnlBotones, 2);
            pnlBotones.Controls.Add(btnCancelar);
            pnlBotones.Controls.Add(btnAbrirCaja);
            pnlBotones.Dock = DockStyle.Fill;
            pnlBotones.Name = "pnlBotones";
            pnlBotones.Padding = new Padding(0, 8, 0, 0);
            pnlBotones.TabIndex = 1;
            // 
            // btnCancelar
            // 
            btnCancelar.Anchor = AnchorStyles.None;
            btnCancelar.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnCancelar.Location = new Point(0, 0);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new Size(120, 36);
            btnCancelar.TabIndex = 2;
            btnCancelar.Text = "Cancelar";
            btnCancelar.UseVisualStyleBackColor = false;
            btnCancelar.Click += btnCancelar_Click;
            // 
            // btnAbrirCaja
            // 
            btnAbrirCaja.Anchor = AnchorStyles.None;
            btnAbrirCaja.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnAbrirCaja.Location = new Point(0, 0);
            btnAbrirCaja.Name = "btnAbrirCaja";
            btnAbrirCaja.Size = new Size(120, 36);
            btnAbrirCaja.TabIndex = 1;
            btnAbrirCaja.Text = "Abrir caja";
            btnAbrirCaja.UseVisualStyleBackColor = false;
            btnAbrirCaja.Click += btnAbrirCaja_Click;
            // 
            // FCajaAbrir
            // 
            AcceptButton = btnAbrirCaja;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = btnCancelar;
            ClientSize = new Size(420, 300);
            Controls.Add(tlpRoot);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            MinimumSize = new Size(420, 300);
            Name = "FCajaAbrir";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Caja";
            ((System.ComponentModel.ISupportInitialize)error).EndInit();
            tlpRoot.ResumeLayout(false);
            tlpRoot.PerformLayout();
            pnlBotones.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tlpRoot;
        private Label lblTitulo;
        private Label lblUsuario;
        private Label lblUsuarioLogeado;
        private Label lblConfirmacion;
        private Label lblMontoApertura;
        private TextBox txtMontoApertura;
        private Label lblSaldoActualTitulo;
        private Label lblSaldoActual;
        private Panel pnlBotones;
        private Button btnAbrirCaja;
        private Button btnCancelar;
    }
}
