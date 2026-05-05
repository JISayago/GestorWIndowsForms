using Servicios.LogicaNegocio.PantallaPrincipal.DTO;
using Servicios.LogicaNegocio.Producto.DTO;
using Servicios.LogicaNegocio.Venta.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios.Helpers.DatosObligatoriosParaInicioSistema
{
    public class ElementoDePanelesPantallaPrincipal
    {
        public DatosTurnoDTO DatosTurno { get; set; }

        public List<NotificacionDTO> NotificacionesLotes { get; set; }
        public List<NotificacionDTO> NotificacionesPromociones { get; set; }
        public List<NotificacionDTO> NotificacionesCuentaCorriente { get; set; }

        public List<ProductoDTO> ProductosIniciales { get; set; }
        public List<VentaDTO> VentasIniciales { get; set; }
    }
}
