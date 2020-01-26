using System;
using System.Collections.Generic;
using System.Text;

namespace RecipeApplication.Shared
{
    public interface IRecipeModel {
        IEnumerable<string> data { get; set; }
    }
    public class RecipeModel: IRecipeModel
    {
        public IEnumerable<string> data { get; set; }
    }
}
