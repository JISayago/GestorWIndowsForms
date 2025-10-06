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
            cbxCombinacionProductos = new CheckBox();
            btnCargarProducto = new Button();
            lblTitulo = new Label();
            lblFechaInicio = new Label();
            lblFechaFin = new Label();
            dtpFechaInicio = new DateTimePicker();
            dtpFechaFin = new DateTimePicker();
            cbxEstaActiva = new CheckBox();
            dgvProductos = new DataGridView();
            lblCantidadProductos = new Label();
            cbx2x1 = new CheckBox();
            lblCodigo = new Label();
            txtCodigoOferta = new TextBox();
            lblDetalle = new Label();
            txtDetalle = new TextBox();
            txtDescripcion = new TextBox();
            lbl = new Label();
            btnCancelar = new Button();
            btnCrear = new Button();
            cbxDescuentoPesos = new CheckBox();
            txtPrecioDescuentoPorcentaje = new TextBox();
            txtPrecioDescuentoPesos = new TextBox();
            lblTotalPrecioReal = new Label();
            lblPerdida = new Label();
            btnCalcular = new Button();
            btnLimpiar = new Button();
            lblPrecioTotalDeLaOferta = new Label();
            txtPrecioTotalRealProductos = new TextBox();
            txtPrecioTotalPerdido = new TextBox();
            txtPrecioTotalOfertaAplicada = new TextBox();
            cbxLimiteCumplirStock = new CheckBox();
            txtLimiteStock = new TextBox();
            lblLimiteStock = new Label();
            cbxDescuentoPorcentaje = new CheckBox();
            ((System.ComponentModel.ISupportInitialize)error).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvProductos).BeginInit();
            SuspendLayout();
            // 
            // cbxCombinacionProductos
            // 
            cbxCombinacionProductos.AutoSize = true;
            cbxCombinacionProductos.Location = new Point(108, 53);
            cbxCombinacionProductos.Name = "cbxCombinacionProductos";
            cbxCombinacionProductos.Size = new Size(308, 19);
            cbxCombinacionProductos.TabIndex = 3;
            cbxCombinacionProductos.Text = "Es una combinación de 2 productos diferentes o más.";
            cbxCombinacionProductos.UseVisualStyleBackColor = true;
            cbxCombinacionProductos.CheckedChanged += cbxCantidadProductos_CheckedChanged;
            // 
            // btnCargarProducto
            // 
            btnCargarProducto.Location = new Point(84, 210);
            btnCargarProducto.Name = "btnCargarProducto";
            btnCargarProducto.Size = new Size(161, 32);
            btnCargarProducto.TabIndex = 14;
            btnCargarProducto.Text = "Cargar Producto";
            btnCargarProducto.UseVisualStyleBackColor = true;
            btnCargarProducto.Click += btnCargarProducto_Click;
            // 
            // lblTitulo
            // 
            lblTitulo.AutoSize = true;
            lblTitulo.Font = new Font("Segoe UI", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblTitulo.Location = new Point(90, 9);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(611, 30);
            lblTitulo.TabIndex = 17;
            lblTitulo.Text = "Seleccione las características que va a tener la Oferta/Descuento";
            // 
            // lblFechaInicio
            // 
            lblFechaInicio.AutoSize = true;
            lblFechaInicio.Location = new Point(146, 784);
            lblFechaInicio.Name = "lblFechaInicio";
            lblFechaInicio.Size = new Size(70, 15);
            lblFechaInicio.TabIndex = 18;
            lblFechaInicio.Text = "Fecha Inicio";
            // 
            // lblFechaFin
            // 
            lblFechaFin.AutoSize = true;
            lblFechaFin.Location = new Point(408, 784);
            lblFechaFin.Name = "lblFechaFin";
            lblFechaFin.Size = new Size(57, 15);
            lblFechaFin.TabIndex = 19;
            lblFechaFin.Text = "Fecha Fin";
            // 
            // dtpFechaInicio
            // 
            dtpFechaInicio.Location = new Point(146, 802);
            dtpFechaInicio.Name = "dtpFechaInicio";
            dtpFechaInicio.Size = new Size(200, 23);
            dtpFechaInicio.TabIndex = 20;
            dtpFechaInicio.ValueChanged += dtpFechaInicio_ValueChanged;
            // 
            // dtpFechaFin
            // 
            dtpFechaFin.Location = new Point(408, 802);
            dtpFechaFin.Name = "dtpFechaFin";
            dtpFechaFin.Size = new Size(200, 23);
            dtpFechaFin.TabIndex = 21;
            dtpFechaFin.ValueChanged += dtpFechaFin_ValueChanged;
            // 
            // cbxEstaActiva
            // 
            cbxEstaActiva.AutoSize = true;
            cbxEstaActiva.Location = new Point(602, 53);
            cbxEstaActiva.Name = "cbxEstaActiva";
            cbxEstaActiva.Size = new Size(135, 19);
            cbxEstaActiva.TabIndex = 22;
            cbxEstaActiva.Text = "La Oferta está activa.";
            cbxEstaActiva.UseVisualStyleBackColor = true;
            cbxEstaActiva.CheckedChanged += cbxEstaActiva_CheckedChanged;
            // 
            // dgvProductos
            // 
            dgvProductos.AllowUserToAddRows = false;
            dgvProductos.AllowUserToDeleteRows = false;
            dgvProductos.AllowUserToResizeRows = false;
            dgvProductos.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvProductos.EditMode = DataGridViewEditMode.EditProgrammatically;
            dgvProductos.Location = new Point(84, 248);
            dgvProductos.MultiSelect = false;
            dgvProductos.Name = "dgvProductos";
            dgvProductos.Size = new Size(602, 119);
            dgvProductos.TabIndex = 25;
            // 
            // lblCantidadProductos
            // 
            lblCantidadProductos.AutoSize = true;
            lblCantidadProductos.Location = new Point(474, 227);
            lblCantidadProductos.Name = "lblCantidadProductos";
            lblCantidadProductos.Size = new Size(115, 15);
            lblCantidadProductos.TabIndex = 26;
            lblCantidadProductos.Text = "Cantidad Productos:";
            // 
            // cbx2x1
            // 
            cbx2x1.AutoSize = true;
            cbx2x1.Location = new Point(108, 78);
            cbx2x1.Name = "cbx2x1";
            cbx2x1.Size = new Size(250, 19);
            cbx2x1.TabIndex = 33;
            cbx2x1.Text = "Es mas de 1 producto del mismo tipo (2x1)";
            cbx2x1.UseVisualStyleBackColor = true;
            cbx2x1.CheckedChanged += cbx2x1_CheckedChanged;
            // 
            // lblCodigo
            // 
            lblCodigo.AutoSize = true;
            lblCodigo.Location = new Point(84, 375);
            lblCodigo.Name = "lblCodigo";
            lblCodigo.Size = new Size(108, 15);
            lblCodigo.TabIndex = 59;
            lblCodigo.Text = "Codigo de la oferta";
            // 
            // txtCodigoOferta
            // 
            txtCodigoOferta.Location = new Point(84, 393);
            txtCodigoOferta.Name = "txtCodigoOferta";
            txtCodigoOferta.Size = new Size(602, 23);
            txtCodigoOferta.TabIndex = 58;
            // 
            // lblDetalle
            // 
            lblDetalle.AutoSize = true;
            lblDetalle.Location = new Point(83, 512);
            lblDetalle.Name = "lblDetalle";
            lblDetalle.Size = new Size(43, 15);
            lblDetalle.TabIndex = 57;
            lblDetalle.Text = "Detalle";
            // 
            // txtDetalle
            // 
            txtDetalle.Location = new Point(83, 530);
            txtDetalle.Multiline = true;
            txtDetalle.Name = "txtDetalle";
            txtDetalle.Size = new Size(602, 50);
            txtDetalle.TabIndex = 56;
            // 
            // txtDescripcion
            // 
            txtDescripcion.Location = new Point(84, 445);
            txtDescripcion.Multiline = true;
            txtDescripcion.Name = "txtDescripcion";
            txtDescripcion.ReadOnly = true;
            txtDescripcion.Size = new Size(602, 50);
            txtDescripcion.TabIndex = 55;
            // 
            // lbl
            // 
            lbl.AutoSize = true;
            lbl.Location = new Point(84, 427);
            lbl.Name = "lbl";
            lbl.Size = new Size(69, 15);
            lbl.TabIndex = 54;
            lbl.Text = "Descripcion";
            // 
            // btnCancelar
            // 
            btnCancelar.Location = new Point(390, 870);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new Size(75, 23);
            btnCancelar.TabIndex = 53;
            btnCancelar.Text = "Cancelar";
            btnCancelar.UseVisualStyleBackColor = true;
            // 
            // btnCrear
            // 
            btnCrear.Location = new Point(296, 870);
            btnCrear.Name = "btnCrear";
            btnCrear.Size = new Size(75, 23);
            btnCrear.TabIndex = 52;
            btnCrear.Text = "Crear";
            btnCrear.UseVisualStyleBackColor = true;
            btnCrear.Click += btnCrear_Click_1;
            // 
            // cbxDescuentoPesos
            // 
            cbxDescuentoPesos.AutoSize = true;
            cbxDescuentoPesos.Location = new Point(83, 619);
            cbxDescuentoPesos.Name = "cbxDescuentoPesos";
            cbxDescuentoPesos.Size = new Size(201, 19);
            cbxDescuentoPesos.TabIndex = 50;
            cbxDescuentoPesos.Text = "Precio de la Oferta/Descuento ($)";
            cbxDescuentoPesos.UseVisualStyleBackColor = true;
            cbxDescuentoPesos.CheckedChanged += cbxDescuentoPesos_CheckedChanged;
            // 
            // txtPrecioDescuentoPorcentaje
            // 
            txtPrecioDescuentoPorcentaje.Location = new Point(377, 640);
            txtPrecioDescuentoPorcentaje.Name = "txtPrecioDescuentoPorcentaje";
            txtPrecioDescuentoPorcentaje.Size = new Size(301, 23);
            txtPrecioDescuentoPorcentaje.TabIndex = 49;
            // 
            // txtPrecioDescuentoPesos
            // 
            txtPrecioDescuentoPesos.Location = new Point(76, 640);
            txtPrecioDescuentoPesos.Name = "txtPrecioDescuentoPesos";
            txtPrecioDescuentoPesos.Size = new Size(285, 23);
            txtPrecioDescuentoPesos.TabIndex = 48;
            // 
            // lblTotalPrecioReal
            // 
            lblTotalPrecioReal.AutoSize = true;
            lblTotalPrecioReal.Location = new Point(82, 719);
            lblTotalPrecioReal.Name = "lblTotalPrecioReal";
            lblTotalPrecioReal.Size = new Size(94, 15);
            lblTotalPrecioReal.TabIndex = 60;
            lblTotalPrecioReal.Text = "Precio Total Real";
            // 
            // lblPerdida
            // 
            lblPerdida.AutoSize = true;
            lblPerdida.Location = new Point(334, 719);
            lblPerdida.Name = "lblPerdida";
            lblPerdida.Size = new Size(77, 15);
            lblPerdida.TabIndex = 61;
            lblPerdida.Text = "Total Perdido";
            // 
            // btnCalcular
            // 
            btnCalcular.Location = new Point(134, 679);
            btnCalcular.Name = "btnCalcular";
            btnCalcular.Size = new Size(177, 23);
            btnCalcular.TabIndex = 63;
            btnCalcular.Text = "Calcular";
            btnCalcular.UseVisualStyleBackColor = true;
            btnCalcular.Click += btnCalcular_Click;
            // 
            // btnLimpiar
            // 
            btnLimpiar.Location = new Point(422, 679);
            btnLimpiar.Name = "btnLimpiar";
            btnLimpiar.Size = new Size(177, 23);
            btnLimpiar.TabIndex = 64;
            btnLimpiar.Text = "Limpiar campos";
            btnLimpiar.UseVisualStyleBackColor = true;
            btnLimpiar.Click += btnLimpiar_Click;
            // 
            // lblPrecioTotalDeLaOferta
            // 
            lblPrecioTotalDeLaOferta.AutoSize = true;
            lblPrecioTotalDeLaOferta.Location = new Point(561, 719);
            lblPrecioTotalDeLaOferta.Name = "lblPrecioTotalDeLaOferta";
            lblPrecioTotalDeLaOferta.Size = new Size(133, 15);
            lblPrecioTotalDeLaOferta.TabIndex = 65;
            lblPrecioTotalDeLaOferta.Text = "Precio Total de la Oferta";
            // 
            // txtPrecioTotalRealProductos
            // 
            txtPrecioTotalRealProductos.Location = new Point(61, 737);
            txtPrecioTotalRealProductos.Name = "txtPrecioTotalRealProductos";
            txtPrecioTotalRealProductos.ReadOnly = true;
            txtPrecioTotalRealProductos.Size = new Size(155, 23);
            txtPrecioTotalRealProductos.TabIndex = 66;
            // 
            // txtPrecioTotalPerdido
            // 
            txtPrecioTotalPerdido.Location = new Point(296, 737);
            txtPrecioTotalPerdido.Name = "txtPrecioTotalPerdido";
            txtPrecioTotalPerdido.ReadOnly = true;
            txtPrecioTotalPerdido.Size = new Size(155, 23);
            txtPrecioTotalPerdido.TabIndex = 67;
            // 
            // txtPrecioTotalOfertaAplicada
            // 
            txtPrecioTotalOfertaAplicada.Location = new Point(541, 737);
            txtPrecioTotalOfertaAplicada.Name = "txtPrecioTotalOfertaAplicada";
            txtPrecioTotalOfertaAplicada.ReadOnly = true;
            txtPrecioTotalOfertaAplicada.Size = new Size(155, 23);
            txtPrecioTotalOfertaAplicada.TabIndex = 68;
            // 
            // cbxLimiteCumplirStock
            // 
            cbxLimiteCumplirStock.AutoSize = true;
            cbxLimiteCumplirStock.Location = new Point(602, 78);
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
            txtLimiteStock.Location = new Point(593, 129);
            txtLimiteStock.Name = "txtLimiteStock";
            txtLimiteStock.Size = new Size(137, 23);
            txtLimiteStock.TabIndex = 70;
            // 
            // lblLimiteStock
            // 
            lblLimiteStock.AutoSize = true;
            lblLimiteStock.Location = new Point(593, 111);
            lblLimiteStock.Name = "lblLimiteStock";
            lblLimiteStock.Size = new Size(140, 15);
            lblLimiteStock.TabIndex = 71;
            lblLimiteStock.Text = "Limite de Stock en Oferta";
            // 
            // cbxDescuentoPorcentaje
            // 
            cbxDescuentoPorcentaje.AutoSize = true;
            cbxDescuentoPorcentaje.Location = new Point(377, 621);
            cbxDescuentoPorcentaje.Name = "cbxDescuentoPorcentaje";
            cbxDescuentoPorcentaje.Size = new Size(201, 19);
            cbxDescuentoPorcentaje.TabIndex = 51;
            cbxDescuentoPorcentaje.Text = "Precio de la Oferta/Descuento ($)";
            cbxDescuentoPorcentaje.UseVisualStyleBackColor = true;
            cbxDescuentoPorcentaje.CheckedChanged += cbxDescuentoPorcentaje_CheckedChanged;
            // 
            // FOfertaABM
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(784, 929);
            Controls.Add(lblLimiteStock);
            Controls.Add(txtLimiteStock);
            Controls.Add(cbxLimiteCumplirStock);
            Controls.Add(txtPrecioTotalOfertaAplicada);
            Controls.Add(txtPrecioTotalPerdido);
            Controls.Add(txtPrecioTotalRealProductos);
            Controls.Add(lblPrecioTotalDeLaOferta);
            Controls.Add(btnLimpiar);
            Controls.Add(btnCalcular);
            Controls.Add(lblPerdida);
            Controls.Add(lblTotalPrecioReal);
            Controls.Add(lblCodigo);
            Controls.Add(txtCodigoOferta);
            Controls.Add(lblDetalle);
            Controls.Add(txtDetalle);
            Controls.Add(txtDescripcion);
            Controls.Add(lbl);
            Controls.Add(btnCancelar);
            Controls.Add(btnCrear);
            Controls.Add(cbxDescuentoPorcentaje);
            Controls.Add(cbxDescuentoPesos);
            Controls.Add(txtPrecioDescuentoPorcentaje);
            Controls.Add(txtPrecioDescuentoPesos);
            Controls.Add(cbx2x1);
            Controls.Add(lblCantidadProductos);
            Controls.Add(dgvProductos);
            Controls.Add(cbxEstaActiva);
            Controls.Add(dtpFechaFin);
            Controls.Add(dtpFechaInicio);
            Controls.Add(lblFechaFin);
            Controls.Add(lblFechaInicio);
            Controls.Add(lblTitulo);
            Controls.Add(btnCargarProducto);
            Controls.Add(cbxCombinacionProductos);
            Name = "FOfertaABM";
            Text = "FOfertaABM";
            Load += FOfertaABM_Load;
            ((System.ComponentModel.ISupportInitialize)error).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvProductos).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private CheckBox cbxCombinacionProductos;
        private Button btnCargarProducto;
        private Label lblTitulo;
        private Label lblFechaInicio;
        private Label lblFechaFin;
        private DateTimePicker dtpFechaInicio;
        private DateTimePicker dtpFechaFin;
        private CheckBox cbxEstaActiva;
        private DataGridView dgvProductos;
        private Label lblCantidadProductos;
        private CheckBox cbx2x1;
        private Label lblCodigo;
        private TextBox txtCodigoOferta;
        private Label lblDetalle;
        private TextBox txtDetalle;
        private TextBox txtDescripcion;
        private Label lbl;
        private Button btnCancelar;
        private Button btnCrear;
        private CheckBox cbxDescuentoPesos;
        private TextBox txtPrecioDescuentoPorcentaje;
        private TextBox txtPrecioDescuentoPesos;
        private Label lblTotalPrecioReal;
        private Label lblPerdida;
        private Button btnCalcular;
        private Button btnLimpiar;
        private Label lblPrecioTotalDeLaOferta;
        private TextBox txtPrecioTotalRealProductos;
        private TextBox txtPrecioTotalPerdido;
        private TextBox txtPrecioTotalOfertaAplicada;
        private CheckBox cbxLimiteCumplirStock;
        private TextBox txtLimiteStock;
        private Label lblLimiteStock;
        private CheckBox cbxDescuentoPorcentaje;
    }
}