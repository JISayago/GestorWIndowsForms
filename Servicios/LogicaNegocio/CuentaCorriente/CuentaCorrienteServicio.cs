using AccesoDatos;
using AccesoDatos.Entidades;
using Microsoft.EntityFrameworkCore;
using Servicios.Helpers.Cliente.CtaCte;
using Servicios.Helpers.Movimiento;
using Servicios.Helpers.Sistema;
using Servicios.Helpers.Sistema.FiltrosConsulta;
using Servicios.LogicaNegocio.CuentaCorriente.DTO;
using Servicios.LogicaNegocio.Movimiento;
using Servicios.LogicaNegocio.Producto.DTO;
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
                    EstaEliminado = false,
                    ClienteId = cuentacorrienteDto.ClienteId,
                    CuentaCorrienteAutorizado = cuentacorrienteDto.DniAutorizados
                        .Select(dni => new CuentaCorrienteAutorizado { Dni = dni })
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

            bool cuentacorrienteDuplicada = context.CuentaCorriente
                .Any(p => p.NombreCuentaCorriente == cuentacorrienteDto.NombreCuentaCorriente && p.NombreCuentaCorriente != cuentacorrienteEditar.NombreCuentaCorriente);

            if (cuentacorrienteDuplicada)
            {
                return new EstadoOperacion
                {
                    Exitoso = false,
                    Mensaje = "Ya existe una cuenta corriente con le mismo nombre."
                };
            }

            // Modificar los campos
            cuentacorrienteEditar.NombreCuentaCorriente = cuentacorrienteDto.NombreCuentaCorriente;
            cuentacorrienteEditar.Saldo = cuentacorrienteDto.Saldo;
            cuentacorrienteEditar.LimiteDeuda = cuentacorrienteDto.LimiteDeuda;
            cuentacorrienteEditar.LimiteDeudaActivo = cuentacorrienteDto.LimiteDeudaActivo;
            cuentacorrienteEditar.FechaVencimiento = cuentacorrienteDto.FechaVencimiento;

            cuentacorrienteEditar.CuentaCorrienteAutorizado.Clear();

            foreach (var dni in cuentacorrienteDto.DniAutorizados)
                cuentacorrienteEditar.CuentaCorrienteAutorizado.Add(new AccesoDatos.Entidades.CuentaCorrienteAutorizado { Dni = dni });

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
                .Include(x => x.CuentaCorrienteAutorizado)
                .FirstOrDefault(x => x.CuentaCorrienteId == cuentacorrienteId);

            if (cuentacorrienteBusqueda == null)
                throw new Exception("No se encontró la cuentacorriente.");

            return new CuentaCorrienteDTO
            {
                Saldo = cuentacorrienteBusqueda.Saldo,
                LimiteDeuda = cuentacorrienteBusqueda.LimiteDeuda,
                NombreCuentaCorriente = cuentacorrienteBusqueda.NombreCuentaCorriente,
                LimiteDeudaActivo = cuentacorrienteBusqueda.LimiteDeudaActivo,
                FechaVencimiento = cuentacorrienteBusqueda.FechaVencimiento,
                CuentaCorrienteId = cuentacorrienteBusqueda.CuentaCorrienteId,
                DniAutorizados = cuentacorrienteBusqueda.CuentaCorrienteAutorizado.Select(dni => dni.Dni).ToList()
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

                    DniAutorizados = x.CuentaCorrienteAutorizado
                        .Select(a => a.Dni)
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
            var cuenta = context.CuentaCorriente.FirstOrDefault(c => c.CuentaCorrienteId == cuentaId && !c.EstaEliminado);

            if (cuenta == null) return false;

            // Si el administrador la cerró manualmente por otra razón, no puede comprar nada
            if (cuenta.EstadoCuentaCorriente == (int)EstadoCuentaCorriente.Cerrada) return false;

            decimal saldoProyectado = cuenta.Saldo - monto;
            bool estaVencidaPorFecha = cuenta.FechaVencimiento.HasValue && cuenta.FechaVencimiento.Value < DateTime.Now;

            // 🌟 REGLA CLAVE: Si está vencida por fecha, solo puede comprar si se mantiene en terreno positivo (gasta su propia plata)
            if (estaVencidaPorFecha)
            {
                // Si el saldo proyectado baja de 0 (quiere pedir fiado estando vencido), se rechaza
                if (saldoProyectado < 0) return false;
            }

            // Validación del límite de deuda (solo aplica si entra o está en saldo negativo)
            if (cuenta.LimiteDeudaActivo && saldoProyectado < 0)
            {
                if (Math.Abs(saldoProyectado) > cuenta.LimiteDeuda)
                    return false;
            }

            return true;
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

        public List<long> ObtenerDnisAutorizados(long? cuentaId)
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
                EstadoCtaCte = x.EstadoCuentaCorriente,
                DniAutorizados = x.CuentaCorrienteAutorizado.Select(dni => dni.Dni).ToList()
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
                    CuentaCorrienteId = x.CuentaCorrienteId,
                    NombreCliente = $"{x.Cliente.Persona.Nombre} {x.Cliente.Persona.Apellido}",
                    DniAutorizados = x.CuentaCorrienteAutorizado.Select(dni => dni.Dni).ToList()
                })
                .ToList();
            return cuentasVencidas;
        }

        private void VerificarYActualizarEstadoInstancia(AccesoDatos.Entidades.CuentaCorriente cuenta)
        {
            // Si la cuenta está cerrada manualmente, no tocamos el estado de forma automática
            if (cuenta.EstadoCuentaCorriente == (int)EstadoCuentaCorriente.Cerrada) return;

            bool estaVencidaPorFecha = cuenta.FechaVencimiento.HasValue && cuenta.FechaVencimiento.Value < DateTime.Now;

            // Está suspendida SOLOTE si la fecha expiró Y además nos debe plata
            if (estaVencidaPorFecha && cuenta.Saldo < 0)
            {
                cuenta.EstadoCuentaCorriente = (int)EstadoCuentaCorriente.Suspendida;
            }
            // Si no venció, o si venció pero tiene saldo a favor (saldo >= 0), comercialmente está Activa
            else
            {
                cuenta.EstadoCuentaCorriente = (int)EstadoCuentaCorriente.Activa;
            }
        }
    }
}