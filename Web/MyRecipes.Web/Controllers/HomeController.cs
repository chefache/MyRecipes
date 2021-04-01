namespace MyRecipes.Web.Controllers
{
    using System.Diagnostics;
    using System.Linq;
    using AutoMapper;
    using Microsoft.AspNetCore.Mvc;
    using MyRecipes.Data;
    using MyRecipes.Data.Common.Repositories;
    using MyRecipes.Data.Models;
    using MyRecipes.Services.Data;
    using MyRecipes.Services.Data.DTOs;
    using MyRecipes.Web.ViewModels;
    using MyRecipes.Web.ViewModels.Home;

    public class HomeController : BaseController
    {
        private readonly IApplicationInfoService infoService;

        public HomeController(IApplicationInfoService infoService)
        {
            this.infoService = infoService;

        }

        public IActionResult Index()
        {
            var dtoInfoModel = this.infoService.GetInfo();

            // var viewModel = this.mapper.Map<IndexViewModel>(info);
            var viewModel = new IndexViewModel
            {
                UsersCount = dtoInfoModel.UsersCount,
                RecipesCount = dtoInfoModel.RecipesCount,
                IngredientsCount = dtoInfoModel.IngredientsCount,
                CategoriesCount = dtoInfoModel.CategoriesCount,
            };

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
    }
}
