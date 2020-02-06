using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using Xunit;
using KeywordSearchBox;

namespace KeywordSearchBoxTests
{
    public class WordSearchTest
    {
        [Fact]
        public async void AsyncTest()
        {
            List<string> nonConcurrenctList = new List<string>();
            Func<IList<string>, Task> searchTask = async (IList<string> words) => { await Task.Delay(1); nonConcurrenctList.AddRange(new string[] { "item1", "item2", "item3", "asdf" }); }; 
            Func<Task> resetTask = async () => { await Task.Delay(1); nonConcurrenctList.Clear(); };
            WordHandler wordHandler = new WordHandler(new WordModel(new List<string>()), searchTask, resetTask);
            //concurrency pressure test: do without awaiting
            //
            await Task.WhenAny(new Task[] {
                wordHandler.Search(),
                wordHandler.Search(),
                wordHandler.Reset(),
                wordHandler.Reset(),
            });
        }
    }
}
