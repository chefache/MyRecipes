namespace MyRecipes.Web.Controllers
{

    using Microsoft.AspNetCore.Mvc;
    using MyRecipes.Services.Data;
    using MyRecipes.Web.ViewModels.Recipes;
    using MyRecipes.Web.ViewModels.SearchRecipes;

    public class SearchRecipesController : BaseController
    {
        private readonly IRecipersService recipersService;
        private readonly IIngredientsService ingredientsService;

        public SearchRecipesController(
            IRecipersService recipersService,
            IIngredientsService ingredientsService)
        {
            this.recipersService = recipersService;
            this.ingredientsService = ingredientsService;
        }

        public IActionResult Index()
        {
            var viewModel = new SearchIndexViewModel
            {
                Ingredients = this.ingredientsService.GetAll<IngredientNameIdViewModel>(),
            };

            return this.View(viewModel);
        }

        [HttpGet]
        public IActionResult List(SearchListInputModel model)
        {
            var viewModel = new ListViewModel
            {
                Recipes = this.recipersService
                .GetByIngredients<RecipeInListViewModel>(model.Ingredients),
            };

            return this.View(viewModel);
        }
    }
}
