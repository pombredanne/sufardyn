using System;
using NUnit.Framework;
using SuffixArray;

namespace Test
{
    [TestFixture]
    public class SuffixArrayTest
    {
        [Test]
        public void Test1()
        {
            Assert.That(TestSuffixArray("banana"));
            Assert.That(TestSuffixArray("abracadabra"));
            Assert.That(TestSuffixArray("mississippi"));
            Assert.That(TestSuffixArray("aabcdefaabd"));
            Assert.That(TestSuffixArray("aaa"));
            Assert.That(TestSuffixArray("abcdefac"));
            Assert.That(TestSuffixArray("aaab"));
            Assert.That(TestSuffixArray("abcdefabd"));
        }

        [Test]
        public void Test2()
        {
            Assert.That(TestBlockSuffixArray("banana"));
            Assert.That(TestBlockSuffixArray("abracadabra"));
            Assert.That(TestBlockSuffixArray("mississippi"));
            Assert.That(TestBlockSuffixArray("aabcdefaabd"));
            Assert.That(TestBlockSuffixArray("aaa"));
            Assert.That(TestBlockSuffixArray("abcdefac"));
            Assert.That(TestBlockSuffixArray("aaab"));
            Assert.That(TestBlockSuffixArray("abcdefabd"));
        }

        [Test]
        public void Test3()
        {
            Assert.That(TestSumBlockSuffixArray("banana"));
            Assert.That(TestSumBlockSuffixArray("abracadabra"));
            Assert.That(TestSumBlockSuffixArray("mississippi"));
            Assert.That(TestSumBlockSuffixArray("aabcdefaabd"));
            Assert.That(TestSumBlockSuffixArray("aaa"));
            Assert.That(TestSumBlockSuffixArray("abcdefac"));
            Assert.That(TestSumBlockSuffixArray("aaab"));
            Assert.That(TestSumBlockSuffixArray("abcdefabd"));
        }

        [Test]
        public void TestSuffixArray()
        {
            SuffixArray.SuffixArray array = GetArrayForBanana();

            Assert.That(array.SearchFirstIndex("a") == 0);
            Assert.That(array.SearchFirstIndex("an") == 1);
            Assert.That(array.SearchFirstIndex("na") == 4);
            Assert.That(array.SearchFirstIndex("ba") == 3);
            Assert.That(array.SearchFirstIndex("nana") == 5);

            Assert.That(array.SearchLastIndex("a") == 2);
            Assert.That(array.SearchLastIndex("an") == 2);
            Assert.That(array.SearchLastIndex("na") == 5);
            Assert.That(array.SearchLastIndex("ba") == 3);
            Assert.That(array.SearchLastIndex("nana") == 5);
        }

        private static SuffixArray.SuffixArray GetArrayForBanana()
        {
            var array = new SuffixArray.SuffixArray();
            array.AddStringAtEnd("banana");
            return array;
        }

        private static bool TestSuffixArray(string str)
        {
            var array = new SuffixArray.SuffixArray();
            return AddString(array, str);
        }

        private static bool TestBlockSuffixArray(string str)
        {
            var array = new BlockSuffixArray();
            return AddString(array, str);
        }

        private static bool TestSumBlockSuffixArray(string str)
        {
            var array = new BlockSuffixArray();
            array.AddStringAtEnd(str);
            bool res = IsSuffixArrayCorrect(array);
            if (!res)
            {
                Console.Out.WriteLine("************");
                Console.Out.WriteLine(array);
            }
            return res;
        }

        private static bool AddString(SuffixArray.SuffixArray array, string s)
        {
            bool res = true;
            foreach (char c in s)
            {
                res &= AddChar(array, c);
                if (!res)
                {
                    Console.Out.WriteLine("************");
                    Console.Out.WriteLine(array);
                    break;
                }
            }

            return res;
        }

        private static bool AddChar(SuffixArray.SuffixArray array, char c)
        {
            array.AddCharAtEnd(c);
            return IsSuffixArrayCorrect(array);
        }

        public static bool IsSuffixArrayCorrect(SuffixArray.SuffixArray array)
        {
            try
            {
                if (!(array.StringLength == array.Array.Count && array.StringLength == array.Lcp.Count))
                {
                    return false;
                }

                for (int i = 0; i < array.StringLength - 1; i++)
                {
                    int lcp = array.Lcp[i];

                    if (array.StringLength - array.Array[i] < lcp)
                    {
                        Console.Out.WriteLine("{0} index. Lcp = {1}", i, lcp);
                        return false;
                    }

                    int pos = array.Array[i];
                    int nextPos = array.Array[i + 1];

                    for (int j = 0; j < lcp; j++)
                    {
                        if (array.String[pos + j] != array.String[nextPos + j])
                        {
                            Console.Out.WriteLine("{0} index. Lcp condition doesn't satisfied", i);
                            return false;
                        }
                    }

                    if (nextPos + lcp >= array.StringLength || !(array.String.Length == pos + lcp ||
                                                        array.String[pos + lcp] < array.String[nextPos + lcp]))
                    {
                        Console.Out.WriteLine("{0} index. Lcp condition doesn't satisfied. Last char", i);
                        return false;
                    }

                    if (array.StringLength - lcp == array.Array[i] && !array.BoundarySuffix.Contains(array.Array[i]))
                    {
                        Console.Out.WriteLine("{0} Index should be boundary.", i);
                        return false;
                    }
                }

                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);

                Console.Out.WriteLine(array);

                throw;
            }
        }
    }
}