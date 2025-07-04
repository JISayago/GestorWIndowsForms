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
            ((System.ComponentModel.ISupportInitialize)error).BeginInit();
            SuspendLayout();
            // 
            // cbxEmpleado
            // 
            cbxEmpleado.FormattingEnabled = true;
            cbxEmpleado.Location = new Point(121, 60);
            cbxEmpleado.Name = "cbxEmpleado";
            cbxEmpleado.Size = new Size(315, 23);
            cbxEmpleado.TabIndex = 0;
            // 
            // FAsignacionRolesEmpleados
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(cbxEmpleado);
            Name = "FAsignacionRolesEmpleados";
            Text = "FAsignacionRolesEmpleados";
            ((System.ComponentModel.ISupportInitialize)error).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private ComboBox cbxEmpleado;
    }
}