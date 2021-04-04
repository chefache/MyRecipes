using Microsoft.AspNetCore.Mvc.Rendering;
using MyRecipes.Data.Common.Repositories;
using MyRecipes.Data.Models;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace MyRecipes.Services.Data
{
    public class CategoriesService : ICategoriesService
    {
        private readonly IDeletableEntityRepository<Category> categoryRepository;

        public CategoriesService(IDeletableEntityRepository<Category> categoryRepository)
        {
            this.categoryRepository = categoryRepository;
        }

        public IEnumerable<SelectListItem> GetAll()
        {
            var listItemCollection = new List<SelectListItem>();

            var dbCategoriesWithId = this.categoryRepository.All().Select(x => new
            {
                x.Id,
                x.Name,
            }).ToList();

            foreach (var categoryWithId in dbCategoriesWithId)
            {
                var listItem = new SelectListItem
                {
                    Value = categoryWithId.Id.ToString(),
                    Text = categoryWithId.Name,
                };

                listItemCollection.Add(listItem);
            }

            return listItemCollection;
        }
    }
}
