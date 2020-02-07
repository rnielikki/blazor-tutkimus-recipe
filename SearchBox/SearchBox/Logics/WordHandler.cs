using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

[assembly: System.Runtime.CompilerServices.InternalsVisibleTo("KeywordSearchBoxTests")]
namespace KeywordSearchBox
{
    partial class WordHandler
    {
        private IWordModel WordModel;
        private SearchHandler SearchBehaviors;

        internal bool ShowSuggestions = false;
        internal SuggestionIterator SuggestionIterator { get; private set; }

        internal WordHandler(IWordModel WordModel, Func<IList<string>, Task> searcher, Func<Task> resetter)
        {
            this.WordModel = WordModel;
            SearchBehaviors = new SearchHandler(WordModel, searcher, resetter);
            SuggestionIterator = new SuggestionIterator(this.WordModel.AvailableWordList.ToList());
        }
        internal void AddWord(string word)
        {
            WordModel.AddWord(word);
            WordModel.WordInput = "";
            ShowSuggestions = false;
            SetSuggestions();
        }

        internal void DeleteWord(string word) => WordModel.RemoveWord(word);
        private void SetSuggestions()
        {
            WordModel.Suggestions = WordModel.AvailableWordList.Where(word => word.StartsWith(WordModel.WordInput, true, System.Globalization.CultureInfo.CurrentCulture)).ToList();
            SuggestionIterator.OnListChanged(WordModel.Suggestions);
        }
        internal async Task Search() => await SearchBehaviors.Search();
        internal async Task Reset() => await SearchBehaviors.Reset();
    }
}
