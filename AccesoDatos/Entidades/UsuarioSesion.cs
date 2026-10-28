
namespace AccesoDatos.Entidades
{
    public class UsuarioSesion
    {
        public long UsuarioSesionId { get; set; }

        public long UsuarioId { get; set; }

        public DateTime FechaLogin { get; set; }

        public DateTime? FechaLogout { get; set; }

        public bool Activa { get; set; }

        // Sesión secundaria: otro usuario (ej. un admin) que opera en el equipo de la sesión
        // principal sin cerrarla. SesionPrincipalId = UsuarioSesionId de la sesión que la lanzó.
        public bool EsSecundaria { get; set; }
        public long? SesionPrincipalId { get; set; }
        public Empleado Usuario { get; set; }
    }
}
