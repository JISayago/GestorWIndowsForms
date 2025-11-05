namespace Presentacion.Core.Cliente
{
    partial class FClienteConsulta
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
            btnSeleccionarCliente = new Button();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Controls.Add(btnSeleccionarCliente);
            panel1.Controls.SetChildIndex(btnSeleccionarCliente, 0);
            // 
            // btnSeleccionarCliente
            // 
            btnSeleccionarCliente.Location = new Point(3, 236);
            btnSeleccionarCliente.Name = "btnSeleccionarCliente";
            btnSeleccionarCliente.Size = new Size(80, 58);
            btnSeleccionarCliente.TabIndex = 1;
            btnSeleccionarCliente.Text = "Seleccionar Cliente";
            btnSeleccionarCliente.UseVisualStyleBackColor = true;
            btnSeleccionarCliente.Click += btnSeleccionarCliente_Click;
            // 
            // FClienteConsulta
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 561);
            Name = "FClienteConsulta";
            Text = "FClienteConsulta";
            Load += FClienteConsulta_Load;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnSeleccionarCliente;
    }
}