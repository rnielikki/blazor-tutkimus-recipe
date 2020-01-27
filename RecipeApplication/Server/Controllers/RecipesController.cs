using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using RecipeApplication.Managers;
using System.Threading.Tasks;

namespace RecipeApplication.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecipesController : ControllerBase
    {
        private RecipesManager RecipeActions;
        public RecipesController(RecipesManager recipe) {
            RecipeActions = recipe;
        }
        [HttpPost("Search")]
        public async Task<IEnumerable<Database.Recipe>> Post([FromBody]IEnumerable<string> content)
        {
            return await RecipeActions.FindRecpies(content);
        }
    }
}