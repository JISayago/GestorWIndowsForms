namespace Presentacion.Core.Oferta
{
    partial class FSeleccionTipoOferta
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
            btnOfertaCompuesta = new Button();
            btnOfertaPorGrupo = new Button();
            SuspendLayout();
            // 
            // btnOfertaCompuesta
            // 
            btnOfertaCompuesta.Location = new Point(58, 54);
            btnOfertaCompuesta.Name = "btnOfertaCompuesta";
            btnOfertaCompuesta.Size = new Size(302, 131);
            btnOfertaCompuesta.TabIndex = 0;
            btnOfertaCompuesta.Text = "Oferta Compuesta";
            btnOfertaCompuesta.UseVisualStyleBackColor = true;
            btnOfertaCompuesta.Click += btnOfertaCompuesta_Click;
            // 
            // btnOfertaPorGrupo
            // 
            btnOfertaPorGrupo.Location = new Point(396, 54);
            btnOfertaPorGrupo.Name = "btnOfertaPorGrupo";
            btnOfertaPorGrupo.Size = new Size(302, 131);
            btnOfertaPorGrupo.TabIndex = 1;
            btnOfertaPorGrupo.Text = "Oferta por Grupo";
            btnOfertaPorGrupo.UseVisualStyleBackColor = true;
            btnOfertaPorGrupo.Click += btnOfertaPorGrupo_Click;
            // 
            // FSeleccionTipoOferta
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(btnOfertaPorGrupo);
            Controls.Add(btnOfertaCompuesta);
            Name = "FSeleccionTipoOferta";
            Text = "FSeleccionTipoOferta";
            ResumeLayout(false);
        }

        #endregion

        private Button btnOfertaCompuesta;
        private Button btnOfertaPorGrupo;
    }
}