using NUnit.Framework;
using SuffixArray;
using SuffixArray.InvertedSuffixArray;

namespace Test
{
    [TestFixture]
    public class InvertedSuffixArrayTest
    {
        [Test, TestCaseSource("ContainsCases")]
        public void ContainsTests(string text, string pattern)
        {
            IStreamBeginningSuffixArray array = new InvertedSuffixArray();
            array.AddStringAtBegin(text);

            Assert.That(array.Contains(pattern));
        }

        private static object[] ContainsCases =
            {
                new object[] {"banana", "na"},
                new object[] {"banana", "banana"},
                new object[] {"banana", "b"},
                new object[] {"banana", "a"},
                new object[] {"banana", "n"},
                new object[] {"banana", "ban"},
                new object[] {"banana", "ana"},
                new object[] {"banana", "anan"},
                new object[] {"banana", "nan"},
            };

        [Test, TestCaseSource("DoesntContainsCases")]
        public void DoesntContainsTests(string text, string pattern)
        {
            IStreamBeginningSuffixArray array = new InvertedSuffixArray();
            array.AddStringAtBegin(text);

            Assert.That(!array.Contains(pattern));
        }

        private static object[] DoesntContainsCases =
            {
                new object[] {"banana", "o"},
                new object[] {"banana", "bananaz"},
                new object[] {"banana", "baa"},
                new object[] {"banana", "bb"},
                new object[] {"banana", "nanan"},
                new object[] {"banana", "bban"},
                new object[] {"banana", "az"},
                new object[] {"banana", "ananz"},
                new object[] {"banana", "naz"},
            };
    }
}