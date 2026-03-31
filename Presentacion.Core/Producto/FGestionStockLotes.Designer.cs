namespace Presentacion.Core.Producto
{
    partial class FGestionStockLotes
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
            lblTituloLotes = new Label();
            lblNombreProducto = new Label();
            label2 = new Label();
            label3 = new Label();
            lblNumeroLote = new Label();
            lblDescripcionLote = new Label();
            lblFechaVencimientoLote = new Label();
            chkLoteEstaActivo = new CheckBox();
            txtNumeroLote = new TextBox();
            txtDescripcionLote = new TextBox();
            dtpFechaVencimiento = new DateTimePicker();
            chkFechaVencimiento = new CheckBox();
            nudStockInicial = new NumericUpDown();
            nudStockActual = new NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)error).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudStockInicial).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudStockActual).BeginInit();
            SuspendLayout();
            // 
            // lblTituloLotes
            // 
            lblTituloLotes.AutoSize = true;
            lblTituloLotes.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold);
            lblTituloLotes.Location = new Point(40, 68);
            lblTituloLotes.Name = "lblTituloLotes";
            lblTituloLotes.Size = new Size(252, 30);
            lblTituloLotes.TabIndex = 0;
            lblTituloLotes.Text = "Crear Lote del producto:";
            // 
            // lblNombreProducto
            // 
            lblNombreProducto.AutoSize = true;
            lblNombreProducto.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold);
            lblNombreProducto.ForeColor = Color.SeaGreen;
            lblNombreProducto.Location = new Point(298, 68);
            lblNombreProducto.Name = "lblNombreProducto";
            lblNombreProducto.Size = new Size(126, 30);
            lblNombreProducto.TabIndex = 1;
            lblNombreProducto.Text = "PRODUCTO";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(40, 157);
            label2.Name = "label2";
            label2.Size = new Size(73, 15);
            label2.TabIndex = 2;
            label2.Text = "Stock Inicial:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(40, 197);
            label3.Name = "label3";
            label3.Size = new Size(76, 15);
            label3.TabIndex = 3;
            label3.Text = "Stock Actual:";
            // 
            // lblNumeroLote
            // 
            lblNumeroLote.AutoSize = true;
            lblNumeroLote.Location = new Point(36, 110);
            lblNumeroLote.Name = "lblNumeroLote";
            lblNumeroLote.Size = new Size(77, 15);
            lblNumeroLote.TabIndex = 5;
            lblNumeroLote.Text = "Numero Lote";
            // 
            // lblDescripcionLote
            // 
            lblDescripcionLote.AutoSize = true;
            lblDescripcionLote.Location = new Point(40, 264);
            lblDescripcionLote.Name = "lblDescripcionLote";
            lblDescripcionLote.Size = new Size(72, 15);
            lblDescripcionLote.TabIndex = 6;
            lblDescripcionLote.Text = "Descripcion:";
            // 
            // lblFechaVencimientoLote
            // 
            lblFechaVencimientoLote.AutoSize = true;
            lblFechaVencimientoLote.Location = new Point(489, 197);
            lblFechaVencimientoLote.Name = "lblFechaVencimientoLote";
            lblFechaVencimientoLote.Size = new Size(110, 15);
            lblFechaVencimientoLote.TabIndex = 7;
            lblFechaVencimientoLote.Text = "Fecha Vencimiento:";
            // 
            // chkLoteEstaActivo
            // 
            chkLoteEstaActivo.AutoSize = true;
            chkLoteEstaActivo.Location = new Point(489, 109);
            chkLoteEstaActivo.Name = "chkLoteEstaActivo";
            chkLoteEstaActivo.Size = new Size(105, 19);
            chkLoteEstaActivo.TabIndex = 9;
            chkLoteEstaActivo.Text = "Esta Habilitado";
            chkLoteEstaActivo.UseVisualStyleBackColor = true;
            // 
            // txtNumeroLote
            // 
            txtNumeroLote.Location = new Point(123, 107);
            txtNumeroLote.Name = "txtNumeroLote";
            txtNumeroLote.Size = new Size(206, 23);
            txtNumeroLote.TabIndex = 10;
            // 
            // txtDescripcionLote
            // 
            txtDescripcionLote.Location = new Point(40, 282);
            txtDescripcionLote.Name = "txtDescripcionLote";
            txtDescripcionLote.Size = new Size(660, 23);
            txtDescripcionLote.TabIndex = 14;
            // 
            // dtpFechaVencimiento
            // 
            dtpFechaVencimiento.Location = new Point(489, 215);
            dtpFechaVencimiento.Name = "dtpFechaVencimiento";
            dtpFechaVencimiento.Size = new Size(211, 23);
            dtpFechaVencimiento.TabIndex = 15;
            // 
            // chkFechaVencimiento
            // 
            chkFechaVencimiento.AutoSize = true;
            chkFechaVencimiento.Location = new Point(489, 153);
            chkFechaVencimiento.Name = "chkFechaVencimiento";
            chkFechaVencimiento.Size = new Size(174, 19);
            chkFechaVencimiento.TabIndex = 16;
            chkFechaVencimiento.Text = "Tiene Fecha de Vencimiento";
            chkFechaVencimiento.UseVisualStyleBackColor = true;
            chkFechaVencimiento.CheckedChanged += chkFechaVencimiento_CheckedChanged_1;
            // 
            // nudStockInicial
            // 
            nudStockInicial.Location = new Point(165, 155);
            nudStockInicial.Name = "nudStockInicial";
            nudStockInicial.Size = new Size(120, 23);
            nudStockInicial.TabIndex = 17;
            // 
            // nudStockActual
            // 
            nudStockActual.Location = new Point(165, 195);
            nudStockActual.Name = "nudStockActual";
            nudStockActual.Size = new Size(120, 23);
            nudStockActual.TabIndex = 18;
            // 
            // FGestionStockLotes
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(780, 339);
            Controls.Add(nudStockActual);
            Controls.Add(nudStockInicial);
            Controls.Add(chkFechaVencimiento);
            Controls.Add(dtpFechaVencimiento);
            Controls.Add(txtDescripcionLote);
            Controls.Add(txtNumeroLote);
            Controls.Add(chkLoteEstaActivo);
            Controls.Add(lblFechaVencimientoLote);
            Controls.Add(lblDescripcionLote);
            Controls.Add(lblNumeroLote);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(lblNombreProducto);
            Controls.Add(lblTituloLotes);
            Name = "FGestionStockLotes";
            Text = "FGestionStockLotes";
            Load += FGestionStockLotes_Load;
            Controls.SetChildIndex(lblTituloLotes, 0);
            Controls.SetChildIndex(lblNombreProducto, 0);
            Controls.SetChildIndex(label2, 0);
            Controls.SetChildIndex(label3, 0);
            Controls.SetChildIndex(lblNumeroLote, 0);
            Controls.SetChildIndex(lblDescripcionLote, 0);
            Controls.SetChildIndex(lblFechaVencimientoLote, 0);
            Controls.SetChildIndex(chkLoteEstaActivo, 0);
            Controls.SetChildIndex(txtNumeroLote, 0);
            Controls.SetChildIndex(txtDescripcionLote, 0);
            Controls.SetChildIndex(dtpFechaVencimiento, 0);
            Controls.SetChildIndex(chkFechaVencimiento, 0);
            Controls.SetChildIndex(nudStockInicial, 0);
            Controls.SetChildIndex(nudStockActual, 0);
            ((System.ComponentModel.ISupportInitialize)error).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudStockInicial).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudStockActual).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblTituloLotes;
        private Label lblNombreProducto;
        private Label label2;
        private Label label3;
        private Label lblNumeroLote;
        private Label lblDescripcionLote;
        private Label lblFechaVencimientoLote;
        private CheckBox chkLoteEstaActivo;
        private TextBox txtNumeroLote;
        private TextBox txtDescripcionLote;
        private DateTimePicker dtpFechaVencimiento;
        private CheckBox chkFechaVencimiento;
        private NumericUpDown nudStockInicial;
        private NumericUpDown nudStockActual;
    }
}