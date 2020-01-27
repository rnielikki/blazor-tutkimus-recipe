using Microsoft.EntityFrameworkCore;
using RecipeApplication.Database;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecipeApplication.Managers
{
    public class RecipesManager : ManagerBase
    {
        public RecipesManager(RecipeContext recipeContext) : base(recipeContext) { }
        public async Task<IEnumerable<Recipe>> FindRecpies(IEnumerable<string> ingredients)
        {
            //the ingredient should not be long, and the length is saved for future.
            int ingredientLength = ingredients.Count();
            var yieldDB = _recipeContext.Recipes.AsAsyncEnumerable();
           
            return await yieldDB
                .Where(recipe => !ingredients
                    .Except(recipe.RecipeIngredients.Select(recipeIngredient => recipeIngredient.Ingredient.Name)).Any())
                .ToListAsync();
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
