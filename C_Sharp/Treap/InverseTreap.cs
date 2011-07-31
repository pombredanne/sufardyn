using System;

namespace Treap
{
    [Serializable]
    internal class InverseTreap<T> : Treap<T>
    {
        protected override BaseTreapNode<T> CreateNode(T data)
        {
            return new ParentTreapNode<T>(data);
        }

        public int GetInverse(ParentTreapNode<T> node)
        {
            ParentTreapNode<T> input = node;

            int res = GetTreeLeftCount(input);

            while (input.Parent != null)
            {
                ParentTreapNode<T> parent = input.Parent;

                if (ReferenceEquals(parent.Right, input))
                {
                    res += GetTreeLeftCount(parent) + 1;
                }

                input = parent;
            }

            return res;
        }

        //TODO: copy-paste, parent-setter
        protected override BaseTreapNode<T> Delete(int idx, BaseTreapNode<T> tNode)
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

            ParentTreapNode<T> node = (ParentTreapNode<T>)tNode;

            ParentTreapNode<T> parent = node.Parent;

            tNode = tNode.DeleteRoot();
            if (tNode != null)
            {
                ((ParentTreapNode<T>)tNode).Parent = parent;
            }

            return tNode;
        }
    }
}