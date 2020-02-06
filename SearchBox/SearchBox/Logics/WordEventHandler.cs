using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System.Linq;

namespace KeywordSearchBox
{
    partial class WordHandler
    {
        internal void OnWordEntered(ChangeEventArgs argument)
        {
            WordModel.WordInput = argument.Value as string;
            SetSuggestions();
            ShowSuggestions = (WordModel.Suggestions != null && !string.IsNullOrEmpty(WordModel.WordInput) && Enumerable.Any(WordModel.Suggestions));
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
                    if (string.IsNullOrEmpty(WordModel.WordInput) && WordModel.AddedWords.Count != 0)
                    {
                        DeleteWord(WordModel.AddedWords.Last());
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
