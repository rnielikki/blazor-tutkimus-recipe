using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace KeywordSearchBox
{
    partial class WordHandler
    {
        private Func<IList<string>, Task> SearchAction { get; set; }
        private Func<Task> ResetAction { get; set; }
        private WordModel wordModel { get; set; }

        internal bool ShowSuggestions = false;
        internal SuggestionIterator SuggestionIterator { get; private set; }

        private CancellationTokenSource _cancellationSource;
        internal WordHandler(WordModel wordModel, Func<IList<string>, Task> searcher, Func<Task> resetter)
        {
            this.wordModel = wordModel;
            SearchAction = searcher;
            ResetAction = resetter;
            SuggestionIterator = new SuggestionIterator(this.wordModel.AvailableWordList.ToList());
        }
        internal void AddWord(string word)
        {
            wordModel.AddedWords.Add(word);
            wordModel.AvailableWordList.Remove(word);
            wordModel.WordInput = "";
            ShowSuggestions = false;
            SetSuggestions();
        }

        internal void DeleteWord(string word)
        {
            if (!wordModel.AddedWords.Remove(word)) return;
            wordModel.AvailableWordList.Add(word);
        }
        internal async Task Search()
        {
            if (_cancellationSource != null)
            {
                _cancellationSource.Cancel();
            }
            using (_cancellationSource = new CancellationTokenSource())
            {
                if (wordModel.AddedWords.Count == 0)
                {
                    await Task.Run(ResetAction, _cancellationSource.Token).ConfigureAwait(false);
                }
                else
                {
                    await Task.Run(() => SearchAction(wordModel.AddedWords), _cancellationSource.Token).ConfigureAwait(false);
                }
            }
            _cancellationSource = null;
        }
        internal async Task Reset()
        {
            wordModel.AvailableWordList.UnionWith(wordModel.AddedWords);
            wordModel.AddedWords.Clear();
            await Search();
        }
        private void SetSuggestions()
        {
            wordModel.Suggestions = wordModel.AvailableWordList.Where(word => word.StartsWith(wordModel.WordInput, true, System.Globalization.CultureInfo.CurrentCulture)).ToList();
            SuggestionIterator.OnListChanged(wordModel.Suggestions);
        }
        internal void SetRange(IEnumerable<string> range) {
            wordModel.AddedWords = range.ToList();
            wordModel.AvailableWordList = new SortedSet<string>(wordModel.AvailableWordList.Except(range));
        }
    }
}
