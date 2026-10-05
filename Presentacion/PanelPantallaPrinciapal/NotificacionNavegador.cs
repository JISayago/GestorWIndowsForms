using Presentacion.Core.Cliente;
using Presentacion.Core.Producto.Lote;
using Presentacion.FBase.Helpers;
using Servicios.Helpers.Sistema.Rol;
using Servicios.LogicaNegocio.PantallaPrincipal;
using Servicios.LogicaNegocio.PantallaPrincipal.DTO;
using System;
using System.Windows.Forms;

namespace Presentacion.Notificaciones
{
    /// <summary>Tipo de aviso de la pantalla principal; define a qué consulta lleva el click.</summary>
    public enum TipoNotificacion
    {
        LoteVencido,
        OfertaVencida,
        CuentaCorrienteVencida
    }

    /// <summary>
    /// Abre la consulta correspondiente a un aviso, ya filtrada por el objeto del aviso.
    /// El objeto se identifica con el título del aviso, que es el mismo texto con el que el
    /// servicio evita duplicados (formato estable).
    /// </summary>
    public static class NotificacionNavegador
    {
        private const string PrefijoLote = "Lote por vencer:";
        private const string PrefijoOferta = "Oferta vencida:";
        private const string PrefijoCtaCte = "CtaCte vencida:";

        public static void Abrir(
            TipoNotificacion tipo,
            NotificacionDTO aviso,
            IPantallaPrincipalServicio pantallaPrincipalServicio)
        {
            if (aviso == null)
                return;

            switch (tipo)
            {
                case TipoNotificacion.LoteVencido:
                    AbrirLote(aviso);
                    break;

                case TipoNotificacion.OfertaVencida:
                    AbrirOferta(aviso);
                    break;

                case TipoNotificacion.CuentaCorrienteVencida:
                    AbrirCliente(aviso, pantallaPrincipalServicio);
                    break;
            }
        }

        private static void AbrirLote(NotificacionDTO aviso)
        {
            if (!TienePermiso("Lotes.Ver"))
                return;

            var numeroLote = ExtraerValor(aviso.Titulo, PrefijoLote);
            if (numeroLote == null)
                return;

            // La consulta de lotes oculta por defecto los vencidos: si el lote ya venció, se
            // pide el estado "Vencido" para que aparezca.
            bool yaVencio = aviso.FechaNotificacion.HasValue && aviso.FechaNotificacion.Value < DateTime.Now;

            new FLoteConsulta
            {
                BusquedaInicial = new BusquedaInicialConsulta
                {
                    TextoBuscar = numeroLote,
                    Filtro1 = "NumeroLote",
                    Filtro2 = yaVencio ? "Vencido" : null
                }
            }.Show();
        }

        private static void AbrirOferta(NotificacionDTO aviso)
        {
            if (!TienePermiso("Ofertas.Ver"))
                return;

            var codigo = ExtraerValor(aviso.Titulo, PrefijoOferta);
            if (codigo == null)
                return;

            // Una oferta vencida ya fue desactivada por el sistema: se pide "Ver ofertas inactivas"
            // para que aparezca. (Los dos checks de esa consulta son excluyentes, por eso solo uno.)
            new FOfertaConsulta
            {
                BusquedaInicial = new BusquedaInicialConsulta
                {
                    TextoBuscar = codigo,
                    Filtro1 = "Codigo",
                    Bool1 = true
                }
            }.Show();
        }

        private static void AbrirCliente(NotificacionDTO aviso, IPantallaPrincipalServicio servicio)
        {
            if (!TienePermiso("Clientes.Ver"))
                return;

            var nombreCuenta = ExtraerValor(aviso.Titulo, PrefijoCtaCte);
            if (nombreCuenta == null)
                return;

            var dni = servicio.ObtenerDniClientePorNombreCuentaCorriente(nombreCuenta);
            if (string.IsNullOrWhiteSpace(dni))
            {
                MessageBox.Show("No se encontró el cliente de esta cuenta corriente.");
                return;
            }

            // "Mostrar todos los clientes" para que aparezca aunque esté dado de baja.
            new FClienteConsulta
            {
                BusquedaInicial = new BusquedaInicialConsulta
                {
                    TextoBuscar = dni,
                    Filtro1 = "Dni",
                    Bool2 = true
                }
            }.Show();
        }

        private static bool TienePermiso(string permiso)
        {
            if (AuthHelper.Tiene(permiso))
                return true;

            MessageBox.Show("No tenés permiso para acceder a esta consulta.");
            return false;
        }

        private static string ExtraerValor(string titulo, string prefijo)
        {
            if (string.IsNullOrWhiteSpace(titulo) ||
                !titulo.StartsWith(prefijo, StringComparison.OrdinalIgnoreCase))
                return null;

            var valor = titulo.Substring(prefijo.Length).Trim();
            return valor.Length == 0 ? null : valor;
        }
    }
}
