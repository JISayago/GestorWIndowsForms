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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FAsignacionPermisosRol));
            tlpRoot = new TableLayoutPanel();
            tlpHeader = new TableLayoutPanel();
            lblTitulo = new Label();
            lblRol = new Label();
            cbxRol = new ComboBox();
            lblBuscar = new Label();
            txtBuscar = new TextBox();
            tlpListas = new TableLayoutPanel();
            tlpDisponibles = new TableLayoutPanel();
            lblDisponibles = new Label();
            dgvPermisosDisponibles = new DataGridView();
            pnlBotonesMedio = new Panel();
            btnAsignarPermisos = new Button();
            btnQuitarPersmisos = new Button();
            tlpAsignados = new TableLayoutPanel();
            lblAsignados = new Label();
            dgvPermisosAsignadas = new DataGridView();
            pnlFooter = new Panel();
            btnSalir = new Button();
            btnActualizar = new Button();
            ((System.ComponentModel.ISupportInitialize)error).BeginInit();
            tlpRoot.SuspendLayout();
            tlpHeader.SuspendLayout();
            tlpListas.SuspendLayout();
            tlpDisponibles.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvPermisosDisponibles).BeginInit();
            pnlBotonesMedio.SuspendLayout();
            tlpAsignados.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvPermisosAsignadas).BeginInit();
            pnlFooter.SuspendLayout();
            SuspendLayout();
            // 
            // tlpRoot
            // 
            tlpRoot.ColumnCount = 1;
            tlpRoot.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpRoot.Controls.Add(tlpHeader, 0, 0);
            tlpRoot.Controls.Add(tlpListas, 0, 1);
            tlpRoot.Controls.Add(pnlFooter, 0, 2);
            tlpRoot.Dock = DockStyle.Fill;
            tlpRoot.Location = new Point(0, 0);
            tlpRoot.Name = "tlpRoot";
            tlpRoot.Padding = new Padding(16, 12, 16, 12);
            tlpRoot.RowCount = 3;
            tlpRoot.RowStyles.Add(new RowStyle(SizeType.Absolute, 118F));
            tlpRoot.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpRoot.RowStyles.Add(new RowStyle(SizeType.Absolute, 64F));
            tlpRoot.Size = new Size(1103, 600);
            tlpRoot.TabIndex = 0;
            // 
            // tlpHeader
            // 
            tlpHeader.ColumnCount = 2;
            tlpHeader.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 70F));
            tlpHeader.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpHeader.Controls.Add(lblTitulo, 0, 0);
            tlpHeader.Controls.Add(lblRol, 0, 1);
            tlpHeader.Controls.Add(cbxRol, 1, 1);
            tlpHeader.Controls.Add(lblBuscar, 0, 2);
            tlpHeader.Controls.Add(txtBuscar, 1, 2);
            tlpHeader.Dock = DockStyle.Fill;
            tlpHeader.Location = new Point(19, 15);
            tlpHeader.Name = "tlpHeader";
            tlpHeader.RowCount = 3;
            tlpHeader.RowStyles.Add(new RowStyle(SizeType.Absolute, 34F));
            tlpHeader.RowStyles.Add(new RowStyle(SizeType.Absolute, 36F));
            tlpHeader.RowStyles.Add(new RowStyle(SizeType.Absolute, 36F));
            tlpHeader.Size = new Size(1065, 112);
            tlpHeader.TabIndex = 0;
            // 
            // lblTitulo
            // 
            tlpHeader.SetColumnSpan(lblTitulo, 2);
            lblTitulo.Dock = DockStyle.Fill;
            lblTitulo.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblTitulo.Location = new Point(3, 0);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(1059, 34);
            lblTitulo.TabIndex = 0;
            lblTitulo.Text = "Asignación de permisos al rol";
            lblTitulo.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblRol
            // 
            lblRol.Dock = DockStyle.Fill;
            lblRol.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblRol.Location = new Point(3, 34);
            lblRol.Name = "lblRol";
            lblRol.Size = new Size(64, 36);
            lblRol.TabIndex = 1;
            lblRol.Text = "Rol:";
            lblRol.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // cbxRol
            // 
            cbxRol.Dock = DockStyle.Fill;
            cbxRol.DropDownStyle = ComboBoxStyle.DropDownList;
            cbxRol.Font = new Font("Segoe UI", 10F);
            cbxRol.FormattingEnabled = true;
            cbxRol.Location = new Point(70, 38);
            cbxRol.Margin = new Padding(0, 4, 0, 4);
            cbxRol.Name = "cbxRol";
            cbxRol.Size = new Size(995, 25);
            cbxRol.TabIndex = 2;
            cbxRol.SelectedIndexChanged += cbxRol_SelectedIndexChanged;
            // 
            // lblBuscar
            // 
            lblBuscar.Dock = DockStyle.Fill;
            lblBuscar.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblBuscar.Location = new Point(3, 70);
            lblBuscar.Name = "lblBuscar";
            lblBuscar.Size = new Size(64, 42);
            lblBuscar.TabIndex = 3;
            lblBuscar.Text = "Buscar:";
            lblBuscar.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // txtBuscar
            // 
            txtBuscar.BorderStyle = BorderStyle.FixedSingle;
            txtBuscar.Dock = DockStyle.Fill;
            txtBuscar.Font = new Font("Segoe UI", 10F);
            txtBuscar.Location = new Point(70, 74);
            txtBuscar.Margin = new Padding(0, 4, 0, 4);
            txtBuscar.Name = "txtBuscar";
            txtBuscar.PlaceholderText = "Filtrar por código o descripción...";
            txtBuscar.Size = new Size(995, 25);
            txtBuscar.TabIndex = 4;
            txtBuscar.TextChanged += txtBuscar_TextChanged;
            // 
            // tlpListas
            // 
            tlpListas.ColumnCount = 3;
            tlpListas.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tlpListas.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 112F));
            tlpListas.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tlpListas.Controls.Add(tlpDisponibles, 0, 0);
            tlpListas.Controls.Add(pnlBotonesMedio, 1, 0);
            tlpListas.Controls.Add(tlpAsignados, 2, 0);
            tlpListas.Dock = DockStyle.Fill;
            tlpListas.Location = new Point(19, 133);
            tlpListas.Name = "tlpListas";
            tlpListas.RowCount = 1;
            tlpListas.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpListas.Size = new Size(1065, 388);
            tlpListas.TabIndex = 1;
            // 
            // tlpDisponibles
            // 
            tlpDisponibles.ColumnCount = 1;
            tlpDisponibles.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpDisponibles.Controls.Add(lblDisponibles, 0, 0);
            tlpDisponibles.Controls.Add(dgvPermisosDisponibles, 0, 1);
            tlpDisponibles.Dock = DockStyle.Fill;
            tlpDisponibles.Location = new Point(0, 0);
            tlpDisponibles.Margin = new Padding(0, 0, 8, 0);
            tlpDisponibles.Name = "tlpDisponibles";
            tlpDisponibles.RowCount = 2;
            tlpDisponibles.RowStyles.Add(new RowStyle(SizeType.Absolute, 28F));
            tlpDisponibles.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpDisponibles.Size = new Size(468, 388);
            tlpDisponibles.TabIndex = 0;
            // 
            // lblDisponibles
            // 
            lblDisponibles.Dock = DockStyle.Fill;
            lblDisponibles.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblDisponibles.Location = new Point(3, 0);
            lblDisponibles.Name = "lblDisponibles";
            lblDisponibles.Size = new Size(462, 28);
            lblDisponibles.TabIndex = 0;
            lblDisponibles.Text = "Permisos disponibles";
            lblDisponibles.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // dgvPermisosDisponibles
            // 
            dgvPermisosDisponibles.AllowUserToAddRows = false;
            dgvPermisosDisponibles.AllowUserToDeleteRows = false;
            dgvPermisosDisponibles.AllowUserToResizeRows = false;
            dgvPermisosDisponibles.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvPermisosDisponibles.BackgroundColor = SystemColors.Control;
            dgvPermisosDisponibles.ColumnHeadersHeight = 34;
            dgvPermisosDisponibles.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgvPermisosDisponibles.Dock = DockStyle.Fill;
            dgvPermisosDisponibles.Location = new Point(3, 31);
            dgvPermisosDisponibles.MultiSelect = false;
            dgvPermisosDisponibles.Name = "dgvPermisosDisponibles";
            dgvPermisosDisponibles.ReadOnly = true;
            dgvPermisosDisponibles.RowHeadersVisible = false;
            dgvPermisosDisponibles.RowTemplate.Height = 30;
            dgvPermisosDisponibles.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvPermisosDisponibles.Size = new Size(462, 354);
            dgvPermisosDisponibles.TabIndex = 1;
            dgvPermisosDisponibles.CellDoubleClick += dgvPermisosDisponibles_CellDoubleClick;
            // 
            // pnlBotonesMedio
            // 
            pnlBotonesMedio.Controls.Add(btnAsignarPermisos);
            pnlBotonesMedio.Controls.Add(btnQuitarPersmisos);
            pnlBotonesMedio.Dock = DockStyle.Fill;
            pnlBotonesMedio.Location = new Point(476, 0);
            pnlBotonesMedio.Margin = new Padding(0);
            pnlBotonesMedio.Name = "pnlBotonesMedio";
            pnlBotonesMedio.Size = new Size(112, 388);
            pnlBotonesMedio.TabIndex = 1;
            // 
            // btnAsignarPermisos
            // 
            btnAsignarPermisos.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnAsignarPermisos.Location = new Point(10, 120);
            btnAsignarPermisos.Name = "btnAsignarPermisos";
            btnAsignarPermisos.Size = new Size(92, 48);
            btnAsignarPermisos.TabIndex = 0;
            btnAsignarPermisos.Text = "Asignar  >";
            btnAsignarPermisos.UseVisualStyleBackColor = false;
            btnAsignarPermisos.Click += btnAsignar_Click;
            // 
            // btnQuitarPersmisos
            // 
            btnQuitarPersmisos.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnQuitarPersmisos.Location = new Point(10, 184);
            btnQuitarPersmisos.Name = "btnQuitarPersmisos";
            btnQuitarPersmisos.Size = new Size(92, 48);
            btnQuitarPersmisos.TabIndex = 1;
            btnQuitarPersmisos.Text = "<  Quitar";
            btnQuitarPersmisos.UseVisualStyleBackColor = false;
            btnQuitarPersmisos.Click += btnQuitar_Click;
            // 
            // tlpAsignados
            // 
            tlpAsignados.ColumnCount = 1;
            tlpAsignados.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpAsignados.Controls.Add(lblAsignados, 0, 0);
            tlpAsignados.Controls.Add(dgvPermisosAsignadas, 0, 1);
            tlpAsignados.Dock = DockStyle.Fill;
            tlpAsignados.Location = new Point(596, 0);
            tlpAsignados.Margin = new Padding(8, 0, 0, 0);
            tlpAsignados.Name = "tlpAsignados";
            tlpAsignados.RowCount = 2;
            tlpAsignados.RowStyles.Add(new RowStyle(SizeType.Absolute, 28F));
            tlpAsignados.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpAsignados.Size = new Size(469, 388);
            tlpAsignados.TabIndex = 2;
            // 
            // lblAsignados
            // 
            lblAsignados.Dock = DockStyle.Fill;
            lblAsignados.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblAsignados.Location = new Point(3, 0);
            lblAsignados.Name = "lblAsignados";
            lblAsignados.Size = new Size(463, 28);
            lblAsignados.TabIndex = 0;
            lblAsignados.Text = "Permisos asignados";
            lblAsignados.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // dgvPermisosAsignadas
            // 
            dgvPermisosAsignadas.AllowUserToAddRows = false;
            dgvPermisosAsignadas.AllowUserToDeleteRows = false;
            dgvPermisosAsignadas.AllowUserToResizeRows = false;
            dgvPermisosAsignadas.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvPermisosAsignadas.BackgroundColor = SystemColors.Control;
            dgvPermisosAsignadas.ColumnHeadersHeight = 34;
            dgvPermisosAsignadas.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgvPermisosAsignadas.Dock = DockStyle.Fill;
            dgvPermisosAsignadas.Location = new Point(3, 31);
            dgvPermisosAsignadas.MultiSelect = false;
            dgvPermisosAsignadas.Name = "dgvPermisosAsignadas";
            dgvPermisosAsignadas.ReadOnly = true;
            dgvPermisosAsignadas.RowHeadersVisible = false;
            dgvPermisosAsignadas.RowTemplate.Height = 30;
            dgvPermisosAsignadas.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvPermisosAsignadas.Size = new Size(463, 354);
            dgvPermisosAsignadas.TabIndex = 1;
            dgvPermisosAsignadas.CellDoubleClick += dgvPermisosAsignadas_CellDoubleClick;
            // 
            // pnlFooter
            // 
            pnlFooter.Controls.Add(btnSalir);
            pnlFooter.Controls.Add(btnActualizar);
            pnlFooter.Dock = DockStyle.Fill;
            pnlFooter.Location = new Point(19, 527);
            pnlFooter.Name = "pnlFooter";
            pnlFooter.Size = new Size(1065, 58);
            pnlFooter.TabIndex = 2;
            // 
            // btnSalir
            // 
            btnSalir.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnSalir.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnSalir.Location = new Point(1751, 10);
            btnSalir.Name = "btnSalir";
            btnSalir.Size = new Size(120, 40);
            btnSalir.TabIndex = 1;
            btnSalir.Text = "Salir (Esc)";
            btnSalir.UseVisualStyleBackColor = false;
            btnSalir.Click += btnSalir_Click;
            // 
            // btnActualizar
            // 
            btnActualizar.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnActualizar.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnActualizar.Location = new Point(446, 3);
            btnActualizar.Name = "btnActualizar";
            btnActualizar.Size = new Size(170, 52);
            btnActualizar.TabIndex = 0;
            btnActualizar.Text = "Guardar cambios";
            btnActualizar.UseVisualStyleBackColor = false;
            btnActualizar.Click += btnActualizar_Click;
            // 
            // FAsignacionPermisosRol
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1103, 600);
            Controls.Add(tlpRoot);
            ForeColor = Color.FromArgb(31, 26, 43);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MinimumSize = new Size(960, 560);
            Name = "FAsignacionPermisosRol";
            Text = "Asignación de permisos";
            ((System.ComponentModel.ISupportInitialize)error).EndInit();
            tlpRoot.ResumeLayout(false);
            tlpHeader.ResumeLayout(false);
            tlpHeader.PerformLayout();
            tlpListas.ResumeLayout(false);
            tlpDisponibles.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvPermisosDisponibles).EndInit();
            pnlBotonesMedio.ResumeLayout(false);
            tlpAsignados.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvPermisosAsignadas).EndInit();
            pnlFooter.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tlpRoot;
        private TableLayoutPanel tlpHeader;
        private Label lblTitulo;
        private Label lblRol;
        private ComboBox cbxRol;
        private Label lblBuscar;
        private TextBox txtBuscar;
        private TableLayoutPanel tlpListas;
        private TableLayoutPanel tlpDisponibles;
        private Label lblDisponibles;
        private DataGridView dgvPermisosDisponibles;
        private Panel pnlBotonesMedio;
        private Button btnAsignarPermisos;
        private Button btnQuitarPersmisos;
        private TableLayoutPanel tlpAsignados;
        private Label lblAsignados;
        private DataGridView dgvPermisosAsignadas;
        private Panel pnlFooter;
        private Button btnSalir;
        private Button btnActualizar;
    }
}
