using System;
using System.Linq;
using System.Threading.Tasks;
using AngleSharp;
using AngleSharp.Dom;

namespace ReceptiteBgWebScraper
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var url = "https://receptite.com/%D1%80%D0%B5%D1%86%D0%B5%D0%BF%D1%82%D0%B0/%D0%BF%D0%B8%D0%BB%D0%B5%D1%88%D0%BA%D0%B0-%D1%81%D1%83%D0%BF%D0%B0-%D1%81-%D1%84%D0%B8%D0%B4%D0%B5";
            var config = Configuration.Default.WithDefaultLoader();
            var context = BrowsingContext.New(config);
            var document = await context.OpenAsync(url);
            var elements = document.QuerySelectorAll("li");
            foreach (var element in elements)
            {
                Console.WriteLine(element.TextContent);
            }
        }
    }
}
