using Servicios.LogicaNegocio.Caja;
using Servicios.LogicaNegocio.Sistema.Administracion.DTO;
using Servicios.LogicaNegocio.Venta;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios.LogicaNegocio.Sistema.Administracion
{
    public class AdministracionGraficosServicios
    {
        private readonly CajaServicio _cajaServicio;
        private readonly VentaServicio _ventaServicio;

        public AdministracionGraficosServicios()
        {
            _cajaServicio = new CajaServicio();
            _ventaServicio = new VentaServicio();
        }

        public GraficosAdministracionDTO ObtenerDatos(int año, int mes)
        {
            return new GraficosAdministracionDTO
            {
                CajasMes = _cajaServicio.ObtenerCajasPorMesYAño(mes, año),

                Cajas31Dias = _cajaServicio.ObtenerCajasUltimosXDias(31),

                VentasMes = _ventaServicio.ObtenerVentasPorMesYAño(mes, año),

                CajasAnio = _cajaServicio.ObtenerLasCajasDeXAño(año),

                VentasAnio = _ventaServicio.ObtenerVentasPorMesYAño(0, año)
            };
        }
    }
}
