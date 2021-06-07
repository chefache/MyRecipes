namespace MyRecipes.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using MyRecipes.Web.ViewModels.Recipes;

    public interface IRecipersService
    {
        Task CreateAsync(CreateRecipeInputModel inputModel, string userId, string imagePath);

        IEnumerable<T> GetAll<T>(int page, int itemsPerPage = 12);

        int GetRecipesCount();

        T GetById<T>(int id);

        IEnumerable<T> GetRandom<T>(int count);

        Task UpdateAsync(int id, EditRecipeInputModel input);

        IEnumerable<T> GetByIngredients<T>(IEnumerable<int> ingredintIds);
    }
}
