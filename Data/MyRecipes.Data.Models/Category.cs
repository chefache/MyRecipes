namespace MyRecipes.Data.Models
{
    using System.Collections.Generic;

    using MyRecipes.Data.Common.Models;

    public class Category : BaseDeletableModel<int>
    {
        public Category()
        {
            this.RecipeCategories = new HashSet<RecipeCategory>();
        }

        public string Name { get; set; }

        public virtual ICollection<RecipeCategory> RecipeCategories { get; set; }
    }
}
