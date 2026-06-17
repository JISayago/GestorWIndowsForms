using AccesoDatos;
using AccesoDatos.Entidades;
using Microsoft.EntityFrameworkCore;
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

        /// <summary>
        /// Función auxiliar para determinar la urgencia según los días restantes del lote
        /// </summary>
        private int CalcularNivelUrgencia(DateTime? fechaVencimiento)
        {
            if (!fechaVencimiento.HasValue)
            {
                return (int)Helpers.Sistema.NivelUrgencia.Baja;
            }

            int diasRestantes = (fechaVencimiento.Value.Date - DateTime.Now.Date).Days;

            if (diasRestantes < 0)
            {
                return (int)Helpers.Sistema.NivelUrgencia.Alta; // Ya se venció
            }
            if (diasRestantes <= 3)
            {
                return (int)Helpers.Sistema.NivelUrgencia.Media; // Vence en 3 días o menos
            }

            return (int)Helpers.Sistema.NivelUrgencia.Baja; // Falta más de una semana
        }

        public List<NotificacionDTO> NotifiacionesProductosVencidos()
        {
            // 1. Obtenemos los lotes por vencer desde el servicio
            var productosNotificar = _loteServicio.ObtenerLotesPorVencer(7);

            if (productosNotificar == null || productosNotificar.Count == 0)
            {
                return new List<NotificacionDTO>();
            }

            // 2. Creamos una estructura intermedia para no perder la relación Lote <-> Entidad
            var mapaNotificaciones = productosNotificar.Select(p => new
            {
                Lote = p,
                Entidad = new Notificacion
                {
                    Titulo = $"Lote por vencer: {p.NombreProducto}",
                    Descripcion = $"El producto {p.NombreProducto} - Lote {p.NumeroLote} registra vencimiento el {p.FechaVencimiento?.ToString("dd/MM/yyyy") ?? "N/A"}.",
                    Mensaje = p.EstaVencidoDescripcion, // Aprovechamos la propiedad calculada de tu LoteDTO
                    FechaCreacion = DateTime.Now,
                    FechaConfirmacion = DateTime.Now,
                    EstaLeida = false
                }
            }).ToList();

            //// 3. Extraemos solo las entidades puras para persistir en Entity Framework
            var entidadesBD = mapaNotificaciones.Select(x => x.Entidad).ToList();

            //using (var context = new GestorContextDBFactory().CreateDbContext(null))
            //{
            //    context.AddRange(entidadesBD);
            //    context.SaveChanges(); // La BD genera los NotificacionId reales acá
            //}

            // 4. Construimos la lista de DTOs cruzando los datos de la Entidad (BD) y del Lote original
            var resultadoDTO = mapaNotificaciones.Select(x => new NotificacionDTO
            {
                NotificacionId = x.Entidad.NotificacionId, // ID asignado por la BD
                Titulo = x.Entidad.Titulo,
                Descripcion = x.Entidad.Descripcion,
                Mensaje = x.Entidad.Mensaje,
                EmpleadoId = x.Entidad.EmpleadoId,
                FechaCreacion = x.Entidad.FechaCreacion,
                FechaConfirmacion = x.Entidad.FechaConfirmacion,
                Leida = x.Entidad.EstaLeida, // Mapeo de EstaLeida -> Leida

                // Información calculada que requiere el DTO mapeada desde el Lote original
                FechaNotificacion = x.Lote.FechaVencimiento ?? DateTime.Now,
                NivelUrgencia = CalcularNivelUrgencia(x.Lote.FechaVencimiento)
            }).ToList();

            return resultadoDTO;
        }

        public List<NotificacionDTO> NotifiacionesOfertasVencidas()
        {
            var promocionesNotificar = _ofertaServicio.ObtenerOfertasVencidas(7);

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

        public List<NotificacionDTO> NotifiacionesCtaCteVencidas()
        {
            var productosNotificar = _cuentaCorrienteServicio.ObtenerCtaCteVencidas(7);

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
                    Egresos = cajaDTO.TotalEgresos,
                    TotalCaja = cajaDTO.SaldoActual,
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
                    Egresos = cajaDTO.TotalEgresos,
                    TotalCaja = cajaDTO.SaldoActual,
                    CajaAbierta = !cajaDTO.EstaCerrada,
                    UsuarioId = datosTurno.UsuarioId,
                    UsuarioLogeado = datosTurno.UsuarioLogeado,
                    HoraIngresoUsuario = datosTurno.HoraIngresoUsuario,
                    NotasTurno = datosTurno.NotasTurno
                };
            }
        }

        public void GuardarNotasRapidas(string textoLimpio, string nombreUsuario)
        {
            var context = new AccesoDatos.GestorContextDBFactory().CreateDbContext(null);

            var notaExistente = context.NotasRapidas.FirstOrDefault(x => x.NotaId == 1);

            if (notaExistente != null)
            {
                // Actualizamos
                notaExistente.Cuerpo = textoLimpio;
                notaExistente.FechaModificacion = DateTime.Now;
                notaExistente.UsuarioNombre = nombreUsuario;
            }
            else
            {
                // Si por alguna razón no existe (primera vez), la creamos con ID 1
                context.NotasRapidas.Add(new NotaRapida
                {
                    NotaId = 1,
                    Cuerpo = textoLimpio,
                    FechaModificacion = DateTime.Now,
                    UsuarioNombre = nombreUsuario
                });
            }

            context.SaveChanges();
        }

        public string? ObtenerNotasRapidas()
        {
            var context = new AccesoDatos.GestorContextDBFactory().CreateDbContext(null);
            return context.NotasRapidas.AsNoTracking().Where(x => x.NotaId == 1).Select(x => x.Cuerpo.ToString()).FirstOrDefault();
        }
    }
}