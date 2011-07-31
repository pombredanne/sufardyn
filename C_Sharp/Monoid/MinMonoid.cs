using System;

namespace Monoid
{
    [Serializable]
    public class MinMonoid : IMonoid<int>
    {
        private static readonly MinMonoid instance = new MinMonoid();

        protected MinMonoid()
        {
        }

        public int NeutralElement
        {
            get { return int.MaxValue; }
        }

        public int Operation(int fst, int snd)
        {
            return Math.Min(fst, snd);
        }

        public static MinMonoid Instance
        {
            get { return instance; }
        }
    }
}