using System.Collections.Generic;

namespace KeywordSearchBox
{
    public interface ISearchModel {
        IEnumerable<string> Keywords { get; set; }
    }
    public interface ISearchModel<T>: ISearchModel
    {
        IEnumerable<T> Results { get; set; }
    }
}
