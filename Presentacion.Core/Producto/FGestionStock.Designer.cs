namespace Presentacion.Core.Producto
{
    partial class FGestionStock
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FGestionStock));
            lblTexto = new Label();
            btnCancelar = new Button();
            rdbAgregar = new RadioButton();
            rdbQuitar = new RadioButton();
            lblDetalle = new Label();
            lblMotivo = new Label();
            txtCantidad = new TextBox();
            txtMotivo = new TextBox();
            btnAccion = new Button();
            lblProductoCargado = new Label();
            lblProducto = new Label();
            tableLayoutPanel1 = new TableLayoutPanel();
            tableLayoutPanel4 = new TableLayoutPanel();
            tableLayoutPanel7 = new TableLayoutPanel();
            tableLayoutPanel6 = new TableLayoutPanel();
            tableLayoutPanel5 = new TableLayoutPanel();
            tableLayoutPanel2 = new TableLayoutPanel();
            tableLayoutPanel3 = new TableLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)error).BeginInit();
            tableLayoutPanel1.SuspendLayout();
            tableLayoutPanel4.SuspendLayout();
            tableLayoutPanel7.SuspendLayout();
            tableLayoutPanel6.SuspendLayout();
            tableLayoutPanel5.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            tableLayoutPanel3.SuspendLayout();
            SuspendLayout();
            // 
            // lblTexto
            // 
            lblTexto.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            lblTexto.AutoSize = true;
            lblTexto.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTexto.Location = new Point(3, 0);
            lblTexto.Name = "lblTexto";
            lblTexto.Size = new Size(243, 51);
            lblTexto.TabIndex = 0;
            lblTexto.Tag = "NoModificarConBase";
            lblTexto.Text = "¿Agregar o Quitar Stock?";
            lblTexto.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // btnCancelar
            // 
            btnCancelar.Location = new Point(251, 3);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new Size(164, 37);
            btnCancelar.TabIndex = 3;
            btnCancelar.Text = "Cancelar";
            btnCancelar.UseVisualStyleBackColor = true;
            btnCancelar.Click += btnCancelar_Click;
            // 
            // rdbAgregar
            // 
            rdbAgregar.Anchor = AnchorStyles.None;
            rdbAgregar.AutoSize = true;
            rdbAgregar.Location = new Point(11, 13);
            rdbAgregar.Name = "rdbAgregar";
            rdbAgregar.Size = new Size(99, 19);
            rdbAgregar.TabIndex = 4;
            rdbAgregar.TabStop = true;
            rdbAgregar.Text = "Agregar Stock";
            rdbAgregar.UseVisualStyleBackColor = true;
            rdbAgregar.CheckedChanged += rdbAgregar_CheckedChanged;
            // 
            // rdbQuitar
            // 
            rdbQuitar.Anchor = AnchorStyles.None;
            rdbQuitar.AutoSize = true;
            rdbQuitar.Location = new Point(138, 13);
            rdbQuitar.Name = "rdbQuitar";
            rdbQuitar.Size = new Size(90, 19);
            rdbQuitar.TabIndex = 5;
            rdbQuitar.TabStop = true;
            rdbQuitar.Text = "Quitar Stock";
            rdbQuitar.UseVisualStyleBackColor = true;
            rdbQuitar.CheckedChanged += rdbQuitar_CheckedChanged;
            // 
            // lblDetalle
            // 
            lblDetalle.Anchor = AnchorStyles.None;
            lblDetalle.AutoSize = true;
            lblDetalle.Location = new Point(5, 21);
            lblDetalle.Name = "lblDetalle";
            lblDetalle.Size = new Size(75, 15);
            lblDetalle.TabIndex = 6;
            lblDetalle.Text = "Monto Stock";
            // 
            // lblMotivo
            // 
            lblMotivo.Anchor = AnchorStyles.None;
            lblMotivo.AutoSize = true;
            lblMotivo.Location = new Point(21, 21);
            lblMotivo.Name = "lblMotivo";
            lblMotivo.Size = new Size(45, 15);
            lblMotivo.TabIndex = 7;
            lblMotivo.Text = "Motivo";
            // 
            // txtCantidad
            // 
            txtCantidad.Anchor = AnchorStyles.None;
            txtCantidad.Location = new Point(89, 17);
            txtCantidad.Name = "txtCantidad";
            txtCantidad.Size = new Size(151, 23);
            txtCantidad.TabIndex = 8;
            // 
            // txtMotivo
            // 
            txtMotivo.Location = new Point(90, 3);
            txtMotivo.Multiline = true;
            txtMotivo.Name = "txtMotivo";
            txtMotivo.ScrollBars = ScrollBars.Vertical;
            txtMotivo.Size = new Size(151, 51);
            txtMotivo.TabIndex = 9;
            // 
            // btnAccion
            // 
            btnAccion.Location = new Point(77, 3);
            btnAccion.Name = "btnAccion";
            btnAccion.Size = new Size(164, 37);
            btnAccion.TabIndex = 10;
            btnAccion.Text = "Acción";
            btnAccion.UseVisualStyleBackColor = true;
            btnAccion.Click += btnAccion_Click;
            // 
            // lblProductoCargado
            // 
            lblProductoCargado.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            lblProductoCargado.AutoSize = true;
            lblProductoCargado.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblProductoCargado.Location = new Point(3, 5);
            lblProductoCargado.Name = "lblProductoCargado";
            lblProductoCargado.Size = new Size(499, 30);
            lblProductoCargado.TabIndex = 11;
            lblProductoCargado.Tag = "NoModificarConBase";
            lblProductoCargado.Text = "Producto seleccionado: ";
            lblProductoCargado.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblProducto
            // 
            lblProducto.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            lblProducto.AutoSize = true;
            lblProducto.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblProducto.ForeColor = Color.FromArgb(67, 20, 135);
            lblProducto.Location = new Point(3, 48);
            lblProducto.Name = "lblProducto";
            lblProducto.Size = new Size(499, 30);
            lblProducto.TabIndex = 12;
            lblProducto.Tag = "NoModificarConBase";
            lblProducto.Text = "Producto";
            lblProducto.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            tableLayoutPanel1.ColumnCount = 1;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.Controls.Add(tableLayoutPanel4, 0, 3);
            tableLayoutPanel1.Controls.Add(tableLayoutPanel5, 0, 4);
            tableLayoutPanel1.Controls.Add(tableLayoutPanel2, 0, 2);
            tableLayoutPanel1.Controls.Add(lblProductoCargado, 0, 0);
            tableLayoutPanel1.Controls.Add(lblProducto, 0, 1);
            tableLayoutPanel1.Location = new Point(12, -3);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 5;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 47.826088F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 52.173912F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 57F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 69F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 63F));
            tableLayoutPanel1.Size = new Size(505, 276);
            tableLayoutPanel1.TabIndex = 13;
            // 
            // tableLayoutPanel4
            // 
            tableLayoutPanel4.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tableLayoutPanel4.ColumnCount = 2;
            tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 250F));
            tableLayoutPanel4.Controls.Add(tableLayoutPanel7, 0, 0);
            tableLayoutPanel4.Controls.Add(tableLayoutPanel6, 1, 0);
            tableLayoutPanel4.Location = new Point(3, 146);
            tableLayoutPanel4.Name = "tableLayoutPanel4";
            tableLayoutPanel4.RowCount = 1;
            tableLayoutPanel4.RowStyles.Add(new RowStyle(SizeType.Percent, 49.25373F));
            tableLayoutPanel4.Size = new Size(499, 63);
            tableLayoutPanel4.TabIndex = 14;
            // 
            // tableLayoutPanel7
            // 
            tableLayoutPanel7.Anchor = AnchorStyles.None;
            tableLayoutPanel7.ColumnCount = 2;
            tableLayoutPanel7.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 35.6998F));
            tableLayoutPanel7.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 64.3002F));
            tableLayoutPanel7.Controls.Add(txtCantidad, 1, 0);
            tableLayoutPanel7.Controls.Add(lblDetalle, 0, 0);
            tableLayoutPanel7.Location = new Point(3, 3);
            tableLayoutPanel7.Name = "tableLayoutPanel7";
            tableLayoutPanel7.RowCount = 1;
            tableLayoutPanel7.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel7.Size = new Size(243, 57);
            tableLayoutPanel7.TabIndex = 10;
            // 
            // tableLayoutPanel6
            // 
            tableLayoutPanel6.Anchor = AnchorStyles.None;
            tableLayoutPanel6.ColumnCount = 2;
            tableLayoutPanel6.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 36.02015F));
            tableLayoutPanel6.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 63.97985F));
            tableLayoutPanel6.Controls.Add(txtMotivo, 1, 0);
            tableLayoutPanel6.Controls.Add(lblMotivo, 0, 0);
            tableLayoutPanel6.Location = new Point(252, 3);
            tableLayoutPanel6.Name = "tableLayoutPanel6";
            tableLayoutPanel6.RowCount = 1;
            tableLayoutPanel6.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel6.Size = new Size(244, 57);
            tableLayoutPanel6.TabIndex = 9;
            // 
            // tableLayoutPanel5
            // 
            tableLayoutPanel5.Anchor = AnchorStyles.Bottom;
            tableLayoutPanel5.ColumnCount = 4;
            tableLayoutPanel5.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 15F));
            tableLayoutPanel5.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 35F));
            tableLayoutPanel5.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 35F));
            tableLayoutPanel5.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 15F));
            tableLayoutPanel5.Controls.Add(btnCancelar, 2, 0);
            tableLayoutPanel5.Controls.Add(btnAccion, 1, 0);
            tableLayoutPanel5.Location = new Point(3, 223);
            tableLayoutPanel5.Name = "tableLayoutPanel5";
            tableLayoutPanel5.RowCount = 1;
            tableLayoutPanel5.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel5.Size = new Size(499, 50);
            tableLayoutPanel5.TabIndex = 17;
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tableLayoutPanel2.ColumnCount = 2;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel2.Controls.Add(tableLayoutPanel3, 1, 0);
            tableLayoutPanel2.Controls.Add(lblTexto, 0, 0);
            tableLayoutPanel2.Location = new Point(3, 89);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 1;
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel2.Size = new Size(499, 51);
            tableLayoutPanel2.TabIndex = 14;
            // 
            // tableLayoutPanel3
            // 
            tableLayoutPanel3.ColumnCount = 2;
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel3.Controls.Add(rdbAgregar, 0, 0);
            tableLayoutPanel3.Controls.Add(rdbQuitar, 1, 0);
            tableLayoutPanel3.Location = new Point(252, 3);
            tableLayoutPanel3.Name = "tableLayoutPanel3";
            tableLayoutPanel3.RowCount = 1;
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel3.Size = new Size(244, 45);
            tableLayoutPanel3.TabIndex = 15;
            // 
            // FGestionStock
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(529, 274);
            Controls.Add(tableLayoutPanel1);
            ForeColor = Color.FromArgb(31, 26, 43);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "FGestionStock";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "ABM Stock Simple";
            Load += FGestionStock_Load;
            ((System.ComponentModel.ISupportInitialize)error).EndInit();
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            tableLayoutPanel4.ResumeLayout(false);
            tableLayoutPanel7.ResumeLayout(false);
            tableLayoutPanel7.PerformLayout();
            tableLayoutPanel6.ResumeLayout(false);
            tableLayoutPanel6.PerformLayout();
            tableLayoutPanel5.ResumeLayout(false);
            tableLayoutPanel2.ResumeLayout(false);
            tableLayoutPanel2.PerformLayout();
            tableLayoutPanel3.ResumeLayout(false);
            tableLayoutPanel3.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Label lblTexto;
        private Button btnCancelar;
        private RadioButton rdbAgregar;
        private RadioButton rdbQuitar;
        private Label lblDetalle;
        private Label lblMotivo;
        private TextBox txtCantidad;
        private TextBox txtMotivo;
        private Button btnAccion;
        private Label lblProductoCargado;
        private Label lblProducto;
        private TableLayoutPanel tableLayoutPanel1;
        private TableLayoutPanel tableLayoutPanel4;
        private TableLayoutPanel tableLayoutPanel5;
        private TableLayoutPanel tableLayoutPanel2;
        private TableLayoutPanel tableLayoutPanel3;
        private TableLayoutPanel tableLayoutPanel6;
        private TableLayoutPanel tableLayoutPanel7;
    }
}