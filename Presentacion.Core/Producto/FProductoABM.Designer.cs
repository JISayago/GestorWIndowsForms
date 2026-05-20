namespace Presentacion.Core.Producto
{
    partial class FProductoABM
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
            lblDescripcion = new Label();
            lblMarca = new Label();
            lblPrecioCosto = new Label();
            lblPrecioVenta = new Label();
            txtProducto = new TextBox();
            txtPrecioCosto = new TextBox();
            txtPrecioVenta = new TextBox();
            lblStock = new Label();
            txtStock = new TextBox();
            lblCategoria = new Label();
            lblMedida = new Label();
            lblUnidadMedida = new Label();
            txtMedida = new TextBox();
            txtUnidadMedida = new TextBox();
            cmbMarca = new ComboBox();
            btnCategorias = new Button();
            cmbRubro = new ComboBox();
            lblRubro = new Label();
            txtCodigo = new TextBox();
            txtCodigoBarra = new TextBox();
            lblCodigo = new Label();
            lblCodigoBarra = new Label();
            chkIvaIncluido = new CheckBox();
            chkEsFraccionable = new CheckBox();
            chkControlPorLotes = new CheckBox();
            chkTieneVencimiento = new CheckBox();
            chkbProductoDiscontinuado = new CheckBox();
            ((System.ComponentModel.ISupportInitialize)error).BeginInit();
            SuspendLayout();
            // 
            // lblDescripcion
            // 
            lblDescripcion.AutoSize = true;
            lblDescripcion.Location = new Point(23, 87);
            lblDescripcion.Name = "lblDescripcion";
            lblDescripcion.Size = new Size(70, 15);
            lblDescripcion.TabIndex = 1;
            lblDescripcion.Text = "Descripcion";
            // 
            // lblMarca
            // 
            lblMarca.AutoSize = true;
            lblMarca.Location = new Point(415, 87);
            lblMarca.Name = "lblMarca";
            lblMarca.Size = new Size(40, 15);
            lblMarca.TabIndex = 2;
            lblMarca.Text = "Marca";
            // 
            // lblPrecioCosto
            // 
            lblPrecioCosto.AutoSize = true;
            lblPrecioCosto.Location = new Point(15, 177);
            lblPrecioCosto.Name = "lblPrecioCosto";
            lblPrecioCosto.Size = new Size(73, 15);
            lblPrecioCosto.TabIndex = 3;
            lblPrecioCosto.Text = "Precio Costo";
            // 
            // lblPrecioVenta
            // 
            lblPrecioVenta.AutoSize = true;
            lblPrecioVenta.Location = new Point(218, 177);
            lblPrecioVenta.Name = "lblPrecioVenta";
            lblPrecioVenta.Size = new Size(73, 15);
            lblPrecioVenta.TabIndex = 4;
            lblPrecioVenta.Text = "Precio Venta";
            // 
            // txtProducto
            // 
            txtProducto.Location = new Point(100, 84);
            txtProducto.Name = "txtProducto";
            txtProducto.Size = new Size(303, 23);
            txtProducto.TabIndex = 5;
            // 
            // txtPrecioCosto
            // 
            txtPrecioCosto.Location = new Point(96, 172);
            txtPrecioCosto.Name = "txtPrecioCosto";
            txtPrecioCosto.Size = new Size(100, 23);
            txtPrecioCosto.TabIndex = 7;
            // 
            // txtPrecioVenta
            // 
            txtPrecioVenta.Location = new Point(302, 174);
            txtPrecioVenta.Name = "txtPrecioVenta";
            txtPrecioVenta.Size = new Size(100, 23);
            txtPrecioVenta.TabIndex = 8;
            // 
            // lblStock
            // 
            lblStock.AutoSize = true;
            lblStock.Location = new Point(418, 179);
            lblStock.Name = "lblStock";
            lblStock.Size = new Size(37, 15);
            lblStock.TabIndex = 10;
            lblStock.Text = "Stock";
            // 
            // txtStock
            // 
            txtStock.Location = new Point(464, 174);
            txtStock.Name = "txtStock";
            txtStock.Size = new Size(100, 23);
            txtStock.TabIndex = 11;
            // 
            // lblCategoria
            // 
            lblCategoria.AutoSize = true;
            lblCategoria.Location = new Point(589, 88);
            lblCategoria.Name = "lblCategoria";
            lblCategoria.Size = new Size(62, 15);
            lblCategoria.TabIndex = 15;
            lblCategoria.Text = "Categorias";
            // 
            // lblMedida
            // 
            lblMedida.AutoSize = true;
            lblMedida.Location = new Point(35, 133);
            lblMedida.Name = "lblMedida";
            lblMedida.Size = new Size(47, 15);
            lblMedida.TabIndex = 16;
            lblMedida.Text = "Medida";
            // 
            // lblUnidadMedida
            // 
            lblUnidadMedida.AutoSize = true;
            lblUnidadMedida.Location = new Point(207, 133);
            lblUnidadMedida.Name = "lblUnidadMedida";
            lblUnidadMedida.Size = new Size(88, 15);
            lblUnidadMedida.TabIndex = 17;
            lblUnidadMedida.Text = "Unidad Medida";
            // 
            // txtMedida
            // 
            txtMedida.Location = new Point(96, 128);
            txtMedida.Name = "txtMedida";
            txtMedida.Size = new Size(100, 23);
            txtMedida.TabIndex = 18;
            // 
            // txtUnidadMedida
            // 
            txtUnidadMedida.Location = new Point(304, 130);
            txtUnidadMedida.Name = "txtUnidadMedida";
            txtUnidadMedida.Size = new Size(100, 23);
            txtUnidadMedida.TabIndex = 19;
            // 
            // cmbMarca
            // 
            cmbMarca.FormattingEnabled = true;
            cmbMarca.Location = new Point(463, 84);
            cmbMarca.Name = "cmbMarca";
            cmbMarca.Size = new Size(100, 23);
            cmbMarca.TabIndex = 21;
            // 
            // btnCategorias
            // 
            btnCategorias.Location = new Point(658, 84);
            btnCategorias.Name = "btnCategorias";
            btnCategorias.Size = new Size(91, 23);
            btnCategorias.TabIndex = 22;
            btnCategorias.Text = "Seleccionar..";
            btnCategorias.UseVisualStyleBackColor = true;
            btnCategorias.Click += btnCategorias_Click;
            // 
            // cmbRubro
            // 
            cmbRubro.FormattingEnabled = true;
            cmbRubro.Location = new Point(464, 129);
            cmbRubro.Name = "cmbRubro";
            cmbRubro.Size = new Size(99, 23);
            cmbRubro.TabIndex = 24;
            // 
            // lblRubro
            // 
            lblRubro.AutoSize = true;
            lblRubro.Location = new Point(415, 132);
            lblRubro.Name = "lblRubro";
            lblRubro.Size = new Size(39, 15);
            lblRubro.TabIndex = 23;
            lblRubro.Text = "Rubro";
            // 
            // txtCodigo
            // 
            txtCodigo.Location = new Point(96, 219);
            txtCodigo.Name = "txtCodigo";
            txtCodigo.Size = new Size(100, 23);
            txtCodigo.TabIndex = 25;
            // 
            // txtCodigoBarra
            // 
            txtCodigoBarra.Location = new Point(302, 219);
            txtCodigoBarra.Name = "txtCodigoBarra";
            txtCodigoBarra.Size = new Size(100, 23);
            txtCodigoBarra.TabIndex = 26;
            // 
            // lblCodigo
            // 
            lblCodigo.AutoSize = true;
            lblCodigo.Location = new Point(35, 224);
            lblCodigo.Name = "lblCodigo";
            lblCodigo.Size = new Size(45, 15);
            lblCodigo.TabIndex = 27;
            lblCodigo.Text = "Codigo";
            // 
            // lblCodigoBarra
            // 
            lblCodigoBarra.AutoSize = true;
            lblCodigoBarra.Location = new Point(218, 224);
            lblCodigoBarra.Name = "lblCodigoBarra";
            lblCodigoBarra.Size = new Size(75, 15);
            lblCodigoBarra.TabIndex = 28;
            lblCodigoBarra.Text = "Codigo Barra";
            // 
            // chkIvaIncluido
            // 
            chkIvaIncluido.AutoSize = true;
            chkIvaIncluido.Location = new Point(593, 119);
            chkIvaIncluido.Name = "chkIvaIncluido";
            chkIvaIncluido.Size = new Size(172, 19);
            chkIvaIncluido.TabIndex = 29;
            chkIvaIncluido.Text = "Iva Incluido en Precio Final ";
            chkIvaIncluido.UseVisualStyleBackColor = true;
            // 
            // chkEsFraccionable
            // 
            chkEsFraccionable.AutoSize = true;
            chkEsFraccionable.Location = new Point(593, 144);
            chkEsFraccionable.Name = "chkEsFraccionable";
            chkEsFraccionable.Size = new Size(107, 19);
            chkEsFraccionable.TabIndex = 30;
            chkEsFraccionable.Text = "Es Fraccionable";
            chkEsFraccionable.UseVisualStyleBackColor = true;
            // 
            // chkControlPorLotes
            // 
            chkControlPorLotes.AutoSize = true;
            chkControlPorLotes.Location = new Point(593, 169);
            chkControlPorLotes.Name = "chkControlPorLotes";
            chkControlPorLotes.Size = new Size(160, 19);
            chkControlPorLotes.TabIndex = 31;
            chkControlPorLotes.Text = "Controlar Stock por Lotes";
            chkControlPorLotes.UseVisualStyleBackColor = true;
            chkControlPorLotes.CheckedChanged += chkControlPorLotes_CheckedChanged;
            // 
            // chkTieneVencimiento
            // 
            chkTieneVencimiento.AutoSize = true;
            chkTieneVencimiento.Location = new Point(593, 194);
            chkTieneVencimiento.Name = "chkTieneVencimiento";
            chkTieneVencimiento.Size = new Size(168, 19);
            chkTieneVencimiento.TabIndex = 32;
            chkTieneVencimiento.Text = "Producto con Vencimineto";
            chkTieneVencimiento.UseVisualStyleBackColor = true;
            // 
            // chkbProductoDiscontinuado
            // 
            chkbProductoDiscontinuado.AutoSize = true;
            chkbProductoDiscontinuado.Location = new Point(593, 247);
            chkbProductoDiscontinuado.Name = "chkbProductoDiscontinuado";
            chkbProductoDiscontinuado.Size = new Size(156, 19);
            chkbProductoDiscontinuado.TabIndex = 33;
            chkbProductoDiscontinuado.Text = "Producto Discontinuado";
            chkbProductoDiscontinuado.UseVisualStyleBackColor = true;
            // 
            // FProductoABM
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(784, 297);
            Controls.Add(chkbProductoDiscontinuado);
            Controls.Add(chkTieneVencimiento);
            Controls.Add(chkControlPorLotes);
            Controls.Add(chkEsFraccionable);
            Controls.Add(chkIvaIncluido);
            Controls.Add(lblCodigoBarra);
            Controls.Add(lblCodigo);
            Controls.Add(txtCodigoBarra);
            Controls.Add(txtCodigo);
            Controls.Add(cmbRubro);
            Controls.Add(lblRubro);
            Controls.Add(btnCategorias);
            Controls.Add(cmbMarca);
            Controls.Add(txtUnidadMedida);
            Controls.Add(txtMedida);
            Controls.Add(lblUnidadMedida);
            Controls.Add(lblMedida);
            Controls.Add(lblCategoria);
            Controls.Add(txtStock);
            Controls.Add(lblStock);
            Controls.Add(txtPrecioVenta);
            Controls.Add(txtPrecioCosto);
            Controls.Add(txtProducto);
            Controls.Add(lblPrecioVenta);
            Controls.Add(lblPrecioCosto);
            Controls.Add(lblMarca);
            Controls.Add(lblDescripcion);
            Name = "FProductoABM";
            Text = "FProductoABM";
            Controls.SetChildIndex(lblDescripcion, 0);
            Controls.SetChildIndex(lblMarca, 0);
            Controls.SetChildIndex(lblPrecioCosto, 0);
            Controls.SetChildIndex(lblPrecioVenta, 0);
            Controls.SetChildIndex(txtProducto, 0);
            Controls.SetChildIndex(txtPrecioCosto, 0);
            Controls.SetChildIndex(txtPrecioVenta, 0);
            Controls.SetChildIndex(lblStock, 0);
            Controls.SetChildIndex(txtStock, 0);
            Controls.SetChildIndex(lblCategoria, 0);
            Controls.SetChildIndex(lblMedida, 0);
            Controls.SetChildIndex(lblUnidadMedida, 0);
            Controls.SetChildIndex(txtMedida, 0);
            Controls.SetChildIndex(txtUnidadMedida, 0);
            Controls.SetChildIndex(cmbMarca, 0);
            Controls.SetChildIndex(btnCategorias, 0);
            Controls.SetChildIndex(lblRubro, 0);
            Controls.SetChildIndex(cmbRubro, 0);
            Controls.SetChildIndex(txtCodigo, 0);
            Controls.SetChildIndex(txtCodigoBarra, 0);
            Controls.SetChildIndex(lblCodigo, 0);
            Controls.SetChildIndex(lblCodigoBarra, 0);
            Controls.SetChildIndex(chkIvaIncluido, 0);
            Controls.SetChildIndex(chkEsFraccionable, 0);
            Controls.SetChildIndex(chkControlPorLotes, 0);
            Controls.SetChildIndex(chkTieneVencimiento, 0);
            Controls.SetChildIndex(chkbProductoDiscontinuado, 0);
            ((System.ComponentModel.ISupportInitialize)error).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblDescripcion;
        private Label lblMarca;
        private Label lblPrecioCosto;
        private Label lblPrecioVenta;
        private TextBox txtProducto;
        private TextBox txtPrecioCosto;
        private TextBox txtPrecioVenta;
        private Label lblStock;
        private TextBox txtStock;
        private Label lblCategoria;
        private Label lblMedida;
        private Label lblUnidadMedida;
        private TextBox txtMedida;
        private TextBox txtUnidadMedida;
        private ComboBox cmbMarca;
        private Button btnCategorias;
        private ComboBox cmbRubro;
        private Label lblRubro;
        private TextBox txtCodigo;
        private TextBox txtCodigoBarra;
        private Label lblCodigo;
        private Label lblCodigoBarra;
        private CheckBox chkIvaIncluido;
        private CheckBox chkEsFraccionable;
        private CheckBox chkControlPorLotes;
        private CheckBox chkTieneVencimiento;
        private CheckBox chkbProductoDiscontinuado;
    }
}