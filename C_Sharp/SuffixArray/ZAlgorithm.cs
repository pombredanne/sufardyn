using System;

namespace SuffixArray
{
    public class ZAlgorithm
    {
        private readonly string str;
        private readonly int[] lcp;

        public ZAlgorithm(string str)
        {
            this.str = str;
            lcp = new int[str.Length];
            Evaluate();
        }

        public int[] Lcp
        {
            get { return lcp; }
        }

        public string Str
        {
            get { return str; }
        }

        /// <summary>
        /// Compare suffix with str.
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public int Compare(int i)
        {
            if (i < 0  || i >= str.Length)
            {
                throw new ArgumentOutOfRangeException("i");
            }

            if (i == 0)
            {
                return 0;
            }

            if (lcp[i] == str.Length - i)
            {
                return -1;
            }

            return str[i + lcp[i]].CompareTo(str[lcp[i]]);
        }

        private void Evaluate()
        {
            int len = str.Length;
            lcp[0] = 0;

            int l = 0, r = 0;
            for (int i = 1; i < len; i++)
            {
                if (i > r)
                {
                    int j;
                    for (j = 0; i + j < len && str[i + j] == str[j]; j++)
                    {
                    }
                    lcp[i] = j;
                    l = i;
                    r = i + j - 1;
                }
                else if (lcp[i - l] < r - i + 1)
                {
                    lcp[i] = lcp[i - l];
                }
                else
                {
                    int j;
                    for (j = 1; r + j < len && str[r + j] == str[r - i + j]; j++)
                    {
                    }
                    lcp[i] = r - i + j;
                    l = i;
                    r = r + j - 1;
                }
            }
        }
    }
}