namespace MyRecipes.Web.Controllers
{
    using System;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Hosting;
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
        private readonly IWebHostEnvironment environment;

        public RecipeController(
            IRecipersService recipersService,
            ICategoriesService categoriesService,
            UserManager<ApplicationUser> userManager,
            IWebHostEnvironment environment)
        {
            this.recipersService = recipersService;
            this.categoriesService = categoriesService;
            this.userManager = userManager;
            this.environment = environment;
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

            try
            {
                await this.recipersService.CreateAsync(inputModel, userId, $"{this.environment.WebRootPath}/images");
            }
            catch (Exception ex)
            {
                this.ModelState.AddModelError(string.Empty, ex.Message);
                inputModel.CategoriesItems = this.categoriesService.GetAll();
                return this.View(inputModel);
            }

            // TODO:Redirect user to the page with the created recipe
            return this.Redirect("/");
        }

        [Authorize]
        public IActionResult Edit(int id)
        {
            var inputModel = this.recipersService.GetById<EditRecipeInputModel>(id);
            inputModel.Id = id;
            inputModel.CategoriesItems = this.categoriesService.GetAll();
            return this.View(inputModel);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Edit(int id, EditRecipeInputModel input)
        {
            // var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            if (!this.ModelState.IsValid)
            {
                input.Id = id;
                input.CategoriesItems = this.categoriesService.GetAll();
                return this.View(input);
            }

            await this.recipersService.UpdateAsync(id, input);
            return this.RedirectToAction(nameof(this.ById), new { id });
        }

        // id = pageNumber
        public IActionResult All(int id = 1)
        {
            if (id <= 0)
            {
                return this.NotFound();
            }

            const int ItemsPerPage = 12;

            var viewModel = new RecipesListViewModel
            {
                ItemsPerPage = ItemsPerPage,
                PageNumber = id,
                Recipes = this.recipersService.GetAll<RecipeInListViewModel>(id, ItemsPerPage),
                RecipesCount = this.recipersService.GetRecipesCount(),
            };

            return this.View(viewModel);
        }

        public IActionResult ById(int id)
        {
            var recipe = this.recipersService.GetById<SingleRecipeViewModel>(id);
            return this.View(recipe);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await this.recipersService.DeleteAsync(id);
            return this.RedirectToAction(nameof(this.All));
        }
    }
}
