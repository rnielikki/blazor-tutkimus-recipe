using System;
using System.Collections.Generic;
using Xunit;
using KeywordSearchBox;
using Egil.RazorComponents.Testing;
using System.Threading.Tasks;
using Egil.RazorComponents.Testing.EventDispatchExtensions;
using System.Linq;
using AngleSharp.Dom;

namespace KeywordSearchBoxTests.ComponentTests
{
    //sample of black box test, Blazor.
    public class ComponentTest : ComponentTestFixture
    {
        readonly List<string> words = new List<string>() { "milk", "water", "wheat flour", "white pepper" };
        private static SearchState state = SearchState.NotStarted;
        private enum SearchState { NotStarted, Searched, Resetted };
        private Func<IList<string>, Task> searchAction = async (IList<string> s) => { await Task.Delay(200); state=SearchState.Searched; };
        private Func<Task> resetAction = async () => { await Task.Delay(100); state = SearchState.Resetted; };
        [Fact]
        public async Task BlackBoxTest()
        {
            var component = RenderComponent<SearchBox<object>>(
                (nameof(SearchBox<object>.WordList), words.AsReadOnly()),
                (nameof(SearchBox<object>.OnSearch), searchAction),
                (nameof(SearchBox<object>.OnReset), resetAction)
                );

            //suggesting---------------------------------------------

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


            //adding-deleting words----------------------------------

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

            //asynchrounous search -----------------------------------

            var searchButton = component.Find(".keyword-search");
            var resetButton = component.Find(".keyword-reset");
            searchButton.Click();
            await Task.Delay(5);
            resetButton.Click();
            await Task.Delay(120);
            Assert.Equal(SearchState.Resetted, state);
        }
    }
}
