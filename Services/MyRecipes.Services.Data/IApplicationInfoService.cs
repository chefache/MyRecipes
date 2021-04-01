namespace MyRecipes.Services.Data
{
    using MyRecipes.Services.Data.DTOs;
    using MyRecipes.Web.ViewModels.Home;

    public interface IApplicationInfoService
    {
        DtoIndexViewModel GetInfo();
    }
}
