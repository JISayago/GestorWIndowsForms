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

        public List<NotificacionDTO> ObtenerNotificacionesProdutosVencidos()
        {
            var notis = new List<Notificacion>();
            using (var context = new GestorContextDBFactory().CreateDbContext(null))
            {
                notis = context.Notificaciones
                    .AsNoTracking()
                    .Where(x => x.EstaLeida != true && x.Titulo.Contains("Lote por vencer:"))
                    .OrderByDescending(n => n.FechaCreacion)
                    .ToList();
            }

            if (notis == null || !notis.Any())
            {
                return new List<NotificacionDTO>();
            }

            var resultadoDTO = notis.Select(x => new NotificacionDTO
            {
                NotificacionId = x.NotificacionId,
                Titulo = x.Titulo,
                Descripcion = x.Descripcion,
                Mensaje = x.Mensaje,
                FechaCreacion = x.FechaCreacion,
                Leida = x.EstaLeida,
                FechaNotificacion = x.FechaVencimiento ?? DateTime.Now,
                NivelUrgencia = CalcularNivelUrgencia(x.FechaVencimiento)
            }).ToList();

            return resultadoDTO;
        }

        public void NotifiacionesProductosVencidos()
        {
            // 1. Obtenemos los lotes por vencer desde el servicio
            var productosNotificar = _loteServicio.ObtenerLotesPorVencer(7);

            if (productosNotificar == null || !productosNotificar.Any()) return;

            // 2. Validar cuáles notificaciones ya existen en la BD por su Título
            var titulosPotenciales = productosNotificar
                .Select(p => $"Lote por vencer: {p.NumeroLote} ")
                .Distinct()
                .ToList();

            List<string> titulosExistentes;
            using (var context = new GestorContextDBFactory().CreateDbContext(null))
            {
                titulosExistentes = context.Notificaciones
                    .Where(n => titulosPotenciales.Contains(n.Titulo))
                    .Select(n => n.Titulo)
                    .ToList();
            }

            // Filtramos la lista original para quedarnos solo con los que NO existen en la BD
            var productosNuevos = productosNotificar
                .Where(p => !titulosExistentes.Contains($"Lote por vencer: {p.NumeroLote} "))
                .ToList();

            if (!productosNuevos.Any()) return;

            // 3. Mapeamos DIRECTO a la entidad Notificacion (Chau paso intermedio innecesario)
            var entidadesBD = productosNuevos.Select(p => new Notificacion
            {
                Titulo = $"Lote por vencer: {p.NumeroLote} ",
                Descripcion = $"El producto {p.NombreProducto} - Registra vencimiento el {p.FechaVencimiento?.ToString("dd/MM/yyyy") ?? "N/A"}.",
                Mensaje = p.EstaVencidoDescripcion,
                FechaCreacion = DateTime.Now,
                FechaVencimiento = p.FechaVencimiento,
                EstaLeida = false,
                EmpleadoId = null // Ahora que es nullable, entra como alerta general del sistema
            }).ToList();

            // 4. Persistencia en la base de datos
            using (var context = new GestorContextDBFactory().CreateDbContext(null))
            {
                context.AddRange(entidadesBD);
                context.SaveChanges();
            }
        }

        public List<NotificacionDTO> ObtenerNotificacionesOfertasVencidas()
        {
            var notis = new List<Notificacion>();
            using (var context = new GestorContextDBFactory().CreateDbContext(null))
            {
                notis = context.Notificaciones
                    .AsNoTracking()
                    .Where(x => x.EstaLeida != true && x.Titulo.Contains("Oferta vencida:"))
                    .OrderByDescending(n => n.FechaCreacion)
                    .ToList();
            }

            if (notis == null || !notis.Any())
            {
                return new List<NotificacionDTO>();
            }

            var resultadoDTO = notis.Select(x => new NotificacionDTO
            {
                NotificacionId = x.NotificacionId,
                Titulo = x.Titulo,
                Descripcion = x.Descripcion,
                Mensaje = x.Mensaje,
                FechaCreacion = x.FechaCreacion,
                Leida = x.EstaLeida,
                FechaNotificacion = x.FechaVencimiento ?? DateTime.Now,
                NivelUrgencia = CalcularNivelUrgencia(x.FechaVencimiento) // Reutiliza tu lógica centralizada
            }).ToList();

            return resultadoDTO;
        }

        //public void NotificacionesOfertasVencidas()
        //{
        //    // 1. Obtenemos las ofertas vencidas desde el servicio
        //    var promocionesNotificar = _ofertaServicio.ObtenerOfertasVencidas(7);

        //    if (promocionesNotificar == null || !promocionesNotificar.Any()) return;

        //    // 2. Generamos títulos únicos basados en el código de oferta para controlar duplicados
        //    var titulosPotenciales = promocionesNotificar
        //        .Select(p => $"Oferta vencida: {p.Codigo}")
        //        .Distinct()
        //        .ToList();

        //    List<string> titulosExistentes;
        //    using (var context = new GestorContextDBFactory().CreateDbContext(null))
        //    {
        //        titulosExistentes = context.Notificaciones
        //            .Where(n => titulosPotenciales.Contains(n.Titulo))
        //            .Select(n => n.Titulo)
        //            .ToList();
        //    }

        //    // Filtramos para dejar solo las que no se guardaron todavía
        //    var promocionesNuevas = promocionesNotificar
        //        .Where(p => !titulosExistentes.Contains($"Oferta vencida: {p.Codigo}"))
        //        .ToList();

        //    if (!promocionesNuevas.Any()) return;

        //    // 3. Mapeo a la entidad base de la base de datos
        //    var entidadesBD = promocionesNuevas.Select(p => new Notificacion
        //    {
        //        Titulo = $"Oferta vencida: {p.Codigo}",
        //        Descripcion = $"La oferta {p.Codigo} - {p.Descripcion} venció el {p.FechaFin?.ToString("dd/MM/yyyy") ?? "N/A"}.",
        //        Mensaje = "La promoción ha cumplido su fecha límite de vigencia.",
        //        FechaCreacion = DateTime.Now,
        //        FechaVencimiento = p.FechaFin, // Seteamos el DateTime? para calcular la urgencia después
        //        EstaLeida = false,
        //        EmpleadoId = null // Alerta general del sistema
        //    }).ToList();

        //    // 4. Guardamos en lote
        //    using (var context = new GestorContextDBFactory().CreateDbContext(null))
        //    {
        //        context.AddRange(entidadesBD);
        //        context.SaveChanges();
        //    }
        //}

        public List<NotificacionDTO> ObtenerNotificacionesCtaCteVencidas()
        {
            var notis = new List<Notificacion>();
            using (var context = new GestorContextDBFactory().CreateDbContext(null))
            {
                notis = context.Notificaciones
                    .AsNoTracking()
                    .Where(x => x.EstaLeida != true && x.Titulo.Contains("CtaCte vencida:"))
                    .OrderByDescending(n => n.FechaCreacion)
                    .ToList();
            }

            if (notis == null || !notis.Any())
            {
                return new List<NotificacionDTO>();
            }

            var resultadoDTO = notis.Select(x => new NotificacionDTO
            {
                NotificacionId = x.NotificacionId,
                Titulo = x.Titulo,
                Descripcion = x.Descripcion,
                Mensaje = x.Mensaje,
                FechaCreacion = x.FechaCreacion,
                Leida = x.EstaLeida,
                FechaNotificacion = x.FechaVencimiento ?? DateTime.Now,
                NivelUrgencia = CalcularNivelUrgencia(x.FechaVencimiento)
            }).ToList();

            return resultadoDTO;
        }

        public void NotificacionesCtaCteVencidas()
        {
            // 1. Obtenemos las cuentas vencidas desde el servicio
            var cuentasNotificar = _cuentaCorrienteServicio.ObtenerCtaCteVencidas(7);

            if (cuentasNotificar == null || !cuentasNotificar.Any()) return;

            // 2. Títulos únicos por nombre de cuenta corriente
            var titulosPotenciales = cuentasNotificar
                .Select(p => $"CtaCte vencida: {p.NombreCuentaCorriente}")
                .Distinct()
                .ToList();

            List<string> titulosExistentes;
            using (var context = new GestorContextDBFactory().CreateDbContext(null))
            {
                titulosExistentes = context.Notificaciones
                    .Where(n => titulosPotenciales.Contains(n.Titulo))
                    .Select(n => n.Titulo)
                    .ToList();
            }

            var cuentasNuevas = cuentasNotificar
                .Where(p => !titulosExistentes.Contains($"CtaCte vencida: {p.NombreCuentaCorriente}"))
                .ToList();

            if (!cuentasNuevas.Any()) return;

            // 3. Mapeo a la entidad limpia
            var entidadesBD = cuentasNuevas.Select(p => new Notificacion
            {
                Titulo = $"CtaCte vencida: {p.NombreCuentaCorriente}",
                Descripcion = $"Cuenta corriente de {p.NombreCliente} registra fecha de vencimiento el {p.FechaVencimiento?.ToString("dd/MM/yyyy") ?? "N/A"}.",
                Mensaje = "Revisar saldo pendiente e historial de pagos del cliente.",
                FechaCreacion = DateTime.Now,
                FechaVencimiento = p.FechaVencimiento,
                EstaLeida = false,
                EmpleadoId = null // Alerta general de administración
            }).ToList();

            // 4. Persistencia
            using (var context = new GestorContextDBFactory().CreateDbContext(null))
            {
                context.AddRange(entidadesBD);
                context.SaveChanges();
            }
        }
        public void MarcarNotificacionComoLeida(long notificacionId)
        {
            using (var context = new GestorContextDBFactory().CreateDbContext(null))
            {
                var notificacion = context.Notificaciones.FirstOrDefault(n => n.NotificacionId == notificacionId);
                if (notificacion != null)
                {
                    notificacion.EstaLeida = true;
                    context.SaveChanges();
                }
            }
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