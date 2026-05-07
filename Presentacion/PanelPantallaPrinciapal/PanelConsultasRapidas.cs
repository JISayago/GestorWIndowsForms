using AccesoDatos.Entidades;
using Presentacion.Core.Movimiento;
using Presentacion.Core.Producto;
using Servicios.Helpers.Sistema.FiltrosConsulta;
using Servicios.LogicaNegocio.Producto;
using Servicios.LogicaNegocio.Venta;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            // Usamos colores neutros de sistema
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

            RefrescarProductos();
            RefrescarVentas();

        }

        // ===========================================================================
        // CONFIGURACIÓN PARA BLOQUEAR RESIZE Y PERMITIR COPIADO
        // ===========================================================================
        private void ConfigurarEstiloGrid(DataGridView dgv)
        {
            // 1. BLOQUEAR RESIZE (Columnas y Filas)
            dgv.AllowUserToResizeRows = false;
            dgv.AllowUserToResizeColumns = false;
            dgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;

            // 2. CONFIGURACIÓN DE EDICIÓN Y SELECCIÓN
            dgv.ReadOnly = true;
            dgv.SelectionMode = DataGridViewSelectionMode.CellSelect;
            dgv.MultiSelect = false;

            // --- SOLUCIÓN 1: Evitar que el Ctrl+C nativo sobrescriba nuestro copiado ---
            dgv.ClipboardCopyMode = DataGridViewClipboardCopyMode.Disable;

            // 3. LÓGICA DE COPIADO AL PORTAPAPELES
            dgv.CellClick += (sender, e) => {
                // Validamos que no sea el encabezado (RowIndex -1)
                if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
                {
                    DataGridView grid = (DataGridView)sender;
                    string nombreColumna = grid.Columns[e.ColumnIndex].Name;

                    // Definimos qué columnas permiten copiado (ajustado a tus nombres de grilla)
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

                            // LÓGICA DE FORMATEO
                            if (nombreColumna.Equals("Id", StringComparison.OrdinalIgnoreCase) || nombreColumna.Contains("Comprobante"))
                            {
                                string[] partes = textoACopiar.Split('-');
                                if (partes.Length >= 2)
                                {
                                    textoACopiar = partes[1]; // Nos quedamos con "20260427"
                                }
                            }

                            // --- SOLUCIÓN 2: Blindar el portapapeles ---
                            // El portapapeles falla si le pasas un texto vacío o si otro proceso lo está leyendo
                            if (!string.IsNullOrWhiteSpace(textoACopiar))
                            {
                                try
                                {
                                    // Intento normal
                                    Clipboard.SetText(textoACopiar);
                                }
                                catch (System.Runtime.InteropServices.ExternalException)
                                {
                                    // Si falla porque Windows lo tiene bloqueado, forzamos la inserción con reintentos
                                    // SetDataObject(datos, mantener_al_cerrar_app, reintentos, delay_ms)
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

            // Footter para VENTAS
            var footer = CrearFooterPaginado(out btnPrevVenta, out btnNextVenta, out lblPaginaInfoVenta, () => {
                // Lógica de cambio de página para ventas
                // El botón presionado se detecta indirectamente o por lógica simple:
                // Nota: Para hacerlo más simple, puedes mover el _paginaActual++ dentro de los clicks en el footer 
                // o manejarlo aquí.
            });

            // Suscribimos los eventos manualmente para mayor claridad:
            btnPrevVenta.Click += (s, e) => { if (_paginaActualV > 1) { _paginaActualV--; RefrescarVentas(); } };
            btnNextVenta.Click += (s, e) => { if (_paginaActualV < _totalPaginasV) { _paginaActualV++; RefrescarVentas(); } };
            btnVerMas.Click += (s, e) => {
                // Aquí podrías abrir un nuevo formulario con una grilla completa o aplicar un filtro más amplio
                var movimientosVenta = new FMovimientoConsulta();
                {

                };
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

            Label lbl = new Label { Text = "Productos", Width = 100, Top = 5, Font = new Font("Segoe UI", 10, FontStyle.Bold) };

            // Inicializamos el buscador
            txtBuscador = new TextBox { Width = 350, Left = 110, Top = 5, PlaceholderText = "Buscar por nombre..." };

            // Evento: Al cambiar el texto, volvemos a la página 1 y buscamos
            txtBuscador.TextChanged += (s, e) => { _paginaActual = 1; RefrescarProductos(); };

            pHeader.Controls.Add(lbl);
            pHeader.Controls.Add(txtBuscador);

            dgvProds = ConfigurarGridSimple();
            dgvProds.Columns.Add("Codigo", "Código");
            dgvProds.Columns.Add("Nombre", "Descripción");
            dgvProds.Columns.Add("Precio", "Precio $");
            dgvProds.Columns.Add("Stock", "Stock");

            // Footer para PRODUCTOS
            var footer = CrearFooterPaginado(out btnPrevProd, out btnNextProd, out lblPaginaInfo, () => { });

            btnPrevProd.Click += (s, e) => { if (_paginaActual > 1) { _paginaActual--; RefrescarProductos(); } };
            btnNextProd.Click += (s, e) => { if (_paginaActual < _totalPaginas) { _paginaActual++; RefrescarProductos(); } };
            btnVerMas.Click += (s, e) => {
                // Aquí podrías abrir un nuevo formulario con una grilla completa o aplicar un filtro más amplio
                var consultaProducto = new FProductoConsulta()
                {

                };
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
                ColumnHeadersHeight = 35, // Un poco más alto para el encabezado
                EnableHeadersVisualStyles = false // Permite personalizar mejor el estilo
            };

            // --- EL CAMBIO CLAVE AQUÍ ---
            dgv.RowTemplate.Height = 28; // Cambia 35 por el valor que prefieras (ej. 40 o 45)

            // Alineación vertical centrada para que el texto no quede arriba
            dgv.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            return dgv;
        }

        // ===========================================================================
        // FOOTER SIMPLE
        // ===========================================================================
        private Panel CrearFooterPaginado(out Button btnPrev, out Button btnNext, out Label lblInfo, Action onRefresh)
        {
            Panel pNav = new Panel { Dock = DockStyle.Bottom, Height = 45 };

            btnPrev = new Button { Text = "<", Width = 40, Dock = DockStyle.Left };
            btnNext = new Button { Text = ">", Width = 40, Dock = DockStyle.Left };
            lblInfo = new Label { Text = "Página 1 de 1", AutoSize = true, Dock = DockStyle.Left, Padding = new Padding(10, 12, 0, 0) };

            btnVerMas = new Button { Text = "Ver Más", Width = 80, Dock = DockStyle.Right };
            // Asignamos la acción que se pase por parámetro
            btnPrev.Click += (s, e) => onRefresh();
            btnNext.Click += (s, e) => onRefresh();

            pNav.Controls.Add(lblInfo);
            pNav.Controls.Add(btnNext);
            pNav.Controls.Add(btnPrev);
            pNav.Controls.Add(btnVerMas);

            return pNav;
        }

        // ===========================================================================
        // METODO PARA CARGAR VENTAS Y PRODUCTOS
        // ===========================================================================
        // Método para cargar Ventas
        public void ActualizarTablaVentas(IEnumerable<dynamic> listaVentas)
        {
            dgvVentas.Rows.Clear(); // Limpiamos datos viejos
            foreach (var v in listaVentas)
            {
                // El orden debe coincidir con las columnas: Id, Fecha, Cliente, Total
                dgvVentas.Rows.Add(v.Id, v.Fecha.ToString("G"), v.Cliente, v.Total.ToString("C2"));
            }
        }

        // Método para cargar Productos
        public void ActualizarTablaProductos(IEnumerable<dynamic> listaProds)
        {
            dgvProds.Rows.Clear();
            foreach (var p in listaProds)
            {
                // El orden debe coincidir con: Codigo, Nombre, Precio, Stock
                dgvProds.Rows.Add(p.Codigo, p.Nombre, p.Precio.ToString("C2"), p.Stock);
            }
        }

        // ===========================================================================
        // Refresh tablas
        // ===========================================================================
        private void RefrescarProductos()
        {
            // 1. Preparamos el filtro según lo que espera tu Service
            //var filtro = new FiltroConsulta
            //{
            //    TextoBuscar = txtBuscador.Text,
            //    Page = _paginaActual,
            //    PageSize = _pageSize,
            //    VerEliminados = false
            //};

            //// 2. Llamada a tu service (Suponiendo que tienes una instancia de tu clase de servicio)
            //var resultado = _productoService.ObtenerProductos(filtro);

            // 3. Limpiar y Cargar Grilla
            dgvProds.Rows.Clear();
            //foreach (var p in resultado.Items)
            //{
            //    dgvProds.Rows.Add(
            //        p.Codigo,
            //        p.Descripcion,
            //        p.PrecioVenta.ToString("C2"),
            //        p.Stock
            //    );
            //}

            // 4. Actualizar UI de paginación
            //_totalPaginas = resultado.TotalPaginas;
            //_paginaActual = resultado.Page; // Usamos la corregida por el service

            lblPaginaInfo.Text = $"Página {_paginaActual} de {_totalPaginas}";
            btnPrevProd.Enabled = _paginaActual > 1;
            btnNextProd.Enabled = _paginaActual < _totalPaginas;
        }

        public void RefrescarVentas()
        {
            //var filtroVentas = new FiltroConsulta
            //{
            //    Page = _paginaActualV,
            //    PageSize = _pageSizeV, // Usamos la constante de 10
            //    TextoBuscar = "",
            //    VerEliminados = false,
            //    TotalRegistros = 24 // Limitar a 20 registros para la consulta rápida
            //};

            //var resultado = _ventaService.ObtenerVentas(filtroVentas);

            //dgvVentas.Rows.Clear();
            //foreach (var v in resultado.Items)
            //{
            //    dgvVentas.Rows.Add(
            //        v.NumeroVenta,
            //        v.FechaVenta.ToString("g"),
            //        v.Detalle,
            //        v.Total.ToString("C2")
            //    );
            //}

            //// 4. ACTUALIZAR UI DE VENTAS (Cuidado aquí de no usar lblPaginaInfo de productos)
            //_totalPaginasV = resultado.TotalPaginas;
            //_paginaActualV = resultado.Page;

            //lblPaginaInfoVenta.Text = $"Página {_paginaActualV} de {_totalPaginasV}";
            //btnPrevVenta.Enabled = _paginaActualV > 1;
            //btnNextVenta.Enabled = _paginaActualV < _totalPaginasV;
        }
    }
}
