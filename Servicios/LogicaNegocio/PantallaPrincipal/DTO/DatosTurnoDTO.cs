using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Servicios.LogicaNegocio.PantallaPrincipal.DTO
{
    public class DatosTurnoDTO
    {
        public long? CajaId { get; set; }
        public decimal MontoInicial { get; set; }
        public decimal Ingresos { get; set; }
        public decimal TotalCaja { get; set; }
        public bool CajaAbierta { get; set; }
        public long UsuarioId { get; set; }
        public string UsuarioLogeado { get; set; } = "";
        public string HoraIngresoUsuario { get; set; } = "00:00:00";
        public TimeSpan TiempoTranscurrido { get; set; }
        public string NotasTurno { get; set; } = "";
    }
}
