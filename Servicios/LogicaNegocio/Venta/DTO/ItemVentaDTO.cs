using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios.LogicaNegocio.Venta.DTO
{
   public class ItemVentaDTO
    {
        public long ItemId { get; set; }
        public decimal Cantidad { get; set; }

        public decimal PrecioVenta { get; set; }

        public decimal PrecioOferta { get; set; }

        public string Descripcion { get; set; }

        public string Medida { get; set; }

        public string UnidadMedida { get; set; }

        public decimal Stock { get; set; }
        public bool EsOferta { get; set; }

        public string CodigoOferta { get; set; }
        public int TipoOferta { get; set; }

        public decimal Subtotal
        {
            get
            {
                if (!EsOferta)
                    return PrecioVenta * Cantidad;

                return PrecioOferta * Cantidad;
            }
        }
    }
    //public class ItemVentaDTO
    //{
    //    public long ItemId { get; set; }
    //    public decimal Cantidad { get; set; }

    //    public decimal PrecioVenta { get; set; }     // precio base producto
    //    public decimal PrecioOferta { get; set; }    // precio aplicado
    //    public decimal PrecioOriginalOferta { get; set; } // 🔥 combo original

    //    public string Descripcion { get; set; }
    //    public string Medida { get; set; }
    //    public string UnidadMedida { get; set; }

    //    public bool EsOferta { get; set; }
    //    public bool EsOfertaPorGrupo { get; set; }

    //    public string EstadoOferta =>
    //        EsOferta ? "Oferta activa" : "Sin oferta";

    //    public decimal Subtotal
    //    {
    //        get
    //        {
    //            if (!EsOferta)
    //                return PrecioVenta * Cantidad;

    //            if (EsOfertaPorGrupo)
    //                return PrecioOferta * Cantidad;

    //            return PrecioOferta; // combo
    //        }
    //    }
    //}
}
