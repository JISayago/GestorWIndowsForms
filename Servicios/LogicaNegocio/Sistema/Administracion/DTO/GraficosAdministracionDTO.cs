using Servicios.LogicaNegocio.Caja.DTO;
using System.Collections.Generic;

namespace Servicios.LogicaNegocio.Sistema.Administracion.DTO
{
    public class GraficosAdministracionDTO
    {
        public List<CajaDTO> CajasMes { get; set; }

        /// <summary>Ventas del mes filtrado (FechaVenta + Total).</summary>
        public List<VentaResumenGraficoDTO> VentasMes { get; set; }

        /// <summary>Agregado por día del mes anterior (comparativo diario / KPI).</summary>
        public List<VentaDiaAgregadoDTO> VentasMesAnteriorPorDia { get; set; }

        /// <summary>Agregado mensual del año filtrado (12 filas máx).</summary>
        public List<VentaMesAgregadoDTO> VentasAnioPorMes { get; set; }

        /// <summary>Agregado mensual del año anterior.</summary>
        public List<VentaMesAgregadoDTO> VentasAnioAnteriorPorMes { get; set; }

        public List<PagoTipoResumenDTO> PagosMes { get; set; }
        public List<ProductoTopDTO> TopProductosMes { get; set; }
        public decimal TotalGastosMes { get; set; }
    }
}
