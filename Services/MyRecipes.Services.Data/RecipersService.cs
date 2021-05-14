namespace MyRecipes.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using MyRecipes.Data.Common.Repositories;
    using MyRecipes.Data.Models;
    using MyRecipes.Services.Mapping;
    using MyRecipes.Web.ViewModels.Recipes;

    public class RecipersService : IRecipersService
    {
        private readonly IDeletableEntityRepository<Ingredient> ingredientsRepository;
        private readonly IDeletableEntityRepository<Recipe> recipeRepository;

        public RecipersService(
            IDeletableEntityRepository<Ingredient> ingredientsRepository,
            IDeletableEntityRepository<Recipe> recipeRepository)
        {
            this.ingredientsRepository = ingredientsRepository;
            this.recipeRepository = recipeRepository;
        }

        public async Task CreateAsync(CreateRecipeInputModel inputModel, string userId)
        {
            var recipe = new Recipe
            {
                Name = inputModel.Name,
                CategoryId = inputModel.CategoryId,
                Complicity = inputModel.Complicity,
                PreparationTime = TimeSpan.FromMinutes(inputModel.PreparationTime),
                Instructions = inputModel.Instructions,
                PortionsCount = inputModel.PortionsCount,
                AddedByUserId = userId,
            };

            foreach (var inputIngredient in inputModel.Ingredients)
            {
                var currentIngredient = this.ingredientsRepository
                    .All()
                    .FirstOrDefault(x =>
                    x.Name == inputIngredient.IngredientName);

                if (currentIngredient == null)
                {
                    currentIngredient = new Ingredient
                    {
                        Name = inputIngredient.IngredientName,
                    };
                }

                recipe.RecipeIngredients.Add(new RecipeIngredient
                {
                    Ingredient = currentIngredient,
                    Quantity = inputIngredient.IngredientQuantity,
                });
            }

            await this.recipeRepository.AddAsync(recipe);
            await this.ingredientsRepository.SaveChangesAsync();
        }

        public IEnumerable<T> GetAll<T>(int page, int itemsPerPage = 12)
        {
            var recipes = this.recipeRepository.AllAsNoTracking()
                 .OrderByDescending(x => x.Id)
                 .Skip((page - 1) * itemsPerPage)
                 .Take(itemsPerPage)
                 .To<T>()
                 .ToList();

            return recipes;
        }
    }
}
