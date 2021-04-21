namespace MyRecipes.Data.Models
{
    using System;
    using System.Collections.Generic;

    using MyRecipes.Data.Common.Models;
    using MyRecipes.Data.Models.Enums;

    public class Recipe : BaseDeletableModel<int>
    {
        public Recipe()
        {
            this.RecipeIngredients = new HashSet<RecipeIngredient>();
            this.RecipeCategories = new HashSet<RecipeCategory>();
            this.Images = new HashSet<Image>();
        }

        public string Name { get; set; }

        public string Instructions { get; set; }

        public TimeSpan PreparationTime { get; set; }

        public Complicity Complicity { get; set; }

        public int PortionsCount { get; set; }

        public string AddedByUserId { get; set; }

        public virtual ApplicationUser AddedByUser { get; set; }

        public virtual ICollection<RecipeIngredient> RecipeIngredients { get; set; }

        public virtual ICollection<RecipeCategory> RecipeCategories { get; set; }

        public virtual ICollection<Image> Images { get; set; }
    }
}
