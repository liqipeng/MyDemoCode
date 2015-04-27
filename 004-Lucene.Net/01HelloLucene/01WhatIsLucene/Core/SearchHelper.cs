using Lucene.Net.Analysis;
using Lucene.Net.Documents;
using Lucene.Net.Index;
using Lucene.Net.QueryParsers;
using Lucene.Net.Search;
using Lucene.Net.Store;
using PanGu.HighLight;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _01WhatIsLucene.Core
{
    class SearchHelper
    {
        private string _indexerFolder;

        public SearchHelper(string indexFolder)
        {
            this._indexerFolder = indexFolder;
        }

        public void Search(Analyzer analyzer, string keyword)
        {
            if (string.IsNullOrEmpty(keyword))
            {
                throw new ArgumentException("content");
            }

            // 计时
            Stopwatch watch = new Stopwatch();
            watch.Start();

            // 设置
            int numHits = 10;
            bool docsScoredInOrder = true;
            bool isReadOnly = true;

            // 创建Searcher
            FSDirectory fsDir = new SimpleFSDirectory(new DirectoryInfo(_indexerFolder));
            IndexSearcher indexSearcher = new IndexSearcher(IndexReader.Open(fsDir, isReadOnly));

            TopScoreDocCollector collector = TopScoreDocCollector.Create(numHits, docsScoredInOrder);
            QueryParser parser = new QueryParser(Lucene.Net.Util.Version.LUCENE_30, Config.Field_Content, analyzer);
            Query query = parser.Parse(keyword);

            indexSearcher.Search(query, collector);

            //Console.WriteLine(collector.TotalHits);
            var result = collector.TopDocs().ScoreDocs;

            watch.Stop();
            Console.WriteLine("总共耗时{0}毫秒", watch.ElapsedMilliseconds);
            Console.WriteLine("总共找到{0}个文件", result.Count());

            foreach (var docs in result)
            {
                Document doc = indexSearcher.Doc(docs.Doc);
                Console.WriteLine("得分：{0}，文件名：{1}", docs.Score, doc.Get(Config.Field_Name));
            }

            indexSearcher.Dispose();

            //BooleanQuery booleanQuery = new BooleanQuery();

            //QueryParser parser1 = new QueryParser(Lucene.Net.Util.Version.LUCENE_30, Config.Field_Path, analyzer);
            //Query query1 = parser1.Parse(path);
            //parser1.DefaultOperator = QueryParser.Operator.AND;
            //booleanQuery.Add(query1, Occur.MUST);

            ////QueryParser parser = new QueryParser(Lucene.Net.Util.Version.LUCENE_30, Config.Field_Content, analyzer);
            ////Query query = parser.Parse(content);
            ////parser.DefaultOperator = QueryParser.Operator.AND;
            ////booleanQuery.Add(query, Occur.MUST);



            ////var queryParserFilePath = new MultiFieldQueryParser(Lucene.Net.Util.Version.LUCENE_30, new string[] { Config.Field_FilePath }, analyzer);
            ////query.Add(queryParserFilePath.Parse(path), Occur.SHOULD);

            ////var queryParserContent = new MultiFieldQueryParser(Lucene.Net.Util.Version.LUCENE_30, new string[] { Config.Field_Content}, analyzer);
            ////query.Add(queryParserContent.Parse(content), Occur.MUST);

            //FSDirectory fsDir = new SimpleFSDirectory(new DirectoryInfo(_indexerFolder));
            //bool isReadOnly = true;
            //IndexSearcher indexSearcher = new IndexSearcher(IndexReader.Open(fsDir, isReadOnly));
            ////TopDocs result = indexSearcher.Search(booleanQuery, 10);
            //Sort sort = new Sort(new SortField(Config.Field_Path, SortField.STRING, true));
            //var result = indexSearcher.Search(booleanQuery, (Filter)null, 10 * 1, sort);

        }
    }
}
