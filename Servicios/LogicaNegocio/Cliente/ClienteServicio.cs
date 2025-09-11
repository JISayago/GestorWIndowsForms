using AccesoDatos;
using AccesoDatos.Entidades;
using Microsoft.EntityFrameworkCore;
using Servicios.Helpers;
using Servicios.LogicaNegocio.Cliente.DTO;

namespace Servicios.LogicaNegocio.Cliente
{
    public class ClienteServicio : IClienteServicio
    {
        public EstadoOperacion Eliminar(long clienteId)
        {
            var context = new GestorContextDBFactory().CreateDbContext(null);
            var clienteEliminar = context.Cliente
                    .Include(e => e.Persona)
                    .FirstOrDefault(x => x.PersonaId == clienteId);

            if (clienteEliminar == null || clienteEliminar.Persona.EstaEliminado) throw new Exception($" No se encontro el Cliente: {clienteEliminar.Persona}");

            clienteEliminar.Persona.EstaEliminado = true;

            context.SaveChanges();
            return new EstadoOperacion
            {
                Exitoso = true,
                Mensaje = $"El cliente {clienteEliminar.Persona.Nombre} {clienteEliminar.Persona.Apellido} fue eliminado correctamente."
            };
        }

        public EstadoOperacion Insertar(ClienteDTO clienteDto)
        {
            var context = new GestorContextDBFactory().CreateDbContext(null);

            if (context.Personas.Any(p => p.Dni == clienteDto.Dni))
                return new EstadoOperacion
                {
                    Exitoso = false,
                    Mensaje = "Ya existe una persona con el mismo DNI."
                };

            var persona = new Persona
            {
                Nombre = clienteDto.Nombre,
                Apellido = clienteDto.Apellido,
                Dni = clienteDto.Dni,
                Cuil = clienteDto.Cuil,
                Telefono = clienteDto.Telefono,
                Telefono2 = clienteDto.Telefono2,
                Email = clienteDto.Email,
                Direccion = clienteDto.Direccion,
                EstaEliminado = false,
                FechaNacimiento = clienteDto.FechaNacimiento
            };

            context.Personas.Add(persona);
            context.SaveChanges();

            var cliente = new AccesoDatos.Entidades.Cliente
            {
                PersonaId = persona.PersonaId,
                NumeroCliente = clienteDto.NumeroCliente,
                FechaAlta = clienteDto.FechaAlta,
                Estado = 0
            };

            context.Cliente.Add(cliente);
            context.SaveChanges();
            return new EstadoOperacion
            {
                Exitoso = true,
                Mensaje = "Cliente creado correctamente.",
                EntidadId = cliente.PersonaId
            };
        }

        public EstadoOperacion Modificar(ClienteDTO clienteDto, long? clienteId)
        {
            var context = new GestorContextDBFactory().CreateDbContext(null);

            var clienteEditar = context.Cliente
                .Include(e => e.Persona)
                .FirstOrDefault(x => x.PersonaId == clienteId);

            if (clienteEditar == null)
            {
                return new EstadoOperacion
                {
                    Exitoso = false,
                    Mensaje = "Cliente no encontrado."
                };
            }

            bool dniDuplicado = context.Personas
                .Any(p => p.Dni == clienteDto.Dni && p.PersonaId != clienteEditar.PersonaId);

            if (dniDuplicado)
            {
                return new EstadoOperacion
                {
                    Exitoso = false,
                    Mensaje = "Ya existe una persona con el mismo DNI."
                };
            }

            var persona = clienteEditar.Persona;
            persona.Nombre = clienteDto.Nombre;
            persona.Apellido = clienteDto.Apellido;
            persona.Dni = clienteDto.Dni;
            persona.Cuil = clienteDto.Cuil;
            persona.Telefono = clienteDto.Telefono;
            persona.Telefono2 = clienteDto.Telefono2;
            persona.Email = clienteDto.Email;
            persona.Direccion = clienteDto.Direccion;
            persona.EstaEliminado = clienteDto.EstaEliminado;
            persona.FechaNacimiento = clienteDto.FechaNacimiento;

            clienteEditar.NumeroCliente = clienteDto.NumeroCliente;
            clienteEditar.FechaAlta = clienteDto.FechaAlta;
            clienteEditar.FechaBaja = clienteDto.FechaBaja;
            clienteEditar.Estado = clienteDto.Estado;



            context.SaveChanges();

            return new EstadoOperacion
            {
                Exitoso = true,
                Mensaje = "Cliente modificado correctamente.",
                EntidadId = clienteEditar.PersonaId
            };
        }

        public ClienteDTO ObtenerClientePorId(long personaId)
        {
            using var context = new GestorContextDBFactory().CreateDbContext(null);
            var cliente = context.Cliente
                 .AsNoTracking()
                 .Include(e => e.Persona)
                 .Where(e => e.Persona != null && e.PersonaId == personaId && !e.Persona.EstaEliminado)
                 .Select(e => new ClienteDTO
                 {
                     PersonaId = e.PersonaId,
                     Nombre = e.Persona.Nombre,
                     Apellido = e.Persona.Apellido,
                     Dni = e.Persona.Dni,
                     Cuil = e.Persona.Cuil,
                     Telefono = e.Persona.Telefono,
                     Telefono2 = e.Persona.Telefono2,
                     Email = e.Persona.Email,
                     Direccion = e.Persona.Direccion,
                     FechaNacimiento = e.Persona.FechaNacimiento,
                     EstaEliminado = e.Persona.EstaEliminado,
                     NumeroCliente = e.NumeroCliente,
                     FechaAlta = e.FechaAlta,
                     FechaBaja = e.FechaBaja,
                     Estado = e.Estado
                 })
                 .FirstOrDefault();
            return cliente;
        }

        public IEnumerable<ClienteDTO> ObtenerClientes(string cadenabuscar)
        {
            using var context = new GestorContextDBFactory().CreateDbContext(null);

            var clientes = context.Cliente
            .AsNoTracking()
            .Include(e => e.Persona)
            .Where(e => e.Persona != null && !e.Persona.EstaEliminado &&
                (e.Persona.Nombre.Contains(cadenabuscar)
                || e.Persona.Apellido.Contains(cadenabuscar)
                || e.Persona.Dni == cadenabuscar
                || e.Persona.Email == cadenabuscar))
            .ToList()
            .Select(e => new ClienteDTO
            {
                PersonaId = e.PersonaId,
                Nombre = e.Persona.Nombre,
                Apellido = e.Persona.Apellido,
                Dni = e.Persona.Dni,
                Cuil = e.Persona.Cuil,
                Telefono = e.Persona.Telefono,
                Telefono2 = e.Persona.Telefono2,
                Email = e.Persona.Email,
                Direccion = e.Persona.Direccion,
                FechaNacimiento = e.Persona.FechaNacimiento,
                EstaEliminado = e.Persona.EstaEliminado,
                NumeroCliente = e.NumeroCliente,
                FechaAlta = e.FechaAlta,
                FechaBaja = e.FechaBaja,
                Estado = e.Estado,
                EstadoDescripcion = Enum.GetName(typeof(EstadoCliente), e.Estado) ?? "Desconocido"
            })
            .ToList();


            return clientes;
        }

        public IEnumerable<ClienteDTO> ObtenerClientesEliminados(string cadenabuscar)
        {
            using var context = new GestorContextDBFactory().CreateDbContext(null);

            var clientes = context.Cliente
                .AsNoTracking()
                .Include(e => e.Persona)
                .Where(e => e.Persona != null && e.Persona.EstaEliminado && (e.Persona.Nombre.Contains(cadenabuscar)
                                || e.Persona.Apellido.Contains(cadenabuscar)
                                || e.Persona.Dni == (cadenabuscar)
                                || e.Persona.Email == (cadenabuscar)))
                .ToList()
                .Select(e => new ClienteDTO
                {
                    PersonaId = e.PersonaId,
                    Nombre = e.Persona.Nombre,
                    Apellido = e.Persona.Apellido,
                    Dni = e.Persona.Dni,
                    Cuil = e.Persona.Cuil,
                    Telefono = e.Persona.Telefono,
                    Telefono2 = e.Persona.Telefono2,
                    Email = e.Persona.Email,
                    Direccion = e.Persona.Direccion,
                    FechaNacimiento = e.Persona.FechaNacimiento,
                    EstaEliminado = e.Persona.EstaEliminado,
                    NumeroCliente = e.NumeroCliente,
                    FechaAlta = e.FechaAlta,
                    FechaBaja = e.FechaBaja,
                    Estado = e.Estado,
                    EstadoDescripcion = Enum.GetName(typeof(EstadoCliente), e.Estado) ?? "Desconocido"
                })
                .ToList();

            return clientes;
        }
    }
}
