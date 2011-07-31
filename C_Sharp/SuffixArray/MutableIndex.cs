using SuffixArray.InvertedSuffixArray;

namespace SuffixArray
{
    public class MutableIndex
    {
        readonly MutableInvertedSuffixArray array = new MutableInvertedSuffixArray();
        private const string stopSymbol = "#";

        public void AddString(string value)
        {
            array.AddStringAtBegin(value + stopSymbol);
        }
    }
}