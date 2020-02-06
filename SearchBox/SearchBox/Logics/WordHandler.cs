using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

[assembly: System.Runtime.CompilerServices.InternalsVisibleTo("KeywordSearchBoxTests")]
namespace KeywordSearchBox
{
    partial class WordHandler
    {
        private Func<IList<string>, Task> SearchAction { get; set; }
        private Func<Task> ResetAction { get; set; }
        private IWordModel WordModel { get; set; }

        internal bool ShowSuggestions = false;
        internal SuggestionIterator SuggestionIterator { get; private set; }

        private CancellationTokenSource _cancellationSource;
        internal WordHandler(IWordModel WordModel, Func<IList<string>, Task> searcher, Func<Task> resetter)
        {
            this.WordModel = WordModel;
            SearchAction = searcher;
            ResetAction = resetter;
            SuggestionIterator = new SuggestionIterator(this.WordModel.AvailableWordList.ToList());
        }
        internal void AddWord(string word)
        {
            if (!WordModel.AvailableWordList.Remove(word)) return;
            WordModel.AddedWords.Add(word);
            WordModel.WordInput = "";
            ShowSuggestions = false;
            SetSuggestions();
        }

        internal void DeleteWord(string word)
        {
            if (!WordModel.AddedWords.Remove(word)) return;
            WordModel.AvailableWordList.Add(word);
        }
        internal async Task Search()
        {
            if (_cancellationSource != null)
            {
                _cancellationSource.Cancel();
            }
            using (_cancellationSource = new CancellationTokenSource())
            {
                if (WordModel.AddedWords.Count == 0)
                {
                    await Task.Run(ResetAction, _cancellationSource.Token).ConfigureAwait(false);
                }
                else
                {
                    await Task.Run(() => SearchAction(WordModel.AddedWords), _cancellationSource.Token).ConfigureAwait(false);
                }
            }
            _cancellationSource = null;
        }
        internal async Task Reset()
        {
            WordModel.AvailableWordList.UnionWith(WordModel.AddedWords);
            WordModel.AddedWords.Clear();
            await Search();
        }
        private void SetSuggestions()
        {
            WordModel.Suggestions = WordModel.AvailableWordList.Where(word => word.StartsWith(WordModel.WordInput, true, System.Globalization.CultureInfo.CurrentCulture)).ToList();
            SuggestionIterator.OnListChanged(WordModel.Suggestions);
        }
        internal void SetRange(IEnumerable<string> range) {
            WordModel.AddedWords = range.ToList();
            WordModel.AvailableWordList = new SortedSet<string>(WordModel.AvailableWordList.Except(range));
        }
    }
}
