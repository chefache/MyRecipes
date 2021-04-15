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
        private readonly IDeletableEntityRepository<Category> categoriesRepository;

        public RecipersService(
            IDeletableEntityRepository<Ingredient> ingredientsRepository,
            IDeletableEntityRepository<Recipe> recipeRepository,
            IDeletableEntityRepository<Category> categoriesRepository)
        {
            this.ingredientsRepository = ingredientsRepository;
            this.recipeRepository = recipeRepository;
            this.categoriesRepository = categoriesRepository;
        }

        public async Task CreateAsync(CreateRecipeInputModel inputModel)
        {
            var recipe = new Recipe
            {
                Name = inputModel.Name,
                Complicity = inputModel.Complicity,
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

            foreach (var inputCategory in inputModel.Categories)
            {
                var currentCategory = this.categoriesRepository
                    .All()
                    .FirstOrDefault(x =>
                        x.Name == inputCategory.CategoryName);

                if (currentCategory == null)
                {
                    currentCategory = new Category
                    {
                        Name = inputCategory.CategoryName,
                    };
                }

                recipe.RecipeCategories.Add(new RecipeCategory
                {
                    Category = currentCategory,
                });
            }

            await this.recipeRepository.AddAsync(recipe);
            await this.ingredientsRepository.SaveChangesAsync();
            await this.categoriesRepository.SaveChangesAsync();
        }
    }
}
