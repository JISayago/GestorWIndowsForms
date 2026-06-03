using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios.Helpers.Cliente.DatosPagadorDTO
{
    public class DatosPagadorDTO
    {
        public string Documento { get; set; }
        public string NombreCompleto { get; set; }

        public bool NoEspecificado =>
            string.IsNullOrWhiteSpace(Documento) &&
            string.IsNullOrWhiteSpace(NombreCompleto);
    }
}
