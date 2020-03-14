using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using RecipeApplication.Managers;
using RecipeApplication.Models;
using System.Threading.Tasks;

namespace RecipeApplication.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecipesController : ControllerBase
    {
        private readonly RecipesManager RecipeActions;
        public RecipesController(RecipesManager recipe) {
            RecipeActions = recipe;
        }
        [HttpGet("{Id}")]
        public async Task<RecipeDto> Get(int id)
        {
            var recipe = await RecipeActions.GetRecipe(id).ConfigureAwait(false);
            if (recipe == null)
            {
                HttpContext.Response.StatusCode = 404;
            }
            return recipe;
        }
        [HttpPost("Search")]
        public async Task<IEnumerable<RecipeDto>> Post([FromBody]IEnumerable<string> content)
        {
            return await RecipeActions.FindRecpies(content).ConfigureAwait(false);
        }
    }
}