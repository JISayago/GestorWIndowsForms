using Servicios.LogicaNegocio.Caja;
using Servicios.LogicaNegocio.Sistema.Administracion.DTO;
using Servicios.LogicaNegocio.Venta;
using Servicios.Helpers.VentaEnum;
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
            int mesAnterior = mes == 1 ? 12 : mes - 1;
            int anioAnterior = mes == 1 ? año - 1 : año;

            return new GraficosAdministracionDTO
            {
                CajasMes = _cajaServicio.ObtenerCajasPorMesYAño(mes, año),

                Cajas31Dias = _cajaServicio.ObtenerCajasUltimosXDias(31),

                VentasMes = _ventaServicio.ObtenerVentasConfirmadasPorMesYAño(mes, año),

                VentasMesAnterior = _ventaServicio.ObtenerVentasConfirmadasPorMesYAño(mesAnterior, anioAnterior),

                CajasAnio = _cajaServicio.ObtenerLasCajasDeXAño(año),

                VentasAnio = _ventaServicio.ObtenerVentasConfirmadasAnio(año)
            };
        }
    }
}
