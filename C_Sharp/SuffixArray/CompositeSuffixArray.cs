using System;
using SuffixArray.BinarySearch;

namespace SuffixArray
{
    [Serializable]
    public class CompositeSuffixArray : BlockSuffixArray
    {
        protected override void StringProcess(string str)
        {
            while (str.Length > 0)
            {
                int addLen = BinarySearch.BinarySearch.Instance.BinarySearchLast(0, str.Length,
                                                                                 new SuffixArrayContainsStringBinarySearchStrategy(this, str));

                int N = 2;
                if (addLen <= N)
                {
                    for (int i = 0; i < N; i++)
                    {
                        base.AddCharAtEnd(str[i]);
                    }

                    str = str.Substring(addLen);
                }
                else
                {
                    AddNewString(str.Substring(0, addLen));
                    str = (str.Substring(addLen));
                }
            }
        }
    }
}