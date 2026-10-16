using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentacion.FBase
{
    public partial class FBase
    {
        protected void Informar(string mensaje, string titulo = null)
            => Mensajes.Informacion(mensaje, titulo, owner: this);

        protected void Exito(string mensaje, string titulo = null)
            => Mensajes.Exito(mensaje, titulo, owner: this);

        protected void Advertir(string mensaje, string titulo = null)
            => Mensajes.Advertencia(mensaje, titulo, owner: this);

        protected void MostrarError(string mensaje, Exception ex = null)
        {
            if (ex != null) Mensajes.Error(ex, mensaje, owner: this);
            else Mensajes.Error(mensaje, owner: this);
        }

        protected bool Confirmar(string mensaje, string titulo = null,
            string si = "Sí", string no = "No", bool noPorDefecto = false)
            => Mensajes.Confirmar(mensaje, titulo, si, no,
                noPorDefecto ? TipoMensaje.Advertencia : TipoMensaje.Pregunta, noPorDefecto, this);
    }
}
