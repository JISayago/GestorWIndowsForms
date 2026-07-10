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
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            txtAreaDetallesVenta = new TextBox();
            tableLayoutPanel10 = new TableLayoutPanel();
            flowLayoutPanel10 = new FlowLayoutPanel();
            btnCancelar = new Button();
            btnConfirmarYFPago = new Button();
            btnLimpiar = new Button();
            dgvProductos = new DataGridView();
            tableLayoutPanel9 = new TableLayoutPanel();
            flowLayoutPanel8 = new FlowLayoutPanel();
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
            flowLayoutPanel7 = new FlowLayoutPanel();
            cbxIncluirCtaCte = new CheckBox();
            cbxDescEfectivo = new CheckBox();
            flowLayoutPanel6 = new FlowLayoutPanel();
            btnCargarCliente = new Button();
            txtCliente = new TextBox();
            cbxConsumidorFinal = new CheckBox();
            tableLayoutPanel15 = new TableLayoutPanel();
            tableLayoutPanel16 = new TableLayoutPanel();
            btnAgregarProducto = new Button();
            tableLayoutPanel7 = new TableLayoutPanel();
            lblDescripcion = new Label();
            txtDescripcion = new TextBox();
            tableLayoutPanel14 = new TableLayoutPanel();
            tableLayoutPanel8 = new TableLayoutPanel();
            lblCodigo = new Label();
            txtCodigoProducto = new TextBox();
            tableLayoutPanel12 = new TableLayoutPanel();
            lblmonto = new Label();
            txtPrecio = new TextBox();
            tableLayoutPanel13 = new TableLayoutPanel();
            lblCantidad = new Label();
            txtCantidad = new TextBox();
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
            tableLayoutPanel18 = new TableLayoutPanel();
            tableLayoutPanel5 = new TableLayoutPanel();
            tableLayoutPanel10.SuspendLayout();
            flowLayoutPanel10.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvProductos).BeginInit();
            tableLayoutPanel9.SuspendLayout();
            flowLayoutPanel8.SuspendLayout();
            tableLayoutPanel11.SuspendLayout();
            flowLayoutPanel13.SuspendLayout();
            flowLayoutPanel11.SuspendLayout();
            flowLayoutPanel12.SuspendLayout();
            flowLayoutPanel7.SuspendLayout();
            flowLayoutPanel6.SuspendLayout();
            tableLayoutPanel15.SuspendLayout();
            tableLayoutPanel16.SuspendLayout();
            tableLayoutPanel7.SuspendLayout();
            tableLayoutPanel14.SuspendLayout();
            tableLayoutPanel8.SuspendLayout();
            tableLayoutPanel12.SuspendLayout();
            tableLayoutPanel13.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            flowLayoutPanel5.SuspendLayout();
            tableLayoutPanel4.SuspendLayout();
            flowLayoutPanel14.SuspendLayout();
            flowLayoutPanel4.SuspendLayout();
            flowLayoutPanel3.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            tableLayoutPanel3.SuspendLayout();
            flowLayoutPanel2.SuspendLayout();
            tableLayoutPanel18.SuspendLayout();
            tableLayoutPanel5.SuspendLayout();
            SuspendLayout();
            // 
            // txtAreaDetallesVenta
            // 
            txtAreaDetallesVenta.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            txtAreaDetallesVenta.BorderStyle = BorderStyle.FixedSingle;
            txtAreaDetallesVenta.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtAreaDetallesVenta.Location = new Point(3, 573);
            txtAreaDetallesVenta.Multiline = true;
            txtAreaDetallesVenta.Name = "txtAreaDetallesVenta";
            txtAreaDetallesVenta.ReadOnly = true;
            txtAreaDetallesVenta.Size = new Size(1154, 146);
            txtAreaDetallesVenta.TabIndex = 48;
            txtAreaDetallesVenta.TabStop = false;
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
            tableLayoutPanel10.Location = new Point(3, 729);
            tableLayoutPanel10.Name = "tableLayoutPanel10";
            tableLayoutPanel10.RowCount = 1;
            tableLayoutPanel10.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel10.Size = new Size(1154, 68);
            tableLayoutPanel10.TabIndex = 51;
            // 
            // flowLayoutPanel10
            // 
            flowLayoutPanel10.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            flowLayoutPanel10.Controls.Add(btnCancelar);
            flowLayoutPanel10.Controls.Add(btnConfirmarYFPago);
            flowLayoutPanel10.FlowDirection = FlowDirection.RightToLeft;
            flowLayoutPanel10.Location = new Point(694, 3);
            flowLayoutPanel10.Name = "flowLayoutPanel10";
            flowLayoutPanel10.Size = new Size(457, 62);
            flowLayoutPanel10.TabIndex = 47;
            // 
            // btnCancelar
            // 
            btnCancelar.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnCancelar.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnCancelar.Location = new Point(294, 3);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new Size(160, 44);
            btnCancelar.TabIndex = 32;
            btnCancelar.Text = "Cancelar";
            btnCancelar.UseVisualStyleBackColor = true;
            btnCancelar.Click += btnCancelar_Click;
            // 
            // btnConfirmarYFPago
            // 
            btnConfirmarYFPago.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnConfirmarYFPago.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnConfirmarYFPago.Location = new Point(128, 3);
            btnConfirmarYFPago.Name = "btnConfirmarYFPago";
            btnConfirmarYFPago.Size = new Size(160, 44);
            btnConfirmarYFPago.TabIndex = 31;
            btnConfirmarYFPago.Text = "Pagar";
            btnConfirmarYFPago.UseVisualStyleBackColor = true;
            btnConfirmarYFPago.Click += btnConfirmarYFPago_Click;
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
            dgvProductos.Location = new Point(3, 92);
            dgvProductos.MultiSelect = false;
            dgvProductos.Name = "dgvProductos";
            dataGridViewCellStyle3.Font = new Font("Microsoft Sans Serif", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dgvProductos.RowsDefaultCellStyle = dataGridViewCellStyle3;
            dgvProductos.Size = new Size(1148, 203);
            dgvProductos.TabIndex = 16;
            dgvProductos.CellContentClick += dgvProductos_CellContentClick;
            // 
            // tableLayoutPanel9
            // 
            tableLayoutPanel9.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tableLayoutPanel9.BackColor = SystemColors.Control;
            tableLayoutPanel9.ColumnCount = 3;
            tableLayoutPanel9.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 34.0524254F));
            tableLayoutPanel9.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 29.0703373F));
            tableLayoutPanel9.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 36.87724F));
            tableLayoutPanel9.Controls.Add(flowLayoutPanel8, 2, 0);
            tableLayoutPanel9.Controls.Add(flowLayoutPanel7, 1, 0);
            tableLayoutPanel9.Controls.Add(flowLayoutPanel6, 0, 0);
            tableLayoutPanel9.Location = new Point(3, 383);
            tableLayoutPanel9.Name = "tableLayoutPanel9";
            tableLayoutPanel9.RowCount = 1;
            tableLayoutPanel9.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel9.Size = new Size(1154, 184);
            tableLayoutPanel9.TabIndex = 52;
            // 
            // flowLayoutPanel8
            // 
            flowLayoutPanel8.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            flowLayoutPanel8.BackColor = SystemColors.Control;
            flowLayoutPanel8.Controls.Add(tableLayoutPanel11);
            flowLayoutPanel8.FlowDirection = FlowDirection.RightToLeft;
            flowLayoutPanel8.Location = new Point(730, 3);
            flowLayoutPanel8.Name = "flowLayoutPanel8";
            flowLayoutPanel8.Size = new Size(421, 178);
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
            tableLayoutPanel11.Location = new Point(3, 3);
            tableLayoutPanel11.Name = "tableLayoutPanel11";
            tableLayoutPanel11.RowCount = 3;
            tableLayoutPanel11.RowStyles.Add(new RowStyle(SizeType.Percent, 33.3333321F));
            tableLayoutPanel11.RowStyles.Add(new RowStyle(SizeType.Percent, 33.3333321F));
            tableLayoutPanel11.RowStyles.Add(new RowStyle(SizeType.Percent, 33.3333321F));
            tableLayoutPanel11.Size = new Size(415, 143);
            tableLayoutPanel11.TabIndex = 41;
            // 
            // flowLayoutPanel13
            // 
            flowLayoutPanel13.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            flowLayoutPanel13.Controls.Add(txtTotal);
            flowLayoutPanel13.Controls.Add(lblTotal);
            flowLayoutPanel13.FlowDirection = FlowDirection.RightToLeft;
            flowLayoutPanel13.Location = new Point(3, 97);
            flowLayoutPanel13.Name = "flowLayoutPanel13";
            flowLayoutPanel13.Size = new Size(409, 43);
            flowLayoutPanel13.TabIndex = 42;
            // 
            // txtTotal
            // 
            txtTotal.BorderStyle = BorderStyle.FixedSingle;
            txtTotal.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            txtTotal.Location = new Point(245, 3);
            txtTotal.Name = "txtTotal";
            txtTotal.ReadOnly = true;
            txtTotal.Size = new Size(161, 33);
            txtTotal.TabIndex = 260;
            // 
            // lblTotal
            // 
            lblTotal.AutoSize = true;
            lblTotal.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTotal.Location = new Point(179, 0);
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
            flowLayoutPanel11.Size = new Size(409, 29);
            flowLayoutPanel11.TabIndex = 0;
            // 
            // txtDescuentoEfectivo
            // 
            txtDescuentoEfectivo.BorderStyle = BorderStyle.FixedSingle;
            txtDescuentoEfectivo.Enabled = false;
            txtDescuentoEfectivo.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            txtDescuentoEfectivo.Location = new Point(245, 3);
            txtDescuentoEfectivo.Name = "txtDescuentoEfectivo";
            txtDescuentoEfectivo.ReadOnly = true;
            txtDescuentoEfectivo.Size = new Size(161, 33);
            txtDescuentoEfectivo.TabIndex = 200;
            txtDescuentoEfectivo.TextChanged += txtDescuentoEfectivo_TextChanged;
            // 
            // lblPorcentajeDescuento
            // 
            lblPorcentajeDescuento.AutoSize = true;
            lblPorcentajeDescuento.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblPorcentajeDescuento.Location = new Point(211, 0);
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
            flowLayoutPanel12.Location = new Point(3, 50);
            flowLayoutPanel12.Name = "flowLayoutPanel12";
            flowLayoutPanel12.Size = new Size(409, 29);
            flowLayoutPanel12.TabIndex = 1;
            // 
            // txtSubtotal
            // 
            txtSubtotal.BorderStyle = BorderStyle.FixedSingle;
            txtSubtotal.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            txtSubtotal.Location = new Point(245, 3);
            txtSubtotal.Name = "txtSubtotal";
            txtSubtotal.ReadOnly = true;
            txtSubtotal.Size = new Size(161, 33);
            txtSubtotal.TabIndex = 250;
            // 
            // lblSubtotal
            // 
            lblSubtotal.AutoSize = true;
            lblSubtotal.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblSubtotal.Location = new Point(100, 0);
            lblSubtotal.Name = "lblSubtotal";
            lblSubtotal.Size = new Size(139, 25);
            lblSubtotal.TabIndex = 19;
            lblSubtotal.Text = "Sin Descuento";
            // 
            // flowLayoutPanel7
            // 
            flowLayoutPanel7.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            flowLayoutPanel7.BackColor = SystemColors.Control;
            flowLayoutPanel7.Controls.Add(cbxIncluirCtaCte);
            flowLayoutPanel7.Controls.Add(cbxDescEfectivo);
            flowLayoutPanel7.FlowDirection = FlowDirection.RightToLeft;
            flowLayoutPanel7.Location = new Point(395, 3);
            flowLayoutPanel7.Name = "flowLayoutPanel7";
            flowLayoutPanel7.Size = new Size(329, 178);
            flowLayoutPanel7.TabIndex = 47;
            // 
            // cbxIncluirCtaCte
            // 
            cbxIncluirCtaCte.AutoSize = true;
            cbxIncluirCtaCte.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            cbxIncluirCtaCte.Location = new Point(156, 3);
            cbxIncluirCtaCte.Name = "cbxIncluirCtaCte";
            cbxIncluirCtaCte.Size = new Size(170, 29);
            cbxIncluirCtaCte.TabIndex = 104;
            cbxIncluirCtaCte.Text = "Permitir Cta Cte";
            cbxIncluirCtaCte.UseVisualStyleBackColor = true;
            cbxIncluirCtaCte.CheckedChanged += cbxIncluirCtaCte_CheckedChanged;
            // 
            // cbxDescEfectivo
            // 
            cbxDescEfectivo.AutoSize = true;
            cbxDescEfectivo.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            cbxDescEfectivo.Location = new Point(89, 38);
            cbxDescEfectivo.Name = "cbxDescEfectivo";
            cbxDescEfectivo.Size = new Size(237, 29);
            cbxDescEfectivo.TabIndex = 105;
            cbxDescEfectivo.Text = "Descuento por Efectivo";
            cbxDescEfectivo.UseVisualStyleBackColor = true;
            cbxDescEfectivo.CheckedChanged += cbxDescEfectivo_CheckedChanged;
            // 
            // flowLayoutPanel6
            // 
            flowLayoutPanel6.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            flowLayoutPanel6.BackColor = SystemColors.Control;
            flowLayoutPanel6.Controls.Add(btnCargarCliente);
            flowLayoutPanel6.Controls.Add(txtCliente);
            flowLayoutPanel6.Controls.Add(cbxConsumidorFinal);
            flowLayoutPanel6.Location = new Point(3, 3);
            flowLayoutPanel6.Name = "flowLayoutPanel6";
            flowLayoutPanel6.Size = new Size(386, 178);
            flowLayoutPanel6.TabIndex = 47;
            // 
            // btnCargarCliente
            // 
            btnCargarCliente.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnCargarCliente.Location = new Point(3, 3);
            btnCargarCliente.Name = "btnCargarCliente";
            btnCargarCliente.Size = new Size(161, 30);
            btnCargarCliente.TabIndex = 90;
            btnCargarCliente.Text = "Cargar Cliente";
            btnCargarCliente.UseVisualStyleBackColor = true;
            btnCargarCliente.Click += btnCargarCliente_Click;
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
            // cbxConsumidorFinal
            // 
            cbxConsumidorFinal.AutoSize = true;
            cbxConsumidorFinal.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            cbxConsumidorFinal.Location = new Point(3, 72);
            cbxConsumidorFinal.Name = "cbxConsumidorFinal";
            cbxConsumidorFinal.Size = new Size(205, 29);
            cbxConsumidorFinal.TabIndex = 100;
            cbxConsumidorFinal.Text = "Es consumidor final";
            cbxConsumidorFinal.UseVisualStyleBackColor = true;
            cbxConsumidorFinal.CheckedChanged += cbxConsumidorFinal_CheckedChanged;
            // 
            // tableLayoutPanel15
            // 
            tableLayoutPanel15.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tableLayoutPanel15.ColumnCount = 3;
            tableLayoutPanel15.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 35F));
            tableLayoutPanel15.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 45F));
            tableLayoutPanel15.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            tableLayoutPanel15.Controls.Add(tableLayoutPanel16, 2, 0);
            tableLayoutPanel15.Controls.Add(tableLayoutPanel7, 0, 0);
            tableLayoutPanel15.Controls.Add(tableLayoutPanel14, 1, 0);
            tableLayoutPanel15.Location = new Point(3, 3);
            tableLayoutPanel15.Name = "tableLayoutPanel15";
            tableLayoutPanel15.RowCount = 1;
            tableLayoutPanel15.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel15.Size = new Size(1148, 83);
            tableLayoutPanel15.TabIndex = 57;
            // 
            // tableLayoutPanel16
            // 
            tableLayoutPanel16.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tableLayoutPanel16.ColumnCount = 1;
            tableLayoutPanel16.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel16.Controls.Add(btnAgregarProducto, 0, 0);
            tableLayoutPanel16.Location = new Point(920, 3);
            tableLayoutPanel16.Name = "tableLayoutPanel16";
            tableLayoutPanel16.RowCount = 1;
            tableLayoutPanel16.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel16.Size = new Size(225, 77);
            tableLayoutPanel16.TabIndex = 4;
            // 
            // btnAgregarProducto
            // 
            btnAgregarProducto.Anchor = AnchorStyles.None;
            btnAgregarProducto.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            btnAgregarProducto.Location = new Point(34, 16);
            btnAgregarProducto.Name = "btnAgregarProducto";
            btnAgregarProducto.Size = new Size(157, 44);
            btnAgregarProducto.TabIndex = 14;
            btnAgregarProducto.Text = "Agregar";
            btnAgregarProducto.UseVisualStyleBackColor = true;
            btnAgregarProducto.Click += btnAgregarProducto_Click;
            // 
            // tableLayoutPanel7
            // 
            tableLayoutPanel7.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tableLayoutPanel7.ColumnCount = 1;
            tableLayoutPanel7.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel7.Controls.Add(lblDescripcion, 0, 0);
            tableLayoutPanel7.Controls.Add(txtDescripcion, 0, 1);
            tableLayoutPanel7.Location = new Point(3, 3);
            tableLayoutPanel7.Name = "tableLayoutPanel7";
            tableLayoutPanel7.RowCount = 2;
            tableLayoutPanel7.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel7.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel7.Size = new Size(395, 77);
            tableLayoutPanel7.TabIndex = 0;
            // 
            // lblDescripcion
            // 
            lblDescripcion.Anchor = AnchorStyles.Left;
            lblDescripcion.AutoSize = true;
            lblDescripcion.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblDescripcion.Location = new Point(3, 8);
            lblDescripcion.Name = "lblDescripcion";
            lblDescripcion.Size = new Size(100, 21);
            lblDescripcion.TabIndex = 18;
            lblDescripcion.Text = "Descripción";
            // 
            // txtDescripcion
            // 
            txtDescripcion.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtDescripcion.BorderStyle = BorderStyle.FixedSingle;
            txtDescripcion.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtDescripcion.Location = new Point(3, 41);
            txtDescripcion.Name = "txtDescripcion";
            txtDescripcion.Size = new Size(389, 29);
            txtDescripcion.TabIndex = 10;
            // 
            // tableLayoutPanel14
            // 
            tableLayoutPanel14.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tableLayoutPanel14.ColumnCount = 3;
            tableLayoutPanel14.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
            tableLayoutPanel14.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
            tableLayoutPanel14.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
            tableLayoutPanel14.Controls.Add(tableLayoutPanel8, 0, 0);
            tableLayoutPanel14.Controls.Add(tableLayoutPanel12, 2, 0);
            tableLayoutPanel14.Controls.Add(tableLayoutPanel13, 1, 0);
            tableLayoutPanel14.Location = new Point(404, 3);
            tableLayoutPanel14.Name = "tableLayoutPanel14";
            tableLayoutPanel14.RowCount = 1;
            tableLayoutPanel14.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel14.Size = new Size(510, 77);
            tableLayoutPanel14.TabIndex = 1;
            // 
            // tableLayoutPanel8
            // 
            tableLayoutPanel8.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tableLayoutPanel8.ColumnCount = 1;
            tableLayoutPanel8.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel8.Controls.Add(lblCodigo, 0, 0);
            tableLayoutPanel8.Controls.Add(txtCodigoProducto, 0, 1);
            tableLayoutPanel8.Location = new Point(3, 3);
            tableLayoutPanel8.Name = "tableLayoutPanel8";
            tableLayoutPanel8.RowCount = 2;
            tableLayoutPanel8.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel8.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel8.Size = new Size(164, 71);
            tableLayoutPanel8.TabIndex = 1;
            // 
            // lblCodigo
            // 
            lblCodigo.Anchor = AnchorStyles.Left;
            lblCodigo.AutoSize = true;
            lblCodigo.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblCodigo.Location = new Point(3, 7);
            lblCodigo.Name = "lblCodigo";
            lblCodigo.Size = new Size(65, 21);
            lblCodigo.TabIndex = 25;
            lblCodigo.Text = "Código";
            // 
            // txtCodigoProducto
            // 
            txtCodigoProducto.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtCodigoProducto.BorderStyle = BorderStyle.FixedSingle;
            txtCodigoProducto.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtCodigoProducto.Location = new Point(3, 38);
            txtCodigoProducto.Name = "txtCodigoProducto";
            txtCodigoProducto.Size = new Size(158, 29);
            txtCodigoProducto.TabIndex = 11;
            // 
            // tableLayoutPanel12
            // 
            tableLayoutPanel12.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tableLayoutPanel12.ColumnCount = 1;
            tableLayoutPanel12.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel12.Controls.Add(lblmonto, 0, 0);
            tableLayoutPanel12.Controls.Add(txtPrecio, 0, 1);
            tableLayoutPanel12.Location = new Point(343, 3);
            tableLayoutPanel12.Name = "tableLayoutPanel12";
            tableLayoutPanel12.RowCount = 2;
            tableLayoutPanel12.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel12.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel12.Size = new Size(164, 71);
            tableLayoutPanel12.TabIndex = 3;
            // 
            // lblmonto
            // 
            lblmonto.Anchor = AnchorStyles.Left;
            lblmonto.AutoSize = true;
            lblmonto.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblmonto.Location = new Point(3, 7);
            lblmonto.Name = "lblmonto";
            lblmonto.Size = new Size(58, 21);
            lblmonto.TabIndex = 20;
            lblmonto.Text = "Precio";
            // 
            // txtPrecio
            // 
            txtPrecio.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtPrecio.BorderStyle = BorderStyle.FixedSingle;
            txtPrecio.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtPrecio.Location = new Point(3, 38);
            txtPrecio.Name = "txtPrecio";
            txtPrecio.Size = new Size(158, 29);
            txtPrecio.TabIndex = 13;
            // 
            // tableLayoutPanel13
            // 
            tableLayoutPanel13.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tableLayoutPanel13.ColumnCount = 1;
            tableLayoutPanel13.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel13.Controls.Add(lblCantidad, 0, 0);
            tableLayoutPanel13.Controls.Add(txtCantidad, 0, 1);
            tableLayoutPanel13.Location = new Point(173, 3);
            tableLayoutPanel13.Name = "tableLayoutPanel13";
            tableLayoutPanel13.RowCount = 2;
            tableLayoutPanel13.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel13.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel13.Size = new Size(164, 71);
            tableLayoutPanel13.TabIndex = 2;
            // 
            // lblCantidad
            // 
            lblCantidad.Anchor = AnchorStyles.Left;
            lblCantidad.AutoSize = true;
            lblCantidad.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblCantidad.Location = new Point(3, 7);
            lblCantidad.Name = "lblCantidad";
            lblCantidad.Size = new Size(79, 21);
            lblCantidad.TabIndex = 22;
            lblCantidad.Text = "Cantidad";
            // 
            // txtCantidad
            // 
            txtCantidad.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtCantidad.BorderStyle = BorderStyle.FixedSingle;
            txtCantidad.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtCantidad.Location = new Point(3, 38);
            txtCantidad.Name = "txtCantidad";
            txtCantidad.Size = new Size(158, 29);
            txtCantidad.TabIndex = 12;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
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
            tableLayoutPanel1.Size = new Size(1154, 70);
            tableLayoutPanel1.TabIndex = 43;
            // 
            // flowLayoutPanel5
            // 
            flowLayoutPanel5.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            flowLayoutPanel5.BackColor = SystemColors.Control;
            flowLayoutPanel5.Controls.Add(lblNro);
            flowLayoutPanel5.Controls.Add(lblNroVenta);
            flowLayoutPanel5.FlowDirection = FlowDirection.RightToLeft;
            flowLayoutPanel5.Location = new Point(794, 3);
            flowLayoutPanel5.Name = "flowLayoutPanel5";
            flowLayoutPanel5.Size = new Size(357, 64);
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
            tableLayoutPanel4.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tableLayoutPanel4.BackColor = SystemColors.Control;
            tableLayoutPanel4.ColumnCount = 3;
            tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 23.0769234F));
            tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 45.3846169F));
            tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 31.5384617F));
            tableLayoutPanel4.Controls.Add(flowLayoutPanel14, 2, 0);
            tableLayoutPanel4.Controls.Add(flowLayoutPanel4, 1, 0);
            tableLayoutPanel4.Controls.Add(flowLayoutPanel3, 0, 0);
            tableLayoutPanel4.Location = new Point(398, 3);
            tableLayoutPanel4.Name = "tableLayoutPanel4";
            tableLayoutPanel4.RowCount = 1;
            tableLayoutPanel4.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel4.Size = new Size(390, 64);
            tableLayoutPanel4.TabIndex = 45;
            // 
            // flowLayoutPanel14
            // 
            flowLayoutPanel14.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            flowLayoutPanel14.Controls.Add(lblHoraMinutos);
            flowLayoutPanel14.Location = new Point(270, 3);
            flowLayoutPanel14.Name = "flowLayoutPanel14";
            flowLayoutPanel14.Size = new Size(117, 43);
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
            flowLayoutPanel4.Size = new Size(171, 27);
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
            flowLayoutPanel3.Size = new Size(84, 43);
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
            tableLayoutPanel2.Size = new Size(389, 49);
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
            tableLayoutPanel3.Size = new Size(383, 43);
            tableLayoutPanel3.TabIndex = 45;
            // 
            // flowLayoutPanel2
            // 
            flowLayoutPanel2.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            flowLayoutPanel2.Controls.Add(lblVendedor);
            flowLayoutPanel2.Controls.Add(lblVendedorAsignado);
            flowLayoutPanel2.Location = new Point(3, 3);
            flowLayoutPanel2.Name = "flowLayoutPanel2";
            flowLayoutPanel2.Size = new Size(377, 37);
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
            lblVendedorAsignado.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblVendedorAsignado.Location = new Point(222, 0);
            lblVendedorAsignado.Name = "lblVendedorAsignado";
            lblVendedorAsignado.Size = new Size(76, 20);
            lblVendedorAsignado.TabIndex = 3;
            lblVendedorAsignado.Text = "Vendedor";
            // 
            // tableLayoutPanel18
            // 
            tableLayoutPanel18.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tableLayoutPanel18.BackColor = SystemColors.Control;
            tableLayoutPanel18.ColumnCount = 1;
            tableLayoutPanel18.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel18.Controls.Add(tableLayoutPanel1, 0, 0);
            tableLayoutPanel18.Controls.Add(tableLayoutPanel5, 0, 1);
            tableLayoutPanel18.Controls.Add(tableLayoutPanel9, 0, 2);
            tableLayoutPanel18.Controls.Add(txtAreaDetallesVenta, 0, 3);
            tableLayoutPanel18.Controls.Add(tableLayoutPanel10, 0, 4);
            tableLayoutPanel18.Location = new Point(12, 12);
            tableLayoutPanel18.Name = "tableLayoutPanel18";
            tableLayoutPanel18.RowCount = 5;
            tableLayoutPanel18.RowStyles.Add(new RowStyle(SizeType.Percent, 9.523809F));
            tableLayoutPanel18.RowStyles.Add(new RowStyle(SizeType.Percent, 38.0952377F));
            tableLayoutPanel18.RowStyles.Add(new RowStyle(SizeType.Percent, 23.8095245F));
            tableLayoutPanel18.RowStyles.Add(new RowStyle(SizeType.Percent, 19.0476189F));
            tableLayoutPanel18.RowStyles.Add(new RowStyle(SizeType.Percent, 9.523809F));
            tableLayoutPanel18.Size = new Size(1160, 800);
            tableLayoutPanel18.TabIndex = 54;
            // 
            // tableLayoutPanel5
            // 
            tableLayoutPanel5.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tableLayoutPanel5.ColumnCount = 1;
            tableLayoutPanel5.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel5.Controls.Add(tableLayoutPanel15, 0, 0);
            tableLayoutPanel5.Controls.Add(dgvProductos, 0, 1);
            tableLayoutPanel5.Location = new Point(3, 79);
            tableLayoutPanel5.Name = "tableLayoutPanel5";
            tableLayoutPanel5.RowCount = 2;
            tableLayoutPanel5.RowStyles.Add(new RowStyle(SizeType.Percent, 30F));
            tableLayoutPanel5.RowStyles.Add(new RowStyle(SizeType.Percent, 70F));
            tableLayoutPanel5.Size = new Size(1154, 298);
            tableLayoutPanel5.TabIndex = 55;
            // 
            // FVentaLibre
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoScroll = true;
            AutoSize = true;
            ClientSize = new Size(1170, 816);
            Controls.Add(tableLayoutPanel18);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            KeyPreview = true;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FVentaLibre";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Venta Libre";
            WindowState = FormWindowState.Maximized;
            Load += FVentaLibre_Load;
            tableLayoutPanel10.ResumeLayout(false);
            flowLayoutPanel10.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvProductos).EndInit();
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
            tableLayoutPanel15.ResumeLayout(false);
            tableLayoutPanel16.ResumeLayout(false);
            tableLayoutPanel7.ResumeLayout(false);
            tableLayoutPanel7.PerformLayout();
            tableLayoutPanel14.ResumeLayout(false);
            tableLayoutPanel8.ResumeLayout(false);
            tableLayoutPanel8.PerformLayout();
            tableLayoutPanel12.ResumeLayout(false);
            tableLayoutPanel12.PerformLayout();
            tableLayoutPanel13.ResumeLayout(false);
            tableLayoutPanel13.PerformLayout();
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
            tableLayoutPanel18.ResumeLayout(false);
            tableLayoutPanel18.PerformLayout();
            tableLayoutPanel5.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion
        private TextBox txtAreaDetallesVenta;
        private TableLayoutPanel tableLayoutPanel10;
        private FlowLayoutPanel flowLayoutPanel10;
        private Button btnCancelar;
        private Button btnConfirmarYFPago;
        private Button btnLimpiar;
        private DataGridView dgvProductos;
        private TableLayoutPanel tableLayoutPanel9;
        private FlowLayoutPanel flowLayoutPanel8;
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
        private FlowLayoutPanel flowLayoutPanel7;
        private CheckBox cbxIncluirCtaCte;
        private CheckBox cbxDescEfectivo;
        private FlowLayoutPanel flowLayoutPanel6;
        private Button btnCargarCliente;
        private TextBox txtCliente;
        private CheckBox cbxConsumidorFinal;
        private TableLayoutPanel tableLayoutPanel15;
        private TableLayoutPanel tableLayoutPanel16;
        private Button btnAgregarProducto;
        private TableLayoutPanel tableLayoutPanel7;
        private Label lblDescripcion;
        private TextBox txtDescripcion;
        private TableLayoutPanel tableLayoutPanel14;
        private TableLayoutPanel tableLayoutPanel8;
        private Label lblCodigo;
        private TextBox txtCodigoProducto;
        private TableLayoutPanel tableLayoutPanel12;
        private Label lblmonto;
        private TextBox txtPrecio;
        private TableLayoutPanel tableLayoutPanel13;
        private Label lblCantidad;
        private TextBox txtCantidad;
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
        private TableLayoutPanel tableLayoutPanel18;
        private TableLayoutPanel tableLayoutPanel5;
    }
}