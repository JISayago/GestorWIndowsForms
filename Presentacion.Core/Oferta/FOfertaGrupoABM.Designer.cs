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
            cbxMarca = new CheckBox();
            cbxRubro = new CheckBox();
            cbxCategoria = new CheckBox();
            lblTitulo = new Label();
            txtMarca = new TextBox();
            lblNumeroProductoAfectados = new Label();
            btnCancelar = new Button();
            btnCrear = new Button();
            cbxDescuentoPorcentaje = new CheckBox();
            cbxDescuentoPesos = new CheckBox();
            txtPrecioDescuentoPorcentaje = new TextBox();
            txtPrecioDescuentoPesos = new TextBox();
            txtDescripcion = new TextBox();
            lbl = new Label();
            textBox1 = new TextBox();
            lblDetalle = new Label();
            lblCodigo = new Label();
            textBox2 = new TextBox();
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
            ((System.ComponentModel.ISupportInitialize)dgvProductos).BeginInit();
            SuspendLayout();
            // 
            // lblCantidadProductos
            // 
            lblCantidadProductos.AutoSize = true;
            lblCantidadProductos.Location = new Point(87, 269);
            lblCantidadProductos.Name = "lblCantidadProductos";
            lblCantidadProductos.Size = new Size(245, 15);
            lblCantidadProductos.TabIndex = 29;
            lblCantidadProductos.Text = "Cantidad Productos Alcanzados por la oferta:";
            // 
            // btnCargarGrupoMarca
            // 
            btnCargarGrupoMarca.Enabled = false;
            btnCargarGrupoMarca.Location = new Point(83, 105);
            btnCargarGrupoMarca.Name = "btnCargarGrupoMarca";
            btnCargarGrupoMarca.Size = new Size(161, 32);
            btnCargarGrupoMarca.TabIndex = 27;
            btnCargarGrupoMarca.Text = "Cargar Grupo";
            btnCargarGrupoMarca.UseVisualStyleBackColor = true;
            btnCargarGrupoMarca.Click += btnCargarGrupoMarca_Click;
            // 
            // cbxMarca
            // 
            cbxMarca.AutoSize = true;
            cbxMarca.Location = new Point(137, 80);
            cbxMarca.Name = "cbxMarca";
            cbxMarca.Size = new Size(59, 19);
            cbxMarca.TabIndex = 30;
            cbxMarca.Text = "Marca";
            cbxMarca.UseVisualStyleBackColor = true;
            cbxMarca.CheckedChanged += cbxMarca_CheckedChanged;
            // 
            // cbxRubro
            // 
            cbxRubro.AutoSize = true;
            cbxRubro.Location = new Point(354, 79);
            cbxRubro.Name = "cbxRubro";
            cbxRubro.Size = new Size(58, 19);
            cbxRubro.TabIndex = 31;
            cbxRubro.Text = "Rubro";
            cbxRubro.UseVisualStyleBackColor = true;
            cbxRubro.CheckedChanged += cbxRubro_CheckedChanged;
            // 
            // cbxCategoria
            // 
            cbxCategoria.AutoSize = true;
            cbxCategoria.Location = new Point(583, 79);
            cbxCategoria.Name = "cbxCategoria";
            cbxCategoria.Size = new Size(77, 19);
            cbxCategoria.TabIndex = 32;
            cbxCategoria.Text = "Categoria";
            cbxCategoria.UseVisualStyleBackColor = true;
            cbxCategoria.CheckedChanged += cbxCategoria_CheckedChanged;
            // 
            // lblTitulo
            // 
            lblTitulo.AutoSize = true;
            lblTitulo.Font = new Font("Segoe UI", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblTitulo.Location = new Point(93, 22);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(611, 30);
            lblTitulo.TabIndex = 33;
            lblTitulo.Text = "Por favor elija el grupo que va a ser considerado para esta oferta";
            // 
            // txtMarca
            // 
            txtMarca.Location = new Point(86, 152);
            txtMarca.Name = "txtMarca";
            txtMarca.ReadOnly = true;
            txtMarca.Size = new Size(158, 23);
            txtMarca.TabIndex = 34;
            txtMarca.TextAlign = HorizontalAlignment.Center;
            // 
            // lblNumeroProductoAfectados
            // 
            lblNumeroProductoAfectados.AutoSize = true;
            lblNumeroProductoAfectados.Font = new Font("Segoe UI", 20.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblNumeroProductoAfectados.Location = new Point(338, 251);
            lblNumeroProductoAfectados.Name = "lblNumeroProductoAfectados";
            lblNumeroProductoAfectados.Size = new Size(52, 37);
            lblNumeroProductoAfectados.TabIndex = 35;
            lblNumeroProductoAfectados.Text = "N1";
            // 
            // btnCancelar
            // 
            btnCancelar.Location = new Point(428, 921);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new Size(75, 23);
            btnCancelar.TabIndex = 41;
            btnCancelar.Text = "Cancelar";
            btnCancelar.UseVisualStyleBackColor = true;
            // 
            // btnCrear
            // 
            btnCrear.Location = new Point(334, 921);
            btnCrear.Name = "btnCrear";
            btnCrear.Size = new Size(75, 23);
            btnCrear.TabIndex = 40;
            btnCrear.Text = "Crear";
            btnCrear.UseVisualStyleBackColor = true;
            // 
            // cbxDescuentoPorcentaje
            // 
            cbxDescuentoPorcentaje.AutoSize = true;
            cbxDescuentoPorcentaje.Location = new Point(428, 769);
            cbxDescuentoPorcentaje.Name = "cbxDescuentoPorcentaje";
            cbxDescuentoPorcentaje.Size = new Size(201, 19);
            cbxDescuentoPorcentaje.TabIndex = 39;
            cbxDescuentoPorcentaje.Text = "Precio de la Oferta/Descuento ($)";
            cbxDescuentoPorcentaje.UseVisualStyleBackColor = true;
            // 
            // cbxDescuentoPesos
            // 
            cbxDescuentoPesos.AutoSize = true;
            cbxDescuentoPesos.Location = new Point(134, 767);
            cbxDescuentoPesos.Name = "cbxDescuentoPesos";
            cbxDescuentoPesos.Size = new Size(201, 19);
            cbxDescuentoPesos.TabIndex = 38;
            cbxDescuentoPesos.Text = "Precio de la Oferta/Descuento ($)";
            cbxDescuentoPesos.UseVisualStyleBackColor = true;
            // 
            // txtPrecioDescuentoPorcentaje
            // 
            txtPrecioDescuentoPorcentaje.Location = new Point(428, 788);
            txtPrecioDescuentoPorcentaje.Name = "txtPrecioDescuentoPorcentaje";
            txtPrecioDescuentoPorcentaje.Size = new Size(301, 23);
            txtPrecioDescuentoPorcentaje.TabIndex = 37;
            // 
            // txtPrecioDescuentoPesos
            // 
            txtPrecioDescuentoPesos.Location = new Point(127, 788);
            txtPrecioDescuentoPesos.Name = "txtPrecioDescuentoPesos";
            txtPrecioDescuentoPesos.Size = new Size(285, 23);
            txtPrecioDescuentoPesos.TabIndex = 36;
            // 
            // txtDescripcion
            // 
            txtDescripcion.Location = new Point(118, 590);
            txtDescripcion.Multiline = true;
            txtDescripcion.Name = "txtDescripcion";
            txtDescripcion.ReadOnly = true;
            txtDescripcion.Size = new Size(602, 50);
            txtDescripcion.TabIndex = 43;
            // 
            // lbl
            // 
            lbl.AutoSize = true;
            lbl.Location = new Point(118, 572);
            lbl.Name = "lbl";
            lbl.Size = new Size(69, 15);
            lbl.TabIndex = 42;
            lbl.Text = "Descripcion";
            // 
            // textBox1
            // 
            textBox1.Location = new Point(118, 676);
            textBox1.Multiline = true;
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(602, 50);
            textBox1.TabIndex = 44;
            // 
            // lblDetalle
            // 
            lblDetalle.AutoSize = true;
            lblDetalle.Location = new Point(118, 658);
            lblDetalle.Name = "lblDetalle";
            lblDetalle.Size = new Size(43, 15);
            lblDetalle.TabIndex = 45;
            lblDetalle.Text = "Detalle";
            // 
            // lblCodigo
            // 
            lblCodigo.AutoSize = true;
            lblCodigo.Location = new Point(118, 520);
            lblCodigo.Name = "lblCodigo";
            lblCodigo.Size = new Size(108, 15);
            lblCodigo.TabIndex = 47;
            lblCodigo.Text = "Codigo de la oferta";
            // 
            // textBox2
            // 
            textBox2.Location = new Point(118, 538);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(602, 23);
            textBox2.TabIndex = 46;
            // 
            // dtpFechaFin
            // 
            dtpFechaFin.Location = new Point(452, 855);
            dtpFechaFin.Name = "dtpFechaFin";
            dtpFechaFin.Size = new Size(200, 23);
            dtpFechaFin.TabIndex = 51;
            // 
            // dtpFechaInicio
            // 
            dtpFechaInicio.Location = new Point(190, 855);
            dtpFechaInicio.Name = "dtpFechaInicio";
            dtpFechaInicio.Size = new Size(200, 23);
            dtpFechaInicio.TabIndex = 50;
            // 
            // lblFechaFin
            // 
            lblFechaFin.AutoSize = true;
            lblFechaFin.Location = new Point(452, 837);
            lblFechaFin.Name = "lblFechaFin";
            lblFechaFin.Size = new Size(57, 15);
            lblFechaFin.TabIndex = 49;
            lblFechaFin.Text = "Fecha Fin";
            // 
            // lblFechaInicio
            // 
            lblFechaInicio.AutoSize = true;
            lblFechaInicio.Location = new Point(190, 837);
            lblFechaInicio.Name = "lblFechaInicio";
            lblFechaInicio.Size = new Size(70, 15);
            lblFechaInicio.TabIndex = 48;
            lblFechaInicio.Text = "Fecha Inicio";
            // 
            // txtRubro
            // 
            txtRubro.Location = new Point(307, 151);
            txtRubro.Name = "txtRubro";
            txtRubro.ReadOnly = true;
            txtRubro.Size = new Size(158, 23);
            txtRubro.TabIndex = 65;
            txtRubro.TextAlign = HorizontalAlignment.Center;
            // 
            // btnCargarGrupoRubro
            // 
            btnCargarGrupoRubro.Enabled = false;
            btnCargarGrupoRubro.Location = new Point(304, 104);
            btnCargarGrupoRubro.Name = "btnCargarGrupoRubro";
            btnCargarGrupoRubro.Size = new Size(161, 32);
            btnCargarGrupoRubro.TabIndex = 64;
            btnCargarGrupoRubro.Text = "Cargar Grupo";
            btnCargarGrupoRubro.UseVisualStyleBackColor = true;
            btnCargarGrupoRubro.Click += btnCargarGrupoRubro_Click;
            // 
            // txtCategoria
            // 
            txtCategoria.Location = new Point(546, 151);
            txtCategoria.Name = "txtCategoria";
            txtCategoria.ReadOnly = true;
            txtCategoria.Size = new Size(158, 23);
            txtCategoria.TabIndex = 67;
            txtCategoria.TextAlign = HorizontalAlignment.Center;
            // 
            // btnCargarGrupoCategoria
            // 
            btnCargarGrupoCategoria.Enabled = false;
            btnCargarGrupoCategoria.Location = new Point(543, 104);
            btnCargarGrupoCategoria.Name = "btnCargarGrupoCategoria";
            btnCargarGrupoCategoria.Size = new Size(161, 32);
            btnCargarGrupoCategoria.TabIndex = 66;
            btnCargarGrupoCategoria.Text = "Cargar Grupo";
            btnCargarGrupoCategoria.UseVisualStyleBackColor = true;
            btnCargarGrupoCategoria.Click += btnCargarGrupoCategoria_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(90, 269);
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
            dgvProductos.Location = new Point(83, 287);
            dgvProductos.MultiSelect = false;
            dgvProductos.Name = "dgvProductos";
            dgvProductos.Size = new Size(602, 155);
            dgvProductos.TabIndex = 69;
            // 
            // btnQuitarProducto
            // 
            btnQuitarProducto.Location = new Point(87, 448);
            btnQuitarProducto.Name = "btnQuitarProducto";
            btnQuitarProducto.Size = new Size(161, 32);
            btnQuitarProducto.TabIndex = 71;
            btnQuitarProducto.Text = "Quitar Producto";
            btnQuitarProducto.UseVisualStyleBackColor = true;
            // 
            // btnCargarProductosAlcanzados
            // 
            btnCargarProductosAlcanzados.Location = new Point(240, 202);
            btnCargarProductosAlcanzados.Name = "btnCargarProductosAlcanzados";
            btnCargarProductosAlcanzados.Size = new Size(285, 32);
            btnCargarProductosAlcanzados.TabIndex = 72;
            btnCargarProductosAlcanzados.Text = "Cargar Productos";
            btnCargarProductosAlcanzados.UseVisualStyleBackColor = true;
            btnCargarProductosAlcanzados.Click += btnCargarProductosAlcanzados_Click;
            // 
            // FOfertaGrupoABM
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 1061);
            Controls.Add(btnCargarProductosAlcanzados);
            Controls.Add(btnQuitarProducto);
            Controls.Add(label1);
            Controls.Add(dgvProductos);
            Controls.Add(txtCategoria);
            Controls.Add(btnCargarGrupoCategoria);
            Controls.Add(txtRubro);
            Controls.Add(btnCargarGrupoRubro);
            Controls.Add(dtpFechaFin);
            Controls.Add(dtpFechaInicio);
            Controls.Add(lblFechaFin);
            Controls.Add(lblFechaInicio);
            Controls.Add(lblCodigo);
            Controls.Add(textBox2);
            Controls.Add(lblDetalle);
            Controls.Add(textBox1);
            Controls.Add(txtDescripcion);
            Controls.Add(lbl);
            Controls.Add(btnCancelar);
            Controls.Add(btnCrear);
            Controls.Add(cbxDescuentoPorcentaje);
            Controls.Add(cbxDescuentoPesos);
            Controls.Add(txtPrecioDescuentoPorcentaje);
            Controls.Add(txtPrecioDescuentoPesos);
            Controls.Add(lblNumeroProductoAfectados);
            Controls.Add(txtMarca);
            Controls.Add(lblTitulo);
            Controls.Add(cbxCategoria);
            Controls.Add(cbxRubro);
            Controls.Add(cbxMarca);
            Controls.Add(lblCantidadProductos);
            Controls.Add(btnCargarGrupoMarca);
            Name = "FOfertaGrupoABM";
            Text = "FOfertaGrupoABM";
            Load += FOfertaGrupoABM_Load;
            ((System.ComponentModel.ISupportInitialize)dgvProductos).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblCantidadProductos;
        private Button btnCargarGrupoMarca;
        private CheckBox cbxMarca;
        private CheckBox cbxRubro;
        private CheckBox cbxCategoria;
        private Label lblTitulo;
        private TextBox txtMarca;
        private Label lblNumeroProductoAfectados;
        private Button btnCancelar;
        private Button btnCrear;
        private CheckBox cbxDescuentoPorcentaje;
        private CheckBox cbxDescuentoPesos;
        private TextBox txtPrecioDescuentoPorcentaje;
        private TextBox txtPrecioDescuentoPesos;
        private TextBox txtDescripcion;
        private Label lbl;
        private TextBox textBox1;
        private Label lblDetalle;
        private Label lblCodigo;
        private TextBox textBox2;
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
    }
}