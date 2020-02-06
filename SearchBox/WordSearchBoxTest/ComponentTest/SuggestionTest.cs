using System;
using System.Collections.Generic;
using Xunit;
using KeywordSearchBox;
using Egil.RazorComponents.Testing;
using System.Threading.Tasks;
using Egil.RazorComponents.Testing.EventDispatchExtensions;
using System.Linq;
using AngleSharp.Dom;

namespace KeywordSearchBoxTests.ComponentTest
{
    //sample of black box test, Blazor.
    public class SuggestionTest : ComponentTestFixture
    {
        readonly List<string> words = new List<string>() { "milk", "water", "wheat flour", "white pepper" };
        private Func<IList<string>, Task> searchAction = (IList<string> s) => Task.CompletedTask;
        [Fact]
        public void SuggestingTest()
        {
            var component = RenderComponent<SearchBox<object>>(
                (nameof(SearchBox<object>.WordList), words.AsReadOnly()),
                (nameof(SearchBox<object>.OnSearch), searchAction)
                );
            Assert.Throws<Xunit.Sdk.ElementNotFoundException>(() => component.Find(".searchbox-wordlist"));
            var inputBox = component.Find(".searchbox-input");
            inputBox.Input("w");
            var wordList = component.Find(".searchbox-wordlist");
            Assert.True(wordList.Children.Select(child => child.TextContent).SequenceEqual(new string[] { "water", "wheat flour", "white pepper" }));
            inputBox.Input("wh");
            wordList = component.Find(".searchbox-wordlist");
            Assert.True(wordList.Children.Select(child => child.TextContent).SequenceEqual(new string[] { "wheat flour", "white pepper" }));
            inputBox.Input("wha");
            Assert.Throws<Xunit.Sdk.ElementNotFoundException>(() => component.Find(".searchbox-wordlist"));
        }
        [Fact]
        public void AddingTest()
        {
            Func<IList<string>, Task> searchAction = (IList<string> s) => Task.CompletedTask;
            var component = RenderComponent<SearchBox<object>>(
                (nameof(SearchBox<object>.WordList), words.AsReadOnly()),
                (nameof(SearchBox<object>.OnSearch), searchAction)
                );
            var inputBox = component.Find(".searchbox-input");
            inputBox.Input("m");
            component.Find(".searchbox-wordlist").FirstElementChild.Click();
            var addedWordsBox = component.Find(".searchbox-words");
            Assert.Equal("milk",addedWordsBox.FirstElementChild.GetNodes<IText>().FirstOrDefault().Data.Trim());

            inputBox.Input("w");
            component.Find(".searchbox-wordlist").FirstElementChild.Click();
            addedWordsBox = component.Find(".searchbox-words");
            Assert.Equal(2, addedWordsBox.ChildElementCount);
            addedWordsBox.LastElementChild.LastElementChild.Click();
            addedWordsBox = component.Find(".searchbox-words");
            Assert.Equal(1, addedWordsBox.ChildElementCount);
        }
    }
}
