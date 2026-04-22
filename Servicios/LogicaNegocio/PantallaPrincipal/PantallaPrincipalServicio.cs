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
        public PantallaPrincipalServicio()
        {
            //inicializar los servicios necesarios para obtener los productos y ofertas vencidos
            _ofertaServicio = new OfertaServicio();
            _loteServicio = new LoteServicio();
        }
        //USAR CONFIG DEL SISTEMA PARA MOSTRAR O NO CIERTAS NOTIFICACIONES

        //crear las notificaciones


        //Notificaciones de producto vencido
        //Deberia verificar los productos que estan vencidos usando el service de lotes (ya que esos porudctos usan fecha vencimiento)
        //y crear una notificacion para cada uno de ellos con el titulo "Producto Vencido" y la descripcion con el nombre del producto y la fecha de vencimiento.

        //vencimiento oferta
        //Deberia verificar las ofertas que estan vencidas usando el service de ofertas


        //Buscar un disparador de las notificacions (ejemplo cambio de dia) y ademas la posibilidad de recargar el formulario para mostrar las nuevas notificaciones.
        //cargar el formulario con las notificaciones y mostrarlas en un datagridview o algo similar

        public List<NotificacionPP> DevolverNotifiaciones()
        {
            var listaNotificaciones = new List<NotificacionPP>();

            crearNotifiaciones();

            return listaNotificaciones;
        }

        public List<NotificacionPP> crearNotifiaciones()
        {
            return checkearProductosVencidos();
            //checkearOfertasVencidas();


            //crear notificaciones de productos vencidos
            //crear notificaciones de ofertas vencidas

            //devolver la lista de notificaciones para mostrarlas en el formulario

        }

        public List<NotificacionPP> checkearProductosVencidos(DateTime? fecha = null)
        {
            //usar el service de lotes para obtener los productos que estan vencidos y crear una notificacion para cada uno de ellos
            var productosNotificar = _loteServicio.ObtenerLotesVencidos(fecha);

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

        public List<NotificacionPP> checkearOfertasVencidas(DateTime? fecha = null)
        {
            var promocionesNotificar = _ofertaServicio.ObtenerOfertasVencidas();

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
    }
}