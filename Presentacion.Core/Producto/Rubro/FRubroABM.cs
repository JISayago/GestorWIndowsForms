using Presentacion.FBase;
using Presentacion.FormulariosBase.Helpers;
using Servicios.LogicaNegocio.Producto.Rubro;
using Servicios.LogicaNegocio.Producto.Rubro.DTO;

namespace Presentacion.Core.Producto.Rubro
{
    public partial class FRubroABM : FBaseABM
    {
        private readonly IRubroServicio _rubroServicio;


        public override void FBaseABM_Load(object sender, EventArgs e)
        {
            base.FBaseABM_Load(sender, e);
            Inicializador(EntidadID);
        }

        public FRubroABM(TipoOperacion tipoOperacion, long? entidadId = null)
            : base(tipoOperacion, entidadId)
        {
            InitializeComponent();

            _rubroServicio = new RubroServicio();

            if (tipoOperacion == TipoOperacion.Eliminar || tipoOperacion == TipoOperacion.Modificar)
            {
                CargarDatos(entidadId);
            }

            if (tipoOperacion == TipoOperacion.Eliminar)
            {
                DesactivarControles(this);
            }

            AgregarControlesObligatorios(txtRubro, "Rubro");
        }

        public override void Inicializador(long? entidadId)
        {
            /*if (entidadId.HasValue) return;

            txtRubro.KeyPress += Validacion.NoSimbolos;
            txtRubro.KeyPress += Validacion.NoNumeros;
            */
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
                Informar("No se puedo obtener el Rubro seleccionado. Por favor intente nuevamente.", "Atención");
                //MessageBox.Show(@"Ocurrio un Error Grave", @"Error Grave", MessageBoxButtons.OK, MessageBoxIcon.Stop);

                this.Close();
            }

            if (TipoOperacion == TipoOperacion.Eliminar)
            {
                btnLimpiar.Enabled = false;
            }

            var rubro = _rubroServicio.ObtenerPorId(entidadId.Value);

            if (rubro != null)
            {
                txtRubro.Text = rubro.Nombre;
            }
            else
            {
                Informar("No se puedo obtener el Rubro seleccionado. Por favor intente nuevamente.", "Atención");
                //MessageBox.Show(@"Ocurrio un Error Grave", @"Error Grave", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }

        }

        public override bool EjecutarComandoNuevo()
        {
            if (!VerificarDatosObligatorios())
            {
                Informar("Por favor ingrese los campos Obligatorios.", "Atención");
                //MessageBox.Show(@"Por favor ingrese los campos Obligatorios.", @"Atención", MessageBoxButtons.OK,
                //    MessageBoxIcon.Error);
                return false;
            }
            var rubroNueva = new RubroDTO
            {
                Nombre = txtRubro.Text,
                EstaEliminado = false

            };
            var response = _rubroServicio.Insertar(rubroNueva);

            if (response.Exitoso)
            {
                Exito(response.Mensaje, "Éxito");
                //MessageBox.Show($"{response.Mensaje}", @"Atención", MessageBoxButtons.OK,
                //   MessageBoxIcon.Information);
                return true;
            }
            else
            {
                MostrarError(response.Mensaje);
                //MessageBox.Show($"{response.Mensaje}", @"Atención", MessageBoxButtons.OK,
                //   MessageBoxIcon.Error);
                return false;
            }

        }

        public override bool EjecutarComandoEliminar()
        {
            if (!EntidadID.HasValue)
            {
                Informar("Por favor seleccione un rubro válido.", "Error");
                //MessageBox.Show(@"´Por favor seleccione una rubro válida.", @"Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;

            }
            if (TipoOperacion == TipoOperacion.Eliminar)
            {
                var response = _rubroServicio.Eliminar((long)EntidadID);
                if (response.Exitoso)
                {
                    Exito(response.Mensaje, "Éxito");
                    //MessageBox.Show($"{response.Mensaje}", @"Atención", MessageBoxButtons.OK,
                    //   MessageBoxIcon.Information);
                    return true;
                }
                else
                {
                    MostrarError(response.Mensaje);
                    //MessageBox.Show($"{response.Mensaje}", @"Atención", MessageBoxButtons.OK,
                    //   MessageBoxIcon.Error);
                    return false;
                }

            }
            return false;

        }

        public override bool EjecutarComandoModificar()
        {
            if (!VerificarDatosObligatorios())
            {
                Informar("Por favor ingrese los campos Obligatorios.", "Atención");
                //MessageBox.Show(@"Por favor ingrese los campos Obligatorios.", @"Atención", MessageBoxButtons.OK,
                //    MessageBoxIcon.Error);
                return false;
            }
            if (TipoOperacion == TipoOperacion.Modificar)
            {
                if (!EntidadID.HasValue)
                {
                    Informar("Por favor seleccione un rubro válido.", "Error");
                    //MessageBox.Show(@"´Por favor seleccione un rubro válida.", @"Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return false;

                }

                var RubroModificar = new RubroDTO
                {
                    Id = EntidadID.Value,
                    Nombre = txtRubro.Text,
                };
                var response = _rubroServicio.Modificar(RubroModificar);

                if (response.Exitoso)
                {
                    Exito(response.Mensaje, "Éxito");
                    //MessageBox.Show($"{response.Mensaje}", @"Atención", MessageBoxButtons.OK,
                    //    MessageBoxIcon.Information);
                    return true;
                }
                else
                {
                    MostrarError(response.Mensaje);
                    //MessageBox.Show($"{response.Mensaje}", @"Atención", MessageBoxButtons.OK,
                    //    MessageBoxIcon.Error);
                    return false;
                }

            }
            return true;

        }
    }
}
