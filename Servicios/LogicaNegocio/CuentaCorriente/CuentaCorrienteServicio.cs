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
                    EstadoCuentaCorriente = x.EstadoCuentaCorriente,

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
        }    // =====================
        // LOGICA DE NEGOCIO
        // =====================
        public bool DniAutorizado(long cuentaId, string dni)
        {
            using var context = new GestorContextDBFactory().CreateDbContext(null);
            var cuenta = context.CuentaCorriente
                .Where(c => c.CuentaCorrienteId == cuentaId && !c.EstaEliminado)
                .Select(c => new { c.CuentaCorrienteAutorizado })
                .FirstOrDefault();

            return cuenta != null && cuenta.CuentaCorrienteAutorizado.Any(a => a.Dni == Convert.ToInt64(dni));
        }

        public bool PuedeComprar(long cuentaId, decimal monto)
        {
            using var context = new GestorContextDBFactory().CreateDbContext(null);
            var cuenta = context.CuentaCorriente.FirstOrDefault(c => c.CuentaCorrienteId == cuentaId && !c.EstaEliminado);
            if (cuenta == null) return false;

            if (cuenta.EstadoCuentaCorriente != (int)EstadoCuentaCorriente.Activa) return false;
            if (cuenta.LimiteDeudaActivo && (cuenta.Saldo - monto) < -cuenta.LimiteDeuda) return false; //todas las ctacte al crear estan activas
            if (cuenta.FechaVencimiento.HasValue && cuenta.FechaVencimiento.Value < DateTime.Now) return false;

            return true;
        }

        public decimal SaldoDisponible(long cuentaId)
        {
            using var context = new GestorContextDBFactory().CreateDbContext(null);

            var cuenta = context.CuentaCorriente.FirstOrDefault(c => c.CuentaCorrienteId == cuentaId);
            if (cuenta == null) return 0;

            if (cuenta.LimiteDeudaActivo)
            {
                return cuenta.Saldo + cuenta.LimiteDeuda;
            }

            return cuenta.Saldo;
        }

        /*
        public decimal SaldoDisponible(long cuentaId)
        {
            using var context = new GestorContextDBFactory().CreateDbContext(null);
            var cuenta = context.CuentaCorriente.FirstOrDefault(c => c.CuentaCorrienteId == cuentaId);
            if (cuenta == null) return 0;

            return cuenta.LimiteDeudaActivo ? cuenta.Saldo + cuenta.LimiteDeuda : decimal.MaxValue;
        }*/

        //Restar saldo de la cuenta corriente
        public EstadoOperacion RegistrarCompra(long cuentaId, decimal monto, long cajaId, string descripcion = "Compra")
        {
            using var context = new GestorContextDBFactory().CreateDbContext(null);
            var cuenta = context.CuentaCorriente.FirstOrDefault(c => c.CuentaCorrienteId == cuentaId);
            if (cuenta == null) throw new Exception("Cuenta corriente no encontrada");

            /*if (!DniAutorizado(cuentaId, dniCliente))
                throw new Exception("DNI no autorizado");
            */

            if (!PuedeComprar(cuentaId, monto))
                throw new Exception("No puede realizar la compra");

            cuenta.Saldo -= monto;

            long ctacteId = cuenta.CuentaCorrienteId;

            _movimientoServicio.CrearMovimientoCtaCte(monto, cajaId, ctacteId, TipoMovimientoDetalle.CuentaCorriente, context);

            context.SaveChanges();

            return new EstadoOperacion { Exitoso = true, Mensaje = "Compra registrada correctamente" };
        }

        //Sumar saldo a la cuenta corriente, paga deuda de una ctacte, NO ESTA SIENDO INPLEMENTADO!!!!!!!!!!!!!
        public EstadoOperacion RegistrarPago(long cuentaId, decimal monto, string descripcion = "Pago")
        {
            using var context = new GestorContextDBFactory().CreateDbContext(null);
            var cuenta = context.CuentaCorriente.FirstOrDefault(c => c.CuentaCorrienteId == cuentaId);
            if (cuenta == null) throw new Exception("Cuenta corriente no encontrada");

            cuenta.Saldo += monto;

            // Registrar movimiento
            //cuenta.Movimientos ??= new List<AccesoDatos.Entidades.Movimiento>();
            //cuenta.Movimientos.Add(new AccesoDatos.Entidades.Movimiento
            //{
            //    //Fecha = DateTime.Now,
            //    //Monto = monto,
            //    //Descripcion = descripcion,
            //    //TipoMovimientoCCorriente = 1,
            //    //CuentaCorrienteId = cuenta.CuentaCorrienteId
            //});

            

            context.SaveChanges();

            return new EstadoOperacion { Exitoso = true, Mensaje = "Pago registrado correctamente" };
        }


        //agregar get dnis autorizados con ctacteID
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

        //obtener cuenta corriente por clienteID

        public CuentaCorrienteDTO ObtenerCuentaCorrientePorClienteId(long clienteId)
        {
            using var context = new GestorContextDBFactory().CreateDbContext(null);
            var cuentacorrienteBusqueda = context.CuentaCorriente
                .Include(x => x.CuentaCorrienteAutorizado)
               // .Include(x => x.Movimientos)
                .FirstOrDefault(x => x.ClienteId == clienteId && !x.EstaEliminado);
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
                    //DniAutorizados = x.CuentaCorrienteAutorizado.Select(dni => dni.Dni).ToList()
                })
                .ToList();
            return cuentasVencidas;
        }
    }
}
