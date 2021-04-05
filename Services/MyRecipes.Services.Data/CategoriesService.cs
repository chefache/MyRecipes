namespace MyRecipes.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;

    using Microsoft.AspNetCore.Mvc.Rendering;
    using MyRecipes.Data.Common.Repositories;
    using MyRecipes.Data.Models;

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

            var dbCategoriesWithId = this.categoryRepository.AllAsNoTracking().Select(x => new
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
