using System;
using System.Collections.Generic;
using System.Text;
using KeywordSearchBox;
using Xunit;

namespace KeywordSearchBoxTests
{
    public class IteratorTest
    {
        static readonly List<string> target = new List<string>() { "apple", "almond", "banana", "chocolate", "salt", "sugar", "sweet potato" };
        [Fact]
        void ResetTest() {
            SuggestionIterator iterator = new SuggestionIterator(target);
            iterator.MoveNext();
            iterator.MoveNext();
            iterator.MoveNext();
            Assert.NotEqual(iterator.Current, target[0]);
            iterator.Reset();
            Assert.Equal(iterator.Current, target[0]);
        }
        [Fact]
        void MoveBeforeTest() {
            SuggestionIterator iterator = new SuggestionIterator(target);
            iterator.MoveBefore();
            Assert.Equal(iterator.Current, target[^1]);
            iterator.MoveBefore();
            Assert.Equal(iterator.Current, target[^2]);
        }
        [Fact]
        void MoveNextTest() {
            SuggestionIterator iterator = new SuggestionIterator(target);
            while (iterator.MoveNext()) { }
            Assert.Equal(iterator.Current, target[0]);
        }
    }
}
