namespace Presentacion.Core.Producto.Asignacion
{
    partial class FAsignacionBase
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
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
            components = new System.ComponentModel.Container();
            txtBuscar = new System.Windows.Forms.TextBox();
            timerBusqueda = new System.Windows.Forms.Timer(components);
            dgvItems = new System.Windows.Forms.DataGridView();
            colSeleccionado = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            colNombre = new System.Windows.Forms.DataGridViewTextBoxColumn();
            btnAnterior = new System.Windows.Forms.Button();
            lblPaginacion = new System.Windows.Forms.Label();
            btnSiguiente = new System.Windows.Forms.Button();
            btnAceptar = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)dgvItems).BeginInit();
            SuspendLayout();
            // 
            // txtBuscar
            // 
            txtBuscar.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            txtBuscar.Location = new System.Drawing.Point(12, 12);
            txtBuscar.Name = "txtBuscar";
            txtBuscar.Size = new System.Drawing.Size(336, 27);
            txtBuscar.TabIndex = 0;
            txtBuscar.TextChanged += txtBuscar_TextChanged;
            // 
            // timerBusqueda
            // 
            timerBusqueda.Interval = 350;
            timerBusqueda.Tick += timerBusqueda_Tick;
            // 
            // dgvItems
            // 
            dgvItems.AllowUserToAddRows = false;
            dgvItems.AllowUserToDeleteRows = false;
            dgvItems.AllowUserToResizeRows = false;
            dgvItems.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            dgvItems.BackgroundColor = System.Drawing.SystemColors.Window;
            dgvItems.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvItems.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] { colSeleccionado, colNombre });
            dgvItems.Location = new System.Drawing.Point(12, 47);
            dgvItems.MultiSelect = false;
            dgvItems.Name = "dgvItems";
            dgvItems.RowHeadersVisible = false;
            dgvItems.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            dgvItems.Size = new System.Drawing.Size(336, 235);
            dgvItems.TabIndex = 1;
            dgvItems.CellValueChanged += Dgv_CellValueChanged;
            dgvItems.CurrentCellDirtyStateChanged += Dgv_CurrentCellDirtyStateChanged;
            // 
            // colSeleccionado
            // 
            colSeleccionado.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            colSeleccionado.HeaderText = "Sel.";
            colSeleccionado.Name = "Seleccionado";
            colSeleccionado.Width = 50;
            // 
            // colNombre
            // 
            colNombre.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            colNombre.HeaderText = "Nombre";
            colNombre.Name = "Nombre";
            colNombre.ReadOnly = true;
            // 
            // btnAnterior
            // 
            btnAnterior.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            btnAnterior.Location = new System.Drawing.Point(12, 290);
            btnAnterior.Name = "btnAnterior";
            btnAnterior.Size = new System.Drawing.Size(40, 28);
            btnAnterior.TabIndex = 2;
            btnAnterior.Text = "◀";
            btnAnterior.UseVisualStyleBackColor = true;
            btnAnterior.Click += btnAnterior_Click;
            // 
            // lblPaginacion
            // 
            lblPaginacion.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            lblPaginacion.Location = new System.Drawing.Point(58, 290);
            lblPaginacion.Name = "lblPaginacion";
            lblPaginacion.Size = new System.Drawing.Size(244, 28);
            lblPaginacion.TabIndex = 5;
            lblPaginacion.Text = "Página 1 de 1  (0 registros)";
            lblPaginacion.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnSiguiente
            // 
            btnSiguiente.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            btnSiguiente.Location = new System.Drawing.Point(308, 290);
            btnSiguiente.Name = "btnSiguiente";
            btnSiguiente.Size = new System.Drawing.Size(40, 28);
            btnSiguiente.TabIndex = 3;
            btnSiguiente.Text = "▶";
            btnSiguiente.UseVisualStyleBackColor = true;
            btnSiguiente.Click += btnSiguiente_Click;
            // 
            // btnAceptar
            // 
            btnAceptar.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            btnAceptar.Location = new System.Drawing.Point(112, 330);
            btnAceptar.Name = "btnAceptar";
            btnAceptar.Size = new System.Drawing.Size(136, 32);
            btnAceptar.TabIndex = 4;
            btnAceptar.Text = "Aceptar";
            btnAceptar.UseVisualStyleBackColor = true;
            btnAceptar.Click += btnAceptar_Click;
            // 
            // FAsignacionBase
            // 
            AcceptButton = btnAceptar;
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(360, 380);
            Controls.Add(txtBuscar);
            Controls.Add(dgvItems);
            Controls.Add(btnAnterior);
            Controls.Add(lblPaginacion);
            Controls.Add(btnSiguiente);
            Controls.Add(btnAceptar);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FAsignacionBase";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            Text = "Asignar";
            Load += FAsignacionBase_Load;
            ((System.ComponentModel.ISupportInitialize)dgvItems).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.TextBox txtBuscar;
        private System.Windows.Forms.Timer timerBusqueda;
        private System.Windows.Forms.DataGridView dgvItems;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colSeleccionado;
        private System.Windows.Forms.DataGridViewTextBoxColumn colNombre;
        private System.Windows.Forms.Button btnAnterior;
        private System.Windows.Forms.Label lblPaginacion;
        private System.Windows.Forms.Button btnSiguiente;
        private System.Windows.Forms.Button btnAceptar;
    }
}
