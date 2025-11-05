namespace Presentacion.Core.Producto.Rubro
{
    partial class FRubroConsulta
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
            btnRubroSeleccion = new Button();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Controls.Add(btnRubroSeleccion);
            panel1.Controls.SetChildIndex(btnRubroSeleccion, 0);
            // 
            // btnRubroSeleccion
            // 
            btnRubroSeleccion.Location = new Point(8, 256);
            btnRubroSeleccion.Name = "btnRubroSeleccion";
            btnRubroSeleccion.Size = new Size(75, 79);
            btnRubroSeleccion.TabIndex = 1;
            btnRubroSeleccion.Text = "Seleccionar Rubro";
            btnRubroSeleccion.UseVisualStyleBackColor = true;
            btnRubroSeleccion.Click += btnRubroSeleccion_Click;
            // 
            // FRubroConsulta
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 561);
            Name = "FRubroConsulta";
            Text = "FRubroConsulta";
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnRubroSeleccion;
    }
}