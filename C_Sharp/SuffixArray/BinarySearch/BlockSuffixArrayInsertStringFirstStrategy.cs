namespace SuffixArray.BinarySearch
{
    public class BlockSuffixArrayInsertStringFirstStrategy : IBinarySearchStrategy
    {
        private readonly BlockSuffixArray array;
        private readonly int curIdx;
        private readonly int strLen;
        private readonly char newChar;

        public BlockSuffixArrayInsertStringFirstStrategy(BlockSuffixArray array, int curIdx,int strLen, char newChar)
        {
            this.array = array;
            this.curIdx = curIdx;
            this.strLen = strLen;
            this.newChar = newChar;
        }

        public bool Condition(int pos)
        {
            bool res = pos < curIdx && (array.Lcp.Aggregate(pos, curIdx - 1) < strLen - 1 || (array.Lcp.Aggregate(pos, curIdx - 1) >= strLen - 1 && array.String[array.Array[pos] + strLen - 1] < newChar));
            return res;
        }
    }
}