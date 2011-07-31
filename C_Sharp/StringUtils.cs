using System;
using System.Text;
using CustomStrings;

namespace TreapCS
{
    /// <summary>
    /// Utility string methods
    /// </summary>
    public class StringUtils
    {
        public static int Compare(string fst, string snd)
        {
            int len = Math.Min(fst.Length, snd.Length);
            for (int i = 0; i < len; i++)
            {
                int r = fst[i].CompareTo(snd[i]);
                if (r != 0)
                {
                    return r;
                }
            }

            if (fst.Length < snd.Length)
            {
                return -1;
            }
            
            if (fst.Length > snd.Length)
            {
                return 1;
            }

            return 0;
        }

        public static string Substring(IString s, int index)
        {
            StringBuilder builder = new StringBuilder();
            for (int i = index; i < s.Length; i++)
            {
                builder.Append(s[i]);
            }

            return builder.ToString();
        }
    }
}