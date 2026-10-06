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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FGestionStockLotes));
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
            tableLayoutPanel1 = new TableLayoutPanel();
            flowLayoutPanel5 = new FlowLayoutPanel();
            flowLayoutPanel8 = new FlowLayoutPanel();
            flowLayoutPanel2 = new FlowLayoutPanel();
            flowLayoutPanel4 = new FlowLayoutPanel();
            flowLayoutPanel3 = new FlowLayoutPanel();
            flowLayoutPanel7 = new FlowLayoutPanel();
            tableLayoutPanel2 = new TableLayoutPanel();
            flowLayoutPanel6 = new FlowLayoutPanel();
            tableLayoutPanel3 = new TableLayoutPanel();
            tableLayoutPanel4 = new TableLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)error).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudStockInicial).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudStockActual).BeginInit();
            tableLayoutPanel1.SuspendLayout();
            flowLayoutPanel5.SuspendLayout();
            flowLayoutPanel8.SuspendLayout();
            flowLayoutPanel2.SuspendLayout();
            flowLayoutPanel4.SuspendLayout();
            flowLayoutPanel3.SuspendLayout();
            flowLayoutPanel7.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            flowLayoutPanel6.SuspendLayout();
            tableLayoutPanel3.SuspendLayout();
            tableLayoutPanel4.SuspendLayout();
            SuspendLayout();
            // 
            // lblTituloLotes
            // 
            lblTituloLotes.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            lblTituloLotes.AutoSize = true;
            lblTituloLotes.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold);
            lblTituloLotes.Location = new Point(3, 0);
            lblTituloLotes.Name = "lblTituloLotes";
            lblTituloLotes.Size = new Size(546, 30);
            lblTituloLotes.TabIndex = 0;
            lblTituloLotes.Tag = "NoModificarConBase";
            lblTituloLotes.Text = "Crear Lote del producto:";
            lblTituloLotes.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblNombreProducto
            // 
            lblNombreProducto.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            lblNombreProducto.AutoSize = true;
            lblNombreProducto.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold);
            lblNombreProducto.ForeColor = Color.FromArgb(67, 20, 135);
            lblNombreProducto.Location = new Point(3, 30);
            lblNombreProducto.Name = "lblNombreProducto";
            lblNombreProducto.Size = new Size(546, 30);
            lblNombreProducto.TabIndex = 1;
            lblNombreProducto.Tag = "NoModificarConBase";
            lblNombreProducto.Text = "PRODUCTO";
            lblNombreProducto.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(129, 0);
            label2.Name = "label2";
            label2.Size = new Size(96, 15);
            label2.TabIndex = 2;
            label2.Text = "Stock Comprado";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(129, 0);
            label3.Name = "label3";
            label3.Size = new Size(89, 15);
            label3.TabIndex = 3;
            label3.Text = "Stock Recibido:";
            // 
            // lblNumeroLote
            // 
            lblNumeroLote.AutoSize = true;
            lblNumeroLote.Location = new Point(3, 0);
            lblNumeroLote.Name = "lblNumeroLote";
            lblNumeroLote.Size = new Size(77, 15);
            lblNumeroLote.TabIndex = 5;
            lblNumeroLote.Text = "Numero Lote";
            // 
            // lblDescripcionLote
            // 
            lblDescripcionLote.AutoSize = true;
            lblDescripcionLote.Location = new Point(3, 0);
            lblDescripcionLote.Name = "lblDescripcionLote";
            lblDescripcionLote.Size = new Size(73, 15);
            lblDescripcionLote.TabIndex = 6;
            lblDescripcionLote.Text = "Descripcion:";
            // 
            // lblFechaVencimientoLote
            // 
            lblFechaVencimientoLote.AutoSize = true;
            lblFechaVencimientoLote.Location = new Point(3, 0);
            lblFechaVencimientoLote.Name = "lblFechaVencimientoLote";
            lblFechaVencimientoLote.Size = new Size(111, 15);
            lblFechaVencimientoLote.TabIndex = 7;
            lblFechaVencimientoLote.Text = "Fecha Vencimiento:";
            // 
            // chkLoteEstaActivo
            // 
            chkLoteEstaActivo.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            chkLoteEstaActivo.AutoSize = true;
            chkLoteEstaActivo.Location = new Point(3, 3);
            chkLoteEstaActivo.Name = "chkLoteEstaActivo";
            chkLoteEstaActivo.Size = new Size(105, 19);
            chkLoteEstaActivo.TabIndex = 9;
            chkLoteEstaActivo.Text = "Esta Habilitado";
            chkLoteEstaActivo.UseVisualStyleBackColor = true;
            // 
            // txtNumeroLote
            // 
            txtNumeroLote.Location = new Point(3, 18);
            txtNumeroLote.Name = "txtNumeroLote";
            txtNumeroLote.Size = new Size(206, 23);
            txtNumeroLote.TabIndex = 10;
            // 
            // txtDescripcionLote
            // 
            txtDescripcionLote.Location = new Point(3, 20);
            txtDescripcionLote.Multiline = true;
            txtDescripcionLote.Name = "txtDescripcionLote";
            txtDescripcionLote.Size = new Size(543, 46);
            txtDescripcionLote.TabIndex = 14;
            // 
            // dtpFechaVencimiento
            // 
            dtpFechaVencimiento.Location = new Point(3, 18);
            dtpFechaVencimiento.Name = "dtpFechaVencimiento";
            dtpFechaVencimiento.Size = new Size(175, 23);
            dtpFechaVencimiento.TabIndex = 15;
            // 
            // chkFechaVencimiento
            // 
            chkFechaVencimiento.Anchor = AnchorStyles.Right;
            chkFechaVencimiento.AutoSize = true;
            chkFechaVencimiento.Location = new Point(3, 3);
            chkFechaVencimiento.Name = "chkFechaVencimiento";
            chkFechaVencimiento.Size = new Size(175, 19);
            chkFechaVencimiento.TabIndex = 16;
            chkFechaVencimiento.Text = "Tiene Fecha de Vencimiento";
            chkFechaVencimiento.UseVisualStyleBackColor = true;
            chkFechaVencimiento.CheckedChanged += chkFechaVencimiento_CheckedChanged_1;
            // 
            // nudStockInicial
            // 
            nudStockInicial.Location = new Point(3, 3);
            nudStockInicial.Maximum = new decimal(new int[] { 999999, 0, 0, 0 });
            nudStockInicial.Name = "nudStockInicial";
            nudStockInicial.Size = new Size(120, 23);
            nudStockInicial.TabIndex = 17;
            nudStockInicial.ValueChanged += nudStockInicial_ValueChanged;
            // 
            // nudStockActual
            // 
            nudStockActual.Location = new Point(3, 3);
            nudStockActual.Maximum = new decimal(new int[] { 999999, 0, 0, 0 });
            nudStockActual.Name = "nudStockActual";
            nudStockActual.Size = new Size(120, 23);
            nudStockActual.TabIndex = 18;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.Controls.Add(flowLayoutPanel5, 1, 2);
            tableLayoutPanel1.Controls.Add(flowLayoutPanel8, 1, 0);
            tableLayoutPanel1.Controls.Add(flowLayoutPanel2, 0, 0);
            tableLayoutPanel1.Controls.Add(flowLayoutPanel4, 0, 2);
            tableLayoutPanel1.Controls.Add(flowLayoutPanel3, 0, 1);
            tableLayoutPanel1.Controls.Add(flowLayoutPanel7, 1, 1);
            tableLayoutPanel1.Location = new Point(3, 69);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 3;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 56.8F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 43.2F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 59F));
            tableLayoutPanel1.Size = new Size(552, 181);
            tableLayoutPanel1.TabIndex = 20;
            // 
            // flowLayoutPanel5
            // 
            flowLayoutPanel5.Anchor = AnchorStyles.Right;
            flowLayoutPanel5.Controls.Add(lblFechaVencimientoLote);
            flowLayoutPanel5.Controls.Add(dtpFechaVencimiento);
            flowLayoutPanel5.Location = new Point(339, 129);
            flowLayoutPanel5.Name = "flowLayoutPanel5";
            flowLayoutPanel5.Size = new Size(210, 44);
            flowLayoutPanel5.TabIndex = 22;
            // 
            // flowLayoutPanel8
            // 
            flowLayoutPanel8.Anchor = AnchorStyles.Right;
            flowLayoutPanel8.Controls.Add(chkLoteEstaActivo);
            flowLayoutPanel8.Location = new Point(339, 19);
            flowLayoutPanel8.Name = "flowLayoutPanel8";
            flowLayoutPanel8.Size = new Size(210, 31);
            flowLayoutPanel8.TabIndex = 24;
            // 
            // flowLayoutPanel2
            // 
            flowLayoutPanel2.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            flowLayoutPanel2.Controls.Add(lblNumeroLote);
            flowLayoutPanel2.Controls.Add(txtNumeroLote);
            flowLayoutPanel2.Location = new Point(3, 11);
            flowLayoutPanel2.Name = "flowLayoutPanel2";
            flowLayoutPanel2.Size = new Size(270, 47);
            flowLayoutPanel2.TabIndex = 20;
            // 
            // flowLayoutPanel4
            // 
            flowLayoutPanel4.Anchor = AnchorStyles.Left;
            flowLayoutPanel4.Controls.Add(nudStockActual);
            flowLayoutPanel4.Controls.Add(label3);
            flowLayoutPanel4.Location = new Point(3, 134);
            flowLayoutPanel4.Name = "flowLayoutPanel4";
            flowLayoutPanel4.Size = new Size(270, 34);
            flowLayoutPanel4.TabIndex = 21;
            // 
            // flowLayoutPanel3
            // 
            flowLayoutPanel3.Controls.Add(nudStockInicial);
            flowLayoutPanel3.Controls.Add(label2);
            flowLayoutPanel3.Location = new Point(3, 72);
            flowLayoutPanel3.Name = "flowLayoutPanel3";
            flowLayoutPanel3.Size = new Size(270, 35);
            flowLayoutPanel3.TabIndex = 20;
            // 
            // flowLayoutPanel7
            // 
            flowLayoutPanel7.Anchor = AnchorStyles.Right;
            flowLayoutPanel7.Controls.Add(chkFechaVencimiento);
            flowLayoutPanel7.Location = new Point(339, 79);
            flowLayoutPanel7.Name = "flowLayoutPanel7";
            flowLayoutPanel7.Size = new Size(210, 31);
            flowLayoutPanel7.TabIndex = 23;
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tableLayoutPanel2.ColumnCount = 1;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel2.Controls.Add(flowLayoutPanel6, 0, 2);
            tableLayoutPanel2.Controls.Add(tableLayoutPanel1, 0, 1);
            tableLayoutPanel2.Controls.Add(tableLayoutPanel4, 0, 0);
            tableLayoutPanel2.Location = new Point(12, 59);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 3;
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 56.1194038F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 24.1791039F));
            tableLayoutPanel2.Size = new Size(558, 335);
            tableLayoutPanel2.TabIndex = 21;
            // 
            // flowLayoutPanel6
            // 
            flowLayoutPanel6.Controls.Add(tableLayoutPanel3);
            flowLayoutPanel6.Dock = DockStyle.Fill;
            flowLayoutPanel6.Location = new Point(3, 256);
            flowLayoutPanel6.Name = "flowLayoutPanel6";
            flowLayoutPanel6.Size = new Size(552, 76);
            flowLayoutPanel6.TabIndex = 22;
            // 
            // tableLayoutPanel3
            // 
            tableLayoutPanel3.ColumnCount = 1;
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel3.Controls.Add(lblDescripcionLote, 0, 0);
            tableLayoutPanel3.Controls.Add(txtDescripcionLote, 0, 1);
            tableLayoutPanel3.Location = new Point(3, 3);
            tableLayoutPanel3.Name = "tableLayoutPanel3";
            tableLayoutPanel3.RowCount = 2;
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 75F));
            tableLayoutPanel3.Size = new Size(549, 70);
            tableLayoutPanel3.TabIndex = 15;
            // 
            // tableLayoutPanel4
            // 
            tableLayoutPanel4.ColumnCount = 1;
            tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel4.Controls.Add(lblTituloLotes, 0, 0);
            tableLayoutPanel4.Controls.Add(lblNombreProducto, 0, 1);
            tableLayoutPanel4.Location = new Point(3, 3);
            tableLayoutPanel4.Name = "tableLayoutPanel4";
            tableLayoutPanel4.RowCount = 2;
            tableLayoutPanel4.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel4.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel4.Size = new Size(552, 60);
            tableLayoutPanel4.TabIndex = 23;
            // 
            // FGestionStockLotes
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(582, 395);
            Controls.Add(tableLayoutPanel2);
            ForeColor = Color.FromArgb(31, 26, 43);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "FGestionStockLotes";
            Text = "ABM Lote";
            Load += FGestionStockLotes_Load;
            Controls.SetChildIndex(tableLayoutPanel2, 0);
            ((System.ComponentModel.ISupportInitialize)error).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudStockInicial).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudStockActual).EndInit();
            tableLayoutPanel1.ResumeLayout(false);
            flowLayoutPanel5.ResumeLayout(false);
            flowLayoutPanel5.PerformLayout();
            flowLayoutPanel8.ResumeLayout(false);
            flowLayoutPanel8.PerformLayout();
            flowLayoutPanel2.ResumeLayout(false);
            flowLayoutPanel2.PerformLayout();
            flowLayoutPanel4.ResumeLayout(false);
            flowLayoutPanel4.PerformLayout();
            flowLayoutPanel3.ResumeLayout(false);
            flowLayoutPanel3.PerformLayout();
            flowLayoutPanel7.ResumeLayout(false);
            flowLayoutPanel7.PerformLayout();
            tableLayoutPanel2.ResumeLayout(false);
            flowLayoutPanel6.ResumeLayout(false);
            tableLayoutPanel3.ResumeLayout(false);
            tableLayoutPanel3.PerformLayout();
            tableLayoutPanel4.ResumeLayout(false);
            tableLayoutPanel4.PerformLayout();
            ResumeLayout(false);
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
        private FlowLayoutPanel flowLayoutPanel3;
        private TableLayoutPanel tableLayoutPanel1;
        private FlowLayoutPanel flowLayoutPanel2;
        private FlowLayoutPanel flowLayoutPanel5;
        private FlowLayoutPanel flowLayoutPanel4;
        private TableLayoutPanel tableLayoutPanel2;
        private FlowLayoutPanel flowLayoutPanel6;
        private TableLayoutPanel tableLayoutPanel3;
        private FlowLayoutPanel flowLayoutPanel8;
        private FlowLayoutPanel flowLayoutPanel7;
        private TableLayoutPanel tableLayoutPanel4;
    }
}