using AccesoDatos;
using Servicios.LogicaNegocio.Caja;
using Servicios.LogicaNegocio.Caja.DTO;
using Servicios.LogicaNegocio.Sistema.DTO;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios.LogicaNegocio.Sistema
{
    public class DetallesSistemaServicio : IDetallesSistemaServicio
    {
        public InfoSistemaDTO ObtenerInfoSistema()
        {
            using var context = new GestorContextDBFactory().CreateDbContext(null);

            // Corrección: pasar la variable 'context' como argumento
            var usuariosLogueados = ObtenerUsuariosLogueados(context);
            var cajaAbierta = ObtenerCajaAbierta(context);

            var lineasPrincipal = new List<string>();

            // Usuarios logueados
            if (usuariosLogueados.Any())
            {
                lineasPrincipal.Add($"• Usuarios logueados: {usuariosLogueados.Count}");

                foreach (var usuario in usuariosLogueados)
                    lineasPrincipal.Add($"   • {usuario}");
            }
            else
            {
                lineasPrincipal.Add("• No hay usuarios logueados");
            }

            // Caja
            if (cajaAbierta != null)
            {
                lineasPrincipal.Add($"• Caja abierta: ${cajaAbierta.SaldoActual:N2}");
            }
            else
            {
                lineasPrincipal.Add("• No hay caja abierta");
            }

            var textoPrincipal = string.Join(Environment.NewLine, lineasPrincipal);

            return new InfoSistemaDTO
            {
                Titulo = "Datos del Sistema",
                TextoPrincipal = textoPrincipal,
                TextoSecundario = $"Actualizado: {DateTime.Now:dd/MM/yyyy HH:mm}",
                Tipo = 1
            };
        }

        // Corrección: quitar el punto y coma y corregir la declaración del método
        private List<string> ObtenerUsuariosLogueados(GestorContextDB context)
        {
            // buscar en la tabla nueva de usuarios logeados. y armando la lista de usuarios logueados. por ahora lo simulo con una lista fija de usuarios
            // Ejemplo simulado
            return new List<string>
            {
                "admin",
                "caja1",
                "ventas3"
            };
        }

            // Corrección: quitar el punto y coma y corregir la declaración del método
        private CajaDTO? ObtenerCajaAbierta(GestorContextDB context)
        {
            var CajaService = new CajaServicio();
            var cajaId = CajaService.ObtenerIdCajaAbierta();
            if (cajaId.HasValue)
            {
            var Cajadto = CajaService.ObtenerCaja((long)cajaId);
            return new CajaDTO
            {
                CajaId = Cajadto.CajaId,
                SaldoInicial = Cajadto.SaldoInicial,
                SaldoActual = Cajadto.SaldoActual,
                FechaInicio = Cajadto.FechaInicio,
                FechaFin = Cajadto.FechaFin,
                TotalIngresos = Cajadto.TotalIngresos,
                TotalEgresos = Cajadto.TotalEgresos,
                BalanceFinal = Cajadto.BalanceFinal,
                EmpleadoApertura = Cajadto.EmpleadoApertura,
                EmpleadoCierre = Cajadto.EmpleadoCierre,
                EstaCerrada = Cajadto.EstaCerrada,
            };
            }
            else
            {
                return null;
            }

        }
    }
}
