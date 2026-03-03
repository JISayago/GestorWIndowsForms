namespace Presentacion.Core.Oferta
{
    partial class FOfertaABM
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
            btnCargarProducto = new Button();
            lblTitulo = new Label();
            lblFechaInicio = new Label();
            lblFechaFin = new Label();
            dtpFechaInicio = new DateTimePicker();
            dtpFechaFin = new DateTimePicker();
            dgvProductos = new DataGridView();
            lblCantidadProductos = new Label();
            lblDetalle = new Label();
            txtDetalle = new TextBox();
            txtDescripcion = new TextBox();
            lbl = new Label();
            btnCancelar = new Button();
            btnCrear = new Button();
            txtPrecioDescuentoPesos = new TextBox();
            btnLimpiar = new Button();
            cbxLimiteCumplirStock = new CheckBox();
            txtLimiteStock = new TextBox();
            lblLimiteStock = new Label();
            lblCodigoManual = new Label();
            lblPrecioOferta = new Label();
            label1 = new Label();
            txtPrecioVentaReal = new TextBox();
            lblTotalPrecioCosto = new Label();
            txtPrecioCostoAcumulado = new TextBox();
            ((System.ComponentModel.ISupportInitialize)error).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvProductos).BeginInit();
            SuspendLayout();
            // 
            // btnCargarProducto
            // 
            btnCargarProducto.Location = new Point(111, 85);
            btnCargarProducto.Name = "btnCargarProducto";
            btnCargarProducto.Size = new Size(161, 32);
            btnCargarProducto.TabIndex = 14;
            btnCargarProducto.Text = "Cargar Producto/s";
            btnCargarProducto.UseVisualStyleBackColor = true;
            btnCargarProducto.Click += btnCargarProducto_Click;
            // 
            // lblTitulo
            // 
            lblTitulo.AutoSize = true;
            lblTitulo.Font = new Font("Segoe UI", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblTitulo.Location = new Point(307, 9);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(181, 30);
            lblTitulo.TabIndex = 17;
            lblTitulo.Text = "Armado de Oferta";
            // 
            // lblFechaInicio
            // 
            lblFechaInicio.AutoSize = true;
            lblFechaInicio.Location = new Point(189, 258);
            lblFechaInicio.Name = "lblFechaInicio";
            lblFechaInicio.Size = new Size(70, 15);
            lblFechaInicio.TabIndex = 18;
            lblFechaInicio.Text = "Fecha Inicio";
            // 
            // lblFechaFin
            // 
            lblFechaFin.AutoSize = true;
            lblFechaFin.Location = new Point(451, 258);
            lblFechaFin.Name = "lblFechaFin";
            lblFechaFin.Size = new Size(57, 15);
            lblFechaFin.TabIndex = 19;
            lblFechaFin.Text = "Fecha Fin";
            // 
            // dtpFechaInicio
            // 
            dtpFechaInicio.Location = new Point(189, 276);
            dtpFechaInicio.Name = "dtpFechaInicio";
            dtpFechaInicio.Size = new Size(200, 23);
            dtpFechaInicio.TabIndex = 20;
            dtpFechaInicio.ValueChanged += dtpFechaInicio_ValueChanged;
            // 
            // dtpFechaFin
            // 
            dtpFechaFin.Location = new Point(451, 276);
            dtpFechaFin.Name = "dtpFechaFin";
            dtpFechaFin.Size = new Size(200, 23);
            dtpFechaFin.TabIndex = 21;
            dtpFechaFin.ValueChanged += dtpFechaFin_ValueChanged;
            // 
            // dgvProductos
            // 
            dgvProductos.AllowUserToAddRows = false;
            dgvProductos.AllowUserToDeleteRows = false;
            dgvProductos.AllowUserToResizeRows = false;
            dgvProductos.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvProductos.EditMode = DataGridViewEditMode.EditProgrammatically;
            dgvProductos.Location = new Point(111, 123);
            dgvProductos.MultiSelect = false;
            dgvProductos.Name = "dgvProductos";
            dgvProductos.Size = new Size(602, 119);
            dgvProductos.TabIndex = 25;
            // 
            // lblCantidadProductos
            // 
            lblCantidadProductos.AutoSize = true;
            lblCantidadProductos.Location = new Point(469, 94);
            lblCantidadProductos.Name = "lblCantidadProductos";
            lblCantidadProductos.Size = new Size(115, 15);
            lblCantidadProductos.TabIndex = 26;
            lblCantidadProductos.Text = "Cantidad Productos:";
            // 
            // lblDetalle
            // 
            lblDetalle.AutoSize = true;
            lblDetalle.Location = new Point(83, 661);
            lblDetalle.Name = "lblDetalle";
            lblDetalle.Size = new Size(43, 15);
            lblDetalle.TabIndex = 57;
            lblDetalle.Text = "Detalle";
            // 
            // txtDetalle
            // 
            txtDetalle.Location = new Point(83, 679);
            txtDetalle.Multiline = true;
            txtDetalle.Name = "txtDetalle";
            txtDetalle.Size = new Size(629, 50);
            txtDetalle.TabIndex = 56;
            // 
            // txtDescripcion
            // 
            txtDescripcion.Location = new Point(83, 354);
            txtDescripcion.Multiline = true;
            txtDescripcion.Name = "txtDescripcion";
            txtDescripcion.ReadOnly = true;
            txtDescripcion.Size = new Size(602, 50);
            txtDescripcion.TabIndex = 55;
            // 
            // lbl
            // 
            lbl.AutoSize = true;
            lbl.Location = new Point(83, 336);
            lbl.Name = "lbl";
            lbl.Size = new Size(69, 15);
            lbl.TabIndex = 54;
            lbl.Text = "Descripcion";
            // 
            // btnCancelar
            // 
            btnCancelar.Location = new Point(308, 764);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new Size(177, 57);
            btnCancelar.TabIndex = 53;
            btnCancelar.Text = "Cancelar";
            btnCancelar.UseVisualStyleBackColor = true;
            // 
            // btnCrear
            // 
            btnCrear.Location = new Point(536, 764);
            btnCrear.Name = "btnCrear";
            btnCrear.Size = new Size(177, 57);
            btnCrear.TabIndex = 52;
            btnCrear.Text = "Crear";
            btnCrear.UseVisualStyleBackColor = true;
            btnCrear.Click += btnCrear_Click_1;
            // 
            // txtPrecioDescuentoPesos
            // 
            txtPrecioDescuentoPesos.Location = new Point(232, 550);
            txtPrecioDescuentoPesos.Name = "txtPrecioDescuentoPesos";
            txtPrecioDescuentoPesos.Size = new Size(285, 23);
            txtPrecioDescuentoPesos.TabIndex = 48;
            // 
            // btnLimpiar
            // 
            btnLimpiar.Location = new Point(81, 764);
            btnLimpiar.Name = "btnLimpiar";
            btnLimpiar.Size = new Size(177, 57);
            btnLimpiar.TabIndex = 64;
            btnLimpiar.Text = "Limpiar ";
            btnLimpiar.UseVisualStyleBackColor = true;
            btnLimpiar.Click += btnLimpiar_Click;
            // 
            // cbxLimiteCumplirStock
            // 
            cbxLimiteCumplirStock.AutoSize = true;
            cbxLimiteCumplirStock.Location = new Point(196, 615);
            cbxLimiteCumplirStock.Name = "cbxLimiteCumplirStock";
            cbxLimiteCumplirStock.Size = new Size(131, 19);
            cbxLimiteCumplirStock.TabIndex = 69;
            cbxLimiteCumplirStock.Text = "Hasta cumplir stock";
            cbxLimiteCumplirStock.UseVisualStyleBackColor = true;
            cbxLimiteCumplirStock.CheckedChanged += cbxLimiteCumplirStock_CheckedChanged;
            // 
            // txtLimiteStock
            // 
            txtLimiteStock.Enabled = false;
            txtLimiteStock.Location = new Point(469, 615);
            txtLimiteStock.Name = "txtLimiteStock";
            txtLimiteStock.Size = new Size(137, 23);
            txtLimiteStock.TabIndex = 70;
            // 
            // lblLimiteStock
            // 
            lblLimiteStock.AutoSize = true;
            lblLimiteStock.Location = new Point(356, 618);
            lblLimiteStock.Name = "lblLimiteStock";
            lblLimiteStock.Size = new Size(107, 15);
            lblLimiteStock.TabIndex = 71;
            lblLimiteStock.Text = "Cantidad en Oferta";
            // 
            // lblCodigoManual
            // 
            lblCodigoManual.AutoSize = true;
            lblCodigoManual.Location = new Point(315, 43);
            lblCodigoManual.Name = "lblCodigoManual";
            lblCodigoManual.Size = new Size(0, 15);
            lblCodigoManual.TabIndex = 72;
            // 
            // lblPrecioOferta
            // 
            lblPrecioOferta.AutoSize = true;
            lblPrecioOferta.Location = new Point(317, 522);
            lblPrecioOferta.Name = "lblPrecioOferta";
            lblPrecioOferta.Size = new Size(104, 15);
            lblPrecioOferta.TabIndex = 73;
            lblPrecioOferta.Text = "Precio Final Oferta";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(494, 436);
            label1.Name = "label1";
            label1.Size = new Size(97, 15);
            label1.TabIndex = 75;
            label1.Text = "Precio Venta Real";
            // 
            // txtPrecioVentaReal
            // 
            txtPrecioVentaReal.Enabled = false;
            txtPrecioVentaReal.Location = new Point(409, 464);
            txtPrecioVentaReal.Name = "txtPrecioVentaReal";
            txtPrecioVentaReal.Size = new Size(285, 23);
            txtPrecioVentaReal.TabIndex = 74;
            // 
            // lblTotalPrecioCosto
            // 
            lblTotalPrecioCosto.AutoSize = true;
            lblTotalPrecioCosto.Location = new Point(162, 436);
            lblTotalPrecioCosto.Name = "lblTotalPrecioCosto";
            lblTotalPrecioCosto.Size = new Size(139, 15);
            lblTotalPrecioCosto.TabIndex = 77;
            lblTotalPrecioCosto.Text = "Precio Costo Acumulado";
            // 
            // txtPrecioCostoAcumulado
            // 
            txtPrecioCostoAcumulado.Enabled = false;
            txtPrecioCostoAcumulado.Location = new Point(77, 464);
            txtPrecioCostoAcumulado.Name = "txtPrecioCostoAcumulado";
            txtPrecioCostoAcumulado.Size = new Size(285, 23);
            txtPrecioCostoAcumulado.TabIndex = 76;
            // 
            // FOfertaABM
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(784, 854);
            Controls.Add(lblTotalPrecioCosto);
            Controls.Add(txtPrecioCostoAcumulado);
            Controls.Add(label1);
            Controls.Add(txtPrecioVentaReal);
            Controls.Add(lblPrecioOferta);
            Controls.Add(lblCodigoManual);
            Controls.Add(lblLimiteStock);
            Controls.Add(txtLimiteStock);
            Controls.Add(cbxLimiteCumplirStock);
            Controls.Add(btnLimpiar);
            Controls.Add(lblDetalle);
            Controls.Add(txtDetalle);
            Controls.Add(txtDescripcion);
            Controls.Add(lbl);
            Controls.Add(btnCancelar);
            Controls.Add(btnCrear);
            Controls.Add(txtPrecioDescuentoPesos);
            Controls.Add(lblCantidadProductos);
            Controls.Add(dgvProductos);
            Controls.Add(dtpFechaFin);
            Controls.Add(dtpFechaInicio);
            Controls.Add(lblFechaFin);
            Controls.Add(lblFechaInicio);
            Controls.Add(lblTitulo);
            Controls.Add(btnCargarProducto);
            Name = "FOfertaABM";
            Text = "FOfertaABM";
            Load += FOfertaABM_Load;
            ((System.ComponentModel.ISupportInitialize)error).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvProductos).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Button btnCargarProducto;
        private Label lblTitulo;
        private Label lblFechaInicio;
        private Label lblFechaFin;
        private DateTimePicker dtpFechaInicio;
        private DateTimePicker dtpFechaFin;
        private DataGridView dgvProductos;
        private Label lblCantidadProductos;
        private Label lblDetalle;
        private TextBox txtDetalle;
        private TextBox txtDescripcion;
        private Label lbl;
        private Button btnCancelar;
        private Button btnCrear;
        private TextBox txtPrecioDescuentoPesos;
        private Button btnLimpiar;
        private CheckBox cbxLimiteCumplirStock;
        private TextBox txtLimiteStock;
        private Label lblLimiteStock;
        private Label lblCodigoManual;
        private Label lblPrecioOferta;
        private Label label1;
        private TextBox txtPrecioVentaReal;
        private Label lblTotalPrecioCosto;
        private TextBox txtPrecioCostoAcumulado;
    }
}