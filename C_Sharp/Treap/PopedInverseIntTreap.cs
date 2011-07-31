using System;
using Treap.Interfaces;

namespace Treap
{
    [Serializable]
    public class PopedInverseIntTreap : IInverseIntTreap<int>
    {
        private readonly InverseTreap<int> treap = new InverseTreap<int>();

        private readonly Treap<ParentTreapNode<int>> map = new Treap<ParentTreapNode<int>>();

        public void Move(int source, int dest)
        {
            int value = treap[source];
            treap.Delete(source);
            BaseTreapNode<int> node = treap.InsertNodeInternal(dest, value);
            map.Insert(dest, (ParentTreapNode<int>)node);
        }

        public void Insert(int idx, int value)
        {
            BaseTreapNode<int> node = treap.InsertNodeInternal(idx, value);
            
            map.Insert(idx, (ParentTreapNode<int>)node);
        }

        public int this[int idx]
        {
            get { return treap[idx]; }

            set 
            { 
                Delete(idx);
                Insert(idx, value);
            }
        }

        public void Delete(int idx)
        {
            map.Delete(idx);
            treap.Delete(idx);
        }

        public int Count
        {
            get { return treap.Count; }
        }

        public int GetInverse(int elem)
        {
            if (elem >= map.Count)
            {
                throw new ArgumentOutOfRangeException("elem");
            }

            return treap.GetInverse(map[elem]);
        }
    }
}