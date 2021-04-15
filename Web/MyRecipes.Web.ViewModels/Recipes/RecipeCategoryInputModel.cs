namespace MyRecipes.Web.ViewModels.Recipes
{
    using System.ComponentModel.DataAnnotations;

    public class RecipeCategoryInputModel
    {
        [Required]
        [MinLength(3)]
        public string CategoryName { get; set; }
    }
}
