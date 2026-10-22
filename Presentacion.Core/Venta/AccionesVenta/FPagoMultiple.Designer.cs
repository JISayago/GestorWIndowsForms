
namespace Presentacion.Core.Venta;
partial class FPagoMultiple
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
        flowPanel = new FlowLayoutPanel();
        panelBottom = new System.Windows.Forms.Panel();
        lblTotal = new Label();
        lblRestante = new Label();
        btnAceptar = new Button();
        btnCancelar = new Button();
        panelBottom.SuspendLayout();
        SuspendLayout();
        // 
        // flowPanel
        // 
        flowPanel.AutoScroll = true;
        flowPanel.Dock = DockStyle.Fill;
        flowPanel.FlowDirection = FlowDirection.TopDown;
        flowPanel.Location = new Point(0, 0);
        flowPanel.Name = "flowPanel";
        flowPanel.Padding = new Padding(8);
        flowPanel.Size = new Size(600, 186);
        flowPanel.TabIndex = 0;
        flowPanel.WrapContents = false;
        flowPanel.SizeChanged += flowPanel_SizeChanged;
        // 
        // panelBottom
        // 
        panelBottom.Controls.Add(lblTotal);
        panelBottom.Controls.Add(lblRestante);
        panelBottom.Controls.Add(btnAceptar);
        panelBottom.Controls.Add(btnCancelar);
        panelBottom.Dock = DockStyle.Bottom;
        panelBottom.Location = new Point(0, 186);
        panelBottom.Name = "panelBottom";
        panelBottom.Padding = new Padding(8);
        panelBottom.Size = new Size(600, 64);
        panelBottom.TabIndex = 1;
        // 
        // lblTotal
        // 
        lblTotal.AutoSize = true;
        lblTotal.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
        lblTotal.Location = new Point(8, 12);
        lblTotal.Name = "lblTotal";
        lblTotal.Size = new Size(41, 15);
        lblTotal.TabIndex = 0;
        lblTotal.Text = "Total:";
        // 
        // lblRestante
        // 
        lblRestante.AutoSize = true;
        lblRestante.Location = new Point(200, 12);
        lblRestante.Name = "lblRestante";
        lblRestante.Size = new Size(58, 15);
        lblRestante.TabIndex = 1;
        lblRestante.Text = "Restante:";
        // 
        // btnAceptar
        // 
        btnAceptar.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        btnAceptar.Location = new Point(380, 17);
        btnAceptar.Name = "btnAceptar";
        btnAceptar.Size = new Size(100, 30);
        btnAceptar.TabIndex = 2;
        btnAceptar.Text = "Aceptar";
        btnAceptar.UseVisualStyleBackColor = true;
        btnAceptar.Click += BtnAceptar_Click;
        // 
        // btnCancelar
        // 
        btnCancelar.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        btnCancelar.Location = new Point(488, 17);
        btnCancelar.Name = "btnCancelar";
        btnCancelar.Size = new Size(100, 30);
        btnCancelar.TabIndex = 3;
        btnCancelar.Text = "Cancelar";
        btnCancelar.UseVisualStyleBackColor = true;
        btnCancelar.Click += BtnCancelar_Click;
        // 
        // FPagoMultiple
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(600, 250);
        Controls.Add(flowPanel);
        Controls.Add(panelBottom);
        Name = "FPagoMultiple";
        Text = "Pagos múltiples";
        Load += FPagoMultiple_Load;
        panelBottom.ResumeLayout(false);
        panelBottom.PerformLayout();
        ResumeLayout(false);
    }

    #endregion

    private FlowLayoutPanel flowPanel;
    private System.Windows.Forms.Panel panelBottom;
    private Label lblTotal;
    private Label lblRestante;
    private Button btnAceptar;
    private Button btnCancelar;
}
