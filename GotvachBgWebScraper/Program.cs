using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

            // Get Instructions

            // Get Preparation time

            // Get Cpmplicity

            // Get Portions count

            // Get Image url

            // Get ingredients
            var ingredientsNames = GetInnerHtml(document, ".recipe-product-name").ToList();
            var ingredientsQuantity = GetInnerHtml(document, ".recipe-measurement").ToList();
            var ingrtedientsMesureUnits = GetInnerHtml(document, ".recipe-unit").ToList();
            var ingredientsNotes = GetInnerHtml(document, ".recipe-ingredient-note").ToList();

            var result = new Dictionary<string, string>();

            var counter = 0;
            while (counter <= ingredientsNames.Count + 1)
            {
                if (ingredientsQuantity[counter] == null || ingrtedientsMesureUnits[counter] == null)
                {
                    result.Add(ingredientsNames[counter], ingredientsNotes[counter]);
                }
                result.Add(ingredientsNames[counter], ingredientsQuantity[counter] + ingrtedientsMesureUnits[counter]);
                counter++;
            }
            foreach (var item in result)
            {
                Console.WriteLine(item.Key + item.Value);
            }
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
