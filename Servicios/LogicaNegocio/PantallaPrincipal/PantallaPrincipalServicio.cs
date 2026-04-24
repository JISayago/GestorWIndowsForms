using Servicios.LogicaNegocio.Caja;
using Servicios.LogicaNegocio.CuentaCorriente;
using Servicios.LogicaNegocio.Empleado;
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
        private readonly IEmpleadoServicio _empleadoServicio;
        //private readonly ICajaServicio _cajaServicio;
        public CajaServicio caja = new CajaServicio();

        //USAR CONFIG DEL SISTEMA PARA MOSTRAR O NO CIERTAS NOTIFICACIONES

        public PantallaPrincipalServicio()
        {
            //inicializar los servicios necesarios para obtener los productos y ofertas vencidos
            _ofertaServicio = new OfertaServicio();
            _loteServicio = new LoteServicio();
            _cuentaCorrienteServicio = new CuentaCorrienteServicio();
            _empleadoServicio = new EmpleadoServicio();
            //_cajaServicio = new CajaServicio();
        }

        public List<NotificacionDTO> NotifiacionesProductosVencidos(int cantidadDiasABuscar)
        {
            //usar el service de lotes para obtener los productos que estan vencidos y crear una notificacion para cada uno de ellos
            var productosNotificar = _loteServicio.ObtenerLotesVencidos(cantidadDiasABuscar);

            return productosNotificar.Select(p => new NotificacionDTO
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

        public List<NotificacionDTO> NotifiacionesOfertasVencidas(int cantidadDiasABuscar)
        {
            var promocionesNotificar = _ofertaServicio.ObtenerOfertasVencidas(cantidadDiasABuscar);

            return promocionesNotificar.Select(p => new NotificacionDTO
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

        public List<NotificacionDTO> NotifiacionesCtaCteVencidas(int cantidadDiasABuscar)
        {
            var productosNotificar = _cuentaCorrienteServicio.ObtenerCtaCteVencidas(cantidadDiasABuscar);

            return productosNotificar.Select(p => new NotificacionDTO
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

        public DatosTurnoDTO ObtenerDatosTurno(long? cajaId, long usuarioId)
        {
            DatosTurnoDTO datosTurno;

            if (!cajaId.HasValue)
            {
                datosTurno = new DatosTurnoDTO
                {
                    MontoInicial = 0,
                    Ingresos = 0,
                    TotalCaja = 0,
                    CajaAbierta = false
                };
            }
            else
            {
                var cajaDTO = caja.ObtenerCajaAbierta(cajaId);
                datosTurno = new DatosTurnoDTO
                {
                    CajaId = cajaDTO.CajaId,
                    MontoInicial = cajaDTO.SaldoInicial,
                    Ingresos = cajaDTO.TotalIngresos,
                    TotalCaja = cajaDTO.BalanceFinal,
                    CajaAbierta = !cajaDTO.EstaCerrada
                };
            }

            // Ahora asignas los datos del usuario que son comunes para ambos casos
            var datosUsuario = _empleadoServicio.ObtenerDatosPanelPrincipal(usuarioId);

            datosTurno.UsuarioId = datosUsuario.UsuarioId;
            datosTurno.UsuarioLogeado = datosUsuario.UsuarioLogeado;
            datosTurno.HoraIngresoUsuario = datosUsuario.HoraIngresoUsuario;

            //aqui se pueden agregar mas datos relacionados al turno, como por ejemplo el usuario que esta logueado, o la caja que esta abierta, etc.
            return datosTurno;
        }

        public DatosTurnoDTO ObtenerActualizarDatosCaja(long? cajaId, DatosTurnoDTO datosTurno)
        {
            if (!cajaId.HasValue)
            {
                return new DatosTurnoDTO
                {
                    MontoInicial = 0,
                    Ingresos = 0,
                    TotalCaja = 0,
                    CajaAbierta = false
                };
            }
            else
            {
                var cajaDTO = caja.ObtenerCajaAbierta(cajaId);
                return new DatosTurnoDTO
                {
                    CajaId = cajaDTO.CajaId,
                    MontoInicial = cajaDTO.SaldoInicial,
                    Ingresos = cajaDTO.TotalIngresos,
                    TotalCaja = cajaDTO.BalanceFinal,
                    CajaAbierta = !cajaDTO.EstaCerrada,
                    UsuarioId = datosTurno.UsuarioId,
                    UsuarioLogeado = datosTurno.UsuarioLogeado,
                    HoraIngresoUsuario = datosTurno.HoraIngresoUsuario,
                    NotasTurno = datosTurno.NotasTurno
                };
            }
        }
    }
}