using PdfiumViewer;
using PdfSharp.Pdf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Presentacion.Core.Herramientas
{
    public partial class FVisorPDF : Form
    {
        public FVisorPDF(string rutaPdf)
        {
            InitializeComponent();

            var viewer = new PdfViewer
            {
                Dock = DockStyle.Fill
            };

            Controls.Add(viewer);

            viewer.Document = PdfiumViewer.PdfDocument.Load(rutaPdf);
            viewer.ZoomMode = PdfViewerZoomMode.FitWidth;
        }
    }
}
