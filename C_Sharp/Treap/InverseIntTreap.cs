using System;
using System.Collections.Generic;
using Treap.Interfaces;

namespace Treap
{
    [Serializable]
    public class InverseIntTreap<T> : IInverseIntTreap<T>
    {
        private readonly InverseTreap<T> treap = new InverseTreap<T>();

        private readonly IDictionary<T, ParentTreapNode<T>> dictionary = new Dictionary<T, ParentTreapNode<T>>();

        public void Move(int source, int dest)
        {
            T value = treap[source];
            treap.Delete(source);
            BaseTreapNode<T> node = treap.InsertNodeInternal(dest, value);
            dictionary[value] = (ParentTreapNode<T>) node;
        }

        public void Insert(int idx, T value)
        {
            BaseTreapNode<T> node = treap.InsertNodeInternal(idx, value);
            dictionary.Add(value, (ParentTreapNode<T>)node);
        }

        public T this[int idx]
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
            dictionary.Remove(treap[idx]);
            treap.Delete(idx);
        }

        public int Count
        {
            get { return treap.Count; }
        }

        public int GetInverse(T elem)
        {
            if (!dictionary.ContainsKey(elem))
            {
                throw new ArgumentOutOfRangeException("elem");
            }

            return treap.GetInverse(dictionary[elem]);
        }
    }
}
