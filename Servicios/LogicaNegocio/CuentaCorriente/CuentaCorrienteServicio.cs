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

            if (context.CuentaCorriente.Any(p => p.NombreCuentaCorriente == cuentacorrienteDto.NombreCuentaCorriente))
                return new EstadoOperacion
                {
                    Exitoso = false,
                    Mensaje = "Ya existe una cuentacorriente con el mismo nombre"
                };
            
            var nuevaCuentaCorriente = new AccesoDatos.Entidades.CuentaCorriente
            {
                NombreCuentaCorriente = cuentacorrienteDto.NombreCuentaCorriente,
                Saldo = cuentacorrienteDto.Saldo,
                LimiteDeuda = cuentacorrienteDto.LimiteDeuda,
                LimiteDeudaActivo = cuentacorrienteDto.LimiteDeudaActivo,
                FechaVencimiento = cuentacorrienteDto.FechaVencimiento,
                EstaEliminado = false,
                ClienteId = cuentacorrienteDto.ClienteId,
                // Convertimos los DNIs a objetos de entidad
                CuentaCorrienteAutorizado = cuentacorrienteDto.DniAutorizados
                    .Select(dni => new CuentaCorrienteAutorizado
                    {
                        Dni = dni
                    })
                    .ToList()
            };

            context.CuentaCorriente.Add(nuevaCuentaCorriente);
            context.SaveChanges(); // Guarda en la DB

            return new EstadoOperacion
            {
                Exitoso = true,
                Mensaje = "CuentaCorriente creada correctamente.",
                EntidadId = nuevaCuentaCorriente.CuentaCorrienteId
            }; // Devuelve el ID generado
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

            /* Actualizar DNIs cuentacorrienteautorizado
            // Eliminamos los existentes
            cuentacorrienteEditar.CuentaCorrienteAutorizado.Clear();
            if (cuentacorrienteDto.DniCuentaCorrienteAutorizado != null)
            {
                cuentacorrienteEditar.CuentaCorrienteAutorizado = cuentacorrienteDto.DniCuentaCorrienteAutorizado
                    .Select(a => new CuentaCorrienteAutorizado
                    {
                        Dni = a
                    }).ToList();//probar si anda bien
            }

            */
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
            /*
            var cuentacorrienteBusqueda = context.CuentaCorriente
                .FirstOrDefault(x => x.CuentaCorrienteId == cuentacorrienteId);
            */

            var cuentacorrienteBusqueda = context.CuentaCorriente
                .Include(x => x.CuentaCorrienteAutorizado)
                //.Include(x => x.Movimientos)
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
                    x.NombreCuentaCorriente.Contains(texto));
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

            //EVALUAMOS EL ESTADO DE ESTA CUENTA ACÁ (por si la compra la dejó vencida/en deuda)
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

            //EVALUAMOS EL ESTADO DE ESTA CUENTA ACÁ (si pagó la deuda, se reactiva sola)
            VerificarYActualizarEstadoInstancia(cuenta);

            _movimientoServicio.CrearMovimientoCtaCte(monto, cajaId, cuenta.CuentaCorrienteId, TipoMovimientoDetalle.CuentaCorriente, true, context);

            context.SaveChanges(); // Guarda todo en una sola transacción limpia

            return new EstadoOperacion { Exitoso = true, Mensaje = "Pago registrado correctamente" };
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
                //.Include(x => x.CuentaCorrienteAutorizado)
                //.Include(x => x.Movimientos)
                .Where(x => !x.EstaEliminado && x.FechaVencimiento.HasValue && x.FechaVencimiento.Value <= fechaLimite) //Probar
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


        /*
        ==========================================================================================
        ARCHITECTURE NOTE: ARQUITECTURA Y REGLAS DE NEGOCIO - MÓDULO CUENTA CORRIENTE
        ==========================================================================================

        Este servicio gestiona el crédito y los saldos de los clientes utilizando un modelo 
        CONTABLE DE SALDO NEGATIVO. A continuación se detallan los pilares de la lógica:

        1. COMPORTAMIENTO DEL SALDO (Modelo Contable)
           - Saldo = 0 : Cuenta al día, sin deuda y sin saldo a favor.
           - Saldo < 0 : El cliente LE DEBE plata al negocio (Ej: Saldo -500 significa debe $500).
           - Saldo > 0 : El cliente tiene plata A FAVOR / PREPAGO (Ej: Saldo 200 significa $200 a favor).

        2. LÍMITE DE DEUDA (LimiteDeuda)
           - Funciona como un tope hacia abajo en terreno negativo. 
           - Si el límite está activo y es de $1000, el saldo nunca podrá ser menor a -$1000.
           - Se evalúa mediante valor absoluto (Math.Abs) sobre el saldo proyectado de la compra.

        3. POLÍTICA CRÍTICA DE VENCIMIENTO (FechaVencimiento vs Estado)
           - El vencimiento por fecha NO bloquea la cuenta por completo; solo congela el CRÉDITO.
           - Escenario Deuda Vencida: Si la fecha expiró y el saldo es negativo (Saldo < 0), la cuenta
             pasa a estado 'Suspendida' y se bloquea cualquier intento de generar nueva deuda.
           - Escenario Prepago Vencido: Si la fecha expiró pero el cliente tiene saldo positivo (Saldo > 0),
             la cuenta opera como 'Activa'. Se le permite comprar SÓLO hasta consumir su propio dinero, 
             impidiendo que el saldo proyectado caiga por debajo de cero.

        4. GESTIÓN DE CONTEXTOS (DbContext) Y FLUJO DE ESTADOS
           - Las operaciones de escritura (RegistrarCompra / RegistrarPago) manejan su propio ciclo 
             de vida con 'using context'.
           - Para evitar deadlocks y conexiones paralelas, las mutaciones de estado ('Activa'/'Suspendida')
             de la cuenta afectada se calculan EN MEMORIA antes de enviar el .SaveChanges() final,
             garantizando atomicidad en la base de datos.
        ==========================================================================================
        */
    }
}
