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
            label1 = new Label();
            label2 = new Label();
            tabPageVenta2 = new TabPage();
            dgvProductos = new DataGridView();
            tabPageVenta1 = new TabPage();
            txtDetalle = new TextBox();
            label4 = new Label();
            label3 = new Label();
            lblNombreEmpleado = new Label();
            lblTipoMovimiento = new Label();
            lblMontoMovimiento = new Label();
            tabVenta = new TabControl();
            tabPageVenta2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvProductos).BeginInit();
            tabPageVenta1.SuspendLayout();
            tabVenta.SuspendLayout();
            SuspendLayout();
            // 
            // lblNumeroMovimiento
            // 
            lblNumeroMovimiento.AutoSize = true;
            lblNumeroMovimiento.Location = new Point(12, 41);
            lblNumeroMovimiento.Name = "lblNumeroMovimiento";
            lblNumeroMovimiento.Size = new Size(116, 15);
            lblNumeroMovimiento.TabIndex = 0;
            lblNumeroMovimiento.Text = "NumeroMovimiento";
            // 
            // lblFechaMovimiento
            // 
            lblFechaMovimiento.AutoSize = true;
            lblFechaMovimiento.Location = new Point(280, 41);
            lblFechaMovimiento.Name = "lblFechaMovimiento";
            lblFechaMovimiento.Size = new Size(140, 15);
            lblFechaMovimiento.TabIndex = 1;
            lblFechaMovimiento.Text = "Fecha Dia Hora completa";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(280, 17);
            label1.Name = "label1";
            label1.Size = new Size(41, 15);
            label1.TabIndex = 4;
            label1.Text = "Fecha:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 17);
            label2.Name = "label2";
            label2.Size = new Size(122, 15);
            label2.TabIndex = 5;
            label2.Text = "Numero Movimiento:";
            // 
            // tabPageVenta2
            // 
            tabPageVenta2.Controls.Add(dgvProductos);
            tabPageVenta2.Location = new Point(4, 24);
            tabPageVenta2.Name = "tabPageVenta2";
            tabPageVenta2.Padding = new Padding(3);
            tabPageVenta2.Size = new Size(455, 262);
            tabPageVenta2.TabIndex = 1;
            tabPageVenta2.Text = "Productos Detalle";
            tabPageVenta2.UseVisualStyleBackColor = true;
            // 
            // dgvProductos
            // 
            dgvProductos.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvProductos.Location = new Point(0, 0);
            dgvProductos.Name = "dgvProductos";
            dgvProductos.Size = new Size(455, 262);
            dgvProductos.TabIndex = 0;
            // 
            // tabPageVenta1
            // 
            tabPageVenta1.Controls.Add(txtDetalle);
            tabPageVenta1.Controls.Add(label4);
            tabPageVenta1.Controls.Add(label3);
            tabPageVenta1.Controls.Add(lblNombreEmpleado);
            tabPageVenta1.Controls.Add(lblTipoMovimiento);
            tabPageVenta1.Controls.Add(lblMontoMovimiento);
            tabPageVenta1.Location = new Point(4, 24);
            tabPageVenta1.Name = "tabPageVenta1";
            tabPageVenta1.Padding = new Padding(3);
            tabPageVenta1.Size = new Size(455, 262);
            tabPageVenta1.TabIndex = 0;
            tabPageVenta1.Text = "Venta Detalle";
            tabPageVenta1.UseVisualStyleBackColor = true;
            // 
            // txtDetalle
            // 
            txtDetalle.BackColor = SystemColors.Window;
            txtDetalle.BorderStyle = BorderStyle.None;
            txtDetalle.Location = new Point(6, 90);
            txtDetalle.Multiline = true;
            txtDetalle.Name = "txtDetalle";
            txtDetalle.ReadOnly = true;
            txtDetalle.Size = new Size(446, 169);
            txtDetalle.TabIndex = 4;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(36, 53);
            label4.Name = "label4";
            label4.Size = new Size(0, 15);
            label4.TabIndex = 2;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(6, 9);
            label3.Name = "label3";
            label3.Size = new Size(63, 15);
            label3.TabIndex = 1;
            label3.Text = "Empleado:";
            // 
            // lblNombreEmpleado
            // 
            lblNombreEmpleado.AutoSize = true;
            lblNombreEmpleado.Location = new Point(6, 24);
            lblNombreEmpleado.Name = "lblNombreEmpleado";
            lblNombreEmpleado.Size = new Size(117, 15);
            lblNombreEmpleado.TabIndex = 0;
            lblNombreEmpleado.Text = "lblNombreEmpleado";
            // 
            // lblTipoMovimiento
            // 
            lblTipoMovimiento.AutoSize = true;
            lblTipoMovimiento.Location = new Point(7, 44);
            lblTipoMovimiento.Name = "lblTipoMovimiento";
            lblTipoMovimiento.Size = new Size(102, 15);
            lblTipoMovimiento.TabIndex = 3;
            lblTipoMovimiento.Text = "Tipo Movimiento:";
            // 
            // lblMontoMovimiento
            // 
            lblMontoMovimiento.AutoSize = true;
            lblMontoMovimiento.Location = new Point(7, 59);
            lblMontoMovimiento.Name = "lblMontoMovimiento";
            lblMontoMovimiento.Size = new Size(43, 15);
            lblMontoMovimiento.TabIndex = 2;
            lblMontoMovimiento.Text = "Monto";
            // 
            // tabVenta
            // 
            tabVenta.Controls.Add(tabPageVenta1);
            tabVenta.Controls.Add(tabPageVenta2);
            tabVenta.Location = new Point(12, 69);
            tabVenta.Name = "tabVenta";
            tabVenta.SelectedIndex = 0;
            tabVenta.Size = new Size(463, 290);
            tabVenta.TabIndex = 6;
            // 
            // FMovimientoDetallado
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(478, 360);
            Controls.Add(tabVenta);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(lblFechaMovimiento);
            Controls.Add(lblNumeroMovimiento);
            Name = "FMovimientoDetallado";
            Text = "FMovimientoDetallado";
            tabPageVenta2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvProductos).EndInit();
            tabPageVenta1.ResumeLayout(false);
            tabPageVenta1.PerformLayout();
            tabVenta.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblNumeroMovimiento;
        private Label lblFechaMovimiento;
        private Label label1;
        private Label label2;
        private TabPage tabPageVenta2;
        private TabPage tabPageVenta1;
        private TextBox txtDetalle;
        private Label label4;
        private Label label3;
        private Label lblNombreEmpleado;
        private Label lblTipoMovimiento;
        private Label lblMontoMovimiento;
        private TabControl tabVenta;
        private DataGridView dgvProductos;
    }
}