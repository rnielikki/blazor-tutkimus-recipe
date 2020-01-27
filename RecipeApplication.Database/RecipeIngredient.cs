using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecipeApplication.Database
{
    public enum IngredientUnit { tbsp, tsp, c, l, dl, ml, pcs, mg, g, kg, pinch }
    #region Required
    public class RecipeIngredient
    {
        [Key]
        public int Id { get; set; }
        public int RecipeId { get; set; }
        public int IngredientId { get; set; }
        [Column(TypeName = "nvarchar(10)")]
        public IngredientUnit Unit { get; set; } //NOTE: Can convert by implementing some methods.
        public decimal Amount { get; set; }
        public virtual Recipe Recipe { get; set; }
        public virtual Ingredient Ingredient { get; set; }
    }
    #endregion
}
