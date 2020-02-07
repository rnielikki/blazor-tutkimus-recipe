using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using System.Threading;

namespace KeywordSearchBox
{
    class SearchHandler
    {
        private IWordModel Model;
        private Func<IList<string>, Task> SearchAction { get; set; }
        private Func<Task> ResetAction { get; set; }
        internal SearchHandler(IWordModel wordModel, Func<IList<string>, Task> searcher, Func<Task> resetter) {
            Model = wordModel;
            SearchAction = searcher;
            ResetAction = resetter;
        }
        private CancellationTokenSource _cancellationSource;
        internal async Task Search()
        {
            if (_cancellationSource != null)
            {
                _cancellationSource.Cancel();
            }
            using (_cancellationSource = new CancellationTokenSource())
            {
                if (Model.AddedWords.Count == 0)
                {
                    await Task.Run(ResetAction, _cancellationSource.Token).ConfigureAwait(false);
                }
                else
                {
                    await Task.Run(() => SearchAction(Model.AddedWords), _cancellationSource.Token).ConfigureAwait(false);
                }
            }
            _cancellationSource = null;
        }
        internal async Task Reset()
        {
            Model.AvailableWordList.UnionWith(Model.AddedWords);
            Model.AddedWords.Clear();
            await Search();
        }
    }
}
