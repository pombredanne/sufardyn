using NUnit.Framework;
using SuffixArray;

namespace Test
{
    [TestFixture]
    public class ZAlgorithmTest
    {
        [Test]
        public void Test()
        {
            ZAlgorithm algorithm = new ZAlgorithm("abcdaaabchoabcd");
           
            Assert.That(algorithm.Lcp[1] == 0);
            Assert.That(algorithm.Lcp[2] == 0);
            Assert.That(algorithm.Lcp[3] == 0);
            Assert.That(algorithm.Lcp[4] == 1);
            Assert.That(algorithm.Lcp[5] == 1);
            Assert.That(algorithm.Lcp[6] == 3);
            Assert.That(algorithm.Lcp[7] == 0);
            Assert.That(algorithm.Lcp[8] == 0);
            Assert.That(algorithm.Lcp[9] == 0);
            Assert.That(algorithm.Lcp[10] == 0);
            Assert.That(algorithm.Lcp[11] == 4);
            Assert.That(algorithm.Lcp[12] == 0);
            Assert.That(algorithm.Lcp[13] == 0);
            Assert.That(algorithm.Lcp[14] == 0);

            Assert.That(algorithm.Compare(0) == 0);
            Assert.That(algorithm.Compare(1) > 0);
            Assert.That(algorithm.Compare(2) > 0);
            Assert.That(algorithm.Compare(3) > 0);
            Assert.That(algorithm.Compare(4) < 0);
            Assert.That(algorithm.Compare(5) < 0);
            Assert.That(algorithm.Compare(6) > 0);
            Assert.That(algorithm.Compare(7) > 0);
            Assert.That(algorithm.Compare(8) > 0);
            Assert.That(algorithm.Compare(9) > 0);
            Assert.That(algorithm.Compare(10) > 0);
            Assert.That(algorithm.Compare(11) < 0);
            Assert.That(algorithm.Compare(12) > 0);
            Assert.That(algorithm.Compare(13) > 0);
            Assert.That(algorithm.Compare(14) > 0);
        }

        [Test]
        public void Test1()
        {
            ZAlgorithm algorithm = new ZAlgorithm("-----");

            Assert.That(algorithm.Compare(2) < 0);
        }
    }
}