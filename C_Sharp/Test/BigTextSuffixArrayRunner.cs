using NUnit.Framework;

namespace Test
{
    [TestFixture, Ignore]
    public class BigTextSuffixArrayRunner : BigTextSuffixArrayTest
    {
        private const int stepCount = 10;

        protected override int Length
        {
            get { return 50000; }
        }

        protected override void AddByCharTest(string fileName, SuffixArray.SuffixArray array)
        {
            string fileText = ReadText(fileName);

            for (int step = 1; step <= stepCount; step++)
            {
                string text = fileText.Substring(0, step * Length / stepCount);

                using (new Timer())
                {
                    for (int i = 0; i < text.Length; i++)
                    {
                        array.AddCharAtEnd(text[i]);
                    }
                }
            }
        }

        protected override void AddByWholeStringTest(string fileName, SuffixArray.SuffixArray array)
        {
            string fileText = ReadText(fileName);

            for (int step = 1; step <= stepCount; step++)
            {
                string text = fileText.Substring(0, step * Length / stepCount);

                using (new Timer())
                {
                    array.AddStringAtEnd(text);
                }
            }
        }
    }
}