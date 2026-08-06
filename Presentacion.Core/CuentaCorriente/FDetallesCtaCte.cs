using Presentacion.Core.Producto;
using Presentacion.FormulariosBase.Helpers;
using ScottPlot.Colormaps;
using Servicios.Helpers.Cliente.CtaCte;
using Servicios.Helpers.Sistema.FiltrosConsulta;
using Servicios.LogicaNegocio.CuentaCorriente;
using Servicios.LogicaNegocio.CuentaCorriente.DTO;
using Servicios.LogicaNegocio.Movimiento.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TuProyecto.Presentacion;

namespace Presentacion.Core.CuentaCorriente
{
    public partial class FDetallesCtaCte : FBase.FBase
    {
        private long CtaCteID;
        private ICuentaCorrienteServicio _cuentaCorrienteServicio;
        private CuentaCorrienteDTO _cuentaCorrienteDto;
        private long? entidadID;
        private const int PageSize = 20;
        private BindingList<long> _dnisAutorizadosLista;
        private int paginaActual = 1;
        private int totalPaginas = 1;
        private int totalRegistros = 0;
        private decimal saldoInicial = 0;
        public FDetallesCtaCte(long? id = null)
        {
            if (id == null)
            {
                MessageBox.Show("No se ha seleccionado un cliente para ver su cuenta corriente.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.Close();
                return;

            }
            InitializeComponent();
            CtaCteID = id.Value;
            _cuentaCorrienteServicio = new CuentaCorrienteServicio();
            _cuentaCorrienteDto = _cuentaCorrienteServicio.ObtenerCuentaCorrientePorId(CtaCteID);
        }
        private void CargarMovimientos()
        {
            var filtros = new FiltroConsulta
            {
                Page = paginaActual,
                PageSize = PageSize,

                Bool1 = false,
                Bool2 = false,

                TextoBuscar = string.Empty,

                FechaDesde = null,
                FechaHasta = null,

                Filtro1 = null,
                Filtro2 = null,
                Filtro3 = null
            };

            var resultado = _cuentaCorrienteServicio.ObtenerMovimientosPorCuentaCorriente(CtaCteID, filtros);

            dgvGrilla.DataSource = resultado.Items;

            ResetearGrilla(dgvGrilla);

            totalRegistros = resultado.TotalRegistros;

            totalPaginas = (int)Math.Ceiling(
                (double)totalRegistros / resultado.PageSize);

            if (totalPaginas <= 0)
                totalPaginas = 1;

            ActualizarBotones();
        }
        public void CargarDatos()
        {
            var perimeteDeuda = "No se le permite tener saldo negativo.";
            var montodeudaMaximo = "$0";
            if (_cuentaCorrienteDto == null)
            {
                MessageBox.Show("No se pudo obtener la información de la cuenta corriente.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }
            lblCliente.Text = _cuentaCorrienteDto.NombreCliente;
            lblNombreCuenta.Text = _cuentaCorrienteDto.NombreCuentaCorriente;
            lblSaldoCuenta.Text = _cuentaCorrienteDto.Saldo.ToString("C");
            if (_cuentaCorrienteDto.LimiteDeudaActivo)
            {
                montodeudaMaximo = "El monto de deuda máximo es: " + _cuentaCorrienteDto.LimiteDeuda.ToString("C");
                perimeteDeuda = "Se le permite tener saldo negativo.";
            }
            lblDetallesExtra.Text = $"{perimeteDeuda}.\n {montodeudaMaximo}";
            lblEstado.Text = _cuentaCorrienteDto.EstadoDescripcionCtaCte;
            lstDnis.DataSource = _cuentaCorrienteDto.DniAutorizados;

        }



        private void DgvGrilla_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            RowEnter(e);
        }
        public virtual void RowEnter(DataGridViewCellEventArgs e)
        {
            try
            {
                entidadID = null;

                if (e.RowIndex < 0 || dgvGrilla.RowCount == 0)
                    return;

                if (!dgvGrilla.Columns.Contains("Id"))
                    return;

                var fila = dgvGrilla.Rows[e.RowIndex];

                if (fila?.Cells["Id"].Value == null)
                    return;

                entidadID = Convert.ToInt64(fila.Cells["Id"].Value);
            }
            catch
            {
                entidadID = null;
            }

        }

        private void DgvGrilla_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;
        }

        private void EjecutarClickDerechoFila(long? id, Point posicionMouse)
        {
            if (!id.HasValue)
                return;

            ContextMenuStrip menu = new ContextMenuStrip();

            menu.Items.Add("Ver Detalle", null, (s, e) =>
            {
                if (!id.HasValue)
                    return;

                var f = new FMovimientoDetallado(id.Value);

                f.ShowDialog();
            });


            menu.Show(dgvGrilla, posicionMouse);
        }



        public virtual void ResetearGrilla(DataGridView grilla)
        {
            for (int i = 0; i < grilla.ColumnCount; i++)
                grilla.Columns[i].Visible = false;

            grilla.ReadOnly = true;

            if (grilla.Columns.Count == 0)
                return;

            // IMPORTANTE: usar Fill real
            grilla.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // =========================================
            // ID (OCULTO)
            // =========================================
            if (grilla.Columns.Contains("MovimientoId"))
            {
                grilla.Columns["MovimientoId"].Visible = false;
                grilla.Columns["MovimientoId"].Name = "Id";
            }

            // =========================================
            // NUMERO (GRANDE)
            // =========================================
            if (grilla.Columns.Contains("NumeroMovimiento"))
            {
                var col = grilla.Columns["NumeroMovimiento"];

                col.Visible = true;
                col.HeaderText = "Número";

                col.FillWeight = 150;   // 🔥 grande
                col.MinimumWidth = 180;
            }

            // =========================================
            // FECHA (MEDIO)
            // =========================================
            if (grilla.Columns.Contains("FechaMovimiento"))
            {
                var col = grilla.Columns["FechaMovimiento"];

                col.Visible = true;
                col.HeaderText = "Fecha";

                col.DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";

                col.FillWeight = 150;
                col.MinimumWidth = 130;
            }

            // =========================================
            // MOVIMIENTO (CHICO)
            // =========================================
            if (grilla.Columns.Contains("TipoMovimientoDescripcion"))
            {
                var col = grilla.Columns["TipoMovimientoDescripcion"];

                col.Visible = true;
                col.HeaderText = "Movimiento";

                col.FillWeight = 100;
                col.MinimumWidth = 90;
            }

            if (grilla.Columns.Contains("TipoMovimiento"))
            {
                grilla.Columns["TipoMovimiento"].Visible = false;
            }

            // =========================================
            // TIPO DETALLE (MEDIO)
            // =========================================
            if (grilla.Columns.Contains("TipoMovimientoDetalleDescripcion"))
            {
                grilla.Columns["TipoMovimientoDetalleDescripcion"].Visible = false;
            }

            if (grilla.Columns.Contains("TipoMovimientoDetalle"))
            {
                grilla.Columns["TipoMovimientoDetalle"].Visible = false;
            }

            // =========================================
            // MONTO (GRANDE)
            // =========================================
            if (grilla.Columns.Contains("Monto"))
            {
                var col = grilla.Columns["Monto"];

                col.Visible = true;
                col.HeaderText = "Monto";

                col.DefaultCellStyle.Format = "C2";

                col.FillWeight = 150;   // 🔥 grande
                col.MinimumWidth = 150;
            }

            // =========================================
            // OCULTOS
            // =========================================
            if (grilla.Columns.Contains("EntidadId"))
                grilla.Columns["EntidadId"].Visible = false;

            if (grilla.Columns.Contains("TipoEntidad"))
                grilla.Columns["TipoEntidad"].Visible = false;

            if (grilla.Columns.Contains("EstaEliminado"))
                grilla.Columns["EstaEliminado"].Visible = false;
        }

        private void FDetallesCtaCte_Load(object sender, EventArgs e)
        {
            CargarDatos();

            CargarMovimientos();
        }

        private void ActualizarBotones()
        {
            lblPagina.Text = $"Página {paginaActual} de {totalPaginas}";
            lblTotalRegistros.Text = $"Total: {totalRegistros}";

            btnAnterior.Enabled = paginaActual > 1;
            btnSiguiente.Enabled = paginaActual < totalPaginas;
        }

        private void btnAnterior_Click(object sender, EventArgs e)
        {
            if (paginaActual <= 1)
                return;

            paginaActual--;

            CargarMovimientos();
        }

        private void btnSiguiente_Click(object sender, EventArgs e)
        {
            if (paginaActual >= totalPaginas)
                return;

            paginaActual++;

            CargarMovimientos();
        }

        private void dgvGrilla_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Right)
                return;

            var hit = dgvGrilla.HitTest(e.X, e.Y);

            if (hit.RowIndex >= 0)
            {
                dgvGrilla.ClearSelection();

                dgvGrilla.Rows[hit.RowIndex].Selected = true;

                RowEnter(new DataGridViewCellEventArgs(0, hit.RowIndex));

                EjecutarClickDerechoFila(entidadID, e.Location);
            }
        }

        private void btnCargarSaldo_Click(object sender, EventArgs e)
        {
            using (var f = new FCargaSaldoCtaCte(
                  saldoInicial,
                  HelperFormularioCargaSaldoCtaCte.Saldo))
            {
                if (f.ShowDialog() != DialogResult.OK)
                    return;

                saldoInicial = f.MontoIngresado;

                //lblSaldo.Text = saldoInicial.ToString("C");
            }
        }

        private void btnAgregarDni_Click(object sender, EventArgs e)
        {
            if (long.TryParse(txtNuevoDni.Text.Trim(), out long dni))
            {
                if (_dnisAutorizadosLista.Contains(dni))
                {
                    MessageBox.Show("Este DNI ya está en la lista.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                _dnisAutorizadosLista.Add(dni);
                txtNuevoDni.Clear();
                txtNuevoDni.Focus();
            }
            else
            {
                MessageBox.Show("Por favor, ingrese un número de DNI válido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // 🔹 EVENTO: Botón Eliminar DNI seleccionado
        private void btnEliminarDni_Click(object sender, EventArgs e)
        {
            if (lstDnis.SelectedItem != null)
            {
                var dniSeleccionado = (long)lstDnis.SelectedItem;
                _dnisAutorizadosLista.Remove(dniSeleccionado);
            }
            else
            {
                MessageBox.Show("Seleccione un DNI de la lista para eliminarlo.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
