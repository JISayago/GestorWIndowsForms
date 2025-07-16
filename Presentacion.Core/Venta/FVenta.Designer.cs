namespace Presentacion.Core.Venta
{
    partial class FVenta
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
            lblHoy = new Label();
            lblFechaHoy = new Label();
            label1 = new Label();
            lblUsuario = new Label();
            lblLocalAsignado = new Label();
            lblLocal = new Label();
            button1 = new Button();
            button2 = new Button();
            txtCliente = new TextBox();
            btnCargarCliente = new Button();
            cbxConsumidorFinal = new CheckBox();
            ((System.ComponentModel.ISupportInitialize)error).BeginInit();
            SuspendLayout();
            // 
            // lblHoy
            // 
            lblHoy.AutoSize = true;
            lblHoy.Location = new Point(12, 9);
            lblHoy.Name = "lblHoy";
            lblHoy.Size = new Size(32, 15);
            lblHoy.TabIndex = 0;
            lblHoy.Text = "Hoy:";
            // 
            // lblFechaHoy
            // 
            lblFechaHoy.AutoSize = true;
            lblFechaHoy.Location = new Point(49, 10);
            lblFechaHoy.Name = "lblFechaHoy";
            lblFechaHoy.Size = new Size(53, 15);
            lblFechaHoy.TabIndex = 1;
            lblFechaHoy.Text = "00/00/00";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(521, 9);
            label1.Name = "label1";
            label1.Size = new Size(93, 15);
            label1.TabIndex = 3;
            label1.Text = "UsuarioLogeado";
            // 
            // lblUsuario
            // 
            lblUsuario.AutoSize = true;
            lblUsuario.Location = new Point(465, 9);
            lblUsuario.Name = "lblUsuario";
            lblUsuario.Size = new Size(50, 15);
            lblUsuario.TabIndex = 2;
            lblUsuario.Text = "Usuario:";
            // 
            // lblLocalAsignado
            // 
            lblLocalAsignado.AutoSize = true;
            lblLocalAsignado.Location = new Point(171, 9);
            lblLocalAsignado.Name = "lblLocalAsignado";
            lblLocalAsignado.Size = new Size(88, 15);
            lblLocalAsignado.TabIndex = 5;
            lblLocalAsignado.Text = "Local Asignado";
            lblLocalAsignado.Click += label2_Click;
            // 
            // lblLocal
            // 
            lblLocal.AutoSize = true;
            lblLocal.Location = new Point(135, 9);
            lblLocal.Name = "lblLocal";
            lblLocal.Size = new Size(38, 15);
            lblLocal.TabIndex = 4;
            lblLocal.Text = "Local:";
            // 
            // button1
            // 
            button1.Location = new Point(1184, 5);
            button1.Name = "button1";
            button1.Size = new Size(161, 23);
            button1.TabIndex = 6;
            button1.Text = "Ultimas Ventas";
            button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            button2.Location = new Point(1362, 5);
            button2.Name = "button2";
            button2.Size = new Size(161, 23);
            button2.TabIndex = 7;
            button2.Text = "Cancelación / Devolución";
            button2.UseVisualStyleBackColor = true;
            // 
            // txtCliente
            // 
            txtCliente.Location = new Point(191, 46);
            txtCliente.Name = "txtCliente";
            txtCliente.Size = new Size(423, 23);
            txtCliente.TabIndex = 8;
            // 
            // btnCargarCliente
            // 
            btnCargarCliente.Location = new Point(12, 46);
            btnCargarCliente.Name = "btnCargarCliente";
            btnCargarCliente.Size = new Size(161, 23);
            btnCargarCliente.TabIndex = 9;
            btnCargarCliente.Text = "Cargar Cliente";
            btnCargarCliente.UseVisualStyleBackColor = true;
            // 
            // cbxConsumidorFinal
            // 
            cbxConsumidorFinal.AutoSize = true;
            cbxConsumidorFinal.Location = new Point(628, 50);
            cbxConsumidorFinal.Name = "cbxConsumidorFinal";
            cbxConsumidorFinal.Size = new Size(130, 19);
            cbxConsumidorFinal.TabIndex = 10;
            cbxConsumidorFinal.Text = "Es consumidor final";
            cbxConsumidorFinal.UseVisualStyleBackColor = true;
            // 
            // FVenta
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1535, 495);
            Controls.Add(cbxConsumidorFinal);
            Controls.Add(btnCargarCliente);
            Controls.Add(txtCliente);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(lblLocalAsignado);
            Controls.Add(lblLocal);
            Controls.Add(label1);
            Controls.Add(lblUsuario);
            Controls.Add(lblFechaHoy);
            Controls.Add(lblHoy);
            Name = "FVenta";
            Text = "FVenta";
            ((System.ComponentModel.ISupportInitialize)error).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblHoy;
        private Label lblFechaHoy;
        private Label label1;
        private Label lblUsuario;
        private Label lblLocalAsignado;
        private Label lblLocal;
        private Button button1;
        private Button button2;
        private TextBox txtCliente;
        private Button btnCargarCliente;
        private CheckBox cbxConsumidorFinal;
    }
}