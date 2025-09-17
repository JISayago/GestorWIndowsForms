using AccesoDatos;
using Servicios.Helpers;
using Servicios.LogicaNegocio.CuentaCorriente.DTO;

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

            if (context.CuentaCorriente.Any(p => p.NombreCuentaCorriente == cuentacorrienteDto.nombreCuentaCorriente))
                return new EstadoOperacion
                {
                    Exitoso = false,
                    Mensaje = "Ya existe una cuentacorriente con el mismo nombre"
                };
            
            var nuevaCuentaCorriente = new AccesoDatos.Entidades.CuentaCorriente
            {
                Saldo = cuentacorrienteDto.Saldo,
                NombreCuentaCorriente = cuentacorrienteDto.nombreCuentaCorriente,
                LimiteDeuda = cuentacorrienteDto.LimiteDeuda,
                LimiteDeudaActivo = cuentacorrienteDto.LimiteDeudaActivo,
                FechaVencimiento = cuentacorrienteDto.FechaVencimiento,
                EstaEliminado = false
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

        public EstadoOperacion Modificar(CuentaCorrienteDTO cuentacorrienteDto)
        {
            using var context = new GestorContextDBFactory().CreateDbContext(null);

            var cuentacorrienteEditar = context.CuentaCorriente.FirstOrDefault(x => x.CuentaCorrienteId == cuentacorrienteDto.CuentaCorrienteId);

            if (cuentacorrienteEditar == null)
            {
                return new EstadoOperacion
                {
                    Exitoso = false,
                    Mensaje = "Cuenta corriente no encontrada."
                };
            }
            
            bool cuentacorrienteDuplicada = context.CuentaCorriente
                .Any(p => p.NombreCuentaCorriente == cuentacorrienteDto.nombreCuentaCorriente && p.NombreCuentaCorriente != cuentacorrienteEditar.NombreCuentaCorriente);
            
            if (cuentacorrienteDuplicada)
            {
                return new EstadoOperacion
                {
                    Exitoso = false,
                    Mensaje = "Ya existe una cuenta corriente con le mismo nombre."
                };
            }
            
            // Modificar los campos
            cuentacorrienteEditar.Saldo = cuentacorrienteDto.Saldo;
            cuentacorrienteEditar.LimiteDeuda = cuentacorrienteDto.LimiteDeuda;
            cuentacorrienteEditar.LimiteDeudaActivo = cuentacorrienteDto.LimiteDeudaActivo;
            cuentacorrienteEditar.FechaVencimiento = cuentacorrienteDto.FechaVencimiento;
            cuentacorrienteEditar.NombreCuentaCorriente = cuentacorrienteDto.nombreCuentaCorriente;

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

            var cuentacorrienteBusqueda = context.CuentaCorriente.FirstOrDefault(x => x.CuentaCorrienteId == cuentacorrienteId);

            if (cuentacorrienteBusqueda == null)
                throw new Exception("No se encontró la cuentacorriente.");

            return new CuentaCorrienteDTO
            {
                Saldo = cuentacorrienteBusqueda.Saldo,
                LimiteDeuda = cuentacorrienteBusqueda.LimiteDeuda,
                nombreCuentaCorriente = cuentacorrienteBusqueda.NombreCuentaCorriente,
                LimiteDeudaActivo = cuentacorrienteBusqueda.LimiteDeudaActivo,
                FechaVencimiento = cuentacorrienteBusqueda.FechaVencimiento,
                CuentaCorrienteId = cuentacorrienteBusqueda.CuentaCorrienteId
            
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
                    nombreCuentaCorriente = x.NombreCuentaCorriente,
                    LimiteDeudaActivo = x.LimiteDeudaActivo,
                    FechaVencimiento = x.FechaVencimiento,
                    CuentaCorrienteId = x.CuentaCorrienteId
                })
                .ToList();
        }

        public IEnumerable<CuentaCorrienteDTO> ObtenerCuentaCorrientesEliminada(string cadenaBuscar)
        {
            using var context = new GestorContextDBFactory().CreateDbContext(null);

            return context.CuentaCorriente
                .Where(x => x.EstaEliminado && x.NombreCuentaCorriente.Contains(cadenaBuscar))
                .Select(x => new CuentaCorrienteDTO
                {
                    Saldo = x.Saldo,
                    LimiteDeuda = x.LimiteDeuda,
                    nombreCuentaCorriente = x.NombreCuentaCorriente,
                    LimiteDeudaActivo = x.LimiteDeudaActivo,
                    FechaVencimiento = x.FechaVencimiento,
                    CuentaCorrienteId = x.CuentaCorrienteId
                })
                .ToList();
        }

        // =====================
        // LOGICA DE NEGOCIO
        // =====================


    }
}
