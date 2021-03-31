namespace MyRecipes.Web.Controllers
{
    using System.Diagnostics;
    using System.Linq;

    using Microsoft.AspNetCore.Mvc;
    using MyRecipes.Data;
    using MyRecipes.Data.Common.Repositories;
    using MyRecipes.Data.Models;
    using MyRecipes.Web.ViewModels;
    using MyRecipes.Web.ViewModels.Home;

    public class HomeController : BaseController
    {
        private readonly IDeletableEntityRepository<ApplicationUser> userRepository;
        private readonly IDeletableEntityRepository<Recipe> recipeRepository;
        private readonly IDeletableEntityRepository<Category> categoryRepository;
        private readonly IDeletableEntityRepository<Ingredient> ingredientRepository;

        public HomeController(
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

        public IActionResult Index()
        {
            var homePageView = new IndexViewModel
            {
                UsersCount = this.userRepository.All().Count(),
                RecipesCount = this.recipeRepository.All().Count(),
                CategoriesCount = this.categoryRepository.All().Count(),
                IngredientsCount = this.ingredientRepository.All().Count(),
            };

            return this.View(homePageView);
        }

        public IActionResult Privacy()
        {
            return this.View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return this.View(
                new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
        }
    }
}
