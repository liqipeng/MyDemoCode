using _02ReadPdf.Core;
using Aspose.Pdf;
using Aspose.Pdf.InteractiveFeatures;
using Aspose.Pdf.InteractiveFeatures.Annotations;
using Aspose.Pdf.Text;
using Aspose.Pdf.Text.TextOptions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _02ReadPdf
{
    class Program
    {
        static void Main(string[] args)
        {
            TestRead();

            //string outputFile = @"E:\test.txt";
            //PdfTextReader reader = new PdfTextReader(Config.TestPdf);
            //using (Stream fileStream = File.OpenWrite(outputFile))
            //{
            //    reader.ExportToText(fileStream);
            //}
            //Console.WriteLine("Export success.");

            //Search();

            //SearchWithRegularExpression();

            Console.ReadKey();
        }

        static void SearchWithRegularExpression() 
        {
            //open document
            Document pdfDocument = new Document(Config.TestPdf);

            //create TextAbsorber object to find all the phrases matching the regular expression
            TextFragmentAbsorber textFragmentAbsorber = new TextFragmentAbsorber("\\d{4}-\\d{4}"); //like 1999-2000

            //###set text search option to specify regular expression usage
            TextSearchOptions textSearchOptions = new TextSearchOptions(true);
            textFragmentAbsorber.TextSearchOptions = textSearchOptions;

            //accept the absorber for all the pages
            pdfDocument.Pages.Accept(textFragmentAbsorber);

            //get the extracted text fragments
            TextFragmentCollection textFragmentCollection = textFragmentAbsorber.TextFragments;

            //loop through the fragments
            foreach (TextFragment textFragment in textFragmentCollection)
            {
                Console.WriteLine("Text : {0} ", textFragment.Text);
                //Console.WriteLine("Position : {0} ", textFragment.Position);
                //Console.WriteLine("XIndent : {0} ", textFragment.Position.XIndent);
                //Console.WriteLine("YIndent : {0} ", textFragment.Position.YIndent);
                //Console.WriteLine("Font - Name : {0}", textFragment.TextState.Font.FontName);
                //Console.WriteLine("Font - IsAccessible : {0} ", textFragment.TextState.Font.IsAccessible);
                //Console.WriteLine("Font - IsEmbedded : {0} ", textFragment.TextState.Font.IsEmbedded);
                //Console.WriteLine("Font - IsSubset : {0} ", textFragment.TextState.Font.IsSubset);
                //Console.WriteLine("Font Size : {0} ", textFragment.TextState.FontSize);
                //Console.WriteLine("Foreground Color : {0} ", textFragment.TextState.ForegroundColor);
            } 
        }

        static void Search()
        {
            //open document
            Document pdfDocument = new Document(Config.TestPdf);

            string keyword = "pattern";

            //create TextAbsorber object to find all instances of the input search phrase
            TextFragmentAbsorber textFragmentAbsorber = new TextFragmentAbsorber(keyword);

            //accept the absorber for all the pages
            pdfDocument.Pages.Accept(textFragmentAbsorber);

            //get the extracted text fragments
            TextFragmentCollection textFragmentCollection = textFragmentAbsorber.TextFragments;

            //loop through the fragments
            foreach (TextFragment textFragment in textFragmentCollection)
            {
                Console.WriteLine("Page Number : {0} ", textFragment.Page.Number);
                //Console.WriteLine("Text : {0} ", textFragment.Text);
                //Console.WriteLine("Position : {0} ", textFragment.Position);
                //Console.WriteLine("XIndent : {0} ", textFragment.Position.XIndent);
                //Console.WriteLine("YIndent : {0} ", textFragment.Position.YIndent);
                //Console.WriteLine("Font - Name : {0}", textFragment.TextState.Font.FontName);
                //Console.WriteLine("Font - IsAccessible : {0} ", textFragment.TextState.Font.IsAccessible);
                //Console.WriteLine("Font - IsEmbedded : {0} ", textFragment.TextState.Font.IsEmbedded);
                //Console.WriteLine("Font - IsSubset : {0} ", textFragment.TextState.Font.IsSubset);
                //Console.WriteLine("Font Size : {0} ", textFragment.TextState.FontSize);
                //Console.WriteLine("Foreground Color : {0} ", textFragment.TextState.ForegroundColor);
            }
        }

        static void TestRead() 
        {
            Document pdfDocument = new Document(Config.TestPdf);

            //create TextAbsorber object to extract text
            TextAbsorber textAbsorber = new TextAbsorber();

            //accept the absorber for a particular page
            pdfDocument.Pages[104].Accept(textAbsorber);

            //get the extracted text
            string extractedText = textAbsorber.Text;

            // create a writer and open the file
            TextWriter tw = new StreamWriter(Console.OpenStandardOutput(), Encoding.Default);

            // write a line of text to the file
            tw.WriteLine(extractedText);

            // close the stream
            tw.Close();
        }
    }
}
