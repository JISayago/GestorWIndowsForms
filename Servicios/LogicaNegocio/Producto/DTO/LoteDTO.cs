using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios.LogicaNegocio.Producto.DTO
{
    public class LoteDTO
    {
        public long Id { get; set; }
        public string NumeroLote { get; set; } // se genera en el service, creo que no deberia ser null
        public long IdProducto { get; set; }
        public string? NombreProducto { get; set; } = null; // para mostrar en la consulta, no se guarda en la base
        public decimal StockInicial { get; set; }
        public decimal StockActual { get; set; }
        public string Descripcion { get; set; }
        public DateTime FechaAlta { get; set; }
        public DateTime? FechaVencimiento { get; set; }
        public bool EstaVencido { get; set; }

        public string EstaVencidoDescripcion
        {
            get
            {
                if (!FechaVencimiento.HasValue)
                    return "Sin fecha de vencimiento";
                if (EstaVencido)
                    return "Vencido";
                var diasRestantes = (FechaVencimiento.Value - DateTime.Now).Days;
                return $"Vence en {diasRestantes} días";
            }
        }
        public bool EstaActivo { get; set; }
        public string EstaActivoDescripcion
        {
            get
            {
                return EstaActivo ? "Activo" : "Inactivo";
            }
        }
    }
}
