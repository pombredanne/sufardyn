namespace SuffixArray.BinarySearch
{
    public class LcpFirstStrategy : IBinarySearchStrategy
    {
        private readonly SuffixArray array;
        private readonly int curLcp;
        private readonly int startPos;

        public LcpFirstStrategy(SuffixArray array, int curLcp, int startPos)
        {
            this.array = array;
            this.curLcp = curLcp;
            this.startPos = startPos;
        }

        public bool Condition(int pos)
        {
            return array.Lcp.Aggregate(startPos, pos) >= curLcp;
        }
    }
}