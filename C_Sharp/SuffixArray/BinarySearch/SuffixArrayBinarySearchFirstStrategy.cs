namespace SuffixArray.BinarySearch
{
    public class SuffixArrayBinarySearchFirstStrategy : IBinarySearchStrategy
    {
        private readonly ISuffixArray array;
        private readonly string str;

        public SuffixArrayBinarySearchFirstStrategy(ISuffixArray array, string str)
        {
            this.array = array;
            this.str = str;
        }

        public bool Condition(int pos)
        {
            int offset = array[pos];
            for (int i = 0; i < str.Length; i++)
            {
                if (offset + i >= array.StringLength)
                {
                    return true;
                }

                int compareTo = str[i].CompareTo(array.String[offset + i]);
                if (compareTo < 0)
                {
                    return false;
                }
                if (compareTo > 0)
                {
                    return true;
                }
            }

            return false;
        }
    }
}