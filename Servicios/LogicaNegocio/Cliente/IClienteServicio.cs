using Servicios.Helpers.Sistema;
using Servicios.Helpers.Sistema.FiltrosConsulta;
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
        ResultadoPaginacion<ClienteDTO> ObtenerClientes(FiltroConsulta filtros);
        ClienteDTO ObtenerClientePorId(long personaId);
        ClienteDTO ObtenerConsumidorFinal();
        ClienteDTO ObtenerClientePorNumero(string numero);
        EstadoOperacion Insertar(ClienteDTO clienteDto);
        EstadoOperacion Modificar(ClienteDTO clienteDto, long? clienteId);
        EstadoOperacion Eliminar(long clienteId);
    }
}
