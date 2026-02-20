namespace Presentacion.Core.Producto
{
    partial class FGestionStock
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
            lblTexto = new Label();
            btnCancelar = new Button();
            rdbAgregar = new RadioButton();
            rdbQuitar = new RadioButton();
            lblDetalle = new Label();
            lblMotivo = new Label();
            txtCantidad = new TextBox();
            txtMotivo = new TextBox();
            btnAccion = new Button();
            lblProductoCargado = new Label();
            lblProducto = new Label();
            SuspendLayout();
            // 
            // lblTexto
            // 
            lblTexto.AutoSize = true;
            lblTexto.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTexto.Location = new Point(143, 39);
            lblTexto.Name = "lblTexto";
            lblTexto.Size = new Size(320, 30);
            lblTexto.TabIndex = 0;
            lblTexto.Text = "¿Desea Agregar o Quitar Stock?";
            // 
            // btnCancelar
            // 
            btnCancelar.Location = new Point(375, 233);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new Size(164, 50);
            btnCancelar.TabIndex = 3;
            btnCancelar.Text = "Cancelar";
            btnCancelar.UseVisualStyleBackColor = true;
            btnCancelar.Click += btnCancelar_Click;
            // 
            // rdbAgregar
            // 
            rdbAgregar.AutoSize = true;
            rdbAgregar.Location = new Point(125, 83);
            rdbAgregar.Name = "rdbAgregar";
            rdbAgregar.Size = new Size(99, 19);
            rdbAgregar.TabIndex = 4;
            rdbAgregar.TabStop = true;
            rdbAgregar.Text = "Agregar Stock";
            rdbAgregar.UseVisualStyleBackColor = true;
            rdbAgregar.CheckedChanged += rdbAgregar_CheckedChanged;
            // 
            // rdbQuitar
            // 
            rdbQuitar.AutoSize = true;
            rdbQuitar.Location = new Point(360, 83);
            rdbQuitar.Name = "rdbQuitar";
            rdbQuitar.Size = new Size(90, 19);
            rdbQuitar.TabIndex = 5;
            rdbQuitar.TabStop = true;
            rdbQuitar.Text = "Quitar Stock";
            rdbQuitar.UseVisualStyleBackColor = true;
            rdbQuitar.CheckedChanged += rdbQuitar_CheckedChanged;
            // 
            // lblDetalle
            // 
            lblDetalle.AutoSize = true;
            lblDetalle.Location = new Point(140, 133);
            lblDetalle.Name = "lblDetalle";
            lblDetalle.Size = new Size(43, 15);
            lblDetalle.TabIndex = 6;
            lblDetalle.Text = "Monto";
            // 
            // lblMotivo
            // 
            lblMotivo.AutoSize = true;
            lblMotivo.Location = new Point(140, 183);
            lblMotivo.Name = "lblMotivo";
            lblMotivo.Size = new Size(45, 15);
            lblMotivo.TabIndex = 7;
            lblMotivo.Text = "Motivo";
            // 
            // txtCantidad
            // 
            txtCantidad.Location = new Point(143, 151);
            txtCantidad.Name = "txtCantidad";
            txtCantidad.Size = new Size(100, 23);
            txtCantidad.TabIndex = 8;
            // 
            // txtMotivo
            // 
            txtMotivo.Location = new Point(143, 201);
            txtMotivo.Name = "txtMotivo";
            txtMotivo.Size = new Size(396, 23);
            txtMotivo.TabIndex = 9;
            // 
            // btnAccion
            // 
            btnAccion.Location = new Point(140, 233);
            btnAccion.Name = "btnAccion";
            btnAccion.Size = new Size(164, 50);
            btnAccion.TabIndex = 10;
            btnAccion.Text = "Acción";
            btnAccion.UseVisualStyleBackColor = true;
            btnAccion.Click += btnAccion_Click;
            // 
            // lblProductoCargado
            // 
            lblProductoCargado.AutoSize = true;
            lblProductoCargado.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblProductoCargado.Location = new Point(56, 9);
            lblProductoCargado.Name = "lblProductoCargado";
            lblProductoCargado.Size = new Size(248, 30);
            lblProductoCargado.TabIndex = 11;
            lblProductoCargado.Text = "Producto seleccionado: ";
            // 
            // lblProducto
            // 
            lblProducto.AutoSize = true;
            lblProducto.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblProducto.ForeColor = Color.SeaGreen;
            lblProducto.Location = new Point(291, 9);
            lblProducto.Name = "lblProducto";
            lblProducto.Size = new Size(104, 30);
            lblProducto.TabIndex = 12;
            lblProducto.Text = "Producto";
            // 
            // FGestionStock
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(624, 295);
            Controls.Add(lblProducto);
            Controls.Add(lblProductoCargado);
            Controls.Add(btnAccion);
            Controls.Add(txtMotivo);
            Controls.Add(txtCantidad);
            Controls.Add(lblMotivo);
            Controls.Add(lblDetalle);
            Controls.Add(rdbQuitar);
            Controls.Add(rdbAgregar);
            Controls.Add(btnCancelar);
            Controls.Add(lblTexto);
            Name = "FGestionStock";
            Text = "FGestionStock";
            Load += FGestionStock_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblTexto;
        private Button btnCancelar;
        private RadioButton rdbAgregar;
        private RadioButton rdbQuitar;
        private Label lblDetalle;
        private Label lblMotivo;
        private TextBox txtCantidad;
        private TextBox txtMotivo;
        private Button btnAccion;
        private Label lblProductoCargado;
        private Label lblProducto;
    }
}