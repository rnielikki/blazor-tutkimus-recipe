﻿using Microsoft.EntityFrameworkCore;
using RecipeApplication.Database;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RecipeApplication.Managers
{
    class IngredientManager:ManagerBase
    {
        public IngredientManager(RecipeContext recipeContext) : base(recipeContext) { }
        public async Task<ICollection<Ingredient>> GetIngredients() => await _recipeContext.Ingredients.ToListAsync();
        public async Task<bool> AddIngredient(Ingredient ingredient)
        {
            try
            {
                await _recipeContext.Ingredients.AddAsync(ingredient);
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
