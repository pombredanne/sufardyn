using NUnit.Framework;
using Treap;
using Treap.Interfaces;

namespace Test
{
    [TestFixture]
    public class PopedInverseIntTreapTest
    {
        [Test]
        public void Test()
        {
            IInverseIntTreap<int> popedInverseIntTreap = new PopedInverseIntTreap();

            for (int i = 0; i < 10; i++ )
            {
                popedInverseIntTreap.Insert(i, i);
            }

            for (int i = 0; i < 10; i++)
            {
                Assert.That(popedInverseIntTreap.GetInverse(i) == i);
            }

            popedInverseIntTreap.Delete(4);
            popedInverseIntTreap.Delete(4);
            popedInverseIntTreap.Delete(4);
            popedInverseIntTreap.Delete(4);

            Assert.That(popedInverseIntTreap.GetInverse(0) == 0);
            Assert.That(popedInverseIntTreap.GetInverse(1) == 1);
            Assert.That(popedInverseIntTreap.GetInverse(2) == 2);
            Assert.That(popedInverseIntTreap.GetInverse(3) == 3);
            Assert.That(popedInverseIntTreap.GetInverse(4) == 4);
            Assert.That(popedInverseIntTreap.GetInverse(5) == 5);
            Assert.That(popedInverseIntTreap.Count == 6);
        }
    }
}