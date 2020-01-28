using RecipeApplication.Models;
using System.Collections.Generic;

namespace RecipeApplication.Client
{
    public interface ISearchModel
    {
        IEnumerable<string> Keywords { get; }
        IEnumerable<RecipeDto> Results { get; }
        System.Threading.Tasks.Task FindAsync(IList<string> data);
    }
}
