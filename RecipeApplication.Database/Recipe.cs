using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RecipeApplication.Database
{
    public class Recipe
    {
        [Key]
        public int ID { get; set; }
        public string FoodName { get; set; }
        public string Content { get; set; }
        public virtual ISet<RecipeIngredient> RecipeIngredients { get; set; }
    }
}
