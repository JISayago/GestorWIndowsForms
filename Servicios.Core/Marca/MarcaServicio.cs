using AccesoDatos;
using Servicios.Core.Marca.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios.Core.Marca
{
    public class MarcaServicio : IMarcaServicio
    {
        public void Eliminar(long marcaId)
        {
            throw new NotImplementedException();
        }

        public long Insertar(MarcaDTO marcaDTO)
        {
            throw new NotImplementedException();
        }

        public void Modificar(MarcaDTO marcaDTO)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<MarcaDTO> ObtenerMarca(string cadenaBuscar)
        {
            throw new NotImplementedException();
        }

        public MarcaDTO ObtenerPorId(long marca)
        {
            throw new NotImplementedException();
        }
    }
}
