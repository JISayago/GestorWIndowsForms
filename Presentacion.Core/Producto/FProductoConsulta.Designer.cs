namespace Presentacion.Core.Producto
{
    partial class FProductoConsulta
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
            btnSeleccionarProducto = new Button();
            btnGestionStock = new Button();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Controls.Add(btnGestionStock);
            panel1.Controls.Add(btnSeleccionarProducto);
            panel1.Controls.SetChildIndex(btnSeleccionarProducto, 0);
            panel1.Controls.SetChildIndex(btnGestionStock, 0);
            // 
            // btnSeleccionarProducto
            // 
            btnSeleccionarProducto.Location = new Point(3, 238);
            btnSeleccionarProducto.Name = "btnSeleccionarProducto";
            btnSeleccionarProducto.Size = new Size(75, 44);
            btnSeleccionarProducto.TabIndex = 1;
            btnSeleccionarProducto.Text = "Seleccionar Producto";
            btnSeleccionarProducto.UseVisualStyleBackColor = true;
            btnSeleccionarProducto.Click += btnSeleccionarProducto_Click;
            // 
            // btnGestionStock
            // 
            btnGestionStock.Location = new Point(2, 311);
            btnGestionStock.Name = "btnGestionStock";
            btnGestionStock.Size = new Size(75, 72);
            btnGestionStock.TabIndex = 2;
            btnGestionStock.Text = "Gestión Stock";
            btnGestionStock.UseVisualStyleBackColor = true;
            //btnGestionStock.Click += btnGestionStock_Click;
            // 
            // FProductoConsulta
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1083, 561);
            Name = "FProductoConsulta";
            Text = "FProductoConsulta";
            Load += FProductoConsulta_Load;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnSeleccionarProducto;
        private Button btnGestionStock;
    }
}