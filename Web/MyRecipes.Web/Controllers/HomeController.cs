namespace MyRecipes.Web.Controllers
{
    using System.Diagnostics;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using MyRecipes.Data;
    using MyRecipes.Data.Common.Repositories;
    using MyRecipes.Data.Models;
    using MyRecipes.Services.Data;
    using MyRecipes.Web.ViewModels;
    using MyRecipes.Web.ViewModels.Home;

    public class HomeController : BaseController
    {
        private readonly IApplicationInfoService infoService;
        private readonly UserManager<ApplicationUser> userManager;

        public HomeController(IApplicationInfoService infoService, UserManager<ApplicationUser> userManager)
        {
            this.infoService = infoService;
            this.userManager = userManager;
        }

        public IActionResult Index()
        {
            var viewModel = this.infoService.GetInfo();

            return this.View(viewModel);
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

        public async Task<IActionResult> UserInfo()
        {
            var user = await this.userManager.GetUserAsync(this.User);
            return this.Json(user);
        }
    }
}
