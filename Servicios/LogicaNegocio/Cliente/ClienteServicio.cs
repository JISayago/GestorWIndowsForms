using AccesoDatos;
using AccesoDatos.Entidades;
using Servicios.Helpers;
using Servicios.LogicaNegocio.Cliente.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios.LogicaNegocio.Cliente
{
    public class ClienteServicio : IClienteServicio
    {
        public EstadoOperacion Eliminar(long clienteId)
        {
            throw new NotImplementedException();
        }

        public EstadoOperacion Insertar(ClienteDTO clienteDto)
        {
            throw new NotImplementedException();
        }

        public EstadoOperacion Modificar(ClienteDTO clienteDto, long? clienteId)
        {
            throw new NotImplementedException();
        }

        public ClienteDTO ObtenerClientePorId(long personaId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ClienteDTO> ObtenerClientes(string cadenabuscar)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ClienteDTO> ObtenerClientesEliminados(string cadenabuscar)
        {
            throw new NotImplementedException();
        }
    }
}
