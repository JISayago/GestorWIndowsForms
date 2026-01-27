using Servicios.Helpers;
using Servicios.LogicaNegocio.Cliente.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios.LogicaNegocio.Cliente
{
    public interface IClienteServicio
    {
        IEnumerable<ClienteDTO> ObtenerClientes(string cadenabuscar);
        IEnumerable<ClienteDTO> ObtenerClientesEliminados(string cadenabuscar);
        ClienteDTO ObtenerClientePorId(long personaId);
        ClienteDTO ObtenerClientePorNumero(string numero);
        EstadoOperacion Insertar(ClienteDTO clienteDto);
        EstadoOperacion Modificar(ClienteDTO clienteDto, long? clienteId);
        EstadoOperacion Eliminar(long clienteId);
    }
}
