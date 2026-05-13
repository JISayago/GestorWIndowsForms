using AccesoDatos;
using AccesoDatos.Entidades;
using Microsoft.EntityFrameworkCore;
using Servicios.Helpers.Cliente;
using Servicios.Helpers.Cliente.CtaCte;
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

            // =========================================================
            // 🧠 CORE: ESTADO + ELIMINADOS
            // =========================================================

            bool hayFiltroEstado =
     filtros.Filtro2 != null &&
     !string.IsNullOrWhiteSpace(filtros.Filtro2.ToString());

            // 👉 HISTÓRICO
            if (filtros.Bool2)
            {
                // no filtrar nada
            }
            // 👉 ELIMINADOS
            else if (filtros.Bool1)
            {
                query = query.Where(c => c.Persona.EstaEliminado);
            }
            // 👉 DEFAULT SOLO SI NO HAY FILTRO EXPLÍCITO
            else if (!hayFiltroEstado)
            {
                query = query.Where(c =>
                    !c.Persona.EstaEliminado &&
                    c.Estado == (int)Helpers.Cliente.EstadoCliente.Activo);
            }

            // =========================================================
            // 🔍 BUSQUEDA
            // =========================================================

            if (!string.IsNullOrWhiteSpace(filtros.TextoBuscar))
            {
                var texto = filtros.TextoBuscar.Trim();

                switch (filtros.Filtro1?.ToString())
                {
                    case "ApyNom":
                        query = query.Where(c =>
                            c.Persona.Nombre.Contains(texto) ||
                            c.Persona.Apellido.Contains(texto));
                        break;

                    case "Dni":
                        query = query.Where(c =>
                            c.Persona.Dni.Contains(texto));
                        break;

                    case "Telefono":
                        query = query.Where(c =>
                            c.Persona.Telefono.Contains(texto) ||
                            c.Persona.Telefono2.Contains(texto));
                        break;

                    case "Email":
                        query = query.Where(c =>
                            c.Persona.Email.Contains(texto));
                        break;

                    default:
                        query = query.Where(c =>
                            c.Persona.Nombre.Contains(texto) ||
                            c.Persona.Apellido.Contains(texto) ||
                            c.Persona.Dni.Contains(texto));
                        break;
                }
            }

            // =========================================================
            // 📌 FILTRO EXTRA (cbx2)
            // =========================================================

            if (filtros.Filtro2 != null &&
                int.TryParse(filtros.Filtro2.ToString(), out var tipoFiltro))
            {
                switch ((TipoFiltroCliente)tipoFiltro)
                {
                    case TipoFiltroCliente.Activo:
                        query = query.Where(c =>
                            c.Estado == (int)Helpers.Cliente.EstadoCliente.Activo &&
                            !c.Persona.EstaEliminado);
                        break;

                    case TipoFiltroCliente.Baja:
                        query = query.Where(c =>
                          c.Estado == (int)Helpers.Cliente.EstadoCliente.Baja);
                        break;

                    case TipoFiltroCliente.Inhabilitado:
                        query = query.Where(c =>
                            c.Estado == (int)Helpers.Cliente.EstadoCliente.Inhabilitado);
                        break;

                    case TipoFiltroCliente.ConCtaCte:
                        query = query.Where(c => c.CuentaCorriente != null);
                        break;

                    case TipoFiltroCliente.SinCtaCte:
                        query = query.Where(c => c.CuentaCorriente == null);
                        break;
                }
            }

            // =========================================================
            // 📅 FECHAS
            // =========================================================

            bool usaFechasManual = filtros.FechaDesde.HasValue || filtros.FechaHasta.HasValue;

            if (usaFechasManual && filtros.Filtro3 != null &&
                int.TryParse(filtros.Filtro3.ToString(), out var tipoFecha))
            {
                switch ((TipoFiltroCliente)tipoFecha)
                {
                    case TipoFiltroCliente.FechaAlta:

                        if (filtros.FechaDesde.HasValue)
                            query = query.Where(c => c.FechaAlta >= filtros.FechaDesde.Value);

                        if (filtros.FechaHasta.HasValue)
                        {
                            var hasta = filtros.FechaHasta.Value.AddDays(1);
                            query = query.Where(c => c.FechaAlta < hasta);
                        }

                        break;

                    case TipoFiltroCliente.FechaBaja:

                        query = query.Where(c => c.FechaBaja.HasValue);

                        if (filtros.FechaDesde.HasValue)
                            query = query.Where(c => c.FechaBaja.Value >= filtros.FechaDesde.Value);

                        if (filtros.FechaHasta.HasValue)
                        {
                            var hasta = filtros.FechaHasta.Value.AddDays(1);
                            query = query.Where(c => c.FechaBaja.Value < hasta);
                        }

                        break;
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

            if (totalPaginas <= 0)
                totalPaginas = 1;

            if (filtros.Page > totalPaginas)
                filtros.Page = totalPaginas;

            if (filtros.Page < 1)
                filtros.Page = 1;

            // =========================================================
            // 📦 DATA
            // =========================================================

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

                    Estado = c.Estado
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
