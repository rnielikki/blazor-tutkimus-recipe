using System.Collections.Generic;

namespace KeywordSearchBox
{
    public class WordModel : IWordModel
    {
        internal SortedSet<string> AvailableWordList;
        public string WordInput { get; internal set; } = "";
        internal List<string> _suggestions;
        public IReadOnlyList<string> Suggestions { get => _suggestions.AsReadOnly(); }
        internal List<string> _addedWords = new List<string>();
        public IReadOnlyList<string> AddedWords { get => _addedWords.AsReadOnly(); } //ordered list
        public WordModel(IList<string> WordList)
        {
            AvailableWordList = new SortedSet<string>(WordList);
        }
    }
}
