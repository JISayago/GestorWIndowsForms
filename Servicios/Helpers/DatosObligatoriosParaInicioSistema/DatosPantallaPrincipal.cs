using Servicios.Helpers.DatosObligatoriosParaInicioSistema;
using Servicios.Helpers.Sistema.FiltrosConsulta;
using Servicios.LogicaNegocio.PantallaPrincipal;
using Servicios.LogicaNegocio.Producto;
using Servicios.LogicaNegocio.Producto.DTO;
using Servicios.LogicaNegocio.Producto.Lote;
using Servicios.LogicaNegocio.Venta;
using Servicios.LogicaNegocio.Venta.DTO;

public class DatosPantallaPrincipal
{
    private readonly IPantallaPrincipalServicio _pantallaServicio;
    private readonly ILoteServicio _loteServicio;
    private readonly IProductoServicio _productoServicio;
    private readonly IVentaServicio _ventaServicio;

    public long UsuarioId { get; private set; }
    public long? CajaId { get; private set; }

    public ElementoDePanelesPantallaPrincipal DPantallaPrincipal { get; private set; }

    // 🔥 NUEVO
    public List<ProductoDTO> Productos { get; private set; }
    public List<VentaDTO> Ventas { get; private set; }

    public DatosPantallaPrincipal()
    {
        _pantallaServicio = new PantallaPrincipalServicio();
        _loteServicio = new LoteServicio();
        _productoServicio = new ProductoServicio();
        _ventaServicio = new VentaServicio();
    }

    public void Inicializar(long usuarioId, long? cajaId)
    {
        UsuarioId = usuarioId;
        CajaId = cajaId;

        // 🔹 Datos principales
        DPantallaPrincipal = new ElementoDePanelesPantallaPrincipal
        {
            DatosTurno = _pantallaServicio.ObtenerDatosTurno(cajaId, usuarioId),

            //NotificacionesLotes = _loteServicio.ObtenerNotificacionesLotes(),

            //NotificacionesPromociones = _pantallaServicio.ObtenerNotificacionesPromociones(),

            //NotificacionesCuentaCorriente = _pantallaServicio.ObtenerNotificacionesCuentaCorriente()
        };

        // 🔥 CONSULTAS RÁPIDAS (LO QUE TE FALTABA)
        var filtroProductos = new FiltroConsulta
        {
            Page = 1,
            PageSize = 10,

            TextoBuscar = string.Empty,

            Bool1 = false // no eliminados
        };

        var resultadoProductos = _productoServicio.ObtenerProductos(filtroProductos);

        Productos = resultadoProductos.Items.ToList();

        var filtroVentas = new FiltroConsulta
        {
            Page = 1,
            PageSize = 12,

            TextoBuscar = string.Empty,

            Bool1 = false // no eliminados
        };

        var resultadoVentas = _ventaServicio.ObtenerVentas(filtroVentas);

        Ventas = resultadoVentas.Items.ToList();
    }
}