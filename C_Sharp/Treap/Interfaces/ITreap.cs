namespace Treap.Interfaces
{
    public interface ITreap<T>
    {
        void Insert(int idx, T data);
        
        T this[int idx] { get; set; }
        
        void Delete(int idx);

        void Move(int source, int dest);
        
        int Count { get; }
    }
}