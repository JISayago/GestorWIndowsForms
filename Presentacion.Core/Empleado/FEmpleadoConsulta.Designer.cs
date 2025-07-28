namespace Presentacion.Core.Empleado
{
    partial class FEmpleadoConsulta
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
            btnAsignacionRoles = new Button();
            btnCrearUsuario = new Button();
            btnSeleccionarVendedor = new Button();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Controls.Add(btnSeleccionarVendedor);
            panel1.Controls.Add(btnCrearUsuario);
            panel1.Controls.Add(btnAsignacionRoles);
            panel1.Controls.SetChildIndex(btnAsignacionRoles, 0);
            panel1.Controls.SetChildIndex(btnCrearUsuario, 0);
            panel1.Controls.SetChildIndex(btnSeleccionarVendedor, 0);
            // 
            // btnAsignacionRoles
            // 
            btnAsignacionRoles.Location = new Point(3, 232);
            btnAsignacionRoles.Name = "btnAsignacionRoles";
            btnAsignacionRoles.Size = new Size(75, 57);
            btnAsignacionRoles.TabIndex = 1;
            btnAsignacionRoles.Text = "Asignación Roles";
            btnAsignacionRoles.UseVisualStyleBackColor = true;
            btnAsignacionRoles.Click += btnAsignacionRoles_Click;
            // 
            // btnCrearUsuario
            // 
            btnCrearUsuario.Location = new Point(3, 310);
            btnCrearUsuario.Name = "btnCrearUsuario";
            btnCrearUsuario.Size = new Size(75, 42);
            btnCrearUsuario.TabIndex = 2;
            btnCrearUsuario.Text = "Crear Usuario";
            btnCrearUsuario.UseVisualStyleBackColor = true;
            btnCrearUsuario.Click += btnCrearUsuario_Click;
            // 
            // btnSeleccionarVendedor
            // 
            btnSeleccionarVendedor.Enabled = false;
            btnSeleccionarVendedor.Location = new Point(3, 370);
            btnSeleccionarVendedor.Name = "btnSeleccionarVendedor";
            btnSeleccionarVendedor.Size = new Size(75, 55);
            btnSeleccionarVendedor.TabIndex = 3;
            btnSeleccionarVendedor.Text = "Seleccionar Vendedor";
            btnSeleccionarVendedor.UseVisualStyleBackColor = true;
            btnSeleccionarVendedor.Visible = false;
            btnSeleccionarVendedor.Click += btnSeleccionarVendedor_Click;
            // 
            // FEmpleadoConsulta
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 552);
            Name = "FEmpleadoConsulta";
            Text = "FEmpleadoConsulta";
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnAsignacionRoles;
        private Button btnCrearUsuario;
        private Button btnSeleccionarVendedor;
    }
}