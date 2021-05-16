namespace MyRecipes.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Hosting;
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

        public async Task CreateAsync(CreateRecipeInputModel inputModel, string userId, string imagePath)
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

                recipe.Ingredients.Add(new RecipeIngredient
                {
                    Ingredient = currentIngredient,
                    Quantity = inputIngredient.IngredientQuantity,
                });
            }

            Directory.CreateDirectory($"{imagePath}/recipes/");

            var allowedExtensions = new[]{"jpg", "png", "gif" };

            foreach (var image in inputModel.Images)
            {
                var extension = Path.GetExtension(image.FileName).TrimStart('.');

                if (!allowedExtensions.Any(x => extension.EndsWith(x)))
                {
                    throw new Exception($"Invalid image extension{extension}");
                }

                var dbImage = new Image
                {
                    AddedByUserId = userId,
                    Extension = extension,
                };
                recipe.Images.Add(dbImage);

                var phisicalPath = $"{imagePath}/recipes/{dbImage.Id}.{extension}";
                using Stream filestream = new FileStream(phisicalPath, FileMode.Create);
                await image.CopyToAsync(filestream);
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

        public T GetById<T>(int id)
        {
            var recipe = this.recipeRepository.AllAsNoTracking()
                .Where(x => x.Id == id)
                .To<T>()
                .FirstOrDefault();

            return recipe;
        }

        public int GetRecipesCount()
        {
            return this.recipeRepository.All().Count();
        }
    }
}
