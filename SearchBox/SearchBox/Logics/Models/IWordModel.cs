using System;
using System.Collections.Generic;
using System.Text;

namespace KeywordSearchBox
{
    internal interface IWordModel
    {
        SortedSet<string> AvailableWordList { get; set; }
        string WordInput { get; set; }
        IList<string> Suggestions { get; set; }
        IList<string> AddedWords { get; set; }
        void AddWord(string word);
        void RemoveWord(string word);
        void SetRange(IEnumerable<string> range);
    }
}
