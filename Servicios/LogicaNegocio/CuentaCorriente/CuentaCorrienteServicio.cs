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

            /*if (context.CuentaCorriente.Any(p => p.numn == cuentacorrienteDTO.Nombre))
                return new EstadoOperacion
                {
                    Exitoso = false,
                    Mensaje = "Ya existe una cuentacorriente con el mismo nombre"
                };
            */
            var nuevaCuentaCorriente = new AccesoDatos.Entidades.CuentaCorriente
            {
                Saldo = cuentacorrienteDto.Saldo,
                LimiteCredito = cuentacorrienteDto.LimiteCredito,
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
                .Any(p => p.nombreCuentaCorriente == cuentacorrienteDto.nombreCuentaCorriente && p.nombreCuentaCorriente != cuentacorrienteEditar.nombreCuentaCorriente);
            
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
            cuentacorrienteEditar.LimiteCredito = cuentacorrienteDto.LimiteCredito;

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
                LimiteCredito = cuentacorrienteBusqueda.LimiteCredito
            };
        }

        public IEnumerable<CuentaCorrienteDTO> ObtenerCuentaCorrientes(string cadenaBuscar)
        {
            using var context = new GestorContextDBFactory().CreateDbContext(null);

            return context.CuentaCorriente
                .Where(x => !x.EstaEliminado && x.nombreCuentaCorriente.Contains(cadenaBuscar))
                .Select(x => new CuentaCorrienteDTO
                {
                    Saldo = x.Saldo,
                    LimiteCredito = x.LimiteCredito
                })
                .ToList();
        }

        public IEnumerable<CuentaCorrienteDTO> ObtenerCuentaCorrientesEliminada(string cadenaBuscar)
        {
            using var context = new GestorContextDBFactory().CreateDbContext(null);

            return context.CuentaCorriente
                .Where(x => x.EstaEliminado && x.nombreCuentaCorriente.Contains(cadenaBuscar))
                .Select(x => new CuentaCorrienteDTO
                {
                    Saldo = x.Saldo,
                    LimiteCredito = x.LimiteCredito
                })
                .ToList();
        }
    }
}
