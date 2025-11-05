namespace Presentacion.Core.Articulo.Marca
{
    partial class FMarcaConsulta
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
            btnSeleccionarMarca = new Button();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Controls.Add(btnSeleccionarMarca);
            panel1.Controls.SetChildIndex(btnSeleccionarMarca, 0);
            // 
            // btnSeleccionarMarca
            // 
            btnSeleccionarMarca.Location = new Point(3, 272);
            btnSeleccionarMarca.Name = "btnSeleccionarMarca";
            btnSeleccionarMarca.Size = new Size(75, 50);
            btnSeleccionarMarca.TabIndex = 1;
            btnSeleccionarMarca.Text = "Seleccionar Marca";
            btnSeleccionarMarca.UseVisualStyleBackColor = true;
            btnSeleccionarMarca.Click += btnSeleccionarMarca_Click;
            // 
            // FMarcaConsulta
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 561);
            Name = "FMarcaConsulta";
            Text = "FMarcaConsulta";
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnSeleccionarMarca;
    }
}