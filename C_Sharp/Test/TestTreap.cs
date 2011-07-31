using System;
using Monoid;
using NUnit.Framework;
using Treap;
using Treap.Interfaces;

namespace Test
{
    [TestFixture]
    public class TestTreap
    {
        [Test]
        public void InsertTest()
        {
            Treap<int> treap = new Treap<int>();

            treap.Insert(0, 1);
            treap.Insert(1, 2);
            treap.Insert(2, 3);
            treap.Insert(3, 4);
            treap.Insert(4, 5);

            treap.Insert(2, 1729);

            Assert.AreEqual(treap[2], 1729);
            Assert.AreEqual(treap[1], 2);
            Assert.AreEqual(treap[3], 3);
        }

        [Test]
        public void MoveTest()
        {
            Treap<int> treap = new Treap<int>();

            treap.Insert(0, 1);
            treap.Insert(1, 2);
            treap.Insert(2, 3);
            treap.Insert(3, 4);
            treap.Insert(4, 5);

            treap.Move(1, 3);
            
            Assert.AreEqual(treap[1], 3);
            Assert.AreEqual(treap[2], 4);
            Assert.AreEqual(treap[3], 2);
        }

        [Test]
        public void RandomAggreagateTreapTest()
        {
            IAggregateTreap<int> fstTreap = new AggregateTreap<int>(AddMonoid.Instance);
            IAggregateTreap<int> sndTreap = new TrivialImplementAggregateTreap(AddMonoid.Instance);
            GenerateTreap(fstTreap, 42);
            GenerateTreap(sndTreap, 42);

            Assert.That(EqAggregateTreap(fstTreap, sndTreap));
        }

        [Test]
        public void ArithmeticSumAggregateTreapTest()
        {
            AggregateTreap<int> treap = new AggregateTreap<int>(AddMonoid.Instance);
            for (int i = 0; i < 1000; i++)
            {
                treap.Insert(i, i + 1);
            }

            bool bo = true;
            for (int i = 0; i < treap.Count; i++)
            {
                for (int j = i; j < treap.Count; j++)
                {
                    bo &= treap.Aggregate(i, j) == (i + j + 2)*(j - i + 1)/2;
                }
            }

            Assert.That(bo);
        }

        [Test]
        public void RandomInverseTreapTest()
        {
            IInverseIntTreap<int> fst = new InverseIntTreap<int>();
            IInverseIntTreap<int> snd = new TrivialImplementIInverseIntTreap();

            GenerateTreap(fst, 42);
            GenerateTreap(snd, 42);

            Assert.That(EqInverseTreap(fst, snd));
        }

        private static bool EqInverseTreap(IInverseIntTreap<int> fstTreap, IInverseIntTreap<int> sndTreap)
        {
            if (fstTreap.Count != sndTreap.Count)
            {
                return false;
            }

            for (int i = 0; i < fstTreap.Count; i++)
            {
                if (fstTreap[i] != sndTreap[i])
                {
                    return false;
                }

                if (fstTreap.GetInverse(i + 1) != sndTreap.GetInverse(i + 1))
                {
                    return false;
                }
            }

            return true;
        }

        private static void GenerateTreap(IInverseIntTreap<int> treap, int seed)
        {
            Random random = new Random(seed);

            for (int i = 0; i < 1000; i++)
            {
                treap.Insert(i, i + 1);
            }

            for (int i = 0; i < 1000; i++)
            {
                int source = random.Next()%treap.Count;
                int dest = random.Next()%treap.Count;
                treap.Move(source, dest);
            }
        }


        private static void GenerateTreap(ITreap<int> treap, int seed)
        {
            Random random = new Random(seed);

            for (int i = 0; i < 1000; i++)
            {
                int idx = random.Next(treap.Count);
                int value = random.Next()%10000;
                treap.Insert(idx, value);
            }

            for (int i = 0; i < 100; i++)
            {
                int idx = random.Next(treap.Count);
                treap.Delete(idx);
            }
        }

        private static bool EqAggregateTreap(IAggregateTreap<int> fstTreap, IAggregateTreap<int> sndTreap)
        {
            if (fstTreap.Count != sndTreap.Count)
            {
                return false;
            }

            for (int i = 0; i < fstTreap.Count; i++)
            {
                if (fstTreap[i] != sndTreap[i])
                {
                    return false;
                }
            }

            for (int i = 0; i < fstTreap.Count; i++)
            {
                for (int j = i; j < fstTreap.Count; j++)
                {
                    int aggregate1 = fstTreap.Aggregate(i, j);
                    int aggregate2 = sndTreap.Aggregate(i, j);
                    if (aggregate1 != aggregate2)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        private static int Height<T>(BaseTreapNode<T> node)
        {
            if (node == null)
            {
                return 0;
            }

            return 1 + Math.Max(Height(node.Left), Height(node.Right));
        }
    }
}