using System;
using SuffixArray.BinarySearch;

namespace SuffixArray
{
    [Serializable]
    public class DoubleBlockSuffixArray : BlockSuffixArray
    {
        protected override void StringProcess(string str)
        {
            int curBlockSize = 3;
            while (str.Length > 0)
            {
                int addLen = BinarySearch.BinarySearch.Instance.BinarySearchLast(0, Math.Min(str.Length, curBlockSize * 2),
                                                                                 new SuffixArrayContainsStringBinarySearchStrategy(this, str));

                if (addLen <= 1)
                {
                    base.AddCharAtEnd(str[0]);
                    str = str.Substring(1);
                    curBlockSize = 1;
                }
                else
                {
                    AddNewString(str.Substring(0, addLen));
                    str = (str.Substring(addLen));
                    curBlockSize = addLen;
                }
            }
        }
    }
}