using Presentacion.FBase;
using Presentacion.FormulariosBase.Helpers;
using Servicios.LogicaNegocio.Articulo.Marca;
using Servicios.LogicaNegocio.Producto;
using Servicios.LogicaNegocio.Producto.DTO;
using Servicios.LogicaNegocio.Producto.Rubro;
using Servicios.LogicaNegocio.Venta.Oferta;
using Servicios.LogicaNegocio.Venta.Oferta.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Presentacion.Core.Oferta
{
    public partial class FOfertaABM : FBaseABM
    {
        private readonly IOfertaServicio _ofertaServicio;
        
        protected long? EntidadID;


        public override void FBaseABM_Load(object sender, EventArgs e)
        {
            base.FBaseABM_Load(sender, e);
            Inicializador(EntidadID);
        }

        public FOfertaABM(TipoOperacion tipoOperacion, long? entidadId = null)
            : base(tipoOperacion, entidadId)
        {
            InitializeComponent();

            _ofertaServicio = new OfertaServicio();
            EntidadID = entidadId;

            if (tipoOperacion == TipoOperacion.Eliminar || tipoOperacion == TipoOperacion.Modificar)
            {
                CargarDatos(entidadId);
            }

            if (tipoOperacion == TipoOperacion.Eliminar)
            {
                DesactivarControles(this);
            }
        }

        public override void Inicializador(long? entidadId)
        {
        }

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

            var oferta = _ofertaServicio.ObtenerPorId(entidadId.Value);

            if (oferta != null)
            {
                /*
                txtProducto.Text = Producto.Descripcion;
                txtEstado.Text = Producto.Estado.ToString();
                txtMedida.Text = Producto.Medida;
                txtUnidadMedida.Text = Producto.UnidadMedida;
                txtStock.Text = Producto.Stock.ToString();
                txtPrecioCosto.Text = Producto.PrecioCosto.ToString();
                txtPrecioVenta.Text = Producto.PrecioVenta.ToString();
                txtCodigo.Text = Producto.Codigo;
                txtCodigoBarra.Text = Producto.CodigoBarra;
                chkIvaIncluido.Checked = Producto.IvaIncluidoPrecioFinal;
                chkEsFraccionable.Checked = Producto.EsFraccionable;
                cmbMarca.SelectedValue = Producto.IdMarca;
                cmbRubro.SelectedValue = Producto.IdRubro;
                */
            }
            else
            {
                MessageBox.Show(@"Ocurrio un Error Grave", @"Error Grave", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }

        }

        public override bool EjecutarComandoNuevo()
        {
            if (!VerificarDatosObligatorios())
            {
                MessageBox.Show(@"Por favor ingrese los campos Obligatorios.", @"Atención", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return false;
            }
            var ofertaNueva = new OfertaDTO
            {
                /*
                Descripcion = txtProducto.Text,
                Stock = decimal.Parse(txtStock.Text),
                PrecioCosto = decimal.Parse(txtPrecioCosto.Text),
                PrecioVenta = decimal.Parse(txtPrecioVenta.Text),
                Estado = int.Parse(txtEstado.Text),
                Medida = txtMedida.Text,
                UnidadMedida = txtUnidadMedida.Text,
                Codigo = txtCodigo.Text,
                CodigoBarra = txtCodigoBarra.Text,
                IvaIncluidoPrecioFinal = chkIvaIncluido.Checked,
                EsFraccionable = chkEsFraccionable.Checked,
                IdMarca = (long)cmbMarca.SelectedValue,
                IdRubro = (long)cmbRubro.SelectedValue,
                CategoriaIds = _categoriasSeleccionadas.ToList(),
                EstaEliminado = false
                */
            };

            //ARREGLAR LOS PARSE y TIPOS DE DATOS

            var response = _ofertaServicio.Insertar(ofertaNueva);

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
                MessageBox.Show(@"´Por favor seleccione una Producto válida.", @"Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;

            }
            if (TipoOperacion == TipoOperacion.Eliminar)
            {
                var response = _ofertaServicio.Eliminar((long)EntidadID);
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
                    MessageBox.Show(@"´Por favor seleccione un Producto válida.", @"Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return false;

                }
                //ARREGLAR LOS PARSE y TIPOS DE DATOS
                var ofertaModificar = new OfertaDTO //completar con los datos del producto a modificar
                {
                    /*
                    Descripcion = txtProducto.Text,
                    Stock = decimal.Parse(txtStock.Text),
                    PrecioCosto = decimal.Parse(txtPrecioCosto.Text),
                    PrecioVenta = decimal.Parse(txtPrecioVenta.Text),
                    Estado = int.Parse(txtEstado.Text),
                    Medida = txtMedida.Text,
                    UnidadMedida = txtUnidadMedida.Text,
                    Codigo = txtCodigo.Text,
                    CodigoBarra = txtCodigoBarra.Text,
                    IvaIncluidoPrecioFinal = chkIvaIncluido.Checked,
                    EsFraccionable = chkEsFraccionable.Checked,
                    IdMarca = (long)cmbMarca.SelectedValue,
                    IdRubro = (long)cmbRubro.SelectedValue,
                    CategoriaIds = _categoriasSeleccionadas.ToList(),
                    EstaEliminado = false
                    */
                };

                var response = _ofertaServicio.Modificar((long)EntidadID,ofertaModificar);

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

    }
}
