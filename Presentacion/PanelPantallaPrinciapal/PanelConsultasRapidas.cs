using AccesoDatos.Entidades;
using Presentacion.Core.Movimiento;
using Presentacion.Core.Producto;
using Presentacion.FBase.Helpers;
using Servicios.Helpers.Sistema.FiltrosConsulta;
using Servicios.LogicaNegocio.Producto;
using Servicios.LogicaNegocio.Producto.DTO;
using Servicios.LogicaNegocio.Venta;
using Servicios.LogicaNegocio.Venta.DTO;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Presentacion.Notificaciones
{
    public class PanelConsultasRapidas : UserControl
    {
        private readonly IProductoServicio _productoService;
        private readonly IVentaServicio _ventaService;

        private DataGridView dgvProds;
        private DataGridView dgvVentas;

        private Button btnPrevProd, btnNextProd, btnVerMas;
        private Label lblPaginaInfo;
        private TextBox txtBuscador;

        // Estado de la paginación Productos
        private int _paginaActual = 1;
        private int _totalPaginas = 1;
        private const int _pageSize = 10;

        // Estado de la paginación Ventas
        private int _paginaActualV = 1;
        private int _totalPaginasV = 1;
        private const int _pageSizeV = 12;

        private Button btnPrevVenta, btnNextVenta;
        private Label lblPaginaInfoVenta;

        public PanelConsultasRapidas()
        {
            _productoService = new ProductoServicio();
            _ventaService = new VentaServicio();
        }

        // ===========================================================================
        // MÉTODO PRINCIPAL: Estructura de Tab 1 en 2 filas (Limpio)
        // ===========================================================================
        public void CargarConsultasRapidas(Control contenedorPadre)
        {
            contenedorPadre.BackColor = SystemColors.Control;

            TableLayoutPanel mainLayout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 2,
                Padding = new Padding(10)
            };

            mainLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 50f));
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 50f));

            mainLayout.Controls.Add(CrearPanelVentas(), 0, 0);
            mainLayout.Controls.Add(CrearPanelProductos(), 0, 1);

            contenedorPadre.Controls.Add(mainLayout);

            ConfigurarEstiloGrid(dgvProds);
            ConfigurarEstiloGrid(dgvVentas);

            ConfigurarColoresGrid(dgvProds);
            ConfigurarColoresGrid(dgvVentas);

            RefrescarProductos();
            RefrescarVentas();
        }

        // ===========================================================================
        // CONFIGURACIÓN DE COLORES ESTILIZADOS - GRILLAS
        // ===========================================================================
        private void ConfigurarColoresGrid(DataGridView dgv)
        {
            dgv.BackgroundColor = TemaSistema.FondoControl;
            dgv.BorderStyle = BorderStyle.None;

            dgv.ColumnHeadersDefaultCellStyle.BackColor = TemaSistema.Oscuro;
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = TemaSistema.Acento;
            dgv.ColumnHeadersDefaultCellStyle.SelectionBackColor = TemaSistema.Oscuro;

            dgv.DefaultCellStyle.BackColor = TemaSistema.FondoControl;
            dgv.DefaultCellStyle.ForeColor = TemaSistema.Texto;

            dgv.DefaultCellStyle.SelectionBackColor = TemaSistema.Seleccion;
            dgv.DefaultCellStyle.SelectionForeColor = TemaSistema.Oscuro;

            dgv.AlternatingRowsDefaultCellStyle.BackColor = TemaSistema.Alternado;

            dgv.GridColor = TemaSistema.Borde;
            dgv.RowHeadersVisible = false;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        // ===========================================================================
        // 🎨 NUEVA SECCIÓN: CONFIGURACIÓN DE COLORES ESTILIZADOS - BOTONES
        // ===========================================================================
        private void ConfigurarColoresBotones(Button btn, bool esPrimario = false)
        {
            btn.BackColor = TemaSistema.Seleccion;
            btn.ForeColor = Color.Black;

            btn.FlatStyle = FlatStyle.Flat;

            btn.FlatAppearance.BorderSize = 1;
            btn.FlatAppearance.BorderColor = Color.Black;
        }

        // ===========================================================================
        // CONFIGURACIÓN PARA BLOQUEAR RESIZE Y PERMITIR COPIADO
        // ===========================================================================
        private void ConfigurarEstiloGrid(DataGridView dgv)
        {
            dgv.AllowUserToResizeRows = false;
            dgv.AllowUserToResizeColumns = false;
            dgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;

            dgv.ReadOnly = true;
            dgv.SelectionMode = DataGridViewSelectionMode.CellSelect;
            dgv.MultiSelect = false;
            dgv.ClipboardCopyMode = DataGridViewClipboardCopyMode.Disable;

            dgv.CellClick += (sender, e) => {
                if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
                {
                    DataGridView grid = (DataGridView)sender;
                    string nombreColumna = grid.Columns[e.ColumnIndex].Name;

                    bool esColumnaCopiable =
                        nombreColumna.Equals("Id", StringComparison.OrdinalIgnoreCase) ||
                        nombreColumna.Equals("Codigo", StringComparison.OrdinalIgnoreCase) ||
                        nombreColumna.Contains("Comprobante");

                    if (esColumnaCopiable)
                    {
                        var valor = grid.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
                        if (valor != null)
                        {
                            string textoACopiar = valor.ToString();

                            if (nombreColumna.Equals("Id", StringComparison.OrdinalIgnoreCase) || nombreColumna.Contains("Comprobante"))
                            {
                                string[] partes = textoACopiar.Split('-');
                                if (partes.Length >= 2)
                                {
                                    textoACopiar = partes[1];
                                }
                            }

                            if (!string.IsNullOrWhiteSpace(textoACopiar))
                            {
                                try
                                {
                                    Clipboard.SetText(textoACopiar);
                                }
                                catch (System.Runtime.InteropServices.ExternalException)
                                {
                                    Clipboard.SetDataObject(textoACopiar, true, 5, 100);
                                }
                            }
                        }
                    }
                }
            };
        }

        // ===========================================================================
        // SECCIÓN SUPERIOR: VENTAS
        // ===========================================================================
        private Panel CrearPanelVentas()
        {
            Panel p = new Panel { Dock = DockStyle.Fill, Padding = new Padding(5) };
            Label lbl = new Label { Text = "Últimas Ventas", Dock = DockStyle.Top, Height = 25, Font = new Font("Segoe UI", 10, FontStyle.Bold) };

            dgvVentas = ConfigurarGridSimple();
            dgvVentas.Columns.Add("Id", "Comprobante");
            dgvVentas.Columns.Add("Fecha", "Fecha/Hora");
            dgvVentas.Columns.Add("Detalle", "Detalle");
            dgvVentas.Columns.Add("Total", "Total $");

            var footer = CrearFooterPaginado(out btnPrevVenta, out btnNextVenta, out lblPaginaInfoVenta, () => { });

            btnPrevVenta.Click += (s, e) => { if (_paginaActualV > 1) { _paginaActualV--; RefrescarVentas(); } };
            btnNextVenta.Click += (s, e) => { if (_paginaActualV < _totalPaginasV) { _paginaActualV++; RefrescarVentas(); } };
            btnVerMas.Click += (s, e) => {
                var movimientosVenta = new FMovimientoConsulta();
                movimientosVenta.ShowDialog();
            };

            p.Controls.Add(dgvVentas);
            p.Controls.Add(lbl);
            p.Controls.Add(footer);
            return p;
        }

        // ===========================================================================
        // SECCIÓN INFERIOR: PRODUCTOS
        // ===========================================================================
        private Panel CrearPanelProductos()
        {
            Panel p = new Panel { Dock = DockStyle.Fill, Padding = new Padding(5) };
            Panel pHeader = new Panel { Dock = DockStyle.Top, Height = 35 };

            Label lbl = new Label
            {
                Text = "Productos",
                Width = 100,
                Top = 5,
                Font = new Font("Segoe UI", 10, FontStyle.Bold)
            };

            txtBuscador = new TextBox
            {
                Width = 350,
                Left = 110,
                Top = 5,
                PlaceholderText = "Buscar por nombre..."
            };

            txtBuscador.TextChanged += (s, e) =>
            {
                _paginaActual = 1;
                RefrescarProductos();
            };

            pHeader.Controls.Add(lbl);
            pHeader.Controls.Add(txtBuscador);

            dgvProds = ConfigurarGridSimple();
            dgvProds.Columns.Add("Codigo", "Código");
            dgvProds.Columns.Add("Nombre", "Descripción");
            dgvProds.Columns.Add("Precio", "Precio $");
            dgvProds.Columns.Add("Stock", "Stock");

            var footer = CrearFooterPaginado(out btnPrevProd, out btnNextProd, out lblPaginaInfo, () => { });

            btnPrevProd.Click += (s, e) =>
            {
                if (_paginaActual > 1)
                {
                    _paginaActual--;
                    RefrescarProductos();
                }
            };

            btnNextProd.Click += (s, e) =>
            {
                if (_paginaActual < _totalPaginas)
                {
                    _paginaActual++;
                    RefrescarProductos();
                }
            };

            btnVerMas.Click += (s, e) =>
            {
                var consultaProducto = new FProductoConsulta();
                consultaProducto.ShowDialog();
            };

            p.Controls.Add(dgvProds);
            p.Controls.Add(pHeader);
            p.Controls.Add(footer);

            return p;
        }

        // ===========================================================================
        // GRID SIMPLE (Sin estilos forzados)
        // ===========================================================================
        private DataGridView ConfigurarGridSimple()
        {
            var dgv = new DataGridView
            {
                Dock = DockStyle.Fill,
                BackgroundColor = SystemColors.Window,
                BorderStyle = BorderStyle.FixedSingle,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                AllowUserToAddRows = false,
                ReadOnly = true,
                RowHeadersVisible = false,
                ColumnHeadersHeight = 35,
                EnableHeadersVisualStyles = false
            };

            dgv.RowTemplate.Height = 28;
            dgv.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgv.Margin = new Padding(0, 0, 0, 8);
            return dgv;
        }

        // ===========================================================================
        // FOOTER SIMPLE
        // ===========================================================================
        private Panel CrearFooterPaginado(out Button btnPrev, out Button btnNext, out Label lblInfo, Action onRefresh)
        {
            Panel pNav = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 50,
                Padding = new Padding(0, 15, 0, 0)
            };

            btnPrev = new Button { Text = "<", Width = 40, Dock = DockStyle.Left };
            btnNext = new Button { Text = ">", Width = 40, Dock = DockStyle.Left };
            lblInfo = new Label { Text = "Página 1 de 1", AutoSize = true, Dock = DockStyle.Left, Padding = new Padding(10, 12, 0, 0) };

            btnVerMas = new Button { Text = "Ver Más", Width = 80, Dock = DockStyle.Right };

            btnPrev.Click += (s, e) => onRefresh();
            btnNext.Click += (s, e) => onRefresh();

            // 🎨 NUEVO: Aplicación automática de estilos a los controles del footer creado
            ConfigurarColoresBotones(btnPrev, esPrimario: false);
            ConfigurarColoresBotones(btnNext, esPrimario: false);
            ConfigurarColoresBotones(btnVerMas, esPrimario: true);

            pNav.Controls.Add(lblInfo);
            pNav.Controls.Add(btnNext);
            pNav.Controls.Add(btnPrev);
            pNav.Controls.Add(btnVerMas);

            return pNav;
        }

        // ===========================================================================
        // METODOS PARA CARGAR VENTAS Y PRODUCTOS
        // ===========================================================================
        public void ActualizarTablaVentas(IEnumerable<VentaDTO> listaVentas)
        {
            dgvVentas.Rows.Clear();
            foreach (var v in listaVentas)
            {
                dgvVentas.Rows.Add(v.NumeroVenta, v.FechaVenta.ToString("G"), v.Detalle, v.Total.ToString("C2"));
            }
        }

        public void ActualizarTablaProductos(IEnumerable<ProductoDTO> listaProds)
        {
            dgvProds.Rows.Clear();
            foreach (var p in listaProds)
            {
                dgvProds.Rows.Add(p.Codigo, p.Descripcion, p.PrecioVenta.ToString("C2"), p.Stock);
            }
        }

        // ===========================================================================
        // Refresh tablas
        // ===========================================================================
        private void RefrescarProductos()
        {
            var filtro = new FiltroConsulta
            {
                TextoBuscar = txtBuscador.Text,
                Page = _paginaActual,
                PageSize = _pageSize,
                Filtro1 = "Descripcion"
            };

            var resultado = _productoService.ObtenerProductos(filtro);
            dgvProds.Rows.Clear();

            foreach (var p in resultado.Items)
            {
                dgvProds.Rows.Add(p.Codigo, p.Descripcion, p.PrecioVenta.ToString("C2"), p.Stock);
            }

            _totalPaginas = resultado.TotalPaginas;
            _paginaActual = resultado.Page;

            lblPaginaInfo.Text = $"Página {_paginaActual} de {_totalPaginas}";
            btnPrevProd.Enabled = _paginaActual > 1;
            btnNextProd.Enabled = _paginaActual < _totalPaginas;
        }

        public void RefrescarVentas()
        {
            var filtroVentas = new FiltroConsulta
            {
                Page = _paginaActualV,
                PageSize = _pageSizeV,
                TextoBuscar = ""
            };

            var resultado = _ventaService.ObtenerVentas(filtroVentas);
            dgvVentas.Rows.Clear();

            foreach (var v in resultado.Items)
            {
                dgvVentas.Rows.Add(v.NumeroVenta, v.FechaVenta.ToString("g"), v.Detalle, v.Total.ToString("C2"));
            }

            _totalPaginasV = resultado.TotalPaginas;
            _paginaActualV = resultado.Page;

            lblPaginaInfoVenta.Text = $"Página {_paginaActualV} de {_totalPaginasV}";
            btnPrevVenta.Enabled = _paginaActualV > 1;
            btnNextVenta.Enabled = _paginaActualV < _totalPaginasV;
        }
    }
}