using AccesoDatos;
using Servicios.LogicaNegocio.Empleado.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios.Helpers.DatosObligatorios
{
    public class InicializadorDatosObligatorios
    {
        private GestorContextDB Context;
        public List<string> mensajes = new List<string>();
        public bool seCargo = false;

        public InicializadorDatosObligatorios()
        {
            Context = new GestorContextDBFactory().CreateDbContext(null);
        }
        public void InicializadorDatos()
        {
            InicializarAdmin();
            IniciarTiposDePago();
            RetornarMensajeOfertasActivadasDesactivadasConflictos();
            seCargo = true;
        }
        public List<string> RetornarMensajeOfertasActivadasDesactivadasConflictos()
        {
            mensajes = OfertasActivarDesactivar.Inicializar(Context);
            return mensajes;
        }
        private void InicializarAdmin()
        {
            UsuarioInicial.Inicializar(Context);

        }
        private void IniciarTiposDePago()
        {
            TipoDePagoInicial.Inicializar(Context);
        }
        private void ActivarDesactivarOfertas()
        {
            OfertasActivarDesactivar.Inicializar(Context);
        }
    }
}
