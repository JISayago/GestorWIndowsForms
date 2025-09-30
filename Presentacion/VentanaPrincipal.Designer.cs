namespace Presentacion
{
    partial class VentanaPrincipal
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            menuStrip1 = new MenuStrip();
            toolStripMenuItem1 = new ToolStripMenuItem();
            aDMINISTRACIONToolStripMenuItem = new ToolStripMenuItem();
            empleadoToolStripMenuItem = new ToolStripMenuItem();
            rolesToolStripMenuItem = new ToolStripMenuItem();
            tipoPagoToolStripMenuItem = new ToolStripMenuItem();
            gastosToolStripMenuItem = new ToolStripMenuItem();
            cuentaCorrienteToolStripMenuItem = new ToolStripMenuItem();
            clienteToolStripMenuItem = new ToolStripMenuItem();
            aRTICULOToolStripMenuItem = new ToolStripMenuItem();
            categoriaToolStripMenuItem = new ToolStripMenuItem();
            marcaToolStripMenuItem = new ToolStripMenuItem();
            articuloToolStripMenuItem1 = new ToolStripMenuItem();
            rubroToolStripMenuItem = new ToolStripMenuItem();
            vENTASToolStripMenuItem = new ToolStripMenuItem();
            cONFIGToolStripMenuItem = new ToolStripMenuItem();
            lblUsuarioLogueado = new Label();
            lblNombreUsuario = new Label();
            btnVenta = new Button();
            btnCaja = new Button();
            btnStock = new Button();
            btnMovimientos = new Button();
            dataGridView1 = new DataGridView();
            panel1 = new Panel();
            ((System.ComponentModel.ISupportInitialize)error).BeginInit();
            menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // menuStrip1
            // 
            menuStrip1.Items.AddRange(new ToolStripItem[] { toolStripMenuItem1, aDMINISTRACIONToolStripMenuItem, aRTICULOToolStripMenuItem, vENTASToolStripMenuItem, cONFIGToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(784, 24);
            menuStrip1.TabIndex = 1;
            menuStrip1.Text = "menuStrip1";
            // 
            // toolStripMenuItem1
            // 
            toolStripMenuItem1.Name = "toolStripMenuItem1";
            toolStripMenuItem1.Size = new Size(12, 20);
            // 
            // aDMINISTRACIONToolStripMenuItem
            // 
            aDMINISTRACIONToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { empleadoToolStripMenuItem, tipoPagoToolStripMenuItem, gastosToolStripMenuItem, cuentaCorrienteToolStripMenuItem });
            aDMINISTRACIONToolStripMenuItem.Name = "aDMINISTRACIONToolStripMenuItem";
            aDMINISTRACIONToolStripMenuItem.Size = new Size(118, 20);
            aDMINISTRACIONToolStripMenuItem.Text = "ADMINISTRACION";
            // 
            // empleadoToolStripMenuItem
            // 
            empleadoToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { rolesToolStripMenuItem });
            empleadoToolStripMenuItem.Name = "empleadoToolStripMenuItem";
            empleadoToolStripMenuItem.Size = new Size(180, 22);
            empleadoToolStripMenuItem.Text = "Empleado";
            empleadoToolStripMenuItem.Click += empleadoToolStripMenuItem_Click;
            // 
            // rolesToolStripMenuItem
            // 
            rolesToolStripMenuItem.Name = "rolesToolStripMenuItem";
            rolesToolStripMenuItem.Size = new Size(102, 22);
            rolesToolStripMenuItem.Text = "Roles";
            rolesToolStripMenuItem.Click += rolesToolStripMenuItem_Click;
            // 
            // tipoPagoToolStripMenuItem
            // 
            tipoPagoToolStripMenuItem.Name = "tipoPagoToolStripMenuItem";
            tipoPagoToolStripMenuItem.Size = new Size(180, 22);
            tipoPagoToolStripMenuItem.Text = "Tipo Pago";
            tipoPagoToolStripMenuItem.Click += tipoPagoToolStripMenuItem_Click;
            // 
            // gastosToolStripMenuItem
            // 
            gastosToolStripMenuItem.Name = "gastosToolStripMenuItem";
            gastosToolStripMenuItem.Size = new Size(180, 22);
            gastosToolStripMenuItem.Text = "Gastos";
            // 
            // cuentaCorrienteToolStripMenuItem
            // 
            cuentaCorrienteToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { clienteToolStripMenuItem });
            cuentaCorrienteToolStripMenuItem.Name = "cuentaCorrienteToolStripMenuItem";
            cuentaCorrienteToolStripMenuItem.Size = new Size(180, 22);
            cuentaCorrienteToolStripMenuItem.Text = "Cuenta Corriente";
            cuentaCorrienteToolStripMenuItem.Click += cuentaCorrienteToolStripMenuItem_Click;
            // 
            // clienteToolStripMenuItem
            // 
            clienteToolStripMenuItem.Name = "clienteToolStripMenuItem";
            clienteToolStripMenuItem.Size = new Size(180, 22);
            clienteToolStripMenuItem.Text = "Cliente";
            clienteToolStripMenuItem.Click += clienteToolStripMenuItem_Click;
            // 
            // aRTICULOToolStripMenuItem
            // 
            aRTICULOToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { categoriaToolStripMenuItem, marcaToolStripMenuItem, articuloToolStripMenuItem1, rubroToolStripMenuItem });
            aRTICULOToolStripMenuItem.Name = "aRTICULOToolStripMenuItem";
            aRTICULOToolStripMenuItem.Size = new Size(81, 20);
            aRTICULOToolStripMenuItem.Text = "PRODUCTO";
            // 
            // categoriaToolStripMenuItem
            // 
            categoriaToolStripMenuItem.Name = "categoriaToolStripMenuItem";
            categoriaToolStripMenuItem.Size = new Size(125, 22);
            categoriaToolStripMenuItem.Text = "Categoria";
            categoriaToolStripMenuItem.Click += categoriaToolStripMenuItem_Click;
            // 
            // marcaToolStripMenuItem
            // 
            marcaToolStripMenuItem.Name = "marcaToolStripMenuItem";
            marcaToolStripMenuItem.Size = new Size(125, 22);
            marcaToolStripMenuItem.Text = "Marca";
            marcaToolStripMenuItem.Click += marcaToolStripMenuItem_Click;
            // 
            // articuloToolStripMenuItem1
            // 
            articuloToolStripMenuItem1.Name = "articuloToolStripMenuItem1";
            articuloToolStripMenuItem1.Size = new Size(125, 22);
            articuloToolStripMenuItem1.Text = "Producto";
            articuloToolStripMenuItem1.Click += articuloToolStripMenuItem1_Click;
            // 
            // rubroToolStripMenuItem
            // 
            rubroToolStripMenuItem.Name = "rubroToolStripMenuItem";
            rubroToolStripMenuItem.Size = new Size(125, 22);
            rubroToolStripMenuItem.Text = "Rubro";
            rubroToolStripMenuItem.Click += rubroToolStripMenuItem_Click;
            // 
            // vENTASToolStripMenuItem
            // 
            vENTASToolStripMenuItem.Name = "vENTASToolStripMenuItem";
            vENTASToolStripMenuItem.Size = new Size(61, 20);
            vENTASToolStripMenuItem.Text = "VENTAS";
            // 
            // cONFIGToolStripMenuItem
            // 
            cONFIGToolStripMenuItem.Name = "cONFIGToolStripMenuItem";
            cONFIGToolStripMenuItem.Size = new Size(72, 20);
            cONFIGToolStripMenuItem.Text = "CONFIG??";
            // 
            // lblUsuarioLogueado
            // 
            lblUsuarioLogueado.AutoSize = true;
            lblUsuarioLogueado.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblUsuarioLogueado.Location = new Point(12, 537);
            lblUsuarioLogueado.Name = "lblUsuarioLogueado";
            lblUsuarioLogueado.Size = new Size(144, 21);
            lblUsuarioLogueado.TabIndex = 2;
            lblUsuarioLogueado.Text = "Usuario Logueado :";
            // 
            // lblNombreUsuario
            // 
            lblNombreUsuario.AutoSize = true;
            lblNombreUsuario.Font = new Font("Segoe UI", 11.25F);
            lblNombreUsuario.Location = new Point(156, 537);
            lblNombreUsuario.Name = "lblNombreUsuario";
            lblNombreUsuario.Size = new Size(0, 20);
            lblNombreUsuario.TabIndex = 3;
            // 
            // btnVenta
            // 
            btnVenta.Location = new Point(596, 0);
            btnVenta.Name = "btnVenta";
            btnVenta.Size = new Size(154, 40);
            btnVenta.TabIndex = 12;
            btnVenta.Text = "VENTA";
            btnVenta.UseVisualStyleBackColor = true;
            btnVenta.Click += btnVenta_Click;
            // 
            // btnCaja
            // 
            btnCaja.Location = new Point(160, 0);
            btnCaja.Name = "btnCaja";
            btnCaja.Size = new Size(154, 40);
            btnCaja.TabIndex = 13;
            btnCaja.Text = "CAJA";
            btnCaja.UseVisualStyleBackColor = true;
            // 
            // btnStock
            // 
            btnStock.Location = new Point(436, 0);
            btnStock.Name = "btnStock";
            btnStock.Size = new Size(154, 40);
            btnStock.TabIndex = 15;
            btnStock.Text = "STOCK";
            btnStock.UseVisualStyleBackColor = true;
            // 
            // btnMovimientos
            // 
            btnMovimientos.Location = new Point(0, 0);
            btnMovimientos.Name = "btnMovimientos";
            btnMovimientos.Size = new Size(154, 40);
            btnMovimientos.TabIndex = 16;
            btnMovimientos.Text = "MOVIMIENTOS";
            btnMovimientos.UseVisualStyleBackColor = true;
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(15, 37);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.Size = new Size(754, 345);
            dataGridView1.TabIndex = 17;
            // 
            // panel1
            // 
            panel1.Controls.Add(btnMovimientos);
            panel1.Controls.Add(btnCaja);
            panel1.Controls.Add(btnVenta);
            panel1.Controls.Add(btnStock);
            panel1.Location = new Point(15, 494);
            panel1.Name = "panel1";
            panel1.Size = new Size(750, 40);
            panel1.TabIndex = 18;
            // 
            // VentanaPrincipal
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(784, 561);
            Controls.Add(panel1);
            Controls.Add(dataGridView1);
            Controls.Add(lblNombreUsuario);
            Controls.Add(lblUsuarioLogueado);
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
            MinimumSize = new Size(800, 600);
            Name = "VentanaPrincipal";
            Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)error).EndInit();
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            panel1.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private MenuStrip menuStrip1;
        private ToolStripMenuItem toolStripMenuItem1;
        private ToolStripMenuItem aDMINISTRACIONToolStripMenuItem;
        private ToolStripMenuItem empleadoToolStripMenuItem;
        private ToolStripMenuItem aRTICULOToolStripMenuItem;
        private ToolStripMenuItem tipoPagoToolStripMenuItem;
        private ToolStripMenuItem categoriaToolStripMenuItem;
        private ToolStripMenuItem marcaToolStripMenuItem;
        private ToolStripMenuItem articuloToolStripMenuItem1;
        private Label lblUsuarioLogueado;
        private Label lblNombreUsuario;
        private ToolStripMenuItem gastosToolStripMenuItem;
        private ToolStripMenuItem vENTASToolStripMenuItem;
        private Button btnVenta;
        private Button btnCaja;
        private Button btnStock;
        private Button btnMovimientos;
        private ToolStripMenuItem cONFIGToolStripMenuItem;
        private DataGridView dataGridView1;
        private Panel panel1;
        private ToolStripMenuItem rolesToolStripMenuItem;
        private ToolStripMenuItem rubroToolStripMenuItem;
        private ToolStripMenuItem cuentaCorrienteToolStripMenuItem;
        private ToolStripMenuItem clienteToolStripMenuItem;
    }
}
