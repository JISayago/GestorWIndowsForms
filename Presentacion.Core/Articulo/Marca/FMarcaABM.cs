using Presentacion.FBase;
using Servicios.Core.Marca;
using Servicios.Core.Marca.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Presentacion.Core.Marca
{
    public partial class FMarcaABM : FBaseABM
    {
        private readonly IMarcaServicio _marcaServicio = new MarcaServicio();

        public FMarcaABM()
        {
            InitializeComponent();
        }

        public override void Ejectuar()
        {
            var marcaNueva = new MarcaDTO
            {
                Nombre = txtMarca.Text
            };

            _marcaServicio.Insertar(marcaNueva);
        }

    }
}
