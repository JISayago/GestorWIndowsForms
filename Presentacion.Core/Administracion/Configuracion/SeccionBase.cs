using Servicios.Helpers.Sistema;
using Servicios.LogicaNegocio.Configuracion;

namespace Presentacion.Core.Configuracion;

/// <summary>Para sumar una pantalla de configuración: 1) valor en este enum, 2) un SeccionBase, 3) una línea en RegistroSecciones.</summary>
public enum SeccionConfiguracion
{
    Negocio,
    Nomenclatura,
    Apariencia
    // Futuras: Impresion, Impuestos, Backups, Notificaciones...
}

public sealed record DefinicionSeccion(SeccionConfiguracion Clave, string Titulo, Func<SeccionBase> Crear);

public static class RegistroSecciones
{
    // El orden de esta lista es el orden del menú lateral.
    public static IReadOnlyList<DefinicionSeccion> Todas { get; } = new List<DefinicionSeccion>
    {
        new(SeccionConfiguracion.Negocio,      "Negocio y comprobantes", () => new SeccionNegocio()),
        new(SeccionConfiguracion.Nomenclatura, "Nomenclatura de códigos", () => new SeccionNomenclatura()),
        new(SeccionConfiguracion.Apariencia,   "Apariencia", () => new SeccionApariencia()),
    };
}

public abstract class SeccionBase : UserControl
{
    protected SeccionBase()
    {
        Dock = DockStyle.Fill;
        Font = new Font("Segoe UI", 9.75F);
    }
    public event EventHandler? Modificada;
    protected void MarcarModificada() => Modificada?.Invoke(this, EventArgs.Empty);
    public virtual bool DetectaCambiosAutomaticamente => true;
    public abstract SeccionConfiguracion Clave { get; }
    public abstract string Descripcion { get; }

    /// <summary>Lee la configuración actual y la vuelca en los controles.</summary>
    public abstract void Cargar();

    /// <summary>Devuelve el mensaje de error, o null si todo está bien.</summary>
    public virtual string? Validar() => null;

    /// <summary>Agrega al lote lo que va a la base. No escribe nada todavía.</summary>
    public abstract void Guardar(LoteConfiguracion lote);

    /// <summary>Se ejecuta recién cuando la transacción de base ya se confirmó (config.json, eventos, etc.).</summary>
    public virtual void DespuesDeGuardar() { }

    protected static Label Etiqueta(string texto, int x, int y) =>
        new() { Text = texto, AutoSize = true, Location = new Point(x, y) };
}