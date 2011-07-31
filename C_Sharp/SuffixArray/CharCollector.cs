using System;
using System.Collections.Generic;

namespace SuffixArray
{
    [Serializable]
    public class CharCollector
    {
        readonly IDictionary<char, int> dictionary = new Dictionary<char, int>();

        public void AddChar(char c)
        {
            if (!dictionary.ContainsKey(c))
            {
                dictionary[c] = 1;
            }
            else
            {
                dictionary[c]++;
            }
        }

        public int GetInt(char c)
        {
            if (!dictionary.ContainsKey(c))
            {
                return 0;
            }

            return dictionary[c];
        }
    }
}