using VersOne.Epub;
using HtmlAgilityPack;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ReaderEpub_20211215
{
    public class CreateEpub
    {
        public List<string> rowsToCreate { get; set; }
        public string fileToCreate;
        public CreateEpub()
        {
            fileToCreate = string.Empty;
        }

        public void CreateEpubLaf()
        {
            rowsToCreate = new List<string>() { };

            EpubBook book = EpubReader.ReadBook(fileToCreate);
            foreach (EpubTextContentFile textContentFile in book.ReadingOrder)
            {
                PrintTextContentFile(textContentFile);                
            }

            File.WriteAllLines(fileToCreate.Replace(".epub",".epublaf"), rowsToCreate.Select(i => i.ToString()).ToArray());
        }
        private void PrintTextContentFile(EpubTextContentFile textContentFile)
        {
            HtmlDocument htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(textContentFile.Content);

            foreach (HtmlNode node in htmlDocument.DocumentNode.SelectNodes("//text()"))
            {
                string textFormatted = GetText(node.InnerText);
                if (textFormatted != string.Empty)
                {
                    rowsToCreate.Add(textFormatted);
                }
            }
        }

        private static string GetText(string innerText)
        {
            string output = innerText;
            if (output.IndexOf("<?xml ") != -1 || output.IndexOf("@page") != -1)
            {
                output = string.Empty;
            }
            output = output.Replace("\n"," ").Replace("&lt;", "").Replace("&gt;", "");

            output = output.Trim();
            return output;
        }
    }
}
