namespace MyRecipes.Web.Controllers
{
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using MyRecipes.Data.Models;
    using MyRecipes.Services.Data;
    using MyRecipes.Web.ViewModels.Recipes;

    public class RecipeController : Controller
    {
        private readonly IRecipersService recipersService;
        private readonly ICategoriesService categoriesService;
        private readonly UserManager<ApplicationUser> userManager;

        public RecipeController(
            IRecipersService recipersService,
            ICategoriesService categoriesService,
            UserManager<ApplicationUser> userManager)
        {
            this.recipersService = recipersService;
            this.categoriesService = categoriesService;
            this.userManager = userManager;
        }

        [Authorize]
        public IActionResult Create()
        {
            var viewModel = new CreateRecipeInputModel();
            viewModel.CategoriesItems = this.categoriesService.GetAll();

            return this.View(viewModel);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(CreateRecipeInputModel inputModel)
        {
            if (!this.ModelState.IsValid)
            {
                inputModel.CategoriesItems = this.categoriesService.GetAll();
                return this.View(inputModel);
            }

            var userId = this.userManager.GetUserId(this.User);
            // var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            await this.recipersService.CreateAsync(inputModel, userId);

            // TODO:Redirect user to the page with the created recipe
            return this.Redirect("/");
        }

        // id = pageNumber
        public IActionResult All(int id)
        {
            return this.View();
        }
    }
}
