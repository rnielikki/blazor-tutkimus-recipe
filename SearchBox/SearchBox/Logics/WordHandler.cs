using Microsoft.AspNetCore.Components;
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
        private string Uri { get; set; }

        internal bool ShowSuggestions = false;
        internal SuggestionIterator SuggestionIterator { get; private set; }

        private CancellationTokenSource _cancellationSource;
        internal WordHandler(NavigationManager navigator, WordModel wordModel, string searchUri, Func<IList<string>, Task> searcher, Func<Task> resetter=null)
        {
            Uri = searchUri;
            this.wordModel = wordModel;
            SearchAction = (async (IList<string> words) => { await searcher(words); navigator.NavigateTo(Uri); });
            if (SearchAction != null)
            {
                ResetAction = resetter ?? (async ()=> { await searcher(this.wordModel.AvailableWordList.ToList()); });
            }
            SuggestionIterator = new SuggestionIterator(this.wordModel.AvailableWordList.ToList());
        }
        internal void AddWord(string word)
        {
            wordModel._addedWords.Add(word);
            wordModel.AvailableWordList.Remove(word);
            wordModel.WordInput = "";
            ShowSuggestions = false;
            SetSuggestions();
        }

        internal void DeleteWord(string word)
        {
            if (!wordModel._addedWords.Remove(word)) return;
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
                if (wordModel._addedWords.Count == 0)
                {
                    await Task.Run(ResetAction, _cancellationSource.Token).ConfigureAwait(false);
                    //await _resetAction?.Invoke();
                }
                else
                {
                    await Task.Run(() => SearchAction(wordModel._addedWords), _cancellationSource.Token).ConfigureAwait(false);
                    //await _searchAction?.Invoke(_wordModel._addedWords);
                }
            }
            _cancellationSource = null;
        }
        internal async Task Reset()
        {
            wordModel.AvailableWordList.UnionWith(wordModel._addedWords);
            wordModel._addedWords.Clear();
            await Search();
        }
        private void SetSuggestions()
        {
            wordModel._suggestions = wordModel.AvailableWordList.Where(word => word.StartsWith(wordModel.WordInput, true, System.Globalization.CultureInfo.CurrentCulture)).ToList();
            SuggestionIterator.OnListChanged(wordModel._suggestions);
        }
    }
}
