using AccesoDatos.Entidades;
using MigraDoc.DocumentObjectModel;
using MigraDoc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios.Infraestructura
{
    public class PdfGenerator : IPdfGenerator
    {
        public string GenerarComprobante(Venta venta)
        {
            var escritorio = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            var carpeta = Path.Combine(escritorio, "ComprobantesPdf");

            if (!Directory.Exists(carpeta))
                Directory.CreateDirectory(carpeta);

            var ruta = Path.Combine(carpeta, $"Factura_{venta.NumeroVenta}.pdf");

            var doc = new Document();
            doc.Info.Title = $"Factura {venta.NumeroVenta}";

            var section = doc.AddSection();
            section.PageSetup.TopMargin = "2cm";

            // ====== ESTILOS ======
            var style = doc.Styles["Normal"];
            style.Font.Name = "Arial";
            style.Font.Size = 10;

            var titulo = doc.Styles.AddStyle("Titulo", "Normal");
            titulo.Font.Size = 16;
            titulo.Font.Bold = true;

            var subtitulo = doc.Styles.AddStyle("Subtitulo", "Normal");
            subtitulo.Font.Size = 10;
            subtitulo.Font.Bold = true;

            // ====== ENCABEZADO ======
            var pTitulo = section.AddParagraph("MI NEGOCIO\n", "Titulo");
            pTitulo.Format.Alignment = ParagraphAlignment.Center;

            section.AddParagraph("Factura\n\n").Format.Alignment = ParagraphAlignment.Center;

            // ====== DATOS DE VENTA ======
            var datos = section.AddTable();
            datos.Borders.Width = 0;
            datos.AddColumn("8cm");
            datos.AddColumn("8cm");

            var row1 = datos.AddRow();
            row1.Cells[0].AddParagraph($"Factura Nº: {venta.NumeroVenta}");
            row1.Cells[1].AddParagraph($"Fecha: {venta.FechaVenta:dd/MM/yyyy}");

            var row2 = datos.AddRow();
            row2.Cells[0].AddParagraph($"Cliente: {venta.Cliente?.Persona.Nombre ?? "Consumidor Final"}");
            row2.Cells[1].AddParagraph($"Vendedor: {venta.Vendedor?.Persona?.Apellido}");

            section.AddParagraph("\n");

            // ====== TABLA DETALLE ======
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
                row.Cells[0].AddParagraph(d.Producto.Descripcion);
                row.Cells[1].AddParagraph(d.Cantidad.ToString("0.##"));
                row.Cells[2].AddParagraph((d.Subtotal / d.Cantidad).ToString("C"));
                row.Cells[3].AddParagraph(d.Subtotal.ToString("C"));
            }

            section.AddParagraph("\n");

            // ====== TOTALES ======
            var totales = section.AddTable();
            totales.AddColumn("12cm");
            totales.AddColumn("4cm");

            void AddTotal(string texto, decimal valor, bool bold = false)
            {
                var r = totales.AddRow();
                r.Cells[0].AddParagraph(texto).Format.Alignment = ParagraphAlignment.Right;
                var p = r.Cells[1].AddParagraph(valor.ToString("C"));
                if (bold) p.Format.Font.Bold = true;
            }

            AddTotal("Subtotal:", venta.TotalSinDescuento);
            AddTotal("Descuento:", venta.Descuento);
            AddTotal("TOTAL:", venta.Total, true);

            section.AddParagraph("\n");

            // ====== PAGOS ======
            section.AddParagraph("Formas de Pago", "Subtitulo");

            foreach (var pago in venta.VentaPagoDetalles)
            {
                section.AddParagraph($"- {pago.TipoPago.Nombre}: {pago.Monto.ToString("C")}");
            }

            section.AddParagraph("\n");

            // ====== PIE ======
            var pie = section.AddParagraph("Gracias por su compra");
            pie.Format.Alignment = ParagraphAlignment.Center;
            pie.Format.Font.Italic = true;

            // ====== RENDER ======
            var renderer = new PdfDocumentRenderer(true)
            {
                Document = doc
            };

            renderer.RenderDocument();
            renderer.PdfDocument.Save(ruta);

            return ruta;
        }


    }
}
