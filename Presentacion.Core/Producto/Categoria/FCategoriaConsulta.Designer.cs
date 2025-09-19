namespace Presentacion.Core.Categoria
{
    partial class FCategoriaConsulta
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
            btnSeleccionarCategoria = new Button();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Controls.Add(btnSeleccionarCategoria);
            panel1.Controls.SetChildIndex(btnSeleccionarCategoria, 0);
            // 
            // btnSeleccionarCategoria
            // 
            btnSeleccionarCategoria.Location = new Point(3, 259);
            btnSeleccionarCategoria.Name = "btnSeleccionarCategoria";
            btnSeleccionarCategoria.Size = new Size(75, 78);
            btnSeleccionarCategoria.TabIndex = 1;
            btnSeleccionarCategoria.Text = "Seleccionar Categoria";
            btnSeleccionarCategoria.UseVisualStyleBackColor = true;
            btnSeleccionarCategoria.Click += btnSeleccionarCategoria_Click;
            // 
            // FCategoriaConsulta
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 561);
            Name = "FCategoriaConsulta";
            Text = "FCategoriaConsulta";
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnSeleccionarCategoria;
    }
}