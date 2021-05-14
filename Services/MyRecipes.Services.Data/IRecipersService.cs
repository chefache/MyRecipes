namespace MyRecipes.Services.Data
{
    using System.Threading.Tasks;

    using MyRecipes.Web.ViewModels.Recipes;

    public interface IRecipersService
    {
       Task CreateAsync(CreateRecipeInputModel inputModel, string userId);
    }
}
