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
                var servicio = new MovimientoServicio();

                // NOTA: Asegúrate de llamar al método unificado o de saber de antemano qué método llamar.
                // Si el método que trae TODO (Ventas o Gastos) se llama ObtenerDatosParaMovimientoConsultaGasto, úsalo aquí.
                var datos = servicio.ObtenerDatosParaMovimientoConsulta(_movimientoId);

                if (datos == null)
                {
                    MessageBox.Show("No se encontró información del movimiento.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    this.Close();
                    return;
                }

                // 1. Llenar el panel general fijo (el de arriba)
                _panelGeneral.CargarDatos(datos);

                // 2. Determinar qué panel inyectar abajo
                CargarPanelSegunEntidad(datos);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar datos: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CargarPanelSegunEntidad(MovimientoHelperDTO datos)
        {
            _pnlContenedorDinamico.Controls.Clear();

            // Si TipoEntidad es nulo, mostramos un mensaje
            if (!datos.TipoEntidad.HasValue)
            {
                MostrarAvisoSinDetalle("Este movimiento no tiene una entidad específica asociada.");
                return;
            }

            // Dependiendo de tu lógica de Enums, evalúas el tipo
            // Supongamos que 1 es Venta y 2 es Gasto (Ajusta los números según tu base de datos)
            switch (datos.TipoEntidad.Value)
            {
                case 1: // VENTA
                    if (datos.Venta != null)
                    {
                        var panelVenta = new PanelMovimientoVenta();
                        _pnlContenedorDinamico.Controls.Add(panelVenta);
                        panelVenta.CargarDatos(datos.Venta);
                    }
                    else
                    {
                        MostrarAvisoSinDetalle("El movimiento está marcado como Venta, pero los detalles no se encontraron.");
                    }
                    break;

                case 5: // GASTO
                    if (datos.Gasto != null)
                    {
                        var panelGasto = new PanelMovimientoGasto();
                        // El Dock.Fill ya está en el constructor del panel, pero lo reforzamos aquí
                        panelGasto.Dock = DockStyle.Fill;
                        _pnlContenedorDinamico.Controls.Add(panelGasto);
                        panelGasto.CargarDatos(datos.Gasto); // Inyectamos el DTO del Gasto
                    }
                    else
                    {
                        MostrarAvisoSinDetalle("El movimiento está marcado como Gasto, pero los detalles no se encontraron.");
                    }
                    break;

                default:
                    MostrarAvisoSinDetalle($"Tipo de entidad desconocida o no soportada (Código: {datos.TipoEntidad.Value}).");
                    break;
            }
        }

        // Método auxiliar para no repetir código visual de advertencia
        private void MostrarAvisoSinDetalle(string mensaje)
        {
            Label lblAviso = new Label
            {
                Text = mensaje,
                AutoSize = true,
                Location = new Point(20, 20),
                Font = new Font("Segoe UI", 11, FontStyle.Italic),
                ForeColor = Color.Gray
            };
            _pnlContenedorDinamico.Controls.Add(lblAviso);
        }
    }
}