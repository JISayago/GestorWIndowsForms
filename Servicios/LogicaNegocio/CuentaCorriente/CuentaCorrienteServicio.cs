using AccesoDatos;
using AccesoDatos.Entidades;
using Microsoft.EntityFrameworkCore;
using Servicios.Helpers.Cliente.CtaCte;
using Servicios.Helpers.Movimiento;
using Servicios.Helpers.Sistema;
using Servicios.Helpers.Sistema.FiltrosConsulta;
using Servicios.LogicaNegocio.CuentaCorriente.DTO;
using Servicios.LogicaNegocio.Movimiento;
using Servicios.LogicaNegocio.Movimiento.DTO;
using Servicios.LogicaNegocio.Producto.DTO;
using System.ComponentModel;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Servicios.LogicaNegocio.CuentaCorriente
{
    public class CuentaCorrienteServicio : ICuentaCorrienteServicio
    {
        private readonly IMovimientoServicio _movimientoServicio;
        public CuentaCorrienteServicio()
        {
            _movimientoServicio = new MovimientoServicio();
        }

        public EstadoOperacion Eliminar(long cuentacorrienteId)
        {
            using var context = new GestorContextDBFactory().CreateDbContext(null);

            var cuentacorrienteEliminar = context.CuentaCorriente.FirstOrDefault(x => x.CuentaCorrienteId == cuentacorrienteId);


            if (cuentacorrienteEliminar == null)
                throw new Exception("No se encontró la cuentacorriente.");

            // Eliminación lógica
            cuentacorrienteEliminar.EstaEliminado = true;

            context.SaveChanges();

            return new EstadoOperacion
            {
                Exitoso = true,
                Mensaje = $"La cuentacorriente fue eliminada correctamente."
            };
        }

        public EstadoOperacion Insertar(CuentaCorrienteDTO cuentacorrienteDto)
        {
            using var context = new GestorContextDBFactory().CreateDbContext(null);

            // Validaciones previas (fuera de la transacción)
            if (context.CuentaCorriente.Any(p => p.NombreCuentaCorriente == cuentacorrienteDto.NombreCuentaCorriente))
                return new EstadoOperacion { Exitoso = false, Mensaje = "Ya existe una cuentacorriente con el mismo nombre" };

            if (context.CuentaCorriente.Any(p => p.ClienteId == cuentacorrienteDto.ClienteId))
                return new EstadoOperacion { Exitoso = false, Mensaje = "El cliente ya tiene una cuenta corriente" };

            using var transaction = context.Database.BeginTransaction();

            try
            {
                var nuevaCuentaCorriente = new AccesoDatos.Entidades.CuentaCorriente
                {
                    NombreCuentaCorriente = cuentacorrienteDto.NombreCuentaCorriente,
                    Saldo = cuentacorrienteDto.Saldo,
                    LimiteDeuda = cuentacorrienteDto.LimiteDeuda,
                    LimiteDeudaActivo = cuentacorrienteDto.LimiteDeudaActivo,
                    FechaVencimiento = cuentacorrienteDto.FechaVencimiento,
                    FechaActivacion = cuentacorrienteDto.FechaActivacion,// de momento automatico, pero lo dejamos por si en el futuro se quiere usar
                    FechaCreacion = cuentacorrienteDto.FechaCreacion,
                    TipoVencimiento = cuentacorrienteDto.TipoVencimiento,
                    EstaEliminado = false,
                    EstadoCuentaCorriente = (int)EstadoCuentaCorriente.Activa, // Por defecto al crearla, está activa
                    ConDeuda = cuentacorrienteDto.Saldo < 0,
                    CantidadMesesVencimiento = cuentacorrienteDto.CantidadMesesVencimiento,
                    ClienteId = cuentacorrienteDto.ClienteId,
                    CuentaCorrienteAutorizado = cuentacorrienteDto.DniAutorizados
                        .Select(dni => new CuentaCorrienteAutorizado { Dni = dni})
                        .ToList()
                };

                // 1. Agregamos la cuenta corriente y guardamos para GENERAR EL ID
                context.CuentaCorriente.Add(nuevaCuentaCorriente);
                context.SaveChanges();

                // 2. Ahora que nuevaCuentaCorriente.CuentaCorrienteId YA TIENE VALOR, buscamos al cliente
                var cliente = context.Cliente.Include(c => c.Persona)
                    .FirstOrDefault(x => x.PersonaId == cuentacorrienteDto.ClienteId);

                if (cliente == null)
                {
                    // Si por alguna razón no existe el cliente, hacemos rollback manual
                    transaction.Rollback();
                    return new EstadoOperacion { Exitoso = false, Mensaje = "El cliente especificado no existe." };
                }

                // 3. Le asignamos el ID recién generado y actualizamos el cliente
                cliente.CuentaCorrienteId = nuevaCuentaCorriente.CuentaCorrienteId;
                context.Cliente.Update(cliente);

                // 4. Guardamos el cambio del cliente en la DB
                context.SaveChanges();

                // 5. Si todo salió bien, confirmamos AMBOS guardados juntos
                transaction.Commit();

                return new EstadoOperacion
                {
                    Exitoso = true,
                    Mensaje = "CuentaCorriente creada correctamente.",
                    EntidadId = nuevaCuentaCorriente.CuentaCorrienteId
                };
            }
            catch (Exception ex)
            {
                // Si salta una excepción en cualquier SaveChanges, se deshace todo por igual
                transaction.Rollback();

                return new EstadoOperacion
                {
                    Exitoso = false,
                    Mensaje = ex.ToString()
                };
            }
        }
        public EstadoOperacion CargarSaldoCuentaCorriente(long cuentaCorrienteId,decimal nuevoSaldo)
        {
            using var context = new GestorContextDBFactory().CreateDbContext(null);


            var cuenta = context.CuentaCorriente
                .FirstOrDefault(x =>
                    x.CuentaCorrienteId == cuentaCorrienteId);


            if (cuenta == null)
            {
                return new EstadoOperacion
                {
                    Exitoso = false,
                    Mensaje = "Cuenta corriente no encontrada."
                };
            }


            decimal saldoAnterior = cuenta.Saldo;


            if (saldoAnterior == nuevoSaldo)
            {
                return new EstadoOperacion
                {
                    Exitoso = false,
                    Mensaje = "El saldo ingresado no genera cambios."
                };
            }


            decimal diferencia = nuevoSaldo - saldoAnterior;


            //---------------------------------------------
            // Actualizar saldo
            //---------------------------------------------

            cuenta.Saldo = nuevoSaldo;


            //---------------------------------------------
            // Movimiento de caja
            //---------------------------------------------
            var cajaServicio = new Caja.CajaServicio();

            var cajaId = cajaServicio.ObtenerIdDeUltimaCajaAbierta(context);

            cajaServicio.RegistrarTransaccion(context, diferencia, TipoMovimiento.Ingreso, cajaId.Value);
            CrearMovimientoCargaSaldoCuentaCorriente(cuenta.CuentaCorrienteId, diferencia, context);


            context.SaveChanges();


            return new EstadoOperacion
            {
                Exitoso = true,
                Mensaje = "Saldo actualizado correctamente.",
                EntidadId = cuenta.CuentaCorrienteId,
                DatoExtra = cuenta.Saldo.ToString("C")
            };
        }

        public void CrearMovimientoCargaSaldoCuentaCorriente(long cuentaCorrienteId,decimal monto,GestorContextDB context)
        {
            if (cuentaCorrienteId <= 0)
                throw new Exception("No puede crearse el movimiento porque la cuenta corriente no posee un Id válido.");

            var movimiento = new AccesoDatos.Entidades.Movimiento
            {
                NumeroMovimiento = $"MOV-CTACTE-{cuentaCorrienteId}-{DateTime.Now:yyyyMMddHHmmss}",
                EntidadId = cuentaCorrienteId,
                TipoEntidad = (int)TipoEntidadMovimiento.CuentaCorriente,
                TipoMovimiento = (int)TipoMovimiento.Ingreso,
                TipoMovimientoDetalle = (int)TipoMovimientoDetalle.CuentaCorriente,
                Monto = Math.Abs(monto),
                FechaMovimiento = DateTime.Now,
                EstaEliminado = false
            };

            context.Movimientos.Add(movimiento);
        }


        public EstadoOperacion Modificar(CuentaCorrienteDTO cuentacorrienteDto, long? cuentacorrienteId)
        {
            using var context = new GestorContextDBFactory().CreateDbContext(null);

            var cuentacorrienteEditar = context.CuentaCorriente
                .Include(x => x.CuentaCorrienteAutorizado)
                .FirstOrDefault(x => x.CuentaCorrienteId == cuentacorrienteId);

            if (cuentacorrienteEditar == null)
            {
                return new EstadoOperacion
                {
                    Exitoso = false,
                    Mensaje = "Cuenta corriente no encontrada."
                };
            }

            bool cuentaDuplicada = context.CuentaCorriente.Any(x =>
                x.NombreCuentaCorriente == cuentacorrienteDto.NombreCuentaCorriente &&
                x.CuentaCorrienteId != cuentacorrienteEditar.CuentaCorrienteId);

            if (cuentaDuplicada)
            {
                return new EstadoOperacion
                {
                    Exitoso = false,
                    Mensaje = "Ya existe una cuenta corriente con el mismo nombre."
                };
            }


            //---------------------------------------------------
            // Actualizar datos
            //---------------------------------------------------

            cuentacorrienteEditar.NombreCuentaCorriente = cuentacorrienteDto.NombreCuentaCorriente;
            cuentacorrienteEditar.Saldo = cuentacorrienteDto.Saldo;
            cuentacorrienteEditar.LimiteDeuda = cuentacorrienteDto.LimiteDeuda;
            cuentacorrienteEditar.LimiteDeudaActivo = cuentacorrienteDto.LimiteDeudaActivo;
            cuentacorrienteEditar.FechaVencimiento = cuentacorrienteDto.FechaVencimiento;
            cuentacorrienteEditar.CantidadMesesVencimiento = cuentacorrienteDto.CantidadMesesVencimiento;
            cuentacorrienteEditar.TipoVencimiento = cuentacorrienteDto.TipoVencimiento;

            //---------------------------------------------------
            // Actualizar DNIs
            //---------------------------------------------------

            cuentacorrienteEditar.CuentaCorrienteAutorizado.Clear();

            foreach (var dni in cuentacorrienteDto.DniAutorizados)
            {
                cuentacorrienteEditar.CuentaCorrienteAutorizado.Add(
                    new CuentaCorrienteAutorizado
                    {
                        Dni = dni
                    });
            }

            //---------------------------------------------------
            // Cambió el saldo
            //---------------------------------------------------

          
            context.SaveChanges();

            return new EstadoOperacion
            {
                Exitoso = true,
                Mensaje = "Cuenta corriente modificada correctamente.",
                EntidadId = cuentacorrienteEditar.CuentaCorrienteId
            };
        }
        public CuentaCorrienteDTO ObtenerCuentaCorrientePorId(long cuentacorrienteId)
        {
            using var context = new GestorContextDBFactory().CreateDbContext(null);

            var cuentacorrienteBusqueda = context.CuentaCorriente
                .Include(x => x.Cliente)
                .ThenInclude(c => c.Persona)
                .Include(x => x.CuentaCorrienteAutorizado)
                .FirstOrDefault(x => x.CuentaCorrienteId == cuentacorrienteId && x.Cliente.CuentaCorrienteId == cuentacorrienteId);

            if (cuentacorrienteBusqueda == null)
                throw new Exception("No se encontró la cuentacorriente.");

            return new CuentaCorrienteDTO
            {
                Saldo = cuentacorrienteBusqueda.Saldo,
                LimiteDeuda = cuentacorrienteBusqueda.LimiteDeuda,
                NombreCuentaCorriente = cuentacorrienteBusqueda.NombreCuentaCorriente,
                LimiteDeudaActivo = cuentacorrienteBusqueda.LimiteDeudaActivo,
                FechaVencimiento = cuentacorrienteBusqueda.FechaVencimiento,
                FechaCreacion = cuentacorrienteBusqueda.FechaCreacion,
                FechaActivacion = cuentacorrienteBusqueda.FechaActivacion,
                CantidadMesesVencimiento = cuentacorrienteBusqueda.CantidadMesesVencimiento,
                ConDeuda = cuentacorrienteBusqueda.ConDeuda,
                EstadoCtaCte = cuentacorrienteBusqueda.EstadoCuentaCorriente,
                TipoVencimiento = cuentacorrienteBusqueda.TipoVencimiento,
                CuentaCorrienteId = cuentacorrienteBusqueda.CuentaCorrienteId,
                NombreCliente = $"{cuentacorrienteBusqueda.Cliente.Persona.Nombre}{cuentacorrienteBusqueda.Cliente.Persona.Apellido}",
                DniAutorizados = cuentacorrienteBusqueda.CuentaCorrienteAutorizado.Select(dni => dni.Dni.ToString()).ToList()
            };
        }

        public ResultadoPaginacion<CuentaCorrienteDTO> ObtenerCuentaCorrientes(FiltroConsulta filtros)
        {
            using var context = new GestorContextDBFactory().CreateDbContext(null);
            string collation = "Latin1_General_CI_AI";
            var query = context.CuentaCorriente
                .AsNoTracking()
                .Include(x => x.CuentaCorrienteAutorizado)
                .AsQueryable();

            // =========================================================
            // 🧠 CORE (DEFAULT / ELIMINADOS / HISTORICO)
            // =========================================================

            bool hayFiltroEstado =
                filtros.Filtro2 != null &&
                !string.IsNullOrWhiteSpace(filtros.Filtro2.ToString());

            if (filtros.Bool2)
            {
                // 👉 HISTÓRICO → no filtramos nada
            }
            else if (filtros.Bool1)
            {
                // 👉 SOLO eliminados
                query = query.Where(x => x.EstaEliminado);
            }
            else if (!hayFiltroEstado)
            {
                // 👉 DEFAULT
                query = query.Where(x =>
                    !x.EstaEliminado &&
                    x.EstadoCuentaCorriente == (int)EstadoCuentaCorriente.Activa);
            }
            else
            {
                // 👉 hay filtro → solo excluir eliminados
                query = query.Where(x => !x.EstaEliminado);
            }

            // =========================================================
            // 🔍 BUSQUEDA
            // =========================================================

            if (!string.IsNullOrWhiteSpace(filtros.TextoBuscar))
            {
                var texto = filtros.TextoBuscar.Trim();

                query = query.Where(x =>
                    EF.Functions.Collate(x.NombreCuentaCorriente, collation)
                        .Contains(texto));
            }

            // =========================================================
            // 📌 ESTADO (cbx2)
            // =========================================================

            if (filtros.Filtro2 != null &&
                int.TryParse(filtros.Filtro2.ToString(), out var estado))
            {
                query = query.Where(x =>
                    (int)x.EstadoCuentaCorriente == estado);
            }

            // =========================================================
            // 📅 FECHAS (cbx3)
            // =========================================================

            var filtroFecha = filtros.Filtro3?.ToString();
            if (filtroFecha == "vto")
            {
                if (filtros.FechaDesde.HasValue)
                {
                    query = query.Where(x =>
                        x.FechaVencimiento.HasValue &&
                        x.FechaVencimiento.Value >= filtros.FechaDesde.Value);
                }

                if (filtros.FechaHasta.HasValue)
                {
                    var hasta = filtros.FechaHasta.Value.AddDays(1);

                    query = query.Where(x =>
                        x.FechaVencimiento.HasValue &&
                        x.FechaVencimiento.Value < hasta);
                }
            }
            else
            {
                //query = query.Where(x=> x.FechaCreacion >= filtros.FechaDesde &&
                //    x.FechaCreacion <= filtros.FechaHasta);
            }

            // =========================================================
            // 📊 TOTAL
            // =========================================================

            var total = query.Count();

            // =========================================================
            // 🔴 PAGINACION
            // =========================================================

            var totalPaginas = (int)Math.Ceiling((double)total / filtros.PageSize);

            if (totalPaginas == 0)
                totalPaginas = 1;

            if (filtros.Page > totalPaginas)
                filtros.Page = totalPaginas;

            if (filtros.Page < 1)
                filtros.Page = 1;

            // =========================================================
            // 📌 ORDEN (CLAVE)
            // =========================================================

            query = query.OrderBy(x => x.FechaVencimiento ?? DateTime.MaxValue);

            // =========================================================
            // 📄 DATA
            // =========================================================

            var data = query
                .Skip((filtros.Page - 1) * filtros.PageSize)
                .Take(filtros.PageSize)
                .AsEnumerable()
                .Select(x => new CuentaCorrienteDTO
                {
                    CuentaCorrienteId = x.CuentaCorrienteId,
                    NombreCuentaCorriente = x.NombreCuentaCorriente,
                    Saldo = x.Saldo,
                    LimiteDeuda = x.LimiteDeuda,
                    LimiteDeudaActivo = x.LimiteDeudaActivo,
                    FechaVencimiento = x.FechaVencimiento,
                    EstadoCtaCte = x.EstadoCuentaCorriente,
                    FechaActivacion = x.FechaActivacion,
                    FechaCreacion = x.FechaCreacion,
                    ConDeuda = x.ConDeuda,
                    CantidadMesesVencimiento = x.CantidadMesesVencimiento,
                    TipoVencimiento = x.TipoVencimiento,
                   

                    DniAutorizados = x.CuentaCorrienteAutorizado
                        .Select(a => a.Dni.ToString())
                        .ToList()
                })
                .ToList();

            return new ResultadoPaginacion<CuentaCorrienteDTO>
            {
                Items = data,
                TotalRegistros = total,
                Page = filtros.Page,
                PageSize = filtros.PageSize
            };
        }

        // ===============================================================// 
        // LOGICA DE NEGOCIO                                              // 
        // ===============================================================// 

        public bool PuedeComprar(long cuentaId, decimal monto)
        {
            using var context = new GestorContextDBFactory().CreateDbContext(null);

            var cuenta = context.CuentaCorriente
                .FirstOrDefault(c => c.CuentaCorrienteId == cuentaId && !c.EstaEliminado);

            if (cuenta == null)
                return false;

            // Cuenta cerrada manualmente o suspendida
            if (cuenta.EstadoCuentaCorriente == (int)EstadoCuentaCorriente.Cerrada || cuenta.EstadoCuentaCorriente == (int)EstadoCuentaCorriente.Suspendida)
                return false;

            decimal saldoProyectado = cuenta.Saldo - monto;

            bool estaVencidaPorFecha =
                cuenta.FechaVencimiento.HasValue &&
                cuenta.FechaVencimiento.Value < DateTime.Now;

            // Si está vencida solamente puede gastar saldo disponible.
            if (estaVencidaPorFecha && saldoProyectado < 0)
                return false;

            // No permite deuda.
            if (!cuenta.LimiteDeudaActivo)
                return saldoProyectado >= 0;

            // Permite deuda ilimitada.
            if (cuenta.LimiteDeuda == 0)
                return true;

            // El saldo no puede ser menor al límite negativo permitido.
            return saldoProyectado >= -cuenta.LimiteDeuda;
        }

        public EstadoOperacion RegistrarCompra(long cuentaId, decimal monto, long cajaId, string descripcion = "Compra")
        {
            using var context = new GestorContextDBFactory().CreateDbContext(null);
            var cuenta = context.CuentaCorriente.FirstOrDefault(c => c.CuentaCorrienteId == cuentaId);

            if (cuenta == null) throw new Exception("Cuenta corriente no encontrada");

            if (!PuedeComprar(cuentaId, monto))
                return new EstadoOperacion { Exitoso = false, Mensaje = "No puede realizar la compra (cuenta vencida, inactiva o sin límite)." };

            cuenta.Saldo -= monto;

            // EVALUAMOS EL ESTADO DE ESTA CUENTA ACÁ (por si la compra la dejó vencida/en deuda)
            VerificarYActualizarEstadoInstancia(cuenta);

            _movimientoServicio.CrearMovimientoCtaCte(monto, cajaId, cuenta.CuentaCorrienteId, TipoMovimientoDetalle.CuentaCorriente, false, context);

            context.SaveChanges(); // Guarda el saldo, el movimiento y el nuevo estado TODO JUNTO.

            return new EstadoOperacion { Exitoso = true, Mensaje = "Compra registrada correctamente" };
        }

        public EstadoOperacion RegistrarPago(long cuentaId, decimal monto, long cajaId, string descripcion = "Pago")
        {
            if (monto <= 0) return new EstadoOperacion { Exitoso = false, Mensaje = "El monto debe ser mayor a cero." };

            using var context = new GestorContextDBFactory().CreateDbContext(null);
            var cuenta = context.CuentaCorriente.FirstOrDefault(c => c.CuentaCorrienteId == cuentaId);

            if (cuenta == null) throw new Exception("Cuenta corriente no encontrada");

            cuenta.Saldo += monto;

            // EVALUAMOS EL ESTADO DE ESTA CUENTA ACÁ (si pagó la deuda, se reactiva sola)
            VerificarYActualizarEstadoInstancia(cuenta);

            _movimientoServicio.CrearMovimientoCtaCte(monto, cajaId, cuenta.CuentaCorrienteId, TipoMovimientoDetalle.CuentaCorriente, true, context);

            context.SaveChanges(); // Guarda todo en una sola transacción limpia

            return new EstadoOperacion { Exitoso = true, Mensaje = "Pago registrado correctamente" };
        }

        // =========================================================================
        // 🔥 NUEVO MÉTODO: Registrar Devolución o Anulación de Venta Interna
        // =========================================================================
        public EstadoOperacion RegistrarDevolucionOAnulacion(long cuentaId, decimal monto, long cajaId, string descripcion = "Anulación/Devolución")
        {
            if (monto <= 0) return new EstadoOperacion { Exitoso = false, Mensaje = "El monto de la devolución debe ser mayor a cero." };

            using var context = new GestorContextDBFactory().CreateDbContext(null);
            var cuenta = context.CuentaCorriente.FirstOrDefault(c => c.CuentaCorrienteId == cuentaId);

            if (cuenta == null) throw new Exception("Cuenta corriente no encontrada");

            // En tu Modelo de Saldo Negativo, revertir una compra SUMA al saldo (lo acerca a 0 o a positivo)
            cuenta.Saldo += monto;

            // Evaluamos el estado en memoria antes de guardar (por si la cuenta sale de la suspensión)
            VerificarYActualizarEstadoInstancia(cuenta);

            // Impactamos el histórico con 'true' ya que incrementa el saldo a favor/reduce saldo deudor
            _movimientoServicio.CrearMovimientoCtaCte(monto, cajaId, cuenta.CuentaCorrienteId, TipoMovimientoDetalle.CuentaCorriente, true, context);

            context.SaveChanges();

            return new EstadoOperacion { Exitoso = true, Mensaje = "Devolución/Anulación registrada correctamente" };
        }

        public List<string> ObtenerDnisAutorizados(long? cuentaId)
        {
            using var context = new GestorContextDBFactory().CreateDbContext(null);
            var cuenta = context.CuentaCorriente
                .Include(c => c.CuentaCorrienteAutorizado)
                .FirstOrDefault(c => c.CuentaCorrienteId == cuentaId);

            if (cuenta == null)
                throw new Exception("Cuenta corriente no encontrada");
            return cuenta.CuentaCorrienteAutorizado.Select(a => a.Dni).ToList();
        }

        public CuentaCorrienteDTO ObtenerCuentaCorrientePorClienteId(long clienteId)
        {
            using var context = new GestorContextDBFactory().CreateDbContext(null);
            var x = context.CuentaCorriente
                .Include(c => c.CuentaCorrienteAutorizado)
                .FirstOrDefault(c => c.ClienteId == clienteId && !c.EstaEliminado);

            if (x == null)
                throw new Exception("No se encontró la cuenta corriente para este cliente.");

            return new CuentaCorrienteDTO
            {
                CuentaCorrienteId = x.CuentaCorrienteId,
                NombreCuentaCorriente = x.NombreCuentaCorriente,
                Saldo = x.Saldo,
                LimiteDeuda = x.LimiteDeuda,
                LimiteDeudaActivo = x.LimiteDeudaActivo,
                FechaVencimiento = x.FechaVencimiento,
                FechaCreacion = x.FechaCreacion,
                FechaActivacion = x.FechaActivacion,
                EstadoCtaCte = x.EstadoCuentaCorriente,
                DniAutorizados = x.CuentaCorrienteAutorizado.Select(dni => dni.Dni.ToString()).ToList()
            };
        }

        public List<CuentaCorrienteDTO> ObtenerCtaCteVencidas(int cantidadDiasVencimiento)
        {
            using var context = new GestorContextDBFactory().CreateDbContext(null);

            var fechaLimite = DateTime.Now.AddDays(cantidadDiasVencimiento);

            var cuentasVencidas = context.CuentaCorriente
                .Where(x => !x.EstaEliminado && x.FechaVencimiento.HasValue && x.FechaVencimiento.Value <= fechaLimite)
                .Select(x => new CuentaCorrienteDTO
                {
                    Saldo = x.Saldo,
                    LimiteDeuda = x.LimiteDeuda,
                    NombreCuentaCorriente = x.NombreCuentaCorriente,
                    LimiteDeudaActivo = x.LimiteDeudaActivo,
                    FechaVencimiento = x.FechaVencimiento,
                    FechaCreacion = x.FechaCreacion,
                    FechaActivacion = x.FechaActivacion,
                    CuentaCorrienteId = x.CuentaCorrienteId,
                    NombreCliente = $"{x.Cliente.Persona.Nombre} {x.Cliente.Persona.Apellido}",
                    DniAutorizados = x.CuentaCorrienteAutorizado.Select(dni => dni.Dni.ToString()).ToList()
                })
                .ToList();
            return cuentasVencidas;
        }

       public static void VerificarYActualizarEstadoInstancia(AccesoDatos.Entidades.CuentaCorriente cuenta)
{
    cuenta.ConDeuda = cuenta.Saldo < 0;

    // Si fue cerrada manualmente no la modificamos.
    if (cuenta.EstadoCuentaCorriente == (int)EstadoCuentaCorriente.Cerrada)
        return;

    bool suspendidaPorFecha =
        cuenta.FechaVencimiento.HasValue &&
        cuenta.FechaVencimiento.Value < DateTime.Now;

    bool suspendidaPorLimite = false;

    if (cuenta.LimiteDeudaActivo && cuenta.LimiteDeuda > 0)
    {
        suspendidaPorLimite = cuenta.Saldo <= -cuenta.LimiteDeuda;
    }

    cuenta.EstadoCuentaCorriente =
        (suspendidaPorFecha || suspendidaPorLimite)
            ? (int)EstadoCuentaCorriente.Suspendida
            : (int)EstadoCuentaCorriente.Activa;
}


        public ResultadoPaginacion<MovimientoDTO> ObtenerMovimientosPorCuentaCorriente(long cuentaCorrienteId,FiltroConsulta filtros)
        {
            using var context = new GestorContextDBFactory().CreateDbContext(null);

            var query = context.Movimientos
            .Where(x =>
                x.TipoMovimientoDetalle == (int)TipoMovimientoDetalle.CuentaCorriente &&
                x.TipoEntidad == (int)TipoEntidadMovimiento.CuentaCorriente &&
                x.EntidadId == cuentaCorrienteId)
            .AsNoTracking()
            .AsQueryable();

            //==========================================
            // ELIMINADOS
            //==========================================

            if (filtros.Bool2)
            {
                // Histórico
            }
            else if (filtros.Bool1)
            {
                query = query.Where(x => x.EstaEliminado);
            }
            else
            {
                query = query.Where(x => !x.EstaEliminado);
            }

            //==========================================
            // BUSQUEDA
            //==========================================

            if (!string.IsNullOrWhiteSpace(filtros.TextoBuscar))
            {
                var texto = filtros.TextoBuscar.Trim();

                query = query.Where(x =>
                    x.NumeroMovimiento.Contains(texto));
            }

            //==========================================
            // FECHA
            //==========================================

            bool hayFiltroFecha =
                filtros.FechaDesde.HasValue ||
                filtros.FechaHasta.HasValue;

            if (hayFiltroFecha)
            {
                if (filtros.FechaDesde.HasValue)
                {
                    query = query.Where(x =>
                        x.FechaMovimiento >= filtros.FechaDesde.Value);
                }

                if (filtros.FechaHasta.HasValue)
                {
                    var hasta = filtros.FechaHasta.Value.AddDays(1);

                    query = query.Where(x =>
                        x.FechaMovimiento < hasta);
                }
            }
            else
            {
                var fechaLimite = filtros.Bool2
                    ? DateTime.Now.AddMonths(-6)
                    : DateTime.Now.AddMonths(-2);

                query = query.Where(x =>
                    x.FechaMovimiento >= fechaLimite);
            }

            //==========================================
            // TOTAL
            //==========================================

            var total = query.Count();

            var totalPaginas =
                (int)Math.Ceiling((double)total / filtros.PageSize);

            if (totalPaginas <= 0)
                totalPaginas = 1;

            if (filtros.Page > totalPaginas)
                filtros.Page = totalPaginas;

            if (filtros.Page < 1)
                filtros.Page = 1;

            //==========================================
            // DATOS
            //==========================================

            var data = query
                .OrderByDescending(x => x.FechaMovimiento)
                .Skip((filtros.Page - 1) * filtros.PageSize)
                .Take(filtros.PageSize)
                .Select(x => new MovimientoDTO
                {
                    MovimientoId = x.MovimientoId,
                    NumeroMovimiento = x.NumeroMovimiento,
                    TipoMovimiento = x.TipoMovimiento,
                    TipoMovimientoDetalle = x.TipoMovimientoDetalle,
                    Monto = x.Monto,
                    FechaMovimiento = x.FechaMovimiento,
                    EstaEliminado = x.EstaEliminado,
                    EntidadId = x.EntidadId,
                    TipoEntidad = x.TipoEntidad
                })
                .ToList();

            return new ResultadoPaginacion<MovimientoDTO>
            {
                Items = data,
                TotalRegistros = total,
                Page = filtros.Page,
                PageSize = filtros.PageSize
            };
        }

        public EstadoOperacion CerrarCuentaCorriente(long ctacteId)
        {
            using var context = new GestorContextDBFactory().CreateDbContext(null);

            var cuenta = context.CuentaCorriente
                .FirstOrDefault(x => x.CuentaCorrienteId == ctacteId);

            if (cuenta == null)
            {
                return new EstadoOperacion
                {
                    Exitoso = false,
                    Mensaje = "Cuenta corriente no encontrada."
                };
            }

            if (cuenta.EstadoCuentaCorriente == (int)EstadoCuentaCorriente.Cerrada)
            {
                return new EstadoOperacion
                {
                    Exitoso = false,
                    Mensaje = "La cuenta corriente ya se encuentra cerrada."
                };
            }

            if (cuenta.Saldo < 0)
            {
                return new EstadoOperacion
                {
                    Exitoso = false,
                    Mensaje = "No es posible cerrar la cuenta corriente porque posee una deuda pendiente. Debe cancelar la deuda antes de cerrarla."
                };
            }

            cuenta.EstadoCuentaCorriente = (int)EstadoCuentaCorriente.Cerrada;


            context.SaveChanges();

            return new EstadoOperacion
            {
                Exitoso = true,
                Mensaje = "Cuenta corriente cerrada correctamente.",
                EntidadId = cuenta.CuentaCorrienteId
            };
        }

        public EstadoOperacion ActivarCuentaCorriente(long ctacteId)
        {
            using var context = new GestorContextDBFactory().CreateDbContext(null);

            var cuenta = context.CuentaCorriente
                .FirstOrDefault(x => x.CuentaCorrienteId == ctacteId);

            if (cuenta == null)
            {
                return new EstadoOperacion
                {
                    Exitoso = false,
                    Mensaje = "Cuenta corriente no encontrada."
                };
            }

            if (cuenta.EstadoCuentaCorriente == (int)EstadoCuentaCorriente.Activa)
            {
                return new EstadoOperacion
                {
                    Exitoso = false,
                    Mensaje = "La cuenta corriente ya se encuentra activa."
                };
            }

            if (cuenta.Saldo < 0)
            {
                return new EstadoOperacion
                {
                    Exitoso = false,
                    Mensaje = "No es posible activar la cuenta corriente porque posee una deuda pendiente. Debe cancelar la deuda antes de activarla."
                };
            }

            cuenta.EstadoCuentaCorriente = (int)EstadoCuentaCorriente.Activa;
            cuenta.FechaActivacion = DateTime.Now;

            context.SaveChanges();

            return new EstadoOperacion
            {
                Exitoso = true,
                Mensaje = "Cuenta corriente activada correctamente.",
                EntidadId = cuenta.CuentaCorrienteId
            };
        }
    }
}