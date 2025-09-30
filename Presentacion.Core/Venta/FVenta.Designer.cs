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
            lblHoy = new Label();
            lblFechaHoy = new Label();
            lblUsuarioLogeadoName = new Label();
            lblUsuario = new Label();
            lblLocalAsignado = new Label();
            lblLocal = new Label();
            button1 = new Button();
            button2 = new Button();
            txtCliente = new TextBox();
            btnCargarCliente = new Button();
            cbxConsumidorFinal = new CheckBox();
            btnCargarProducto = new Button();
            txtProductoCargado = new TextBox();
            dgvProductos = new DataGridView();
            lblHoraMinutos = new Label();
            lblHoraHoy = new Label();
            lblTotal = new Label();
            lblSubtotal = new Label();
            cbxEnOferta = new CheckBox();
            lblNroVenta = new Label();
            lblNro = new Label();
            cbxUsuarioLogeado = new CheckBox();
            btnCargarVendedor = new Button();
            txtVendedorAsignado = new TextBox();
            btnConfirmarYFPago = new Button();
            btnCancelar = new Button();
            txtSubtotal = new TextBox();
            txtDescuento = new TextBox();
            txtTotal = new TextBox();
            lblDetallesDeVenta = new Label();
            txtAreaDetallesVenta = new TextBox();
            cbxIncluirCtaCte = new CheckBox();
            lblPorcentajeDescuento = new Label();
            cbxAplicarDescuento = new CheckBox();
            ((System.ComponentModel.ISupportInitialize)error).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvProductos).BeginInit();
            SuspendLayout();
            // 
            // lblHoy
            // 
            lblHoy.AutoSize = true;
            lblHoy.Location = new Point(695, 7);
            lblHoy.Name = "lblHoy";
            lblHoy.Size = new Size(41, 15);
            lblHoy.TabIndex = 0;
            lblHoy.Text = "Fecha:";
            // 
            // lblFechaHoy
            // 
            lblFechaHoy.AutoSize = true;
            lblFechaHoy.Location = new Point(736, 8);
            lblFechaHoy.Name = "lblFechaHoy";
            lblFechaHoy.Size = new Size(53, 15);
            lblFechaHoy.TabIndex = 1;
            lblFechaHoy.Text = "00/00/00";
            // 
            // lblUsuarioLogeadoName
            // 
            lblUsuarioLogeadoName.AutoSize = true;
            lblUsuarioLogeadoName.Location = new Point(243, 8);
            lblUsuarioLogeadoName.Name = "lblUsuarioLogeadoName";
            lblUsuarioLogeadoName.Size = new Size(93, 15);
            lblUsuarioLogeadoName.TabIndex = 3;
            lblUsuarioLogeadoName.Text = "UsuarioLogeado";
            // 
            // lblUsuario
            // 
            lblUsuario.AutoSize = true;
            lblUsuario.Location = new Point(187, 8);
            lblUsuario.Name = "lblUsuario";
            lblUsuario.Size = new Size(50, 15);
            lblUsuario.TabIndex = 2;
            lblUsuario.Text = "Usuario:";
            // 
            // lblLocalAsignado
            // 
            lblLocalAsignado.AutoSize = true;
            lblLocalAsignado.Location = new Point(50, 9);
            lblLocalAsignado.Name = "lblLocalAsignado";
            lblLocalAsignado.Size = new Size(88, 15);
            lblLocalAsignado.TabIndex = 5;
            lblLocalAsignado.Text = "Local Asignado";
            // 
            // lblLocal
            // 
            lblLocal.AutoSize = true;
            lblLocal.Location = new Point(14, 9);
            lblLocal.Name = "lblLocal";
            lblLocal.Size = new Size(38, 15);
            lblLocal.TabIndex = 4;
            lblLocal.Text = "Local:";
            // 
            // button1
            // 
            button1.Location = new Point(32, 581);
            button1.Name = "button1";
            button1.Size = new Size(161, 42);
            button1.TabIndex = 6;
            button1.Text = "Ultimas Ventas";
            button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            button2.Location = new Point(210, 581);
            button2.Name = "button2";
            button2.Size = new Size(161, 42);
            button2.TabIndex = 7;
            button2.Text = "Cancelación / Devolución";
            button2.UseVisualStyleBackColor = true;
            // 
            // txtCliente
            // 
            txtCliente.Location = new Point(191, 87);
            txtCliente.Name = "txtCliente";
            txtCliente.ReadOnly = true;
            txtCliente.Size = new Size(423, 23);
            txtCliente.TabIndex = 8;
            // 
            // btnCargarCliente
            // 
            btnCargarCliente.Location = new Point(12, 87);
            btnCargarCliente.Name = "btnCargarCliente";
            btnCargarCliente.Size = new Size(161, 23);
            btnCargarCliente.TabIndex = 9;
            btnCargarCliente.Text = "Cargar Cliente";
            btnCargarCliente.UseVisualStyleBackColor = true;
            // 
            // cbxConsumidorFinal
            // 
            cbxConsumidorFinal.AutoSize = true;
            cbxConsumidorFinal.Location = new Point(628, 91);
            cbxConsumidorFinal.Name = "cbxConsumidorFinal";
            cbxConsumidorFinal.Size = new Size(130, 19);
            cbxConsumidorFinal.TabIndex = 10;
            cbxConsumidorFinal.Text = "Es consumidor final";
            cbxConsumidorFinal.UseVisualStyleBackColor = true;
            cbxConsumidorFinal.CheckedChanged += cbxConsumidorFinal_CheckedChanged;
            // 
            // btnCargarProducto
            // 
            btnCargarProducto.Location = new Point(12, 116);
            btnCargarProducto.Name = "btnCargarProducto";
            btnCargarProducto.Size = new Size(161, 23);
            btnCargarProducto.TabIndex = 12;
            btnCargarProducto.Text = "Cargar Producto";
            btnCargarProducto.UseVisualStyleBackColor = true;
            btnCargarProducto.Click += btnCargarProducto_Click;
            // 
            // txtProductoCargado
            // 
            txtProductoCargado.Location = new Point(191, 116);
            txtProductoCargado.Name = "txtProductoCargado";
            txtProductoCargado.ReadOnly = true;
            txtProductoCargado.Size = new Size(423, 23);
            txtProductoCargado.TabIndex = 11;
            // 
            // dgvProductos
            // 
            dgvProductos.AllowUserToAddRows = false;
            dgvProductos.AllowUserToDeleteRows = false;
            dgvProductos.AllowUserToResizeRows = false;
            dgvProductos.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvProductos.EditMode = DataGridViewEditMode.EditProgrammatically;
            dgvProductos.Location = new Point(14, 156);
            dgvProductos.MultiSelect = false;
            dgvProductos.Name = "dgvProductos";
            dgvProductos.Size = new Size(1072, 302);
            dgvProductos.TabIndex = 15;
            dgvProductos.CellClick += dgvProductos_CellClick;
            dgvProductos.RowEnter += dgvProductos_RowEnter;
            dgvProductos.SelectionChanged += dgvProductos_SelectionChanged;
            // 
            // lblHoraMinutos
            // 
            lblHoraMinutos.AutoSize = true;
            lblHoraMinutos.Location = new Point(862, 8);
            lblHoraMinutos.Name = "lblHoraMinutos";
            lblHoraMinutos.Size = new Size(49, 15);
            lblHoraMinutos.TabIndex = 17;
            lblHoraMinutos.Text = "00:00:00";
            // 
            // lblHoraHoy
            // 
            lblHoraHoy.AutoSize = true;
            lblHoraHoy.Location = new Point(825, 7);
            lblHoraHoy.Name = "lblHoraHoy";
            lblHoraHoy.Size = new Size(36, 15);
            lblHoraHoy.TabIndex = 16;
            lblHoraHoy.Text = "Hora:";
            // 
            // lblTotal
            // 
            lblTotal.AutoSize = true;
            lblTotal.Location = new Point(951, 541);
            lblTotal.Name = "lblTotal";
            lblTotal.Size = new Size(36, 15);
            lblTotal.TabIndex = 18;
            lblTotal.Text = "Total:";
            // 
            // lblSubtotal
            // 
            lblSubtotal.AutoSize = true;
            lblSubtotal.Location = new Point(926, 501);
            lblSubtotal.Name = "lblSubtotal";
            lblSubtotal.Size = new Size(61, 15);
            lblSubtotal.TabIndex = 19;
            lblSubtotal.Text = "Sub-Total:";
            // 
            // cbxEnOferta
            // 
            cbxEnOferta.AutoSize = true;
            cbxEnOferta.Location = new Point(628, 120);
            cbxEnOferta.Name = "cbxEnOferta";
            cbxEnOferta.Size = new Size(137, 19);
            cbxEnOferta.TabIndex = 20;
            cbxEnOferta.Text = "Producto/s en Oferta";
            cbxEnOferta.UseVisualStyleBackColor = true;
            cbxEnOferta.CheckedChanged += cbxEnOferta_CheckedChanged;
            // 
            // lblNroVenta
            // 
            lblNroVenta.AutoSize = true;
            lblNroVenta.Location = new Point(928, 8);
            lblNroVenta.Name = "lblNroVenta";
            lblNroVenta.Size = new Size(62, 15);
            lblNroVenta.TabIndex = 26;
            lblNroVenta.Text = "Nro Venta:";
            // 
            // lblNro
            // 
            lblNro.AutoSize = true;
            lblNro.Location = new Point(989, 8);
            lblNro.Name = "lblNro";
            lblNro.Size = new Size(97, 15);
            lblNro.TabIndex = 27;
            lblNro.Text = "000000000000000";
            // 
            // cbxUsuarioLogeado
            // 
            cbxUsuarioLogeado.AutoSize = true;
            cbxUsuarioLogeado.Location = new Point(628, 62);
            cbxUsuarioLogeado.Name = "cbxUsuarioLogeado";
            cbxUsuarioLogeado.Size = new Size(125, 19);
            cbxUsuarioLogeado.TabIndex = 30;
            cbxUsuarioLogeado.Text = "Es usuario logeado";
            cbxUsuarioLogeado.UseVisualStyleBackColor = true;
            cbxUsuarioLogeado.CheckedChanged += cbxUsuarioLogeado_CheckedChanged;
            // 
            // btnCargarVendedor
            // 
            btnCargarVendedor.Location = new Point(14, 58);
            btnCargarVendedor.Name = "btnCargarVendedor";
            btnCargarVendedor.Size = new Size(161, 23);
            btnCargarVendedor.TabIndex = 29;
            btnCargarVendedor.Text = "Cargar Vendedor";
            btnCargarVendedor.UseVisualStyleBackColor = true;
            btnCargarVendedor.Click += btnCargarVendedor_Click;
            // 
            // txtVendedorAsignado
            // 
            txtVendedorAsignado.Location = new Point(193, 58);
            txtVendedorAsignado.Name = "txtVendedorAsignado";
            txtVendedorAsignado.ReadOnly = true;
            txtVendedorAsignado.Size = new Size(423, 23);
            txtVendedorAsignado.TabIndex = 28;
            // 
            // btnConfirmarYFPago
            // 
            btnConfirmarYFPago.Location = new Point(743, 579);
            btnConfirmarYFPago.Name = "btnConfirmarYFPago";
            btnConfirmarYFPago.Size = new Size(160, 44);
            btnConfirmarYFPago.TabIndex = 31;
            btnConfirmarYFPago.Text = "Pagar";
            btnConfirmarYFPago.UseVisualStyleBackColor = true;
            btnConfirmarYFPago.Click += btnConfirmarYFPago_Click;
            // 
            // btnCancelar
            // 
            btnCancelar.Location = new Point(926, 579);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new Size(160, 44);
            btnCancelar.TabIndex = 32;
            btnCancelar.Text = "Cancelar";
            btnCancelar.UseVisualStyleBackColor = true;
            btnCancelar.Click += btnCancelar_Click;
            // 
            // txtSubtotal
            // 
            txtSubtotal.Location = new Point(993, 493);
            txtSubtotal.Name = "txtSubtotal";
            txtSubtotal.ReadOnly = true;
            txtSubtotal.Size = new Size(93, 23);
            txtSubtotal.TabIndex = 33;
            // 
            // txtDescuento
            // 
            txtDescuento.Location = new Point(993, 466);
            txtDescuento.Name = "txtDescuento";
            txtDescuento.Size = new Size(93, 23);
            txtDescuento.TabIndex = 34;
            txtDescuento.TextChanged += txtDescuento_TextChanged;
            // 
            // txtTotal
            // 
            txtTotal.Location = new Point(993, 533);
            txtTotal.Name = "txtTotal";
            txtTotal.ReadOnly = true;
            txtTotal.Size = new Size(93, 23);
            txtTotal.TabIndex = 35;
            // 
            // lblDetallesDeVenta
            // 
            lblDetallesDeVenta.AutoSize = true;
            lblDetallesDeVenta.Location = new Point(32, 464);
            lblDetallesDeVenta.Name = "lblDetallesDeVenta";
            lblDetallesDeVenta.Size = new Size(108, 15);
            lblDetallesDeVenta.TabIndex = 36;
            lblDetallesDeVenta.Text = "Detalles de la Venta";
            // 
            // txtAreaDetallesVenta
            // 
            txtAreaDetallesVenta.BorderStyle = BorderStyle.FixedSingle;
            txtAreaDetallesVenta.Location = new Point(37, 488);
            txtAreaDetallesVenta.Multiline = true;
            txtAreaDetallesVenta.Name = "txtAreaDetallesVenta";
            txtAreaDetallesVenta.ReadOnly = true;
            txtAreaDetallesVenta.Size = new Size(709, 71);
            txtAreaDetallesVenta.TabIndex = 37;
            // 
            // cbxIncluirCtaCte
            // 
            cbxIncluirCtaCte.AutoSize = true;
            cbxIncluirCtaCte.Location = new Point(763, 501);
            cbxIncluirCtaCte.Name = "cbxIncluirCtaCte";
            cbxIncluirCtaCte.Size = new Size(110, 19);
            cbxIncluirCtaCte.TabIndex = 39;
            cbxIncluirCtaCte.Text = "Permitir Cta Cte";
            cbxIncluirCtaCte.UseVisualStyleBackColor = true;
            cbxIncluirCtaCte.CheckedChanged += cbxIncluirCtaCte_CheckedChanged;
            // 
            // lblPorcentajeDescuento
            // 
            lblPorcentajeDescuento.AutoSize = true;
            lblPorcentajeDescuento.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblPorcentajeDescuento.Location = new Point(959, 464);
            lblPorcentajeDescuento.Name = "lblPorcentajeDescuento";
            lblPorcentajeDescuento.Size = new Size(28, 25);
            lblPorcentajeDescuento.TabIndex = 40;
            lblPorcentajeDescuento.Text = "%";
            // 
            // cbxAplicarDescuento
            // 
            cbxAplicarDescuento.AutoSize = true;
            cbxAplicarDescuento.Enabled = false;
            cbxAplicarDescuento.Location = new Point(763, 469);
            cbxAplicarDescuento.Name = "cbxAplicarDescuento";
            cbxAplicarDescuento.Size = new Size(122, 19);
            cbxAplicarDescuento.TabIndex = 41;
            cbxAplicarDescuento.Text = "Aplicar Descuento";
            cbxAplicarDescuento.UseVisualStyleBackColor = true;
            cbxAplicarDescuento.CheckedChanged += cbxAplicarDescuento_CheckedChanged;
            // 
            // FVenta
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1108, 630);
            Controls.Add(cbxAplicarDescuento);
            Controls.Add(lblPorcentajeDescuento);
            Controls.Add(cbxIncluirCtaCte);
            Controls.Add(txtAreaDetallesVenta);
            Controls.Add(lblDetallesDeVenta);
            Controls.Add(txtTotal);
            Controls.Add(txtDescuento);
            Controls.Add(txtSubtotal);
            Controls.Add(btnCancelar);
            Controls.Add(btnConfirmarYFPago);
            Controls.Add(cbxUsuarioLogeado);
            Controls.Add(btnCargarVendedor);
            Controls.Add(txtVendedorAsignado);
            Controls.Add(lblNro);
            Controls.Add(lblNroVenta);
            Controls.Add(cbxEnOferta);
            Controls.Add(lblSubtotal);
            Controls.Add(lblTotal);
            Controls.Add(lblHoraMinutos);
            Controls.Add(lblHoraHoy);
            Controls.Add(dgvProductos);
            Controls.Add(btnCargarProducto);
            Controls.Add(txtProductoCargado);
            Controls.Add(cbxConsumidorFinal);
            Controls.Add(btnCargarCliente);
            Controls.Add(txtCliente);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(lblLocalAsignado);
            Controls.Add(lblLocal);
            Controls.Add(lblUsuarioLogeadoName);
            Controls.Add(lblUsuario);
            Controls.Add(lblFechaHoy);
            Controls.Add(lblHoy);
            Name = "FVenta";
            Text = "FVenta";
            Load += FVenta_Load;
            ((System.ComponentModel.ISupportInitialize)error).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvProductos).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblHoy;
        private Label lblFechaHoy;
        private Label lblUsuarioLogeadoName;
        private Label lblUsuario;
        private Label lblLocalAsignado;
        private Label lblLocal;
        private Button button1;
        private Button button2;
        private TextBox txtCliente;
        private Button btnCargarCliente;
        private CheckBox cbxConsumidorFinal;
        private Button btnCargarProducto;
        private TextBox txtProductoCargado;
        private DataGridView dgvProductos;
        private Label lblHoraMinutos;
        private Label lblHoraHoy;
        private Label lblTotal;
        private Label lblSubtotal;
        private CheckBox cbxEnOferta;
        private Label lblNroVenta;
        private Label lblNro;
        private CheckBox cbxUsuarioLogeado;
        private Button btnCargarVendedor;
        private TextBox txtVendedorAsignado;
        private Button btnConfirmarYFPago;
        private Button btnCancelar;
        private TextBox txtSubtotal;
        private TextBox txtDescuento;
        private TextBox txtTotal;
        private Label lblDetallesDeVenta;
        private TextBox txtAreaDetallesVenta;
        private CheckBox cbxIncluirCtaCte;
        private Label lblPorcentajeDescuento;
        private CheckBox cbxAplicarDescuento;
    }
}