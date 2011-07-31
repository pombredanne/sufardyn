using System.Collections.Generic;
using Monoid;
using Treap.Interfaces;

namespace Test
{
    public class TrivialImplementAggregateTreap : IAggregateTreap<int>
    {
        private readonly IMonoid<int> monoid;
        private readonly IList<int> list = new List<int>();

        public TrivialImplementAggregateTreap(IMonoid<int> monoid)
        {
            this.monoid = monoid;
        }

        public void Insert(int idx, int data)
        {
            list.Insert(idx, data);
        }

        public int this[int idx]
        {
            get { return list[idx]; }
            set { list[idx] = value; }
        }

        public int Aggregate(int l, int r)
        {
            int res = monoid.NeutralElement;
            for (int i = l; i <= r; i++)
            {
                res = monoid.Operation(res, list[i]);
            }

            return res;
        }

        public void Move(int source, int dest)
        {
            int value = this[source];
            Delete(source);
            Insert(dest, value);
        }

        public void Delete(int idx)
        {
            list.RemoveAt(idx);
        }

        public int Count
        {
            get { return list.Count; }
        }
    }
}