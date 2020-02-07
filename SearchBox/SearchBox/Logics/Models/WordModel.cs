using System.Collections.Generic;
using System.Linq;

namespace KeywordSearchBox
{
    internal class WordModel : IWordModel
    {
        public SortedSet<string> AvailableWordList{ get; set; }
        public string WordInput { get; set; } = "";
        public IList<string> Suggestions { get; set; }
        public IList<string> AddedWords { get; set; } //ordered list
        internal WordModel(IList<string> WordList)
        {
            AvailableWordList = new SortedSet<string>(WordList);
            AddedWords = new List<string>();
        }
        public void AddWord(string word)
        {
            if (!AvailableWordList.Remove(word)) return;
            AddedWords.Add(word);
        }
        public void RemoveWord(string word)
        {
            if (!AddedWords.Remove(word)) return;
            AvailableWordList.Add(word);
        }
        public void SetRange(IEnumerable<string> range)
        {
            AddedWords = range.ToList();
            AvailableWordList = new SortedSet<string>(AvailableWordList.Except(range));
        }
    }
}
