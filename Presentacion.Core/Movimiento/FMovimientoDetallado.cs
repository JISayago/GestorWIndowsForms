using Servicios.LogicaNegocio.Movimiento;
using Servicios.LogicaNegocio.Movimiento.DTO;
using System;
using System.Drawing;
using System.Windows.Forms;
using Presentacion.Core.Movimiento;
using TuProyecto.Presentacion.Paneles; // Asegúrate de apuntar a la carpeta de tus paneles

namespace TuProyecto.Presentacion
{
    public partial class FMovimientoDetallado : Form
    {
        private readonly long _movimientoId;

        // Controles de interfaz
        private PanelMovimientoGeneral _panelGeneral;
        private Panel _pnlContenedorDinamico;
        private Button _btnCerrar;

        public FMovimientoDetallado(long movimientoId)
        {
            _movimientoId = movimientoId;
            //InitializeComponent(); // Estándar de WinForms
            CrearInterfazGrafica();
        }

        private void CrearInterfazGrafica()
        {
            // Tamaño amplio para que las columnas de la grilla respiren
            this.Size = new Size(1100, 850);
            this.MinimumSize = new Size(900, 700);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Consulta Detallada de Movimiento";
            this.BackColor = Color.White;

            // 1. Panel Superior
            _panelGeneral = new PanelMovimientoGeneral();
            _panelGeneral.Dock = DockStyle.Top;

            // 2. Panel Inferior (Botones)
            Panel pnlBotonera = new Panel { Dock = DockStyle.Bottom, Height = 70, BackColor = Color.FromArgb(245, 245, 245) };
            _btnCerrar = new Button
            {
                Text = "Cerrar Detalle",
                Size = new Size(150, 40),
                Location = new Point(pnlBotonera.Width - 170, 15),
                Anchor = AnchorStyles.Bottom | AnchorStyles.Right,
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.White,
                Cursor = Cursors.Hand
            };
            pnlBotonera.Controls.Add(_btnCerrar);

            // 3. Contenedor Central
            _pnlContenedorDinamico = new Panel();
            _pnlContenedorDinamico.Dock = DockStyle.Fill;
            // Agregamos una línea divisoria visual (opcional)
            _pnlContenedorDinamico.Padding = new Padding(5, 10, 5, 5);

            // Agregamos en orden inverso de importancia para el Docking
            this.Controls.Add(_pnlContenedorDinamico);
            this.Controls.Add(_panelGeneral);
            this.Controls.Add(pnlBotonera);

            _btnCerrar.Click += (s, e) => this.Close();
            this.Load += FrmDetalleMovimiento_Load;
        }

        private void FrmDetalleMovimiento_Load(object sender, EventArgs e)
        {
            CargarInformacion();
        }

        private void CargarInformacion()
        {
            try
            {
                // Llamamos a tu función del service
                // (Asumo que tienes una instancia de tu servicio o es estática)
                var servicio = new MovimientoServicio();
                var datos = servicio.ObtenerDatosParaMovimientoConsulta(_movimientoId);

                if (datos == null)
                {
                    MessageBox.Show("No se encontró información del movimiento.");
                    this.Close();
                    return;
                }

                // Paso 1: Llenar el panel general
                _panelGeneral.CargarDatos(datos);

                // Paso 2: Determinar qué panel inyectar en el contenedor dinámico
                CargarPanelSegunEntidad(datos);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar datos: " + ex.Message);
            }
        }

        private void CargarPanelSegunEntidad(MovimientoHelperDTO datos)
        {
            _pnlContenedorDinamico.Controls.Clear();

            // Usamos el TipoEntidad para decidir
            // Reemplaza 'TipoEntidadMovimiento' por tu Enum real
            if (datos.TipoEntidad == 1) // Supongamos que 1 es Venta
            {
                var panelVenta = new PanelMovimientoVenta();
                panelVenta.Dock = DockStyle.Fill;
                panelVenta.CargarDatos(datos.Venta); // Le pasamos el DTO de venta

                _pnlContenedorDinamico.Controls.Add(panelVenta);
            }
            else
            {
                // Si en el futuro agregas Compras, pondrías otro 'else if' aquí
                Label lblAviso = new Label
                {
                    Text = "Información detallada no disponible para este tipo de entidad.",
                    AutoSize = true,
                    Location = new Point(20, 20)
                };
                _pnlContenedorDinamico.Controls.Add(lblAviso);
            }
        }
    }
}