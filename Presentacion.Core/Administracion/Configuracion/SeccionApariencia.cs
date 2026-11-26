using AccesoDatos.Config;
using Servicios.LogicaNegocio.Configuracion;
using Presentacion.FBase.Helpers;

namespace Presentacion.Core.Configuracion;

public sealed class SeccionApariencia : SeccionBase
{
    private readonly ComboBox cboTema = new() { DropDownStyle = ComboBoxStyle.DropDownList, Location = new Point(0, 20), Size = new Size(220, 25) };
    private readonly Panel pnlMuestras = new() { Location = new Point(0, 90), Size = new Size(760, 80) };
    private TipoTema _elegido;

    public override SeccionConfiguracion Clave => SeccionConfiguracion.Apariencia;
    public override string Descripcion => "Tema de colores (se guarda en esta PC). Las ventanas ya abiertas lo toman al reabrirlas.";

    public SeccionApariencia()
    {
        cboTema.DataSource = Enum.GetValues<TipoTema>();
        cboTema.SelectedIndexChanged += (_, _) => { if (cboTema.SelectedItem is TipoTema t) DibujarMuestras(t); };
        Controls.AddRange(new Control[] { Etiqueta("Tema", 0, 0), cboTema, Etiqueta("Paleta", 0, 68), pnlMuestras });
    }

    public override void Cargar()
    {
        cboTema.SelectedItem = TemaSistema.Actual;
        DibujarMuestras(TemaSistema.Actual);
    }

    // El tema es una preferencia de esta PC: va al config.json (ConfigManager), no a la base.
    public override void Guardar(LoteConfiguracion lote) =>
        _elegido = cboTema.SelectedItem is TipoTema t ? t : TemaSistema.Actual;

    // Se ejecuta recién cuando la base ya confirmó.
    public override void DespuesDeGuardar()
    {
        ConfigManager.Config.Apariencia.Tema = _elegido.ToString();
        ConfigManager.Guardar();
        TemaSistema.Aplicar(_elegido);
    }

    private void DibujarMuestras(TipoTema tema)
    {
        pnlMuestras.Controls.Clear();
        int x = 0;
        foreach (var (nombre, color) in TemaSistema.Muestras(tema))
        {
            pnlMuestras.Controls.Add(new Panel { BackColor = color, BorderStyle = BorderStyle.FixedSingle, Location = new Point(x, 0), Size = new Size(80, 40) });
            pnlMuestras.Controls.Add(new Label { Text = nombre, AutoSize = true, Font = new Font("Segoe UI", 8F), Location = new Point(x, 46) });
            x += 90;
        }
    }
}