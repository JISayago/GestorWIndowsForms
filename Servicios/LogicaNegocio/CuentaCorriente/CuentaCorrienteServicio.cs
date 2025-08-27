using Servicios.Helpers;
using Servicios.LogicaNegocio.CuentaCorriente.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios.LogicaNegocio.CuentaCorriente
{
    public class CuentaCorrienteServicio : ICuentaCorrienteServicio
    {
        public EstadoOperacion Eliminar(long cuentacorrienteId)
        {
            throw new NotImplementedException();
        }

        public EstadoOperacion Insertar(CuentaCorrienteDTO cuentacorrienteDto)
        {
            throw new NotImplementedException();
        }

        public EstadoOperacion Modificar(CuentaCorrienteDTO cuentacorrienteDto, long? cuentacorrienteId)
        {
            throw new NotImplementedException();
        }

        public CuentaCorrienteDTO ObtenerCuentaCorrientePorId(long cuentacorrienteId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CuentaCorrienteDTO> ObtenerCuentaCorrientes(string cadenabuscar)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CuentaCorrienteDTO> ObtenerCuentaCorrientesEliminados(string cadenabuscar)
        {
            throw new NotImplementedException();
        }
    }
}
