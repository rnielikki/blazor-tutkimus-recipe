using System;
using System.Collections.Generic;
using System.Text;


namespace RecipeApplication.Models
{
    public class IngredientDto
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Unit { get; set; }
        public decimal Amount { get; set; }
    }
}
