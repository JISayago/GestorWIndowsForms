using AccesoDatos;
using AccesoDatos.Entidades;
using Microsoft.EntityFrameworkCore;
using Servicios.Helpers;
using Servicios.LogicaNegocio.CuentaCorriente.DTO;
using Servicios.LogicaNegocio.Producto.DTO;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Servicios.LogicaNegocio.CuentaCorriente
{
    public class CuentaCorrienteServicio : ICuentaCorrienteServicio
    {
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
                .Include(x => x.Movimientos)
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

        public IEnumerable<CuentaCorrienteDTO> ObtenerCuentaCorrientes(string cadenaBuscar)
        {
            using var context = new GestorContextDBFactory().CreateDbContext(null);

            return context.CuentaCorriente
                .Where(x => !x.EstaEliminado && x.NombreCuentaCorriente.Contains(cadenaBuscar))
                .Select(x => new CuentaCorrienteDTO
                {
                    Saldo = x.Saldo,
                    LimiteDeuda = x.LimiteDeuda,
                    NombreCuentaCorriente = x.NombreCuentaCorriente,
                    LimiteDeudaActivo = x.LimiteDeudaActivo,
                    FechaVencimiento = x.FechaVencimiento,
                    CuentaCorrienteId = x.CuentaCorrienteId,
                    DniAutorizados = x.CuentaCorrienteAutorizado
                        .Select(c => c.Dni)
                        .ToList()
                })
                .ToList();
        }

        public IEnumerable<CuentaCorrienteDTO> ObtenerCuentaCorrientesEliminada(string cadenaBuscar)
        {
            using var context = new GestorContextDBFactory().CreateDbContext(null);

            return context.CuentaCorriente
                .Where(x => x.EstaEliminado && x.NombreCuentaCorriente.Contains(cadenaBuscar))
                .Include(x => x.CuentaCorrienteAutorizado)
                .Select(x => new CuentaCorrienteDTO
                {
                    Saldo = x.Saldo,
                    LimiteDeuda = x.LimiteDeuda,
                    NombreCuentaCorriente = x.NombreCuentaCorriente,
                    LimiteDeudaActivo = x.LimiteDeudaActivo,
                    FechaVencimiento = x.FechaVencimiento,
                    CuentaCorrienteId = x.CuentaCorrienteId,
                    DniAutorizados = x.CuentaCorrienteAutorizado
                        .Select(cp => cp.Dni)
                        .ToList()
                })
                .ToList();
        }

        // =====================
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
        public EstadoOperacion RegistrarCompra(long cuentaId, decimal monto, string descripcion = "Compra")
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

            // Registrar movimiento
            cuenta.Movimientos ??= new List<AccesoDatos.Entidades.Movimiento>();
            cuenta.Movimientos.Add(new AccesoDatos.Entidades.Movimiento
            {/*
                FechaMovimiento = DateTime.Now,
                Monto = -monto,
                Descripcion = descripcion,
                TipoMovimiento = 2, // 2 = compra
                CuentaCorrienteId = cuenta.CuentaCorrienteId*/
            });

            context.SaveChanges();

            return new EstadoOperacion { Exitoso = true, Mensaje = "Compra registrada correctamente" };
        }

        //Sumar saldo a la cuenta corriente
        public EstadoOperacion RegistrarPago(long cuentaId, decimal monto, string descripcion = "Pago")
        {
            using var context = new GestorContextDBFactory().CreateDbContext(null);
            var cuenta = context.CuentaCorriente.FirstOrDefault(c => c.CuentaCorrienteId == cuentaId);
            if (cuenta == null) throw new Exception("Cuenta corriente no encontrada");

            cuenta.Saldo += monto;

            // Registrar movimiento
            cuenta.Movimientos ??= new List<AccesoDatos.Entidades.Movimiento>();
            cuenta.Movimientos.Add(new AccesoDatos.Entidades.Movimiento
            {/*
                Fecha = DateTime.Now,
                Monto = monto,
                Descripcion = descripcion,
                TipoMovimientoCCorriente = 1,
                CuentaCorrienteId = cuenta.CuentaCorrienteId*/
            });

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
                .Include(x => x.Movimientos)
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
    }
}
