using System;

namespace Treap
{
    [Serializable]
    public class ParentTreapNode<T> : BaseTreapNode<T>
    {
        private ParentTreapNode<T> parent;

        public ParentTreapNode(T value) : base(value)
        {
        }

        protected override void LeftSetter(BaseTreapNode<T> node)
        {
            base.LeftSetter(node);
            ParentTreapNode<T> treapNode = node as ParentTreapNode<T>;
            if (treapNode != null)
            {
                treapNode.Parent = this;
            }
        }

        protected override void RightSetter(BaseTreapNode<T> node)
        {
            base.RightSetter(node);
            ParentTreapNode<T> treapNode = node as ParentTreapNode<T>;
            if (treapNode != null)
            {
                treapNode.Parent = this;
            }
        }

        public ParentTreapNode<T> Parent
        {
            get { return parent; }
            set
            {
                if (parent != null && this.Value.Equals(parent.Value))
                {
                    
                }
                parent = value;
            }
        }

        public override BaseTreapNode<T> RotateLeft()
        {
            ParentTreapNode<T> parent1 = parent;

            BaseTreapNode<T> node = base.RotateLeft();
            ((ParentTreapNode<T>) node).Parent = parent1;
            return node;
        }

        public override BaseTreapNode<T> RotateRight()
        {
            ParentTreapNode<T> parent1 = parent;
            BaseTreapNode<T> node = base.RotateRight();
            ((ParentTreapNode<T>)node).Parent = parent1;
            return node;
        }
    }
}