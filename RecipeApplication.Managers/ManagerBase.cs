using RecipeApplication.Database;

namespace RecipeApplication.Managers
{
    public abstract class ManagerBase
    {
        protected readonly RecipeContext _recipeContext;
        public ManagerBase(RecipeContext recipeContext)
        {
            _recipeContext = recipeContext;
        }
    }
}
