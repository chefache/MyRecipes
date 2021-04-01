namespace MyRecipes.Services.Data
{
    using System;
    using System.Linq;

    using MyRecipes.Data.Common.Repositories;
    using MyRecipes.Data.Models;
    using MyRecipes.Services.Data.DTOs;
    using MyRecipes.Web.ViewModels.Home;

    public class ApplicationInfoService : IApplicationInfoService
    {
        private readonly IDeletableEntityRepository<ApplicationUser> userRepository;
        private readonly IDeletableEntityRepository<Recipe> recipeRepository;
        private readonly IDeletableEntityRepository<Category> categoryRepository;
        private readonly IDeletableEntityRepository<Ingredient> ingredientRepository;

        public ApplicationInfoService(
            IDeletableEntityRepository<ApplicationUser> userRepository,
            IDeletableEntityRepository<Recipe> recipeRepository,
            IDeletableEntityRepository<Category> categoryRepository,
            IDeletableEntityRepository<Ingredient> ingredientRepository)
        {
            this.userRepository = userRepository;
            this.recipeRepository = recipeRepository;
            this.categoryRepository = categoryRepository;
            this.ingredientRepository = ingredientRepository;
        }

        public DtoIndexViewModel GetInfo()
        {
            var homePageView = new DtoIndexViewModel
            {
                UsersCount = this.userRepository.All().Count(),
                RecipesCount = this.recipeRepository.All().Count(),
                CategoriesCount = this.categoryRepository.All().Count(),
                IngredientsCount = this.ingredientRepository.All().Count(),
            };

            return homePageView;
        }
    }
}
