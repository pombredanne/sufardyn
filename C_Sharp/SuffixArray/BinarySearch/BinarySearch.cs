namespace SuffixArray.BinarySearch
{
    public class BinarySearch
    {
        private static readonly BinarySearch instance = new BinarySearch();

        protected BinarySearch()
        {
        }

        public static BinarySearch Instance
        {
            get { return instance; }
        }

        /// <summary>
        /// The first index that a >= opt
        /// <see cref="strategy"/> middle &lt; opt
        /// </summary>
        public int BinarySearchFirst(int left, int right, IBinarySearchStrategy strategy)
        {
            int l = left;
            int r = right;

            while (l < r)
            {
                int m = (l + r) / 2;
                if (strategy.Condition(m))
                {
                    l = m + 1;
                }
                else
                {
                    r = m;
                }
            }

            return l;
        }

        /// <summary>
        /// The last index that a &lt;= opt
        /// <see cref="strategy"/> middle &lt;= opt
        /// </summary>
        public int BinarySearchLast(int left, int right, IBinarySearchStrategy strategy)
        {
            int l = left;
            int r = right;

            while (l < r)
            {
                int m = (l + r + 1) / 2;
                if (strategy.Condition(m))
                {
                    l = m;
                }
                else
                {
                    r = m - 1;
                }
            }

            return l;
        }
    }
}