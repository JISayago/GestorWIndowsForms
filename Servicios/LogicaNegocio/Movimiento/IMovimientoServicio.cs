using AccesoDatos;
using Servicios.Helpers.Movimiento;
using Servicios.Helpers.Sistema.FiltrosConsulta;
using Servicios.LogicaNegocio.Movimiento.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Servicios.LogicaNegocio.Movimiento
{
    public interface IMovimientoServicio
    {
        /// <summary>Punto único de alta. No guarda ni abre transacciones; devuelve null si el monto es 0.</summary>
        AccesoDatos.Entidades.Movimiento? Registrar(GestorContextDB context, NuevoMovimiento datos);
        void CrearMovimientoVenta(long ventaId, decimal monto,int estado, TipoMovimientoDetalle detalleTipo, TipoEntidadMovimiento tipoEntidad, GestorContextDB context);
        void CrearMovimientoCtaCte(decimal total, long cajaId, long ctacteId, TipoMovimientoDetalle detalleTipo, bool esPago, GestorContextDB context);
        void CrearMovimientoGasto(long gastoId, decimal monto, TipoMovimientoDetalle detalleTipo, GestorContextDB context);
        ResultadoPaginacion<MovimientoDTO> ObtenerMovimientos(FiltroConsulta filtros);
        MovimientoHelperDTO ObtenerDatosParaMovimientoConsulta(long movimientoId);
    }
}
