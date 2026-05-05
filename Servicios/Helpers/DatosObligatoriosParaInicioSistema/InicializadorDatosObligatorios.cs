using AccesoDatos;
using AccesoDatos.Entidades;
using Servicios.Helpers.DatosObligatoriosParaInicioSistema;
using Servicios.LogicaNegocio.PantallaPrincipal;
using Servicios.LogicaNegocio.Producto.DTO;
using Servicios.LogicaNegocio.Producto.Lote;
using Servicios.LogicaNegocio.Sistema;
using Servicios.LogicaNegocio.Venta.DTO;
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
        public long personaId = 0;
        public long? cajaId = null;
        public List<ProductoDTO> Productos { get; private set; }
        public List<VentaDTO> Ventas { get; private set; }
        public ElementoDePanelesPantallaPrincipal DatosPantallaPrincipal { get; private set; }

        public InicializadorDatosObligatorios()
        {
            _inicializador = new InicializadorDatosObligatoriosServicio();
            Context = _inicializador.ContextParaInicializar();
        }
        public InicializadorDatosObligatorios(long personaId, long? cajaId) : this()
        {
            this.personaId = personaId;
            this.cajaId = cajaId;
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

                progress?.Report((60, "Procesando ofertas..."));
                mensajes = RetornarMensajeOfertasActivadasDesactivadasConflictos();

                // 🔥 NUEVO BLOQUE (lo importante para vos)
                progress?.Report((80, "Cargando datos de pantalla principal..."));
                InicializarDatosPantallaPrincipal();

                progress?.Report((100, "Finalizando..."));

                seCargo = true;
            }
            catch
            {
                seCargo = false;
                throw;
            }
        }

        // 🔥 NUEVO MÉTODO
        private void InicializarDatosPantallaPrincipal()
        {
            var datos = new DatosPantallaPrincipal();
            datos.Inicializar(personaId, cajaId);

            DatosPantallaPrincipal = datos.DPantallaPrincipal;

            // 🔥 Guardás también las listas
            Productos = datos.Productos;
            Ventas = datos.Ventas;
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
    }
}