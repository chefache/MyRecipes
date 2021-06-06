namespace MyRecipes.Web.ViewModels.Recipes
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using MyRecipes.Data.Models.Enums;

    public abstract class BaseRecipeInputModel
    {
        [Required]
        [MinLength(4)]
        [Display(Name = "Име")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Категория")]
        public int CategoryId { get; set; }

        [Required]
        [MinLength(30)]
        [Display(Name = "Инструкции")]
        public string Instructions { get; set; }

        [Range(2, 60 * 24)]
        [Display(Name = "Време за приготвяне")]
        public int PreparationTime { get; set; }

        [Display(Name = "Сложност")]
        public Complicity Complicity { get; set; }

        [Range(1, 100)]
        [Display(Name = "Брой порции")]
        public int PortionsCount { get; set; }

        public IEnumerable<SelectListItem> CategoriesItems { get; set; }
    }
}
