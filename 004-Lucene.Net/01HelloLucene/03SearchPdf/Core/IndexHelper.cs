using Aspose.Pdf;
using Aspose.Pdf.Text;
using Lucene.Net.Analysis;
using Lucene.Net.Documents;
using Lucene.Net.Index;
using Lucene.Net.Store;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _03SearchPdf.Core
{
    public class IndexHelper : Helper
    {
        private string _indexerFolder;
        private string _textFilesFolder;
        private string _fileSearchPattern;

        public IndexHelper(string indexFolder, string textFilesFolder)
        {
            this._indexerFolder = indexFolder;
            this._textFilesFolder = textFilesFolder;
            this._fileSearchPattern = LuceneConfig.FileSearchPattern;
        }

        public void CreateIndex(Analyzer analayer)
        {
            FSDirectory fsDir = new SimpleFSDirectory(new DirectoryInfo(_indexerFolder));
            IndexWriter indexWriter = new IndexWriter(fsDir, analayer, true, Lucene.Net.Index.IndexWriter.MaxFieldLength.UNLIMITED);
            Stopwatch stopWatch = Stopwatch.StartNew();
            int analyzedCount = 0;

            string[] files = System.IO.Directory.GetFiles(_textFilesFolder, this._fileSearchPattern, SearchOption.AllDirectories);

            //统计需要索引的文件页数
            int totalPages = GetTotalPages(files);
            WriteLog("Total pages statistics takes {0}ms", stopWatch.Elapsed.Milliseconds);

            stopWatch.Restart();

            TextAbsorber textAbsorber = new TextAbsorber();
            //开始索引
            foreach (string pdfFile in files)
            {
                var fileInfo = new FileInfo(pdfFile);
                var fileName = fileInfo.Name;
                Aspose.Pdf.Document pdfDocument = new Aspose.Pdf.Document(pdfFile);

                WriteLog("Current file is {0}", pdfFile);

                //注意pdf页码从1开始
                for (int i = 1;i<=pdfDocument.Pages.Count;i++) 
                {
                    Page page = pdfDocument.Pages[i];
                    page.Accept(textAbsorber);
                    string pageContent = textAbsorber.Text;

                    Lucene.Net.Documents.Document doc = new Lucene.Net.Documents.Document();
                    doc.Add(new Field(LuceneConfig.Field_Path, pdfFile, Field.Store.YES, Field.Index.NOT_ANALYZED));
                    doc.Add(new Field(LuceneConfig.Field_FileName, fileName, Field.Store.YES, Field.Index.ANALYZED));
                    doc.Add(new Field(LuceneConfig.Field_PageNumber, i.ToString(), Field.Store.YES, Field.Index.ANALYZED));
                    doc.Add(new Field(LuceneConfig.Field_ContentByPage, pageContent, Field.Store.NO, Field.Index.ANALYZED));

                    indexWriter.AddDocument(doc);

                    analyzedCount++;

                    RaiseProgressChanged(analyzedCount * 100 / totalPages);
                }

                
            }

            indexWriter.Optimize();
            indexWriter.Dispose();

            stopWatch.Stop();
            Console.WriteLine("All completed. It takes {0}ms", stopWatch.Elapsed);
        }

        private int GetTotalPages(string[] files) 
        {
            int sum = 0;
            foreach(string pdfFile in files)
            {
                Aspose.Pdf.Document pdfDocument = new Aspose.Pdf.Document(pdfFile);
                sum += pdfDocument.Pages.Count;
            }
            return sum;
        }

    }
}
