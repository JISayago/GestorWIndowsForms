namespace Presentacion.Core.Producto.Categoria
{
    partial class FAsignacionCategoriaProducto
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
            dvgCategoriasProducto = new DataGridView();
            btnAgregarCategoria = new Button();
            btnQuitarCategoria = new Button();
            btnAceptar = new Button();
            ((System.ComponentModel.ISupportInitialize)error).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dvgCategoriasProducto).BeginInit();
            SuspendLayout();
            // 
            // dvgCategoriasProducto
            // 
            dvgCategoriasProducto.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dvgCategoriasProducto.Location = new Point(12, 12);
            dvgCategoriasProducto.Name = "dvgCategoriasProducto";
            dvgCategoriasProducto.Size = new Size(308, 222);
            dvgCategoriasProducto.TabIndex = 0;
            // 
            // btnAgregarCategoria
            // 
            btnAgregarCategoria.Location = new Point(12, 240);
            btnAgregarCategoria.Name = "btnAgregarCategoria";
            btnAgregarCategoria.Size = new Size(80, 32);
            btnAgregarCategoria.TabIndex = 1;
            btnAgregarCategoria.Text = "Agregar";
            btnAgregarCategoria.UseVisualStyleBackColor = true;
            // 
            // btnQuitarCategoria
            // 
            btnQuitarCategoria.Location = new Point(240, 240);
            btnQuitarCategoria.Name = "btnQuitarCategoria";
            btnQuitarCategoria.Size = new Size(80, 32);
            btnQuitarCategoria.TabIndex = 2;
            btnQuitarCategoria.Text = "Quitar";
            btnQuitarCategoria.UseVisualStyleBackColor = true;
            // 
            // btnAceptar
            // 
            btnAceptar.Location = new Point(98, 240);
            btnAceptar.Name = "btnAceptar";
            btnAceptar.Size = new Size(136, 32);
            btnAceptar.TabIndex = 3;
            btnAceptar.Text = "Aceptar";
            btnAceptar.UseVisualStyleBackColor = true;
            btnAceptar.Click += btnAceptar_Click;
            // 
            // FAsignacionCategoriaProducto
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(335, 291);
            Controls.Add(btnAceptar);
            Controls.Add(btnQuitarCategoria);
            Controls.Add(btnAgregarCategoria);
            Controls.Add(dvgCategoriasProducto);
            Name = "FAsignacionCategoriaProducto";
            Text = "FAsignacionCategoriaProducto";
            ((System.ComponentModel.ISupportInitialize)error).EndInit();
            ((System.ComponentModel.ISupportInitialize)dvgCategoriasProducto).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DataGridView dvgCategoriasProducto;
        private Button btnAgregarCategoria;
        private Button btnQuitarCategoria;
        private Button btnAceptar;
    }
}