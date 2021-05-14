namespace MyRecipes.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using MyRecipes.Data.Models;
    using MyRecipes.Services.Data;
    using MyRecipes.Web.ViewModels.Recipes;

    public class RecipesSeeder : ISeeder
    {
        private readonly IRecipersService recipersService;

        public RecipesSeeder(IRecipersService recipersService)
        {
            this.recipersService = recipersService;
        }

        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            var ingredientOne = new RecipeIngredientInputModel { IngredientName = "Piper", IngredientQuantity = "1kg." }; 


            var recipe = recipersService.CreateAsync(new CreateRecipeInputModel
            {
                Name = "Шопска салата",
                Instructions = "",
                PreparationTime = TimeSpan.FromMinutes(20).Minutes,
                Complicity = Models.Enums.Complicity.Ниска,
                PortionsCount = 6,
                CategoryId = 7,
                Ingredients = new List<RecipeIngredientInputModel> { ingredientOne },
            });

            await dbContext.Recipes.AddAsync(new Recipe
            {
                Name = "Шопска салата",
                Instructions = "",
                PreparationTime = TimeSpan.FromMinutes(20),
                Complicity = Models.Enums.Complicity.Ниска,
                PortionsCount = 6,
                CategoryId = 7,
                RecipeIngredients = new List<RecipeIngredient> {  }
            });
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
