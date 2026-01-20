namespace Presentacion.Core.Caja
{
    partial class FCajaConsulta
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
            dgvCajas = new DataGridView();
            dtpAbierta = new DateTimePicker();
            dtpCerrada = new DateTimePicker();
            label1 = new Label();
            label2 = new Label();
            btnFiltrar = new Button();
            btnClear = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvCajas).BeginInit();
            SuspendLayout();
            // 
            // dgvCajas
            // 
            dgvCajas.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvCajas.Location = new Point(12, 99);
            dgvCajas.Name = "dgvCajas";
            dgvCajas.Size = new Size(602, 254);
            dgvCajas.TabIndex = 0;
            // 
            // dtpAbierta
            // 
            dtpAbierta.Location = new Point(75, 15);
            dtpAbierta.Name = "dtpAbierta";
            dtpAbierta.Size = new Size(200, 23);
            dtpAbierta.TabIndex = 1;
            // 
            // dtpCerrada
            // 
            dtpCerrada.Location = new Point(414, 15);
            dtpCerrada.Name = "dtpCerrada";
            dtpCerrada.Size = new Size(200, 23);
            dtpCerrada.TabIndex = 2;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 21);
            label1.Name = "label1";
            label1.Size = new Size(57, 15);
            label1.TabIndex = 3;
            label1.Text = "Abierta el";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(348, 21);
            label2.Name = "label2";
            label2.Size = new Size(60, 15);
            label2.TabIndex = 4;
            label2.Text = "Cerrada el";
            // 
            // btnFiltrar
            // 
            btnFiltrar.Location = new Point(12, 61);
            btnFiltrar.Name = "btnFiltrar";
            btnFiltrar.Size = new Size(75, 23);
            btnFiltrar.TabIndex = 5;
            btnFiltrar.Text = "Filtrar";
            btnFiltrar.UseVisualStyleBackColor = true;
            btnFiltrar.Click += btnFiltrar_Click;
            // 
            // btnClear
            // 
            btnClear.Location = new Point(93, 61);
            btnClear.Name = "btnClear";
            btnClear.Size = new Size(75, 23);
            btnClear.TabIndex = 6;
            btnClear.Text = "Clear";
            btnClear.UseVisualStyleBackColor = true;
            btnClear.Click += btnClear_Click;
            // 
            // FCajaConsulta
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(627, 365);
            Controls.Add(btnClear);
            Controls.Add(dtpAbierta);
            Controls.Add(btnFiltrar);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(dtpCerrada);
            Controls.Add(dgvCajas);
            Name = "FCajaConsulta";
            Text = "FCajaConsulta";
            ((System.ComponentModel.ISupportInitialize)dgvCajas).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dgvCajas;
        private DateTimePicker dtpAbierta;
        private DateTimePicker dtpCerrada;
        private Label label1;
        private Label label2;
        private Button btnFiltrar;
        private Button btnClear;
    }
}