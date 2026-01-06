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
            dataGridView1 = new DataGridView();
            dtpAbierta = new DateTimePicker();
            dtpCerrada = new DateTimePicker();
            label1 = new Label();
            label2 = new Label();
            btnBuscar = new Button();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(12, 99);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.Size = new Size(602, 254);
            dataGridView1.TabIndex = 0;
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
            dtpCerrada.Location = new Point(75, 53);
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
            label2.Location = new Point(9, 59);
            label2.Name = "label2";
            label2.Size = new Size(60, 15);
            label2.TabIndex = 4;
            label2.Text = "Cerrada el";
            // 
            // btnBuscar
            // 
            btnBuscar.Location = new Point(281, 35);
            btnBuscar.Name = "btnBuscar";
            btnBuscar.Size = new Size(75, 23);
            btnBuscar.TabIndex = 5;
            btnBuscar.Text = "Buscar";
            btnBuscar.UseVisualStyleBackColor = true;
            // 
            // FCajaConsulta
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(627, 365);
            Controls.Add(btnBuscar);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(dtpCerrada);
            Controls.Add(dtpAbierta);
            Controls.Add(dataGridView1);
            Name = "FCajaConsulta";
            Text = "FCajaConsulta";
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dataGridView1;
        private DateTimePicker dtpAbierta;
        private DateTimePicker dtpCerrada;
        private Label label1;
        private Label label2;
        private Button btnBuscar;
    }
}