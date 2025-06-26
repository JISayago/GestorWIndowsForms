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
            if (TipoOperacion == TipoOperacion.Eliminar || TipoOperacion == TipoOperacion.Modificar)
            {
                CargarDatos(EntidadID);

            }
            else
            {
                Limpiar(this);
            }
        }

        public virtual void CargarDatos(object entidadId)
        {
            
        }
        public virtual void EjecutarComando()
        {
            switch (TipoOperacion)
            {
                case TipoOperacion.Nuevo:
                    if (EjecutarComandoNuevo())
                    {
                        MessageBox.Show(@"Los datos se Guardaron Correctamente.", @"Atención", MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                        Limpiar(this);
                        RealizoAlgunaOperacion = true;
                    }
                    break;
                case TipoOperacion.Eliminar:
                    if (EjecutarComandoEliminar())
                    {
                        MessageBox.Show(@"Los datos se Eliminaron Correctamente.", @"Atención", MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                        RealizoAlgunaOperacion = true;
                        this.Close();
                    }
                    break;
                case TipoOperacion.Modificar:
                    if (EjecutarComandoModificar())
                    {
                        MessageBox.Show(@"Los datos se Modificaron Correctamente.", @"Atención", MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
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
    }
}
