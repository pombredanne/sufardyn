using CustomStrings;

namespace SuffixArray
{
    /// <summary>
    /// Suffix array interface
    /// </summary>
    public interface ISuffixArray
    {
        int StringLength { get; }

        IString String { get; }

        int this[int idx]
        {
            get;
        }

        int SearchFirstIndex(string str);

        int SearchLastIndex(string str);

        bool Contains(string str);
    }
}