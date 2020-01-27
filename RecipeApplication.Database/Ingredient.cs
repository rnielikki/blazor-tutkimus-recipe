using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RecipeApplication.Database
{
    #region Required
    public class Ingredient
    {
        [Key]
        public int ID { get; set; }
        [Required]
        [MaxLength(30)]
        public string Name { get; set; }
        public virtual ISet<RecipeIngredient> RecipeIngredients { get; set; }
    }
    #endregion
}
