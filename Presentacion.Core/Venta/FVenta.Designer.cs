namespace Presentacion.Core.Venta
{
    partial class FVenta
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
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            lblFechaHoy = new Label();
            btnLimpiar = new Button();
            txtCliente = new TextBox();
            btnCargarCliente = new Button();
            cbxConsumidorFinal = new CheckBox();
            btnCargarProducto = new Button();
            dgvProductos = new DataGridView();
            lblHoraMinutos = new Label();
            lblTotal = new Label();
            lblSubtotal = new Label();
            lblNroVenta = new Label();
            lblNro = new Label();
            btnCambiarVendedor = new Button();
            btnConfirmarYFPago = new Button();
            btnCancelar = new Button();
            txtSubtotal = new TextBox();
            txtDescuentoEfectivo = new TextBox();
            txtTotal = new TextBox();
            cbxIncluirCtaCte = new CheckBox();
            lblPorcentajeDescuento = new Label();
            cbxDescEfectivo = new CheckBox();
            tableLayoutPanel5 = new TableLayoutPanel();
            tableLayoutPanel1 = new TableLayoutPanel();
            flowLayoutPanel5 = new FlowLayoutPanel();
            tableLayoutPanel4 = new TableLayoutPanel();
            flowLayoutPanel14 = new FlowLayoutPanel();
            flowLayoutPanel4 = new FlowLayoutPanel();
            flowLayoutPanel3 = new FlowLayoutPanel();
            lblDiaAbrev = new Label();
            tableLayoutPanel2 = new TableLayoutPanel();
            tableLayoutPanel3 = new TableLayoutPanel();
            flowLayoutPanel2 = new FlowLayoutPanel();
            lblVendedor = new Label();
            lblVendedorAsignado = new Label();
            btnCargarOferta = new Button();
            tableLayoutPanel6 = new TableLayoutPanel();
            flowLayoutPanel15 = new FlowLayoutPanel();
            tableLayoutPanel7 = new TableLayoutPanel();
            tableLayoutPanel8 = new TableLayoutPanel();
            tableLayoutPanel10 = new TableLayoutPanel();
            flowLayoutPanel10 = new FlowLayoutPanel();
            tableLayoutPanel9 = new TableLayoutPanel();
            flowLayoutPanel8 = new FlowLayoutPanel();
            tableLayoutPanel11 = new TableLayoutPanel();
            flowLayoutPanel13 = new FlowLayoutPanel();
            flowLayoutPanel11 = new FlowLayoutPanel();
            flowLayoutPanel12 = new FlowLayoutPanel();
            flowLayoutPanel7 = new FlowLayoutPanel();
            flowLayoutPanel6 = new FlowLayoutPanel();
            txtAreaDetallesVenta = new TextBox();
            ((System.ComponentModel.ISupportInitialize)error).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvProductos).BeginInit();
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
            tableLayoutPanel6.SuspendLayout();
            flowLayoutPanel15.SuspendLayout();
            tableLayoutPanel7.SuspendLayout();
            tableLayoutPanel8.SuspendLayout();
            tableLayoutPanel10.SuspendLayout();
            flowLayoutPanel10.SuspendLayout();
            tableLayoutPanel9.SuspendLayout();
            flowLayoutPanel8.SuspendLayout();
            tableLayoutPanel11.SuspendLayout();
            flowLayoutPanel13.SuspendLayout();
            flowLayoutPanel11.SuspendLayout();
            flowLayoutPanel12.SuspendLayout();
            flowLayoutPanel7.SuspendLayout();
            flowLayoutPanel6.SuspendLayout();
            SuspendLayout();
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
            // btnLimpiar
            // 
            btnLimpiar.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnLimpiar.Location = new Point(3, 3);
            btnLimpiar.Name = "btnLimpiar";
            btnLimpiar.Size = new Size(161, 45);
            btnLimpiar.TabIndex = 7;
            btnLimpiar.Text = "Limpiar";
            btnLimpiar.UseVisualStyleBackColor = true;
            btnLimpiar.Click += btnLimpiar_Click;
            // 
            // txtCliente
            // 
            txtCliente.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            txtCliente.Location = new Point(3, 39);
            txtCliente.Name = "txtCliente";
            txtCliente.ReadOnly = true;
            txtCliente.Size = new Size(289, 27);
            txtCliente.TabIndex = 8;
            // 
            // btnCargarCliente
            // 
            btnCargarCliente.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnCargarCliente.Location = new Point(3, 3);
            btnCargarCliente.Name = "btnCargarCliente";
            btnCargarCliente.Size = new Size(161, 30);
            btnCargarCliente.TabIndex = 9;
            btnCargarCliente.Text = "Cargar Cliente";
            btnCargarCliente.UseVisualStyleBackColor = true;
            btnCargarCliente.Click += btnCargarCliente_Click;
            // 
            // cbxConsumidorFinal
            // 
            cbxConsumidorFinal.AutoSize = true;
            cbxConsumidorFinal.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            cbxConsumidorFinal.Location = new Point(3, 72);
            cbxConsumidorFinal.Name = "cbxConsumidorFinal";
            cbxConsumidorFinal.Size = new Size(205, 29);
            cbxConsumidorFinal.TabIndex = 10;
            cbxConsumidorFinal.Text = "Es consumidor final";
            cbxConsumidorFinal.UseVisualStyleBackColor = true;
            cbxConsumidorFinal.CheckedChanged += cbxConsumidorFinal_CheckedChanged;
            // 
            // btnCargarProducto
            // 
            btnCargarProducto.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnCargarProducto.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnCargarProducto.Location = new Point(115, 3);
            btnCargarProducto.Name = "btnCargarProducto";
            btnCargarProducto.Size = new Size(211, 41);
            btnCargarProducto.TabIndex = 12;
            btnCargarProducto.Text = "Cargar Producto";
            btnCargarProducto.UseVisualStyleBackColor = true;
            btnCargarProducto.Click += btnCargarProducto_Click;
            // 
            // dgvProductos
            // 
            dgvProductos.AllowUserToAddRows = false;
            dgvProductos.AllowUserToDeleteRows = false;
            dgvProductos.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dgvProductos.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            dgvProductos.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = SystemColors.Control;
            dataGridViewCellStyle2.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            dataGridViewCellStyle2.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
            dgvProductos.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            dgvProductos.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvProductos.EditMode = DataGridViewEditMode.EditProgrammatically;
            dgvProductos.Location = new Point(3, 64);
            dgvProductos.MultiSelect = false;
            dgvProductos.Name = "dgvProductos";
            dataGridViewCellStyle3.Font = new Font("Microsoft Sans Serif", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dgvProductos.RowsDefaultCellStyle = dataGridViewCellStyle3;
            dgvProductos.Size = new Size(1089, 318);
            dgvProductos.TabIndex = 15;
            dgvProductos.CellClick += dgvProductos_CellClick;
            dgvProductos.CellContentClick += dgvProductos_CellContentClick;
            dgvProductos.CellFormatting += dgvProductos_CellFormatting;
            dgvProductos.RowEnter += dgvProductos_RowEnter;
            dgvProductos.SelectionChanged += dgvProductos_SelectionChanged;
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
            // lblTotal
            // 
            lblTotal.AutoSize = true;
            lblTotal.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTotal.Location = new Point(79, 0);
            lblTotal.Name = "lblTotal";
            lblTotal.Size = new Size(60, 25);
            lblTotal.TabIndex = 18;
            lblTotal.Text = "Total:";
            // 
            // lblSubtotal
            // 
            lblSubtotal.AutoSize = true;
            lblSubtotal.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblSubtotal.Location = new Point(36, 0);
            lblSubtotal.Name = "lblSubtotal";
            lblSubtotal.Size = new Size(103, 25);
            lblSubtotal.TabIndex = 19;
            lblSubtotal.Text = "Sub-Total:";
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
            // btnCambiarVendedor
            // 
            btnCambiarVendedor.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnCambiarVendedor.Location = new Point(3, 3);
            btnCambiarVendedor.Name = "btnCambiarVendedor";
            btnCambiarVendedor.Size = new Size(209, 39);
            btnCambiarVendedor.TabIndex = 29;
            btnCambiarVendedor.Text = "Cambiar Vendedor";
            btnCambiarVendedor.UseVisualStyleBackColor = true;
            btnCambiarVendedor.Click += btnCambiarVendedor_Click;
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
            btnConfirmarYFPago.Click += btnConfirmarYFPago_Click;
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
            btnCancelar.Click += btnCancelar_Click;
            // 
            // txtSubtotal
            // 
            txtSubtotal.BorderStyle = BorderStyle.FixedSingle;
            txtSubtotal.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            txtSubtotal.Location = new Point(145, 3);
            txtSubtotal.Name = "txtSubtotal";
            txtSubtotal.ReadOnly = true;
            txtSubtotal.Size = new Size(161, 33);
            txtSubtotal.TabIndex = 33;
            // 
            // txtDescuentoEfectivo
            // 
            txtDescuentoEfectivo.BorderStyle = BorderStyle.FixedSingle;
            txtDescuentoEfectivo.Enabled = false;
            txtDescuentoEfectivo.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            txtDescuentoEfectivo.Location = new Point(145, 3);
            txtDescuentoEfectivo.Name = "txtDescuentoEfectivo";
            txtDescuentoEfectivo.Size = new Size(161, 33);
            txtDescuentoEfectivo.TabIndex = 34;
            txtDescuentoEfectivo.TextChanged += txtDescuentoEfectivo_TextChanged;
            // 
            // txtTotal
            // 
            txtTotal.BorderStyle = BorderStyle.FixedSingle;
            txtTotal.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            txtTotal.Location = new Point(145, 3);
            txtTotal.Name = "txtTotal";
            txtTotal.ReadOnly = true;
            txtTotal.Size = new Size(161, 33);
            txtTotal.TabIndex = 35;
            // 
            // cbxIncluirCtaCte
            // 
            cbxIncluirCtaCte.AutoSize = true;
            cbxIncluirCtaCte.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            cbxIncluirCtaCte.Location = new Point(148, 3);
            cbxIncluirCtaCte.Name = "cbxIncluirCtaCte";
            cbxIncluirCtaCte.Size = new Size(170, 29);
            cbxIncluirCtaCte.TabIndex = 39;
            cbxIncluirCtaCte.Text = "Permitir Cta Cte";
            cbxIncluirCtaCte.UseVisualStyleBackColor = true;
            cbxIncluirCtaCte.CheckedChanged += cbxIncluirCtaCte_CheckedChanged;
            // 
            // lblPorcentajeDescuento
            // 
            lblPorcentajeDescuento.AutoSize = true;
            lblPorcentajeDescuento.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblPorcentajeDescuento.Location = new Point(111, 0);
            lblPorcentajeDescuento.Name = "lblPorcentajeDescuento";
            lblPorcentajeDescuento.Size = new Size(28, 25);
            lblPorcentajeDescuento.TabIndex = 40;
            lblPorcentajeDescuento.Text = "%";
            // 
            // cbxDescEfectivo
            // 
            cbxDescEfectivo.AutoSize = true;
            cbxDescEfectivo.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            cbxDescEfectivo.Location = new Point(81, 38);
            cbxDescEfectivo.Name = "cbxDescEfectivo";
            cbxDescEfectivo.Size = new Size(237, 29);
            cbxDescEfectivo.TabIndex = 41;
            cbxDescEfectivo.Text = "Descuento por Efectivo";
            cbxDescEfectivo.UseVisualStyleBackColor = true;
            cbxDescEfectivo.CheckedChanged += cbxDescEfectivo_CheckedChanged;
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
            tableLayoutPanel5.Size = new Size(1095, 64);
            tableLayoutPanel5.TabIndex = 0;
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
            tableLayoutPanel1.Size = new Size(1089, 58);
            tableLayoutPanel1.TabIndex = 43;
            // 
            // flowLayoutPanel5
            // 
            flowLayoutPanel5.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            flowLayoutPanel5.BackColor = SystemColors.Control;
            flowLayoutPanel5.Controls.Add(lblNro);
            flowLayoutPanel5.Controls.Add(lblNroVenta);
            flowLayoutPanel5.FlowDirection = FlowDirection.RightToLeft;
            flowLayoutPanel5.Location = new Point(729, 3);
            flowLayoutPanel5.Name = "flowLayoutPanel5";
            flowLayoutPanel5.Size = new Size(357, 29);
            flowLayoutPanel5.TabIndex = 43;
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
            tableLayoutPanel4.Location = new Point(333, 3);
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
            // flowLayoutPanel4
            // 
            flowLayoutPanel4.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            flowLayoutPanel4.Controls.Add(lblFechaHoy);
            flowLayoutPanel4.Location = new Point(93, 3);
            flowLayoutPanel4.Name = "flowLayoutPanel4";
            flowLayoutPanel4.Size = new Size(171, 36);
            flowLayoutPanel4.TabIndex = 44;
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
            tableLayoutPanel2.Size = new Size(324, 51);
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
            tableLayoutPanel3.Size = new Size(318, 45);
            tableLayoutPanel3.TabIndex = 45;
            // 
            // flowLayoutPanel2
            // 
            flowLayoutPanel2.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            flowLayoutPanel2.Controls.Add(lblVendedor);
            flowLayoutPanel2.Controls.Add(lblVendedorAsignado);
            flowLayoutPanel2.Location = new Point(3, 3);
            flowLayoutPanel2.Name = "flowLayoutPanel2";
            flowLayoutPanel2.Size = new Size(312, 39);
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
            lblVendedorAsignado.Location = new Point(3, 30);
            lblVendedorAsignado.Name = "lblVendedorAsignado";
            lblVendedorAsignado.Size = new Size(108, 30);
            lblVendedorAsignado.TabIndex = 3;
            lblVendedorAsignado.Text = "Vendedor";
            // 
            // btnCargarOferta
            // 
            btnCargarOferta.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnCargarOferta.Location = new Point(332, 3);
            btnCargarOferta.Name = "btnCargarOferta";
            btnCargarOferta.Size = new Size(206, 41);
            btnCargarOferta.TabIndex = 42;
            btnCargarOferta.Text = "Cargar Oferta";
            btnCargarOferta.UseVisualStyleBackColor = true;
            btnCargarOferta.Click += btnCargarOferta_Click;
            // 
            // tableLayoutPanel6
            // 
            tableLayoutPanel6.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            tableLayoutPanel6.ColumnCount = 7;
            tableLayoutPanel6.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tableLayoutPanel6.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 5F));
            tableLayoutPanel6.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 5F));
            tableLayoutPanel6.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 5F));
            tableLayoutPanel6.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 5F));
            tableLayoutPanel6.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 5F));
            tableLayoutPanel6.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel6.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            tableLayoutPanel6.Controls.Add(btnCambiarVendedor, 0, 0);
            tableLayoutPanel6.Controls.Add(flowLayoutPanel15, 6, 0);
            tableLayoutPanel6.Location = new Point(3, 3);
            tableLayoutPanel6.Name = "tableLayoutPanel6";
            tableLayoutPanel6.RowCount = 1;
            tableLayoutPanel6.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel6.Size = new Size(1089, 53);
            tableLayoutPanel6.TabIndex = 43;
            // 
            // flowLayoutPanel15
            // 
            flowLayoutPanel15.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            flowLayoutPanel15.Controls.Add(btnCargarOferta);
            flowLayoutPanel15.Controls.Add(btnCargarProducto);
            flowLayoutPanel15.FlowDirection = FlowDirection.RightToLeft;
            flowLayoutPanel15.Location = new Point(545, 3);
            flowLayoutPanel15.Name = "flowLayoutPanel15";
            flowLayoutPanel15.Size = new Size(541, 47);
            flowLayoutPanel15.TabIndex = 43;
            // 
            // tableLayoutPanel7
            // 
            tableLayoutPanel7.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            tableLayoutPanel7.BackColor = SystemColors.Control;
            tableLayoutPanel7.ColumnCount = 1;
            tableLayoutPanel7.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel7.Controls.Add(dgvProductos, 0, 1);
            tableLayoutPanel7.Controls.Add(tableLayoutPanel6, 0, 0);
            tableLayoutPanel7.Location = new Point(3, 73);
            tableLayoutPanel7.Name = "tableLayoutPanel7";
            tableLayoutPanel7.RowCount = 2;
            tableLayoutPanel7.RowStyles.Add(new RowStyle(SizeType.Percent, 15.89571F));
            tableLayoutPanel7.RowStyles.Add(new RowStyle(SizeType.Percent, 84.1042938F));
            tableLayoutPanel7.Size = new Size(1095, 385);
            tableLayoutPanel7.TabIndex = 44;
            // 
            // tableLayoutPanel8
            // 
            tableLayoutPanel8.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tableLayoutPanel8.BackColor = SystemColors.Control;
            tableLayoutPanel8.ColumnCount = 1;
            tableLayoutPanel8.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel8.Controls.Add(tableLayoutPanel10, 0, 4);
            tableLayoutPanel8.Controls.Add(tableLayoutPanel9, 0, 2);
            tableLayoutPanel8.Controls.Add(tableLayoutPanel5, 0, 0);
            tableLayoutPanel8.Controls.Add(tableLayoutPanel7, 0, 1);
            tableLayoutPanel8.Controls.Add(txtAreaDetallesVenta, 0, 3);
            tableLayoutPanel8.Location = new Point(12, 19);
            tableLayoutPanel8.Name = "tableLayoutPanel8";
            tableLayoutPanel8.RowCount = 5;
            tableLayoutPanel8.RowStyles.Add(new RowStyle(SizeType.Percent, 9F));
            tableLayoutPanel8.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel8.RowStyles.Add(new RowStyle(SizeType.Percent, 17.789072F));
            tableLayoutPanel8.RowStyles.Add(new RowStyle(SizeType.Percent, 15.6289711F));
            tableLayoutPanel8.RowStyles.Add(new RowStyle(SizeType.Percent, 8.005082F));
            tableLayoutPanel8.Size = new Size(1101, 787);
            tableLayoutPanel8.TabIndex = 45;
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
            tableLayoutPanel10.Location = new Point(3, 725);
            tableLayoutPanel10.Name = "tableLayoutPanel10";
            tableLayoutPanel10.RowCount = 1;
            tableLayoutPanel10.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel10.Size = new Size(1095, 59);
            tableLayoutPanel10.TabIndex = 46;
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
            // tableLayoutPanel9
            // 
            tableLayoutPanel9.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            tableLayoutPanel9.BackColor = SystemColors.Control;
            tableLayoutPanel9.ColumnCount = 3;
            tableLayoutPanel9.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40.24144F));
            tableLayoutPanel9.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 29.87928F));
            tableLayoutPanel9.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 29.8792763F));
            tableLayoutPanel9.Controls.Add(flowLayoutPanel8, 2, 0);
            tableLayoutPanel9.Controls.Add(flowLayoutPanel7, 1, 0);
            tableLayoutPanel9.Controls.Add(flowLayoutPanel6, 0, 0);
            tableLayoutPanel9.Location = new Point(3, 464);
            tableLayoutPanel9.Name = "tableLayoutPanel9";
            tableLayoutPanel9.RowCount = 1;
            tableLayoutPanel9.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel9.Size = new Size(1095, 133);
            tableLayoutPanel9.TabIndex = 46;
            // 
            // flowLayoutPanel8
            // 
            flowLayoutPanel8.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            flowLayoutPanel8.BackColor = SystemColors.Control;
            flowLayoutPanel8.Controls.Add(tableLayoutPanel11);
            flowLayoutPanel8.FlowDirection = FlowDirection.RightToLeft;
            flowLayoutPanel8.Location = new Point(770, 3);
            flowLayoutPanel8.Name = "flowLayoutPanel8";
            flowLayoutPanel8.Size = new Size(322, 127);
            flowLayoutPanel8.TabIndex = 47;
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
            tableLayoutPanel11.Location = new Point(4, 3);
            tableLayoutPanel11.Name = "tableLayoutPanel11";
            tableLayoutPanel11.RowCount = 3;
            tableLayoutPanel11.RowStyles.Add(new RowStyle(SizeType.Percent, 33.3333321F));
            tableLayoutPanel11.RowStyles.Add(new RowStyle(SizeType.Percent, 33.3333321F));
            tableLayoutPanel11.RowStyles.Add(new RowStyle(SizeType.Percent, 33.3333321F));
            tableLayoutPanel11.Size = new Size(315, 123);
            tableLayoutPanel11.TabIndex = 41;
            // 
            // flowLayoutPanel13
            // 
            flowLayoutPanel13.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            flowLayoutPanel13.Controls.Add(txtTotal);
            flowLayoutPanel13.Controls.Add(lblTotal);
            flowLayoutPanel13.FlowDirection = FlowDirection.RightToLeft;
            flowLayoutPanel13.Location = new Point(3, 85);
            flowLayoutPanel13.Name = "flowLayoutPanel13";
            flowLayoutPanel13.Size = new Size(309, 35);
            flowLayoutPanel13.TabIndex = 42;
            // 
            // flowLayoutPanel11
            // 
            flowLayoutPanel11.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            flowLayoutPanel11.Controls.Add(txtDescuentoEfectivo);
            flowLayoutPanel11.Controls.Add(lblPorcentajeDescuento);
            flowLayoutPanel11.FlowDirection = FlowDirection.RightToLeft;
            flowLayoutPanel11.Location = new Point(3, 3);
            flowLayoutPanel11.Name = "flowLayoutPanel11";
            flowLayoutPanel11.Size = new Size(309, 35);
            flowLayoutPanel11.TabIndex = 0;
            // 
            // flowLayoutPanel12
            // 
            flowLayoutPanel12.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            flowLayoutPanel12.Controls.Add(txtSubtotal);
            flowLayoutPanel12.Controls.Add(lblSubtotal);
            flowLayoutPanel12.FlowDirection = FlowDirection.RightToLeft;
            flowLayoutPanel12.Location = new Point(3, 44);
            flowLayoutPanel12.Name = "flowLayoutPanel12";
            flowLayoutPanel12.Size = new Size(309, 35);
            flowLayoutPanel12.TabIndex = 1;
            // 
            // flowLayoutPanel7
            // 
            flowLayoutPanel7.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            flowLayoutPanel7.BackColor = SystemColors.Control;
            flowLayoutPanel7.Controls.Add(cbxIncluirCtaCte);
            flowLayoutPanel7.Controls.Add(cbxDescEfectivo);
            flowLayoutPanel7.FlowDirection = FlowDirection.RightToLeft;
            flowLayoutPanel7.Location = new Point(443, 3);
            flowLayoutPanel7.Name = "flowLayoutPanel7";
            flowLayoutPanel7.Size = new Size(321, 127);
            flowLayoutPanel7.TabIndex = 47;
            // 
            // flowLayoutPanel6
            // 
            flowLayoutPanel6.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            flowLayoutPanel6.BackColor = SystemColors.Control;
            flowLayoutPanel6.Controls.Add(btnCargarCliente);
            flowLayoutPanel6.Controls.Add(txtCliente);
            flowLayoutPanel6.Controls.Add(cbxConsumidorFinal);
            flowLayoutPanel6.Location = new Point(3, 3);
            flowLayoutPanel6.Name = "flowLayoutPanel6";
            flowLayoutPanel6.Size = new Size(434, 127);
            flowLayoutPanel6.TabIndex = 47;
            // 
            // txtAreaDetallesVenta
            // 
            txtAreaDetallesVenta.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            tableLayoutPanel8.SetColumnSpan(txtAreaDetallesVenta, 4);
            txtAreaDetallesVenta.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtAreaDetallesVenta.Location = new Point(3, 603);
            txtAreaDetallesVenta.Multiline = true;
            txtAreaDetallesVenta.Name = "txtAreaDetallesVenta";
            txtAreaDetallesVenta.Size = new Size(1095, 116);
            txtAreaDetallesVenta.TabIndex = 47;
            // 
            // FVenta
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            ClientSize = new Size(1125, 818);
            Controls.Add(tableLayoutPanel8);
            MaximizeBox = true;
            MdiChildrenMinimizedAnchorBottom = false;
            MinimizeBox = true;
            Name = "FVenta";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "FVenta";
            WindowState = FormWindowState.Maximized;
            Load += FVenta_Load;
            ((System.ComponentModel.ISupportInitialize)error).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvProductos).EndInit();
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
            tableLayoutPanel6.ResumeLayout(false);
            flowLayoutPanel15.ResumeLayout(false);
            tableLayoutPanel7.ResumeLayout(false);
            tableLayoutPanel8.ResumeLayout(false);
            tableLayoutPanel8.PerformLayout();
            tableLayoutPanel10.ResumeLayout(false);
            flowLayoutPanel10.ResumeLayout(false);
            tableLayoutPanel9.ResumeLayout(false);
            flowLayoutPanel8.ResumeLayout(false);
            tableLayoutPanel11.ResumeLayout(false);
            flowLayoutPanel13.ResumeLayout(false);
            flowLayoutPanel13.PerformLayout();
            flowLayoutPanel11.ResumeLayout(false);
            flowLayoutPanel11.PerformLayout();
            flowLayoutPanel12.ResumeLayout(false);
            flowLayoutPanel12.PerformLayout();
            flowLayoutPanel7.ResumeLayout(false);
            flowLayoutPanel7.PerformLayout();
            flowLayoutPanel6.ResumeLayout(false);
            flowLayoutPanel6.PerformLayout();
            ResumeLayout(false);
        }

        #endregion
        private Label lblFechaHoy;
        private Button button1;
        private Button btnLimpiar;
        private TextBox txtCliente;
        private Button btnCargarCliente;
        private CheckBox cbxConsumidorFinal;
        private Button btnCargarProducto;
        private DataGridView dgvProductos;
        private Label lblHoraMinutos;
        private Label lblTotal;
        private Label lblSubtotal;
        private Label lblNroVenta;
        private Label lblNro;
        private Button btnCambiarVendedor;
        private Button btnConfirmarYFPago;
        private Button btnCancelar;
        private TextBox txtSubtotal;
        private TextBox txtDescuentoEfectivo;
        private TextBox txtTotal;
        private CheckBox cbxIncluirCtaCte;
        private Label lblPorcentajeDescuento;
        private CheckBox cbxDescEfectivo;
        private TableLayoutPanel tableLayoutPanel1;
        private TableLayoutPanel tableLayoutPanel2;
        private FlowLayoutPanel flowLayoutPanel2;
        private Label lblVendedor;
        private Label lblVendedorAsignado;
        private TableLayoutPanel tableLayoutPanel3;
        private FlowLayoutPanel flowLayoutPanel3;
        private FlowLayoutPanel flowLayoutPanel4;
        private FlowLayoutPanel flowLayoutPanel5;
        private TableLayoutPanel tableLayoutPanel4;
        private TableLayoutPanel tableLayoutPanel5;
        private Button btnCargarOferta;
        private TableLayoutPanel tableLayoutPanel6;
        private TableLayoutPanel tableLayoutPanel7;
        private TableLayoutPanel tableLayoutPanel8;
        private TableLayoutPanel tableLayoutPanel9;
        private FlowLayoutPanel flowLayoutPanel8;
        private FlowLayoutPanel flowLayoutPanel7;
        private FlowLayoutPanel flowLayoutPanel6;
        private TableLayoutPanel tableLayoutPanel10;
        private FlowLayoutPanel flowLayoutPanel10;
        private TableLayoutPanel tableLayoutPanel11;
        private FlowLayoutPanel flowLayoutPanel13;
        private FlowLayoutPanel flowLayoutPanel11;
        private FlowLayoutPanel flowLayoutPanel12;
        private Label lblDiaAbrev;
        private FlowLayoutPanel flowLayoutPanel14;
        private FlowLayoutPanel flowLayoutPanel15;
        private TextBox txtAreaDetallesVenta;
    }
}