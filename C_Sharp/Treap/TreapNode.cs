using System;
using Monoid;

namespace Treap
{
    [Serializable]
    public class TreapNode<T> : BaseTreapNode<T>
    {
        private T aggregate;
        private readonly IMonoid<T> monoid;

        public TreapNode(T value, IMonoid<T> monoid) : base(value)
        {
            aggregate = value;
            this.monoid = monoid;
        }

        public T Aggregate
        {
            get { return aggregate; }
        }

        protected override void Update()
        {
            base.Update();
            UpdateAggregate();
        }

        public override void UpdateAggregate()
        {
            aggregate = Value;
            if (Left != null)
            {
                aggregate = monoid.Operation(aggregate, ((TreapNode<T>)Left).Aggregate);
            }

            if (Right != null)
            {
                aggregate = monoid.Operation(aggregate, ((TreapNode<T>)Right).Aggregate);
            }
        }

        public override BaseTreapNode<T> RotateLeft()
        {
            BaseTreapNode<T> node = base.RotateLeft();
            TreapNode<T> treapNode = node as TreapNode<T>;
            if (treapNode != null)
            {
                treapNode.UpdateAggregate();
            }

            return node;
        }

        public override BaseTreapNode<T> RotateRight()
        {
            BaseTreapNode<T> node = base.RotateRight();
            TreapNode<T> treapNode = node as TreapNode<T>;
            if (treapNode != null)
            {
                treapNode.UpdateAggregate();
            }

            return node;
        }
    }
}