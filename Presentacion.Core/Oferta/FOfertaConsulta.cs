using Presentacion.Core.Oferta;
using Presentacion.Core.Presentacion.Core.Helpers;
using Presentacion.FBase;
using Servicios.Helpers.Sistema.FiltrosConsulta;
using Servicios.Helpers.Venta.Oferta;
using Servicios.LogicaNegocio.Venta.Oferta;

public partial class FOfertaConsulta : FBaseConsulta
{
    private readonly IOfertaServicio _ofertaServicio;

    public long? ofertaSeleccionada = null;

    private bool _vieneDeVenta = false;
    private bool activarDesactivar = true;

    public FOfertaConsulta()
    {
        _ofertaServicio = new OfertaServicio();
    }

    public FOfertaConsulta(bool vieneDeVenta) : this()
    {
        _vieneDeVenta = vieneDeVenta;
        activarDesactivar = !vieneDeVenta;

        MessageBox.Show("Seleccione la oferta que desea aplicar a la venta.");
    }

    #region INIT

    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);

        ConfigurarFormulario();
    }

    private void ConfigurarFormulario()
    {
        Text = _vieneDeVenta
            ? "Seleccionar Oferta"
            : "Consulta de Ofertas";
    }

    #endregion
    #region FILTROS

    protected override bool UsarCheck1 => true;
    protected override bool UsarCheck2 => true;

    protected override bool EsModoSoloLectura(FiltroConsulta filtro)
    {
        return true;
    }

    protected override void ConfigurarFiltrosUI()
    {
        base.ConfigurarFiltrosUI();

        // Buscar por
        var opcionesBusqueda = new List<OpcionFiltro>
    {
        new OpcionFiltro { Texto = "Todos", Valor = "" },
        new OpcionFiltro { Texto = "Código", Valor = "Codigo" },
        new OpcionFiltro { Texto = "Descripción", Valor = "Descripcion" },
        new OpcionFiltro { Texto = "Producto", Valor = "Producto" }
    };

        ActivarCombo(
            cbx1,
            lblcbx1,
            opcionesBusqueda,
            "Texto",
            "Valor",
            "Buscar por"
        );

        // Tipo de oferta
        var opcionesTipoOferta = new List<OpcionFiltro>
    {
        new OpcionFiltro { Texto = "Todos", Valor = "" },
        new OpcionFiltro { Texto = "Grupo", Valor = ((int)TipoOferta.Grupo).ToString() },
        new OpcionFiltro { Texto = "Producto", Valor = ((int)TipoOferta.Producto).ToString() },
        new OpcionFiltro { Texto = "Combo", Valor = ((int)TipoOferta.Combo).ToString() },
        new OpcionFiltro { Texto = "2x1", Valor = ((int)TipoOferta.DosPorUno).ToString() }
    };

        ActivarCombo(
            cbx2,
            lblcbx2,
            opcionesTipoOferta,
            "Texto",
            "Valor",
            "Tipo de Oferta"
        );

        // Fecha
        var opcionFecha = new List<OpcionFiltro>
    {
        new OpcionFiltro { Texto = "Todos", Valor = "" },
        new OpcionFiltro { Texto = "Fecha de Inicio", Valor = "FechaInicio" },
        new OpcionFiltro { Texto = "Fecha de Fin", Valor = "FechaFin" }
    };

        ActivarCombo(
            cbx3,
            lblcbx3,
            opcionFecha,
            "Texto",
            "Valor",
            "Fecha"
        );

        ActivarFiltroFechas("Filtrar por fecha");

        ActivarCheck(chkBool1, "Ver ofertas inactivas");
        ActivarCheck(chkBool2, "Ver ofertas (últimos 6 meses)");

        cbx1.SelectedValue = "";
        cbx2.SelectedValue = "";
        cbx3.SelectedValue = "";
    }

    protected override string TextoLblBuscar
    => "Buscar Oferta:";

    protected override string TextoLblCbx1
        => "Buscar por";

    protected override string TextoLblCbx2
        => "Tipo de Oferta";

    protected override string TextoLblCbx3
        => "Fecha";

    protected override string TextoTitular
        => "Listado de Ofertas";
    #endregion
    #region ACCIONES DINAMICAS
    protected override void AccionCheck1()
    {
        if (chkBool1.Checked)
        {
            _actualizandoFiltros = true;
            chkBool2.Checked = false;
            _actualizandoFiltros = false;

            LimpiarFiltrosParaTodos();
        }

        paginaActual = 1;
    }

    protected override void AccionCheck2()
    {
        if (chkBool2.Checked)
        {
            _actualizandoFiltros = true;
            chkBool1.Checked = false;
            _actualizandoFiltros = false;

            LimpiarFiltrosParaTodos();
        }

        paginaActual = 1;
    }
    private void LimpiarFiltrosParaTodos()
    {
        _actualizandoFiltros = true;

        txtBuscar.Clear();

        if (cbx1.Enabled)
            cbx1.SelectedIndex = 0;

        if (cbx2.Enabled)
            cbx2.SelectedIndex = 0;

        if (cbx3.Enabled)
            cbx3.SelectedIndex = 0;

        chkUsarFecha.Checked = false;

        _actualizandoFiltros = false;
    }
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
        if (!id.HasValue)
        {
            MessageBox.Show("Seleccione una oferta.");
            return;
        }

        //var ofertaAD = _ofertaServicio.ActivarDesactivar(id.Value);
        MessageBox.Show($"Funcionalidad de activar/desactivar oferta no implementada en el servicio.{entidadID}/{id}");

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

        ofertaSeleccionada = entidadID;

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

        var resultado = _ofertaServicio.ObtenerOfertas(filtros, _vieneDeVenta);

        dgv.DataSource = resultado.Items;

        ResetearGrilla(dgv);

        var paginacion = new DatosPaginacion
        {
            PaginaActual = resultado.Page,
            PageSize = resultado.PageSize,
            CantidadRegistros = resultado.TotalRegistros
        };

        ActualizarPaginacionUI(paginacion);
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
        if (grilla.Columns.Contains("Codigo"))
        {
            grilla.Columns["Codigo"].Visible = true;
            grilla.Columns["Codigo"].HeaderText = "Código";
            grilla.Columns["Codigo"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            grilla.Columns["Codigo"].Width = 180;
        }
        if (grilla.Columns.Contains("Descripcion"))
        {
            grilla.Columns["Descripcion"].Visible = true;
            grilla.Columns["Descripcion"].HeaderText = "Descripción";
            grilla.Columns["Descripcion"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
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

        // Ocultar propiedad bool
        if (grilla.Columns.Contains("EstaActiva"))
        {
            grilla.Columns["EstaActiva"].Visible = false;
        }

        // Mostrar descripción del estado
        if (grilla.Columns.Contains("DescripcionEstado"))
        {
            grilla.Columns["DescripcionEstado"].Visible = true;
            grilla.Columns["DescripcionEstado"].HeaderText = "Estado";
            grilla.Columns["DescripcionEstado"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
        }

        // Ocultar int
        if (grilla.Columns.Contains("TipoOferta"))
        {
            grilla.Columns["TipoOferta"].Visible = false;
        }

        // Mostrar descripción
        if (grilla.Columns.Contains("DescripcionTipoOferta"))
        {
            grilla.Columns["DescripcionTipoOferta"].Visible = true;
            grilla.Columns["DescripcionTipoOferta"].HeaderText = "Tipo Oferta";
            grilla.Columns["DescripcionTipoOferta"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
        }

        if (grilla.Columns.Contains("PorcentajeDescuento"))
        {
            grilla.Columns["PorcentajeDescuento"].Visible = true;
            grilla.Columns["PorcentajeDescuento"].HeaderText = "% Desc.";
            grilla.Columns["PorcentajeDescuento"].DefaultCellStyle.Format = "N2";
            grilla.Columns["PorcentajeDescuento"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
        }

        if (grilla.Columns.Contains("PrecioFinal"))
        {
            grilla.Columns["PrecioFinal"].Visible = true;
            grilla.Columns["PrecioFinal"].HeaderText = "Precio Oferta";
            grilla.Columns["PrecioFinal"].DefaultCellStyle.Format = "C2";
            grilla.Columns["PrecioFinal"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
        }

        // Ocultar navegación
        if (grilla.Columns.Contains("Productos"))
        {
            grilla.Columns["Productos"].Visible = false;
        }
    }

    #endregion
}