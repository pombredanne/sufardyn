using System;
using System.Collections.Generic;
using Treap.Interfaces;

namespace Test
{
    public class TrivialImplementIInverseIntTreap : IInverseIntTreap<int>
    {
        private readonly IList<int> list = new List<int>();

        public void Insert(int idx, int data)
        {   
            list.Insert(idx, data);
        }

        public int this[int idx]
        {
            get { return list[idx]; }
            set { list[idx] = value; }
        }

        public void Delete(int idx)
        {
            list.RemoveAt(idx);
        }

        public int Count
        {
            get { return list.Count; }
        }

        public int GetInverse(int elem)
        {
            for (int i = 0; i < Count; i++)
            {
                if (this[i] == elem)
                {
                    return i;
                }
            }
            
            throw new ArgumentException();
        }

        public void Move(int source, int dest)
        {
            int t = list[source];
            Delete(source);
            list.Insert(dest, t);
        }
    }
}