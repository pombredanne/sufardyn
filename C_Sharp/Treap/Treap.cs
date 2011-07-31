using System;
using Treap.Interfaces;

namespace Treap
{
    [Serializable]
    public class Treap<T> : ITreap<T> 
    {
        private int count;
		private BaseTreapNode<T> treapTree;

        public void Insert(int idx, T data)
		{
            if (idx < 0 || idx > count)
            {
                throw new ArgumentException(string.Format("Out of range. Idx = {0}, count = {1}", idx, count), "idx");
            }

		    InsertNodeInternal(idx, data);
		}

        internal BaseTreapNode<T> InsertNodeInternal(int idx, T data)
        {
            BaseTreapNode<T> node = CreateNode(data);
            treapTree = InsertNode(idx, node, treapTree);
            count++;
            return node;
        }


        protected virtual BaseTreapNode<T> CreateNode(T data)
        {
            return new BaseTreapNode<T>(data);
        }

        protected BaseTreapNode<T> TreapTree
        {
            get { return treapTree; }
        }

        public T this[int idx]
	    {
	        get
	        {
                ValidateIdx(idx);
                
	            BaseTreapNode<T> treeNode = treapTree;

	            while (treeNode != null)
	            {
	                int leftTreeCount = treeNode.Left != null ? treeNode.Left.Count : 0;

	                if (idx == leftTreeCount)
	                {
	                    return treeNode.Value;
	                }
	                if (idx < leftTreeCount)
	                {
	                    treeNode = treeNode.Left;
	                }
	                else
	                {
	                    idx -= leftTreeCount + 1;
	                    treeNode = treeNode.Right;
	                }
	            }

	            throw (new ArgumentException("Treap key was not found"));
	        }

	        set
	        {
	            ValidateIdx(idx);
                Set(treapTree, idx, value);
	        }
	    }

        public void Move(int source, int dest)
        {
            T temp = this[source];
            Delete(source);
            Insert(dest, temp);
        }

        public int Count
        {
            get { return count; }
        }


        public void Delete(int idx)
        {
            ValidateIdx(idx);

            treapTree = Delete(idx, treapTree);
            count--;
        }

        protected void ValidateIdx(int idx)
        {
            if (idx < 0 || idx >= count)
            {
                throw new ArgumentException(string.Format("Out of range. Idx = {0}, count = {1}", idx, count), "idx");
            }
        }

        private BaseTreapNode<T> InsertNode(int idx, BaseTreapNode<T> node, BaseTreapNode<T> tree)
        {
            if(tree == null)
            {
                return node;
            }

            int leftTreeCount = GetTreeLeftCount(tree);
            if(idx - leftTreeCount <= 0)
            {
                tree.Left = InsertNode(idx, node, tree.Left);
                if(tree.Left.Priority < tree.Priority)
                {
                    tree = tree.RotateRight();
                }

                return tree;
            }
            {
                tree.Right = InsertNode(idx - leftTreeCount - 1, node, tree.Right);
                if(tree.Right.Priority < tree.Priority)
                {
                    tree = tree.RotateLeft();
                }
                return tree;
            }
        }

        private void Set(BaseTreapNode<T> node, int idx, T value)
        {
            int leftTreeCount = GetTreeLeftCount(node);

            if (idx == leftTreeCount)
            {
                node.Value = value;    
                node.UpdateAggregate();
                return;
            }

            if (idx < leftTreeCount)
            {
                Set(node.Left, idx, value);
                node.UpdateAggregate();
                return;
            }

            Set(node.Right, idx - (leftTreeCount + 1), value);
            node.UpdateAggregate();
        }

        protected virtual BaseTreapNode<T> Delete(int idx, BaseTreapNode<T> tNode)
		{
            int leftTreeCount = GetTreeLeftCount(tNode);

		    if (idx < leftTreeCount)
		    {
		        tNode.Left = Delete(idx, tNode.Left);
                tNode.UpdateAggregate();
                return tNode;
		    }
		    if (idx > leftTreeCount)
		    {
		        tNode.Right = Delete(idx - leftTreeCount - 1, tNode.Right);
                tNode.UpdateAggregate();
                return tNode;
		    }
		    
		    tNode = tNode.DeleteRoot();

		    return tNode;
		}

        protected int GetTreeLeftCount(BaseTreapNode<T> tNode)
	    {
	        return tNode.Left != null ? tNode.Left.Count : 0;
	    }
	}
}
