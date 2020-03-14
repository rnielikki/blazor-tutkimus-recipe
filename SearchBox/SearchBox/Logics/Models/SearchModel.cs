using System.Collections.Generic;

namespace KeywordSearchBox
{
    public class SearchModel<T> : ISearchModel<T>
    {
        public IEnumerable<string> Keywords { get; set; }
        public IEnumerable<T> Results { get; set; }
        bool ISearchModel.IsReset { get; set; } = true;
    }
}
