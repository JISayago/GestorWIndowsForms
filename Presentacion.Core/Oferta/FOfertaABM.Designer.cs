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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FOfertaABM));
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
            btnDevolverAOferta = new Button();
            label2 = new Label();
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
            tableLayoutPanel21 = new TableLayoutPanel();
            btnCargarProducto = new Button();
            textBox1 = new TextBox();
            tableLayoutPanel4 = new TableLayoutPanel();
            cbxCodigoAutomatico = new CheckBox();
            tableLayoutPanel5 = new TableLayoutPanel();
            tableLayoutPanel6 = new TableLayoutPanel();
            flowLayoutPanel1 = new FlowLayoutPanel();
            tableLayoutPanel8 = new TableLayoutPanel();
            tableLayoutPanel11 = new TableLayoutPanel();
            tableLayoutPanel12 = new TableLayoutPanel();
            tableLayoutPanel13 = new TableLayoutPanel();
            tableLayoutPanel14 = new TableLayoutPanel();
            tableLayoutPanel19 = new TableLayoutPanel();
            tableLayoutPanel18 = new TableLayoutPanel();
            tableLayoutPanel15 = new TableLayoutPanel();
            tableLayoutPanel16 = new TableLayoutPanel();
            pnlExcluidos = new Panel();
            ((System.ComponentModel.ISupportInitialize)error).BeginInit();
            tableLayoutPanel2.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvProductos).BeginInit();
            tableLayoutPanel21.SuspendLayout();
            tableLayoutPanel4.SuspendLayout();
            tableLayoutPanel5.SuspendLayout();
            tableLayoutPanel6.SuspendLayout();
            flowLayoutPanel1.SuspendLayout();
            tableLayoutPanel8.SuspendLayout();
            tableLayoutPanel11.SuspendLayout();
            tableLayoutPanel12.SuspendLayout();
            tableLayoutPanel13.SuspendLayout();
            tableLayoutPanel14.SuspendLayout();
            tableLayoutPanel19.SuspendLayout();
            tableLayoutPanel18.SuspendLayout();
            tableLayoutPanel15.SuspendLayout();
            tableLayoutPanel16.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tableLayoutPanel2.ColumnCount = 1;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel2.Controls.Add(tableLayoutPanel1, 0, 0);
            tableLayoutPanel2.Controls.Add(btnCargarProductosAlcanzados, 0, 1);
            tableLayoutPanel2.Controls.Add(lblDetalleGruposFiltro, 0, 2);
            tableLayoutPanel2.Location = new Point(430, 12);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 3;
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 52.8409081F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 26.704546F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            tableLayoutPanel2.Size = new Size(430, 143);
            tableLayoutPanel2.TabIndex = 93;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tableLayoutPanel1.ColumnCount = 3;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333359F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333359F));
            tableLayoutPanel1.Controls.Add(btnCargarGrupoMarca, 0, 0);
            tableLayoutPanel1.Controls.Add(btnCargarGrupoRubro, 1, 0);
            tableLayoutPanel1.Controls.Add(btnCargarGrupoCategoria, 2, 0);
            tableLayoutPanel1.Controls.Add(txtMarca, 0, 1);
            tableLayoutPanel1.Controls.Add(txtRubro, 1, 1);
            tableLayoutPanel1.Controls.Add(txtCategorias, 2, 1);
            tableLayoutPanel1.Location = new Point(3, 3);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 2;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 60F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 40F));
            tableLayoutPanel1.Size = new Size(424, 69);
            tableLayoutPanel1.TabIndex = 91;
            // 
            // btnCargarGrupoMarca
            // 
            btnCargarGrupoMarca.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            btnCargarGrupoMarca.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnCargarGrupoMarca.Location = new Point(3, 3);
            btnCargarGrupoMarca.Name = "btnCargarGrupoMarca";
            btnCargarGrupoMarca.Size = new Size(135, 35);
            btnCargarGrupoMarca.TabIndex = 27;
            btnCargarGrupoMarca.Text = "Cargar Marca";
            btnCargarGrupoMarca.UseVisualStyleBackColor = true;
            btnCargarGrupoMarca.Click += btnCargarGrupoMarca_Click;
            // 
            // btnCargarGrupoRubro
            // 
            btnCargarGrupoRubro.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            btnCargarGrupoRubro.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnCargarGrupoRubro.Location = new Point(144, 3);
            btnCargarGrupoRubro.Name = "btnCargarGrupoRubro";
            btnCargarGrupoRubro.Size = new Size(135, 35);
            btnCargarGrupoRubro.TabIndex = 64;
            btnCargarGrupoRubro.Text = "Cargar Rubro";
            btnCargarGrupoRubro.UseVisualStyleBackColor = true;
            btnCargarGrupoRubro.Click += btnCargarGrupoRubro_Click;
            // 
            // btnCargarGrupoCategoria
            // 
            btnCargarGrupoCategoria.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            btnCargarGrupoCategoria.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnCargarGrupoCategoria.Location = new Point(285, 3);
            btnCargarGrupoCategoria.Name = "btnCargarGrupoCategoria";
            btnCargarGrupoCategoria.Size = new Size(136, 35);
            btnCargarGrupoCategoria.TabIndex = 66;
            btnCargarGrupoCategoria.Text = "Cargar Categorias";
            btnCargarGrupoCategoria.UseVisualStyleBackColor = true;
            btnCargarGrupoCategoria.Click += btnCargarGrupoCategoria_Click;
            // 
            // txtMarca
            // 
            txtMarca.Anchor = AnchorStyles.Top;
            txtMarca.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtMarca.Location = new Point(3, 44);
            txtMarca.Name = "txtMarca";
            txtMarca.ReadOnly = true;
            txtMarca.Size = new Size(135, 27);
            txtMarca.TabIndex = 34;
            txtMarca.TextAlign = HorizontalAlignment.Center;
            // 
            // txtRubro
            // 
            txtRubro.Anchor = AnchorStyles.Top;
            txtRubro.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtRubro.Location = new Point(144, 44);
            txtRubro.Name = "txtRubro";
            txtRubro.ReadOnly = true;
            txtRubro.Size = new Size(135, 27);
            txtRubro.TabIndex = 65;
            txtRubro.TextAlign = HorizontalAlignment.Center;
            // 
            // txtCategorias
            // 
            txtCategorias.Anchor = AnchorStyles.Top;
            txtCategorias.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtCategorias.Location = new Point(285, 44);
            txtCategorias.Name = "txtCategorias";
            txtCategorias.ReadOnly = true;
            txtCategorias.Size = new Size(136, 27);
            txtCategorias.TabIndex = 67;
            txtCategorias.TextAlign = HorizontalAlignment.Center;
            // 
            // btnCargarProductosAlcanzados
            // 
            btnCargarProductosAlcanzados.Anchor = AnchorStyles.None;
            btnCargarProductosAlcanzados.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnCargarProductosAlcanzados.Location = new Point(72, 78);
            btnCargarProductosAlcanzados.Name = "btnCargarProductosAlcanzados";
            btnCargarProductosAlcanzados.Size = new Size(285, 32);
            btnCargarProductosAlcanzados.TabIndex = 72;
            btnCargarProductosAlcanzados.Text = "Cargar Productos Alcanzados";
            btnCargarProductosAlcanzados.UseVisualStyleBackColor = true;
            btnCargarProductosAlcanzados.Click += btnCargarProductosAlcanzados_Click;
            // 
            // lblDetalleGruposFiltro
            // 
            lblDetalleGruposFiltro.Anchor = AnchorStyles.None;
            lblDetalleGruposFiltro.AutoSize = true;
            lblDetalleGruposFiltro.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblDetalleGruposFiltro.Location = new Point(28, 118);
            lblDetalleGruposFiltro.Name = "lblDetalleGruposFiltro";
            lblDetalleGruposFiltro.Size = new Size(373, 20);
            lblDetalleGruposFiltro.TabIndex = 92;
            lblDetalleGruposFiltro.Text = "Productos alcanzados por: Marca - Categoria - Rubro";
            // 
            // btnDevolverAOferta
            // 
            btnDevolverAOferta.Anchor = AnchorStyles.Bottom;
            btnDevolverAOferta.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnDevolverAOferta.Location = new Point(839, 594);
            btnDevolverAOferta.Name = "btnDevolverAOferta";
            btnDevolverAOferta.Size = new Size(161, 32);
            btnDevolverAOferta.TabIndex = 103;
            btnDevolverAOferta.Text = "Volver a Oferta";
            btnDevolverAOferta.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(1070, 609);
            label2.Name = "label2";
            label2.Size = new Size(0, 15);
            label2.TabIndex = 102;
            // 
            // lblNumeroProductoQuitados
            // 
            lblNumeroProductoQuitados.AutoSize = true;
            lblNumeroProductoQuitados.Font = new Font("Segoe UI", 20.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblNumeroProductoQuitados.Location = new Point(1094, 538);
            lblNumeroProductoQuitados.Name = "lblNumeroProductoQuitados";
            lblNumeroProductoQuitados.Size = new Size(52, 37);
            lblNumeroProductoQuitados.TabIndex = 100;
            lblNumeroProductoQuitados.Tag = "NoModificarConBase";
            lblNumeroProductoQuitados.Text = "N1";
            // 
            // lblCantidadProductosQuitados
            // 
            lblCantidadProductosQuitados.AutoSize = true;
            lblCantidadProductosQuitados.Font = new Font("Segoe UI", 20.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblCantidadProductosQuitados.Location = new Point(766, 538);
            lblCantidadProductosQuitados.Name = "lblCantidadProductosQuitados";
            lblCantidadProductosQuitados.Size = new Size(321, 37);
            lblCantidadProductosQuitados.TabIndex = 99;
            lblCantidadProductosQuitados.Text = "Productos fuera de Oferta";
            // 
            // btnQuitarProducto
            // 
            btnQuitarProducto.Anchor = AnchorStyles.Bottom;
            btnQuitarProducto.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnQuitarProducto.Location = new Point(444, 8);
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
            dgvProductos.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvProductos.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvProductos.EditMode = DataGridViewEditMode.EditProgrammatically;
            dgvProductos.Location = new Point(27, 388);
            dgvProductos.MultiSelect = false;
            dgvProductos.Name = "dgvProductos";
            dgvProductos.Size = new Size(519, 92);
            dgvProductos.TabIndex = 96;
            dgvProductos.CellMouseDown += dgvProductos_CellMouseDown_1;
            // 
            // lblNumeroProductoAfectados
            // 
            lblNumeroProductoAfectados.AutoSize = true;
            lblNumeroProductoAfectados.Font = new Font("Segoe UI", 20.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblNumeroProductoAfectados.Location = new Point(262, 0);
            lblNumeroProductoAfectados.Name = "lblNumeroProductoAfectados";
            lblNumeroProductoAfectados.Size = new Size(52, 37);
            lblNumeroProductoAfectados.TabIndex = 95;
            lblNumeroProductoAfectados.Tag = "NoModificarConBase";
            lblNumeroProductoAfectados.Text = "N1";
            // 
            // lblCantidadProductos
            // 
            lblCantidadProductos.AutoSize = true;
            lblCantidadProductos.Font = new Font("Segoe UI", 20.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblCantidadProductos.Location = new Point(3, 0);
            lblCantidadProductos.Name = "lblCantidadProductos";
            lblCantidadProductos.Size = new Size(253, 37);
            lblCantidadProductos.TabIndex = 94;
            lblCantidadProductos.Text = "Productos en Oferta";
            // 
            // txtCodigo
            // 
            txtCodigo.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtCodigo.Location = new Point(3, 35);
            txtCodigo.Name = "txtCodigo";
            txtCodigo.Size = new Size(130, 27);
            txtCodigo.TabIndex = 105;
            // 
            // lblCodigoOf
            // 
            lblCodigoOf.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            lblCodigoOf.AutoSize = true;
            lblCodigoOf.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblCodigoOf.Location = new Point(3, 11);
            lblCodigoOf.Name = "lblCodigoOf";
            lblCodigoOf.Size = new Size(60, 21);
            lblCodigoOf.TabIndex = 106;
            lblCodigoOf.Text = "Código";
            // 
            // lblDescripcion
            // 
            lblDescripcion.AutoSize = true;
            lblDescripcion.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblDescripcion.Location = new Point(3, 0);
            lblDescripcion.Name = "lblDescripcion";
            lblDescripcion.Size = new Size(87, 20);
            lblDescripcion.TabIndex = 107;
            lblDescripcion.Text = "Descripcion";
            // 
            // txtDescripcion
            // 
            txtDescripcion.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            txtDescripcion.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtDescripcion.Location = new Point(3, 24);
            txtDescripcion.Multiline = true;
            txtDescripcion.Name = "txtDescripcion";
            txtDescripcion.ReadOnly = true;
            txtDescripcion.Size = new Size(266, 46);
            txtDescripcion.TabIndex = 108;
            // 
            // lblTotalAcumuladoReal
            // 
            lblTotalAcumuladoReal.Anchor = AnchorStyles.Top;
            lblTotalAcumuladoReal.AutoSize = true;
            lblTotalAcumuladoReal.Font = new Font("Segoe UI Semibold", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTotalAcumuladoReal.Location = new Point(106, 0);
            lblTotalAcumuladoReal.Name = "lblTotalAcumuladoReal";
            lblTotalAcumuladoReal.Size = new Size(367, 25);
            lblTotalAcumuladoReal.TabIndex = 109;
            lblTotalAcumuladoReal.Text = "Monto acumulado de precio Costo: $0,00";
            lblTotalAcumuladoReal.TextAlign = ContentAlignment.TopCenter;
            // 
            // lblTotalAcumuladoVenta
            // 
            lblTotalAcumuladoVenta.Anchor = AnchorStyles.Top;
            lblTotalAcumuladoVenta.AutoSize = true;
            lblTotalAcumuladoVenta.Font = new Font("Segoe UI Semibold", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTotalAcumuladoVenta.Location = new Point(106, 41);
            lblTotalAcumuladoVenta.Name = "lblTotalAcumuladoVenta";
            lblTotalAcumuladoVenta.Size = new Size(367, 25);
            lblTotalAcumuladoVenta.TabIndex = 110;
            lblTotalAcumuladoVenta.Text = "Monto acumulado de precio Venta: $0,00";
            lblTotalAcumuladoVenta.TextAlign = ContentAlignment.TopCenter;
            // 
            // lblTotalPerdidoConREspectoAlVentayREal
            // 
            lblTotalPerdidoConREspectoAlVentayREal.Anchor = AnchorStyles.Top;
            lblTotalPerdidoConREspectoAlVentayREal.AutoSize = true;
            lblTotalPerdidoConREspectoAlVentayREal.Font = new Font("Segoe UI Semibold", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTotalPerdidoConREspectoAlVentayREal.Location = new Point(170, 82);
            lblTotalPerdidoConREspectoAlVentayREal.Name = "lblTotalPerdidoConREspectoAlVentayREal";
            lblTotalPerdidoConREspectoAlVentayREal.Size = new Size(240, 25);
            lblTotalPerdidoConREspectoAlVentayREal.TabIndex = 111;
            lblTotalPerdidoConREspectoAlVentayREal.Text = "Sin descuento configurado";
            // 
            // txtPorcentajeDescuento
            // 
            txtPorcentajeDescuento.Anchor = AnchorStyles.Top;
            txtPorcentajeDescuento.Font = new Font("Segoe UI", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtPorcentajeDescuento.Location = new Point(3, 27);
            txtPorcentajeDescuento.Name = "txtPorcentajeDescuento";
            txtPorcentajeDescuento.Size = new Size(240, 35);
            txtPorcentajeDescuento.TabIndex = 112;
            txtPorcentajeDescuento.TextChanged += txtPorcentajeDescuento_TextChanged;
            // 
            // lblDescuentoPor
            // 
            lblDescuentoPor.Anchor = AnchorStyles.None;
            lblDescuentoPor.AutoSize = true;
            lblDescuentoPor.Font = new Font("Segoe UI Semibold", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblDescuentoPor.Location = new Point(61, 0);
            lblDescuentoPor.Name = "lblDescuentoPor";
            lblDescuentoPor.Size = new Size(124, 24);
            lblDescuentoPor.TabIndex = 113;
            lblDescuentoPor.Text = "Descuento %";
            // 
            // label3
            // 
            label3.Anchor = AnchorStyles.None;
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI Semibold", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label3.Location = new Point(82, 0);
            label3.Name = "label3";
            label3.Size = new Size(81, 24);
            label3.TabIndex = 115;
            label3.Text = "Precio $";
            // 
            // txtPrecioFinal
            // 
            txtPrecioFinal.Anchor = AnchorStyles.Top;
            txtPrecioFinal.Font = new Font("Segoe UI", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtPrecioFinal.Location = new Point(3, 27);
            txtPrecioFinal.Name = "txtPrecioFinal";
            txtPrecioFinal.Size = new Size(240, 35);
            txtPrecioFinal.TabIndex = 114;
            txtPrecioFinal.TextChanged += txtPrecioFinal_TextChanged;
            // 
            // dtpFechaInicio
            // 
            dtpFechaInicio.Anchor = AnchorStyles.Top;
            dtpFechaInicio.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dtpFechaInicio.Location = new Point(87, 47);
            dtpFechaInicio.Name = "dtpFechaInicio";
            dtpFechaInicio.Size = new Size(447, 33);
            dtpFechaInicio.TabIndex = 116;
            dtpFechaInicio.ValueChanged += dtpFechaInicio_ValueChanged;
            // 
            // dtpFechaFin
            // 
            dtpFechaFin.Anchor = AnchorStyles.Top;
            dtpFechaFin.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dtpFechaFin.Location = new Point(94, 50);
            dtpFechaFin.Name = "dtpFechaFin";
            dtpFechaFin.Size = new Size(433, 33);
            dtpFechaFin.TabIndex = 117;
            dtpFechaFin.ValueChanged += dtpFechaFin_ValueChanged;
            // 
            // lblFechaInicio
            // 
            lblFechaInicio.Anchor = AnchorStyles.Top;
            lblFechaInicio.AutoSize = true;
            lblFechaInicio.Font = new Font("Segoe UI Semibold", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblFechaInicio.Location = new Point(189, 0);
            lblFechaInicio.Name = "lblFechaInicio";
            lblFechaInicio.Size = new Size(243, 25);
            lblFechaInicio.TabIndex = 118;
            lblFechaInicio.Text = "Fecha de inicio de la oferta";
            // 
            // lblFechaFin
            // 
            lblFechaFin.Anchor = AnchorStyles.Top;
            lblFechaFin.AutoSize = true;
            lblFechaFin.Font = new Font("Segoe UI Semibold", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblFechaFin.Location = new Point(163, 0);
            lblFechaFin.Name = "lblFechaFin";
            lblFechaFin.Size = new Size(295, 25);
            lblFechaFin.TabIndex = 119;
            lblFechaFin.Text = "Fecha de finalización de la oferta";
            // 
            // lblLimiteStock
            // 
            lblLimiteStock.Anchor = AnchorStyles.Right;
            lblLimiteStock.AutoSize = true;
            lblLimiteStock.Font = new Font("Segoe UI", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblLimiteStock.Location = new Point(4, 0);
            lblLimiteStock.Name = "lblLimiteStock";
            lblLimiteStock.Size = new Size(51, 34);
            lblLimiteStock.TabIndex = 122;
            lblLimiteStock.Text = "Limite ";
            lblLimiteStock.TextAlign = ContentAlignment.MiddleRight;
            // 
            // txtLimiteStock
            // 
            txtLimiteStock.Anchor = AnchorStyles.Left;
            txtLimiteStock.Enabled = false;
            txtLimiteStock.Font = new Font("Segoe UI", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtLimiteStock.Location = new Point(61, 3);
            txtLimiteStock.Name = "txtLimiteStock";
            txtLimiteStock.Size = new Size(53, 35);
            txtLimiteStock.TabIndex = 121;
            txtLimiteStock.TextChanged += txtLimiteStock_TextChanged;
            // 
            // cbxLimiteCumplirStock
            // 
            cbxLimiteCumplirStock.Anchor = AnchorStyles.Right;
            cbxLimiteCumplirStock.AutoSize = true;
            cbxLimiteCumplirStock.Font = new Font("Segoe UI", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            cbxLimiteCumplirStock.Location = new Point(3, 74);
            cbxLimiteCumplirStock.Name = "cbxLimiteCumplirStock";
            cbxLimiteCumplirStock.Size = new Size(246, 34);
            cbxLimiteCumplirStock.TabIndex = 120;
            cbxLimiteCumplirStock.Text = "Agregar limite general a productos";
            cbxLimiteCumplirStock.UseVisualStyleBackColor = true;
            cbxLimiteCumplirStock.CheckedChanged += cbxLimiteCumplirStock_CheckedChanged;
            // 
            // btnControlStockDisponible
            // 
            btnControlStockDisponible.Anchor = AnchorStyles.None;
            btnControlStockDisponible.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnControlStockDisponible.Location = new Point(126, 3);
            btnControlStockDisponible.Name = "btnControlStockDisponible";
            btnControlStockDisponible.Size = new Size(117, 34);
            btnControlStockDisponible.TabIndex = 123;
            btnControlStockDisponible.Text = "Aplicar límite";
            btnControlStockDisponible.UseVisualStyleBackColor = true;
            btnControlStockDisponible.Click += btnControlStockDisponible_Click;
            // 
            // btnCrearOferta
            // 
            btnCrearOferta.Anchor = AnchorStyles.None;
            btnCrearOferta.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnCrearOferta.Location = new Point(1094, 927);
            btnCrearOferta.Name = "btnCrearOferta";
            btnCrearOferta.Size = new Size(140, 55);
            btnCrearOferta.TabIndex = 124;
            btnCrearOferta.Text = "Crear Oferta";
            btnCrearOferta.UseVisualStyleBackColor = true;
            btnCrearOferta.Click += btnCrearOferta_Click;
            // 
            // btnLimpiarCampos
            // 
            btnLimpiarCampos.Anchor = AnchorStyles.Right;
            btnLimpiarCampos.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnLimpiarCampos.Location = new Point(933, 927);
            btnLimpiarCampos.Name = "btnLimpiarCampos";
            btnLimpiarCampos.Size = new Size(140, 55);
            btnLimpiarCampos.TabIndex = 125;
            btnLimpiarCampos.Text = "Limpiar Campos";
            btnLimpiarCampos.UseVisualStyleBackColor = true;
            btnLimpiarCampos.Click += btnLimpiarCampos_Click;
            // 
            // btnCancelarYSalir
            // 
            btnCancelarYSalir.Anchor = AnchorStyles.None;
            btnCancelarYSalir.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnCancelarYSalir.Location = new Point(777, 927);
            btnCancelarYSalir.Name = "btnCancelarYSalir";
            btnCancelarYSalir.Size = new Size(140, 56);
            btnCancelarYSalir.TabIndex = 126;
            btnCancelarYSalir.Text = "Cancelar y Salir";
            btnCancelarYSalir.UseVisualStyleBackColor = true;
            btnCancelarYSalir.Click += btnCancelarYSalir_Click;
            // 
            // lblTotalFinal
            // 
            lblTotalFinal.Anchor = AnchorStyles.Top;
            lblTotalFinal.AutoSize = true;
            lblTotalFinal.Font = new Font("Segoe UI Semibold", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTotalFinal.Location = new Point(170, 125);
            lblTotalFinal.Name = "lblTotalFinal";
            lblTotalFinal.Size = new Size(240, 25);
            lblTotalFinal.TabIndex = 127;
            lblTotalFinal.Text = "Sin descuento configurado";
            // 
            // tableLayoutPanel21
            // 
            tableLayoutPanel21.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tableLayoutPanel21.ColumnCount = 1;
            tableLayoutPanel21.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel21.Controls.Add(btnCargarProducto, 0, 0);
            tableLayoutPanel21.Controls.Add(textBox1, 0, 1);
            tableLayoutPanel21.Location = new Point(1037, 31);
            tableLayoutPanel21.Name = "tableLayoutPanel21";
            tableLayoutPanel21.RowCount = 2;
            tableLayoutPanel21.RowStyles.Add(new RowStyle(SizeType.Percent, 31.25F));
            tableLayoutPanel21.RowStyles.Add(new RowStyle(SizeType.Percent, 68.75F));
            tableLayoutPanel21.Size = new Size(182, 143);
            tableLayoutPanel21.TabIndex = 148;
            // 
            // btnCargarProducto
            // 
            btnCargarProducto.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            btnCargarProducto.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnCargarProducto.Location = new Point(3, 5);
            btnCargarProducto.Margin = new Padding(3, 5, 3, 3);
            btnCargarProducto.Name = "btnCargarProducto";
            btnCargarProducto.Size = new Size(176, 36);
            btnCargarProducto.TabIndex = 104;
            btnCargarProducto.Text = "Cargar Producto/s para Combo";
            btnCargarProducto.UseVisualStyleBackColor = true;
            btnCargarProducto.Click += btnCargarProducto_Click;
            // 
            // textBox1
            // 
            textBox1.Anchor = AnchorStyles.Top;
            textBox1.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            textBox1.Location = new Point(3, 47);
            textBox1.Name = "textBox1";
            textBox1.ReadOnly = true;
            textBox1.Size = new Size(176, 27);
            textBox1.TabIndex = 147;
            textBox1.TextAlign = HorizontalAlignment.Center;
            // 
            // tableLayoutPanel4
            // 
            tableLayoutPanel4.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            tableLayoutPanel4.ColumnCount = 2;
            tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel4.Controls.Add(lblCodigoOf, 0, 0);
            tableLayoutPanel4.Controls.Add(txtCodigo, 0, 1);
            tableLayoutPanel4.Controls.Add(cbxCodigoAutomatico, 1, 1);
            tableLayoutPanel4.Location = new Point(3, 119);
            tableLayoutPanel4.Name = "tableLayoutPanel4";
            tableLayoutPanel4.RowCount = 2;
            tableLayoutPanel4.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel4.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel4.Size = new Size(272, 64);
            tableLayoutPanel4.TabIndex = 129;
            // 
            // cbxCodigoAutomatico
            // 
            cbxCodigoAutomatico.AutoSize = true;
            cbxCodigoAutomatico.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            cbxCodigoAutomatico.Location = new Point(139, 35);
            cbxCodigoAutomatico.Name = "cbxCodigoAutomatico";
            cbxCodigoAutomatico.Size = new Size(130, 25);
            cbxCodigoAutomatico.TabIndex = 107;
            cbxCodigoAutomatico.Text = "Generar código automático";
            cbxCodigoAutomatico.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel5
            // 
            tableLayoutPanel5.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            tableLayoutPanel5.ColumnCount = 1;
            tableLayoutPanel5.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel5.Controls.Add(txtDescripcion, 0, 1);
            tableLayoutPanel5.Controls.Add(lblDescripcion, 0, 0);
            tableLayoutPanel5.Location = new Point(3, 3);
            tableLayoutPanel5.Name = "tableLayoutPanel5";
            tableLayoutPanel5.RowCount = 2;
            tableLayoutPanel5.RowStyles.Add(new RowStyle(SizeType.Percent, 30F));
            tableLayoutPanel5.RowStyles.Add(new RowStyle(SizeType.Percent, 70F));
            tableLayoutPanel5.Size = new Size(272, 73);
            tableLayoutPanel5.TabIndex = 130;
            // 
            // tableLayoutPanel6
            // 
            tableLayoutPanel6.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            tableLayoutPanel6.ColumnCount = 1;
            tableLayoutPanel6.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel6.Controls.Add(tableLayoutPanel5, 0, 0);
            tableLayoutPanel6.Controls.Add(tableLayoutPanel4, 0, 1);
            tableLayoutPanel6.Location = new Point(24, 31);
            tableLayoutPanel6.Name = "tableLayoutPanel6";
            tableLayoutPanel6.RowCount = 2;
            tableLayoutPanel6.RowStyles.Add(new RowStyle(SizeType.Percent, 53.2258072F));
            tableLayoutPanel6.RowStyles.Add(new RowStyle(SizeType.Percent, 46.7741928F));
            tableLayoutPanel6.Size = new Size(278, 219);
            tableLayoutPanel6.TabIndex = 131;
            // 
            // flowLayoutPanel1
            // 
            flowLayoutPanel1.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            flowLayoutPanel1.Controls.Add(lblCantidadProductos);
            flowLayoutPanel1.Controls.Add(lblNumeroProductoAfectados);
            flowLayoutPanel1.Location = new Point(3, 5);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            flowLayoutPanel1.Size = new Size(426, 35);
            flowLayoutPanel1.TabIndex = 132;
            // 
            // tableLayoutPanel8
            // 
            tableLayoutPanel8.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tableLayoutPanel8.ColumnCount = 2;
            tableLayoutPanel8.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 70F));
            tableLayoutPanel8.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30F));
            tableLayoutPanel8.Controls.Add(btnQuitarProducto, 1, 0);
            tableLayoutPanel8.Controls.Add(flowLayoutPanel1, 0, 0);
            tableLayoutPanel8.Location = new Point(24, 292);
            tableLayoutPanel8.Name = "tableLayoutPanel8";
            tableLayoutPanel8.RowCount = 1;
            tableLayoutPanel8.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel8.Size = new Size(618, 43);
            tableLayoutPanel8.TabIndex = 134;
            // 
            // tableLayoutPanel11
            // 
            tableLayoutPanel11.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tableLayoutPanel11.ColumnCount = 1;
            tableLayoutPanel11.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel11.Controls.Add(lblTotalAcumuladoReal, 0, 0);
            tableLayoutPanel11.Controls.Add(lblTotalAcumuladoVenta, 0, 1);
            tableLayoutPanel11.Controls.Add(lblTotalPerdidoConREspectoAlVentayREal, 0, 2);
            tableLayoutPanel11.Controls.Add(lblTotalFinal, 0, 3);
            tableLayoutPanel11.Location = new Point(702, 739);
            tableLayoutPanel11.Name = "tableLayoutPanel11";
            tableLayoutPanel11.RowCount = 4;
            tableLayoutPanel11.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel11.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel11.RowStyles.Add(new RowStyle(SizeType.Absolute, 43F));
            tableLayoutPanel11.RowStyles.Add(new RowStyle(SizeType.Absolute, 53F));
            tableLayoutPanel11.Size = new Size(580, 179);
            tableLayoutPanel11.TabIndex = 137;
            // 
            // tableLayoutPanel12
            // 
            tableLayoutPanel12.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tableLayoutPanel12.ColumnCount = 1;
            tableLayoutPanel12.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel12.Controls.Add(lblDescuentoPor, 0, 0);
            tableLayoutPanel12.Controls.Add(txtPorcentajeDescuento, 0, 1);
            tableLayoutPanel12.Location = new Point(255, 3);
            tableLayoutPanel12.Name = "tableLayoutPanel12";
            tableLayoutPanel12.RowCount = 2;
            tableLayoutPanel12.RowStyles.Add(new RowStyle(SizeType.Percent, 40F));
            tableLayoutPanel12.RowStyles.Add(new RowStyle(SizeType.Percent, 60F));
            tableLayoutPanel12.Size = new Size(246, 62);
            tableLayoutPanel12.TabIndex = 138;
            // 
            // tableLayoutPanel13
            // 
            tableLayoutPanel13.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tableLayoutPanel13.ColumnCount = 1;
            tableLayoutPanel13.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel13.Controls.Add(label3, 0, 0);
            tableLayoutPanel13.Controls.Add(txtPrecioFinal, 0, 1);
            tableLayoutPanel13.Location = new Point(3, 3);
            tableLayoutPanel13.Name = "tableLayoutPanel13";
            tableLayoutPanel13.RowCount = 2;
            tableLayoutPanel13.RowStyles.Add(new RowStyle(SizeType.Percent, 40F));
            tableLayoutPanel13.RowStyles.Add(new RowStyle(SizeType.Percent, 60F));
            tableLayoutPanel13.Size = new Size(246, 62);
            tableLayoutPanel13.TabIndex = 139;
            // 
            // tableLayoutPanel14
            // 
            tableLayoutPanel14.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tableLayoutPanel14.ColumnCount = 2;
            tableLayoutPanel14.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel14.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel14.Controls.Add(cbxLimiteCumplirStock, 0, 1);
            tableLayoutPanel14.Controls.Add(tableLayoutPanel19, 1, 1);
            tableLayoutPanel14.Controls.Add(tableLayoutPanel13, 0, 0);
            tableLayoutPanel14.Controls.Add(tableLayoutPanel12, 1, 0);
            tableLayoutPanel14.Location = new Point(99, 591);
            tableLayoutPanel14.Name = "tableLayoutPanel14";
            tableLayoutPanel14.RowCount = 2;
            tableLayoutPanel14.RowStyles.Add(new RowStyle(SizeType.Percent, 59.86842F));
            tableLayoutPanel14.RowStyles.Add(new RowStyle(SizeType.Percent, 40.13158F));
            tableLayoutPanel14.Size = new Size(504, 114);
            tableLayoutPanel14.TabIndex = 140;
            // 
            // tableLayoutPanel19
            // 
            tableLayoutPanel19.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tableLayoutPanel19.ColumnCount = 2;
            tableLayoutPanel19.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel19.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel19.Controls.Add(tableLayoutPanel18, 0, 0);
            tableLayoutPanel19.Controls.Add(btnControlStockDisponible, 1, 0);
            tableLayoutPanel19.Location = new Point(255, 71);
            tableLayoutPanel19.Name = "tableLayoutPanel19";
            tableLayoutPanel19.RowCount = 1;
            tableLayoutPanel19.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel19.Size = new Size(246, 40);
            tableLayoutPanel19.TabIndex = 145;
            // 
            // tableLayoutPanel18
            // 
            tableLayoutPanel18.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            tableLayoutPanel18.ColumnCount = 2;
            tableLayoutPanel18.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel18.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel18.Controls.Add(txtLimiteStock, 1, 0);
            tableLayoutPanel18.Controls.Add(lblLimiteStock, 0, 0);
            tableLayoutPanel18.Location = new Point(3, 3);
            tableLayoutPanel18.Name = "tableLayoutPanel18";
            tableLayoutPanel18.RowCount = 1;
            tableLayoutPanel18.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel18.Size = new Size(117, 34);
            tableLayoutPanel18.TabIndex = 144;
            // 
            // tableLayoutPanel15
            // 
            tableLayoutPanel15.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tableLayoutPanel15.ColumnCount = 1;
            tableLayoutPanel15.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel15.Controls.Add(lblFechaInicio, 0, 0);
            tableLayoutPanel15.Controls.Add(dtpFechaInicio, 0, 1);
            tableLayoutPanel15.Location = new Point(12, 789);
            tableLayoutPanel15.Name = "tableLayoutPanel15";
            tableLayoutPanel15.RowCount = 2;
            tableLayoutPanel15.RowStyles.Add(new RowStyle(SizeType.Percent, 45F));
            tableLayoutPanel15.RowStyles.Add(new RowStyle(SizeType.Percent, 55F));
            tableLayoutPanel15.Size = new Size(621, 98);
            tableLayoutPanel15.TabIndex = 141;
            // 
            // tableLayoutPanel16
            // 
            tableLayoutPanel16.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tableLayoutPanel16.ColumnCount = 1;
            tableLayoutPanel16.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel16.Controls.Add(lblFechaFin, 0, 0);
            tableLayoutPanel16.Controls.Add(dtpFechaFin, 0, 1);
            tableLayoutPanel16.Location = new Point(12, 893);
            tableLayoutPanel16.Name = "tableLayoutPanel16";
            tableLayoutPanel16.RowCount = 2;
            tableLayoutPanel16.RowStyles.Add(new RowStyle(SizeType.Percent, 45F));
            tableLayoutPanel16.RowStyles.Add(new RowStyle(SizeType.Percent, 55F));
            tableLayoutPanel16.Size = new Size(621, 106);
            tableLayoutPanel16.TabIndex = 142;
            // 
            // pnlExcluidos
            // 
            pnlExcluidos.Location = new Point(718, 292);
            pnlExcluidos.Name = "pnlExcluidos";
            pnlExcluidos.Size = new Size(404, 126);
            pnlExcluidos.TabIndex = 149;
            // 
            // FOfertaABM
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1279, 994);
            Controls.Add(pnlExcluidos);
            Controls.Add(lblNumeroProductoQuitados);
            Controls.Add(btnDevolverAOferta);
            Controls.Add(lblCantidadProductosQuitados);
            Controls.Add(btnCrearOferta);
            Controls.Add(btnCancelarYSalir);
            Controls.Add(btnLimpiarCampos);
            Controls.Add(tableLayoutPanel16);
            Controls.Add(tableLayoutPanel15);
            Controls.Add(tableLayoutPanel11);
            Controls.Add(tableLayoutPanel14);
            Controls.Add(dgvProductos);
            Controls.Add(tableLayoutPanel8);
            Controls.Add(tableLayoutPanel6);
            Controls.Add(tableLayoutPanel2);
            Controls.Add(tableLayoutPanel21);
            Controls.Add(label2);
            Controls.Add(label1);
            ForeColor = Color.FromArgb(31, 26, 43);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "FOfertaABM";
            Text = "FOfertaABM";
            WindowState = FormWindowState.Maximized;
            Load += FOfertaABM_Load;
            ((System.ComponentModel.ISupportInitialize)error).EndInit();
            tableLayoutPanel2.ResumeLayout(false);
            tableLayoutPanel2.PerformLayout();
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvProductos).EndInit();
            tableLayoutPanel21.ResumeLayout(false);
            tableLayoutPanel21.PerformLayout();
            tableLayoutPanel4.ResumeLayout(false);
            tableLayoutPanel4.PerformLayout();
            tableLayoutPanel5.ResumeLayout(false);
            tableLayoutPanel5.PerformLayout();
            tableLayoutPanel6.ResumeLayout(false);
            flowLayoutPanel1.ResumeLayout(false);
            flowLayoutPanel1.PerformLayout();
            tableLayoutPanel8.ResumeLayout(false);
            tableLayoutPanel11.ResumeLayout(false);
            tableLayoutPanel11.PerformLayout();
            tableLayoutPanel12.ResumeLayout(false);
            tableLayoutPanel12.PerformLayout();
            tableLayoutPanel13.ResumeLayout(false);
            tableLayoutPanel13.PerformLayout();
            tableLayoutPanel14.ResumeLayout(false);
            tableLayoutPanel14.PerformLayout();
            tableLayoutPanel19.ResumeLayout(false);
            tableLayoutPanel18.ResumeLayout(false);
            tableLayoutPanel18.PerformLayout();
            tableLayoutPanel15.ResumeLayout(false);
            tableLayoutPanel15.PerformLayout();
            tableLayoutPanel16.ResumeLayout(false);
            tableLayoutPanel16.PerformLayout();
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
        private Button btnDevolverAOferta;
        private Label label2;
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
        private TableLayoutPanel tableLayoutPanel4;
        private TableLayoutPanel tableLayoutPanel5;
        private CheckBox cbxCodigoAutomatico;
        private TableLayoutPanel tableLayoutPanel6;
        private Button btnCargarProducto;
        private FlowLayoutPanel flowLayoutPanel1;
        private TableLayoutPanel tableLayoutPanel8;
        private TableLayoutPanel tableLayoutPanel11;
        private TableLayoutPanel tableLayoutPanel12;
        private TableLayoutPanel tableLayoutPanel13;
        private TableLayoutPanel tableLayoutPanel14;
        private TableLayoutPanel tableLayoutPanel15;
        private TableLayoutPanel tableLayoutPanel16;
        private TableLayoutPanel tableLayoutPanel18;
        private TableLayoutPanel tableLayoutPanel19;
        private TableLayoutPanel tableLayoutPanel21;
        private TextBox textBox1;
        private Panel pnlExcluidos;
    }
}