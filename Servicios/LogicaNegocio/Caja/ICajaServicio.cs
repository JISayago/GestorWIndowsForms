using AccesoDatos;
using Servicios.Helpers.Movimiento;
using Servicios.Helpers.Sistema.FiltrosConsulta;
using Servicios.LogicaNegocio.Caja.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios.LogicaNegocio.Caja
{
    public interface ICajaServicio
    {
        public void AbrirCaja(decimal montoInicial, long empleadoId);
        public void CerrarCaja(long empleadoId);
        public CajaDTO ObtenerCaja(long cajaId);
        public long? ObtenerIdCajaAbierta(GestorContextDB context = null);
        public long? ObtenerIdDeEña(GestorContextDB context);
        public bool ObtenerEstadoCaja();
        public decimal ObtenerSaldoCaja();
        public CajaDTO EstadoInicioCaja();
        public void RegistrarTransaccion(GestorContextDB context, decimal monto, TipoMovimiento tipo, long cajaId);
        public void ActualizarSaldoCaja(AccesoDatos.Entidades.Caja caja, TipoMovimiento tipo, decimal monto);
        public ResultadoPaginacion<CajaDTO> ObtenerCajas(FiltroConsulta filtros);
        public List<CajaDTO> ObtenerUltimasXCajas(int cantidadDeCajas);
        public List<CajaDTO> ObtenerCajasUltimosXDias(int cantidadDeDias);
        public List<CajaDTO> ObtenerLasCajasDeXAño(int AñoDeLasCajas);
        public List<CajaDTO> ObtenerCajasPorMesYAño(int mesDeLasCajas, int añoDeLasCajas);
        public CajaDTO ObtenerCajaAbierta(long? cajaId);

    }
}
