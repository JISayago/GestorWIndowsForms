namespace Presentacion.Core.Movimiento
{
    partial class FMovimientoConsulta
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
            lblAbrir = new Button();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Controls.Add(lblAbrir);
            panel1.Controls.SetChildIndex(lblAbrir, 0);
            // 
            // lblAbrir
            // 
            lblAbrir.Location = new Point(3, 241);
            lblAbrir.Name = "lblAbrir";
            lblAbrir.Size = new Size(75, 42);
            lblAbrir.TabIndex = 1;
            lblAbrir.Text = "Abrir";
            lblAbrir.UseVisualStyleBackColor = true;
            lblAbrir.Click += lblAbrir_Click;
            // 
            // FMovimientoConsulta
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 561);
            Name = "FMovimientoConsulta";
            Text = "FMovimientoConsulta";
            Load += FMovimientoConsulta_Load;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button lblAbrir;
    }
}