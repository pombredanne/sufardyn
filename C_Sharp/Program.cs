using System;
using SuffixArray.InvertedSuffixArray;
using Test;

namespace TreapCS
{
    public class Program
    {
        public static void Main()
        {
            InvertedSuffixArray array = new InvertedSuffixArray();

            string text = "banana";

            using (new Timer())
            {
                array.AddStringAtBegin(text);
            }

            Console.Out.WriteLine("array = {0}", array);
        }
    }
}