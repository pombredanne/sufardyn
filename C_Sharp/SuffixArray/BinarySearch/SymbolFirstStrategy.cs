namespace SuffixArray.BinarySearch
{
    public class SymbolFirstStrategy : IBinarySearchStrategy
    {
        private readonly SuffixArray array;
        private readonly int offset;
        private readonly char sym;

        public SymbolFirstStrategy(SuffixArray array, int offset, char sym)
        {
            this.array = array;
            this.offset = offset;
            this.sym = sym;
        }

        public bool Condition(int pos)
        {
            return array.String[array.Array[pos] + offset] < sym;
        }
    }
}