using AccesoDatos.Entidades;
using Servicios.Helpers;
using Servicios.LogicaNegocio.CuentaCorriente.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios.LogicaNegocio.CuentaCorriente
{
    public interface ICuentaCorrienteServicio
    {
        IEnumerable<CuentaCorrienteDTO> ObtenerCuentaCorrientes(string cadenabuscar);
        IEnumerable<CuentaCorrienteDTO> ObtenerCuentaCorrientesEliminada(string cadenabuscar);
        CuentaCorrienteDTO ObtenerCuentaCorrientePorId(long cuentacorrienteId);
        EstadoOperacion Insertar(CuentaCorrienteDTO cuentacorrienteDto);
        EstadoOperacion Modificar(CuentaCorrienteDTO cuentacorrienteDto, long? cuentacorrienteId);
        EstadoOperacion Eliminar(long cuentacorrienteId);
        List<long> ObtenerDnisAutorizados(long? cuentaId);
        CuentaCorrienteDTO ObtenerCuentaCorrientePorClienteId(long clienteId);
        bool PuedeComprar(long cuentaId, decimal monto);

    }
}
