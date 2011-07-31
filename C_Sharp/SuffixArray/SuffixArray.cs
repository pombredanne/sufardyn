using System;
using System.Text;
using CustomStrings;
using Iesi.Collections.Generic;
using Monoid;
using SuffixArray.BinarySearch;
using Treap;
using Treap.Interfaces;
using TreapCS;

namespace SuffixArray
{
    [Serializable]
    public class SuffixArray : AbstractSuffixArray, IStreamEndSuffixArray
    {
        private readonly CustomStringBuilder stringBuilder = new CustomStringBuilder();

        private readonly IAggregateTreap<int> lcp = new AggregateTreap<int>(MinMonoid.Instance);

        private readonly IInverseIntTreap<int> array = new InverseIntTreap<int>();

        private readonly HashedSet<int> boundarySuffix = new HashedSet<int>();

        private int arrayLength;

        readonly CharCollector charCollector = new CharCollector();

        public IAggregateTreap<int> Lcp
        {
            get { return lcp; }
        }

        public IInverseIntTreap<int> Array
        {
            get { return array; }
        }

        public HashedSet<int> BoundarySuffix
        {
            get { return boundarySuffix; }
        }

        protected int ArrayLength
        {
            get { return arrayLength; }
            set { arrayLength = value; }
        }

        protected CharCollector CharCollector
        {
            get { return charCollector; }
        }

        public override int StringLength
        {
            get { return stringBuilder.Length; }
        }

        public override IString String
        {
            get { return stringBuilder; }
        }

        public override int this[int idx]
        {
            get { return array[idx]; }
        }

        protected int SearchInsertPosition(string str)
        {
            int idx = SearchFirstIndex(str);
            if (StringUtils.Compare(stringBuilder.ToString(array[idx], str.Length), str) < 0)
            {
                idx++;
            }

            return idx;
        }

        public virtual void AddCharAtEnd(char sym)
        {
            if (IsEmpty())
            {
                InsertFirstSymbol(sym);
            }
            else
            {
                BoundarySuffixesProcess(sym);
                InsertChar(sym);
            }
        }

        public virtual void AddStringAtEnd(string str)
        {
            foreach (char c in str)
            {
                AddCharAtEnd(c);
            }
        }

        protected bool IsEmpty()
        {
            return arrayLength == 0;
        }

        private void BoundarySuffixesProcess(char sym)
        {
            HashedSet<int> newBoundarySuffix = new HashedSet<int>();

            foreach (int boundIdx in boundarySuffix)
            {
                int idx = array.GetInverse(boundIdx);

                char c = stringBuilder[lcp[idx] + array[idx + 1]];
                if (sym < c)
                {
                }
                else if (sym == c)
                {
                    lcp[idx]++;
                    newBoundarySuffix.Add(boundIdx);
                }
                else
                {
                    MoveSuffix(sym, boundIdx, idx, newBoundarySuffix);
                }
            }

            UpdateBoundarySuffixes(newBoundarySuffix);
        }

        protected void UpdateBoundarySuffixes(HashedSet<int> newBoundarySuffix)
        {
            boundarySuffix.Clear();
            boundarySuffix.AddAll(newBoundarySuffix);
        }

        private void MoveSuffix(char sym, int boundIdx, int idx, HashedSet<int> newBoundarySuffix)
        {
            int curLcp = lcp[idx];

            int rightBound = BinarySearch.BinarySearch.Instance.BinarySearchLast(idx, Math.Min(arrayLength - 1, 1 + idx + charCollector.GetInt(stringBuilder[boundIdx])), new LcpFirstStrategy(this, curLcp, idx)) + 1;
            int newPosition = BinarySearch.BinarySearch.Instance.BinarySearchFirst(idx + 1, Math.Min(rightBound + 1, arrayLength), new SymbolFirstStrategy(this, curLcp, sym)) - 1;

            if (idx > 0)
            {
                int lc = lcp.Aggregate(idx - 1, idx);
                lcp[idx - 1] = lc;
            }

            int newLcp;
            int newLcpPrev;
            if (lcp[newPosition] >= curLcp)
            {
                newLcp = curLcp;
                newLcpPrev = curLcp;
                if (stringBuilder[array[newPosition + 1] + curLcp] == sym)
                {
                    newBoundarySuffix.Add(boundIdx);
                    newLcp++;
                }
            }
            else
            {
                newLcp = lcp[newPosition];
                newLcpPrev = curLcp;
                if (stringBuilder[array[newPosition] + curLcp] == sym)
                {
                    newLcpPrev++;
                }
            }

            lcp.Move(idx, newPosition);
            array.Move(idx, newPosition);
            lcp[newPosition] = newLcp;
            lcp[newPosition - 1] = newLcpPrev;
        }

        protected void InsertChar(char sym)
        {
            int first = SearchInsertPosition(sym.ToString());

            array.Insert(first, arrayLength);
            lcp.Insert(first, 0);
            if (first != arrayLength && stringBuilder[array[first + 1]] == sym)
            {
                lcp[first]++;
                boundarySuffix.Add(arrayLength);
            }

            stringBuilder.Append(sym);
            charCollector.AddChar(sym);
            arrayLength++;
        }

        protected void InsertFirstSymbol(char sym)
        {
            arrayLength++;
            stringBuilder.Append(sym);
            lcp.Insert(0, 0);
            array.Insert(0, 0);
            charCollector.AddChar(sym);
        }

        #region Object override methods

        public override string ToString()
        {
            if (ArrayLength > 500)
            {
                return string.Format("Len = {0}", ArrayLength);
            }

            StringBuilder res = new StringBuilder();
            string str = stringBuilder.ToString();
            res.AppendLine(string.Format("String: {0}", str));
            res.AppendLine(string.Format("Len: {0}", ArrayLength));
            res.AppendLine(string.Format(" i    lcp    array"));

            for (int i = 0; i < array.Count; i++)
            {
                res.AppendLine(string.Format("{0,4} {1,4} {2,4}      {3}", i, lcp[i], array[i], NormalizeString(str.Substring(array[i]))));
            }

            return res.ToString();
        }

        public static string NormalizeString(string str)
        {
            return str.Replace("\r", "\\r").Replace("\n", "\\n");
        }

        #endregion
    }
}