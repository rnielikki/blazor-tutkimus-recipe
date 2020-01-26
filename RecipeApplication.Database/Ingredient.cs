using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RecipeApplication.Database
{
    public enum IngredientUnit { tbsp, tsp, c, l, dl, ml, pcs, mg, g, kg, pinch }
    public class Ingredient
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
        public IngredientUnit Unit { get; set; } //NOTE: Can convert by implementing some methods.
        public virtual ISet<RecipeIngredient> RecipeIngredients { get; set; }

    }
}
