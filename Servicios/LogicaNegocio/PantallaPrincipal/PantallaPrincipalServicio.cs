using Servicios.LogicaNegocio.CuentaCorriente;
using Servicios.LogicaNegocio.PantallaPrincipal.DTO;
using Servicios.LogicaNegocio.Producto;
using Servicios.LogicaNegocio.Producto.Lote;
using Servicios.LogicaNegocio.Venta.Oferta;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios.LogicaNegocio.PantallaPrincipal
{
    public class PantallaPrincipalServicio : IPantallaPrincipalServicio
    {
        private readonly ILoteServicio _loteServicio;
        private readonly IOfertaServicio _ofertaServicio;
        private readonly ICuentaCorrienteServicio _cuentaCorrienteServicio;

        //USAR CONFIG DEL SISTEMA PARA MOSTRAR O NO CIERTAS NOTIFICACIONES

        public PantallaPrincipalServicio()
        {
            //inicializar los servicios necesarios para obtener los productos y ofertas vencidos
            _ofertaServicio = new OfertaServicio();
            _loteServicio = new LoteServicio();
            _cuentaCorrienteServicio = new CuentaCorrienteServicio();
        }

        public List<NotificacionPP> notifiacionesProductosVencidos(int cantidadDiasABuscar)
        {
            //usar el service de lotes para obtener los productos que estan vencidos y crear una notificacion para cada uno de ellos
            var productosNotificar = _loteServicio.ObtenerLotesVencidos(cantidadDiasABuscar);

            return productosNotificar.Select(p => new NotificacionPP
            {
                NotificacionId = p.Id, //o algun id unico para la notificacion
                Titulo = $"Producto: {p.NombreProducto}  ",
                Descripcion = $"Lote: {p.NumeroLote} con fecha de vencimiento: {p.FechaVencimiento?.ToString("dd/MM/yyyy")}.",
                FechaNotificacion = DateTime.Now,
                Leida = false,
                NivelUrgencia = (int)(p.FechaVencimiento.HasValue
                    ? (p.FechaVencimiento.Value.Date < DateTime.Now.Date
                        ? Helpers.Sistema.NivelUrgencia.Alta
                        : (p.FechaVencimiento.Value.Date <= DateTime.Now.Date.AddDays(3)
                            ? Helpers.Sistema.NivelUrgencia.Media
                            : Helpers.Sistema.NivelUrgencia.Baja))
                    : Helpers.Sistema.NivelUrgencia.Baja) // Si no tiene fecha de vencimiento, se considera baja urgencia
            }).ToList();
        }

        public List<NotificacionPP> notifiacionesOfertasVencidas(int cantidadDiasABuscar)
        {
            var promocionesNotificar = _ofertaServicio.ObtenerOfertasVencidas(cantidadDiasABuscar);

            return promocionesNotificar.Select(p => new NotificacionPP
            {
                NotificacionId = p.OfertaDescuentoId, // o algún ID único para la notificación
                Titulo = "Oferta Vencida",
                Descripcion = $"La oferta {p.Codigo} - {p.Descripcion} venció el {p.FechaFin.Value:dd/MM/yyyy}.",
                FechaNotificacion = DateTime.Now,
                Leida = false,
                NivelUrgencia = (int)(p.FechaFin.HasValue
                    ? (p.FechaFin.Value.Date < DateTime.Now.Date
                        ? Helpers.Sistema.NivelUrgencia.Alta
                        : (p.FechaFin.Value.Date <= DateTime.Now.Date.AddDays(3)
                            ? Helpers.Sistema.NivelUrgencia.Media
                            : Helpers.Sistema.NivelUrgencia.Baja))
                    : Helpers.Sistema.NivelUrgencia.Baja) // Si no tiene fecha de vencimiento, se considera baja urgencia
            }).ToList();
        }

        public List<NotificacionPP> notifiacionesCtaCteVencidas(int cantidadDiasABuscar)
        {
            var productosNotificar = _cuentaCorrienteServicio.ObtenerCtaCteVencidas(cantidadDiasABuscar);

            return productosNotificar.Select(p => new NotificacionPP
            {
                NotificacionId = p.CuentaCorrienteId, //o algun id unico para la notificacion
                Titulo = $"Nombre Ctate: {p.NombreCuentaCorriente}  ",
                Descripcion = $"Del Clientes: {p.NombreCliente} con fecha de vencimiento: {p.FechaVencimiento?.ToString("dd/MM/yyyy")}.",
                FechaNotificacion = DateTime.Now,
                Leida = false,
                NivelUrgencia = (int)(p.FechaVencimiento.HasValue
                    ? (p.FechaVencimiento.Value.Date < DateTime.Now.Date
                        ? Helpers.Sistema.NivelUrgencia.Alta
                        : (p.FechaVencimiento.Value.Date <= DateTime.Now.Date.AddDays(3)
                            ? Helpers.Sistema.NivelUrgencia.Media
                            : Helpers.Sistema.NivelUrgencia.Baja))
                    : Helpers.Sistema.NivelUrgencia.Baja) // Si no tiene fecha de vencimiento, se considera baja urgencia
            }).ToList();
        }
    }
}