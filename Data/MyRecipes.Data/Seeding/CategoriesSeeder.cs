namespace MyRecipes.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    public class CategoriesSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Categories.Any())
            {
                return;
            }

            await dbContext.Categories.AddAsync(new Models.Category { Name = "Тарт" });
            await dbContext.Categories.AddAsync(new Models.Category { Name = "Кекс" });
            await dbContext.Categories.AddAsync(new Models.Category { Name = "Печено свинско" });

            await dbContext.SaveChangesAsync();
        }
    }
}
