using System.Collections.Generic;

namespace KeywordSearchBox
{
    public interface ISearchModel<T>
    {
        IEnumerable<string> Keywords { get; set; }
        IEnumerable<T> Results { get; set; }
    }
}
