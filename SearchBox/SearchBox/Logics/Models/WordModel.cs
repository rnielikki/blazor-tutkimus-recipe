using System.Collections.Generic;

namespace KeywordSearchBox
{
    public class WordModel : IWordModel
    {
        internal SortedSet<string> AvailableWordList;
        public string WordInput { get; internal set; } = "";
        public IList<string> Suggestions { get; internal set; }
        public IList<string> AddedWords { get; internal set; } //ordered list
        internal WordModel(IList<string> WordList)
        {
            AvailableWordList = new SortedSet<string>(WordList);
            AddedWords = new List<string>();
        }
    }
}
