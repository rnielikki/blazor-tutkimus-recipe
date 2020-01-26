using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace RecipeApplication.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecipeController : ControllerBase
    {
        [HttpPost]
        public IEnumerable<string> Post([FromBody]IEnumerable<string> content)
        {
            return new List<string>() { content.FirstOrDefault(), "asdfadsf", content.LastOrDefault() };
        }
        /*
        [HttpGet]
        public string Get() {
            //return "Got string " + content.FirstOrDefault();
            return "Got string ";
        }
        */
    }
}