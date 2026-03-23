namespace Presentacion.Core.Venta
{
    partial class FVentaLibre
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
            DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle5 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle6 = new DataGridViewCellStyle();
            tableLayoutPanel6 = new TableLayoutPanel();
            tableLayoutPanel5 = new TableLayoutPanel();
            tableLayoutPanel1 = new TableLayoutPanel();
            flowLayoutPanel5 = new FlowLayoutPanel();
            lblNro = new Label();
            lblNroVenta = new Label();
            tableLayoutPanel4 = new TableLayoutPanel();
            flowLayoutPanel14 = new FlowLayoutPanel();
            lblHoraMinutos = new Label();
            flowLayoutPanel4 = new FlowLayoutPanel();
            lblFechaHoy = new Label();
            flowLayoutPanel3 = new FlowLayoutPanel();
            lblDiaAbrev = new Label();
            tableLayoutPanel2 = new TableLayoutPanel();
            tableLayoutPanel3 = new TableLayoutPanel();
            flowLayoutPanel2 = new FlowLayoutPanel();
            lblVendedor = new Label();
            lblVendedorAsignado = new Label();
            dgvProductos = new DataGridView();
            txtDescripcion = new TextBox();
            lblDescripcion = new Label();
            lblmonto = new Label();
            txtPrecio = new TextBox();
            lblCantidad = new Label();
            textBox1 = new TextBox();
            btnAgregarProducto = new Button();
            lblCodigo = new Label();
            txtCodigoProducto = new TextBox();
            txtAreaDetallesVenta = new TextBox();
            lblDetalles = new Label();
            tableLayoutPanel11 = new TableLayoutPanel();
            flowLayoutPanel13 = new FlowLayoutPanel();
            txtTotal = new TextBox();
            lblTotal = new Label();
            flowLayoutPanel11 = new FlowLayoutPanel();
            txtDescuentoEfectivo = new TextBox();
            lblPorcentajeDescuento = new Label();
            flowLayoutPanel12 = new FlowLayoutPanel();
            txtSubtotal = new TextBox();
            lblSubtotal = new Label();
            tableLayoutPanel10 = new TableLayoutPanel();
            flowLayoutPanel10 = new FlowLayoutPanel();
            btnCancelar = new Button();
            btnConfirmarYFPago = new Button();
            btnLimpiar = new Button();
            tableLayoutPanel6.SuspendLayout();
            tableLayoutPanel5.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            flowLayoutPanel5.SuspendLayout();
            tableLayoutPanel4.SuspendLayout();
            flowLayoutPanel14.SuspendLayout();
            flowLayoutPanel4.SuspendLayout();
            flowLayoutPanel3.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            tableLayoutPanel3.SuspendLayout();
            flowLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvProductos).BeginInit();
            tableLayoutPanel11.SuspendLayout();
            flowLayoutPanel13.SuspendLayout();
            flowLayoutPanel11.SuspendLayout();
            flowLayoutPanel12.SuspendLayout();
            tableLayoutPanel10.SuspendLayout();
            flowLayoutPanel10.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayoutPanel6
            // 
            tableLayoutPanel6.ColumnCount = 1;
            tableLayoutPanel6.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel6.Controls.Add(tableLayoutPanel5, 0, 0);
            tableLayoutPanel6.Location = new Point(-1, 18);
            tableLayoutPanel6.Name = "tableLayoutPanel6";
            tableLayoutPanel6.RowCount = 1;
            tableLayoutPanel6.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel6.Size = new Size(1169, 100);
            tableLayoutPanel6.TabIndex = 2;
            // 
            // tableLayoutPanel5
            // 
            tableLayoutPanel5.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            tableLayoutPanel5.BackColor = SystemColors.Control;
            tableLayoutPanel5.ColumnCount = 1;
            tableLayoutPanel5.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
            tableLayoutPanel5.Controls.Add(tableLayoutPanel1, 0, 0);
            tableLayoutPanel5.Location = new Point(3, 3);
            tableLayoutPanel5.Name = "tableLayoutPanel5";
            tableLayoutPanel5.RowCount = 1;
            tableLayoutPanel5.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel5.Size = new Size(1163, 64);
            tableLayoutPanel5.TabIndex = 2;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            tableLayoutPanel1.BackColor = SystemColors.Control;
            tableLayoutPanel1.ColumnCount = 3;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel1.Controls.Add(flowLayoutPanel5, 2, 0);
            tableLayoutPanel1.Controls.Add(tableLayoutPanel4, 1, 0);
            tableLayoutPanel1.Controls.Add(tableLayoutPanel2, 0, 0);
            tableLayoutPanel1.Location = new Point(3, 3);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 1;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Size = new Size(1157, 58);
            tableLayoutPanel1.TabIndex = 43;
            // 
            // flowLayoutPanel5
            // 
            flowLayoutPanel5.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            flowLayoutPanel5.BackColor = SystemColors.Control;
            flowLayoutPanel5.Controls.Add(lblNro);
            flowLayoutPanel5.Controls.Add(lblNroVenta);
            flowLayoutPanel5.FlowDirection = FlowDirection.RightToLeft;
            flowLayoutPanel5.Location = new Point(797, 3);
            flowLayoutPanel5.Name = "flowLayoutPanel5";
            flowLayoutPanel5.Size = new Size(357, 29);
            flowLayoutPanel5.TabIndex = 43;
            // 
            // lblNro
            // 
            lblNro.AutoSize = true;
            lblNro.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblNro.Location = new Point(161, 0);
            lblNro.Name = "lblNro";
            lblNro.Size = new Size(193, 30);
            lblNro.TabIndex = 27;
            lblNro.Text = "000000000000000";
            // 
            // lblNroVenta
            // 
            lblNroVenta.AutoSize = true;
            lblNroVenta.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblNroVenta.Location = new Point(36, 0);
            lblNroVenta.Name = "lblNroVenta";
            lblNroVenta.Size = new Size(119, 30);
            lblNroVenta.TabIndex = 26;
            lblNroVenta.Text = "Nro Venta:";
            // 
            // tableLayoutPanel4
            // 
            tableLayoutPanel4.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            tableLayoutPanel4.BackColor = SystemColors.Control;
            tableLayoutPanel4.ColumnCount = 3;
            tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 23.0769234F));
            tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 45.3846169F));
            tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 31.5384617F));
            tableLayoutPanel4.Controls.Add(flowLayoutPanel14, 2, 0);
            tableLayoutPanel4.Controls.Add(flowLayoutPanel4, 1, 0);
            tableLayoutPanel4.Controls.Add(flowLayoutPanel3, 0, 0);
            tableLayoutPanel4.Location = new Point(401, 3);
            tableLayoutPanel4.Name = "tableLayoutPanel4";
            tableLayoutPanel4.RowCount = 1;
            tableLayoutPanel4.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel4.Size = new Size(390, 52);
            tableLayoutPanel4.TabIndex = 45;
            // 
            // flowLayoutPanel14
            // 
            flowLayoutPanel14.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            flowLayoutPanel14.Controls.Add(lblHoraMinutos);
            flowLayoutPanel14.Location = new Point(270, 3);
            flowLayoutPanel14.Name = "flowLayoutPanel14";
            flowLayoutPanel14.Size = new Size(117, 36);
            flowLayoutPanel14.TabIndex = 45;
            // 
            // lblHoraMinutos
            // 
            lblHoraMinutos.AutoSize = true;
            lblHoraMinutos.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblHoraMinutos.Location = new Point(3, 0);
            lblHoraMinutos.Name = "lblHoraMinutos";
            lblHoraMinutos.Size = new Size(97, 30);
            lblHoraMinutos.TabIndex = 17;
            lblHoraMinutos.Text = "00:00:00";
            // 
            // flowLayoutPanel4
            // 
            flowLayoutPanel4.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            flowLayoutPanel4.Controls.Add(lblFechaHoy);
            flowLayoutPanel4.Location = new Point(93, 3);
            flowLayoutPanel4.Name = "flowLayoutPanel4";
            flowLayoutPanel4.Size = new Size(171, 36);
            flowLayoutPanel4.TabIndex = 44;
            // 
            // lblFechaHoy
            // 
            lblFechaHoy.AutoSize = true;
            lblFechaHoy.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblFechaHoy.Location = new Point(3, 0);
            lblFechaHoy.Name = "lblFechaHoy";
            lblFechaHoy.Size = new Size(103, 30);
            lblFechaHoy.TabIndex = 1;
            lblFechaHoy.Text = "00/00/00";
            // 
            // flowLayoutPanel3
            // 
            flowLayoutPanel3.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            flowLayoutPanel3.Controls.Add(lblDiaAbrev);
            flowLayoutPanel3.FlowDirection = FlowDirection.RightToLeft;
            flowLayoutPanel3.Location = new Point(3, 3);
            flowLayoutPanel3.Name = "flowLayoutPanel3";
            flowLayoutPanel3.Size = new Size(84, 36);
            flowLayoutPanel3.TabIndex = 43;
            // 
            // lblDiaAbrev
            // 
            lblDiaAbrev.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblDiaAbrev.AutoSize = true;
            lblDiaAbrev.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblDiaAbrev.Location = new Point(30, 0);
            lblDiaAbrev.Name = "lblDiaAbrev";
            lblDiaAbrev.Size = new Size(51, 30);
            lblDiaAbrev.TabIndex = 2;
            lblDiaAbrev.Text = "Dia.";
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            tableLayoutPanel2.BackColor = SystemColors.Control;
            tableLayoutPanel2.ColumnCount = 1;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            tableLayoutPanel2.Controls.Add(tableLayoutPanel3, 0, 0);
            tableLayoutPanel2.Location = new Point(3, 3);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 1;
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel2.Size = new Size(392, 51);
            tableLayoutPanel2.TabIndex = 45;
            // 
            // tableLayoutPanel3
            // 
            tableLayoutPanel3.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            tableLayoutPanel3.ColumnCount = 1;
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 63.0374374F));
            tableLayoutPanel3.Controls.Add(flowLayoutPanel2, 0, 0);
            tableLayoutPanel3.Location = new Point(3, 3);
            tableLayoutPanel3.Name = "tableLayoutPanel3";
            tableLayoutPanel3.RowCount = 1;
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel3.Size = new Size(386, 45);
            tableLayoutPanel3.TabIndex = 45;
            // 
            // flowLayoutPanel2
            // 
            flowLayoutPanel2.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            flowLayoutPanel2.Controls.Add(lblVendedor);
            flowLayoutPanel2.Controls.Add(lblVendedorAsignado);
            flowLayoutPanel2.Location = new Point(3, 3);
            flowLayoutPanel2.Name = "flowLayoutPanel2";
            flowLayoutPanel2.Size = new Size(380, 39);
            flowLayoutPanel2.TabIndex = 44;
            // 
            // lblVendedor
            // 
            lblVendedor.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            lblVendedor.AutoSize = true;
            lblVendedor.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblVendedor.Location = new Point(3, 0);
            lblVendedor.Name = "lblVendedor";
            lblVendedor.Size = new Size(213, 30);
            lblVendedor.TabIndex = 2;
            lblVendedor.Text = "Vendedor Asignado:";
            // 
            // lblVendedorAsignado
            // 
            lblVendedorAsignado.AutoSize = true;
            lblVendedorAsignado.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblVendedorAsignado.Location = new Point(222, 0);
            lblVendedorAsignado.Name = "lblVendedorAsignado";
            lblVendedorAsignado.Size = new Size(108, 30);
            lblVendedorAsignado.TabIndex = 3;
            lblVendedorAsignado.Text = "Vendedor";
            // 
            // dgvProductos
            // 
            dgvProductos.AllowUserToAddRows = false;
            dgvProductos.AllowUserToDeleteRows = false;
            dgvProductos.AllowUserToResizeRows = false;
            dataGridViewCellStyle4.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dgvProductos.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle4;
            dgvProductos.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            dataGridViewCellStyle5.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = SystemColors.Control;
            dataGridViewCellStyle5.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            dataGridViewCellStyle5.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle5.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = DataGridViewTriState.True;
            dgvProductos.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            dgvProductos.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvProductos.EditMode = DataGridViewEditMode.EditProgrammatically;
            dgvProductos.Location = new Point(11, 252);
            dgvProductos.MultiSelect = false;
            dgvProductos.Name = "dgvProductos";
            dataGridViewCellStyle6.Font = new Font("Microsoft Sans Serif", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dgvProductos.RowsDefaultCellStyle = dataGridViewCellStyle6;
            dgvProductos.Size = new Size(1089, 185);
            dgvProductos.TabIndex = 16;
            // 
            // txtDescripcion
            // 
            txtDescripcion.Location = new Point(31, 153);
            txtDescripcion.Name = "txtDescripcion";
            txtDescripcion.Size = new Size(402, 23);
            txtDescripcion.TabIndex = 17;
            // 
            // lblDescripcion
            // 
            lblDescripcion.AutoSize = true;
            lblDescripcion.Location = new Point(31, 135);
            lblDescripcion.Name = "lblDescripcion";
            lblDescripcion.Size = new Size(69, 15);
            lblDescripcion.TabIndex = 18;
            lblDescripcion.Text = "Descripción";
            // 
            // lblmonto
            // 
            lblmonto.AutoSize = true;
            lblmonto.Location = new Point(535, 135);
            lblmonto.Name = "lblmonto";
            lblmonto.Size = new Size(40, 15);
            lblmonto.TabIndex = 20;
            lblmonto.Text = "Precio";
            // 
            // txtPrecio
            // 
            txtPrecio.Location = new Point(535, 153);
            txtPrecio.Name = "txtPrecio";
            txtPrecio.Size = new Size(143, 23);
            txtPrecio.TabIndex = 19;
            // 
            // lblCantidad
            // 
            lblCantidad.AutoSize = true;
            lblCantidad.Location = new Point(535, 189);
            lblCantidad.Name = "lblCantidad";
            lblCantidad.Size = new Size(55, 15);
            lblCantidad.TabIndex = 22;
            lblCantidad.Text = "Cantidad";
            // 
            // textBox1
            // 
            textBox1.Location = new Point(535, 207);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(143, 23);
            textBox1.TabIndex = 21;
            // 
            // btnAgregarProducto
            // 
            btnAgregarProducto.Location = new Point(828, 207);
            btnAgregarProducto.Name = "btnAgregarProducto";
            btnAgregarProducto.Size = new Size(75, 23);
            btnAgregarProducto.TabIndex = 23;
            btnAgregarProducto.Text = "Agregar";
            btnAgregarProducto.UseVisualStyleBackColor = true;
            // 
            // lblCodigo
            // 
            lblCodigo.AutoSize = true;
            lblCodigo.Location = new Point(31, 189);
            lblCodigo.Name = "lblCodigo";
            lblCodigo.Size = new Size(46, 15);
            lblCodigo.TabIndex = 25;
            lblCodigo.Text = "Código";
            // 
            // txtCodigoProducto
            // 
            txtCodigoProducto.Location = new Point(31, 207);
            txtCodigoProducto.Name = "txtCodigoProducto";
            txtCodigoProducto.Size = new Size(143, 23);
            txtCodigoProducto.TabIndex = 24;
            // 
            // txtAreaDetallesVenta
            // 
            txtAreaDetallesVenta.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtAreaDetallesVenta.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtAreaDetallesVenta.Location = new Point(8, 484);
            txtAreaDetallesVenta.Multiline = true;
            txtAreaDetallesVenta.Name = "txtAreaDetallesVenta";
            txtAreaDetallesVenta.Size = new Size(567, 116);
            txtAreaDetallesVenta.TabIndex = 48;
            // 
            // lblDetalles
            // 
            lblDetalles.AutoSize = true;
            lblDetalles.Location = new Point(14, 466);
            lblDetalles.Name = "lblDetalles";
            lblDetalles.Size = new Size(69, 15);
            lblDetalles.TabIndex = 49;
            lblDetalles.Text = "Descripción";
            // 
            // tableLayoutPanel11
            // 
            tableLayoutPanel11.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            tableLayoutPanel11.BackColor = SystemColors.ControlDark;
            tableLayoutPanel11.ColumnCount = 1;
            tableLayoutPanel11.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel11.Controls.Add(flowLayoutPanel13, 0, 2);
            tableLayoutPanel11.Controls.Add(flowLayoutPanel11, 0, 0);
            tableLayoutPanel11.Controls.Add(flowLayoutPanel12, 0, 1);
            tableLayoutPanel11.Location = new Point(704, 477);
            tableLayoutPanel11.Name = "tableLayoutPanel11";
            tableLayoutPanel11.RowCount = 3;
            tableLayoutPanel11.RowStyles.Add(new RowStyle(SizeType.Percent, 33.3333321F));
            tableLayoutPanel11.RowStyles.Add(new RowStyle(SizeType.Percent, 33.3333321F));
            tableLayoutPanel11.RowStyles.Add(new RowStyle(SizeType.Percent, 33.3333321F));
            tableLayoutPanel11.Size = new Size(396, 123);
            tableLayoutPanel11.TabIndex = 50;
            // 
            // flowLayoutPanel13
            // 
            flowLayoutPanel13.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            flowLayoutPanel13.Controls.Add(txtTotal);
            flowLayoutPanel13.Controls.Add(lblTotal);
            flowLayoutPanel13.FlowDirection = FlowDirection.RightToLeft;
            flowLayoutPanel13.Location = new Point(3, 85);
            flowLayoutPanel13.Name = "flowLayoutPanel13";
            flowLayoutPanel13.Size = new Size(390, 35);
            flowLayoutPanel13.TabIndex = 42;
            // 
            // txtTotal
            // 
            txtTotal.BorderStyle = BorderStyle.FixedSingle;
            txtTotal.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            txtTotal.Location = new Point(226, 3);
            txtTotal.Name = "txtTotal";
            txtTotal.ReadOnly = true;
            txtTotal.Size = new Size(161, 33);
            txtTotal.TabIndex = 35;
            // 
            // lblTotal
            // 
            lblTotal.AutoSize = true;
            lblTotal.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTotal.Location = new Point(160, 0);
            lblTotal.Name = "lblTotal";
            lblTotal.Size = new Size(60, 25);
            lblTotal.TabIndex = 18;
            lblTotal.Text = "Total:";
            // 
            // flowLayoutPanel11
            // 
            flowLayoutPanel11.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            flowLayoutPanel11.Controls.Add(txtDescuentoEfectivo);
            flowLayoutPanel11.Controls.Add(lblPorcentajeDescuento);
            flowLayoutPanel11.FlowDirection = FlowDirection.RightToLeft;
            flowLayoutPanel11.Location = new Point(3, 3);
            flowLayoutPanel11.Name = "flowLayoutPanel11";
            flowLayoutPanel11.Size = new Size(390, 35);
            flowLayoutPanel11.TabIndex = 0;
            // 
            // txtDescuentoEfectivo
            // 
            txtDescuentoEfectivo.BorderStyle = BorderStyle.FixedSingle;
            txtDescuentoEfectivo.Enabled = false;
            txtDescuentoEfectivo.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            txtDescuentoEfectivo.Location = new Point(226, 3);
            txtDescuentoEfectivo.Name = "txtDescuentoEfectivo";
            txtDescuentoEfectivo.Size = new Size(161, 33);
            txtDescuentoEfectivo.TabIndex = 34;
            // 
            // lblPorcentajeDescuento
            // 
            lblPorcentajeDescuento.AutoSize = true;
            lblPorcentajeDescuento.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblPorcentajeDescuento.Location = new Point(192, 0);
            lblPorcentajeDescuento.Name = "lblPorcentajeDescuento";
            lblPorcentajeDescuento.Size = new Size(28, 25);
            lblPorcentajeDescuento.TabIndex = 40;
            lblPorcentajeDescuento.Text = "%";
            // 
            // flowLayoutPanel12
            // 
            flowLayoutPanel12.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            flowLayoutPanel12.Controls.Add(txtSubtotal);
            flowLayoutPanel12.Controls.Add(lblSubtotal);
            flowLayoutPanel12.FlowDirection = FlowDirection.RightToLeft;
            flowLayoutPanel12.Location = new Point(3, 44);
            flowLayoutPanel12.Name = "flowLayoutPanel12";
            flowLayoutPanel12.Size = new Size(390, 35);
            flowLayoutPanel12.TabIndex = 1;
            // 
            // txtSubtotal
            // 
            txtSubtotal.BorderStyle = BorderStyle.FixedSingle;
            txtSubtotal.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            txtSubtotal.Location = new Point(226, 3);
            txtSubtotal.Name = "txtSubtotal";
            txtSubtotal.ReadOnly = true;
            txtSubtotal.Size = new Size(161, 33);
            txtSubtotal.TabIndex = 33;
            // 
            // lblSubtotal
            // 
            lblSubtotal.AutoSize = true;
            lblSubtotal.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblSubtotal.Location = new Point(81, 0);
            lblSubtotal.Name = "lblSubtotal";
            lblSubtotal.Size = new Size(139, 25);
            lblSubtotal.TabIndex = 19;
            lblSubtotal.Text = "Sin Descuento";
            // 
            // tableLayoutPanel10
            // 
            tableLayoutPanel10.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tableLayoutPanel10.ColumnCount = 3;
            tableLayoutPanel10.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 26.666666F));
            tableLayoutPanel10.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
            tableLayoutPanel10.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40F));
            tableLayoutPanel10.Controls.Add(flowLayoutPanel10, 2, 0);
            tableLayoutPanel10.Controls.Add(btnLimpiar, 0, 0);
            tableLayoutPanel10.Location = new Point(5, 635);
            tableLayoutPanel10.Name = "tableLayoutPanel10";
            tableLayoutPanel10.RowCount = 1;
            tableLayoutPanel10.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel10.Size = new Size(1095, 59);
            tableLayoutPanel10.TabIndex = 51;
            // 
            // flowLayoutPanel10
            // 
            flowLayoutPanel10.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            flowLayoutPanel10.Controls.Add(btnCancelar);
            flowLayoutPanel10.Controls.Add(btnConfirmarYFPago);
            flowLayoutPanel10.FlowDirection = FlowDirection.RightToLeft;
            flowLayoutPanel10.Location = new Point(660, 3);
            flowLayoutPanel10.Name = "flowLayoutPanel10";
            flowLayoutPanel10.Size = new Size(432, 53);
            flowLayoutPanel10.TabIndex = 47;
            // 
            // btnCancelar
            // 
            btnCancelar.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnCancelar.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnCancelar.Location = new Point(269, 3);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new Size(160, 44);
            btnCancelar.TabIndex = 32;
            btnCancelar.Text = "Cancelar";
            btnCancelar.UseVisualStyleBackColor = true;
            // 
            // btnConfirmarYFPago
            // 
            btnConfirmarYFPago.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnConfirmarYFPago.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnConfirmarYFPago.Location = new Point(103, 3);
            btnConfirmarYFPago.Name = "btnConfirmarYFPago";
            btnConfirmarYFPago.Size = new Size(160, 44);
            btnConfirmarYFPago.TabIndex = 31;
            btnConfirmarYFPago.Text = "Pagar";
            btnConfirmarYFPago.UseVisualStyleBackColor = true;
            // 
            // btnLimpiar
            // 
            btnLimpiar.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnLimpiar.Location = new Point(3, 3);
            btnLimpiar.Name = "btnLimpiar";
            btnLimpiar.Size = new Size(161, 45);
            btnLimpiar.TabIndex = 7;
            btnLimpiar.Text = "Limpiar";
            btnLimpiar.UseVisualStyleBackColor = true;
            // 
            // FVentaLibre
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1170, 706);
            Controls.Add(tableLayoutPanel10);
            Controls.Add(tableLayoutPanel11);
            Controls.Add(lblDetalles);
            Controls.Add(txtAreaDetallesVenta);
            Controls.Add(lblCodigo);
            Controls.Add(txtCodigoProducto);
            Controls.Add(btnAgregarProducto);
            Controls.Add(lblCantidad);
            Controls.Add(textBox1);
            Controls.Add(lblmonto);
            Controls.Add(txtPrecio);
            Controls.Add(lblDescripcion);
            Controls.Add(txtDescripcion);
            Controls.Add(dgvProductos);
            Controls.Add(tableLayoutPanel6);
            Name = "FVentaLibre";
            Text = "FVentaLibre";
            tableLayoutPanel6.ResumeLayout(false);
            tableLayoutPanel5.ResumeLayout(false);
            tableLayoutPanel1.ResumeLayout(false);
            flowLayoutPanel5.ResumeLayout(false);
            flowLayoutPanel5.PerformLayout();
            tableLayoutPanel4.ResumeLayout(false);
            flowLayoutPanel14.ResumeLayout(false);
            flowLayoutPanel14.PerformLayout();
            flowLayoutPanel4.ResumeLayout(false);
            flowLayoutPanel4.PerformLayout();
            flowLayoutPanel3.ResumeLayout(false);
            flowLayoutPanel3.PerformLayout();
            tableLayoutPanel2.ResumeLayout(false);
            tableLayoutPanel3.ResumeLayout(false);
            flowLayoutPanel2.ResumeLayout(false);
            flowLayoutPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvProductos).EndInit();
            tableLayoutPanel11.ResumeLayout(false);
            flowLayoutPanel13.ResumeLayout(false);
            flowLayoutPanel13.PerformLayout();
            flowLayoutPanel11.ResumeLayout(false);
            flowLayoutPanel11.PerformLayout();
            flowLayoutPanel12.ResumeLayout(false);
            flowLayoutPanel12.PerformLayout();
            tableLayoutPanel10.ResumeLayout(false);
            flowLayoutPanel10.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TableLayoutPanel tableLayoutPanel6;
        private TableLayoutPanel tableLayoutPanel5;
        private TableLayoutPanel tableLayoutPanel1;
        private FlowLayoutPanel flowLayoutPanel5;
        private Label lblNro;
        private Label lblNroVenta;
        private TableLayoutPanel tableLayoutPanel4;
        private FlowLayoutPanel flowLayoutPanel14;
        private Label lblHoraMinutos;
        private FlowLayoutPanel flowLayoutPanel4;
        private Label lblFechaHoy;
        private FlowLayoutPanel flowLayoutPanel3;
        private Label lblDiaAbrev;
        private TableLayoutPanel tableLayoutPanel2;
        private TableLayoutPanel tableLayoutPanel3;
        private FlowLayoutPanel flowLayoutPanel2;
        private Label lblVendedor;
        private Label lblVendedorAsignado;
        private DataGridView dgvProductos;
        private TextBox txtDescripcion;
        private Label lblDescripcion;
        private Label lblmonto;
        private TextBox txtPrecio;
        private Label lblCantidad;
        private TextBox textBox1;
        private Button btnAgregarProducto;
        private Label lblCodigo;
        private TextBox txtCodigoProducto;
        private TextBox txtAreaDetallesVenta;
        private Label lblDetalles;
        private TableLayoutPanel tableLayoutPanel11;
        private FlowLayoutPanel flowLayoutPanel13;
        private TextBox txtTotal;
        private Label lblTotal;
        private FlowLayoutPanel flowLayoutPanel11;
        private TextBox txtDescuentoEfectivo;
        private Label lblPorcentajeDescuento;
        private FlowLayoutPanel flowLayoutPanel12;
        private TextBox txtSubtotal;
        private Label lblSubtotal;
        private TableLayoutPanel tableLayoutPanel10;
        private FlowLayoutPanel flowLayoutPanel10;
        private Button btnCancelar;
        private Button btnConfirmarYFPago;
        private Button btnLimpiar;
    }
}