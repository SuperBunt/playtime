using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Tables;
using MigraDoc.Rendering;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Web;

namespace MockInterview

{
    public class PdfGenerator
    {
        public Document Migradoc { get; set; }
        public PdfDocument PdfDoc { get; set; }
        public House home { get; private set; }

        public PdfGenerator()
        {
            Migradoc = new Document();
            PdfDoc = new PdfDocument();
        }


        public void GeneratePdf(House details)
        {
            home = details;
            var folder = @"C:\Users\pec\Documents\My Received Files\Daft";
            string filename = CleanInput(home.Address) + ".pdf";
            var target = Path.Combine(folder, filename);

            //PdfPage page = document.AddPage();
            DefineStyles();

            BuildPages();
            const bool unicode = false;
            const PdfFontEmbedding embedding = PdfFontEmbedding.Always;

            PdfDocumentRenderer pdfRenderer = new PdfDocumentRenderer();
            pdfRenderer.Document = Migradoc;
            pdfRenderer.RenderDocument();
            // Create or Replace the file
            try
            {

                // Delete the file if it exists.
                if (File.Exists(target))
                {
                    // Note that no lock is put on the
                    // file and the possibility exists
                    // that another process could do
                    // something with it between
                    // the calls to Exists and Delete.
                    File.Delete(target);
                }

                // Create the file.
                
                using (FileStream fs = File.Create(target))
                {
                    File.SetAttributes(target, FileAttributes.Normal);
                    pdfRenderer.PdfDocument.Save(fs, true);
                    //Process.Start(target);
                }
                
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            
        }

        private void BuildPages()
        {
            PdfPage page = PdfDoc.AddPage();
            Section section = Migradoc.AddSection();
            Paragraph paragraph = section.AddParagraph(home.Address, "Heading1");

            Migradoc.LastSection.AddParagraph(home.Price, "Heading2");

            Migradoc.LastSection.AddParagraph(home.BriefFeatures.ToString(), "Heading3");

            //AddImage();

            Migradoc.LastSection.AddParagraph(home.Description, "Normal");
            PopulateImages();
        }

        private void PopulateImages()
        {

            Table table = Migradoc.LastSection.AddTable();
            table.Borders.Visible = true;
            const int numColumns = 3;
            Column column = table.AddColumn();
            column.Format.Alignment = ParagraphAlignment.Center;
            table.AddColumn();
            table.AddColumn();
            int numRows = (int)Math.Ceiling((double)home.Photos.Count / numColumns);
            table.Rows.Height = 85;
            Row row = new Row();
            int index = 0;
            for (int i = 0; i < numRows; i++)
            {
                row = table.AddRow();
                for (int j = 0; j < numColumns; j++)
                {
                    if (index != home.Photos.Count - 1)
                    {
                        string imageFilename = LoadImage(string.Format(@"{0}", home.Photos[index]));
                        row.Cells[j].AddImage(imageFilename);
                        index++;
                    }
                }
            }
        }

        private void AddImage(string src)
        {
            //string imageFilename = LoadImage(string.Format(@"{0}",home.MainPhoto));
            string imageFilename = LoadImage(string.Format(@"{0}", src));
            MigraDoc.DocumentObjectModel.Shapes.Image image = Migradoc.LastSection.AddImage(imageFilename);
            image.Width = "6cm";
            image.LockAspectRatio = true;      
        }

        static double A4Width = XUnit.FromCentimeter(21).Point;
        static double A4Height = XUnit.FromCentimeter(29.7).Point;

        public static void DefineParagraphs(Document document)
        {
            Paragraph paragraph = document.LastSection.AddParagraph("Paragraph Layout Overview", "Heading1");
            paragraph.AddBookmark("Paragraphs");

            //DemonstrateAlignment(document);
            //DemonstrateIndent(document);
            //DemonstrateFormattedText(document);
            //DemonstrateBordersAndShading(document);
        }
        /// <summary>
        /// Defines the styles used in the document.
        /// </summary>
        public void DefineStyles()
        {
            // Get the predefined style Normal.
            Style style = Migradoc.Styles["Normal"];
            // Because all styles are derived from Normal, the next line changes the 
            // font of the whole document. Or, more exactly, it changes the font of
            // all styles and paragraphs that do not redefine the font.
            style.Font.Name = "Arial";
            style.Font.Color = Colors.DarkGray;

            // Heading1 to Heading9 are predefined styles with an outline level. An outline level
            // other than OutlineLevel.BodyText automatically creates the outline (or bookmarks) 
            // in PDF.

            style = Migradoc.Styles["Heading1"];
            style.ParagraphFormat.Alignment = ParagraphAlignment.Right;
            style.Font.Name = "Tahoma";
            style.Font.Size = 14;
            style.Font.Bold = true;
            style.Font.Color = Colors.DarkBlue;
            style.ParagraphFormat.PageBreakBefore = true;
            style.ParagraphFormat.SpaceAfter = 6;

            style = Migradoc.Styles["Heading2"];
            style.Font.Size = 12;
            style.Font.Bold = true;
            style.ParagraphFormat.Alignment = ParagraphAlignment.Right;
            style.ParagraphFormat.PageBreakBefore = false;
            style.ParagraphFormat.SpaceBefore = 6;
            style.ParagraphFormat.SpaceAfter = 6;

            style = Migradoc.Styles["Heading3"];
            style.Font.Size = 10;
            style.Font.Bold = true;
            style.Font.Italic = true;
            style.ParagraphFormat.SpaceBefore = 6;
            style.ParagraphFormat.SpaceAfter = 3;

            style = Migradoc.Styles[StyleNames.Header];
            style.ParagraphFormat.AddTabStop("16cm", TabAlignment.Right);

            style = Migradoc.Styles[StyleNames.Footer];
            style.ParagraphFormat.AddTabStop("8cm", TabAlignment.Center);

            // Create a new style called TextBox based on style Normal
            style = Migradoc.Styles.AddStyle("TextBox", "Normal");
            style.ParagraphFormat.Alignment = ParagraphAlignment.Justify;
            style.ParagraphFormat.Borders.Width = 2.5;
            style.ParagraphFormat.Borders.Distance = "3pt";
            style.ParagraphFormat.Shading.Color = Colors.SkyBlue;

            // Create a new style called TOC based on style Normal
            style = Migradoc.Styles.AddStyle("TOC", "Normal");
            style.ParagraphFormat.AddTabStop("16cm", TabAlignment.Right, TabLeader.Dots);
            style.ParagraphFormat.Font.Color = Colors.Blue;
        }

        static string CleanInput(string strIn)
        {
            // Replace invalid characters with empty strings.
            try
            {
                return Regex.Replace(strIn, @"[^\w\.@-]", "",
                                     RegexOptions.None, TimeSpan.FromSeconds(1.5));
            }
            // If we timeout when replacing invalid characters, 
            // we should return Empty.
            catch (RegexMatchTimeoutException)
            {
                return String.Empty;
            }
        }

        private string LoadImage(string name)
        {
            using (var webClient = new WebClient())
            {
                byte[] imageBytes = webClient.DownloadData(name); // do something with imageBytes` 
                return "base64:" + Convert.ToBase64String(imageBytes);
            }
        }

        

        static string MigradocFileNameFromByteArray(byte[] image)
        {
            return "base64:" + Convert.ToBase64String(image);
        }

    }
}
