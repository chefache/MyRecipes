﻿namespace MyRecipes.Web.ViewModels.Recipes
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Microsoft.AspNetCore.Mvc.Rendering;

    public class CreateRecipeInputModel
    {
        [Required]
        [MinLength(4)]
        [Display(Name = "Име")]
        public string Name { get; set; }

        [Required]
        [MinLength(30)]
        [Display(Name = "Инструкции")]
        public string Instructions { get; set; }

        [Range(2, 60 * 24)]
        [Display(Name = "Време за подготовка в минути")]
        public int PreparationTime { get; set; }

        [Range(1, 60 * 12)]
        [Display(Name = "Време за сготвяне в минути")]
        public int CookingTime { get; set; }

        [Range(1, 100)]
        [Display(Name = "Брой порции")]
        public int PortionsCount { get; set; }

        [Required]
        [Display(Name = "Категория")]
        public int CategoryId { get; set; }

        [Display(Name = "Съставки: ")]
        public IEnumerable<RecipeIngredientInputModel> Ingredients { get; set; }

        public IEnumerable<SelectListItem> CategoriesItems { get; set; }
    }
}
