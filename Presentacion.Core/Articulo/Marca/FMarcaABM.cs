using Presentacion.FBase;
using Presentacion.FormulariosBase.Helpers;

using Servicios.Marca;
using Servicios.Marca.DTO;
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
        private readonly IMarcaServicio _marcaServicio;

        public override void FBaseABM_Load(object sender, EventArgs e)
        {
            base.FBaseABM_Load(sender, e);
            Inicializador(EntidadID);
        }

        private void Inicializador(long? entidadID)
        {

        }

        public FMarcaABM(TipoOperacion tipoOperacion, long? entidadId = null) :base(tipoOperacion,entidadId)
        {
            InitializeComponent();
            _marcaServicio = new MarcaServicio();
        }

        /*public override void Ejectuar()
        {
            var marcaNueva = new MarcaDTO
            {
                Nombre = txtMarca.Text
            };

            _marcaServicio.Insertar(marcaNueva);
        }*/
        
    }
}
