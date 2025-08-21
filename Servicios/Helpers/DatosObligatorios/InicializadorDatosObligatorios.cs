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
        }
        private void InicializarAdmin()
        {
            UsuarioInicial.Inicializar(Context);

        }
        private void IniciarTiposDePago()
        {

        }
    }
}
