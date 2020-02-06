using System.Collections.Generic;

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
    }
}
