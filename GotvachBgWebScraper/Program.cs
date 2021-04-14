using System;
using System.Threading.Tasks;
using AngleSharp;
using AngleSharp.Html.Parser;

namespace GotvachBgWebScraper
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var config = Configuration.Default;
            var context = BrowsingContext.New(config);

            //Source to be parsed
            var source = "https://recepti.gotvach.bg/r-174518-%D0%9A%D1%80%D0%B5%D1%85%D0%BA%D0%BE_%D0%B0%D0%B3%D0%BD%D0%B5%D1%88%D0%BA%D0%BE_%D1%81%D1%8A%D1%81_%D0%B7%D0%B5%D0%BB%D0%B5%D0%BD_%D0%BB%D1%83%D0%BA_%D0%B7%D0%B0_%D0%92%D0%B5%D0%BB%D0%B8%D0%BA%D0%B4%D0%B5%D0%BD";

            //Create a virtual request to specify the document to load (here from our fixed string)
            var document = await context.OpenAsync(source);

            //Do something with document like the following
            Console.WriteLine("Serializing the (original) document:");
            Console.WriteLine(document.DocumentElement.OuterHtml);
        }
    }
}
