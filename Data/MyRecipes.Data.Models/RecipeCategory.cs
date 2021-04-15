﻿namespace MyRecipes.Data.Models
{
    public class RecipeCategory
    {
        public int Id { get; set; }

        public int RecipeId { get; set; }

        public virtual Recipe Recipe { get; set; }

        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }
    }
}
