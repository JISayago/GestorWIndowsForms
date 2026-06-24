using Presentacion.Core.Oferta;
using Presentacion.FBase;
using Servicios.Helpers.Sistema.FiltrosConsulta;
using Servicios.LogicaNegocio.Venta.Oferta;

public partial class FOfertaConsulta : FBaseConsulta
{
    private readonly IOfertaServicio _ofertaServicio;

    public long? ofertaSeleccionada = null;

    private bool _vieneDeVenta = false;
    private bool activarDesactivar = false;

    public FOfertaConsulta()
    {
        _ofertaServicio = new OfertaServicio();
    }

    public FOfertaConsulta(bool vieneDeVenta) : this()
    {
        _vieneDeVenta = vieneDeVenta;

        MessageBox.Show("Seleccione la oferta que desea aplicar a la venta.");
    }

    public FOfertaConsulta(bool activarDesactivar, string a) : this()
    {
        this.activarDesactivar = activarDesactivar;
    }

    #region CONFIG FILTROS

    protected override bool UsarCheck1 => false;

    protected override void ConfigurarFiltrosUI()
    {
        base.ConfigurarFiltrosUI();

        ActivarFiltroFechas("Filtrar por fecha");
    }

    protected override void ActualizarTextosLabels()
    {
        base.ActualizarTextosLabels();

    }

    #endregion

    #region ACCIONES DINAMICAS

    protected override void ConfigurarAccionesPersonalizadas()
    {
        if (activarDesactivar)
        {
            AgregarAccion(
                "Activar/Desactivar Oferta",
                Presentacion.Constantes.Imagenes.ImgPromocion,
                ActivarDesactivarOferta,
                true
            );
        }

        if (_vieneDeVenta)
        {
            AgregarAccion(
                "Seleccionar para venta",
                Presentacion.Constantes.Imagenes.ImgPromocion,
                SeleccionarParaVenta,
                true
            );
        }
    }

    private void ActivarDesactivarOferta(long? id)
    {
        //if (!id.HasValue)
        //{
        //    MessageBox.Show("Seleccione una oferta.");
        //    return;
        //}

        //var ofertaAD = _ofertaServicio.ActivarDesactivar(id.Value);

        //if (ofertaAD.OfertaDescuentoId != null)
        //    MessageBox.Show($"La oferta {ofertaAD.Codigo} cambió su estado.");
        //else
        //    MessageBox.Show("No se pudo cambiar el estado.");

        RefrescarGrilla();
    }

    private void SeleccionarParaVenta(long? id)
    {
        if (!id.HasValue)
            return;

        ofertaSeleccionada = id;

        DialogResult = DialogResult.OK;

        Close();
    }

    #endregion

    #region BOTONES BASE

    public override void EjecutarBtnNuevo()
    {
        var f = new FOfertaGrupoABM();

        f.ShowDialog();

        RefrescarGrilla();
    }

    #endregion

    #region DATOS

    public override void ActualizarDatos(DataGridView dgv, FiltroConsulta filtros)
    {
        base.ActualizarDatos(dgv, filtros);

        //var resultado = _ofertaServicio.ObtenerOfertas(filtros, _vieneDeVenta);

        //dgv.DataSource = resultado.Items;

        //ResetearGrilla(dgv);

        //var paginacion = new DatosPaginacion
        //{
        //    PaginaActual = resultado.Page,
        //    PageSize = resultado.PageSize,
        //    CantidadRegistros = resultado.TotalRegistros
        //};

        //ActualizarPaginacionUI(paginacion);
    }

    #endregion

    #region LOAD

    private void FOfertaConsulta_Load(object sender, EventArgs e)
    {

    }

    #endregion

    #region GRILLA

    public override void ResetearGrilla(DataGridView grilla)
    {
        base.ResetearGrilla(grilla);

        if (grilla.Columns.Count == 0)
            return;

        if (grilla.Columns.Contains("OfertaDescuentoId"))
        {
            grilla.Columns["OfertaDescuentoId"].Visible = false;
            grilla.Columns["OfertaDescuentoId"].Name = "Id";
        }

        if (grilla.Columns.Contains("Descripcion"))
        {
            grilla.Columns["Descripcion"].Visible = true;
            grilla.Columns["Descripcion"].HeaderText = "Descripción";
            grilla.Columns["Descripcion"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }

        if (grilla.Columns.Contains("Codigo"))
        {
            grilla.Columns["Codigo"].Visible = true;
            grilla.Columns["Codigo"].HeaderText = "Código";
            grilla.Columns["Codigo"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
        }

        if (grilla.Columns.Contains("GrupoNombre"))
        {
            grilla.Columns["GrupoNombre"].Visible = true;
            grilla.Columns["GrupoNombre"].HeaderText = "Grupo";
            grilla.Columns["GrupoNombre"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
        }

        if (grilla.Columns.Contains("FechaInicio"))
        {
            grilla.Columns["FechaInicio"].Visible = true;
            grilla.Columns["FechaInicio"].HeaderText = "Fecha Inicio";
            grilla.Columns["FechaInicio"].DefaultCellStyle.Format = "dd/MM/yyyy";
            grilla.Columns["FechaInicio"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
        }

        if (grilla.Columns.Contains("FechaFin"))
        {
            grilla.Columns["FechaFin"].Visible = true;
            grilla.Columns["FechaFin"].HeaderText = "Fecha Fin";
            grilla.Columns["FechaFin"].DefaultCellStyle.Format = "dd/MM/yyyy";
            grilla.Columns["FechaFin"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
        }

        if (grilla.Columns.Contains("EstaActiva"))
        {
            grilla.Columns["EstaActiva"].Visible = true;
            grilla.Columns["EstaActiva"].HeaderText = "Estado";
            grilla.Columns["EstaActiva"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
        }
    }

    #endregion
}