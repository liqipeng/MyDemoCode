using _01WhatIsLucene.Core;
using Lucene.Net.Analysis.PanGu;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _01WhatIsLucene
{
    class Program
    {
        static void Main(string[] args)
        {
            Index();
            Search();

            Console.ReadKey();
        }

        private static void Menu()
        {
            bool isContinue = true;

            while (isContinue)
            {
                Console.WriteLine("Press key to enter: ");
                Console.WriteLine("A -- Index");
                Console.WriteLine("B -- Search");
                switch (Console.ReadKey().Key)
                {
                    case ConsoleKey.A:
                        {
                            Index();
                            break;
                        }
                    case ConsoleKey.B:
                        {
                            Search();
                            break;
                        }
                    default:
                        {
                            isContinue = false;
                            Console.WriteLine("Press any key to quit");
                            break;
                        }
                }
            }
        }

        private static void Index() 
        {
            IndexHelper indexHelper = new IndexHelper(Config.IndexFolder, Config.TextFilesFolder);
            indexHelper.CreateIndex(new PanGuAnalyzer());
            Console.WriteLine("索引完成！");
        }

        private static void Search() 
        {
            SearchHelper searchHelper = new SearchHelper(Config.IndexFolder);
            searchHelper.Search(new PanGuAnalyzer(), "框架");
        }
    }
}
