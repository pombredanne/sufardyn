using System;
using Monoid;
using Treap.Interfaces;

namespace Treap
{
    [Serializable]
    public class AggregateTreap<T> : Treap<T>, IAggregateTreap<T>
    {
        private readonly IMonoid<T> monoid;

        public AggregateTreap(IMonoid<T> monoid)
        {
            this.monoid = monoid;
        }

        public T Aggregate(int l, int r)
        {
            ValidateIdx(l);
            ValidateIdx(r);
            if (l > r)
            {
                throw new ArgumentException(string.Format("l > r. l = {0}, r = {1}", l, r), "l");
            }

            return Aggregate(TreapTree, l, r);
        }

        protected override BaseTreapNode<T> CreateNode(T data)
        {
            return new TreapNode<T>(data, monoid);
        }

        private T Aggregate(BaseTreapNode<T> node, int l, int r)
        {
            int leftCount = GetTreeLeftCount(node);

            if (l == r && l == leftCount)
            {
                return node.Value;
            }

            if (l < leftCount && r < leftCount)
            {
                return Aggregate(node.Left, l, r);
            }

            if (l > leftCount && r > leftCount)
            {
                int shift = (leftCount + 1);
                return Aggregate(node.Right, l - shift, r - shift);
            }

            return AggregateFromNode((TreapNode<T>) node, l, r);
        }

        private T AggregateFromNode(TreapNode<T> node, int l, int r)
        {
            T res = node.Value;

            BaseTreapNode<T> lNode = node.Left;
            while (lNode != null)
            {
                int treeLeftCount = GetTreeLeftCount(lNode);

                if (l < treeLeftCount)
                {
                    if (lNode.Right != null)
                    {
                        res = monoid.Operation(res, ((TreapNode<T>)lNode.Right).Aggregate);
                    }

                    res = monoid.Operation(res, lNode.Value);
                    lNode = lNode.Left;
                }
                else if (l == treeLeftCount)
                {
                    if (lNode.Right != null)
                    {
                        res = monoid.Operation(res, ((TreapNode<T>)lNode.Right).Aggregate);
                    }

                    res = monoid.Operation(res, lNode.Value);
                    break;
                }
                else if (l > treeLeftCount)
                {
                    lNode = lNode.Right;
                    l -= treeLeftCount + 1;
                }
            }

            BaseTreapNode<T> rNode = node.Right;
            r -= GetTreeLeftCount(node) + 1;

            while (r >= 0 && rNode != null)
            {
                int treeLeftCount = GetTreeLeftCount(rNode);

                if (r < treeLeftCount)
                {
                    rNode = rNode.Left;
                }
                else if (r == treeLeftCount)
                {
                    if (rNode.Left != null)
                    {
                        res = monoid.Operation(res, ((TreapNode<T>)rNode.Left).Aggregate);
                    }

                    res = monoid.Operation(res, rNode.Value);
                    break;
                }
                else if (r > treeLeftCount)
                {

                    if (rNode.Left != null)
                    {
                        res = monoid.Operation(res, ((TreapNode<T>)rNode.Left).Aggregate);
                    }

                    res = monoid.Operation(res, rNode.Value);

                    rNode = rNode.Right;
                    r -= treeLeftCount + 1;
                }
            }

            return res;
        }
    }
}