namespace Presentacion.Core.Empleado.Rol
{
    partial class FAsignacionRolesEmpleados
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
            cbxEmpleado = new ComboBox();
            dgvRolesDisponibles = new DataGridView();
            dgvRolesAsignados = new DataGridView();
            btnAsignarRol = new Button();
            btnQuitarRol = new Button();
            btnActualizarRoles = new Button();
            ((System.ComponentModel.ISupportInitialize)error).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvRolesDisponibles).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvRolesAsignados).BeginInit();
            SuspendLayout();
            // 
            // cbxEmpleado
            // 
            cbxEmpleado.FormattingEnabled = true;
            cbxEmpleado.Location = new Point(212, 12);
            cbxEmpleado.Name = "cbxEmpleado";
            cbxEmpleado.Size = new Size(315, 23);
            cbxEmpleado.TabIndex = 0;
            cbxEmpleado.SelectedIndexChanged += cbxEmpleado_SelectedIndexChanged;
            // 
            // dgvRolesDisponibles
            // 
            dgvRolesDisponibles.BackgroundColor = SystemColors.Control;
            dgvRolesDisponibles.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvRolesDisponibles.Location = new Point(32, 78);
            dgvRolesDisponibles.Name = "dgvRolesDisponibles";
            dgvRolesDisponibles.Size = new Size(295, 271);
            dgvRolesDisponibles.TabIndex = 1;
            // 
            // dgvRolesAsignados
            // 
            dgvRolesAsignados.BackgroundColor = SystemColors.Control;
            dgvRolesAsignados.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvRolesAsignados.Location = new Point(446, 78);
            dgvRolesAsignados.Name = "dgvRolesAsignados";
            dgvRolesAsignados.Size = new Size(295, 271);
            dgvRolesAsignados.TabIndex = 2;
            // 
            // btnAsignarRol
            // 
            btnAsignarRol.Location = new Point(340, 110);
            btnAsignarRol.Name = "btnAsignarRol";
            btnAsignarRol.Size = new Size(94, 101);
            btnAsignarRol.TabIndex = 3;
            btnAsignarRol.Text = "Asignar Rol >";
            btnAsignarRol.UseVisualStyleBackColor = true;
            btnAsignarRol.Click += btnAsignarRol_Click;
            // 
            // btnQuitarRol
            // 
            btnQuitarRol.Location = new Point(340, 215);
            btnQuitarRol.Name = "btnQuitarRol";
            btnQuitarRol.Size = new Size(94, 101);
            btnQuitarRol.TabIndex = 4;
            btnQuitarRol.Text = "Quitar Rol <";
            btnQuitarRol.UseVisualStyleBackColor = true;
            btnQuitarRol.Click += btnQuitarRol_Click;
            // 
            // btnActualizarRoles
            // 
            btnActualizarRoles.Location = new Point(252, 390);
            btnActualizarRoles.Name = "btnActualizarRoles";
            btnActualizarRoles.Size = new Size(264, 48);
            btnActualizarRoles.TabIndex = 5;
            btnActualizarRoles.Text = "Actualizar nuevos roles";
            btnActualizarRoles.UseVisualStyleBackColor = true;
            btnActualizarRoles.Click += btnActualizarRoles_Click;
            // 
            // FAsignacionRolesEmpleados
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(btnActualizarRoles);
            Controls.Add(btnQuitarRol);
            Controls.Add(btnAsignarRol);
            Controls.Add(dgvRolesAsignados);
            Controls.Add(dgvRolesDisponibles);
            Controls.Add(cbxEmpleado);
            Name = "FAsignacionRolesEmpleados";
            Text = "FAsignacionRolesEmpleados";
            Load += FAsignacionRolesEmpleados_Load;
            ((System.ComponentModel.ISupportInitialize)error).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvRolesDisponibles).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvRolesAsignados).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private ComboBox cbxEmpleado;
        private DataGridView dgvRolesDisponibles;
        private DataGridView dgvRolesAsignados;
        private Button btnAsignarRol;
        private Button btnQuitarRol;
        private Button btnActualizarRoles;
    }
}