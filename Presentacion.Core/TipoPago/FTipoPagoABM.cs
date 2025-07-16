using Presentacion.FBase;
using Presentacion.FormulariosBase.Helpers;
using Servicios.LogicaNegocio.Empleado.Rol;
using Servicios.LogicaNegocio.Empleado.Rol.DTO;
using Servicios.LogicaNegocio.TipoPago;
using Servicios.LogicaNegocio.TipoPago.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Presentacion.Core.TipoPago
{
    public partial class FTipoPagoABM : FBaseABM
    {
        private readonly ITipoPagoServicio _tipoPagoServicio;

        public override void FBaseABM_Load(object sender, EventArgs e)
        {
            base.FBaseABM_Load(sender, e);
            Inicializador(EntidadID);
        }
        public FTipoPagoABM(TipoOperacion tipoOperacion, long? entidadID = null) : base(tipoOperacion, entidadID)
        {
            InitializeComponent();
            _tipoPagoServicio = new TipoPagoServicio();

            if (tipoOperacion == TipoOperacion.Eliminar || tipoOperacion == TipoOperacion.Modificar)
            {
                CargarDatos(entidadID);
            }

            if (tipoOperacion == TipoOperacion.Eliminar)
            {
                DesactivarControles(this);
            }

            AgregarControlesObligatorios(txtNombre, "Nombre del Tipo de pago");
            AgregarControlesObligatorios(txtCodigo, "Código del Tipo de pago");
            AgregarControlesObligatorios(txtDescripcion, "Descripcion del Tipo de pago");
        }
        public override bool EjecutarComandoNuevo()
        {

            if (!VerificarDatosObligatorios())
            {
                MessageBox.Show(@"Por favor ingrese los campos Obligatorios.", @"Atención", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return false;
            }

            var nuevoTipoPago = new TipoPagoDTO
            {
                Nombre = txtNombre.Text,
                Codigo = txtCodigo.Text,
                Detalle = txtDescripcion.Text,
            };


            var response = _tipoPagoServicio.Insertar(nuevoTipoPago);

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
        public override void Inicializador(long? entidadId)
        {

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

            var tipoPago = _tipoPagoServicio.ObtenerTipoPagoPorId(entidadId.Value);

            // Datos Personales
            txtNombre.Text = tipoPago.Nombre;
            txtCodigo.Text =tipoPago.Codigo;
            txtDescripcion.Text = tipoPago.Detalle;

        }


        public override bool EjecutarComandoEliminar()
        {
            if (!EntidadID.HasValue)
            {
                MessageBox.Show(@"´Por favor seleccione un Rol válido.", @"Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;

            }
            if (TipoOperacion == TipoOperacion.Eliminar)
            {
                var response = _tipoPagoServicio.Eliminar((long)EntidadID);
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
            if (!EntidadID.HasValue)
            {
                MessageBox.Show(@"´Por favor seleccione un Rol válido.", @"Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;

            }
            if (TipoOperacion == TipoOperacion.Modificar)
            {

                var tipoPagoEditar = new TipoPagoDTO
                {
                    Nombre = txtNombre.Text,
                    Codigo = txtCodigo.Text,
                    Detalle = txtDescripcion.Text,
                };

                var response = _tipoPagoServicio.Modificar(tipoPagoEditar, EntidadID);

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

    }
}
