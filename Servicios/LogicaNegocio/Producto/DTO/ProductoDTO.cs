using Microsoft.Identity.Client;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios.LogicaNegocio.Producto.DTO
{
    public class ProductoDTO
    {
        public long ProductoId { get; set; }
        public long IdMarca { get; set; }
        public long IdRubro { get; set; }

        public decimal Stock { get; set; }
        public decimal PrecioCosto { get; set; }
        public decimal PrecioVenta { get; set; }
        public string Descripcion { get; set; }
        public bool EstaEliminado { get; set; }
        public int Estado { get; set; }
        public string Medida { get; set; }
        public string UnidadMedida { get; set; }
        public string? Codigo { get; set; }
        public string? CodigoBarra { get; set; }
        public bool IvaIncluidoPrecioFinal { get; set; }
        public bool EsFraccionable { get; set; }

        // Relación simplificada
        public string MarcaNombre { get; set; }
        public string RubroNombre { get; set; }

        // Relación a múltiples categorías
        public List<long> CategoriaIds { get; set; }
    }
}

