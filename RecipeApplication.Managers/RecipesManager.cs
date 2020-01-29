using Microsoft.EntityFrameworkCore;
using RecipeApplication.Database;
using System.Collections.Generic;
using System.Linq;
using RecipeApplication.Models;
using System.Threading.Tasks;

namespace RecipeApplication.Managers
{
    public class RecipesManager : ManagerBase
    {
        public RecipesManager(RecipeContext recipeContext) : base(recipeContext) { }
        public async Task<IEnumerable<RecipeDto>> FindRecpies(IEnumerable<string> ingredients)
        {
            var yieldDB = _recipeContext.Recipes
                .Include(recipe=>recipe.RecipeIngredients)
                    .ThenInclude(ri => ri.Ingredient)
                .AsAsyncEnumerable();

            var ingredientIds = await _recipeContext.Ingredients.AsAsyncEnumerable().Where(i => ingredients.Contains(i.Name)).Select(i => i.ID).ToListAsync();

            return await yieldDB
                    .Where(recipe => !ingredientIds
                    .Except(recipe.RecipeIngredients.Select(ri => ri.IngredientId)).Any())
                .Select(r =>
                    new RecipeDto
                    {
                        ID = r.ID,
                        FoodName = r.FoodName,
                        Content = r.Content,
                        Ingredients = r.RecipeIngredients.Select(ri => new IngredientDto
                        {
                            ID = ri.Ingredient.ID,
                            Name = ri.Ingredient.Name,
                            Unit = ri.Unit.ToString(),
                            Amount = ri.Amount
                        })

                    }
                    ).ToListAsync();
        }
    }
}
