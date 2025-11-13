using AccesoDatos.Entidades;
using Servicios.LogicaNegocio.Movimiento;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Presentacion.Core.Movimiento
{
    public partial class FMovimientoDetallado : Form
    {
        public long entidadID;
        public FMovimientoDetallado(long? entidadId)
        {
            entidadID = (long)entidadId;

            var movService = new MovimientoServicio();
            var movimiento = movService.ObtenerMovimientoPorId(entidadID);

            InitializeComponent();

            lblMontoMovimiento.Text = movimiento.Monto.ToString();
            lblNumeroMovimiento.Text = movimiento.NumeroMovimiento;
            lblTipoMovimiento.Text = movimiento.TipoMovimiento == 1 ? "Ingreso" : "Egreso";
            lblFechaMovimiento.Text = movimiento.FechaMovimiento.ToString();

            
        }


    }
}
