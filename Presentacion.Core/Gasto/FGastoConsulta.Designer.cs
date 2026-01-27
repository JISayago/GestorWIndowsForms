namespace Presentacion.Core.Gasto
{
    partial class FGastoConsulta
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
            btnAnularGasto = new Button();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Controls.Add(btnAnularGasto);
            panel1.Controls.SetChildIndex(btnAnularGasto, 0);
            // 
            // btnAnularGasto
            // 
            btnAnularGasto.Location = new Point(0, 234);
            btnAnularGasto.Name = "btnAnularGasto";
            btnAnularGasto.Size = new Size(75, 73);
            btnAnularGasto.TabIndex = 1;
            btnAnularGasto.Text = "Anular Gasto";
            btnAnularGasto.UseVisualStyleBackColor = true;
            btnAnularGasto.Click += btnAnularGasto_Click;
            // 
            // FGastoConsulta
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 561);
            Name = "FGastoConsulta";
            Text = "FGastoConsulta";
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnAnularGasto;
    }
}