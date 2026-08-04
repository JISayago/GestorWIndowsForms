using AccesoDatos.Entidades;
using Servicios.Helpers.Sistema;
using Servicios.Helpers.Sistema.FiltrosConsulta;
using Servicios.LogicaNegocio.CuentaCorriente.DTO;
using Servicios.LogicaNegocio.Movimiento.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios.LogicaNegocio.CuentaCorriente
{
    public interface ICuentaCorrienteServicio
    {
        ResultadoPaginacion<CuentaCorrienteDTO> ObtenerCuentaCorrientes(FiltroConsulta filtros);
        EstadoOperacion CerrarCuentaCorriente(long ctacteId);
        EstadoOperacion ActivarCuentaCorriente(long ctacteId);
        ResultadoPaginacion<MovimientoDTO> ObtenerMovimientosPorCuentaCorriente(long cuentaCorrienteId, FiltroConsulta filtros);
        CuentaCorrienteDTO ObtenerCuentaCorrientePorId(long cuentacorrienteId);
        EstadoOperacion Insertar(CuentaCorrienteDTO cuentacorrienteDto);
        EstadoOperacion Modificar(CuentaCorrienteDTO cuentacorrienteDto, long? cuentacorrienteId);
        EstadoOperacion Eliminar(long cuentacorrienteId);
        List<long> ObtenerDnisAutorizados(long? cuentaId);
        CuentaCorrienteDTO ObtenerCuentaCorrientePorClienteId(long clienteId);
        bool PuedeComprar(long cuentaId, decimal monto);
        List<CuentaCorrienteDTO> ObtenerCtaCteVencidas(int cantidadDiasVencimiento);

    }
}
