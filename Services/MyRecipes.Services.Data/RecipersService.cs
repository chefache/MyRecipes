namespace MyRecipes.Services.Data
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using MyRecipes.Data.Common.Repositories;
    using MyRecipes.Data.Models;
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

        public async Task CreateAsync(CreateRecipeInputModel inputModel)
        {
            var recipe = new Recipe
            {
                Name = inputModel.Name,
                CategoryId = inputModel.CategoryId,
                CookingTime = TimeSpan.FromMinutes(inputModel.CookingTime),
                PreparationTime = TimeSpan.FromMinutes(inputModel.PreparationTime),
                Instructions = inputModel.Instructions,
                PortionsCount = inputModel.PortionsCount,
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
    }
}
