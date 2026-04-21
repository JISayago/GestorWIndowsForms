using AccesoDatos;
using AccesoDatos.Entidades;
using Microsoft.EntityFrameworkCore;
using Servicios.Helpers.CtaCte;
using Servicios.Helpers.Producto;
using Servicios.Helpers.Sistema;
using Servicios.Helpers.Sistema.FiltrosConsulta;
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

            context.Personas.Add(persona); //Ser carga antes una persona que un cliente, en caso de no cargar el cliente igual se carga la persona
            context.SaveChanges();

            var cliente = new AccesoDatos.Entidades.Cliente
            {
                PersonaId = persona.PersonaId,
                FechaAlta = clienteDto.FechaAlta,
                NumeroCliente = string.IsNullOrEmpty(clienteDto.NumeroCliente) ? $"{DateTime.Now:ddMMyyyyHHmmssfff}{persona.PersonaId}" : "0",
                //CuentaCorriente = clienteDto != null ? context.CuentaCorriente.Find(clienteDto.CuentaCorrienteId) : null,
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
        public ClienteDTO ObtenerClientePorNumero(string numero)
        {
            using var context = new GestorContextDBFactory().CreateDbContext(null);
            var cliente = context.Cliente
                 .AsNoTracking()
                 .Include(c => c.Persona)
                 .Where(c => c.Persona != null && c.NumeroCliente == numero)
                 .Select(c => new ClienteDTO
                 {
                     PersonaId = c.PersonaId,
                     Nombre = c.Persona.Nombre,
                     Apellido = c.Persona.Apellido,
                     Dni = c.Persona.Dni,
                     Cuil = c.Persona.Cuil,
                     Telefono = c.Persona.Telefono,
                     Telefono2 = c.Persona.Telefono2,
                     Email = c.Persona.Email,
                     Direccion = c.Persona.Direccion,
                     FechaNacimiento = c.Persona.FechaNacimiento,
                     EstaEliminado = c.Persona.EstaEliminado,
                     NumeroCliente = c.NumeroCliente,
                     FechaAlta = c.FechaAlta,
                     FechaBaja = c.FechaBaja,
                     Estado = c.Estado
                 })
                 .FirstOrDefault();
            return cliente;
        }

        public ResultadoPaginacion<ClienteDTO> ObtenerClientes(FiltroConsulta filtros)
        {
            using var context = new GestorContextDBFactory().CreateDbContext(null);

            var query = context.Cliente
                .AsNoTracking()
                .Include(c => c.Persona)
                .Where(c => c.Persona != null)
                .AsQueryable();

            // 🔴 ELIMINADOS (centralizado)
            query = filtros.VerEliminados
                ? query.Where(c => c.Persona.EstaEliminado)
                : query.Where(c => !c.Persona.EstaEliminado);

            // 🔍 BUSQUEDA
            if (!string.IsNullOrWhiteSpace(filtros.TextoBuscar))
            {
                var texto = filtros.TextoBuscar;

                switch (filtros.Extra?.ToString())
                {
                    case "Nombre":
                        query = query.Where(c => c.Persona.Nombre.Contains(texto));
                        break;

                    case "Apellido":
                        query = query.Where(c => c.Persona.Apellido.Contains(texto));
                        break;

                    case "Dni":
                        query = query.Where(c => c.Persona.Dni.Contains(texto));
                        break;

                    case "Telefono":
                        query = query.Where(c => c.Persona.Telefono.Contains(texto)
                                               || c.Persona.Telefono2.Contains(texto));
                        break;

                    case "Email":
                        query = query.Where(c => c.Persona.Email.Contains(texto));
                        break;

                    default:
                        query = query.Where(c =>
                            c.Persona.Nombre.Contains(texto) ||
                            c.Persona.Apellido.Contains(texto) ||
                            c.Persona.Dni.Contains(texto));
                        break;
                }
            }

            // 📅 FECHAS (si querés usarlo con FechaAlta / Baja)
            var tipoFecha = (TipoFiltroFecha?)filtros.Extra2;

            //if (tipoFecha.HasValue && tipoFecha != TipoFiltroFecha.Ninguno)   VER FILTROS DE FECHA SEGUN CORRESPONDAw
            //{
            //    if (tipoFecha == TipoFiltroFecha.Alta)
            //    {
            //        if (filtros.FechaDesde.HasValue)
            //            query = query.Where(c => c.FechaAlta >= filtros.FechaDesde.Value);

            //        if (filtros.FechaHasta.HasValue)
            //            query = query.Where(c => c.FechaAlta <= filtros.FechaHasta.Value);
            //    }

            //    if (tipoFecha == TipoFiltroFecha.Baja)
            //    {
            //        if (filtros.FechaDesde.HasValue)
            //            query = query.Where(c => c.FechaBaja.HasValue && c.FechaBaja >= filtros.FechaDesde.Value);

            //        if (filtros.FechaHasta.HasValue)
            //            query = query.Where(c => c.FechaBaja.HasValue && c.FechaBaja <= filtros.FechaHasta.Value);
            //    }
            //}

            // 📊 TOTAL (antes de paginar)
            var total = query.Count();

            // 📄 PAGINACION
            var data = query
                .OrderBy(c => c.PersonaId)
                .Skip((filtros.Page - 1) * filtros.PageSize)
                .Take(filtros.PageSize)
                .Select(c => new ClienteDTO
                {
                    PersonaId = c.PersonaId,
                    Nombre = c.Persona.Nombre,
                    Apellido = c.Persona.Apellido,
                    Dni = c.Persona.Dni,
                    Cuil = c.Persona.Cuil,
                    Telefono = c.Persona.Telefono,
                    Telefono2 = c.Persona.Telefono2,
                    Email = c.Persona.Email,
                    Direccion = c.Persona.Direccion,
                    FechaNacimiento = c.Persona.FechaNacimiento,
                    EstaEliminado = c.Persona.EstaEliminado,
                    NumeroCliente = c.NumeroCliente,
                    FechaAlta = c.FechaAlta,
                    FechaBaja = c.FechaBaja,
                    Estado = c.Estado,
                    //EstadoDescripcion = Enum.GetName(typeof(EstadoCliente), c.Estado) ?? "Desconocido"
                    EstadoDescripcion = ""
                })
                .ToList();

            return new ResultadoPaginacion<ClienteDTO>
            {
                Items = data,
                TotalRegistros = total,
                Page = filtros.Page,
                PageSize = filtros.PageSize
            };
        }
    }
}
