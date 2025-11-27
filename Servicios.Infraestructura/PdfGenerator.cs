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
            // Carpeta en el escritorio
            var escritorio = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            var carpeta = Path.Combine(escritorio, "ComprobantesPdf");

            // Crear la carpeta si no existe
            if (!Directory.Exists(carpeta))
                Directory.CreateDirectory(carpeta);

            // Archivo final
            var ruta = Path.Combine(carpeta, $"Factura_{venta.NumeroVenta}.pdf");

            // Generación PDF
            var doc = new Document();
            var sec = doc.AddSection();
            sec.AddParagraph($"Comprobante Nº {venta.NumeroVenta}");

            var renderer = new PdfDocumentRenderer(true);
            renderer.Document = doc;
            renderer.RenderDocument();
            renderer.PdfDocument.Save(ruta);

            return ruta;
        }

    }
}
