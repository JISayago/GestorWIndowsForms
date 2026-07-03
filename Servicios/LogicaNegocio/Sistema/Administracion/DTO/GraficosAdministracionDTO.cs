using Servicios.LogicaNegocio.Caja.DTO;
using Servicios.LogicaNegocio.Venta.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios.LogicaNegocio.Sistema.Administracion.DTO
{
    public class GraficosAdministracionDTO
    {
        public List<CajaDTO> CajasMes { get; set; }
        public List<CajaDTO> Cajas31Dias { get; set; }

        public List<VentaDTO> VentasMes { get; set; }
        public List<CajaDTO> CajasAnio { get; set; }
        public List<VentaDTO> VentasAnio { get; set; }
    }
}
