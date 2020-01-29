using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Json;
using System;
using System.Text;

namespace KeywordSearchBox
{
    public class SearchModel<T> : ISearchModel<T>
    {
        public IEnumerable<string> Keywords { get; set; }
        public IEnumerable<T> Results { get; set; }
    }
}
