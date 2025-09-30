using Presentacion.Core.Cliente;
using Presentacion.FBase;
using Presentacion.FormulariosBase.Helpers;
using Servicios.LogicaNegocio.Cliente;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Presentacion.Core.Cliente
{
    public partial class FClienteConsulta : FBaseConsulta
    {

        private readonly IClienteServicio _clienteServicio;

        public FClienteConsulta() : this(new ClienteServicio())
        {
            InitializeComponent();
        }
        public FClienteConsulta(IClienteServicio clienteServicio)
        {
            _clienteServicio = clienteServicio;
        }

        public override void EjecutarBtnNuevo()
        {
            var FormularioClienteABM = new FClienteABM(TipoOperacion.Nuevo);
            FormularioClienteABM.ShowDialog();
        }

        public override void ResetearGrilla(DataGridView grilla)
        {
            base.ResetearGrilla(grilla);
            grilla.Columns["PersonaId"].Visible = false;
            grilla.Columns["PersonaId"].Name = "Id";

            grilla.Columns["Nombre"].Visible = true;
            grilla.Columns["Nombre"].Width = 100;

            grilla.Columns["Apellido"].Visible = true;
            grilla.Columns["Apellido"].Width = 100;

            grilla.Columns["DNI"].Visible = true;
            grilla.Columns["DNI"].Width = 100;

            grilla.Columns["Email"].Visible = true;
            grilla.Columns["Email"].Width = 130;

            grilla.Columns["Telefono"].Visible = true;
            grilla.Columns["Telefono"].Width = 100;

            grilla.Columns["EstadoDescripcion"].Visible = true;
            grilla.Columns["EstadoDescripcion"].Width = 100;
            grilla.Columns["EstadoDescripcion"].HeaderText = "Estado";
        }

        public override void ActualizarDatos(DataGridView grilla, string cadenaBuscar, CheckBox check, ToolStrip toolStrip)
        {
            base.ActualizarDatos(grilla, cadenaBuscar, check, toolStrip);

            if (check.Checked)
            {
                grilla.DataSource = _clienteServicio.ObtenerClientesEliminados(cadenaBuscar);
                toolStrip.Enabled = false;

            }
            else
            {
                grilla.DataSource = _clienteServicio.ObtenerClientes(cadenaBuscar);
                toolStrip.Enabled = true;
            }
        }

        public override void EjecutarBtnModificar()
        {
            base.EjecutarBtnModificar();
            if (puedeEjecutarComando)
            {
                var FormularioABMCliente = new FClienteABM(TipoOperacion.Modificar, entidadID);
                FormularioABMCliente.ShowDialog();
                ActualizarSegunOperacion(FormularioABMCliente.RealizoAlgunaOperacion);
            }
        }
        public override void EjecutarBtnEliminar()
        {
            base.EjecutarBtnEliminar();
            if (puedeEjecutarComando)
            {
                var FormularioABMCliente = new FClienteABM(TipoOperacion.Eliminar, entidadID);
                FormularioABMCliente.ShowDialog();
                ActualizarSegunOperacion(FormularioABMCliente.RealizoAlgunaOperacion);
            }
        }
        private void ActualizarSegunOperacion(bool realizoOperacion)
        {
            if (realizoOperacion)
            {
                ActualizarDatos(dgvGrilla, string.Empty, cbxEstaEliminado, BarraLateralBotones);
            }
        }
        public override void EjecutarMostrarEliminados()
        {
            base.EjecutarMostrarEliminados();
        }
    }
}
