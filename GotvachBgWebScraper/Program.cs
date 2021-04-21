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
            // [\u0000-\u024F]+
            var categories = GetInnerHtml(document, "#category_cloud-single-recipe > div").ToList();
            var textCategories = string.Join(" ", categories);
            string resultCategories = Regex.Replace(textCategories, @"[\u0000-\u024F]+", "-");
            Console.WriteLine(resultCategories);

            // Get Recipe name
            var recipeName = GetInnerHtml(document, "div.post-body > div.summary.entry-summary > h1").ToList();
            Console.WriteLine(string.Join("", recipeName));

            // Get Instructions
            var instructions = GetInnerHtml(document, ".recipe-steps > li");
            Console.WriteLine(string.Join("", instructions));

            // Get Preparation time
            var preparationTime = GetInnerHtml(document, ".recipe-total-time");
            var textTime = string.Join("", preparationTime);
            string resultTime = Regex.Replace(textTime, @"[^\d]", "");
            Console.WriteLine(resultTime);

            // Get Cpmplicity

            // Get Portions count
            var portionsCount = GetInnerHtml(document, "#recipe-quick-items > ul > li:nth-child(1)");
            var textPortions = string.Join("", portionsCount);
            string resultPortions = Regex.Replace(textPortions, @"[^\d]", "");
            Console.WriteLine(resultPortions);


            // Get Image url

            // Get ingredients
            var ingredientsNames = GetInnerHtml(document, ".recipe-product-name").ToList();
            var ingredientsQuantity = GetInnerHtml(document, ".recipe-measurement").ToList();
            var ingrtedientsMesureUnits = GetInnerHtml(document, ".recipe-unit").ToList();
            var ingredientsNotes = GetInnerHtml(document, ".recipe-ingredient-note").ToList();

            Console.WriteLine(string.Join(" ", ingredientsNames));
            Console.WriteLine(string.Join(" ", ingredientsQuantity));
            Console.WriteLine(string.Join(" ", ingrtedientsMesureUnits));
            Console.WriteLine(string.Join(" ", ingredientsNotes));
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
