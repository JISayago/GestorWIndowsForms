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
            btnQuitar = new Button();
            btnAsignar = new Button();
            dgvAsignados = new DataGridView();
            dgvDisponibles = new DataGridView();
            cbxRoles = new ComboBox();
            ((System.ComponentModel.ISupportInitialize)dgvAsignados).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvDisponibles).BeginInit();
            SuspendLayout();
            // 
            // btnActualizar
            // 
            btnActualizar.Location = new Point(266, 390);
            btnActualizar.Name = "btnActualizar";
            btnActualizar.Size = new Size(264, 48);
            btnActualizar.TabIndex = 11;
            btnActualizar.Text = "Actualizar";
            btnActualizar.UseVisualStyleBackColor = true;
            // 
            // btnQuitar
            // 
            btnQuitar.Location = new Point(354, 215);
            btnQuitar.Name = "btnQuitar";
            btnQuitar.Size = new Size(94, 101);
            btnQuitar.TabIndex = 10;
            btnQuitar.Text = "Quitar  <";
            btnQuitar.UseVisualStyleBackColor = true;
            // 
            // btnAsignar
            // 
            btnAsignar.Location = new Point(354, 110);
            btnAsignar.Name = "btnAsignar";
            btnAsignar.Size = new Size(94, 101);
            btnAsignar.TabIndex = 9;
            btnAsignar.Text = "Asignar  >";
            btnAsignar.UseVisualStyleBackColor = true;
            // 
            // dgvAsignados
            // 
            dgvAsignados.BackgroundColor = SystemColors.Control;
            dgvAsignados.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvAsignados.Location = new Point(460, 78);
            dgvAsignados.Name = "dgvAsignados";
            dgvAsignados.Size = new Size(295, 271);
            dgvAsignados.TabIndex = 8;
            // 
            // dgvDisponibles
            // 
            dgvDisponibles.BackgroundColor = SystemColors.Control;
            dgvDisponibles.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvDisponibles.Location = new Point(46, 78);
            dgvDisponibles.Name = "dgvDisponibles";
            dgvDisponibles.Size = new Size(295, 271);
            dgvDisponibles.TabIndex = 7;
            // 
            // cbxRoles
            // 
            cbxRoles.FormattingEnabled = true;
            cbxRoles.Location = new Point(226, 12);
            cbxRoles.Name = "cbxRoles";
            cbxRoles.Size = new Size(315, 23);
            cbxRoles.TabIndex = 6;
            // 
            // FAsignacionPermisosRol
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(btnActualizar);
            Controls.Add(btnQuitar);
            Controls.Add(btnAsignar);
            Controls.Add(dgvAsignados);
            Controls.Add(dgvDisponibles);
            Controls.Add(cbxRoles);
            Name = "FAsignacionPermisosRol";
            Text = "FAsignacionPermisosRol";
            ((System.ComponentModel.ISupportInitialize)dgvAsignados).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvDisponibles).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Button btnActualizar;
        private Button btnQuitar;
        private Button btnAsignar;
        private DataGridView dgvAsignados;
        private DataGridView dgvDisponibles;
        private ComboBox cbxRoles;
    }
}