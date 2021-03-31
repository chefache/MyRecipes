namespace MyRecipes.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    public class IngredientsSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Ingredients.Any())
            {
                return;
            }

            await dbContext.Ingredients.AddAsync(new Models.Ingredient { Name = "Сол" });
            await dbContext.Ingredients.AddAsync(new Models.Ingredient { Name = "Черен пипер" });
            await dbContext.Ingredients.AddAsync(new Models.Ingredient { Name = "Домати" });

            await dbContext.SaveChangesAsync();
        }
    }
}
