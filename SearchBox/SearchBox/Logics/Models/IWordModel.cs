using System;
using System.Collections.Generic;
using System.Text;

namespace KeywordSearchBox
{
    public interface IWordModel
    {
        string WordInput { get; }
        IList<string> Suggestions { get; }
        IList<string> AddedWords { get; } 
    }
}
