namespace Presentacion.Core.Venta.CancelacionVenta
{
    partial class FGrillaDeComprobantes
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
            lblTitulo = new Label();
            dgvComprobantes = new DataGridView();
            btnCancelar = new Button();
            btnCargar = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvComprobantes).BeginInit();
            SuspendLayout();
            // 
            // lblTitulo
            // 
            lblTitulo.AutoSize = true;
            lblTitulo.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblTitulo.Location = new Point(94, 9);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(618, 50);
            lblTitulo.TabIndex = 0;
            lblTitulo.Text = "Se encontraron múltiples ventas con el mismo número de comprobante.\r\n Por favor elija la que desea cancelar.";
            lblTitulo.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // dgvComprobantes
            // 
            dgvComprobantes.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvComprobantes.Location = new Point(94, 99);
            dgvComprobantes.Name = "dgvComprobantes";
            dgvComprobantes.Size = new Size(618, 205);
            dgvComprobantes.TabIndex = 1;
            // 
            // btnCancelar
            // 
            btnCancelar.Location = new Point(232, 340);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new Size(75, 23);
            btnCancelar.TabIndex = 2;
            btnCancelar.Text = "Cancelar";
            btnCancelar.UseVisualStyleBackColor = true;
            // 
            // btnCargar
            // 
            btnCargar.Location = new Point(455, 340);
            btnCargar.Name = "btnCargar";
            btnCargar.Size = new Size(75, 23);
            btnCargar.TabIndex = 3;
            btnCargar.Text = "Cargar";
            btnCargar.UseVisualStyleBackColor = true;
            btnCargar.Click += btnCargar_Click;
            // 
            // FGrillaDeComprobantes
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 382);
            Controls.Add(btnCargar);
            Controls.Add(btnCancelar);
            Controls.Add(dgvComprobantes);
            Controls.Add(lblTitulo);
            Name = "FGrillaDeComprobantes";
            Text = "FGrillaDeComprobantes";
            Load += FGrillaDeComprobantes_Load;
            ((System.ComponentModel.ISupportInitialize)dgvComprobantes).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblTitulo;
        private DataGridView dgvComprobantes;
        private Button btnCancelar;
        private Button btnCargar;
    }
}