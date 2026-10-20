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

        public void NotificacionesOfertasVencidas()
        {
            // 1. Obtenemos las ofertas vencidas desde el servicio
            var promocionesNotificar = _ofertaServicio.ObtenerOfertasVencidas(7);

            if (promocionesNotificar == null || !promocionesNotificar.Any()) return;

            // 2. Generamos títulos únicos basados en el código de oferta para controlar duplicados
            var titulosPotenciales = promocionesNotificar
                .Select(p => $"Oferta vencida: {p.Codigo}")
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

            // Filtramos para dejar solo las que no se guardaron todavía
            var promocionesNuevas = promocionesNotificar
                .Where(p => !titulosExistentes.Contains($"Oferta vencida: {p.Codigo}"))
                .ToList();

            if (!promocionesNuevas.Any()) return;

            // 3. Mapeo a la entidad base de la base de datos
            var entidadesBD = promocionesNuevas.Select(p => new Notificacion
            {
                Titulo = $"Oferta vencida: {p.Codigo}",
                Descripcion = $"La oferta {p.Codigo} - {p.Descripcion} venció el {p.FechaFin?.ToString("dd/MM/yyyy") ?? "N/A"}.",
                Mensaje = "La promoción ha cumplido su fecha límite de vigencia.",
                FechaCreacion = DateTime.Now,
                FechaVencimiento = p.FechaFin, // Seteamos el DateTime? para calcular la urgencia después
                EstaLeida = false,
                EmpleadoId = null // Alerta general del sistema
            }).ToList();

            // 4. Guardamos en lote
            using (var context = new GestorContextDBFactory().CreateDbContext(null))
            {
                context.AddRange(entidadesBD);
                context.SaveChanges();
            }
        }

        public List<NotificacionDTO> ObtenerNotificacionesOfertasBajoStock()
        {
            var notis = new List<Notificacion>();
            using (var context = new GestorContextDBFactory().CreateDbContext(null))
            {
                notis = context.Notificaciones
                    .AsNoTracking()
                    .Where(x => x.EstaLeida != true && x.Titulo.Contains("Oferta con bajo stock:"))
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
                // El bajo stock no tiene "fecha de vencimiento" real; lo urgente acá es la falta
                // de stock en sí, así que se prioriza siempre como Alta (no depende de una fecha).
                FechaNotificacion = x.FechaCreacion,
                NivelUrgencia = (int)Helpers.Sistema.NivelUrgencia.Alta
            }).ToList();

            return resultadoDTO;
        }

        public void NotificacionesOfertasBajoStock()
        {
            // 1. Obtenemos los productos de ofertas activas cuyo stock ya no alcanza
            var productosNotificar = _ofertaServicio.ObtenerOfertasConBajoStock();

            if (productosNotificar == null || !productosNotificar.Any()) return;

            // 2. Título único por combinación Oferta + Producto, para poder controlar duplicados
            var titulosPotenciales = productosNotificar
                .Select(p => $"Oferta con bajo stock: {p.Codigo} - {p.NombreProducto}")
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

            // Filtramos las que ya se notificaron antes y siguen sin leerse/resolverse
            var productosNuevos = productosNotificar
                .Where(p => !titulosExistentes.Contains($"Oferta con bajo stock: {p.Codigo} - {p.NombreProducto}"))
                .ToList();

            if (!productosNuevos.Any()) return;

            // 3. Mapeo a la entidad de notificación
            var entidadesBD = productosNuevos.Select(p => new Notificacion
            {
                Titulo = $"Oferta con bajo stock: {p.Codigo} - {p.NombreProducto}",
                Descripcion = $"La oferta {p.Codigo} necesita {p.CantidadRequerida} unidad(es) de {p.NombreProducto} por aplicación, pero sólo quedan {p.StockActual} en stock.",
                Mensaje = "Considerar reponer stock o desactivar la oferta para este producto.",
                FechaCreacion = DateTime.Now,
                FechaVencimiento = null, // No aplica: no es una alerta por fecha, sino por stock
                EstaLeida = false,
                EmpleadoId = null // Alerta general del sistema
            }).ToList();

            // 4. Persistencia en lote
            using (var context = new GestorContextDBFactory().CreateDbContext(null))
            {
                context.AddRange(entidadesBD);
                context.SaveChanges();
            }
        }

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
        /// <summary>
        /// Avisos pendientes (no leídos) con fecha de vencimiento dentro del rango, para marcar los días
        /// del calendario. Los avisos de bajo stock no tienen fecha, así que no aparecen. Un aviso leído
        /// deja de figurar: el calendario muestra lo mismo que las listas de avisos.
        /// </summary>
        public List<VencimientoCalendarioDTO> ObtenerVencimientosCalendario(DateTime desde, DateTime hasta)
        {
            DateTime inicio = desde.Date;
            DateTime finExclusivo = hasta.Date.AddDays(1);

            List<Notificacion> notis;
            using (var context = new GestorContextDBFactory().CreateDbContext(null))
            {
                notis = context.Notificaciones
                    .AsNoTracking()
                    .Where(n => n.EstaLeida != true
                                && n.FechaVencimiento != null
                                && n.FechaVencimiento >= inicio
                                && n.FechaVencimiento < finExclusivo)
                    .ToList();
            }

            var resultado = new List<VencimientoCalendarioDTO>();
            foreach (var n in notis)
            {
                TipoVencimientoCalendario? tipo = ClasificarPorTitulo(n.Titulo);
                if (tipo == null || n.FechaVencimiento == null)
                    continue;

                resultado.Add(new VencimientoCalendarioDTO
                {
                    Fecha = n.FechaVencimiento.Value.Date,
                    Tipo = tipo.Value
                });
            }

            return resultado;
        }

        /// <summary>Avisos pendientes (no leídos) que vencen en la fecha indicada, de lotes, ofertas y cuentas corrientes.</summary>
        public List<AvisoDelDiaDTO> ObtenerAvisosDelDia(DateTime fecha)
        {
            DateTime inicio = fecha.Date;
            DateTime finExclusivo = inicio.AddDays(1);

            List<Notificacion> notis;
            using (var context = new GestorContextDBFactory().CreateDbContext(null))
            {
                notis = context.Notificaciones
                    .AsNoTracking()
                    .Where(n => n.EstaLeida != true
                                && n.FechaVencimiento != null
                                && n.FechaVencimiento >= inicio
                                && n.FechaVencimiento < finExclusivo)
                    .OrderBy(n => n.Titulo)
                    .ToList();
            }

            var resultado = new List<AvisoDelDiaDTO>();
            foreach (var n in notis)
            {
                TipoVencimientoCalendario? tipo = ClasificarPorTitulo(n.Titulo);
                if (tipo == null)
                    continue;

                resultado.Add(new AvisoDelDiaDTO
                {
                    Tipo = tipo.Value,
                    Aviso = new NotificacionDTO
                    {
                        NotificacionId = n.NotificacionId,
                        Titulo = n.Titulo,
                        Descripcion = n.Descripcion,
                        Mensaje = n.Mensaje,
                        FechaCreacion = n.FechaCreacion,
                        Leida = n.EstaLeida,
                        FechaNotificacion = n.FechaVencimiento ?? DateTime.Now,
                        NivelUrgencia = CalcularNivelUrgencia(n.FechaVencimiento)
                    }
                });
            }

            return resultado;
        }

        private static TipoVencimientoCalendario? ClasificarPorTitulo(string titulo)
        {
            if (string.IsNullOrEmpty(titulo))
                return null;
            if (titulo.StartsWith("Lote por vencer:", StringComparison.Ordinal))
                return TipoVencimientoCalendario.Lote;
            if (titulo.StartsWith("Oferta vencida:", StringComparison.Ordinal))
                return TipoVencimientoCalendario.Oferta;
            if (titulo.StartsWith("CtaCte vencida:", StringComparison.Ordinal))
                return TipoVencimientoCalendario.CuentaCorriente;
            return null;
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

        /// <summary>
        /// Devuelve el DNI del cliente dueño de la cuenta corriente con ese nombre (el que figura en
        /// el título del aviso), o null si no se encuentra. Sirve para abrir la consulta de clientes
        /// filtrada por ese cliente.
        /// </summary>
        public string ObtenerDniClientePorNombreCuentaCorriente(string nombreCuentaCorriente)
        {
            if (string.IsNullOrWhiteSpace(nombreCuentaCorriente))
                return null;

            var nombre = nombreCuentaCorriente.Trim();

            using (var context = new GestorContextDBFactory().CreateDbContext(null))
            {
                return context.CuentaCorriente
                    .AsNoTracking()
                    .Where(c => c.NombreCuentaCorriente == nombre)
                    .Select(c => c.Cliente.Persona.Dni)
                    .FirstOrDefault();
            }
        }

        /// <summary>
        /// Devuelve el código de la oferta de un aviso de bajo stock. El título tiene la forma
        /// "Oferta con bajo stock: {Codigo} - {Producto}"; como el código puede escribirse a mano y
        /// contener " - ", primero se lo busca entre las ofertas que hoy tienen bajo stock y, si ya
        /// no figura ahí, se toma lo que está antes del primer " - ".
        /// </summary>
        public string ObtenerCodigoOfertaDeAvisoBajoStock(string tituloAviso)
        {
            const string prefijo = "Oferta con bajo stock:";

            if (string.IsNullOrWhiteSpace(tituloAviso) ||
                !tituloAviso.StartsWith(prefijo, StringComparison.OrdinalIgnoreCase))
                return null;

            var resto = tituloAviso.Substring(prefijo.Length).Trim();
            if (resto.Length == 0)
                return null;

            var coincidencia = _ofertaServicio.ObtenerOfertasConBajoStock()
                .FirstOrDefault(o => $"{o.Codigo} - {o.NombreProducto}" == resto);

            if (coincidencia != null)
                return coincidencia.Codigo;

            var corte = resto.IndexOf(" - ", StringComparison.Ordinal);
            var codigo = corte > 0 ? resto.Substring(0, corte) : resto;
            return string.IsNullOrWhiteSpace(codigo) ? null : codigo.Trim();
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