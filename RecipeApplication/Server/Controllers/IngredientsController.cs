using Microsoft.AspNetCore.Mvc;
using RecipeApplication.Managers;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecipeApplication.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IngredientsController : ControllerBase
    {
        private IngredientsManager IngredientActions;
        public IngredientsController(IngredientsManager ingredient)
        {
            IngredientActions = ingredient;
        }
        [HttpGet]
        public async Task<IEnumerable<string>> Get() {
            return (await IngredientActions.GetIngredients()).Select(ingredient => ingredient.Name);
        }
    }
}