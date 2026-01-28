using Presentacion.Core.Articulo.Marca;
using Presentacion.Core.Categoria;
using Presentacion.Core.Cliente;
using Presentacion.Core.CuentaCorriente;
using Presentacion.Core.Empleado;
using Presentacion.Core.Empleado.Rol;
using Presentacion.Core.Herramientas;
using Presentacion.Core.Movimiento;
using Presentacion.Core.Oferta;
using Presentacion.Core.Producto;
using Presentacion.Core.Producto.Rubro;
using Presentacion.Core.TipoPago;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Presentacion.Core.Administracion
{
    public partial class FAdministracion : Form
    {
        private readonly long _logeadoId;
        public FAdministracion(long logeadoId)
        {
            InitializeComponent();
            _logeadoId = logeadoId;
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void sTOCKToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var fProducto = new FProductoConsulta();

            fProducto.Show();
        }

        private void mARCASToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var fmarca = new FMarcaConsulta();

            fmarca.Show();
        }

        private void cATEGORIASToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var fCategoria = new FCategoriaConsulta();

            fCategoria.Show();
        }

        private void rUBROSToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var fRubro = new FRubroConsulta();
            fRubro.Show();
        }

        private void lISTADOEMPLEADOSToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var fEmpleado = new FEmpleadoConsulta();
            fEmpleado.Show();
        }

        private void lISTADOCLIENTESToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var FCliente = new FClienteConsulta();
            FCliente.Show();

        }

        private void cUENTASCORRIENTESToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var FCuentaCorriente = new FCuentaCorrienteConsulta();
            FCuentaCorriente.Show();
        }

        private void lISTADOOFERTASToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //ver porque creo q falta una pantalla intermedia entre creacion y listado

            var FOferta = new FOfertaConsulta();
            FOferta.Show();

        }

        private void aCTIVARDESACTIVARToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var FActDesac = new FOfertaConsulta(true, "a");
            FActDesac.Show();
        }

        private void btnMovimientos_Click(object sender, EventArgs e)
        {
            var FMovimiento = new FMovimientoConsulta();
            FMovimiento.Show();
        }

        private void tIPOPAGOToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var fTipoPago = new FTipoPagoConsulta();
            fTipoPago.Show();
        }

        private void rOLESToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var FRoles = new FRolConsulta();
            FRoles.Show();
        }

        private void nUEVAOFERTAToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var FOferta = new FSeleccionTipoOferta();
            FOferta.Show();
        }

        private void btnGasto_Click(object sender, EventArgs e)
        {
            var FGasto = new Gasto.FGastoConsulta(_logeadoId);
            FGasto.Show();

        }


        private void btnComprobantes_Click(object sender, EventArgs e)
        {
            var escritorio = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            var carpeta = Path.Combine(escritorio, "ComprobantesPdf");

            if (!Directory.Exists(carpeta))
            {
                MessageBox.Show(
                    "La carpeta de comprobantes todavía no existe.",
                    "Comprobantes",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
                return;
            }

            using var dialog = new OpenFileDialog
            {
                InitialDirectory = carpeta,
                Filter = "Archivos PDF (*.pdf)|*.pdf",
                Title = "Seleccionar comprobante"
            };

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                using var visor = new FVisorPDF(dialog.FileName);
                visor.ShowDialog();
            }
        }
    }
}
