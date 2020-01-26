using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecipeApplication.Database
{
    public enum IngredientUnit { tbsp, tsp, c, l, dl, ml, pcs, mg, g, kg, pinch }
    #region Required
    public class Ingredient
    {
        [Key]
        public int ID { get; set; }
        [Required]
        [MaxLength(30)]
        public string Name { get; set; }
        [Column(TypeName = "nvarchar(10)")]
        public IngredientUnit Unit { get; set; } //NOTE: Can convert by implementing some methods.
        public virtual ISet<RecipeIngredient> RecipeIngredients { get; set; }

    }
    #endregion
}
