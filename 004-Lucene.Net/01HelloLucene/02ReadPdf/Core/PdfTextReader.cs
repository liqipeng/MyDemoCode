using Aspose.Pdf;
using Aspose.Pdf.Text;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _02ReadPdf.Core
{
    public class PdfTextReader
    {
        private string _path;

        public PdfTextReader(string path)
        {
            this._path = path;
        }

        public void ExportToText(Stream stream) 
        {
            Document pdfDocument = new Document(_path);
            TextAbsorber textAbsorber = new TextAbsorber();

            using (TextWriter tw = new StreamWriter(stream, Encoding.Default)) 
            {
                for (int i = 1; i <= pdfDocument.Pages.Count; i++)
                {
                    Page page = pdfDocument.Pages[i];
                    page.Accept(textAbsorber);
                    tw.Write(textAbsorber.Text);
                }
            }
        }
    }
}
