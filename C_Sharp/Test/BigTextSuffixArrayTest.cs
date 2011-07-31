using System;
using System.IO;
using System.Text;
using NUnit.Framework;

namespace Test
{
    [TestFixture]
    public class BigTextSuffixArrayTest
    {
        protected virtual int Length
        {
            get { return 40000;}
        }

        [Test]
        public void SuffixArrayLcpNaturalTest()
        {
            AddByCharTest("nat.txt", new SuffixArray.SuffixArray());
        }

        [Test]
        public void SuffixArrayLcpMinimumTest()
        {
            AddByCharTest("rand.txt", new SuffixArray.SuffixArray());
        }

        [Test, Ignore]
        public void SuffixArrayLcpRegularTest()
        {
            AddByCharTest("reg.txt", new SuffixArray.SuffixArray());
        }

        [Test]
        public void BlockSuffixArrayLcpNaturalTest()
        {
            AddByWholeStringTest("nat.txt", new SuffixArray.BlockSuffixArray());
        }

        [Test]
        public void BlockSuffixArrayLcpMinimumTest()
        {
            AddByWholeStringTest("rand.txt", new SuffixArray.BlockSuffixArray());
        }

        [Test]
        public void BlockSuffixArrayLcpRegularTest()
        {
            AddByWholeStringTest("reg.txt", new SuffixArray.BlockSuffixArray());
        }

        [Test]
        public void DoubleBlockSuffixArrayLcpNaturalTest()
        {
            AddByWholeStringTest("nat.txt", new SuffixArray.DoubleBlockSuffixArray());
        }

        [Test]
        public void DoubleBlockSuffixArrayLcpRegularTest()
        {
            AddByWholeStringTest("reg.txt", new SuffixArray.DoubleBlockSuffixArray());
        }

        protected virtual void AddByCharTest(string fileName, SuffixArray.SuffixArray array)
        {
            string text = CropText(ReadText(fileName));

            text = CropText(text);

            for (int i = 0; i < text.Length; i++)
            {
                array.AddCharAtEnd(text[i]);
            }

            Assert(array);
        }

        protected virtual void AddByWholeStringTest(string fileName, SuffixArray.SuffixArray array)
        {
            string text = ReadText(fileName);

            text = CropText(text);

            array.AddStringAtEnd(text);

            Assert(array);
        }

        protected virtual void Assert(SuffixArray.SuffixArray array)
        {
            NUnit.Framework.Assert.That(SuffixArrayTest.IsSuffixArrayCorrect(array));
        }

        private string CropText(string text)
        {
            if (text.Length > Length)
            {
                text = text.Substring(0, Length);
            }

            return text;
        }

        protected static string ReadText(string fileName)
        {
            return File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + string.Format("/../../Files/{0}", fileName), Encoding.Default);
        }
    }
}