namespace MyRecipes.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using MyRecipes.Web.ViewModels.Recipes;

    public interface IRecipersService
    {
        Task CreateAsync(CreateRecipeInputModel inputModel, string userId);

        IEnumerable<T> GetAll<T>(int page, int itemsPerPage = 12);
    }
}
