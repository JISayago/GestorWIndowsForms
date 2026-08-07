using System;

namespace Servicios.LogicaNegocio.Sistema.Administracion.DTO
{
    /// <summary>
    /// Venta liviana para KPIs, gráfico diario y heatmap (sin campos innecesarios).
    /// </summary>
    public class VentaResumenGraficoDTO
    {
        public DateTime FechaVenta { get; set; }
        public decimal Total { get; set; }
    }
}
