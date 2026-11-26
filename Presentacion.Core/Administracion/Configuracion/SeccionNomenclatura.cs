using Servicios.Helpers.Sistema;
using Servicios.LogicaNegocio.Configuracion;
using Servicios.LogicaNegocio.Configuracion.DTO;

namespace Presentacion.Core.Configuracion;

public sealed class SeccionNomenclatura : SeccionBase
{
    private readonly IConfiguracionServicio _servicio = new ConfiguracionServicio();
    private readonly Dictionary<TipoCodigo, NomenclaturaCodigoDTO> _datos = new();
    private TipoCodigo _tipoActual = TipoCodigo.Venta;
    private bool _cargando = true; // evita que los eventos pisen datos mientras se llena la pantalla

    private readonly ComboBox cboTipo = new() { DropDownStyle = ComboBoxStyle.DropDownList, Location = new Point(0, 20), Size = new Size(200, 25) };
    private readonly TextBox txtPrefijo = new() { MaxLength = 10, Location = new Point(0, 80), Size = new Size(120, 25) };
    private readonly TextBox txtSeparador = new() { MaxLength = 2, Location = new Point(140, 80), Size = new Size(60, 25) };
    private readonly NumericUpDown nudDigitos = new() { Minimum = 1, Maximum = 12, Value = 8, Location = new Point(220, 80), Size = new Size(80, 25) };
    private readonly CheckBox chkAnio = new() { Text = "Incluir año", AutoSize = true, Location = new Point(0, 130) };
    private readonly CheckBox chkPuntoVenta = new() { Text = "Incluir punto de venta", AutoSize = true, Location = new Point(140, 130) };
    private readonly NumericUpDown nudPuntoVenta = new() { Minimum = 1, Maximum = 9999, Value = 1, Location = new Point(330, 127), Size = new Size(80, 25), Enabled = false };
    private readonly Label lblVista = new() { AutoSize = true, Location = new Point(0, 195), Font = new Font("Segoe UI", 11F, FontStyle.Bold) };

    public override SeccionConfiguracion Clave => SeccionConfiguracion.Nomenclatura;
    public override bool DetectaCambiosAutomaticamente => false;
    public override string Descripcion => "Cómo se arman los códigos de ventas, movimientos y gastos.";

    public SeccionNomenclatura()
    {
        cboTipo.DataSource = Enum.GetValues<TipoCodigo>();
        cboTipo.SelectedIndexChanged += CambioTipo;

        foreach (var c in new Control[] { txtPrefijo, txtSeparador, chkAnio, chkPuntoVenta })
            if (c is TextBox t) t.TextChanged += CambioCampo; else if (c is CheckBox k) k.CheckedChanged += CambioCampo;
        nudDigitos.ValueChanged += CambioCampo;
        nudPuntoVenta.ValueChanged += CambioCampo;

        Controls.AddRange(new Control[]
        {
            Etiqueta("Tipo de código", 0, 0), cboTipo,
            Etiqueta("Prefijo", 0, 60), txtPrefijo,
            Etiqueta("Separador", 140, 60), txtSeparador,
            Etiqueta("Dígitos", 220, 60), nudDigitos,
            chkAnio, chkPuntoVenta, nudPuntoVenta,
            Etiqueta("Vista previa", 0, 170), lblVista
        });
    }

    public override void Cargar()
    {
        _cargando = true;
        foreach (var t in Enum.GetValues<TipoCodigo>())
            _datos[t] = _servicio.ObtenerNomenclatura(t);

        _tipoActual = TipoCodigo.Venta;
        cboTipo.SelectedItem = _tipoActual;
        Mostrar(_tipoActual);
        _cargando = false;
        ActualizarVista();
    }

    public override string? Validar()
    {
        Volcar(_tipoActual);
        var invalidos = Path.GetInvalidFileNameChars(); // los códigos pueden terminar en nombres de archivo/PDF
        foreach (var (tipo, n) in _datos)
            if (n.Prefijo.IndexOfAny(invalidos) >= 0 || (n.Separador ?? "").IndexOfAny(invalidos) >= 0)
                return $"La nomenclatura de {tipo} tiene caracteres no permitidos (\\ / : * ? \" < > |).";
        return null;
    }

    public override void Guardar(LoteConfiguracion lote)
    {
        Volcar(_tipoActual);
        foreach (var (tipo, n) in _datos)
            lote.Texto(ConfiguracionClaves.Para(tipo), n);
    }

    private void CambioTipo(object? sender, EventArgs e)
    {
        if (_cargando || cboTipo.SelectedItem is not TipoCodigo nuevo) return;
        Volcar(_tipoActual);
        _tipoActual = nuevo;
        _cargando = true;
        Mostrar(nuevo);
        _cargando = false;
        ActualizarVista();
    }

    private void CambioCampo(object? sender, EventArgs e)
    {
        nudPuntoVenta.Enabled = chkPuntoVenta.Checked;
        if (!_cargando)
        {
            ActualizarVista();
            MarcarModificada();
        }
    }

    private void Mostrar(TipoCodigo tipo)
    {
        var n = _datos[tipo];
        txtPrefijo.Text = n.Prefijo;
        txtSeparador.Text = n.Separador;
        nudDigitos.Value = Math.Clamp(n.Digitos, 1, 12);
        chkAnio.Checked = n.IncluirAnio;
        chkPuntoVenta.Checked = n.IncluirPuntoVenta;
        nudPuntoVenta.Value = Math.Clamp(n.PuntoVenta, 1, 9999);
        nudPuntoVenta.Enabled = n.IncluirPuntoVenta;
    }

    private NomenclaturaCodigoDTO Leer() => new()
    {
        Prefijo = txtPrefijo.Text.Trim(),
        Separador = txtSeparador.Text,
        Digitos = (int)nudDigitos.Value,
        IncluirAnio = chkAnio.Checked,
        IncluirPuntoVenta = chkPuntoVenta.Checked,
        PuntoVenta = (int)nudPuntoVenta.Value
    };

    private void Volcar(TipoCodigo tipo) => _datos[tipo] = Leer();

    private void ActualizarVista()
    {
        var n = Leer();
        lblVista.Text = $"{n.Formatear(1, DateTime.Today)}    {n.Formatear(125, DateTime.Today)}";
    }
}