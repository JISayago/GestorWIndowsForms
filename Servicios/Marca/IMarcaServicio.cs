using Servicios.Marca.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios.Marca
{
    public interface IMarcaServicio
    {
        IEnumerable<MarcaDTO> ObtenerMarca(string cadenaBuscar);

        //IEnumerable<MarcaDTO> ObtenerMarcaEliminada(string cadenaBuscar);

        MarcaDTO ObtenerPorId(long marca);

        long Insertar(MarcaDTO marcaDTO);

        void Modificar(MarcaDTO marcaDTO);

        void Eliminar(long marcaId);
    }
}
