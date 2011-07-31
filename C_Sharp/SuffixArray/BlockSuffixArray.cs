using System;
using System.Collections.Generic;
using CustomStrings;
using Iesi.Collections.Generic;
using SuffixArray.BinarySearch;

namespace SuffixArray
{
    [Serializable]
    public class BlockSuffixArray : SuffixArray 
    {
        public override void AddStringAtEnd(string str)
        {
            if (IsEmpty())
            {
                ProcessWhenEmpty(str);
            }
            else
            {
                StringProcess(str);
            }
        }

        protected void ProcessWhenEmpty(string str)
        {
            InsertFirstSymbol(str[0]);

            StringProcess(str.Substring(1));
        }

        protected virtual void StringProcess(string str)
        {
            while (str.Length > 0)
            {
                int addLen = BinarySearch.BinarySearch.Instance.BinarySearchLast(0, Math.Min(str.Length, ArrayLength), new SuffixArrayContainsStringBinarySearchStrategy(this, str));

                if (addLen <= 1)
                {
                    base.AddCharAtEnd(str[0]);
                    str = str.Substring(1);
                }
                else
                {
                    AddNewString(str.Substring(0, addLen));
                    str = (str.Substring(addLen));
                }
            }
        }

        private static IList<int> Sorted(HashedSet<int> values)
        {
            List<int> res = new List<int>(values);
            res.Sort(Comparison);
            return res;
        }

        private static int Comparison(int x, int y)
        {
            return x.CompareTo(y);
        }

        protected void AddNewString(string str)
        {
            int position = FindInsertPosisition(str);

            int index = Array[position];
            int currentLength = ArrayLength;

            ((CustomStringBuilder)String).Append(str);

            ZAlgorithm zAlgorithm = new ZAlgorithm(str);

            BoundarySuffixesProcess(str, zAlgorithm, index);

            ArrayLength += str.Length;
            
            InsertSuffixes(str, index, currentLength);

            UpdateCharCollector(str);
        }

        private void UpdateCharCollector(string str)
        {
            foreach (char c in str)
            {
                CharCollector.AddChar(c);
            }
        }

        private void BoundarySuffixesProcess(string str, ZAlgorithm zAlgorithm, int index)
        {
            HashedSet<int> newBoundarySuffix = new HashedSet<int>();

            foreach (int boundSuffix in Sorted(BoundarySuffix))
            {
                BoundarySuffixProcess(str, boundSuffix, zAlgorithm, index, newBoundarySuffix);
            }

            UpdateBoundarySuffixes(newBoundarySuffix);
        }

        private int FindInsertPosisition(string str)
        {
            int posisition = SearchInsertPosition(str);

            while (BoundarySuffix.Contains(Array[posisition]))
            {
                posisition++;
            }

            return posisition;
        }

        private void BoundarySuffixProcess(string str, int boundSuffix, ZAlgorithm zAlgorithm, int index, HashedSet<int> newBoundarySuffix)
        {
            int idx = Array.GetInverse(boundSuffix);
            CompareResult compare = Compare(idx, idx + 1, zAlgorithm, index);

            if (compare.Compare >= 1)
            {
                MoveSuffix(boundSuffix, idx, zAlgorithm, index, str, newBoundarySuffix);
            }
            else
            {
                Lcp[idx] = compare.Lcp;
                if (compare.Lcp == ArrayLength + str.Length - Array[idx])
                {
                    newBoundarySuffix.Add(boundSuffix);
                }
            }
        }

        private void MoveSuffix(int boundSuffix, int idx, ZAlgorithm zAlgorithm, int index, string str, HashedSet<int> newBoundarySuffix)
        {
            int first = BinarySearch.BinarySearch.Instance.BinarySearchFirst(idx + 1,
                                                                             Math.Min(ArrayLength + 1, idx + 1 + CharCollector.GetInt(String[boundSuffix])), 
                                                                             new BlockSuffixArrayStrategy(this, idx, zAlgorithm, index)) - 1;

            if (idx > 0)
            {
                Lcp[idx - 1] = Math.Min(Lcp[idx], Lcp[idx - 1]);
            }

            CompareResult compare1 = Compare(idx, first, zAlgorithm, index);

            CompareResult compare2;
            if (first == ArrayLength - 1)
            {
                compare2 = new CompareResult(1, 0);
            }
            else
            {
                compare2 = Compare(idx, first + 1, zAlgorithm, index);
            }

            Array.Move(idx, first);
            Lcp.Move(idx, first);
            Lcp[first - 1] = compare1.Lcp;
            Lcp[first] = compare2.Lcp;

            //boundary support
            if (compare2.Lcp == str.Length + ArrayLength - Array[first])
            {
                newBoundarySuffix.Add(boundSuffix);
            }
        }

        private void InsertSuffixes(string str, int index, int currentLength)
        {
            for (int i = str.Length; i >= 1; i--)
            {
                int z = Array.GetInverse(index - i + str.Length);
                int suffixLength = currentLength - i + str.Length;

                int pos = BinarySearch.BinarySearch.Instance.BinarySearchFirst(0, z, new BlockSuffixArrayInsertStringFirstStrategy(this, z, i, str[str.Length - 1]));

                Array.Insert(pos, suffixLength);
                Lcp.Insert(pos, i);
                BoundarySuffix.Add(suffixLength);
            }
        }

        internal struct CompareResult
        {
            private readonly int compare;
            private readonly int lcp;

            public CompareResult(int compare, int lcp)
            {
                this.compare = compare;
                this.lcp = lcp;
            }

            public int Compare
            {
                get { return compare; }
            }

            public int Lcp
            {
                get { return lcp; }
            }
        }

        internal CompareResult Compare(int i, int j, ZAlgorithm zAlgorithm, int newSuffixIndex)
        {
            if (i >= j)
            {
                throw new ArgumentOutOfRangeException("j");
            }

            int mainLcp = Lcp.Aggregate(i, j - 1);
            int arrayI = Array[i];
            int arrayJ = Array[j];
            int lenI = ArrayLength - arrayI;
            int lenJ = ArrayLength - arrayJ;

            int newSuffixPosition = Array.GetInverse(newSuffixIndex);

            if (mainLcp >= lenI)
            {
                int lenDeltaIJ = arrayI - arrayJ;

                if (lenDeltaIJ < 0)
                {
                    throw new ArgumentOutOfRangeException();
                }

                if (lenDeltaIJ >= zAlgorithm.Str.Length)
                {
                    int posDeltaIJ = Array.GetInverse(ArrayLength - lenDeltaIJ);
                    int lcp = GetLcp(newSuffixPosition, posDeltaIJ);
                    if (lcp >= lenDeltaIJ)
                    {
                        return new CompareResult(-1, lenI + zAlgorithm.Str.Length);
                    }
                    else
                    {
                        if (lcp > zAlgorithm.Str.Length)
                        {
                            return new CompareResult(-1, lenI + zAlgorithm.Str.Length);
                        }
                        else
                        {
                            return new CompareResult(String[newSuffixIndex + lcp].CompareTo(String[ArrayLength - lenDeltaIJ + lcp]), lenI + lcp);
                        }
                    }
                }
                else
                {
                    int posDeltaIJ = Array.GetInverse(ArrayLength - lenDeltaIJ);
                    int lcp = GetLcp(newSuffixPosition, posDeltaIJ);
                    if (lcp >= lenDeltaIJ)
                    {
                        int zDelta = zAlgorithm.Str.Length - lenDeltaIJ;
                        if (zDelta == 0)
                        {
                            return new CompareResult(-1, lenI + zAlgorithm.Str.Length);
                        }
                        else
                        {
                            return new CompareResult(zAlgorithm.Compare(zAlgorithm.Str.Length - zDelta), lenJ + zAlgorithm.Lcp[zAlgorithm.Str.Length - zDelta]);
                        }
                    }
                    else
                    {
                        return new CompareResult(String[newSuffixIndex + lcp].CompareTo(String[ArrayLength - lenDeltaIJ + lcp]), lenI + lcp);
                    }
                }
            }

            return new CompareResult(-1, mainLcp);
        }

        private int GetLcp(int l, int r)
        {
            if (l == r)
            {
                return ArrayLength - Array[l];
            }

            if (l < r)
            {
                return Lcp.Aggregate(l, r - 1);
            }

            return Lcp.Aggregate(r, l - 1);
        }

        private class BlockSuffixArrayStrategy : IBinarySearchStrategy
        {
            private readonly BlockSuffixArray array;
            private readonly int idx;
            private readonly ZAlgorithm algorithm;
            private readonly int newSuffixIndex;

            public BlockSuffixArrayStrategy(BlockSuffixArray array, int idx, ZAlgorithm algorithm, int newSuffixIndex)
            {
                this.array = array;
                this.idx = idx;
                this.algorithm = algorithm;
                this.newSuffixIndex = newSuffixIndex;
            }

            public bool Condition(int pos)
            {
                if (pos == array.ArrayLength)
                {
                    return false;
                }

                return array.Compare(idx, pos, algorithm, newSuffixIndex).Compare > 0;
            }
        }
    }
}