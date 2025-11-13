namespace Presentacion.Core.Movimiento
{
    partial class FMovimientoDetallado
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
            lblNumeroMovimiento = new Label();
            lblFechaMovimiento = new Label();
            lblMontoMovimiento = new Label();
            lblTipoMovimiento = new Label();
            SuspendLayout();
            // 
            // lblNumeroMovimiento
            // 
            lblNumeroMovimiento.AutoSize = true;
            lblNumeroMovimiento.Location = new Point(39, 20);
            lblNumeroMovimiento.Name = "lblNumeroMovimiento";
            lblNumeroMovimiento.Size = new Size(116, 15);
            lblNumeroMovimiento.TabIndex = 0;
            lblNumeroMovimiento.Text = "NumeroMovimiento";
            // 
            // lblFechaMovimiento
            // 
            lblFechaMovimiento.AutoSize = true;
            lblFechaMovimiento.Location = new Point(39, 66);
            lblFechaMovimiento.Name = "lblFechaMovimiento";
            lblFechaMovimiento.Size = new Size(38, 15);
            lblFechaMovimiento.TabIndex = 1;
            lblFechaMovimiento.Text = "Fecha";
            // 
            // lblMontoMovimiento
            // 
            lblMontoMovimiento.AutoSize = true;
            lblMontoMovimiento.Location = new Point(39, 96);
            lblMontoMovimiento.Name = "lblMontoMovimiento";
            lblMontoMovimiento.Size = new Size(43, 15);
            lblMontoMovimiento.TabIndex = 2;
            lblMontoMovimiento.Text = "Monto";
            // 
            // lblTipoMovimiento
            // 
            lblTipoMovimiento.AutoSize = true;
            lblTipoMovimiento.Location = new Point(39, 128);
            lblTipoMovimiento.Name = "lblTipoMovimiento";
            lblTipoMovimiento.Size = new Size(99, 15);
            lblTipoMovimiento.TabIndex = 3;
            lblTipoMovimiento.Text = "Tipo Movimiento";
            // 
            // FMovimientoDetallado
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(583, 578);
            Controls.Add(lblTipoMovimiento);
            Controls.Add(lblMontoMovimiento);
            Controls.Add(lblFechaMovimiento);
            Controls.Add(lblNumeroMovimiento);
            Name = "FMovimientoDetallado";
            Text = "FMovimientoDetallado";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblNumeroMovimiento;
        private Label lblFechaMovimiento;
        private Label lblMontoMovimiento;
        private Label lblTipoMovimiento;
    }
}