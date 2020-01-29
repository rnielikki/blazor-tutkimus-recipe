using Microsoft.EntityFrameworkCore;
using RecipeApplication.Database;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace RecipeApplication.Managers
{
    public class IngredientsManager : ManagerBase
    {
        public IngredientsManager(RecipeContext recipeContext) : base(recipeContext) { }
        public async Task<IEnumerable<string>> GetIngredientsList() => await _recipeContext.Ingredients.AsQueryable().Select(ingredient=>ingredient.Name).ToListAsync();
    }
}
