namespace Treap.Interfaces
{
    public interface IInverseIntTreap<T> : ITreap<T>
    {
        int GetInverse(T elem);
    }
}