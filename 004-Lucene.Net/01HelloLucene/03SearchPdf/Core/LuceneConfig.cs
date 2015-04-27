using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _03SearchPdf.Core
{
    class LuceneConfig
    {
        public const string Field_Path = "Path";
        public const string Field_FileName = "FileName";
        public const string Field_PageNumber = "PageNumber";
        public const string Field_ContentByPage = "ContentByPage";
        public const string Field_Score = "Score";

        public static readonly string FileSearchPattern = "*.pdf";
        public static readonly string FileExt = ".pdf";
    }
}
