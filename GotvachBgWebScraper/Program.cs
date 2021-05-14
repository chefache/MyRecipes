using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using AngleSharp;
using AngleSharp.Dom;

namespace ReceptiteBgWebScraper
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var url = "https://matekitchen.com/recipes/zapekanka-s-brokoli-i-pasta/";
            var config = Configuration.Default.WithDefaultLoader();
            var context = BrowsingContext.New(config);
            var document = await context.OpenAsync(url);

            // Get Categories

            // Get Recipe name
            var recipeName = GetInnerHtml(document, ".middle > .box").ToList();
            Console.WriteLine(string.Join("", recipeName));


            // Get Instructions
            var instructions = GetInnerHtml(document, ".recipe_step").ToList();
            Console.WriteLine(string.Join("", instructions));

            // Get Preparation time


            // Get Cpmplicity


            // Get Portions count


            // Get Image url


            // Get ingredients
            var ingredientsNames = GetInnerHtml(document, ".recipe_step").ToList();
            Console.WriteLine(string.Join("", ingredientsNames));

        }

        public static IEnumerable<string> GetInnerHtml(IDocument document, string className)
        {
            var items = document.QuerySelectorAll(className);
            var list = new string[items.Length];
            for (int i = 0; i < items.Length; i++)
            {
                list[i] = items[i].InnerHtml;
            }
            return list;
        }
    }
}
