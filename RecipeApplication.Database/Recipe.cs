using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RecipeApplication.Database
{
    #region Required
    public class Recipe
    {
        [Key]
        public int ID { get; set; }
        [Required]
        [MaxLength(50)]
        public string FoodName { get; set; }
        [Required]
        public string Content { get; set; }
        public virtual ISet<RecipeIngredient> RecipeIngredients { get; set; }
    }
    #endregion
}
