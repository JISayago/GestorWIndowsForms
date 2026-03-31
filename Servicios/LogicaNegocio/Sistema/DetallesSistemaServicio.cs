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

            var usuariosLogueados = ObtenerUsuariosLogueados(context);
            var cajaAbierta = ObtenerCajaAbierta();

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

        private List<string> ObtenerUsuariosLogueados(GestorContextDB context)
        {
             return context.Usuarios
                .Where(x => x.Activa)
               .Select(x => $"{x.Usuario.Username} ({x.FechaLogin:dd/MM/yyyy HH:mm:ss})")
                .ToList();
        }

        private CajaDTO? ObtenerCajaAbierta()
        {
            using var context = new GestorContextDBFactory().CreateDbContext(null);
            var cajaService = new CajaServicio();

            var cajaId = cajaService.ObtenerIdCajaAbierta(context);

            if (!cajaId.HasValue)
                return null;

            var caja = cajaService.ObtenerCaja((long)cajaId);

            return new CajaDTO
            {
                CajaId = caja.CajaId,
                SaldoInicial = caja.SaldoInicial,
                SaldoActual = caja.SaldoActual,
                FechaInicio = caja.FechaInicio,
                FechaFin = caja.FechaFin,
                TotalIngresos = caja.TotalIngresos,
                TotalEgresos = caja.TotalEgresos,
                BalanceFinal = caja.BalanceFinal,
                EmpleadoApertura = caja.EmpleadoApertura,
                EmpleadoCierre = caja.EmpleadoCierre,
                EstaCerrada = caja.EstaCerrada
            };
        }
    }
}
