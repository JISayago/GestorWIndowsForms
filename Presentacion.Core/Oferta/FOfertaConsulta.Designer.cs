namespace Presentacion.Core.Oferta
{
    partial class FOfertaConsulta
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
            btnSeleccionarParaVenta = new Button();
            btnActivarDesactivarOferta = new Button();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Controls.Add(btnActivarDesactivarOferta);
            panel1.Controls.Add(btnSeleccionarParaVenta);
            panel1.Controls.SetChildIndex(btnSeleccionarParaVenta, 0);
            panel1.Controls.SetChildIndex(btnActivarDesactivarOferta, 0);
            // 
            // btnSeleccionarParaVenta
            // 
            btnSeleccionarParaVenta.Location = new Point(3, 241);
            btnSeleccionarParaVenta.Name = "btnSeleccionarParaVenta";
            btnSeleccionarParaVenta.Size = new Size(75, 85);
            btnSeleccionarParaVenta.TabIndex = 1;
            btnSeleccionarParaVenta.Text = "Seleccionar para venta";
            btnSeleccionarParaVenta.UseVisualStyleBackColor = true;
            btnSeleccionarParaVenta.Click += btnSeleccionarParaVenta_Click;
            // 
            // btnActivarDesactivarOferta
            // 
            btnActivarDesactivarOferta.Enabled = false;
            btnActivarDesactivarOferta.Location = new Point(3, 332);
            btnActivarDesactivarOferta.Name = "btnActivarDesactivarOferta";
            btnActivarDesactivarOferta.Size = new Size(75, 85);
            btnActivarDesactivarOferta.TabIndex = 2;
            btnActivarDesactivarOferta.Text = "Activar/Desactivar";
            btnActivarDesactivarOferta.UseVisualStyleBackColor = true;
            btnActivarDesactivarOferta.Visible = false;
            btnActivarDesactivarOferta.Click += btnActivarDesactivarOferta_Click;
            // 
            // FOfertaConsulta
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1052, 561);
            Name = "FOfertaConsulta";
            Text = "FOfertaConsulta";
            Load += FOfertaConsulta_Load;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnSeleccionarParaVenta;
        private Button btnActivarDesactivarOferta;
    }
}