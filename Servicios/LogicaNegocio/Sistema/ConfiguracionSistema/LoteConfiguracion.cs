using Servicios.Helpers.Sistema;
using System.Collections.Generic;
using System.Text.Json;

namespace Servicios.LogicaNegocio.Configuracion
{
    public class RegistroConfiguracion
    {
        public string Clave { get; set; }
        public string Texto { get; set; }
        public byte[] Binario { get; set; }
    }

    /// <summary>
    /// Cambios pendientes de configuración. Las pantallas lo van llenando y el servicio
    /// lo guarda todo junto en una sola transacción (un único SaveChanges).
    /// </summary>
    public class LoteConfiguracion
    {
        internal List<RegistroConfiguracion> Items { get; } = new List<RegistroConfiguracion>();

        public void Texto<T>(ClaveConfiguracion clave, T valor)
        {
            Items.Add(new RegistroConfiguracion { Clave = clave.ToString(), Texto = JsonSerializer.Serialize(valor) });
        }

        /// <summary>bytes == null deja la clave vacía (ej.: "Quitar logo").</summary>
        public void Binario(ClaveConfiguracion clave, byte[] bytes)
        {
            Items.Add(new RegistroConfiguracion { Clave = clave.ToString(), Binario = bytes });
        }
    }
}