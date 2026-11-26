namespace Presentacion.Core.Administracion
{
    partial class FAdministracion
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();

            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FAdministracion));
            menuStrip1 = new MenuStrip();
            pRODUCTOToolStripMenuItem = new ToolStripMenuItem();
            sTOCKToolStripMenuItem = new ToolStripMenuItem();
            mARCASToolStripMenuItem = new ToolStripMenuItem();
            cATEGORIASToolStripMenuItem = new ToolStripMenuItem();
            rUBROSToolStripMenuItem = new ToolStripMenuItem();
            lOTESToolStripMenuItem = new ToolStripMenuItem();
            eMPLEADOSToolStripMenuItem = new ToolStripMenuItem();
            lISTADOEMPLEADOSToolStripMenuItem = new ToolStripMenuItem();
            rOLESToolStripMenuItem = new ToolStripMenuItem();
            cLIENTESToolStripMenuItem = new ToolStripMenuItem();
            lISTADOCLIENTESToolStripMenuItem = new ToolStripMenuItem();
            oFERTASToolStripMenuItem = new ToolStripMenuItem();
            nUEVAOFERTAToolStripMenuItem = new ToolStripMenuItem();
            lISTADOOFERTASToolStripMenuItem = new ToolStripMenuItem();
            vENTAToolStripMenuItem = new ToolStripMenuItem();
            historialToolStripMenuItem = new ToolStripMenuItem();
            nuevaVentaLibreToolStripMenuItem = new ToolStripMenuItem();
            historialVentasLibresToolStripMenuItem = new ToolStripMenuItem();
            cONFIGURACIONToolStripMenuItem = new ToolStripMenuItem();
            modificarPDFToolStripMenuItem = new ToolStripMenuItem();
            acccesoToolStripMenuItem = new ToolStripMenuItem();
            ayudaToolStripMenuItem = new ToolStripMenuItem();
            manualDeUsuarioToolStripMenuItem = new ToolStripMenuItem();
            tableLayoutPanel1 = new TableLayoutPanel();
            btnEstadisticas = new Button();
            btnGasto = new Button();
            btnComprobantes = new Button();
            btnMovimientos = new Button();
            btnVolver = new Button();
            pnlInfoInicial = new Panel();
            tlpContenidoPrincipal = new TableLayoutPanel();
            pnlConfiguracion = new Panel();
            pnlDatosNegocio = new Panel();
            lblTextoPieNegocio = new Label();
            lblSubtituloNegocio = new Label();
            lblNombreNegocio = new Label();
            picLogoNegocio = new PictureBox();
            lblTituloNegocio = new Label();
            ((System.ComponentModel.ISupportInitialize)error).BeginInit();
            menuStrip1.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            pnlInfoInicial.SuspendLayout();
            tlpContenidoPrincipal.SuspendLayout();
            pnlDatosNegocio.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picLogoNegocio).BeginInit();
            SuspendLayout();
            // 
            // menuStrip1
            // 
            menuStrip1.Dock = DockStyle.Top;
            menuStrip1.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            menuStrip1.Items.AddRange(new ToolStripItem[] { pRODUCTOToolStripMenuItem, eMPLEADOSToolStripMenuItem, cLIENTESToolStripMenuItem, oFERTASToolStripMenuItem, vENTAToolStripMenuItem, cONFIGURACIONToolStripMenuItem, ayudaToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Padding = new Padding(6, 3, 3, 3);
            menuStrip1.Size = new Size(1610, 30);
            menuStrip1.TabIndex = 2;
            menuStrip1.Text = "menuStrip1";
            // 
            // pRODUCTOToolStripMenuItem
            // 
            pRODUCTOToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { sTOCKToolStripMenuItem, mARCASToolStripMenuItem, cATEGORIASToolStripMenuItem, rUBROSToolStripMenuItem, lOTESToolStripMenuItem });
            pRODUCTOToolStripMenuItem.Name = "pRODUCTOToolStripMenuItem";
            pRODUCTOToolStripMenuItem.Size = new Size(99, 24);
            pRODUCTOToolStripMenuItem.Text = "PRODUCTO";
            // 
            // sTOCKToolStripMenuItem
            // 
            sTOCKToolStripMenuItem.Name = "sTOCKToolStripMenuItem";
            sTOCKToolStripMenuItem.Size = new Size(199, 24);
            sTOCKToolStripMenuItem.Text = "Listado Productos";
            sTOCKToolStripMenuItem.Click += sTOCKToolStripMenuItem_Click;
            // 
            // mARCASToolStripMenuItem
            // 
            mARCASToolStripMenuItem.Name = "mARCASToolStripMenuItem";
            mARCASToolStripMenuItem.Size = new Size(199, 24);
            mARCASToolStripMenuItem.Text = "Marcas";
            mARCASToolStripMenuItem.Click += mARCASToolStripMenuItem_Click;
            // 
            // cATEGORIASToolStripMenuItem
            // 
            cATEGORIASToolStripMenuItem.Name = "cATEGORIASToolStripMenuItem";
            cATEGORIASToolStripMenuItem.Size = new Size(199, 24);
            cATEGORIASToolStripMenuItem.Text = "Categorias";
            cATEGORIASToolStripMenuItem.Click += cATEGORIASToolStripMenuItem_Click;
            // 
            // rUBROSToolStripMenuItem
            // 
            rUBROSToolStripMenuItem.Name = "rUBROSToolStripMenuItem";
            rUBROSToolStripMenuItem.Size = new Size(199, 24);
            rUBROSToolStripMenuItem.Text = "Rubros";
            rUBROSToolStripMenuItem.Click += rUBROSToolStripMenuItem_Click;
            // 
            // lOTESToolStripMenuItem
            // 
            lOTESToolStripMenuItem.Name = "lOTESToolStripMenuItem";
            lOTESToolStripMenuItem.Size = new Size(199, 24);
            lOTESToolStripMenuItem.Text = "Lotes";
            lOTESToolStripMenuItem.Click += lOTESToolStripMenuItem_Click;
            // 
            // eMPLEADOSToolStripMenuItem
            // 
            eMPLEADOSToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { lISTADOEMPLEADOSToolStripMenuItem, rOLESToolStripMenuItem });
            eMPLEADOSToolStripMenuItem.Name = "eMPLEADOSToolStripMenuItem";
            eMPLEADOSToolStripMenuItem.Size = new Size(107, 24);
            eMPLEADOSToolStripMenuItem.Text = "EMPLEADOS";
            // 
            // lISTADOEMPLEADOSToolStripMenuItem
            // 
            lISTADOEMPLEADOSToolStripMenuItem.Name = "lISTADOEMPLEADOSToolStripMenuItem";
            lISTADOEMPLEADOSToolStripMenuItem.Size = new Size(204, 24);
            lISTADOEMPLEADOSToolStripMenuItem.Text = "Listado Empleados";
            lISTADOEMPLEADOSToolStripMenuItem.Click += lISTADOEMPLEADOSToolStripMenuItem_Click;
            // 
            // rOLESToolStripMenuItem
            // 
            rOLESToolStripMenuItem.Name = "rOLESToolStripMenuItem";
            rOLESToolStripMenuItem.Size = new Size(204, 24);
            rOLESToolStripMenuItem.Text = "Roles";
            rOLESToolStripMenuItem.Click += rOLESToolStripMenuItem_Click;
            // 
            // cLIENTESToolStripMenuItem
            // 
            cLIENTESToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { lISTADOCLIENTESToolStripMenuItem });
            cLIENTESToolStripMenuItem.Name = "cLIENTESToolStripMenuItem";
            cLIENTESToolStripMenuItem.Size = new Size(85, 24);
            cLIENTESToolStripMenuItem.Text = "CLIENTES";
            // 
            // lISTADOCLIENTESToolStripMenuItem
            // 
            lISTADOCLIENTESToolStripMenuItem.Name = "lISTADOCLIENTESToolStripMenuItem";
            lISTADOCLIENTESToolStripMenuItem.Size = new Size(183, 24);
            lISTADOCLIENTESToolStripMenuItem.Text = "Listado Clientes";
            lISTADOCLIENTESToolStripMenuItem.Click += lISTADOCLIENTESToolStripMenuItem_Click;
            // 
            // oFERTASToolStripMenuItem
            // 
            oFERTASToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { nUEVAOFERTAToolStripMenuItem, lISTADOOFERTASToolStripMenuItem });
            oFERTASToolStripMenuItem.Name = "oFERTASToolStripMenuItem";
            oFERTASToolStripMenuItem.Size = new Size(82, 24);
            oFERTASToolStripMenuItem.Text = "OFERTAS";
            // 
            // nUEVAOFERTAToolStripMenuItem
            // 
            nUEVAOFERTAToolStripMenuItem.Name = "nUEVAOFERTAToolStripMenuItem";
            nUEVAOFERTAToolStripMenuItem.Size = new Size(179, 24);
            nUEVAOFERTAToolStripMenuItem.Text = "Nueva Oferta";
            nUEVAOFERTAToolStripMenuItem.Click += nUEVAOFERTAToolStripMenuItem_Click;
            // 
            // lISTADOOFERTASToolStripMenuItem
            // 
            lISTADOOFERTASToolStripMenuItem.Name = "lISTADOOFERTASToolStripMenuItem";
            lISTADOOFERTASToolStripMenuItem.Size = new Size(179, 24);
            lISTADOOFERTASToolStripMenuItem.Text = "Listado Ofertas";
            lISTADOOFERTASToolStripMenuItem.Click += lISTADOOFERTASToolStripMenuItem_Click;
            // 
            // vENTAToolStripMenuItem
            // 
            vENTAToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { historialToolStripMenuItem, nuevaVentaLibreToolStripMenuItem, historialVentasLibresToolStripMenuItem });
            vENTAToolStripMenuItem.Name = "vENTAToolStripMenuItem";
            vENTAToolStripMenuItem.Size = new Size(68, 24);
            vENTAToolStripMenuItem.Text = "VENTA";
            // 
            // historialToolStripMenuItem
            // 
            historialToolStripMenuItem.Name = "historialToolStripMenuItem";
            historialToolStripMenuItem.Size = new Size(228, 24);
            historialToolStripMenuItem.Text = "Historial Ventas";
            historialToolStripMenuItem.Click += historialToolStripMenuItem_Click;
            // 
            // nuevaVentaLibreToolStripMenuItem
            // 
            nuevaVentaLibreToolStripMenuItem.Name = "nuevaVentaLibreToolStripMenuItem";
            nuevaVentaLibreToolStripMenuItem.Size = new Size(228, 24);
            nuevaVentaLibreToolStripMenuItem.Text = "Nueva Venta Libre";
            nuevaVentaLibreToolStripMenuItem.Click += nuevaVentaLibreToolStripMenuItem_Click;
            // 
            // historialVentasLibresToolStripMenuItem
            // 
            historialVentasLibresToolStripMenuItem.Name = "historialVentasLibresToolStripMenuItem";
            historialVentasLibresToolStripMenuItem.Size = new Size(228, 24);
            historialVentasLibresToolStripMenuItem.Text = "Historial Ventas Libres";
            historialVentasLibresToolStripMenuItem.Click += historialVentasLibresToolStripMenuItem_Click;
            // 
            // cONFIGURACIONToolStripMenuItem
            // 
            cONFIGURACIONToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { modificarPDFToolStripMenuItem, acccesoToolStripMenuItem });
            cONFIGURACIONToolStripMenuItem.Name = "cONFIGURACIONToolStripMenuItem";
            cONFIGURACIONToolStripMenuItem.Size = new Size(141, 24);
            cONFIGURACIONToolStripMenuItem.Text = "CONFIGURACION";
            // 
            // modificarPDFToolStripMenuItem
            // 
            modificarPDFToolStripMenuItem.Name = "modificarPDFToolStripMenuItem";
            modificarPDFToolStripMenuItem.Size = new Size(221, 24);
            modificarPDFToolStripMenuItem.Text = "Datos Comprobantes";
            modificarPDFToolStripMenuItem.Click += modificarPDFToolStripMenuItem_Click;
            // 
            // acccesoToolStripMenuItem
            // 
            acccesoToolStripMenuItem.Name = "acccesoToolStripMenuItem";
            acccesoToolStripMenuItem.Size = new Size(221, 24);
            acccesoToolStripMenuItem.Text = "Configuración";
            acccesoToolStripMenuItem.Click += acccesoToolStripMenuItem_Click;
            // 
            // ayudaToolStripMenuItem
            // 
            ayudaToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { manualDeUsuarioToolStripMenuItem });
            ayudaToolStripMenuItem.ImageAlign = ContentAlignment.MiddleLeft;
            ayudaToolStripMenuItem.Name = "ayudaToolStripMenuItem";
            ayudaToolStripMenuItem.Size = new Size(71, 24);
            ayudaToolStripMenuItem.Text = "AYUDA";
            // 
            // manualDeUsuarioToolStripMenuItem
            // 
            manualDeUsuarioToolStripMenuItem.Name = "manualDeUsuarioToolStripMenuItem";
            manualDeUsuarioToolStripMenuItem.Size = new Size(208, 24);
            manualDeUsuarioToolStripMenuItem.Text = "Manual de Usuario";

            //
            // tableLayoutPanel1
            //
            tableLayoutPanel1.ColumnCount = 5;
            tableLayoutPanel1.ColumnStyles.Clear();

            tableLayoutPanel1.ColumnStyles.Add(
                new ColumnStyle(SizeType.Percent, 20F));
            tableLayoutPanel1.ColumnStyles.Add(
                new ColumnStyle(SizeType.Percent, 20F));
            tableLayoutPanel1.ColumnStyles.Add(
                new ColumnStyle(SizeType.Percent, 20F));
            tableLayoutPanel1.ColumnStyles.Add(
                new ColumnStyle(SizeType.Percent, 20F));
            tableLayoutPanel1.ColumnStyles.Add(
                new ColumnStyle(SizeType.Percent, 20F));

            tableLayoutPanel1.Controls.Add(btnEstadisticas, 0, 0);
            tableLayoutPanel1.Controls.Add(btnGasto, 1, 0);
            tableLayoutPanel1.Controls.Add(btnVolver, 2, 0);
            tableLayoutPanel1.Controls.Add(btnMovimientos, 3, 0);
            tableLayoutPanel1.Controls.Add(btnComprobantes, 4, 0);

            tableLayoutPanel1.Dock = DockStyle.Top;
            tableLayoutPanel1.Height = 112;
            tableLayoutPanel1.Margin = Padding.Empty;
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.Padding = new Padding(20, 14, 20, 8);
            tableLayoutPanel1.RowCount = 1;
            tableLayoutPanel1.RowStyles.Add(
                new RowStyle(SizeType.Percent, 100F));

            tableLayoutPanel1.TabIndex = 7;

            // 
            // btnEstadisticas
            // 
            btnEstadisticas.Anchor = AnchorStyles.None;
            btnEstadisticas.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold);
            btnEstadisticas.Location = new Point(103, 25);
            btnEstadisticas.MaximumSize = new Size(220, 68);
            btnEstadisticas.MinimumSize = new Size(190, 60);
            btnEstadisticas.Name = "btnEstadisticas";
            btnEstadisticas.Size = new Size(220, 68);
            btnEstadisticas.TabIndex = 17;
            btnEstadisticas.Text = "ESTADÍSTICAS";
            btnEstadisticas.UseVisualStyleBackColor = true;
            btnEstadisticas.Click += btnEstadisticas_Click;
            // 
            // btnGasto
            // 
            btnGasto.Anchor = AnchorStyles.None;
            btnGasto.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold);
            btnGasto.Location = new Point(490, 25);
            btnGasto.MaximumSize = new Size(220, 68);
            btnGasto.MinimumSize = new Size(190, 60);
            btnGasto.Name = "btnGasto";
            btnGasto.Size = new Size(220, 68);
            btnGasto.TabIndex = 12;
            btnGasto.Text = "GASTO";
            btnGasto.UseVisualStyleBackColor = true;
            btnGasto.Click += btnGasto_Click;
            // 
            // btnComprobantes
            // 
            btnComprobantes.Anchor = AnchorStyles.None;
            btnComprobantes.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold);
            btnComprobantes.Location = new Point(1571, 25);
            btnComprobantes.MaximumSize = new Size(220, 68);
            btnComprobantes.MinimumSize = new Size(190, 60);
            btnComprobantes.Name = "btnComprobantes";
            btnComprobantes.Size = new Size(190, 68);
            btnComprobantes.TabIndex = 15;
            btnComprobantes.Text = "COMPROBANTES";
            btnComprobantes.UseVisualStyleBackColor = true;
            btnComprobantes.Click += btnComprobantes_Click;
            // 
            // btnMovimientos
            // 
            btnMovimientos.Anchor = AnchorStyles.None;
            btnMovimientos.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold);
            btnMovimientos.Location = new Point(1264, 25);
            btnMovimientos.MaximumSize = new Size(220, 68);
            btnMovimientos.MinimumSize = new Size(190, 60);
            btnMovimientos.Name = "btnMovimientos";
            btnMovimientos.Size = new Size(220, 68);
            btnMovimientos.TabIndex = 16;
            btnMovimientos.Text = "MOVIMIENTOS";
            btnMovimientos.UseVisualStyleBackColor = true;
            btnMovimientos.Click += btnMovimientos_Click;
            // 
            // btnVolver
            // 
            btnVolver.Anchor = AnchorStyles.None;
            btnVolver.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold);
            btnVolver.Location = new Point(877, 25);
            btnVolver.MaximumSize = new Size(220, 68);
            btnVolver.MinimumSize = new Size(190, 60);
            btnVolver.Name = "btnVolver";
            btnVolver.Size = new Size(220, 68);
            btnVolver.TabIndex = 13;
            btnVolver.Text = "VOLVER AL INICIO";
            btnVolver.UseVisualStyleBackColor = true;
            btnVolver.Click += btnVolver_Click;
            // 
            // pnlInfoInicial
            // 
            pnlInfoInicial.Controls.Add(tlpContenidoPrincipal);
            pnlInfoInicial.Dock = DockStyle.Fill;
            pnlInfoInicial.Location = new Point(0, 30);
            pnlInfoInicial.Name = "pnlInfoInicial";
            pnlInfoInicial.Padding = new Padding(28, 18, 28, 28);
            pnlInfoInicial.Size = new Size(1610, 960);
            pnlInfoInicial.TabIndex = 24;
            pnlInfoInicial.Dock = DockStyle.Fill;
            // 
            // tlpContenidoPrincipal
            // 
            tlpContenidoPrincipal.ColumnCount = 2;
            tlpContenidoPrincipal.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 73F));
            tlpContenidoPrincipal.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 27F));
            tlpContenidoPrincipal.Controls.Add(pnlConfiguracion, 0, 0);
            tlpContenidoPrincipal.Controls.Add(pnlDatosNegocio, 1, 0);
            tlpContenidoPrincipal.Dock = DockStyle.Fill;
            tlpContenidoPrincipal.Location = new Point(28, 18);
            tlpContenidoPrincipal.Margin = new Padding(0);
            tlpContenidoPrincipal.Name = "tlpContenidoPrincipal";
            tlpContenidoPrincipal.RowCount = 1;
            tlpContenidoPrincipal.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpContenidoPrincipal.Size = new Size(1554, 914);
            tlpContenidoPrincipal.TabIndex = 0;

            //
            // pnlConfiguracion
            //
            pnlConfiguracion.BackColor = Color.White;
            pnlConfiguracion.BorderStyle = BorderStyle.FixedSingle;
            pnlConfiguracion.Dock = DockStyle.Fill;
            pnlConfiguracion.Location = new Point(0, 0);
            pnlConfiguracion.Margin = new Padding(0, 0, 18, 0);
            pnlConfiguracion.Name = "pnlConfiguracion";
            pnlConfiguracion.Padding = new Padding(18);
            pnlConfiguracion.Size = new Size(1116, 914);
            pnlConfiguracion.TabIndex = 0;

            // 
            // pnlDatosNegocio
            // 
            pnlDatosNegocio.BackColor = Color.White;
            pnlDatosNegocio.BorderStyle = BorderStyle.FixedSingle;
            pnlDatosNegocio.Controls.Add(lblTextoPieNegocio);
            pnlDatosNegocio.Controls.Add(lblSubtituloNegocio);
            pnlDatosNegocio.Controls.Add(lblNombreNegocio);
            pnlDatosNegocio.Controls.Add(picLogoNegocio);
            pnlDatosNegocio.Controls.Add(lblTituloNegocio);
            pnlDatosNegocio.Dock = DockStyle.Fill;
            pnlDatosNegocio.Location = new Point(1142, 0);
            pnlDatosNegocio.Margin = new Padding(8, 0, 0, 0);
            pnlDatosNegocio.Name = "pnlDatosNegocio";
            pnlDatosNegocio.Padding = new Padding(18);
            pnlDatosNegocio.Size = new Size(412, 914);
            pnlDatosNegocio.TabIndex = 1;
            // 
            // lblTextoPieNegocio
            // 
            lblTextoPieNegocio.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            lblTextoPieNegocio.Font = new Font("Segoe UI", 9F);
            lblTextoPieNegocio.ForeColor = Color.Gray;
            lblTextoPieNegocio.Location = new Point(12, 330);
            lblTextoPieNegocio.Name = "lblTextoPieNegocio";
            lblTextoPieNegocio.Size = new Size(430, 60);
            lblTextoPieNegocio.TabIndex = 0;
            lblTextoPieNegocio.Text = "Texto adicional del negocio";
            lblTextoPieNegocio.TextAlign = ContentAlignment.TopCenter;
            // 
            // lblSubtituloNegocio
            // 
            lblSubtituloNegocio.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            lblSubtituloNegocio.Font = new Font("Segoe UI", 9.5F);
            lblSubtituloNegocio.Location = new Point(12, 255);
            lblSubtituloNegocio.Name = "lblSubtituloNegocio";
            lblSubtituloNegocio.Size = new Size(430, 55);
            lblSubtituloNegocio.TabIndex = 1;
            lblSubtituloNegocio.Text = "Subtítulo o descripción del negocio";
            lblSubtituloNegocio.TextAlign = ContentAlignment.TopCenter;
            // 
            // lblNombreNegocio
            // 
            lblNombreNegocio.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            lblNombreNegocio.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold);
            lblNombreNegocio.Location = new Point(12, 215);
            lblNombreNegocio.Name = "lblNombreNegocio";
            lblNombreNegocio.Size = new Size(430, 32);
            lblNombreNegocio.TabIndex = 2;
            lblNombreNegocio.Text = "NOMBRE DEL NEGOCIO";
            lblNombreNegocio.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // picLogoNegocio
            // 
            picLogoNegocio.Anchor = AnchorStyles.Top;
            picLogoNegocio.BackColor = Color.FromArgb(245, 245, 248);
            picLogoNegocio.BorderStyle = BorderStyle.FixedSingle;
            picLogoNegocio.Location = new Point(140, 65);
            picLogoNegocio.Name = "picLogoNegocio";
            picLogoNegocio.Size = new Size(150, 130);
            picLogoNegocio.SizeMode = PictureBoxSizeMode.Zoom;
            picLogoNegocio.TabIndex = 3;
            picLogoNegocio.TabStop = false;
            // 
            // lblTituloNegocio
            // 
            lblTituloNegocio.Dock = DockStyle.Top;
            lblTituloNegocio.Font = new Font("Segoe UI Semibold", 11F, FontStyle.Bold);
            lblTituloNegocio.Location = new Point(18, 18);
            lblTituloNegocio.Name = "lblTituloNegocio";
            lblTituloNegocio.Size = new Size(374, 34);
            lblTituloNegocio.TabIndex = 4;
            lblTituloNegocio.Text = "DATOS DEL NEGOCIO";
            lblTituloNegocio.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // FAdministracion
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1610, 990);
            Controls.Add(pnlInfoInicial);
            Controls.Add(tableLayoutPanel1);
            Controls.Add(menuStrip1);
            ForeColor = Color.FromArgb(31, 26, 43);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "FAdministracion";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Panel Administración";
            WindowState = FormWindowState.Maximized;
            Load += FAdministracion_Load;
            ((System.ComponentModel.ISupportInitialize)error).EndInit();
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            tableLayoutPanel1.ResumeLayout(false);
            pnlInfoInicial.ResumeLayout(false);
            tlpContenidoPrincipal.ResumeLayout(false);
            pnlDatosNegocio.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)picLogoNegocio).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip menuStrip1;
        private ToolStripMenuItem eMPLEADOSToolStripMenuItem;
        private ToolStripMenuItem lISTADOEMPLEADOSToolStripMenuItem;
        private ToolStripMenuItem cLIENTESToolStripMenuItem;
        private ToolStripMenuItem lISTADOCLIENTESToolStripMenuItem;
        private ToolStripMenuItem pRODUCTOToolStripMenuItem;
        private ToolStripMenuItem sTOCKToolStripMenuItem;
        private ToolStripMenuItem mARCASToolStripMenuItem;
        private ToolStripMenuItem cATEGORIASToolStripMenuItem;
        private ToolStripMenuItem rUBROSToolStripMenuItem;
        private ToolStripMenuItem oFERTASToolStripMenuItem;
        private ToolStripMenuItem lISTADOOFERTASToolStripMenuItem;
        private ToolStripMenuItem cONFIGURACIONToolStripMenuItem;
        private TableLayoutPanel tableLayoutPanel1;
        private Button btnGasto;
        private Button btnMovimientos;
        private Button btnComprobantes;
        private Button btnVolver;
        private ToolStripMenuItem rOLESToolStripMenuItem;
        private ToolStripMenuItem nUEVAOFERTAToolStripMenuItem;
        private ToolStripMenuItem lOTESToolStripMenuItem;
        private ToolStripMenuItem vENTAToolStripMenuItem;
        private ToolStripMenuItem historialToolStripMenuItem;
        private ToolStripMenuItem historialVentasLibresToolStripMenuItem;
        private ToolStripMenuItem nuevaVentaLibreToolStripMenuItem;
        private Panel pnlInfoInicial;
        private TableLayoutPanel tlpContenidoPrincipal;
        private Panel pnlConfiguracion;
        private Panel pnlDatosNegocio;
        private PictureBox picLogoNegocio;
        private Label lblTituloNegocio;
        private Label lblNombreNegocio;
        private Label lblSubtituloNegocio;
        private Label lblTextoPieNegocio;
        private ToolStripMenuItem modificarPDFToolStripMenuItem;
        private ToolStripMenuItem ayudaToolStripMenuItem;
        private ToolStripMenuItem manualDeUsuarioToolStripMenuItem;
        private ToolStripMenuItem acccesoToolStripMenuItem;
        private Button btnEstadisticas;
    }
}
