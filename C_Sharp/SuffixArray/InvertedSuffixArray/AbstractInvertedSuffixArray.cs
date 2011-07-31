using System.Text;
using CustomStrings;
using SuffixArray.InvertedSuffixArray.BinarySearch;
using Treap.Interfaces;
using TreapCS;

namespace SuffixArray.InvertedSuffixArray
{
    public abstract class AbstractInvertedSuffixArray : AbstractSuffixArray, IStreamBeginningSuffixArray
    {
        public abstract IInverseIntTreap<int> Array { get; }

        public void AddCharAtBegin(char ch)
        {
            if (StreamString.Length == 0)
            {
                Array.Insert(0, 0);
                StreamString.AddCharAtBegging(ch);

                return;
            }

            int pos = Array.GetInverse(StreamString.Length - 1);

            InvertedSuffixArrayInsertSearchStrategy strategy = new InvertedSuffixArrayInsertSearchStrategy(this, pos, ch);
            int first = global::SuffixArray.BinarySearch.BinarySearch.Instance.BinarySearchFirst(0, StreamString.Length, strategy);
            Array.Insert(first, StreamString.Length);

            StreamString.AddCharAtBegging(ch);
        }

        public void AddStringAtBegin(string str)
        {
            for (int i = 0; i < str.Length; i++)
            {
                AddCharAtBegin(str[str.Length - i - 1]);
            }
        }

        public abstract IBeginningStreamString StreamString { get; }

        public override int StringLength
        {
            get { return String.Length; }
        }

        public override int this[int idx]
        {
            get { return StringLength - Array[idx] - 1; }
        }


        public override string ToString()
        {
            int len = String.Length;
            if (len > 500)
            {
                return string.Format("Len = {0}", len);
            }

            StringBuilder res = new StringBuilder();
            res.AppendLine(string.Format("String: {0}", String));
            res.AppendLine(string.Format("Len: {0}", len));
            res.AppendLine(string.Format(" i      array"));

            for (int i = 0; i < Array.Count; i++)
            {
                res.AppendLine(string.Format("{0,4} {1,4}      {2}", i, Array[i], NormalizeString(StringUtils.Substring(String, len - 1 - Array[i]))));
            }

            return res.ToString();
        }

        public static string NormalizeString(string str)
        {
            return str.Replace("\r", "\\r").Replace("\n", "\\n");
        }
    }
}