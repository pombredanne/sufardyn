using SuffixArray.BinarySearch;

namespace SuffixArray.InvertedSuffixArray.BinarySearch
{
    public class InvertedSuffixArrayInsertSearchStrategy : IBinarySearchStrategy
    {
        private readonly AbstractInvertedSuffixArray array;
        private readonly int posLongestSuffix;
        private readonly char ch;

        public InvertedSuffixArrayInsertSearchStrategy(AbstractInvertedSuffixArray array, int posLongestSuffix, char ch)
        {
            this.array = array;
            this.posLongestSuffix = posLongestSuffix;
            this.ch = ch;
        }

        public bool Condition(int pos)
        {
            int idx = array.Array[pos];

            char fstCh = array.String[array.StringLength - 1 - idx];
            if (fstCh == ch)
            {
                if (idx == 0)
                {
                    return true;
                }

                int inverse = array.Array.GetInverse(idx - 1);

                return posLongestSuffix > inverse;
            }

            return fstCh < ch;
        }
    }
}