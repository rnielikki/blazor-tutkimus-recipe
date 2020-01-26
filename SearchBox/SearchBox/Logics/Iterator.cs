using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace KeywordSearchBox
{
    internal class SuggestionIterator : IEnumerator<string>
    {
        private IList<string> _target;
        public string Current { get; private set; }
        object IEnumerator.Current => Current;
        internal SuggestionIterator(IList<string> target) {
            _target = target;
            Reset();
        }
        public void Dispose()
        {
        }
        public bool MoveBefore()
        {
            int index = _target.IndexOf(Current);
            if(IsValidIndex(index) && --index > -1)
            {
                Current = _target[index];
                return true;
            }
            else
            {
                Current = _target[^1];
                return false;
            }
        }

        public bool MoveNext()
        {
            int index = _target.IndexOf(Current);
            if(IsValidIndex(index) && ++index < _target.Count)
            {
                Current = _target[index];
                return true;
            }
            else
            {
                Current = _target[0];
                return false;
            }
        }
        public void Reset()
        {
            Current = _target.FirstOrDefault();
        }
        public void OnListChanged(IList<string> changedList) {
            _target = changedList;
            Reset();
        }
        private bool IsValidIndex(int index) => (index != -1 && index < _target.Count);
    }

}
