using AccesoDatos;
using AccesoDatos.Entidades;
using Servicios.LogicaNegocio.Cliente;
using Servicios.LogicaNegocio.Cliente.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios.Helpers.DatosObligatorios
{
    internal class ConsumidorFInal
    {
        private readonly static IClienteServicio _clienteServicio = new ClienteServicio();
        public static void Inicializar(GestorContextDB context)
        {
            if (context.Cliente.Any(e => e.NumeroCliente == "00000000"))
                return;
           

            var nuevoCliente = new ClienteDTO
            {
                Nombre = "Consumidor Final",
                Apellido = string.Empty,
                Dni = "00000000",
                Cuil = "00-00000000-0",
                Telefono = "0000000000",
                Email = string.Empty,
                Direccion = "Consumidor Final",
                EstaEliminado = false,
                FechaNacimiento = DateTime.Today.AddYears(-30),
                Telefono2 = string.Empty,
                NumeroCliente = "0", 
                Estado = 1, 
                CuentaCorrienteId = null,
            };

            var response = _clienteServicio.Insertar(nuevoCliente);
        }

    }
}
