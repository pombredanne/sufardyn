namespace Monoid
{
    /// <summary>
    /// Interface for commutative monoid
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IMonoid<T>
    {
        /// <summary>
        /// Neutral element
        /// </summary>
        T NeutralElement { get; }
        
        /// <summary>
        /// Associative commutative closed operation 
        /// </summary>
        /// <param name="fst"></param>
        /// <param name="snd"></param>
        /// <returns></returns>
        T Operation(T fst, T snd);
    }
}