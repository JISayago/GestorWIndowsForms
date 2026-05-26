using AccesoDatos.Entidades;
using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Tables;
using MigraDoc.Rendering;
using Servicios.Infraestructura;
using System;
using System.IO;
using System.Linq;

public class PdfGenerator : IPdfGenerator
{
    // =========================
    // PUBLIC METHODS
    // =========================
    
    public string GenerarVenta(Venta venta)
    {
        var ruta = ObtenerRutaPdf("Ventas", "Ventas Realizadas", venta.NumeroVenta.ToString());

        var doc = CrearDocumentoBase($"Factura {venta.NumeroVenta}");
        var section = doc.AddSection();

        AgregarHeader(section, "FACTURA");

        AgregarDatosVenta(section, venta);

        AgregarDetalleVenta(section, venta);

        AgregarTotalesVenta(section, venta);

        AgregarPagos(section, venta.VentaPagoDetalles);

        AgregarPie(section);

        Guardar(doc, ruta);

        return ruta;
    }

    public string GenerarVentaLibre(VentaLibre venta)
    {
        var ruta = ObtenerRutaPdf("Ventas Libres","Todas", venta.NumeroVenta.ToString());

        var doc = CrearDocumentoBase($"Venta Libre {venta.NumeroVenta}");
        var section = doc.AddSection();

        AgregarHeader(section, "VENTA LIBRE");

        AgregarDatosVentaLibre(section, venta);

        AgregarTotalesVentaLibre(section, venta);

        AgregarPagos(section, venta.VentaPagoDetalles);

        AgregarPie(section);

        Guardar(doc, ruta);

        return ruta;
    }

    public string GenerarGasto(Gasto gasto)
    {
        var ruta = ObtenerRutaPdf("Gastos", "Gastos Realizados", gasto.NumeroGasto);

        var doc = CrearDocumentoBase($"Gasto {gasto.NumeroGasto}");
        var section = doc.AddSection();

        AgregarHeader(section, "COMPROBANTE DE GASTO");

        AgregarDatosGasto(section, gasto);

        AgregarTotalesGasto(section, gasto);

        AgregarPagos(section, gasto.VentaPagoDetalles);

        AgregarPie(section, "Comprobante de gasto");

        Guardar(doc, ruta);

        return ruta;
    }

    public string GenerarGastoAnulado(Gasto gasto)
    {
        var ruta = ObtenerRutaPdf("Gastos", "Gastos Anulados", gasto.NumeroGasto);

        var doc = CrearDocumentoBase($"Gasto Anulado {gasto.NumeroGasto}");
        var section = doc.AddSection();

        AgregarHeader(section, "GASTO ANULADO");

        AgregarDatosGasto(section, gasto);

        // 🔥 aclaración clave
        section.AddParagraph("Este gasto fue anulado luego de haber sido pagado.", "Subtitulo");
        section.AddParagraph("\n");

        // Totales (lo que se pagó originalmente)
        var tabla = section.AddTable();
        tabla.AddColumn("12cm");
        tabla.AddColumn("4cm");

        AddRow(tabla, "Monto original:", gasto.MontoTotal);
        AddRow(tabla, "Monto pagado:", gasto.MontoPagado);
        AddRow(tabla, "Monto revertido:", gasto.MontoPagado, true);

        section.AddParagraph("\n");

        // Pagos
        AgregarPagos(section, gasto.VentaPagoDetalles, "Pagos revertidos");

        AgregarPie(section, "Comprobante de anulación");

        Guardar(doc, ruta);

        return ruta;
    }

    public string GenerarCancelacionVenta(Venta venta)
    {
        var ruta = ObtenerRutaPdf("Ventas", "Ventas Canceladas", venta.NumeroVenta.ToString());

        var doc = CrearDocumentoBase($"Cancelación {venta.NumeroVenta}");
        var section = doc.AddSection();

        AgregarHeader(section, "CANCELACIÓN DE VENTA");

        AgregarDatosVenta(section, venta);

        AgregarDetalleVenta(section, venta);

        AgregarTotalesVenta(section, venta, "TOTAL CANCELADO:");

        AgregarPagos(section, venta.VentaPagoDetalles, "Reverso de pagos");

        AgregarPie(section, "Operación anulada");

        Guardar(doc, ruta);

        return ruta;
    }

    // =========================
    // PATH / FILE
    // =========================

    private string ObtenerRutaPdf(string modulo, string tipo, string numero)
    {
        var escritorio = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

        var año = DateTime.Now.Year.ToString();
        var mes = DateTime.Now.Month.ToString("D2");

        var carpeta = Path.Combine(
            escritorio,
            "ComprobantesPdf",
            modulo,     // Ventas / Gastos
            tipo,       // Realizados / Anulados / etc
            año,
            mes
        );

        if (!Directory.Exists(carpeta))
            Directory.CreateDirectory(carpeta);

        var nombreBase = $"{tipo}_{numero}.pdf";

        return GenerarNombreUnico(carpeta, nombreBase);
    }

    private string GenerarNombreUnico(string carpeta, string nombreBase)
    {
        var ruta = Path.Combine(carpeta, nombreBase);
        int i = 1;

        while (File.Exists(ruta))
        {
            var nombre = Path.GetFileNameWithoutExtension(nombreBase);
            var ext = Path.GetExtension(nombreBase);

            ruta = Path.Combine(carpeta, $"{nombre}_{i}{ext}");
            i++;
        }

        return ruta;
    }

    // =========================
    // BASE PDF
    // =========================

    private Document CrearDocumentoBase(string titulo)
    {
        var doc = new Document();
        doc.Info.Title = titulo;

        var normal = doc.Styles["Normal"];
        normal.Font.Name = "Arial";
        normal.Font.Size = 10;

        var tituloStyle = doc.Styles.AddStyle("Titulo", "Normal");
        tituloStyle.Font.Size = 16;
        tituloStyle.Font.Bold = true;

        var subtitulo = doc.Styles.AddStyle("Subtitulo", "Normal");
        subtitulo.Font.Bold = true;

        return doc;
    }

    private void Guardar(Document doc, string ruta)
    {
        var renderer = new PdfDocumentRenderer(true) { Document = doc };
        renderer.RenderDocument();
        renderer.PdfDocument.Save(ruta);
    }

    // =========================
    // BLOQUES REUTILIZABLES
    // =========================

    private void AgregarHeader(Section section, string titulo)
    {
        var p = section.AddParagraph("MI NEGOCIO\n", "Titulo");
        p.Format.Alignment = ParagraphAlignment.Center;

        var sub = section.AddParagraph(titulo + "\n\n");
        sub.Format.Alignment = ParagraphAlignment.Center;
    }

    private void AgregarDatosVenta(Section section, Venta venta, bool consumidorFinal = false)
    {
        var tabla = section.AddTable();
        tabla.AddColumn("8cm");
        tabla.AddColumn("8cm");

        var r1 = tabla.AddRow();
        r1.Cells[0].AddParagraph($"N°: {venta.NumeroVenta}");
        r1.Cells[1].AddParagraph($"Fecha: {venta.FechaVenta:dd/MM/yyyy}");

        var r2 = tabla.AddRow();
        r2.Cells[0].AddParagraph($"Cliente: {(consumidorFinal ? "Consumidor Final" : venta.Cliente?.Persona?.Nombre ?? "")}");
        r2.Cells[1].AddParagraph($"Vendedor: {venta.Vendedor?.Persona?.Apellido ?? ""}");

        section.AddParagraph("\n");
    }

    private void AgregarDatosGasto(Section section, Gasto gasto)
    {
        var tabla = section.AddTable();
        tabla.AddColumn("8cm");
        tabla.AddColumn("8cm");

        var r1 = tabla.AddRow();
        r1.Cells[0].AddParagraph($"N°: {gasto.NumeroGasto}");
        r1.Cells[1].AddParagraph($"Fecha: {(gasto.FechaGasto ?? gasto.FechaRegistro):dd/MM/yyyy}");

        var r2 = tabla.AddRow();
        r2.Cells[0].AddParagraph($"Empleado: {gasto.Empleado?.Persona?.Apellido ?? ""}");
        r2.Cells[1].AddParagraph($"Estado: {gasto.EstadoGasto}");

        section.AddParagraph("\n");

        section.AddParagraph("Detalle", "Subtitulo");
        section.AddParagraph(gasto.Detalle ?? "Sin detalle");

        section.AddParagraph("\n");
    }

    private void AgregarDetalleVenta(Section section, Venta venta)
    {
        var tabla = section.AddTable();
        tabla.Borders.Width = 0.75;

        tabla.AddColumn("8cm");
        tabla.AddColumn("2cm");
        tabla.AddColumn("3cm");
        tabla.AddColumn("3cm");

        var header = tabla.AddRow();
        header.Shading.Color = Colors.LightGray;
        header.Format.Font.Bold = true;

        header.Cells[0].AddParagraph("Producto");
        header.Cells[1].AddParagraph("Cant");
        header.Cells[2].AddParagraph("Precio");
        header.Cells[3].AddParagraph("Subtotal");

        foreach (var d in venta.DetallesVentas)
        {
            var row = tabla.AddRow();

            row.Cells[0].AddParagraph(d.Producto?.Descripcion ?? d.Descripcion);
            row.Cells[1].AddParagraph(d.Cantidad.ToString("0.##"));
            row.Cells[2].AddParagraph(d.PrecioUnitarioFinal.ToString("C"));
            row.Cells[3].AddParagraph(d.Subtotal.ToString("C"));
        }

        section.AddParagraph("\n");
    }

    private void AgregarTotalesVenta(Section section, Venta venta, string labelTotal = "TOTAL:")
    {
        var tabla = section.AddTable();
        tabla.AddColumn("12cm");
        tabla.AddColumn("4cm");

        AddRow(tabla, "Subtotal:", venta.TotalSinDescuento);
        AddRow(tabla, "Descuento:", venta.Descuento);
        AddRow(tabla, labelTotal, venta.Total, true);

        section.AddParagraph("\n");
    }

    private void AgregarTotalesGasto(Section section, Gasto gasto)
    {
        var tabla = section.AddTable();
        tabla.AddColumn("12cm");
        tabla.AddColumn("4cm");

        AddRow(tabla, "Total:", gasto.MontoTotal, true);
        AddRow(tabla, "Pagado:", gasto.MontoPagado);

        var saldo = gasto.MontoTotal - gasto.MontoPagado;
        if (saldo > 0)
            AddRow(tabla, "Saldo:", saldo, true);

        section.AddParagraph("\n");
    }

    private void AgregarPagos(Section section, ICollection<VentaPagoDetalle> pagos, string titulo = "Formas de Pago")
    {
        section.AddParagraph(titulo, "Subtitulo");

        if (pagos != null && pagos.Any())
        {
            foreach (var p in pagos)
            {
                section.AddParagraph($"- {p.TipoPago?.Nombre}: {p.Monto.ToString("C")}");
            }
        }
        else
        {
            section.AddParagraph("- Sin pagos");
        }

        section.AddParagraph("\n");
    }

    private void AgregarPie(Section section, string texto = "Gracias por su compra")
    {
        var pie = section.AddParagraph(texto);
        pie.Format.Alignment = ParagraphAlignment.Center;
        pie.Format.Font.Italic = true;
    }

    private void AddRow(Table t, string label, decimal value, bool bold = false)
    {
        var r = t.AddRow();
        r.Cells[0].AddParagraph(label).Format.Alignment = ParagraphAlignment.Right;

        var p = r.Cells[1].AddParagraph(value.ToString("C"));
        p.Format.Alignment = ParagraphAlignment.Right;

        if (bold)
            p.Format.Font.Bold = true;
    }

    private void AgregarDatosVentaLibre(Section section, VentaLibre venta)
    {
        var tabla = section.AddTable();
        tabla.AddColumn("8cm");
        tabla.AddColumn("8cm");

        var r1 = tabla.AddRow();
        r1.Cells[0].AddParagraph($"N°: {venta.NumeroVenta}");
        r1.Cells[1].AddParagraph($"Fecha: {venta.FechaVenta:dd/MM/yyyy}");

        var r2 = tabla.AddRow();
        r2.Cells[0].AddParagraph($"Cliente: Consumidor Final");
        r2.Cells[1].AddParagraph($"Vendedor: {venta.Vendedor?.Persona?.Apellido ?? ""}");

        section.AddParagraph("\n");

        // 🔥 Detalle directo (no hay grilla)
        section.AddParagraph("Detalle", "Subtitulo");
        section.AddParagraph(venta.Detalle ?? "Sin detalle");

        section.AddParagraph("\n");
    }
    private void AgregarTotalesVentaLibre(Section section, VentaLibre venta)
    {
        var tabla = section.AddTable();
        tabla.AddColumn("12cm");
        tabla.AddColumn("4cm");

        AddRow(tabla, "Total:", venta.Total, true);
        AddRow(tabla, "Pagado:", venta.MontoPagado);

        if (venta.MontoAdeudado > 0)
            AddRow(tabla, "Adeudado:", venta.MontoAdeudado, true);

        section.AddParagraph("\n");
    }
}