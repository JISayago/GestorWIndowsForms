namespace Presentacion.Core.Empleado.Rol.Permisos
{
    partial class FAsignacionPermisosRol
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
            btnActualizar = new Button();
            btnQuitarPersmisos = new Button();
            btnAsignarPermisos = new Button();
            dgvPermisosAsignadas = new DataGridView();
            dgvPermisosDisponibles = new DataGridView();
            cbxRol = new ComboBox();
            ((System.ComponentModel.ISupportInitialize)dgvPermisosAsignadas).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvPermisosDisponibles).BeginInit();
            SuspendLayout();
            // 
            // btnActualizar
            // 
            btnActualizar.Location = new Point(394, 337);
            btnActualizar.Name = "btnActualizar";
            btnActualizar.Size = new Size(264, 48);
            btnActualizar.TabIndex = 11;
            btnActualizar.Text = "Actualizar";
            btnActualizar.UseVisualStyleBackColor = true;
            btnActualizar.Click += btnActualizar_Click;
            // 
            // btnQuitarPersmisos
            // 
            btnQuitarPersmisos.Location = new Point(481, 213);
            btnQuitarPersmisos.Name = "btnQuitarPersmisos";
            btnQuitarPersmisos.Size = new Size(94, 101);
            btnQuitarPersmisos.TabIndex = 10;
            btnQuitarPersmisos.Text = "Quitar  <";
            btnQuitarPersmisos.UseVisualStyleBackColor = true;
            btnQuitarPersmisos.Click += btnQuitar_Click;
            // 
            // btnAsignarPermisos
            // 
            btnAsignarPermisos.Location = new Point(481, 89);
            btnAsignarPermisos.Name = "btnAsignarPermisos";
            btnAsignarPermisos.Size = new Size(94, 101);
            btnAsignarPermisos.TabIndex = 9;
            btnAsignarPermisos.Text = "Asignar  >";
            btnAsignarPermisos.UseVisualStyleBackColor = true;
            btnAsignarPermisos.Click += btnAsignar_Click;
            // 
            // dgvPermisosAsignadas
            // 
            dgvPermisosAsignadas.BackgroundColor = SystemColors.Control;
            dgvPermisosAsignadas.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvPermisosAsignadas.Location = new Point(593, 60);
            dgvPermisosAsignadas.Name = "dgvPermisosAsignadas";
            dgvPermisosAsignadas.Size = new Size(447, 271);
            dgvPermisosAsignadas.TabIndex = 8;
            // 
            // dgvPermisosDisponibles
            // 
            dgvPermisosDisponibles.BackgroundColor = SystemColors.Control;
            dgvPermisosDisponibles.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvPermisosDisponibles.Location = new Point(16, 60);
            dgvPermisosDisponibles.Name = "dgvPermisosDisponibles";
            dgvPermisosDisponibles.Size = new Size(447, 271);
            dgvPermisosDisponibles.TabIndex = 7;
            // 
            // cbxRol
            // 
            cbxRol.FormattingEnabled = true;
            cbxRol.Location = new Point(363, 31);
            cbxRol.Name = "cbxRol";
            cbxRol.Size = new Size(315, 23);
            cbxRol.TabIndex = 6;
            cbxRol.SelectedIndexChanged += cbxRol_SelectedIndexChanged;
            // 
            // FAsignacionPermisosRol
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1055, 414);
            Controls.Add(btnActualizar);
            Controls.Add(btnQuitarPersmisos);
            Controls.Add(btnAsignarPermisos);
            Controls.Add(dgvPermisosAsignadas);
            Controls.Add(dgvPermisosDisponibles);
            Controls.Add(cbxRol);
            Name = "FAsignacionPermisosRol";
            Text = "Asignacion Permisos de Roles";
            ((System.ComponentModel.ISupportInitialize)dgvPermisosAsignadas).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvPermisosDisponibles).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Button btnActualizar;
        private Button btnQuitarPersmisos;
        private Button btnAsignarPermisos;
        private DataGridView dgvPermisosAsignadas;
        private DataGridView dgvPermisosDisponibles;
        private ComboBox cbxRol;
    }
}