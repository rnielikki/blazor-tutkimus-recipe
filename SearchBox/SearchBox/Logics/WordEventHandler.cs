using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System.Linq;

namespace KeywordSearchBox
{
    partial class WordHandler
    {
        internal void OnWordEntered(ChangeEventArgs argument)
        {
            _wordModel.WordInput = argument.Value as string;
            SetSuggestions();
            ShowSuggestions = (_wordModel.Suggestions != null && !string.IsNullOrEmpty(_wordModel.WordInput) && Enumerable.Any(_wordModel.Suggestions));
        }
        internal void OnKeyInput(KeyboardEventArgs args)
        {
            switch (args.Key)
            {
                case "Enter":
                case "Tab":
                case "ArrowRight":
                    if (ShowSuggestions)
                    {
                        AddWord(SuggestionIterator.Current);
                    }
                    break;
                case "Backspace":
                    if (string.IsNullOrEmpty(_wordModel.WordInput) && _wordModel._addedWords.Count != 0)
                    {
                        DeleteWord(_wordModel._addedWords.Last());
                    }
                    break;
                case "ArrowUp":
                    SuggestionIterator.MoveBefore();
                    break;
                case "ArrowDown":
                    SuggestionIterator.MoveNext();
                    break;
            }
        }
    }
}
