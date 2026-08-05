using Presentacion.FBase.Helpers;
using Servicios.Helpers.Movimiento;
using Servicios.LogicaNegocio.Movimiento;
using Servicios.LogicaNegocio.Movimiento.DTO;
using System;
using System.Drawing;
using System.Windows.Forms;
using Presentacion.Core.Movimiento;
using TuProyecto.Presentacion.Paneles; // Asegúrate de apuntar a la carpeta de tus paneles

namespace TuProyecto.Presentacion
{
    public partial class FMovimientoDetallado : global::Presentacion.FBase.FBase
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
            this.Size = new Size(1000, 800);
            // Evita que el layout se rompa (grid de Venta, TextBox de detalle con MaximumSize fijo)
            // si el usuario achica demasiado la ventana.
            this.MinimumSize = new Size(750, 550);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Consulta Detallada de Movimiento";
            // El color de fondo lo aplica AplicarTema (FBase) al cargar el formulario.

            // 1. Panel Superior (General)
            _panelGeneral = new PanelMovimientoGeneral();
            _panelGeneral.Dock = DockStyle.Top;

            // 2. Panel Inferior (Botones)
            Panel pnlBotonera = new Panel { Dock = DockStyle.Bottom, Height = 70, BackColor = TemaSistema.Fondo };
            _btnCerrar = new Button
            {
                Text = "Cerrar Detalle",
                Size = new Size(150, 40),
                Anchor = AnchorStyles.Bottom | AnchorStyles.Right
                // Colores y FlatStyle los aplica AplicarTema (FBase), igual que en el resto de los forms.
            };
            // Posición fija inicial, el Anchor lo mantendrá ahí
            _btnCerrar.Location = new Point(pnlBotonera.Width - 170, 15);
            pnlBotonera.Controls.Add(_btnCerrar);

            // 3. Contenedor Central (Dinamico) - SE AGREGA AL FINAL
            _pnlContenedorDinamico = new Panel();
            _pnlContenedorDinamico.Dock = DockStyle.Fill;

            // IMPORTANTE: El orden de agregado para que el Dock.Fill funcione bien
            this.Controls.Add(_pnlContenedorDinamico); // El que llena el espacio
            this.Controls.Add(_panelGeneral);          // El que va arriba
            this.Controls.Add(pnlBotonera);           // El que va abajo

            _btnCerrar.Click += (s, e) => this.Close();
            this.Load += FrmDetalleMovimiento_Load;
        }

        private void FrmDetalleMovimiento_Load(object sender, EventArgs e)
        {
            CargarInformacion();

            // Foco inicial en "Cerrar Detalle": permite cerrar con Enter/Espacio sin usar el mouse,
            // y evita que el modal quede sin ningún control activo al abrirse.
            // Se valida que el form siga abierto porque CargarInformacion pudo haberlo cerrado
            // (caso de error o "movimiento no encontrado").
            if (!this.IsDisposed)
                _btnCerrar.Focus();
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
                // Antes solo se mostraba el error y la ventana quedaba abierta con el panel general
                // vacío (sin datos cargados) — daba la sensación de que el sistema se colgó. Ahora se
                // cierra igual que en el caso "datos == null", que es el otro camino de fallo esperado.
                MessageBox.Show("Error al cargar datos: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
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

            if (!Enum.IsDefined(typeof(TipoEntidadMovimiento), datos.TipoEntidad.Value))
            {
                MostrarAvisoSinDetalle($"Tipo de entidad desconocida o no soportada (Código: {datos.TipoEntidad.Value}).");
                return;
            }

            switch ((TipoEntidadMovimiento)datos.TipoEntidad.Value)
            {
                case TipoEntidadMovimiento.Venta:
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

                case TipoEntidadMovimiento.CuentaCorriente:
                    if (datos.CuentaCorriente != null)
                    {
                        var panelCC = new PanelMovimientoCuentaCorriente();
                        _pnlContenedorDinamico.Controls.Add(panelCC);
                        panelCC.CargarDatos(datos.CuentaCorriente);
                    }
                    else
                    {
                        MostrarAvisoSinDetalle("Movimiento de Cuenta Corriente sin datos asociados.");
                    }
                    break;

                case TipoEntidadMovimiento.Gasto:
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
                    MostrarAvisoSinDetalle($"Tipo de entidad '{(TipoEntidadMovimiento)datos.TipoEntidad.Value}' sin panel de detalle asociado.");
                    break;
            }
        }

        // Método auxiliar para no repetir código visual de advertencia
        private void MostrarAvisoSinDetalle(string mensaje)
        {
            // Antes quedaba pegado en la esquina superior izquierda (Point(20,20)), se veía como
            // texto perdido. Ahora ocupa todo el contenedor dinámico y centra el mensaje, como un
            // estado vacío prolijo en vez de una advertencia suelta.
            Label lblAviso = new Label
            {
                Text = mensaje,
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Segoe UI", 11, FontStyle.Italic),
                ForeColor = TemaSistema.TextoSecundario
            };
            _pnlContenedorDinamico.Controls.Add(lblAviso);
        }
    }
}