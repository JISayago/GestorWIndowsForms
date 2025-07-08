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
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Controls.Add(btnAsignacionRoles);
            panel1.Controls.SetChildIndex(btnAsignacionRoles, 0);
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
    }
}