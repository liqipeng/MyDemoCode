using Lucene.Net.Analysis;
using Lucene.Net.Documents;
using Lucene.Net.Index;
using Lucene.Net.QueryParsers;
using Lucene.Net.Search;
using Lucene.Net.Store;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _03SearchPdf.Core
{
    public class SearchHelper : Helper
    {
        private string _indexerFolder;

        public SearchHelper(string indexFolder)
        {
            this._indexerFolder = indexFolder;
        }

        public DataTable Search(Analyzer analyzer, string keyword)
        {
            if (string.IsNullOrEmpty(keyword))
            {
                throw new ArgumentException("keyword");
            }

            // 计时
            Stopwatch watch = Stopwatch.StartNew();

            // 设置
            int numHits = 10;
            bool docsScoredInOrder = true;
            bool isReadOnly = true;

            // 创建Searcher
            FSDirectory fsDir = new SimpleFSDirectory(new DirectoryInfo(_indexerFolder));
            IndexSearcher indexSearcher = new IndexSearcher(IndexReader.Open(fsDir, isReadOnly));

            TopScoreDocCollector collector = TopScoreDocCollector.Create(numHits, docsScoredInOrder);
            QueryParser parser = new QueryParser(Lucene.Net.Util.Version.LUCENE_30, LuceneConfig.Field_ContentByPage, analyzer);
            Query query = parser.Parse(keyword);

            indexSearcher.Search(query, collector);

            //Console.WriteLine(collector.TotalHits);
            var result = collector.TopDocs().ScoreDocs;

            DataTable dt = ConvertToDataTable(indexSearcher, result);

            // dispose IndexSearcher
            indexSearcher.Dispose();

            watch.Stop();
            WriteLog("总共耗时{0}毫秒", watch.ElapsedMilliseconds);
            WriteLog("总共找到{0}个文件", result.Count());

            return dt;
        }

        private static DataTable ConvertToDataTable(IndexSearcher indexSearcher, ScoreDoc[] result)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn(LuceneConfig.Field_Path));
            dt.Columns.Add(new DataColumn(LuceneConfig.Field_FileName));
            dt.Columns.Add(new DataColumn(LuceneConfig.Field_PageNumber));
            dt.Columns.Add(new DataColumn(LuceneConfig.Field_ContentByPage));
            dt.Columns.Add(new DataColumn(LuceneConfig.Field_Score));

            foreach (ScoreDoc scoreDoc in result)
            {
                Document doc = indexSearcher.Doc(scoreDoc.Doc);
                DataRow dr = dt.NewRow();

                dr[LuceneConfig.Field_Path] = doc.Get(LuceneConfig.Field_Path);
                dr[LuceneConfig.Field_FileName] = doc.Get(LuceneConfig.Field_FileName);
                dr[LuceneConfig.Field_PageNumber] = doc.Get(LuceneConfig.Field_PageNumber);
                dr[LuceneConfig.Field_ContentByPage] = doc.Get(LuceneConfig.Field_ContentByPage);
                dr[LuceneConfig.Field_Score] = scoreDoc.Score;

                dt.Rows.Add(dr);
            }

            return dt;
        }
    }
}
