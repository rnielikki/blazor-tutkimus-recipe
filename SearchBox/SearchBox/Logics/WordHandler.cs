using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace KeywordSearchBox
{
    partial class WordHandler
    {
        private Func<IList<string>, Task> _searchAction { get; set; }
        private Func<Task> _resetAction { get; set; }
        private WordModel _wordModel { get; set; }

        internal bool ShowSuggestions = false;
        internal SuggestionIterator SuggestionIterator { get; private set; }

        private CancellationTokenSource _cancellationSource;
        internal WordHandler(WordModel wordModel, Func<IList<string>, Task> searchAction, Func<Task> resetAction=null)
        {
            _wordModel = wordModel;
            _searchAction = searchAction;
            if (_searchAction != null)
            {
                _resetAction = resetAction ?? (async ()=> { await searchAction(_wordModel.AvailableWordList.ToList()); });
            }
            SuggestionIterator = new SuggestionIterator(_wordModel.AvailableWordList.ToList());
        }
        internal void AddWord(string word)
        {
            _wordModel._addedWords.Add(word);
            _wordModel.AvailableWordList.Remove(word);
            _wordModel.WordInput = "";
            ShowSuggestions = false;
            SetSuggestions();
        }

        internal void DeleteWord(string word)
        {
            if (!_wordModel._addedWords.Remove(word)) return;
            _wordModel.AvailableWordList.Add(word);
        }
        internal async Task Search()
        {
            if (_cancellationSource != null)
            {
                _cancellationSource.Cancel();
            }
            using (_cancellationSource = new CancellationTokenSource())
            {
                if (_wordModel._addedWords.Count == 0)
                {
                    await Task.Run(_resetAction, _cancellationSource.Token).ConfigureAwait(false);
                    //await _resetAction?.Invoke();
                }
                else
                {
                    await Task.Run(() => _searchAction(_wordModel._addedWords), _cancellationSource.Token).ConfigureAwait(false);
                    //await _searchAction?.Invoke(_wordModel._addedWords);
                }
            }
            _cancellationSource = null;
        }
        internal async Task Reset()
        {
            _wordModel.AvailableWordList.UnionWith(_wordModel._addedWords);
            _wordModel._addedWords.Clear();
            await Search();
        }
        private void SetSuggestions()
        {
            _wordModel._suggestions = _wordModel.AvailableWordList.Where(word => word.StartsWith(_wordModel.WordInput, true, System.Globalization.CultureInfo.CurrentCulture)).ToList();
            SuggestionIterator.OnListChanged(_wordModel._suggestions);
        }
    }
}
