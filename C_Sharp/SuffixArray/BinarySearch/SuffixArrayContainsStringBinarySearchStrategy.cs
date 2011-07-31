namespace SuffixArray.BinarySearch
{
    public class SuffixArrayContainsStringBinarySearchStrategy : IBinarySearchStrategy
    {
        private readonly SuffixArray array;
        private readonly string str;

        public SuffixArrayContainsStringBinarySearchStrategy(SuffixArray array, string str)
        {
            this.array = array;
            this.str = str;
        }

        public bool Condition(int pos)
        {
            return array.Contains(str.Substring(0, pos));
        }
    }
}