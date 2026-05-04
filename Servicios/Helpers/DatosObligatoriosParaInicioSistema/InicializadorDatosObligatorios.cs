using AccesoDatos;
using AccesoDatos.Entidades;
using Servicios.Helpers.DatosObligatoriosParaInicioSistema;
using Servicios.LogicaNegocio.Sistema;
using Servicios.Seguridad;
using System;
using System.Collections.Generic;

namespace Servicios.Helpers.DatosObligatorios
{
    public class InicializadorDatosObligatorios
    {
        private InicializadorDatosObligatoriosServicio _inicializador;
        private GestorContextDB Context;
        public List<string> mensajes = new List<string>();
        public bool seCargo = false;

        public InicializadorDatosObligatorios()
        {
            _inicializador = new InicializadorDatosObligatoriosServicio();
            Context = _inicializador.ContextParaInicializar();
        }
        public void InicializarBaseMinima()
        {
            InicializarPermisos();
            InicializarRoles();
            InicializarAdmin();
        }
        public void InicializadorDatos(IProgress<(int progreso, string mensaje)> progress = null)
        {
            try
            {
                progress?.Report((30, "Configurando consumidor final..."));
                InicializarConsumidorFinal();

                progress?.Report((50, "Cargando tipos de pago..."));
                IniciarTiposDePago();

                progress?.Report((80, "Procesando ofertas..."));
                mensajes = RetornarMensajeOfertasActivadasDesactivadasConflictos();

                progress?.Report((100, "Finalizando..."));

                seCargo = true;
            }
            catch
            {
                seCargo = false;
                throw;
            }
        }

        public List<string> RetornarMensajeOfertasActivadasDesactivadasConflictos()
        {
            return OfertasActivarDesactivar.Inicializar(Context);
        }

        private void InicializarAdmin()
        {
            UsuarioInicial.Inicializar(Context);
        }
        private void IniciarTiposDePago()
        {
            TipoDePagoInicial.Inicializar(Context);
        }

        private void InicializarConsumidorFinal()
        {
            ConsumidorFInal.Inicializar(Context);
        }

        private void InicializarPermisos()
        {
            PermisosIni.Inicializar(Context);
        }

        private void InicializarRoles()
        {
           RolesIni.Inicializar(Context);
        }
        private void CargarUltimas
    }
}