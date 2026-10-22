using Servicios.Helpers.Cliente;
using Servicios.Helpers.Cliente.CtaCte;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios.LogicaNegocio.Cliente.DTO
{
    public class ClienteDTO
    {
        //Datos PERSONA
        public long PersonaId { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string NombreCompleto => $"{Nombre} {Apellido}";
        public string Dni { get; set; }
        public string Cuil { get; set; }
        public string Telefono { get; set; }
        public string? Telefono2 { get; set; }
        public string? Email { get; set; }
        public string? Direccion { get; set; }
        public DateTime? FechaNacimiento { get; set; }
        public bool EstaEliminado { get; set; }
        //Datos CLIENTE
        public string NumeroCliente { get; set; } //Borrarlo????
        public DateTime FechaAlta { get; set; } //Cargar al crear entidad
        public DateTime? FechaBaja { get; set; } // Puede ser null si sigue siendo cliente
        public int Estado { get; set; } //Que tipos de estados puede tener un cliente?
        public string EstadoDescripcion
        {
            get
            {
                // prioridad: eliminado manda
                if (EstaEliminado)
                    return "Eliminado";

                return Estado switch
                {
                    (int)EstadoCliente.Activo => "Activo",
                    (int)EstadoCliente.Baja => "Baja",
                    (int)EstadoCliente.Inhabilitado => "Inhabilitado",
                    _ => "Desconocido"
                };
            }
        }
        public long? CuentaCorrienteId { get; set; } // Puede ser null si no tiene cuenta corriente asociada

        public string TieneCuentaCorriente
        {
            get
            {
                if(CuentaCorrienteId != null)
                    return "Asignada";

                return "Sin Asignar";
            }
        }

        public int EstadoCtaCte { get; set; } = -1;

        public string DescripcionEstadoCtaCte
        {
            get
            {
                switch (EstadoCtaCte)
                {
                    case (int)EstadoCuentaCorriente.Activa:
                        return "Activa";

                    case (int)EstadoCuentaCorriente.Cerrada:
                        return "Cerrada";

                    case (int)EstadoCuentaCorriente.Suspendida:
                        return "Suspendida";

                    default:
                        return "Sin Asignar";
                }
            }
        }

    }
}
