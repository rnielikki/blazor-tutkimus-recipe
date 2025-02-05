﻿using System.Collections.Generic;

[assembly: System.Runtime.CompilerServices.InternalsVisibleTo("DynamicProxyGenAssembly2")]
namespace KeywordSearchBox
{
    public interface ISearchModel {
        IEnumerable<string> Keywords { get; set; }
        bool IsReset { get; internal set; }
    }
    public interface ISearchModel<T>: ISearchModel
    {
        IEnumerable<T> Results { get; set; }
    }
}
