using System;

namespace Treap
{
    [Serializable]
    public class BaseTreapNode<T> 
    {
        protected static readonly Random RndPriority = new Random(42);

        private int count;
        private T value;
        private readonly int priority;
        private BaseTreapNode<T> left;
        private BaseTreapNode<T> right;

        public BaseTreapNode(T value)
        {
            this.value = value;
            priority = RndPriority.Next();
            count = 1;
        }

        public T Value
        {
            get
            {
                return value;
            }
            set
            {
                this.value = value;

            }
        }

        public int Priority
        {
            get
            {
                return priority;
            }
			
        }

        public BaseTreapNode<T> Left
        {
            get
            {
                return left;
            }
			
            set { LeftSetter(value); }
        }

        protected virtual void LeftSetter(BaseTreapNode<T> node)
        {
            left = node;
            Update();
        }

        public BaseTreapNode<T> Right
        {
            get
            {
                return right;
            }
			
            set { RightSetter(value); }
        }

        protected virtual void RightSetter(BaseTreapNode<T> node)
        {
            right = node;
            Update();
        }

        public int Count
        {
            get { return count; }
        }

        protected virtual void Update()
        {
            UpdateCount();
        }

        private void UpdateCount()
        {
            count = (left != null ? left.count : 0) + 1 + (right != null ? right.count : 0);
        }


        public virtual BaseTreapNode<T> RotateLeft()
        {
            BaseTreapNode<T> temp  = Right;
            Right           = Right.Left;
            temp.Left       = this;
            return temp;
        }

        public virtual BaseTreapNode<T> RotateRight()
        {
            BaseTreapNode<T> temp  = Left;
            Left            = Left.Right;
            temp.Right      = this;
            return temp;
        }

        public BaseTreapNode<T> DeleteRoot()
        {
            BaseTreapNode<T> temp;
			
            if(Left == null)
            {
                return Right;
            }
			
            if(Right == null)
            {
                return Left;
            }
			
            if(Left.Priority < Right.Priority)
            {
                temp        = RotateRight();
                temp.Right  = DeleteRoot();
            }
            else
            {
                temp        = RotateLeft();
                temp.Left   = DeleteRoot();
            }

            return temp;
        }

        public virtual void UpdateAggregate()
        {
        }
    }
}