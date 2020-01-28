using System;
using System.Collections.Generic;
using System.Text;

namespace RecipeApplication.Models
{
    public class RecipeDto
    {
        public int ID { get; set; }
        public string FoodName { get; set; }
        public string Content { get; set; }
        public IEnumerable<IngredientDto> Ingredients { get; set; }
    }
}
