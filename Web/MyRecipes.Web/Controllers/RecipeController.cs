namespace MyRecipes.Web.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using MyRecipes.Services.Data;
    using MyRecipes.Web.ViewModels.Recipes;

    public class RecipeController : Controller
    {
        private readonly IRecipersService recipersService;
        private readonly ICategoriesService categoriesService;

        public RecipeController(
            IRecipersService recipersService,
            ICategoriesService categoriesService)
        {
            this.recipersService = recipersService;
            this.categoriesService = categoriesService;
        }

        public IActionResult Create()
        {
            var viewModel = new CreateRecipeInputModel();
            //  viewModel.CategoriesItems = this.categoriesService.GetAll();

            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateRecipeInputModel inputModel)
        {
            if (!this.ModelState.IsValid)
            {
               // inputModel.CategoriesItems = this.categoriesService.GetAll();
                return this.View(inputModel);
            }

            await this.recipersService.CreateAsync(inputModel);

            // TODO:Redirect user to the page with the created recipe
            return this.Redirect("/");
        }
    }
}
