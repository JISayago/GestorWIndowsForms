namespace Presentacion.Core.Empleado
{
    partial class FEmpleadoCrearUsuario
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
            btnCrearUsuario = new Button();
            txtNombreUsuario = new TextBox();
            cbxHabilitarEdicionNombre = new CheckBox();
            ((System.ComponentModel.ISupportInitialize)error).BeginInit();
            SuspendLayout();
            // 
            // btnCrearUsuario
            // 
            btnCrearUsuario.Location = new Point(231, 281);
            btnCrearUsuario.Name = "btnCrearUsuario";
            btnCrearUsuario.Size = new Size(294, 65);
            btnCrearUsuario.TabIndex = 0;
            btnCrearUsuario.Text = "Crear Usuario";
            btnCrearUsuario.UseVisualStyleBackColor = true;
            btnCrearUsuario.Click += btnCrearUsuario_Click;
            // 
            // txtNombreUsuario
            // 
            txtNombreUsuario.Location = new Point(231, 201);
            txtNombreUsuario.Name = "txtNombreUsuario";
            txtNombreUsuario.Size = new Size(294, 23);
            txtNombreUsuario.TabIndex = 1;
            // 
            // cbxHabilitarEdicionNombre
            // 
            cbxHabilitarEdicionNombre.AutoSize = true;
            cbxHabilitarEdicionNombre.Location = new Point(231, 166);
            cbxHabilitarEdicionNombre.Name = "cbxHabilitarEdicionNombre";
            cbxHabilitarEdicionNombre.Size = new Size(249, 19);
            cbxHabilitarEdicionNombre.TabIndex = 3;
            cbxHabilitarEdicionNombre.Text = "Asignar Nombre de Usuario Manualmente";
            cbxHabilitarEdicionNombre.UseVisualStyleBackColor = true;
            cbxHabilitarEdicionNombre.CheckedChanged += cbxHabilitarEdicionNombre_CheckedChanged;
            // 
            // FEmpleadoCrearUsuario
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(cbxHabilitarEdicionNombre);
            Controls.Add(txtNombreUsuario);
            Controls.Add(btnCrearUsuario);
            Name = "FEmpleadoCrearUsuario";
            Text = "FEmpleadoCrearUsuario";
            Load += FEmpleadoCrearUsuario_Load;
            ((System.ComponentModel.ISupportInitialize)error).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnCrearUsuario;
        private TextBox txtNombreUsuario;
        private CheckBox cbxHabilitarEdicionNombre;
    }
}