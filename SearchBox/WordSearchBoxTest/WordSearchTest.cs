using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using KeywordSearchBox;
using System.Linq;

namespace KeywordSearchBoxTests
{
    public class WordSearchTest
    {
        static readonly List<string> target = new List<string>() { "apple", "almond", "banana", "chocolate", "salt", "sugar", "sweet potato" };
        [Fact]
        public async void AsyncTest()
        {
            List<string> nonConcurrenctList = new List<string>();
            async Task searchTask(IList<string> words) { await Task.Delay(1); nonConcurrenctList.AddRange(new string[] { "item1", "item2", "item3", "asdf" }); }
            async Task resetTask() { await Task.Delay(1); nonConcurrenctList.Clear(); }
            SearchHandler searchHandler = new SearchHandler(new WordModel(new List<string>()), searchTask, resetTask);

            //concurrency pressure test: do without awaiting
            await Task.WhenAny(new Task[] {
                searchHandler.Search(),
                searchHandler.Search(),
                searchHandler.Reset(),
                searchHandler.Reset(),
            });
        }
        [Fact]
        public async Task ResetWordTest()
        {
            IWordModel model = new WordModel(target);
            SearchHandler searcher = new SearchHandler(model, (IList<string> s)=> Task.CompletedTask, ()=>Task.CompletedTask);
            await searcher.Reset();
            Assert.Empty(model.AddedWords);
            Assert.True(model.AvailableWordList.SequenceEqual(target.OrderBy(str => str))); //AvailableWordList is "Sorted"Set.
        }
        [Fact]
        public async Task SearchWordTest()
        {
            IWordModel model = new WordModel(target);
            model.AddWord("chocolate");
            model.AddWord("salt");
            string result="";
            SearchHandler searcher = new SearchHandler(model, (IList<string> s)=> { result = s.FirstOrDefault(); return Task.CompletedTask; }, ()=>Task.CompletedTask);
            await searcher.Search();
            Assert.Equal("chocolate", result);
        }
    }
}
