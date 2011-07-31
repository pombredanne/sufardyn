using NUnit.Framework;
using Treap;

namespace Test
{
    [TestFixture]
    public class InverseTreapTest
    {
        [Test]
        public void Test()
        {
            InverseIntTreap<int> treap = new InverseIntTreap<int>();

            for (int i = 0; i < 100; i++)
            {
                treap.Insert(i, i);
            }
            
            for (int i = 0; i < 100; i++)
            {
                Assert.AreEqual(treap.GetInverse(i), i);
            }
        }
    }
}