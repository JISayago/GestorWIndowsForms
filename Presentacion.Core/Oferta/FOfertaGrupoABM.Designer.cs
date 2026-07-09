namespace Presentacion.Core.Oferta
{
    partial class FOfertaGrupoABM
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
            lblCantidadProductos = new Label();
            btnCargarGrupoMarca = new Button();
            lblTitulo = new Label();
            txtMarca = new TextBox();
            lblNumeroProductoAfectados = new Label();
            btnCancelar = new Button();
            btnCrear = new Button();
            cbxDescuentoPorcentaje = new CheckBox();
            cbxDescuentoPesos = new CheckBox();
            txtMontoPorcentaje = new TextBox();
            txtDescripcion = new TextBox();
            lbl = new Label();
            txtDetalle = new TextBox();
            lblDetalle = new Label();
            dtpFechaFin = new DateTimePicker();
            dtpFechaInicio = new DateTimePicker();
            lblFechaFin = new Label();
            lblFechaInicio = new Label();
            txtRubro = new TextBox();
            btnCargarGrupoRubro = new Button();
            txtCategoria = new TextBox();
            btnCargarGrupoCategoria = new Button();
            label1 = new Label();
            dgvProductos = new DataGridView();
            btnQuitarProducto = new Button();
            btnCargarProductosAlcanzados = new Button();
            btnDevolverAOferta = new Button();
            label2 = new Label();
            dgvProductosQuitados = new DataGridView();
            lblNumeroProductoQuitados = new Label();
            lblCantidadProductosQuitados = new Label();
            cbxLimiteCumplirStock = new CheckBox();
            lblLimiteStock = new Label();
            txtLimiteStock = new TextBox();
            btnLimpiar = new Button();
            lblCodigoManual = new Label();
            btnCargarProducto = new Button();
            lblTotalPrecioCosto = new Label();
            txtPrecioCostoAcumulado = new TextBox();
            lblTotalPrecioReal = new Label();
            txtPrecioVentaReal = new TextBox();
            tableLayoutPanel1 = new TableLayoutPanel();
            tableLayoutPanel2 = new TableLayoutPanel();
            label3 = new Label();
            ((System.ComponentModel.ISupportInitialize)dgvProductos).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvProductosQuitados).BeginInit();
            tableLayoutPanel1.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            SuspendLayout();
            // 
            // lblCantidadProductos
            // 
            lblCantidadProductos.AutoSize = true;
            lblCantidadProductos.Location = new Point(585, 195);
            lblCantidadProductos.Name = "lblCantidadProductos";
            lblCantidadProductos.Size = new Size(245, 15);
            lblCantidadProductos.TabIndex = 29;
            lblCantidadProductos.Text = "Cantidad Productos Alcanzados por la oferta:";
            // 
            // btnCargarGrupoMarca
            // 
            btnCargarGrupoMarca.Enabled = false;
            btnCargarGrupoMarca.Location = new Point(3, 3);
            btnCargarGrupoMarca.Name = "btnCargarGrupoMarca";
            btnCargarGrupoMarca.Size = new Size(142, 32);
            btnCargarGrupoMarca.TabIndex = 27;
            btnCargarGrupoMarca.Text = "Cargar Grupo";
            btnCargarGrupoMarca.UseVisualStyleBackColor = true;
            btnCargarGrupoMarca.Click += btnCargarGrupoMarca_Click;
            // 
            // lblTitulo
            // 
            lblTitulo.AutoSize = true;
            lblTitulo.Font = new Font("Segoe UI", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblTitulo.Location = new Point(711, 482);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(611, 30);
            lblTitulo.TabIndex = 33;
            lblTitulo.Tag = "NoModificarConBase";
            lblTitulo.Text = "Por favor elija el grupo que va a ser considerado para esta oferta";
            // 
            // txtMarca
            // 
            txtMarca.Location = new Point(3, 52);
            txtMarca.Name = "txtMarca";
            txtMarca.ReadOnly = true;
            txtMarca.Size = new Size(142, 23);
            txtMarca.TabIndex = 34;
            txtMarca.TextAlign = HorizontalAlignment.Center;
            // 
            // lblNumeroProductoAfectados
            // 
            lblNumeroProductoAfectados.AutoSize = true;
            lblNumeroProductoAfectados.Font = new Font("Segoe UI", 20.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblNumeroProductoAfectados.Location = new Point(854, 177);
            lblNumeroProductoAfectados.Name = "lblNumeroProductoAfectados";
            lblNumeroProductoAfectados.Size = new Size(52, 37);
            lblNumeroProductoAfectados.TabIndex = 35;
            lblNumeroProductoAfectados.Tag = "NoModificarConBase";
            lblNumeroProductoAfectados.Text = "N1";
            // 
            // btnCancelar
            // 
            btnCancelar.Location = new Point(1142, 972);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new Size(75, 23);
            btnCancelar.TabIndex = 41;
            btnCancelar.Text = "Cancelar";
            btnCancelar.UseVisualStyleBackColor = true;
            // 
            // btnCrear
            // 
            btnCrear.Location = new Point(1352, 972);
            btnCrear.Name = "btnCrear";
            btnCrear.Size = new Size(75, 23);
            btnCrear.TabIndex = 40;
            btnCrear.Text = "Crear";
            btnCrear.UseVisualStyleBackColor = true;
            // 
            // cbxDescuentoPorcentaje
            // 
            cbxDescuentoPorcentaje.AutoSize = true;
            cbxDescuentoPorcentaje.Location = new Point(1252, 751);
            cbxDescuentoPorcentaje.Name = "cbxDescuentoPorcentaje";
            cbxDescuentoPorcentaje.Size = new Size(205, 19);
            cbxDescuentoPorcentaje.TabIndex = 39;
            cbxDescuentoPorcentaje.Text = "Precio de la Oferta/Descuento (%)";
            cbxDescuentoPorcentaje.UseVisualStyleBackColor = true;
            cbxDescuentoPorcentaje.CheckedChanged += cbxDescuentoPorcentaje_CheckedChanged;
            // 
            // cbxDescuentoPesos
            // 
            cbxDescuentoPesos.AutoSize = true;
            cbxDescuentoPesos.Location = new Point(958, 749);
            cbxDescuentoPesos.Name = "cbxDescuentoPesos";
            cbxDescuentoPesos.Size = new Size(201, 19);
            cbxDescuentoPesos.TabIndex = 38;
            cbxDescuentoPesos.Text = "Precio de la Oferta/Descuento ($)";
            cbxDescuentoPesos.UseVisualStyleBackColor = true;
            cbxDescuentoPesos.CheckedChanged += cbxDescuentoPesos_CheckedChanged;
            // 
            // txtMontoPorcentaje
            // 
            txtMontoPorcentaje.Location = new Point(1072, 776);
            txtMontoPorcentaje.Name = "txtMontoPorcentaje";
            txtMontoPorcentaje.Size = new Size(301, 23);
            txtMontoPorcentaje.TabIndex = 37;
            // 
            // txtDescripcion
            // 
            txtDescripcion.Location = new Point(40, 945);
            txtDescripcion.Multiline = true;
            txtDescripcion.Name = "txtDescripcion";
            txtDescripcion.ReadOnly = true;
            txtDescripcion.Size = new Size(602, 50);
            txtDescripcion.TabIndex = 43;
            // 
            // lbl
            // 
            lbl.AutoSize = true;
            lbl.Location = new Point(40, 927);
            lbl.Name = "lbl";
            lbl.Size = new Size(69, 15);
            lbl.TabIndex = 42;
            lbl.Text = "Descripcion";
            // 
            // txtDetalle
            // 
            txtDetalle.Location = new Point(886, 891);
            txtDetalle.Multiline = true;
            txtDetalle.Name = "txtDetalle";
            txtDetalle.Size = new Size(602, 50);
            txtDetalle.TabIndex = 44;
            // 
            // lblDetalle
            // 
            lblDetalle.AutoSize = true;
            lblDetalle.Location = new Point(886, 863);
            lblDetalle.Name = "lblDetalle";
            lblDetalle.Size = new Size(43, 15);
            lblDetalle.TabIndex = 45;
            lblDetalle.Text = "Detalle";
            // 
            // dtpFechaFin
            // 
            dtpFechaFin.Location = new Point(352, 881);
            dtpFechaFin.Name = "dtpFechaFin";
            dtpFechaFin.Size = new Size(200, 23);
            dtpFechaFin.TabIndex = 51;
            // 
            // dtpFechaInicio
            // 
            dtpFechaInicio.Location = new Point(90, 881);
            dtpFechaInicio.Name = "dtpFechaInicio";
            dtpFechaInicio.Size = new Size(200, 23);
            dtpFechaInicio.TabIndex = 50;
            // 
            // lblFechaFin
            // 
            lblFechaFin.AutoSize = true;
            lblFechaFin.Location = new Point(352, 863);
            lblFechaFin.Name = "lblFechaFin";
            lblFechaFin.Size = new Size(57, 15);
            lblFechaFin.TabIndex = 49;
            lblFechaFin.Text = "Fecha Fin";
            // 
            // lblFechaInicio
            // 
            lblFechaInicio.AutoSize = true;
            lblFechaInicio.Location = new Point(90, 863);
            lblFechaInicio.Name = "lblFechaInicio";
            lblFechaInicio.Size = new Size(70, 15);
            lblFechaInicio.TabIndex = 48;
            lblFechaInicio.Text = "Fecha Inicio";
            // 
            // txtRubro
            // 
            txtRubro.Location = new Point(181, 52);
            txtRubro.Name = "txtRubro";
            txtRubro.ReadOnly = true;
            txtRubro.Size = new Size(142, 23);
            txtRubro.TabIndex = 65;
            txtRubro.TextAlign = HorizontalAlignment.Center;
            // 
            // btnCargarGrupoRubro
            // 
            btnCargarGrupoRubro.Enabled = false;
            btnCargarGrupoRubro.Location = new Point(181, 3);
            btnCargarGrupoRubro.Name = "btnCargarGrupoRubro";
            btnCargarGrupoRubro.Size = new Size(142, 32);
            btnCargarGrupoRubro.TabIndex = 64;
            btnCargarGrupoRubro.Text = "Cargar Grupo";
            btnCargarGrupoRubro.UseVisualStyleBackColor = true;
            btnCargarGrupoRubro.Click += btnCargarGrupoRubro_Click;
            // 
            // txtCategoria
            // 
            txtCategoria.Location = new Point(359, 52);
            txtCategoria.Name = "txtCategoria";
            txtCategoria.ReadOnly = true;
            txtCategoria.Size = new Size(143, 23);
            txtCategoria.TabIndex = 67;
            txtCategoria.TextAlign = HorizontalAlignment.Center;
            // 
            // btnCargarGrupoCategoria
            // 
            btnCargarGrupoCategoria.Enabled = false;
            btnCargarGrupoCategoria.Location = new Point(359, 3);
            btnCargarGrupoCategoria.Name = "btnCargarGrupoCategoria";
            btnCargarGrupoCategoria.Size = new Size(143, 32);
            btnCargarGrupoCategoria.TabIndex = 66;
            btnCargarGrupoCategoria.Text = "Cargar Grupo";
            btnCargarGrupoCategoria.UseVisualStyleBackColor = true;
            btnCargarGrupoCategoria.Click += btnCargarGrupoCategoria_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(648, 195);
            label1.Name = "label1";
            label1.Size = new Size(0, 15);
            label1.TabIndex = 70;
            // 
            // dgvProductos
            // 
            dgvProductos.AllowUserToAddRows = false;
            dgvProductos.AllowUserToDeleteRows = false;
            dgvProductos.AllowUserToResizeRows = false;
            dgvProductos.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvProductos.EditMode = DataGridViewEditMode.EditProgrammatically;
            dgvProductos.Location = new Point(570, 213);
            dgvProductos.MultiSelect = false;
            dgvProductos.Name = "dgvProductos";
            dgvProductos.Size = new Size(278, 155);
            dgvProductos.TabIndex = 69;
            dgvProductos.RowEnter += dgvProductos_RowEnter;
            // 
            // btnQuitarProducto
            // 
            btnQuitarProducto.Location = new Point(645, 374);
            btnQuitarProducto.Name = "btnQuitarProducto";
            btnQuitarProducto.Size = new Size(161, 32);
            btnQuitarProducto.TabIndex = 71;
            btnQuitarProducto.Text = "Quitar Producto";
            btnQuitarProducto.UseVisualStyleBackColor = true;
            btnQuitarProducto.Click += btnQuitarProducto_Click;
            // 
            // btnCargarProductosAlcanzados
            // 
            btnCargarProductosAlcanzados.Location = new Point(3, 186);
            btnCargarProductosAlcanzados.Name = "btnCargarProductosAlcanzados";
            btnCargarProductosAlcanzados.Size = new Size(285, 32);
            btnCargarProductosAlcanzados.TabIndex = 72;
            btnCargarProductosAlcanzados.Text = "Cargar Productos Alcanzados";
            btnCargarProductosAlcanzados.UseVisualStyleBackColor = true;
            btnCargarProductosAlcanzados.Click += btnCargarProductosAlcanzados_Click;
            // 
            // btnDevolverAOferta
            // 
            btnDevolverAOferta.Location = new Point(1089, 401);
            btnDevolverAOferta.Name = "btnDevolverAOferta";
            btnDevolverAOferta.Size = new Size(161, 32);
            btnDevolverAOferta.TabIndex = 77;
            btnDevolverAOferta.Text = "Volver a Oferta";
            btnDevolverAOferta.UseVisualStyleBackColor = true;
            btnDevolverAOferta.Click += btnDevolverAOferta_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(1298, 213);
            label2.Name = "label2";
            label2.Size = new Size(0, 15);
            label2.TabIndex = 76;
            // 
            // dgvProductosQuitados
            // 
            dgvProductosQuitados.AllowUserToAddRows = false;
            dgvProductosQuitados.AllowUserToDeleteRows = false;
            dgvProductosQuitados.AllowUserToResizeRows = false;
            dgvProductosQuitados.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvProductosQuitados.EditMode = DataGridViewEditMode.EditProgrammatically;
            dgvProductosQuitados.Location = new Point(1006, 231);
            dgvProductosQuitados.MultiSelect = false;
            dgvProductosQuitados.Name = "dgvProductosQuitados";
            dgvProductosQuitados.Size = new Size(292, 155);
            dgvProductosQuitados.TabIndex = 75;
            // 
            // lblNumeroProductoQuitados
            // 
            lblNumeroProductoQuitados.AutoSize = true;
            lblNumeroProductoQuitados.Font = new Font("Segoe UI", 20.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblNumeroProductoQuitados.Location = new Point(1314, 195);
            lblNumeroProductoQuitados.Name = "lblNumeroProductoQuitados";
            lblNumeroProductoQuitados.Size = new Size(52, 37);
            lblNumeroProductoQuitados.TabIndex = 74;
            lblNumeroProductoQuitados.Tag = "NoModificarConBase";
            lblNumeroProductoQuitados.Text = "N1";
            // 
            // lblCantidadProductosQuitados
            // 
            lblCantidadProductosQuitados.AutoSize = true;
            lblCantidadProductosQuitados.Location = new Point(1063, 213);
            lblCantidadProductosQuitados.Name = "lblCantidadProductosQuitados";
            lblCantidadProductosQuitados.Size = new Size(229, 15);
            lblCantidadProductosQuitados.TabIndex = 73;
            lblCantidadProductosQuitados.Text = "Cantidad Productos Excluidos de la oferta:";
            // 
            // cbxLimiteCumplirStock
            // 
            cbxLimiteCumplirStock.AutoSize = true;
            cbxLimiteCumplirStock.Location = new Point(944, 842);
            cbxLimiteCumplirStock.Name = "cbxLimiteCumplirStock";
            cbxLimiteCumplirStock.Size = new Size(131, 19);
            cbxLimiteCumplirStock.TabIndex = 79;
            cbxLimiteCumplirStock.Text = "Hasta cumplir stock";
            cbxLimiteCumplirStock.UseVisualStyleBackColor = true;
            // 
            // lblLimiteStock
            // 
            lblLimiteStock.AutoSize = true;
            lblLimiteStock.Location = new Point(1090, 846);
            lblLimiteStock.Name = "lblLimiteStock";
            lblLimiteStock.Size = new Size(140, 15);
            lblLimiteStock.TabIndex = 81;
            lblLimiteStock.Text = "Limite de Stock en Oferta";
            // 
            // txtLimiteStock
            // 
            txtLimiteStock.Enabled = false;
            txtLimiteStock.Location = new Point(1236, 843);
            txtLimiteStock.Name = "txtLimiteStock";
            txtLimiteStock.Size = new Size(137, 23);
            txtLimiteStock.TabIndex = 80;
            // 
            // btnLimpiar
            // 
            btnLimpiar.Location = new Point(886, 967);
            btnLimpiar.Name = "btnLimpiar";
            btnLimpiar.Size = new Size(161, 32);
            btnLimpiar.TabIndex = 83;
            btnLimpiar.Text = "Limpiar";
            btnLimpiar.UseVisualStyleBackColor = true;
            // 
            // lblCodigoManual
            // 
            lblCodigoManual.AutoSize = true;
            lblCodigoManual.Location = new Point(1076, 522);
            lblCodigoManual.Name = "lblCodigoManual";
            lblCodigoManual.Size = new Size(38, 15);
            lblCodigoManual.TabIndex = 84;
            lblCodigoManual.Text = "label3";
            // 
            // btnCargarProducto
            // 
            btnCargarProducto.Enabled = false;
            btnCargarProducto.Location = new Point(711, 116);
            btnCargarProducto.Name = "btnCargarProducto";
            btnCargarProducto.Size = new Size(218, 32);
            btnCargarProducto.TabIndex = 85;
            btnCargarProducto.Text = "Cargar Producto/s";
            btnCargarProducto.UseVisualStyleBackColor = true;
            btnCargarProducto.Click += btnCargarProducto_Click;
            // 
            // lblTotalPrecioCosto
            // 
            lblTotalPrecioCosto.AutoSize = true;
            lblTotalPrecioCosto.Enabled = false;
            lblTotalPrecioCosto.Location = new Point(975, 668);
            lblTotalPrecioCosto.Name = "lblTotalPrecioCosto";
            lblTotalPrecioCosto.Size = new Size(139, 15);
            lblTotalPrecioCosto.TabIndex = 90;
            lblTotalPrecioCosto.Text = "Precio Costo Acumulado";
            lblTotalPrecioCosto.Visible = false;
            // 
            // txtPrecioCostoAcumulado
            // 
            txtPrecioCostoAcumulado.Enabled = false;
            txtPrecioCostoAcumulado.Location = new Point(890, 696);
            txtPrecioCostoAcumulado.Name = "txtPrecioCostoAcumulado";
            txtPrecioCostoAcumulado.Size = new Size(285, 23);
            txtPrecioCostoAcumulado.TabIndex = 89;
            txtPrecioCostoAcumulado.Visible = false;
            // 
            // lblTotalPrecioReal
            // 
            lblTotalPrecioReal.AutoSize = true;
            lblTotalPrecioReal.Enabled = false;
            lblTotalPrecioReal.Location = new Point(1307, 668);
            lblTotalPrecioReal.Name = "lblTotalPrecioReal";
            lblTotalPrecioReal.Size = new Size(97, 15);
            lblTotalPrecioReal.TabIndex = 88;
            lblTotalPrecioReal.Text = "Precio Venta Real";
            lblTotalPrecioReal.Visible = false;
            // 
            // txtPrecioVentaReal
            // 
            txtPrecioVentaReal.Enabled = false;
            txtPrecioVentaReal.Location = new Point(1222, 696);
            txtPrecioVentaReal.Name = "txtPrecioVentaReal";
            txtPrecioVentaReal.Size = new Size(285, 23);
            txtPrecioVentaReal.TabIndex = 87;
            txtPrecioVentaReal.Visible = false;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 3;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
            tableLayoutPanel1.Controls.Add(btnCargarGrupoMarca, 0, 0);
            tableLayoutPanel1.Controls.Add(btnCargarGrupoRubro, 1, 0);
            tableLayoutPanel1.Controls.Add(btnCargarGrupoCategoria, 2, 0);
            tableLayoutPanel1.Controls.Add(txtMarca, 0, 1);
            tableLayoutPanel1.Controls.Add(txtRubro, 1, 1);
            tableLayoutPanel1.Controls.Add(txtCategoria, 2, 1);
            tableLayoutPanel1.Location = new Point(3, 3);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 2;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.Size = new Size(534, 98);
            tableLayoutPanel1.TabIndex = 91;
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.ColumnCount = 1;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel2.Controls.Add(tableLayoutPanel1, 0, 0);
            tableLayoutPanel2.Controls.Add(btnCargarProductosAlcanzados, 0, 2);
            tableLayoutPanel2.Controls.Add(label3, 0, 1);
            tableLayoutPanel2.Location = new Point(12, 98);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 3;
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 56.7441864F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 43.2558136F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Absolute, 52F));
            tableLayoutPanel2.Size = new Size(540, 236);
            tableLayoutPanel2.TabIndex = 92;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(3, 104);
            label3.Name = "label3";
            label3.Size = new Size(394, 15);
            label3.TabIndex = 92;
            label3.Text = "alcance de los productos de la oferta sea un combo de los tres o de 1 solo";
            // 
            // FOfertaGrupoABM
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1571, 1061);
            Controls.Add(tableLayoutPanel2);
            Controls.Add(lblTotalPrecioCosto);
            Controls.Add(txtPrecioCostoAcumulado);
            Controls.Add(btnCargarProducto);
            Controls.Add(lblTotalPrecioReal);
            Controls.Add(txtPrecioVentaReal);
            Controls.Add(lblCodigoManual);
            Controls.Add(btnLimpiar);
            Controls.Add(lblLimiteStock);
            Controls.Add(txtLimiteStock);
            Controls.Add(cbxLimiteCumplirStock);
            Controls.Add(btnDevolverAOferta);
            Controls.Add(label2);
            Controls.Add(dgvProductosQuitados);
            Controls.Add(lblNumeroProductoQuitados);
            Controls.Add(lblCantidadProductosQuitados);
            Controls.Add(btnQuitarProducto);
            Controls.Add(label1);
            Controls.Add(dgvProductos);
            Controls.Add(dtpFechaFin);
            Controls.Add(dtpFechaInicio);
            Controls.Add(lblFechaFin);
            Controls.Add(lblFechaInicio);
            Controls.Add(lblDetalle);
            Controls.Add(txtDetalle);
            Controls.Add(txtDescripcion);
            Controls.Add(lbl);
            Controls.Add(btnCancelar);
            Controls.Add(btnCrear);
            Controls.Add(cbxDescuentoPorcentaje);
            Controls.Add(cbxDescuentoPesos);
            Controls.Add(txtMontoPorcentaje);
            Controls.Add(lblNumeroProductoAfectados);
            Controls.Add(lblTitulo);
            Controls.Add(lblCantidadProductos);
            Name = "FOfertaGrupoABM";
            Text = "FOfertaGrupoABM";
            Load += FOfertaGrupoABM_Load;
            ((System.ComponentModel.ISupportInitialize)dgvProductos).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvProductosQuitados).EndInit();
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            tableLayoutPanel2.ResumeLayout(false);
            tableLayoutPanel2.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblCantidadProductos;
        private Button btnCargarGrupoMarca;
        private Label lblTitulo;
        private TextBox txtMarca;
        private Label lblNumeroProductoAfectados;
        private Button btnCancelar;
        private Button btnCrear;
        private CheckBox cbxDescuentoPorcentaje;
        private CheckBox cbxDescuentoPesos;
        private TextBox txtMontoPorcentaje;
        private TextBox txtDescripcion;
        private Label lbl;
        private TextBox txtDetalle;
        private Label lblDetalle;
        private DateTimePicker dtpFechaFin;
        private DateTimePicker dtpFechaInicio;
        private Label lblFechaFin;
        private Label lblFechaInicio;
        private TextBox txtRubro;
        private Button btnCargarGrupoRubro;
        private TextBox txtCategoria;
        private Button btnCargarGrupoCategoria;
        private Label label1;
        private DataGridView dgvProductos;
        private Button btnQuitarProducto;
        private Button btnCargarProductosAlcanzados;
        private Button btnDevolverAOferta;
        private Label label2;
        private DataGridView dgvProductosQuitados;
        private Label lblNumeroProductoQuitados;
        private Label lblCantidadProductosQuitados;
        private CheckBox cbxLimiteCumplirStock;
        private Label lblLimiteStock;
        private TextBox txtLimiteStock;
        private Button btnLimpiar;
        private Label lblCodigoManual;
        private Button btnCargarProducto;
        private Label lblTotalPrecioCosto;
        private TextBox txtPrecioCostoAcumulado;
        private Label lblTotalPrecioReal;
        private TextBox txtPrecioVentaReal;
        private TableLayoutPanel tableLayoutPanel1;
        private TableLayoutPanel tableLayoutPanel2;
        private Label label3;
    }
}