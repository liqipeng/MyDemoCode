using Lucene.Net.Analysis.PanGu;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _03SearchPdf.Core
{
    public static class SearchHelperExtension
    {
        public static DataTable Search(this SearchHelper searchHelper, string keyword)
        {
            return searchHelper.Search(new PanGuAnalyzer(), keyword);
        }
    }
}
