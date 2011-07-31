using NUnit.Framework;
using SuffixArray.BinarySearch;

namespace Test
{
    [TestFixture]
    public class BinarySearchTest
    {
        [Test]
        public void Test()
        {
            int[] a = new[]{1,1,1,2,2,2,3,3,3,4,4,4,5,5,5,6,6,6};

            int first = BinarySearch.Instance.BinarySearchFirst(0, a.Length - 1, new FirstArrayBinarySerachStrategy(a, 3));
            Assert.That(first == 6);

            int last = BinarySearch.Instance.BinarySearchLast(0, a.Length - 1, new LastArrayBinarySerachStrategy(a, 3));
            Assert.That(last == 8);
        }

        private class FirstArrayBinarySerachStrategy : IBinarySearchStrategy
        {
            private readonly int[] a;
            private readonly int value;

            public FirstArrayBinarySerachStrategy(int[] a, int value)
            {
                this.a = a;
                this.value = value;
            }

            public bool Condition(int pos)
            {
                return a[pos] < value;
            }
        }

        private class LastArrayBinarySerachStrategy : IBinarySearchStrategy
        {
            private readonly int[] a;
            private readonly int value;

            public LastArrayBinarySerachStrategy(int[] a, int value)
            {
                this.a = a;
                this.value = value;
            }

            public bool Condition(int pos)
            {
                return a[pos] <= value;
            }
        }
    }
}