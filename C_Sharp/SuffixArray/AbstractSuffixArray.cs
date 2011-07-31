using System;
using CustomStrings;
using SuffixArray.BinarySearch;

namespace SuffixArray
{
    public abstract class AbstractSuffixArray : ISuffixArray
    {
        public abstract int StringLength { get; }
        
        public abstract IString String { get; }

        public abstract int this[int idx] 
        { 
            get;
        }

        public int SearchFirstIndex(string str)
        {
            return BinarySearch.BinarySearch.Instance.BinarySearchFirst(0, StringLength - 1, new SuffixArrayBinarySearchFirstStrategy(this, str));
        }

        public int SearchLastIndex(string str)
        {
            return BinarySearch.BinarySearch.Instance.BinarySearchLast(0, StringLength - 1, new SuffixArrayBinarySearchLastStrategy(this, str));
        }

        public bool Contains(string str)
        {
            int index = SearchFirstIndex(str);
            if (String.ToString(this[index], Math.Min(str.Length, StringLength - this[index])) == str)
            {
                return true;
            }

            return false;
        }
    }
}