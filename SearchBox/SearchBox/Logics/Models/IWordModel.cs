using System;
using System.Collections.Generic;
using System.Text;

namespace KeywordSearchBox
{
    public interface IWordModel
    {
        string WordInput { get; }
        IReadOnlyList<string> Suggestions { get; }
        IReadOnlyList<string> AddedWords { get; } 
    }
}
