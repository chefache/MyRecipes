namespace MyRecipes.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Identity;
    using MyRecipes.Data.Common.Repositories;
    using MyRecipes.Data.Models;
    using MyRecipes.Data.Models.Enums;
    using MyRecipes.Services.Data;
    using MyRecipes.Web.ViewModels.Recipes;

    public class RecipesSeeder : ISeeder
    {
        private readonly IRecipersService recipersService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IngredientsService ingredientsService;
        private readonly IRepository<Ingredient> ingredientsRepository;

        public RecipesSeeder(
            IRecipersService recipersService,
            UserManager<ApplicationUser> userManager,
            IngredientsService ingredientsService,
            IRepository<Ingredient> ingredientsRepository)
        {
            this.recipersService = recipersService;
            this.userManager = userManager;
            this.ingredientsService = ingredientsService;
            this.ingredientsRepository = ingredientsRepository;
        }

        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            var userId = this.userManager.FindByNameAsync("stefan.m.koev@gmail.com").GetAwaiter().GetResult().Id;
            var ingredientOne = new RecipeIngredientInputModel { IngredientName = "Кисело мляко", IngredientQuantity = "Кофичка" };
            var ingredientTwo = new RecipeIngredientInputModel { IngredientName = "Минерална вода", IngredientQuantity = "Кофичка" };
            var ingredientThree = new RecipeIngredientInputModel { IngredientName = "Сол", IngredientQuantity = "1 щипка" };
            var ingredients = new List<RecipeIngredientInputModel> { ingredientOne, ingredientTwo, ingredientThree };


            var recipe = new Recipe
            {
                Name = "Айран",
                CategoryId = 4,
                Complicity = Complicity.Ниска,
                Instructions = "Разбиваме кофичка кисело мляко и кофичка вода, добавяме щипка сол и разбъркваме всичко енергично.Сервираме студен с лед.",
                PreparationTime = TimeSpan.FromMinutes(5),
                PortionsCount = 4,
                AddedByUserId = userId,

            };

            foreach (var inputIngredient in ingredients)
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

            //Directory.CreateDirectory($"{imagePath}/recipes/");

            //var allowedExtensions = new[] { "jpg", "png", "gif" };

            //foreach (var image in recipe.Images)
            //{
            //    var extension = Path.GetExtension(image.FileName).TrimStart('.');

            //    if (!allowedExtensions.Any(x => extension.EndsWith(x)))
            //    {
            //        throw new Exception($"Invalid image extension{extension}");
            //    }

            //    var dbImage = new Image
            //    {
            //        AddedByUserId = userId,
            //        Extension = extension,
            //    };
            //    recipe.Images.Add(dbImage);

            //    var phisicalPath = $"{imagePath}/recipes/{dbImage.Id}.{extension}";
            //    using Stream filestream = new FileStream(phisicalPath, FileMode.Create);
            //    await image.CopyToAsync(filestream);
            //}

            //await this.recipeRepository.AddAsync(recipe);
            //await this.ingredientsRepository.SaveChangesAsync();
        }

        //public string Name { get; set; }

        //public string Instructions { get; set; }

        //public TimeSpan PreparationTime { get; set; }

        //public Complicity Complicity { get; set; }

        //public int PortionsCount { get; set; }

        //public string AddedByUserId { get; set; }

        //public virtual ApplicationUser AddedByUser { get; set; }

        //public virtual ICollection<RecipeIngredient> RecipeIngredients { get; set; }

        //public int CategoryId { get; set; }

        //public virtual Category Category { get; set; }

        //public virtual ICollection<Image> Images { get; set; }
    }
}
