using Xunit;
using System.Collections.Generic;
using System.Linq;
using KeywordSearchBox;
using System.Threading.Tasks;

namespace KeywordSearchBoxTests
{
    public class WordListEditTest
    {
        static readonly List<string> target = new List<string>() { "apple", "almond", "banana", "chocolate", "salt", "sugar", "sweet potato" };
        [Fact]
        public void AddWordTest()
        {
            IWordModel model = new WordModel(target);
            WordHandler @WordHandler = new WordHandler(model, null, null);
            string nonExistWord = "shoe";
            @WordHandler.AddWord(nonExistWord);
            Assert.DoesNotContain(nonExistWord, model.AddedWords);

            string existWord = "sweet potato";
            Assert.Contains(existWord, model.AvailableWordList);
            @WordHandler.AddWord(existWord);
            Assert.Contains(existWord, model.AddedWords);
            Assert.DoesNotContain(existWord, model.AvailableWordList);

            //already added, assert that the word is't added anymore
            @WordHandler.AddWord(existWord);
            Assert.Equal(1, model.AddedWords.Count(value => value == existWord));
        }
        [Fact]
        public void DeleteWordTest()
        {
            IWordModel model = new WordModel(target);
            WordHandler @WordHandler = new WordHandler(model, null, null);
            @WordHandler.AddWord("apple");
            @WordHandler.AddWord("sugar");
            @WordHandler.AddWord("salt");

            string nonExistWord = "computer";
            @WordHandler.DeleteWord(nonExistWord);
            Assert.DoesNotContain(nonExistWord, model.AvailableWordList);
            @WordHandler.DeleteWord(nonExistWord);
            Assert.DoesNotContain(nonExistWord, model.AvailableWordList);

            string existWord = "sugar";
            @WordHandler.DeleteWord(existWord);
            Assert.Contains(existWord, model.AvailableWordList);
            Assert.DoesNotContain(existWord, model.AddedWords);
        }
        [Fact]
        public async Task ResetWordTest()
        {
            IWordModel model = new WordModel(target);
            WordHandler @WordHandler = new WordHandler(model, (IList<string> s)=> Task.CompletedTask, ()=>Task.CompletedTask);
            await @WordHandler.Reset();
            Assert.Empty(model.AddedWords);
            Assert.True(model.AvailableWordList.SequenceEqual(target.OrderBy(str=>str))); //AvailableWordList is "Sorted"Set.
        }
        [Fact]
        public void RangeSetTest()
        {
            IWordModel model = new WordModel(target);
            WordHandler @WordHandler = new WordHandler(model, null, null);
            List<string> ranges = new List<string>() { "apple", "chocolate", "salt" };
            @WordHandler.SetRange(ranges);
            Assert.True(model.AvailableWordList.SequenceEqual(new string[] { "almond", "banana", "sugar", "sweet potato" }));
            Assert.True(model.AddedWords.OrderBy(str=>str).SequenceEqual(ranges));
        }
    }
}
