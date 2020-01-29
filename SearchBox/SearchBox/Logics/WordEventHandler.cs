using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System.Linq;

namespace KeywordSearchBox
{
    partial class WordHandler
    {
        internal void OnWordEntered(ChangeEventArgs argument)
        {
            wordModel.WordInput = argument.Value as string;
            SetSuggestions();
            ShowSuggestions = (wordModel.Suggestions != null && !string.IsNullOrEmpty(wordModel.WordInput) && Enumerable.Any(wordModel.Suggestions));
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
                    if (string.IsNullOrEmpty(wordModel.WordInput) && wordModel.AddedWords.Count != 0)
                    {
                        DeleteWord(wordModel.AddedWords.Last());
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
