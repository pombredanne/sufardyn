using System;

namespace Monoid
{
    [Serializable]
    public class AddMonoid : IMonoid<int>
    {
        private static readonly AddMonoid instance = new AddMonoid();

        protected AddMonoid()
        {
        }

        public int NeutralElement
        {
            get { return 0; }
        }

        public int Operation(int fst, int snd)
        {
            return fst + snd;
        }

        public static AddMonoid Instance
        {
            get { return instance; }
        }
    }
}