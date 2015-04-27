using System;
using System.IO;

namespace _03SearchPdf.UI
{
    class Config
    {
        public static readonly string IndexFolder = Path.Combine(new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.FullName, "IndexFiles");
        public static readonly string TextFilesFolder = Path.Combine(new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.FullName, "PdfFiles");
    }
}
