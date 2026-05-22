using AccesoDatos.Entidades;
using Presentacion.FBase;
using Servicios.Helpers.Gasto;
using Servicios.LogicaNegocio.Gasto;
using Servicios.LogicaNegocio.Gasto.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Presentacion.Core.Gasto
{
    public partial class FGastoABM : FBaseABM
    {
        private readonly IGastoServicio _gastoServicio;
        private readonly long _logeadoId;
        public bool RealizoAlgunaOperacion = false;
        public FGastoABM(long logeadoId)
        {
            InitializeComponent();
            _gastoServicio = new GastoServicio();
            _logeadoId = logeadoId;

            AgregarControlesObligatorios(txtDetalle, "Detalle");
            AgregarControlesObligatorios(txtMontoPago, "Monto Pago");
        }


        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private void RegistrarGasto()
        {
            var gasto = new GastoDTO
            {
                IdEmpleado = _logeadoId,
                CategoriaGasto = (int)cmbCategoriaGasto.SelectedValue,
                Detalle = txtDetalle.Text,
                MontoTotal = decimal.Parse(txtMontoPago.Text),
                FechaGasto = dtpDiaGasto.Value,
                EstadoGasto = (int)cmbEstado.SelectedValue

            };
            gasto.EstadoGasto = (int)cmbCategoriaGasto.SelectedValue;
            var resultado = _gastoServicio.NuevoGasto(gasto);
            if (resultado.Exitoso)
            {
                RealizoAlgunaOperacion = true;
                MessageBox.Show($"{resultado.Mensaje}", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show($"{resultado.Mensaje}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }


        private void FGastoABM_Load(object sender, EventArgs e)
        {
            var listaCategoria = Enum.GetValues(typeof(CategoriaGasto))
                .Cast<CategoriaGasto>()
                .Select(x => new
                {
                    Texto = x.ToString().Replace("_", " "),
                    Valor = (int)x
                })
                .ToList();

            cmbCategoriaGasto.DataSource = listaCategoria;
            cmbCategoriaGasto.DisplayMember = "Texto";
            cmbCategoriaGasto.ValueMember = "Valor";


            var listaEstado = Enum.GetValues(typeof(EstadoGasto))
                .Cast<EstadoGasto>()
                .Where(x => x == EstadoGasto.Pagado || x == EstadoGasto.Pendiente)
                .Select(x => new
                {
                    Texto = x.ToString(),
                    Valor = (int)x
                })
                .ToList();

            cmbEstado.DataSource = listaEstado;
            cmbEstado.DisplayMember = "Texto";
            cmbEstado.ValueMember = "Valor";
        }

        private void btnRegistrarGasto_Click(object sender, EventArgs e)
        {
            RegistrarGasto();
        }
    }
}
