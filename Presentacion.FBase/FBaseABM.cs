using Presentacion.FormulariosBase.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Presentacion.FBase
{
    public partial class FBaseABM : FBase
    {
        protected TipoOperacion TipoOperacion;
        protected long? EntidadID;
        public bool RealizoAlgunaOperacion;
        public FBaseABM()
        {
            InitializeComponent();
            btnSalir.Image = Constantes.Imagenes.ImgCerrar;
        }
        public FBaseABM(TipoOperacion tipoOperacion, long? entidadId) : this()
        {
            TipoOperacion = tipoOperacion;
            EntidadID = entidadId;
            RealizoAlgunaOperacion = false;
            CambiarTextoImagenBotones(tipoOperacion);
        }

        private void CambiarTextoImagenBotones(TipoOperacion tipoOperacion)
        {
            if (tipoOperacion == TipoOperacion.Eliminar)
            {
                btnEjecutar.Text = "Eliminar";
                btnEjecutar.Image = Constantes.Imagenes.ImgEliminar;
                btnLimpiar.Image = Constantes.Imagenes.ImgLimpiar;
            }
            else
            {
                btnEjecutar.Text = "Guardar";
                btnEjecutar.Image = Constantes.Imagenes.ImgGuardar;
                btnLimpiar.Image = Constantes.Imagenes.ImgLimpiar;
            }
        }

        private void btnEjecutar_Click(object sender, EventArgs e)
        {
            EjecutarComando();
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(@"Esta seguro de Limpiar los Datos", @"Atención", MessageBoxButtons.YesNo,
                   MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Limpiar(this);
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        public virtual void FBaseABM_Load(object sender, EventArgs e)
        {
            EstiloControlHelper.AplicarEstiloALabels(this);

            if (TipoOperacion == TipoOperacion.Eliminar || TipoOperacion == TipoOperacion.Modificar)
            {
                CargarDatos(EntidadID);

            }
            else
            {
                Limpiar(this);
            }
        }

        public virtual void CargarDatos(long? entidadId)
        {
            
        }
        public virtual void EjecutarComando()
        {
            switch (TipoOperacion)
            {
                case TipoOperacion.Nuevo:
                    if (EjecutarComandoNuevo())
                    {
                        //MessageBox.Show(@"Los datos se Guardaron Correctamente.", @"Atención", MessageBoxButtons.OK,
                        //    MessageBoxIcon.Information);
                        Limpiar(this);
                        RealizoAlgunaOperacion = true;
                    }
                    break;
                case TipoOperacion.Eliminar:
                    if (EjecutarComandoEliminar())
                    {
                        //MessageBox.Show(@"Los datos se Eliminaron Correctamente.", @"Atención", MessageBoxButtons.OK,
                        //    MessageBoxIcon.Information);
                        RealizoAlgunaOperacion = true;
                        this.Close();
                    }
                    break;
                case TipoOperacion.Modificar:
                    if (EjecutarComandoModificar())
                    {
                        //MessageBox.Show(@"Los datos se Modificaron Correctamente.", @"Atención", MessageBoxButtons.OK,
                        //    MessageBoxIcon.Information);
                        RealizoAlgunaOperacion = true;
                        this.Close();
                    }
                    break;
            }
        }

        public virtual bool EjecutarComandoModificar()
        {
            return false;
        }

        public virtual bool EjecutarComandoEliminar()
        {
            return false;
        }

        public virtual bool EjecutarComandoNuevo()
        {
            return false;
        }
        public virtual void Inicializador(long? entidadId)
        {

        }

        //private void AplicarEstiloALabels(Control contenedor) ESTO SOLO ESTA POR LOS COMENTARIOS SE PUEDE BORRAR
        //{
        //    if (contenedor == null) return;

        //    foreach (Control control in contenedor.Controls)
        //    {
        //        // Si encontramos un Label, le aplicamos los cambios directamente
        //        if (control is Label lbl)
        //        {
        //            // Usamos Segoe UI, que renderiza mucho mejor en pantallas modernas
        //            lbl.Font = new Font("Segoe UI", 9F, FontStyle.Regular);

        //            // Un gris oscuro neutral (Antracita) en lugar de negro puro (#000). 
        //            // Esto reduce la fatiga visual y suaviza el diseño.
        //            lbl.ForeColor = Color.FromArgb(50, 51, 53);

        //            // OPCIONAL: Si tenés labels que actúan como títulos o textos secundarios,
        //            // podés usar la propiedad .Tag en el diseñador para diferenciarlos:
        //            if (lbl.Tag?.ToString() == "Titulo")
        //            {
        //                lbl.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
        //                lbl.ForeColor = Color.FromArgb(30, 30, 30);
        //            }
        //        }
        //        // También te sugiero incluir los CheckBox acá, ya que el texto que tienen al lado
        //        // en WinForms se comporta y se lee igual que un Label
        //        else if (control is CheckBox chk)
        //        {
        //            chk.Font = new Font("Segoe UI", 9F, FontStyle.Regular);
        //            chk.ForeColor = Color.FromArgb(50, 51, 53);
        //        }
        //    }

        //private void AplicarEstiloALabels(Control contenedor)
        //{
        //    if (contenedor == null) return;

        //    foreach (Control control in contenedor.Controls)
        //    {
        //        // 1. Aplicamos el estilo al control actual si corresponde
        //        if (control is Label lbl && lbl.Tag?.ToString() != "NoModificarConBase")
        //        {
        //            lbl.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
        //            lbl.ForeColor = Color.FromArgb(50, 51, 53);

        //            if (lbl.Tag?.ToString() == "NoModificarConBase")
        //            {
        //                //lbl.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
        //                //lbl.ForeColor = Color.FromArgb(30, 30, 30);
        //            }
        //        }
        //        else if (control is CheckBox chk)
        //        {
        //            chk.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
        //            chk.ForeColor = Color.FromArgb(50, 51, 53);
        //        }

        //        // 2. CORRECCIÓN RECURSIVA: Si el control tiene hijos, entramos a revisarlos también
        //        if (control.HasChildren)
        //        {
        //            AplicarEstiloALabels(control);
        //        }
        //    }
        //}
    }
    
}
