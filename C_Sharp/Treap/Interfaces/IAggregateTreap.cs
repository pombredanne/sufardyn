namespace Treap.Interfaces
{
    public interface IAggregateTreap<T> : ITreap<T>
    {
        T Aggregate(int l, int r);
    }
}