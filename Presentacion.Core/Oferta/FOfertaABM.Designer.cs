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
            tableLayoutPanel2 = new TableLayoutPanel();
            tableLayoutPanel1 = new TableLayoutPanel();
            btnCargarGrupoMarca = new Button();
            btnCargarGrupoRubro = new Button();
            btnCargarGrupoCategoria = new Button();
            txtMarca = new TextBox();
            txtRubro = new TextBox();
            txtCategorias = new TextBox();
            btnCargarProductosAlcanzados = new Button();
            lblDetalleGruposFiltro = new Label();
            btnCargarProducto = new Button();
            btnDevolverAOferta = new Button();
            label2 = new Label();
            dgvProductosQuitados = new DataGridView();
            lblNumeroProductoQuitados = new Label();
            lblCantidadProductosQuitados = new Label();
            btnQuitarProducto = new Button();
            label1 = new Label();
            dgvProductos = new DataGridView();
            lblNumeroProductoAfectados = new Label();
            lblCantidadProductos = new Label();
            txtCodigo = new TextBox();
            lblCodigoOf = new Label();
            lblDescripcion = new Label();
            txtDescripcion = new TextBox();
            lblTotalAcumuladoReal = new Label();
            lblTotalAcumuladoVenta = new Label();
            lblTotalPerdidoConREspectoAlVentayREal = new Label();
            txtPorcentajeDescuento = new TextBox();
            lblDescuentoPor = new Label();
            label3 = new Label();
            txtPrecioFinal = new TextBox();
            dtpFechaInicio = new DateTimePicker();
            dtpFechaFin = new DateTimePicker();
            lblFechaInicio = new Label();
            lblFechaFin = new Label();
            lblLimiteStock = new Label();
            txtLimiteStock = new TextBox();
            cbxLimiteCumplirStock = new CheckBox();
            btnControlStockDisponible = new Button();
            btnCrearOferta = new Button();
            btnLimpiarCampos = new Button();
            btnCancelarYSalir = new Button();
            lblTotalFinal = new Label();
            ((System.ComponentModel.ISupportInitialize)error).BeginInit();
            tableLayoutPanel2.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvProductosQuitados).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvProductos).BeginInit();
            SuspendLayout();
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.ColumnCount = 1;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel2.Controls.Add(tableLayoutPanel1, 0, 0);
            tableLayoutPanel2.Controls.Add(btnCargarProductosAlcanzados, 0, 2);
            tableLayoutPanel2.Controls.Add(lblDetalleGruposFiltro, 0, 1);
            tableLayoutPanel2.Location = new Point(31, 12);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 3;
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 56.7441864F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 43.2558136F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Absolute, 52F));
            tableLayoutPanel2.Size = new Size(755, 236);
            tableLayoutPanel2.TabIndex = 93;
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
            tableLayoutPanel1.Controls.Add(txtCategorias, 2, 1);
            tableLayoutPanel1.Location = new Point(3, 3);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 2;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.Size = new Size(534, 98);
            tableLayoutPanel1.TabIndex = 91;
            // 
            // btnCargarGrupoMarca
            // 
            btnCargarGrupoMarca.Location = new Point(3, 3);
            btnCargarGrupoMarca.Name = "btnCargarGrupoMarca";
            btnCargarGrupoMarca.Size = new Size(142, 32);
            btnCargarGrupoMarca.TabIndex = 27;
            btnCargarGrupoMarca.Text = "Cargar Marca";
            btnCargarGrupoMarca.UseVisualStyleBackColor = true;
            btnCargarGrupoMarca.Click += btnCargarGrupoMarca_Click;
            // 
            // btnCargarGrupoRubro
            // 
            btnCargarGrupoRubro.Location = new Point(181, 3);
            btnCargarGrupoRubro.Name = "btnCargarGrupoRubro";
            btnCargarGrupoRubro.Size = new Size(142, 32);
            btnCargarGrupoRubro.TabIndex = 64;
            btnCargarGrupoRubro.Text = "Cargar Rubro";
            btnCargarGrupoRubro.UseVisualStyleBackColor = true;
            btnCargarGrupoRubro.Click += btnCargarGrupoRubro_Click;
            // 
            // btnCargarGrupoCategoria
            // 
            btnCargarGrupoCategoria.Location = new Point(359, 3);
            btnCargarGrupoCategoria.Name = "btnCargarGrupoCategoria";
            btnCargarGrupoCategoria.Size = new Size(143, 32);
            btnCargarGrupoCategoria.TabIndex = 66;
            btnCargarGrupoCategoria.Text = "Cargar Categorias";
            btnCargarGrupoCategoria.UseVisualStyleBackColor = true;
            btnCargarGrupoCategoria.Click += btnCargarGrupoCategoria_Click;
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
            // txtRubro
            // 
            txtRubro.Location = new Point(181, 52);
            txtRubro.Name = "txtRubro";
            txtRubro.ReadOnly = true;
            txtRubro.Size = new Size(142, 23);
            txtRubro.TabIndex = 65;
            txtRubro.TextAlign = HorizontalAlignment.Center;
            // 
            // txtCategorias
            // 
            txtCategorias.Location = new Point(359, 52);
            txtCategorias.Name = "txtCategorias";
            txtCategorias.ReadOnly = true;
            txtCategorias.Size = new Size(143, 23);
            txtCategorias.TabIndex = 67;
            txtCategorias.TextAlign = HorizontalAlignment.Center;
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
            // lblDetalleGruposFiltro
            // 
            lblDetalleGruposFiltro.AutoSize = true;
            lblDetalleGruposFiltro.Location = new Point(3, 104);
            lblDetalleGruposFiltro.Name = "lblDetalleGruposFiltro";
            lblDetalleGruposFiltro.Size = new Size(394, 15);
            lblDetalleGruposFiltro.TabIndex = 92;
            lblDetalleGruposFiltro.Text = "alcance de los productos de la oferta sea un combo de los tres o de 1 solo";
            // 
            // btnCargarProducto
            // 
            btnCargarProducto.Location = new Point(156, 269);
            btnCargarProducto.Name = "btnCargarProducto";
            btnCargarProducto.Size = new Size(218, 32);
            btnCargarProducto.TabIndex = 104;
            btnCargarProducto.Text = "Cargar Producto/s para Combo";
            btnCargarProducto.UseVisualStyleBackColor = true;
            btnCargarProducto.Click += btnCargarProducto_Click;
            // 
            // btnDevolverAOferta
            // 
            btnDevolverAOferta.Location = new Point(95, 731);
            btnDevolverAOferta.Name = "btnDevolverAOferta";
            btnDevolverAOferta.Size = new Size(161, 32);
            btnDevolverAOferta.TabIndex = 103;
            btnDevolverAOferta.Text = "Volver a Oferta";
            btnDevolverAOferta.UseVisualStyleBackColor = true;
            btnDevolverAOferta.Click += btnDevolverAOferta_Click_1;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(1187, 279);
            label2.Name = "label2";
            label2.Size = new Size(0, 15);
            label2.TabIndex = 102;
            // 
            // dgvProductosQuitados
            // 
            dgvProductosQuitados.AllowUserToAddRows = false;
            dgvProductosQuitados.AllowUserToDeleteRows = false;
            dgvProductosQuitados.AllowUserToResizeRows = false;
            dgvProductosQuitados.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvProductosQuitados.EditMode = DataGridViewEditMode.EditProgrammatically;
            dgvProductosQuitados.Location = new Point(12, 561);
            dgvProductosQuitados.MultiSelect = false;
            dgvProductosQuitados.Name = "dgvProductosQuitados";
            dgvProductosQuitados.Size = new Size(1263, 155);
            dgvProductosQuitados.TabIndex = 101;
            // 
            // lblNumeroProductoQuitados
            // 
            lblNumeroProductoQuitados.AutoSize = true;
            lblNumeroProductoQuitados.Font = new Font("Segoe UI", 20.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblNumeroProductoQuitados.Location = new Point(994, 257);
            lblNumeroProductoQuitados.Name = "lblNumeroProductoQuitados";
            lblNumeroProductoQuitados.Size = new Size(52, 37);
            lblNumeroProductoQuitados.TabIndex = 100;
            lblNumeroProductoQuitados.Tag = "NoModificarConBase";
            lblNumeroProductoQuitados.Text = "N1";
            // 
            // lblCantidadProductosQuitados
            // 
            lblCantidadProductosQuitados.AutoSize = true;
            lblCantidadProductosQuitados.Location = new Point(69, 543);
            lblCantidadProductosQuitados.Name = "lblCantidadProductosQuitados";
            lblCantidadProductosQuitados.Size = new Size(229, 15);
            lblCantidadProductosQuitados.TabIndex = 99;
            lblCantidadProductosQuitados.Text = "Cantidad Productos Excluidos de la oferta:";
            // 
            // btnQuitarProducto
            // 
            btnQuitarProducto.Location = new Point(96, 505);
            btnQuitarProducto.Name = "btnQuitarProducto";
            btnQuitarProducto.Size = new Size(161, 32);
            btnQuitarProducto.TabIndex = 98;
            btnQuitarProducto.Text = "Quitar Producto";
            btnQuitarProducto.UseVisualStyleBackColor = true;
            btnQuitarProducto.Click += btnQuitarProducto_Click_1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(99, 326);
            label1.Name = "label1";
            label1.Size = new Size(0, 15);
            label1.TabIndex = 97;
            // 
            // dgvProductos
            // 
            dgvProductos.AllowUserToAddRows = false;
            dgvProductos.AllowUserToDeleteRows = false;
            dgvProductos.AllowUserToResizeRows = false;
            dgvProductos.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvProductos.EditMode = DataGridViewEditMode.EditProgrammatically;
            dgvProductos.Location = new Point(21, 344);
            dgvProductos.MultiSelect = false;
            dgvProductos.Name = "dgvProductos";
            dgvProductos.Size = new Size(1246, 155);
            dgvProductos.TabIndex = 96;
            dgvProductos.CellMouseDown += dgvProductos_CellMouseDown_1;
            // 
            // lblNumeroProductoAfectados
            // 
            lblNumeroProductoAfectados.AutoSize = true;
            lblNumeroProductoAfectados.Font = new Font("Segoe UI", 20.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblNumeroProductoAfectados.Location = new Point(287, 304);
            lblNumeroProductoAfectados.Name = "lblNumeroProductoAfectados";
            lblNumeroProductoAfectados.Size = new Size(52, 37);
            lblNumeroProductoAfectados.TabIndex = 95;
            lblNumeroProductoAfectados.Tag = "NoModificarConBase";
            lblNumeroProductoAfectados.Text = "N1";
            // 
            // lblCantidadProductos
            // 
            lblCantidadProductos.AutoSize = true;
            lblCantidadProductos.Location = new Point(36, 326);
            lblCantidadProductos.Name = "lblCantidadProductos";
            lblCantidadProductos.Size = new Size(245, 15);
            lblCantidadProductos.TabIndex = 94;
            lblCantidadProductos.Text = "Cantidad Productos Alcanzados por la oferta:";
            // 
            // txtCodigo
            // 
            txtCodigo.Location = new Point(869, 24);
            txtCodigo.Name = "txtCodigo";
            txtCodigo.Size = new Size(100, 23);
            txtCodigo.TabIndex = 105;
            // 
            // lblCodigoOf
            // 
            lblCodigoOf.AutoSize = true;
            lblCodigoOf.Location = new Point(817, 32);
            lblCodigoOf.Name = "lblCodigoOf";
            lblCodigoOf.Size = new Size(46, 15);
            lblCodigoOf.TabIndex = 106;
            lblCodigoOf.Text = "Código";
            // 
            // lblDescripcion
            // 
            lblDescripcion.AutoSize = true;
            lblDescripcion.Location = new Point(817, 67);
            lblDescripcion.Name = "lblDescripcion";
            lblDescripcion.Size = new Size(69, 15);
            lblDescripcion.TabIndex = 107;
            lblDescripcion.Text = "Descripcion";
            // 
            // txtDescripcion
            // 
            txtDescripcion.Location = new Point(817, 85);
            txtDescripcion.Multiline = true;
            txtDescripcion.Name = "txtDescripcion";
            txtDescripcion.ReadOnly = true;
            txtDescripcion.Size = new Size(235, 163);
            txtDescripcion.TabIndex = 108;
            // 
            // lblTotalAcumuladoReal
            // 
            lblTotalAcumuladoReal.AutoSize = true;
            lblTotalAcumuladoReal.Font = new Font("Segoe UI Semibold", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTotalAcumuladoReal.Location = new Point(411, 727);
            lblTotalAcumuladoReal.Name = "lblTotalAcumuladoReal";
            lblTotalAcumuladoReal.Size = new Size(208, 25);
            lblTotalAcumuladoReal.TabIndex = 109;
            lblTotalAcumuladoReal.Text = "lblTotalAcumuladoReal";
            // 
            // lblTotalAcumuladoVenta
            // 
            lblTotalAcumuladoVenta.AutoSize = true;
            lblTotalAcumuladoVenta.Font = new Font("Segoe UI Semibold", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTotalAcumuladoVenta.Location = new Point(411, 773);
            lblTotalAcumuladoVenta.Name = "lblTotalAcumuladoVenta";
            lblTotalAcumuladoVenta.Size = new Size(221, 25);
            lblTotalAcumuladoVenta.TabIndex = 110;
            lblTotalAcumuladoVenta.Text = "lblTotalAcumuladoVenta";
            // 
            // lblTotalPerdidoConREspectoAlVentayREal
            // 
            lblTotalPerdidoConREspectoAlVentayREal.AutoSize = true;
            lblTotalPerdidoConREspectoAlVentayREal.Font = new Font("Segoe UI Semibold", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTotalPerdidoConREspectoAlVentayREal.Location = new Point(411, 817);
            lblTotalPerdidoConREspectoAlVentayREal.Name = "lblTotalPerdidoConREspectoAlVentayREal";
            lblTotalPerdidoConREspectoAlVentayREal.Size = new Size(365, 25);
            lblTotalPerdidoConREspectoAlVentayREal.TabIndex = 111;
            lblTotalPerdidoConREspectoAlVentayREal.Text = "lblTotalPerdidoConREspectoAlVentayREal";
            // 
            // txtPorcentajeDescuento
            // 
            txtPorcentajeDescuento.Location = new Point(30, 817);
            txtPorcentajeDescuento.Name = "txtPorcentajeDescuento";
            txtPorcentajeDescuento.Size = new Size(100, 23);
            txtPorcentajeDescuento.TabIndex = 112;
            txtPorcentajeDescuento.TextChanged += txtPorcentajeDescuento_TextChanged;
            // 
            // lblDescuentoPor
            // 
            lblDescuentoPor.AutoSize = true;
            lblDescuentoPor.Font = new Font("Segoe UI Semibold", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblDescuentoPor.Location = new Point(30, 777);
            lblDescuentoPor.Name = "lblDescuentoPor";
            lblDescuentoPor.Size = new Size(124, 25);
            lblDescuentoPor.TabIndex = 113;
            lblDescuentoPor.Text = "Descuento %";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI Semibold", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label3.Location = new Point(198, 777);
            label3.Name = "label3";
            label3.Size = new Size(81, 25);
            label3.TabIndex = 115;
            label3.Text = "Precio $";
            // 
            // txtPrecioFinal
            // 
            txtPrecioFinal.Location = new Point(198, 817);
            txtPrecioFinal.Name = "txtPrecioFinal";
            txtPrecioFinal.Size = new Size(100, 23);
            txtPrecioFinal.TabIndex = 114;
            txtPrecioFinal.TextChanged += txtPrecioFinal_TextChanged;
            // 
            // dtpFechaInicio
            // 
            dtpFechaInicio.Location = new Point(98, 860);
            dtpFechaInicio.Name = "dtpFechaInicio";
            dtpFechaInicio.Size = new Size(200, 23);
            dtpFechaInicio.TabIndex = 116;
            dtpFechaInicio.ValueChanged += dtpFechaInicio_ValueChanged;
            // 
            // dtpFechaFin
            // 
            dtpFechaFin.Location = new Point(98, 889);
            dtpFechaFin.Name = "dtpFechaFin";
            dtpFechaFin.Size = new Size(200, 23);
            dtpFechaFin.TabIndex = 117;
            dtpFechaFin.ValueChanged += dtpFechaFin_ValueChanged;
            // 
            // lblFechaInicio
            // 
            lblFechaInicio.AutoSize = true;
            lblFechaInicio.Font = new Font("Segoe UI Semibold", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblFechaInicio.Location = new Point(14, 862);
            lblFechaInicio.Name = "lblFechaInicio";
            lblFechaInicio.Size = new Size(78, 25);
            lblFechaInicio.TabIndex = 118;
            lblFechaInicio.Text = "F. Inicio";
            // 
            // lblFechaFin
            // 
            lblFechaFin.AutoSize = true;
            lblFechaFin.Font = new Font("Segoe UI Semibold", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblFechaFin.Location = new Point(35, 887);
            lblFechaFin.Name = "lblFechaFin";
            lblFechaFin.Size = new Size(57, 25);
            lblFechaFin.TabIndex = 119;
            lblFechaFin.Text = "F. Fin";
            // 
            // lblLimiteStock
            // 
            lblLimiteStock.AutoSize = true;
            lblLimiteStock.Location = new Point(20, 962);
            lblLimiteStock.Name = "lblLimiteStock";
            lblLimiteStock.Size = new Size(43, 15);
            lblLimiteStock.TabIndex = 122;
            lblLimiteStock.Text = "Limite ";
            // 
            // txtLimiteStock
            // 
            txtLimiteStock.Enabled = false;
            txtLimiteStock.Location = new Point(80, 959);
            txtLimiteStock.Name = "txtLimiteStock";
            txtLimiteStock.Size = new Size(137, 23);
            txtLimiteStock.TabIndex = 121;
            txtLimiteStock.TextChanged += txtLimiteStock_TextChanged;
            // 
            // cbxLimiteCumplirStock
            // 
            cbxLimiteCumplirStock.AutoSize = true;
            cbxLimiteCumplirStock.Location = new Point(19, 934);
            cbxLimiteCumplirStock.Name = "cbxLimiteCumplirStock";
            cbxLimiteCumplirStock.Size = new Size(209, 19);
            cbxLimiteCumplirStock.TabIndex = 120;
            cbxLimiteCumplirStock.Text = "Agregar limite general a productos";
            cbxLimiteCumplirStock.UseVisualStyleBackColor = true;
            cbxLimiteCumplirStock.CheckedChanged += cbxLimiteCumplirStock_CheckedChanged;
            // 
            // btnControlStockDisponible
            // 
            btnControlStockDisponible.Location = new Point(245, 934);
            btnControlStockDisponible.Name = "btnControlStockDisponible";
            btnControlStockDisponible.Size = new Size(140, 43);
            btnControlStockDisponible.TabIndex = 123;
            btnControlStockDisponible.Text = "Aplicar limite de stock";
            btnControlStockDisponible.UseVisualStyleBackColor = true;
            btnControlStockDisponible.Click += btnControlStockDisponible_Click;
            // 
            // btnCrearOferta
            // 
            btnCrearOferta.Location = new Point(906, 840);
            btnCrearOferta.Name = "btnCrearOferta";
            btnCrearOferta.Size = new Size(140, 43);
            btnCrearOferta.TabIndex = 124;
            btnCrearOferta.Text = "Crear Oferta";
            btnCrearOferta.UseVisualStyleBackColor = true;
            btnCrearOferta.Click += btnCrearOferta_Click;
            // 
            // btnLimpiarCampos
            // 
            btnLimpiarCampos.Location = new Point(906, 777);
            btnLimpiarCampos.Name = "btnLimpiarCampos";
            btnLimpiarCampos.Size = new Size(140, 43);
            btnLimpiarCampos.TabIndex = 125;
            btnLimpiarCampos.Text = "Limpiar Campos";
            btnLimpiarCampos.UseVisualStyleBackColor = true;
            btnLimpiarCampos.Click += btnLimpiarCampos_Click;
            // 
            // btnCancelarYSalir
            // 
            btnCancelarYSalir.Location = new Point(906, 894);
            btnCancelarYSalir.Name = "btnCancelarYSalir";
            btnCancelarYSalir.Size = new Size(140, 43);
            btnCancelarYSalir.TabIndex = 126;
            btnCancelarYSalir.Text = "Cancelar y Salir";
            btnCancelarYSalir.UseVisualStyleBackColor = true;
            btnCancelarYSalir.Click += btnCancelarYSalir_Click;
            // 
            // lblTotalFinal
            // 
            lblTotalFinal.AutoSize = true;
            lblTotalFinal.Font = new Font("Segoe UI Semibold", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTotalFinal.Location = new Point(411, 858);
            lblTotalFinal.Name = "lblTotalFinal";
            lblTotalFinal.Size = new Size(115, 25);
            lblTotalFinal.TabIndex = 127;
            lblTotalFinal.Text = "lblTotalFinal";
            // 
            // FOfertaABM
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1279, 994);
            Controls.Add(lblTotalFinal);
            Controls.Add(btnCancelarYSalir);
            Controls.Add(btnLimpiarCampos);
            Controls.Add(btnCrearOferta);
            Controls.Add(btnControlStockDisponible);
            Controls.Add(lblLimiteStock);
            Controls.Add(txtLimiteStock);
            Controls.Add(cbxLimiteCumplirStock);
            Controls.Add(lblFechaFin);
            Controls.Add(lblFechaInicio);
            Controls.Add(dtpFechaFin);
            Controls.Add(dtpFechaInicio);
            Controls.Add(label3);
            Controls.Add(txtPrecioFinal);
            Controls.Add(lblDescuentoPor);
            Controls.Add(txtPorcentajeDescuento);
            Controls.Add(lblTotalPerdidoConREspectoAlVentayREal);
            Controls.Add(lblTotalAcumuladoVenta);
            Controls.Add(lblTotalAcumuladoReal);
            Controls.Add(txtDescripcion);
            Controls.Add(lblDescripcion);
            Controls.Add(lblCodigoOf);
            Controls.Add(txtCodigo);
            Controls.Add(btnCargarProducto);
            Controls.Add(btnDevolverAOferta);
            Controls.Add(label2);
            Controls.Add(dgvProductosQuitados);
            Controls.Add(lblNumeroProductoQuitados);
            Controls.Add(lblCantidadProductosQuitados);
            Controls.Add(btnQuitarProducto);
            Controls.Add(label1);
            Controls.Add(dgvProductos);
            Controls.Add(lblNumeroProductoAfectados);
            Controls.Add(lblCantidadProductos);
            Controls.Add(tableLayoutPanel2);
            ForeColor = Color.FromArgb(31, 26, 43);
            Name = "FOfertaABM";
            Text = "FOfertaABM";
            Load += FOfertaABM_Load;
            ((System.ComponentModel.ISupportInitialize)error).EndInit();
            tableLayoutPanel2.ResumeLayout(false);
            tableLayoutPanel2.PerformLayout();
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvProductosQuitados).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvProductos).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TableLayoutPanel tableLayoutPanel2;
        private TableLayoutPanel tableLayoutPanel1;
        private Button btnCargarGrupoMarca;
        private Button btnCargarGrupoRubro;
        private Button btnCargarGrupoCategoria;
        private TextBox txtCategorias;
        private Button btnCargarProductosAlcanzados;
        private Label lblDetalleGruposFiltro;
        private Button btnCargarProducto;
        private Button btnDevolverAOferta;
        private Label label2;
        private DataGridView dgvProductosQuitados;
        private Label lblNumeroProductoQuitados;
        private Label lblCantidadProductosQuitados;
        private Button btnQuitarProducto;
        private Label label1;
        private DataGridView dgvProductos;
        private Label lblNumeroProductoAfectados;
        private Label lblCantidadProductos;
        private TextBox txtMarca;
        private TextBox txtRubro;
        private TextBox txtCodigo;
        private Label lblCodigoOf;
        private Label lblDescripcion;
        private TextBox txtDescripcion;
        private Label lblTotalAcumuladoReal;
        private Label lblTotalAcumuladoVenta;
        private Label lblTotalPerdidoConREspectoAlVentayREal;
        private TextBox txtPorcentajeDescuento;
        private Label lblDescuentoPor;
        private Label label3;
        private TextBox txtPrecioFinal;
        private DateTimePicker dtpFechaInicio;
        private DateTimePicker dtpFechaFin;
        private Label lblFechaInicio;
        private Label lblFechaFin;
        private Label lblLimiteStock;
        private TextBox txtLimiteStock;
        private CheckBox cbxLimiteCumplirStock;
        private Button btnControlStockDisponible;
        private Button btnCrearOferta;
        private Button btnLimpiarCampos;
        private Button btnCancelarYSalir;
        private Label lblTotalFinal;
    }
}