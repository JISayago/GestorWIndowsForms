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

        public InicializadorDatosObligatorios()
        {
            Context = new GestorContextDBFactory().CreateDbContext(null);
        }
        public void InicializadorDatos()
        {
            InicializarAdmin();
            IniciarTiposDePago();
            ActivarDesactivarOfertas();
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
