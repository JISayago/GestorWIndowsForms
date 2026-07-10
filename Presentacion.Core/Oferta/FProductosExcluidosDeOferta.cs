using Servicios.LogicaNegocio.Producto.DTO;
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
    public partial class FProductosExcluidosDeOferta : FBase.FBase
    {
        private readonly BindingList<ProductoOfertaDTO> _productos;

        private DataGridView dgvProductos;
        private Label lblTitulo;
        private Label lblCantidad;

        private Button btnRestaurar;
        private Button btnRestaurarTodos;
        private Button btnCerrar;

        public List<ProductoOfertaDTO> ProductosRestaurados { get; }
            = new List<ProductoOfertaDTO>();

        public FProductosExcluidosDeOferta(BindingList<ProductoOfertaDTO> productos)
        {
            _productos = productos;

            InicializarComponentes();

            ConfigurarGrilla();

            RefrescarGrilla();
        }

        private void InicializarComponentes()
        {
            Text = "Productos excluidos";
            StartPosition = FormStartPosition.CenterParent;

            Width = 900;
            Height = 520;

            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;

            lblTitulo = new Label
            {
                Text = "Productos excluidos de la oferta",
                Left = 20,
                Top = 15,
                Width = 400,
                Font = new Font("Segoe UI", 12, FontStyle.Bold)
            };

            lblCantidad = new Label
            {
                Left = 20,
                Top = 45,
                Width = 300,
                Font = new Font("Segoe UI", 9)
            };

            dgvProductos = new DataGridView
            {
                Left = 20,
                Top = 75,
                Width = 840,
                Height = 330,

                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                AllowUserToResizeRows = false,

                MultiSelect = false,
                ReadOnly = true,

                SelectionMode = DataGridViewSelectionMode.FullRowSelect,

                AutoGenerateColumns = false
            };

            btnRestaurar = new Button
            {
                Text = "Restaurar seleccionado",
                Width = 170,
                Height = 35,
                Left = 20,
                Top = 425
            };

            btnRestaurar.Click += BtnRestaurar_Click;

            btnRestaurarTodos = new Button
            {
                Text = "Restaurar todos",
                Width = 170,
                Height = 35,
                Left = 210,
                Top = 425
            };

            btnRestaurarTodos.Click += BtnRestaurarTodos_Click;

            btnCerrar = new Button
            {
                Text = "Cerrar",
                Width = 120,
                Height = 35,
                Left = 740,
                Top = 425
            };

            btnCerrar.Click += BtnCerrar_Click;

            dgvProductos.CellDoubleClick += DgvProductos_CellDoubleClick;

            Controls.Add(lblTitulo);
            Controls.Add(lblCantidad);

            Controls.Add(dgvProductos);

            Controls.Add(btnRestaurar);
            Controls.Add(btnRestaurarTodos);
            Controls.Add(btnCerrar);
        }

        private void ConfigurarGrilla()
        {
            dgvProductos.Columns.Clear();

            dgvProductos.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Codigo",
                HeaderText = "Código",
                Width = 90
            });

            dgvProductos.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Descripcion",
                HeaderText = "Descripción",
                Width = 250
            });

            dgvProductos.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "MarcaNombre",
                HeaderText = "Marca",
                Width = 120
            });

            dgvProductos.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "RubroNombre",
                HeaderText = "Rubro",
                Width = 120
            });

            dgvProductos.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "CantidadItemEnOferta",
                HeaderText = "Cantidad",
                Width = 80,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Alignment = DataGridViewContentAlignment.MiddleRight
                }
            });

            dgvProductos.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Stock",
                HeaderText = "Stock",
                Width = 80,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Alignment = DataGridViewContentAlignment.MiddleRight
                }
            });
        }

        private void RefrescarGrilla()
        {
            dgvProductos.DataSource = null;
            dgvProductos.DataSource = _productos;

            lblCantidad.Text = $"Productos excluidos: {_productos.Count}";

            btnRestaurar.Enabled = _productos.Any();
            btnRestaurarTodos.Enabled = _productos.Any();
        }

        private void BtnRestaurar_Click(object sender, EventArgs e)
        {
            if (dgvProductos.CurrentRow == null)
                return;

            var producto =
                (ProductoOfertaDTO)dgvProductos.CurrentRow.DataBoundItem;

            ProductosRestaurados.Add(producto);

            _productos.Remove(producto);

            RefrescarGrilla();

            CerrarSiNoHayMas();
        }

        private void BtnRestaurarTodos_Click(object sender, EventArgs e)
        {
            foreach (var producto in _productos.ToList())
            {
                ProductosRestaurados.Add(producto);
                _productos.Remove(producto);
            }

            RefrescarGrilla();

            CerrarSiNoHayMas();
        }

        private void BtnCerrar_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void DgvProductos_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            BtnRestaurar_Click(sender, EventArgs.Empty);
        }

        private void CerrarSiNoHayMas()
        {
            if (_productos.Any())
                return;

            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
