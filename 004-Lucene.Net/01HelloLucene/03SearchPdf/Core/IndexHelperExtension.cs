using Lucene.Net.Analysis.PanGu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _03SearchPdf.Core
{
    public static class IndexHelperExtension
    {
        public static void CreateIndex(this IndexHelper indexHelper) 
        {
            indexHelper.CreateIndex(new PanGuAnalyzer());
        }
    }
}
