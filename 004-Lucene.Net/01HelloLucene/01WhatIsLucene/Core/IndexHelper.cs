using Lucene.Net.Analysis;
using Lucene.Net.Documents;
using Lucene.Net.Index;
using Lucene.Net.Store;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _01WhatIsLucene.Core
{
    class IndexHelper
    {
        private string _indexerFolder;
        private string _textFilesFolder;

        public IndexHelper(string indexFolder, string textFilesFolder)
        {
            this._indexerFolder = indexFolder;
            this._textFilesFolder = textFilesFolder;
        }

        public void CreateIndex(Analyzer analayer) 
        {
            FSDirectory fsDir = new SimpleFSDirectory(new DirectoryInfo(_indexerFolder));
            IndexWriter indexWriter = new IndexWriter(fsDir, analayer, true, Lucene.Net.Index.IndexWriter.MaxFieldLength.UNLIMITED);

            string[] files = System.IO.Directory.GetFiles(_textFilesFolder, Config.FileSearchPattern, SearchOption.AllDirectories);
            foreach (string file in files)
            {
                string name = new FileInfo(file).Name;
                string content = File.ReadAllText(file);

                Document doc = new Document();
                doc.Add(new Field(Config.Field_Path, file, Field.Store.YES, Field.Index.NOT_ANALYZED));
                doc.Add(new Field(Config.Field_Name, name, Field.Store.YES, Field.Index.ANALYZED));
                doc.Add(new Field(Config.Field_Content, content, Field.Store.NO, Field.Index.ANALYZED));

                indexWriter.AddDocument(doc);

                Console.WriteLine("{0} - {1}", file, name);
            }

            indexWriter.Optimize();
            indexWriter.Dispose();

            Console.WriteLine("File count: {0}", files.Length);
        }
    }
}
