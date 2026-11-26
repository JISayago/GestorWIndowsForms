using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios.LogicaNegocio.PantallaPrincipal.DTO
{
    /// <summary>
    /// Una sesión de la tabla UsuarioSesion lista para mostrar: quién es, cuándo entró/salió y si es la
    /// sesión principal del terminal o una secundaria (EsSecundaria + SesionPrincipalId).
    /// </summary>
    public class SesionInfoDTO
    {
        public long UsuarioSesionId { get; set; }
        public long UsuarioId { get; set; }
        public string Username { get; set; } = "";

        /// <summary>Nombre y apellido. Lo completa la capa de presentación; puede venir vacío.</summary>
        public string NombreCompleto { get; set; } = "";

        public DateTime FechaLogin { get; set; }
        public DateTime? FechaLogout { get; set; }
        public bool Activa { get; set; }
        public bool EsSecundaria { get; set; }
        public long? SesionPrincipalId { get; set; }
    }
}
