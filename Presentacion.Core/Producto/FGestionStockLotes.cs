using AccesoDatos.Entidades;
using Presentacion.FBase;
using Presentacion.FormulariosBase.Helpers;
using Servicios.LogicaNegocio.Articulo.Marca.DTO;
using Servicios.LogicaNegocio.Producto;
using Servicios.LogicaNegocio.Producto.DTO;
using Servicios.LogicaNegocio.Producto.Lote;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Presentacion.Core.Producto
{
    public partial class FGestionStockLotes : FBaseABM
    {
        private readonly ILoteServicio _loteSevicio;
        private readonly IProductoServicio _productoServicio;

        //borrar si no hacen falta
        TipoOperacion TipoOperacion;
        private string NombreProducto;
        private string NumeroLote;
        private decimal stockIncial = 0;
        private decimal stockActual = 0;
        public bool RealizoOperacion { get; private set; } = false;

        public FGestionStockLotes(TipoOperacion tipoOperacion, string? nombreProducto = null, long? entidadId = null)
            : base(tipoOperacion, entidadId)
        {
            InitializeComponent();

            _loteSevicio = new LoteServicio();
            _productoServicio = new ProductoServicio();

            TipoOperacion = tipoOperacion;
            NombreProducto = nombreProducto;

            lblNombreProducto.Text = nombreProducto;

            dtpFechaVencimiento.Format = DateTimePickerFormat.Short;
            dtpFechaVencimiento.MinDate = DateTime.Now;

            if (chkFechaVencimiento.Checked)
            {
                dtpFechaVencimiento.Enabled = true;
                dtpFechaVencimiento.Value = DateTime.Now; // o el valor que quieras
            }
            else
            {
                dtpFechaVencimiento.Enabled = false;

                // Simular NULL
                dtpFechaVencimiento.Format = DateTimePickerFormat.Custom;
                dtpFechaVencimiento.CustomFormat = " ";
            }

            if(TipoOperacion == TipoOperacion.Nuevo)
            {
                NumeroLote = _loteSevicio.GenerarNumeroLote();
            }
        }

        #region 🔵 EVENTOS
        private void chkFechaVencimiento_CheckedChanged_1(object sender, EventArgs e)
        {
            if (chkFechaVencimiento.Checked)
            {
                dtpFechaVencimiento.Enabled = true;
                dtpFechaVencimiento.Format = DateTimePickerFormat.Short;
                dtpFechaVencimiento.Value = DateTime.Now; // o el valor que quieras
            }
            else
            {
                dtpFechaVencimiento.Enabled = false;

                // Simular NULL
                dtpFechaVencimiento.Format = DateTimePickerFormat.Custom;
                dtpFechaVencimiento.CustomFormat = " ";
            }
        }

        #endregion
        #region 🔵 OVERIDES FBASEABM
        public override void DesactivarControles(object obj)
        {
            base.DesactivarControles(obj);

            btnLimpiar.Enabled = false;
            btnLimpiar.Visible = false;
        }

        public override void CargarDatos(long? entidadId)
        {
            if (!entidadId.HasValue)
            {
                MessageBox.Show(@"Ocurrio un Error Grave", @"Error Grave", MessageBoxButtons.OK, MessageBoxIcon.Stop);

                this.Close();
            }

            if (TipoOperacion == TipoOperacion.Eliminar)
            {
                btnLimpiar.Enabled = false;
            }

            var lote = _loteSevicio.ObtenerLotePorId(entidadId.Value);

            //CARGAR ESTADOS CHECKBOX CON EL LOTE BUSCADO

            if (lote != null)
            {
                txtNumeroLote.Text = lote.NumeroLote;
                txtDescripcionLote.Text = lote.Descripcion;
                nudStockInicial.Value = lote.StockInicial;
                nudStockActual.Value = lote.StockActual;
                chkLoteEstaActivo.Checked = lote.EstaActivo;

                if (lote.FechaVencimiento.HasValue)
                {
                    chkFechaVencimiento.Checked = true;
                    chkFechaVencimiento.Enabled = false;

                    dtpFechaVencimiento.Value = lote.FechaVencimiento.Value;
                }
                else
                {
                    chkFechaVencimiento.Checked = false;
                    dtpFechaVencimiento.Enabled = false;
                    // Simular NULL
                    dtpFechaVencimiento.Format = DateTimePickerFormat.Custom;
                    dtpFechaVencimiento.CustomFormat = " ";
                }
            }
            else
            {
                MessageBox.Show(@"Ocurrio un Error Grave", @"Error Grave", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }

        }

        public override bool EjecutarComandoNuevo()
        {
            if (!VerificarDatosObligatorios()) //no la estoy usando
            {
                MessageBox.Show(@"Por favor ingrese los campos Obligatorios.", @"Atención", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return false;
            }

            var loteNuevo = new LoteDTO
            {
                IdProducto = EntidadID.Value,
                StockInicial = nudStockInicial.Value,
                StockActual = nudStockActual.Value,
                NumeroLote = txtNumeroLote.Text,
                Descripcion = txtDescripcionLote.Text,
                FechaAlta = DateTime.Now, //cambiar por datetime , asi se puede filtrar el mas viejo con la hora incluida
                FechaVencimiento = chkFechaVencimiento.Checked ? dtpFechaVencimiento.Value : null,
                EstaVencido = false,
                EstaActivo = true
            };

            var response = _loteSevicio.CrearLote(loteNuevo);

            if (response.Exitoso)
            {
                MessageBox.Show($"{response.Mensaje}", @"Atención", MessageBoxButtons.OK,
                   MessageBoxIcon.Information);
                return true;
            }
            else
            {
                MessageBox.Show($"{response.Mensaje}", @"Atención", MessageBoxButtons.OK,
                   MessageBoxIcon.Error);
                return false;
            }
        }

        public override bool EjecutarComandoEliminar()
        {
            if (!EntidadID.HasValue)
            {
                MessageBox.Show(@"´Por favor seleccione una marca válida.", @"Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;

            }
            if (TipoOperacion == TipoOperacion.Eliminar)
            {
                var response = _loteSevicio.EliminarLote((long)EntidadID);
                if (response.Exitoso)
                {
                    MessageBox.Show($"{response.Mensaje}", @"Atención", MessageBoxButtons.OK,
                       MessageBoxIcon.Information);
                    return true;
                }
                else
                {
                    MessageBox.Show($"{response.Mensaje}", @"Atención", MessageBoxButtons.OK,
                       MessageBoxIcon.Error);
                    return false;
                }

            }
            return false;

        }

        public override bool EjecutarComandoModificar()
        {
            if (!VerificarDatosObligatorios())
            {
                MessageBox.Show(@"Por favor ingrese los campos Obligatorios.", @"Atención", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return false;
            }
            if (TipoOperacion == TipoOperacion.Modificar)
            {
                if (!EntidadID.HasValue)
                {
                    MessageBox.Show(@"´Por favor seleccione un marca válida.", @"Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return false;

                }

                var LoteModificar = new LoteDTO
                {
                    NumeroLote = txtNumeroLote.Text,
                    IdProducto = EntidadID.Value,
                    StockInicial = nudStockInicial.Value,
                    StockActual = nudStockActual.Value,
                    Descripcion = txtDescripcionLote.Text,
                    FechaVencimiento = chkFechaVencimiento.Checked ? dtpFechaVencimiento.Value : null,
                    EstaVencido = chkFechaVencimiento.Checked && dtpFechaVencimiento.Value < DateTime.Now, //revisar
                    EstaActivo = chkLoteEstaActivo.Checked
                };
                var response = _loteSevicio.ModficiarLote(LoteModificar, EntidadID.Value);

                if (response.Exitoso)
                {
                    MessageBox.Show($"{response.Mensaje}", @"Atención", MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                    return true;
                }
                else
                {
                    MessageBox.Show($"{response.Mensaje}", @"Atención", MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    return false;
                }

            }
            return true;

        }
        
        #endregion

        private void FGestionStockLotes_Load(object sender, EventArgs e)
        {
            if (TipoOperacion == TipoOperacion.Nuevo ) //Nombre default auto generado cuando cargar un lote nuevo
            {
                chkLoteEstaActivo.Checked = true;
                //txtNombreLote.Text = $"{NombreProducto}-{DateTime.Now: yyyyMMddHHmmss}".ToUpper();
                txtNumeroLote.Text = NumeroLote;
            }
        }
    }
}
