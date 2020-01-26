using Microsoft.EntityFrameworkCore;
using RecipeApplication.Database;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecipeApplication.Managers
{
    public class RecipeManager : ManagerBase
    {
        public RecipeManager(RecipeContext recipeContext) : base(recipeContext) { }
        public async Task<ICollection<Recipe>> FindRecpies(IEnumerable<string> ingredients)
        {
            return await _recipeContext.Recipes
                .Where(recipe => !ingredients
                    .Except(recipe.RecipeIngredients
                        .Select(recipeIngredient => recipeIngredient.Ingredient.Name))
                    .Any()).ToListAsync();
        }
        public async Task<bool> AddRecipe(Recipe recipe)
        {
            try
            {
                await _recipeContext.Recipes.AddAsync(recipe);
                await _recipeContext.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                return false;
            }
            return true;
        }
    }
}
