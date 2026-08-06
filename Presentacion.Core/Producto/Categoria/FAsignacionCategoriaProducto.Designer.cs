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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FAsignacionCategoriaProducto));
            components = new System.ComponentModel.Container();
            txtBuscar = new TextBox();
            timerBusqueda = new System.Windows.Forms.Timer(components);
            dvgCategoriasProducto = new DataGridView();
            btnAceptar = new Button();
            ((System.ComponentModel.ISupportInitialize)error).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dvgCategoriasProducto).BeginInit();
            SuspendLayout();
            // 
            // txtBuscar
            // 
            txtBuscar.Location = new Point(12, 12);
            txtBuscar.Name = "txtBuscar";
            txtBuscar.PlaceholderText = "Buscar categoria...";
            txtBuscar.Size = new Size(308, 27);
            txtBuscar.TabIndex = 0;
            txtBuscar.TextChanged += txtBuscar_TextChanged;
            // 
            // timerBusqueda
            // 
            timerBusqueda.Interval = 350;
            timerBusqueda.Tick += timerBusqueda_Tick;
            // 
            // dvgCategoriasProducto
            // 
            dvgCategoriasProducto.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dvgCategoriasProducto.Location = new Point(12, 47);
            dvgCategoriasProducto.Name = "dvgCategoriasProducto";
            dvgCategoriasProducto.Size = new Size(308, 196);
            dvgCategoriasProducto.TabIndex = 2;
            // 
            // btnAceptar
            // 
            btnAceptar.Location = new Point(98, 254);
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
            ClientSize = new Size(335, 298);
            Controls.Add(btnAceptar);
            Controls.Add(dvgCategoriasProducto);
            Controls.Add(txtBuscar);
            ForeColor = Color.FromArgb(31, 26, 43);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "FAsignacionCategoriaProducto";
            Text = "Asignar Categorias";
            Load += FAsignacionCategoriaProducto_Load;
            ((System.ComponentModel.ISupportInitialize)error).EndInit();
            ((System.ComponentModel.ISupportInitialize)dvgCategoriasProducto).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtBuscar;
        private System.Windows.Forms.Timer timerBusqueda;
        private DataGridView dvgCategoriasProducto;
        private Button btnAceptar;
    }
}