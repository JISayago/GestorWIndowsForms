using Servicios.Helpers.Producto;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Servicios.LogicaNegocio.Producto.DTO
{
    public class ProductoOfertaDTO : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this,
                new PropertyChangedEventArgs(propertyName));
        }

        public long ProductoId { get; set; }
        public long IdMarca { get; set; }
        public long IdRubro { get; set; }

        private decimal? _cantidadItemEnOferta;
        public decimal? CantidadItemEnOferta
        {
            get => _cantidadItemEnOferta;
            set
            {
                if (_cantidadItemEnOferta == value) return;

                _cantidadItemEnOferta = value;
                OnPropertyChanged();
            }
        }

        public decimal Stock { get; set; }
        public bool ControlPorLote { get; set; }

        public string ControlLoteDescripcion =>
            ControlPorLote ? "Control Activo" : "Desactivado";

        public decimal PrecioCosto { get; set; }
        public decimal PrecioVenta { get; set; }
        public string Descripcion { get; set; }
        public bool EstaEliminado { get; set; }
        public int Estado { get; set; }

        public string EstadoDescripcion =>
            Estado switch
            {
                (int)EstadoProducto.Disponible => "Disponible",
                (int)EstadoProducto.Vencido => "Vencido",
                (int)EstadoProducto.Discontinuado => "Discontinuado",
                (int)EstadoProducto.SinStock => "Sin Stock",
                _ => "Desconocido"
            };

        public string Medida { get; set; }
        public string UnidadMedida { get; set; }
        public string? Codigo { get; set; }
        public string? CodigoBarra { get; set; }
        public bool IvaIncluidoPrecioFinal { get; set; }
        public bool TieneVencimiento { get; set; }

        public string MarcaNombre { get; set; }
        public string RubroNombre { get; set; }
        public bool EsFraccionable { get; set; }

        private bool _conLimiteEnOferta;
        public bool ConLimiteEnOferta
        {
            get => _conLimiteEnOferta;
            set
            {
                if (_conLimiteEnOferta == value) return;

                _conLimiteEnOferta = value;

                OnPropertyChanged();
                OnPropertyChanged(nameof(DescripcionLimiteEnOferta));
            }
        }

        public string DescripcionLimiteEnOferta =>
            ConLimiteEnOferta
                ? "Con límite asignado"
                : "Sin límite";

        private decimal? _limiteEnOferta;
        public decimal? LimiteEnOferta
        {
            get => _limiteEnOferta;
            set
            {
                if (_limiteEnOferta == value) return;

                _limiteEnOferta = value;
                OnPropertyChanged();
            }
        }

        public List<long> CategoriaIds { get; set; }
    }
}