using MigraDoc.DocumentObjectModel;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;

namespace AwaitAsync
{
    internal class PdfGenerator
    {
        public PdfGenerator()
        {

        }

        //public void GetResponse(IHouse home)
        //{
            
        //    PdfDocument document = new PdfDocument();

        //    PdfPage page = document.AddPage();

        //    XFont font = new XFont("Verdana", 20, XFontStyle.BoldItalic);

        //    XGraphics gfx = XGraphics.FromPdfPage(page);

        //    gfx.DrawString("Hello, World!", font, XBrushes.Black, new XRect(0, 0, page.Width, page.Height), XStringFormats.Center);


        //    string filename = "C:/Users/pec/Documents/My Received Files/HelloWorld.pdf";

        //    document.Save(filename);

        //    Process.Start(filename);

        //}
    }
}
