namespace MyRecipes.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using MyRecipes.Web.ViewModels.Recipes;

    public class RecipeController : Controller
    {
        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        public IActionResult Create(CreateRecipeInputModel inputModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View();
            }

            // TODO: Create recipe using service method
            // TODO:Redirect user to the page with the created recipe
            return this.Redirect("/");
        }
    }
}
